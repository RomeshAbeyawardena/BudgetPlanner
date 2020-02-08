﻿using BudgetPlanner.Domains.Data;
using DNI.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Responses
{
    public class CreateTokenResponse : ResponseBase
    {
        public Dto.RequestToken TokenRequest { get; set; }
    }
}
