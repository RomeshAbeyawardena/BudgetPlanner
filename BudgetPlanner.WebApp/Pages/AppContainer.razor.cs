using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.WebApp.Pages
{
    public class AppContainerHelper : AppComponent
    {
        protected ICollection<AppComponent> AppComponents { get; }
        protected override void OnInitialized()
        {
            AppComponents.Add(new Login { Enabled = true, Visible = true });
            base.OnInitialized();
        }

        protected RenderFragment Create(AppComponent appComponent) => builder => builder.OpenComponent(0, appComponent.GetType());

        public AppContainerHelper()
        {
            AppComponents = new List<AppComponent>();
        }
    }
}
