﻿using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class CreateTokenRequest : IRequest<CreateTokenResponse>
    {
        public double ValidityPeriodInMinutes { get; set; }
    }
}
