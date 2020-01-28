using AutoMapper;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using DNI.Shared.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            services
                .AddSingleton<ApplicationSettings>()
                .AddTransient<IBudgetPlannerService, BudgetPlannerService>()
                .AddTransient<ITransactionService, TransactionService>()
                .AddMediatR(Assembly.GetAssembly(typeof(ServiceRegistration)))
                .AddAutoMapper(Assembly.GetAssembly(typeof(DomainProfile)));
        }
    }
}
