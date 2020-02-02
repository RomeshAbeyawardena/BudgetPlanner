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

        [Encrypt(EncryptionKeyConstants.PersonalData)]
        public byte[] EmailAddress { get; set; }

        public byte[] Password { get; set; }

        [Encrypt(EncryptionKeyConstants.IdentificationData)]
        public byte[] FirstName { get; set; }

        [Encrypt(EncryptionKeyConstants.IdentificationData)]
        public byte[] LastName { get; set; }
        
        public bool IsActive { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        
        [Modifier(ModifierFlag.Modified)]
        public DateTimeOffset? Modified { get; set; }
    }
}
