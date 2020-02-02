using BudgetPlanner.Domains.Constants;
using DNI.Shared.Contracts.Enumerations;
using DNI.Shared.Services.Attributes;
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
        
        [Encrypt(EncryptionKeyConstants.IdentificationData)]
        public string EmailAddress { get; set; }

        public IEnumerable<byte> Password { get; set; }

        [Encrypt(EncryptionKeyConstants.PersonalData)]
        public string FirstName { get; set; }

        [Encrypt(EncryptionKeyConstants.PersonalData)]
        public string LastName { get; set; }
        
        public bool Active { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        
        [Modifier(ModifierFlag.Modified)]
        public DateTimeOffset? Modified { get; set; }
    }
}
