using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.PasswordServices
{
    public interface IPasswordService
    {
        byte[] HashPasswordV2(string password, RandomNumberGenerator rng);
        bool VerifyHashedPasswordV2(byte[] hashedPassword, string password);
    }
}
