using AutoMapper;
using BudgetPlanner.Domains.Requests;
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
            CreateMap<CreateBudgetPlannerRequest, Data.Budget>();
        }
    }
}
