using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Models
{
    //Expense
    public class ExpensesCategoryModel : List<ExpensesModel>
    {
        public string Name { get; private set; }
        public ExpensesCategoryModel(string name, List<ExpensesModel> list) : base(list)
        {
            Name = name;
        }

    }

}
