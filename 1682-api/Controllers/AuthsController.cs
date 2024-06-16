using AutoMapper;
using Azure.Core;
using DataServices.Data;
using DataServices.Interfaces;
using DataServices.Repositories;
using DataServices.Services.EmailServices;
using DataServices.Services.JwtServices;
using DataServices.Services.PasswordServices;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Auth;
using Models.DTO.Password;
using Models.Entities;
using Org.BouncyCastle.Crypto.Generators;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _1682_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public AuthsController(IUnitOfWorks unitOfWorks, IMapper mapper, IPasswordService passwordService, IJwtService jwtService, IEmailService emailService) : base(unitOfWorks, mapper)
        {
            _passwordService = passwordService;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            var hashed = _passwordService.HashPasswordV2(userRegistrationDTO.Password!, RandomNumberGenerator.Create());
            var passwordHash = Convert.ToBase64String(hashed);

            userRegistrationDTO.Password = passwordHash;

            var result = _mapper.Map<Users>(userRegistrationDTO);

            var addResult = await _unitOfWorks.Auths.CreateAccountAsync(result);

            if (!addResult)
            {
                return BadRequest("Email or phone number already exists.");
            }
            await _unitOfWorks.CompleteAsync();

            return Ok("User registered successfully.");
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
        {
            var existing_user = await _unitOfWorks.Auths.FindByEmailAsync(userLoginDTO.Email);
            if (existing_user == null)
            {
                return BadRequest("Invalid email.");
            }

            var hashedPassword = Convert.FromBase64String(existing_user.password_hash);
            if (!_passwordService.VerifyHashedPasswordV2(hashedPassword, userLoginDTO.Password!))
            {
                return BadRequest("Password is not correct.");
            }

            var jwtToken = _jwtService.GenerateJwtToken(existing_user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var result = new TokenResponse
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };


            return Ok(result);
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var existing_user = await _unitOfWorks.Auths.FindByEmailAsync(email);
            if (existing_user != null)
            {
                var token = _jwtService.GeneratePasswordResetTokenAsync(existing_user);
                var encodedToken = HttpUtility.UrlEncode(token);
                var encodedEmail = HttpUtility.UrlEncode(email);
                var forgotPasswordUrl = $"http://localhost:3000/reset-password?token={encodedToken}&email={encodedEmail}";
                var message = new Message(new string[] { email! }, "[System] Change Password at test.localhoast.vn", $"Hello,\n\nPlease click the following link to reset your password:\n\n{forgotPasswordUrl}");
                _emailService.SendEmail(message);

                return Ok("Change Password requrest is sent to your email. Please Open your email and click to the link.");
            }
            return BadRequest("Could not send link to email, please try against.");
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            var existing_user = await _unitOfWorks.Auths.FindByEmailAsync(model.Email);

            if (existing_user != null)
            {
                var hashed = _passwordService.HashPasswordV2(model.Password!, RandomNumberGenerator.Create());
                var passwordHash = Convert.ToBase64String(hashed);

                var resetPasswordResult = await _unitOfWorks.Auths.ResetPasswordAsync(existing_user, model.Token, passwordHash);

                if (!resetPasswordResult)
                { 
                    return BadRequest();
                }
            }
            return Ok("Password has been changed.");
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenResponse refreshTokenDTO)
        {
            // Check if model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if refresh token is valid
            var refreshToken = await _unitOfWorks.Auths.CheckRefreshTokenAsync(refreshTokenDTO.RefreshToken);
            if (!refreshToken)
            {
                return BadRequest("Invalid refresh token.");
            }

            // Generate new access token
            var accessToken = _jwtService.GenerateAccessTokenFromRefreshToken(refreshTokenDTO.RefreshToken);

            return Ok(accessToken);
        }



    }
}
