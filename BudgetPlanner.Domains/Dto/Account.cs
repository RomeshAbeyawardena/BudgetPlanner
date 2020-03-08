using BudgetPlanner.Domains.Constants;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Dto
{
    public class Account
    {
        public int Id { get; set; }
        
        [Encrypt(EncryptionKeyConstants.IdentificationData, EncryptionMethod.Encryption, StringCase.Upper)]
        public string EmailAddress { get; set; }

        [Encrypt(EncryptionKeyConstants.Default, EncryptionMethod.Hashing)]
        public IEnumerable<byte> Password { get; set; }

        [Encrypt(EncryptionKeyConstants.PersonalData, EncryptionMethod.Encryption, StringCase.Upper)]
        public string FirstName { get; set; }

        [Encrypt(EncryptionKeyConstants.PersonalData, EncryptionMethod.Encryption, StringCase.Upper)]
        public string LastName { get; set; }
        
        public bool Active { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        
        [Modifier(ModifierFlag.Modified)]
        public DateTimeOffset? Modified { get; set; }
    }
}
