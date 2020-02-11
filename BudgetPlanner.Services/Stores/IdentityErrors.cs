using Microsoft.AspNetCore.Identity;

namespace BudgetPlanner.Services.Stores
{
    public static class IdentityErrors
    {
        public static IdentityError RoleNotFound = new IdentityError { Code = "RoleNotFound", Description = "Role not found" };
        public static IdentityError DuplicateRole = new IdentityError { Code = "DuplicateRole", Description = "Duplicate role exists" };
        public static IdentityError DuplicateAccount = new IdentityError { Code = "DuplicateAccount", Description = "Duplicate account exists" };
        public static IdentityError AccountNotFound = new IdentityError { Code = "AccountNotFound", Description = "Unable to find account" };
        public static IdentityError InvalidAccountOrPassword = new IdentityError { Code = "InvalidAccountOrPassword", Description = "Invalid account or password" };
    }
}
