using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.JwtServices
{
    public interface IJwtService
    {
        string GenerateJwtToken(Users user);
        string GenerateEmailVerificationToken(Users user);
        string GeneratePasswordResetTokenAsync(Users user);
        string GenerateRefreshToken();
        string GenerateAccessTokenFromRefreshToken(string refreshToken);

    }
}
