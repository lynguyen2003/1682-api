using DataServices.Data;
using DataServices.Interfaces;
using DataServices.Services.PasswordServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.DTO.Auth;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Repositories
{
    public class AuthsRepository : GenericRepository<Users>, IAuthsRepository
    {
        public AuthsRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<Users> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Users> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<bool> CreateAccountAsync(Users user)
        {
            if (await _context.Users.AnyAsync(u => u.email == user.email || u.phone_number == user.phone_number))
            {
                return false;
            }

            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckRefreshTokenAsync(string refreshToken)
        {
            try
            {
                await _context.RefreshTokens.AnyAsync(rt => rt.token == refreshToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking refresh token.");
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(Users user, string token, string password)
        {
            if (user == null)
            {
                return false;
            }

            //TODO: Check if token is valid

            user.password_hash = password;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }



        /*===========================================================================*/


        public async Task<bool> CheckPasswordAsync(Users user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAccountAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailExistsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEmailAsync(string email, string newEmail)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUsernameAsync(string email, string newUsername)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerifyEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.email == email);
        }

        public Task<bool> VerifyEmailVerificationTokenAsync(string email, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyPasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyPasswordResetTokenAsync(string email, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
