﻿using BudgetPlanner.Services;
using DNI.Core.Services.Abstraction;
using DataServiceRegistratration = BudgetPlanner.Data.ServiceRegistration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Services.Stores;
using BudgetPlanner.Services.Validators;

namespace BudgetPlanner.Broker
{
    public class ServiceBroker : ServiceBrokerBase
    {
        public ServiceBroker()
        {
            DescribeAssemblies = describe => describe
                .GetAssembly<ServiceRegistration>()
                .GetAssembly<DataServiceRegistratration>();
        }

        public static IdentityBuilder ConfigureIdentity(IdentityBuilder identityBuilder)
        {
            return identityBuilder
                .AddRoles<Role>()
                .AddUserStore<AccountStore>()
                .AddRoleStore<RoleStore>()
                .AddPasswordValidator<AccountPasswordValidator>()
                .AddDefaultTokenProviders();
        }
    }
}
