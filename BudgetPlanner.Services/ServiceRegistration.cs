using AutoMapper;
using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Services.Providers;
using DNI.Shared.Contracts;
using DNI.Shared.Contracts.Options;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DNI.Shared.Services.Extensions;
using DNI.Shared.Services.Providers;
using DNI.Shared.Contracts.Providers;
using BudgetPlanner.Domains.Constants;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BudgetPlanner.Services
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IServiceRegistrationOptions serviceRegistrationOptions)
        {
            services
                .AddSingleton<ApplicationSettings>()
                .AddSingleton<IMarkdownToHtmlService, MarkdownToHtmlService>()
                .RegisterCryptographicCredentialsFactory<AppCryptographicCredentials>(RegisterCryptographicCredentialsFactory)
                .AddTransient<ICookieValidationService, CookieValidationService>()
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<IBudgetPlannerCacheProvider, BudgetPlannerCacheProvider>()
                .AddTransient<ITransactionProvider, TransactionProvider>()
                .AddTransient<IBudgetPlannerService, BudgetPlannerService>()
                .AddTransient<ITransactionService, TransactionService>()
                .AddTransient<ITransactionTypeService, TransactionTypeService>()
                .AddTransient<ITransactionLedgerService, TransactionLedgerService>()
                .AddMediatR(Assembly.GetAssembly(typeof(ServiceRegistration)))
                .AddAutoMapper(Assembly.GetAssembly(typeof(DomainProfile)));
        }

        private void RegisterCryptographicCredentialsFactory(ISwitch<string, ICryptographicCredentials> factory, 
            ICryptographyProvider cryptographyProvider, IServiceProvider services)
        {
            var applicationSettings = services.GetRequiredService<ApplicationSettings>();

            if(!applicationSettings.EncryptionKeys
                .TryGetValue(EncryptionKeyConstants.IdentificationData, out var identificationEncryptionKey))
                throw new KeyNotFoundException();

            if(!applicationSettings.EncryptionKeys
                .TryGetValue(EncryptionKeyConstants.PersonalData, out var personalDataEncryptionKey))
                throw new KeyNotFoundException();

            if(!applicationSettings.EncryptionKeys
                .TryGetValue(EncryptionKeyConstants.Default, out var defaultDataEncryptionKey))
                throw new KeyNotFoundException();


            factory
                .CaseWhen(EncryptionKeyConstants.Default,
                    cryptographyProvider.GetCryptographicCredentials<AppCryptographicCredentials>(KeyDerivationPrf.HMACSHA512,
                    Encoding.UTF8, defaultDataEncryptionKey.Password, defaultDataEncryptionKey.Salt,
                        defaultDataEncryptionKey.Iterations, 32,
                        Convert.FromBase64String(defaultDataEncryptionKey.InitialVector)))
                .CaseWhen(EncryptionKeyConstants.IdentificationData, 
                    cryptographyProvider
                        .GetCryptographicCredentials<AppCryptographicCredentials>(KeyDerivationPrf.HMACSHA512, 
                            Encoding.UTF8, identificationEncryptionKey.Password, identificationEncryptionKey.Salt, 
                            identificationEncryptionKey.Iterations, 32, 
                            Convert.FromBase64String(identificationEncryptionKey.InitialVector)))
                .CaseWhen(EncryptionKeyConstants.PersonalData, 
                    cryptographyProvider
                        .GetCryptographicCredentials<AppCryptographicCredentials>(KeyDerivationPrf.HMACSHA512, 
                            Encoding.UTF8, personalDataEncryptionKey.Password, personalDataEncryptionKey.Salt, 
                            personalDataEncryptionKey.Iterations, 32, 
                            Convert.FromBase64String(personalDataEncryptionKey.InitialVector)))
                ;
        }
    }
}
