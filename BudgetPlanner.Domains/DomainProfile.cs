using AutoMapper;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Dto.Budget, BudgetPanelDashboardItemViewModel>();
            CreateMap<BudgetPanelDashboardListViewModel, RetrieveBudgetPlannersRequest>();
            CreateMap<CreateBudgetPlannerViewModel,CreateBudgetPlannerRequest>();
            CreateMap<CreateBudgetPlannerRequest, Budget>();
            CreateMap<Budget, Dto.Budget>();
            CreateMap<Budget, BudgetPlannerDetailsViewModel>();
            CreateMap<RetrieveTransactionsResponse,TransactionListViewModel>();
            CreateMap<AddBudgetTransactionViewModel,CreateTransactionRequest>();
            CreateMap<CreateTransactionRequest, Transaction>();
            CreateMap<RegisterAccountViewModel, Dto.Account>()
                .ForMember(member => member.Password, options => options.ConvertUsing(new BytesValueConverter()));
            CreateMap<LoginRequest, Dto.Account>()
                .ForMember(member => member.Password, options => options.ConvertUsing(new BytesValueConverter()));
            CreateMap<Dto.Account, Account>()
                .ForMember(member => member.EmailAddress, options => options.Ignore())
                .ForMember(member => member.FirstName, options => options.Ignore())
                .ForMember(member => member.LastName, options => options.Ignore())
                ;
        }

        class BytesValueConverter : IValueConverter<string, IEnumerable<byte>>
        {
            public IEnumerable<byte> Convert(string sourceMember, ResolutionContext context)
            {
                return System.Convert.FromBase64String(sourceMember);
            }
        }
    }
}
