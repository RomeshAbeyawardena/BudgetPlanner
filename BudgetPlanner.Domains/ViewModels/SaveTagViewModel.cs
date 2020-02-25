using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class SaveTagViewModel
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
