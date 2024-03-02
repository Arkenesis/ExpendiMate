using ExpendiMate.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Models
{

    //Expense
    public class ExpensesModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ExpenseName { get; set; } = "";
        public string ExpenseCategory { get; set; } = "";
        public double ExpenseCost { get; set; }
        public string ExpenseComments { get; set; } = "";
        public DateTime ExpenseDate { get; set; }
        public string ExpensePicturePath { get; set; } = "";


    }
}
