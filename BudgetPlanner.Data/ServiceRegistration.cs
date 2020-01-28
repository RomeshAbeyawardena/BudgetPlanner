﻿using BudgetPlanner.Domains;
using DNI.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DNI.Shared.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Data;

namespace BudgetPlanner.Data
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            services
                .AddDbContextPool<BudgetPlannerDbContext>((serviceProvider, setup) => { 
                var applicationSettings = serviceProvider.GetRequiredService<ApplicationSettings>();
                setup
                    .UseSqlServer(applicationSettings.DefaultConnectionString); 
            }).RegisterDbContentRepositories<BudgetPlannerDbContext>(ServiceLifetime.Transient, 
                typeof(Budget), 
                typeof(Transaction));
        }
    }
}
