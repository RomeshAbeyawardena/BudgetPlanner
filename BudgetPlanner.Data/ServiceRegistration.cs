using BudgetPlanner.Domains;
using DNI.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DNI.Core.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts.Options;

namespace BudgetPlanner.Data
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IServiceRegistrationOptions serviceRegistrationOptions)
        {
            services
                .AddDbContextPool<BudgetPlannerDbContext>((serviceProvider, setup) => { 
                var applicationSettings = serviceProvider.GetRequiredService<ApplicationSettings>();
                setup
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(applicationSettings.DefaultConnectionString); 
            }).RegisterDbContentRepositories<BudgetPlannerDbContext>(ServiceLifetime.Transient, 
                typeof(Account), typeof(Role), typeof(AccountRole), typeof(Claim),
                typeof(AccountClaim), typeof(Budget), typeof(Transaction), typeof(TransactionType),
                typeof(TransactionLedger), typeof(RequestToken), typeof(AccessType), typeof(AccountAccess),
                typeof(Tag), typeof(TransactionTag));
        }
    }
}
