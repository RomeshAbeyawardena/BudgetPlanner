using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class SaveTagRequest : IRequest<SaveTagResponse>
    {
        public string Name { get; set; }
        public int Id { get; set; 
    }
}
