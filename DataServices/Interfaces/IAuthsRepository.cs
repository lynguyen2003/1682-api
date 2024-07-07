using Models.DTO.Auth;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Interfaces
{
    public interface IAuthsRepository
    {
        Task<Users> FindAsync(int id);
        Task<Users> FindByEmailAsync(string email);
        Task<bool> CreateAccountAsync(Users user);
        Task<bool> CheckPasswordAsync(Users user, string password);
        Task<bool> CheckRefreshTokenAsync(string refreshToken);
        Task<bool> ResetPasswordAsync(Users user, string token, string password);



        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> VerifyEmailAsync(string email);
        Task<bool> VerifyUsernameAsync(string username);
        Task<bool> VerifyEmailVerificationTokenAsync(string email, string token);
        Task<bool> VerifyPasswordResetTokenAsync(string email, string token);
        Task<bool> UpdateEmailAsync(string email, string newEmail);
        Task<bool> UpdateUsernameAsync(string email, string newUsername);
        Task<bool> DeleteAccountAsync(string email);
    }
}
