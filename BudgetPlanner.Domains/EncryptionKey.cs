using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains
{
    public class EncryptionKey
    {
        public string Salt { get; set; }
        public string Password { get; set; }
        public int Iterations { get; set; }
        public string InitialVector { get; set; }
    }
}
