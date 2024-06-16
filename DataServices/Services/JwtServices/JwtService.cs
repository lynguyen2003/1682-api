using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.JwtServices
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(Users user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtConfig = new JwtConfig(); 
            var secretKey = jwtConfig.Secret;
            var key = Encoding.UTF8.GetBytes(secretKey);

            //Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.email),
                    new Claim(JwtRegisteredClaimNames.Email, user.email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)),
                Issuer = _configuration.GetSection("JwtConfig:Issuer").Value,
                Audience = _configuration.GetSection("JwtConfig:Audience").Value,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                user_id = user.id,
                token = GenerateRefreshToken(),
                jwt_id = token.Id,
                is_used = false,
                is_revoked = false,
                added_date = DateTime.Now,
                expiry_date = DateTime.Now.AddMonths(6)
            };

            return jwtToken;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string GenerateAccessTokenFromRefreshToken(string refreshToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtConfig = new JwtConfig();
            var secretKey = jwtConfig.Secret;
            var key = Encoding.UTF8.GetBytes(secretKey);

            //Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public string GenerateEmailVerificationToken(Users user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtConfig = new JwtConfig();
            var secretKey = jwtConfig.Secret;
            var key = Encoding.UTF8.GetBytes(secretKey);

            //Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.email),
                    new Claim(JwtRegisteredClaimNames.Email, user.email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:EmailVerificationExpiryTimeFrame").Value)),
                Issuer = _configuration.GetSection("JwtConfig:Issuer").Value,
                Audience = _configuration.GetSection("JwtConfig:Audience").Value,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public string GeneratePasswordResetTokenAsync(Users user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtConfig = new JwtConfig();
            var secretKey = jwtConfig.Secret;
            var key = Encoding.UTF8.GetBytes(secretKey);

            //Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.email),
                    new Claim(JwtRegisteredClaimNames.Email, user.email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:PasswordResetExpiryTimeFrame").Value)),
                Issuer = _configuration.GetSection("JwtConfig:Issuer").Value,
                Audience = _configuration.GetSection("JwtConfig:Audience").Value,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
