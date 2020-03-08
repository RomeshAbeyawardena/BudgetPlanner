using BudgetPlanner.Domains;
using DNI.Core.Contracts;

using Microsoft.Extensions.DependencyInjection;
using DNI.Core.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts.Options;
using DNI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Data
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IServiceRegistrationOptions serviceRegistrationOptions)
        {
            services
                .RegisterDbContextRepositories<BudgetPlannerDbContext>(
                configure =>
                {
                    configure.ServiceLifetime = ServiceLifetime.Transient;
                    configure.UseDbContextPool = true;
                    configure.DbContextServiceProviderOptions = (serviceProvider, setup) =>
                    {
                        var applicationSettings = serviceProvider.GetRequiredService<ApplicationSettings>();
                        setup
                            .EnableSensitiveDataLogging()
                            .UseSqlServer(applicationSettings.DefaultConnectionString);
                    };
                    configure.DescribedEntityTypes = TypesDescriptor
                        .Describe<Account>()
                        .Describe<AccountClaim>()
                        .Describe<Role>()
                        .Describe<AccountRole>()
                        .Describe<AccountAccess>()
                        .Describe<Transaction>()
                        .Describe<TransactionType>()
                        .Describe<TransactionLedger>()
                        .Describe<Claim>()
                        .Describe<AccessType>()
                        .Describe<RequestToken>()
                        .Describe<Budget>();
                });
        }
    }
}
