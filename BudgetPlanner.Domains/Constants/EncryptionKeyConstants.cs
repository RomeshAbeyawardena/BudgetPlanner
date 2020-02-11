using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Constants
{
    public static class EncryptionKeyConstants
    {
        public const string Api = nameof(Api);
        public const string PersonalData = nameof(PersonalData);
        public const string IdentificationData = nameof(IdentificationData);
        public const string Default = nameof(Default);
        public const int AesKeySize = 32;
    }
}
