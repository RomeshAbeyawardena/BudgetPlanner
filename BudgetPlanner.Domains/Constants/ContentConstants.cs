using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Constants
{
    public static class ContentConstants
    {
        //page
        public const string Title = "title";
        public const string Content = "content";
        public const string MetaDescription = "metaDescription";
        public const string MetaTags = "metaTags";

        public const string RegisterContentPath = "pages/account/register";
        public const string ConfirmPasswordLabel = "confirmPasswordLabel";
        public const string FirstNameLabel = "firstNameLabel";
        public const string LastNameLabel = "lastNameLabel";
        public const string AcceptTermsOfUseLabel = "acceptTermsOfUseLabel";

        public const string LoginContentPath = "pages/account/login";
        public const string EmailAddressLabel = "emailAddressLabel";
        public const string PasswordLabel = "passwordLabel";
        public const string RememberMeLabel = "rememberMeLabel";
        
        public const string BudgetPlannerEditor = "pages/budgetplanner/editor";
        public const string ReferenceLabel = "referenceLabel";
        public const string NameLabel = "nameLabel";
        public const string ActiveLabel = "activeLabel";

        public const string TransactionEditor = "pages/budgetplanner/transactioneditor";
        public const string TransactionTypeLabel = "transactionTypeLabel";
        public const string DescriptionLabel = "descriptionLabel";
        public const string AmountLabel = "amountLabel";

        public const string Dashboard = "pages/dashboard/dashboarditem";
        public const string EmptyDashboard = "pages/dashboard/emptydashboard";

        public const string ReplaceParameterStart = "#{";
        public const string ReplaceParameterEnd = "}";
    }
}
