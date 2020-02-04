using Microsoft.Extensions.Configuration;
using BudgetPlanner.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Constants;

namespace BudgetPlanner.Domains
{
    public class ApplicationSettings
    {
        public ApplicationSettings(IConfiguration configuration)
        {
            configuration.Bind(this);
            DefaultConnectionString = configuration.GetConnectionString(DataConstants.DefaultConnectionStringKey);
        }

        public int SessionExpiryInMinutes { get; set; }
        public IDictionary<string, EncryptionKey> EncryptionKeys { get; set; }
        public string DefaultConnectionString { get; set; }
    }
}
