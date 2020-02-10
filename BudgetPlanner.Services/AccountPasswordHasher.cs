using BudgetPlanner.Domains.Dto;
using DNI.Shared.Contracts.Providers;
using DNI.Shared.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class AccountPasswordHasher : IPasswordHasher<Account>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public string HashPassword(Account user, string password)
        {
            user.Password = password.GetBytes(Encoding.UTF8);
            var encrypted = _encryptionProvider.Encrypt<Account, Domains.Data.Account>(user).Result;
            return Convert.ToBase64String(encrypted.Password);
        }

        public PasswordVerificationResult VerifyHashedPassword(Account user, string hashedPassword, string providedPassword)
        {
            var hashedProvidedPassword = HashPassword(user, providedPassword);
            
            if(hashedProvidedPassword.Equals(hashedPassword))
                return PasswordVerificationResult.Success;

            return PasswordVerificationResult.Failed;
        }

        public AccountPasswordHasher(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }
    }
}
