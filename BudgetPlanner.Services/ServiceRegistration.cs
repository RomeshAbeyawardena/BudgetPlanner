using AutoMapper;
using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Services.Providers;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Options;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DNI.Core.Services.Extensions;
using DNI.Core.Services.Providers;
using DNI.Core.Contracts.Providers;
using BudgetPlanner.Domains.Constants;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Services.Stores;
using BudgetPlanner.Services.Validators;
using BudgetPlanner.Contracts.HttpServices;
using BudgetPlanner.Services.HttpServices;

namespace BudgetPlanner.Services
{
    public class ServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services, IServiceRegistrationOptions serviceRegistrationOptions)
        {
            services
                .AddSingleton<ApplicationSettings>()
                .RegisterCryptographicCredentialsFactory<AppCryptographicCredentials>(RegisterCryptographicCredentialsFactory)
                .AddSingleton<ICmsHttpService, CmsHttpService>()
                .AddTransient<ICookieValidationService, CookieValidationService>()
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<IBudgetPlannerCacheProvider, BudgetPlannerCacheProvider>()
                .AddTransient<ICmsContentProvider, CmsContentProvider>()
                .AddTransient<IClaimService, ClaimService>()
                .AddTransient<IRoleService, RoleService>()
                .AddTransient<ITransactionProvider, TransactionProvider>()
                .AddTransient<IBudgetPlannerService, BudgetPlannerService>()
                .AddTransient<ITransactionService, TransactionService>()
                .AddTransient<ITransactionTypeService, TransactionTypeService>()
                .AddTransient<ITransactionLedgerService, TransactionLedgerService>()
                .AddTransient<IRequestTokenService, RequestTokenService>()
                .AddTransient<IAccountAccessService, AccountAccessService>()
                .AddTransient<SignInManager<Domains.Dto.Account>>()
                .AddTransient<IPasswordHasher<Domains.Dto.Account>, AccountPasswordHasher>()
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
                        defaultDataEncryptionKey.Iterations, EncryptionKeyConstants.AesKeySize,
                        Convert.FromBase64String(defaultDataEncryptionKey.InitialVector)))
                .CaseWhen(EncryptionKeyConstants.IdentificationData, 
                    cryptographyProvider
                        .GetCryptographicCredentials<AppCryptographicCredentials>(KeyDerivationPrf.HMACSHA512, 
                            Encoding.UTF8, identificationEncryptionKey.Password, identificationEncryptionKey.Salt, 
                            identificationEncryptionKey.Iterations, EncryptionKeyConstants.AesKeySize, 
                            Convert.FromBase64String(identificationEncryptionKey.InitialVector)))
                .CaseWhen(EncryptionKeyConstants.PersonalData, 
                    cryptographyProvider
                        .GetCryptographicCredentials<AppCryptographicCredentials>(KeyDerivationPrf.HMACSHA512, 
                            Encoding.UTF8, personalDataEncryptionKey.Password, personalDataEncryptionKey.Salt, 
                            personalDataEncryptionKey.Iterations, EncryptionKeyConstants.AesKeySize, 
                            Convert.FromBase64String(personalDataEncryptionKey.InitialVector)));
        }
    }
}
