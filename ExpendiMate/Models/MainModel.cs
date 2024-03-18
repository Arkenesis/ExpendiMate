using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.ViewModels;

namespace ExpendiMate.Models
{
    public class MainModel : ObservableObject
    {
        public double Income { get; set; } = 0;
        public double TotalExpense { get; set; } = 0;
        public double BudgetPercent { get; set; }
        public double Budget 
        {
            get
            {
                return (double)Income / BudgetPercent;
            } 
        }
        public double BudgetBalance { get; set; }
        public double ExpenditurePercent { get; set; }

        //Account Page
        public string AccountEmail { get; set; } = string.Empty;
        public string AccountPassword { get; set; } = string.Empty;
    }
}
