using BudgetPlanner.Services;
using DNI.Shared.Services.Abstraction;
using DataServiceRegistratration = BudgetPlanner.Data.ServiceRegistration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Broker
{
    public class ServiceBroker : ServiceBrokerBase
    {
        public ServiceBroker()
        {
            Assemblies = new [] { 
                DefaultAssembly, 
                GetAssembly<ServiceRegistration>(), 
                GetAssembly<DataServiceRegistratration>() };    
        }
    }
}
