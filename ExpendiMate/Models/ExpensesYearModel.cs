using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Models
{
    //Expense
    public class ExpensesYearModel : List<ExpensesModel>
    {
        private List<ExpensesModel> expensesModels;

        public string Name { get; set; }
        public double Total { get; set; }
        public double Percentage { get; set; }
        public string IconColor { get; set; }
        public ExpensesYearModel(string name, List<ExpensesModel> list) : base(list)
        {
            Name = name;
        }
    }

}
