using Microsoft.AspNetCore.Identity;

namespace BudgetPlanner.Services.Stores
{
    public static class AccountStoreIdentityErrors
    {
        public static IdentityError DuplicateAccount = new IdentityError { Code = "DuplicateAccount", Description = "Duplicate account exists" };
        public static IdentityError AccountNotFound = new IdentityError { Code = "AccountNotFound", Description = "Unable to find account" };
    }
}
