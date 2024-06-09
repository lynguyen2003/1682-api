using DataServices.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Request.Auth;
using Models.Entities;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _1682_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            // Check if model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if email is already registered
            var existingUser = _context.Users.FirstOrDefault(u => u.email == userRegistrationDTO.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already registered.");
            }

            // Check if phone number is already registered
            existingUser = _context.Users.FirstOrDefault(u => u.phone_number == userRegistrationDTO.PhoneNumber);
            if (existingUser != null)
            {
                return BadRequest("Phone number is already registered.");
            }

            // Hash password
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: userRegistrationDTO.Password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

            // Create new user
            var newUser = new Users
            {
                email = userRegistrationDTO.Email,
                phone_number = userRegistrationDTO.PhoneNumber,
                password_hash = passwordHash,
                is_verify = false,
                Timestamp = DateTime.Now
            };

            // Add user to database
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }

    }
}
