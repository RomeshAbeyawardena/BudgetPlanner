using BudgetPlanner.Domains.Constants;
using DNI.Shared.Contracts.Enumerations;
using DNI.Shared.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    //Entity framework does not accept IEnumerable byte properties
    #pragma warning disable CA1819
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Encrypt(EncryptionKeyConstants.IdentificationData, EncryptionMethod.Encryption)]
        public byte[] EmailAddress { get; set; }

        [Encrypt(EncryptionKeyConstants.PersonalData, EncryptionMethod.Hashing)]
        public byte[] Password { get; set; }

        [Encrypt(EncryptionKeyConstants.PersonalData, EncryptionMethod.Encryption)]
        public byte[] FirstName { get; set; }

        [Encrypt(EncryptionKeyConstants.PersonalData, EncryptionMethod.Encryption)]
        public byte[] LastName { get; set; }
        
        public bool Active { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        
        [Modifier(ModifierFlag.Modified)]
        public DateTimeOffset? Modified { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
