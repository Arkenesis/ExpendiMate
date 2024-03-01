using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Models
{
    public class MainViewModel : ObservableObject
    {
        public double Income { get; set; } = 0;
        public double Expense { get; set; } = 0;
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

        //Expense Page
        public string ExpenseName { get; set; } = "";
        public string ExpenseCategory { get; set; } = "";
        public double ExpenseCost { get; set; }
        public string ExpenseComments { get; set; } = "";
        public DateTime ExpenseDate { get; set; }
        public string ExpensePicturePath { get; set; } = "";

        //Installment Date
        public string InstallmentName { get; set; } = "";
        public double InstallmentCost { get; set; }
        public DateTime InstallmentDate { get; set; }
        public double InstallmentSummary { get; set; }

        //Account Page
        public string AccountEmail { get; set; } = "";
        public string AccountPassword { get; set; } = "";
    }
}
