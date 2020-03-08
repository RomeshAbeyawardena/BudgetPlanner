using BudgetPlanner.Domains.Constants;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class RequestToken
    {
        [Key]
        public int Id { get; set; }

        [Encrypt(EncryptionKeyConstants.IdentificationData, EncryptionMethod.Encryption)]
        public byte[] Key { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        [DefaultValue]
        public DateTimeOffset Expires { get; set; }
    }
}
