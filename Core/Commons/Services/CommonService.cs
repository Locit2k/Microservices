using Core.Commons.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commons.Services
{
    public class CommonService : ICommonService
    {
        public string HashPassword(object user, string password)
        {
            var _hasher = new PasswordHasher<object>();
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(object user, string hashedPassword, string inputPassword)
        {
            var _hasher = new PasswordHasher<object>();
            var verificationResult = _hasher.VerifyHashedPassword(user, hashedPassword, inputPassword);
            return verificationResult == PasswordVerificationResult.Success;
        }
    }
}
