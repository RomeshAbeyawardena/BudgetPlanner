using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Data;
using DNI.Shared.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Data
{
    public class BudgetPlannerDbContext : DbContextBase
    {
        public BudgetPlannerDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        
        }
        public DbSet<RequestToken> RequestTokens { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountAccess> AccountAccesses { get; set; }
        public DbSet<AccessType> AccessTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<AccountClaim> AccountClaims { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<TransactionLedger> TransactionLedgers { get; set; }
    }
}
