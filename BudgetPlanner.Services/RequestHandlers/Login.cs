using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Contracts;
using DNI.Shared.Contracts.Providers;
using DNI.Shared.Domains;
using DNI.Shared.Shared.Extensions;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class Login : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IMapperProvider _mapperProvider;
        private readonly IAccountService _accountService;
        private readonly IEncryptionProvider _encryptionProvider;

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Password = Convert.ToBase64String(
                request.Password.GetBytes(Encoding.UTF8).ToArray());

                var account = _mapperProvider.Map<LoginRequest, Account>(request);
                var encryptedAccount = await _encryptionProvider.Encrypt<Account, Domains.Data.Account>(account);

                var foundAccount = await _accountService.GetAccount(encryptedAccount.EmailAddress);

                if(foundAccount == null)
                    throw new NullReferenceException();

                if(!foundAccount.Password.SequenceEqual(encryptedAccount.Password))
                    throw new UnauthorizedAccessException();

                account = await _encryptionProvider.Decrypt<Domains.Data.Account, Account>(foundAccount);

                return Response.Success<LoginResponse>(account);
            }
            catch (Exception ex)
            { 
                var exceptionType = ex.GetType();
                
                if(exceptionType == typeof(NullReferenceException) 
                    || exceptionType == typeof(UnauthorizedAccessException))
                    return new LoginResponse { IsSuccessful = false, Errors = new []{ 
                        new ValidationFailure(nameof(Account.EmailAddress), "EmailAddress or password invalid") } };

                throw;
            }
        }

        public Login(IMapperProvider mapperProvider, IEncryptionProvider encryptionProvider, IAccountService accountService)
        {
            _mapperProvider = mapperProvider;
            _accountService = accountService;
            _encryptionProvider = encryptionProvider;
        }
    }
}
