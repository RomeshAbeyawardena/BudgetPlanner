﻿using BudgetPlanner.Domains.Dto;
using DNI.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Responses
{
    public class RegisterAccountResponse : ResponseBase<Account>
    {
        public  Exception Exception { get; set; }
    }
}
