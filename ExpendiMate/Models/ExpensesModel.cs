using CommunityToolkit.Mvvm.ComponentModel;
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
    [Table("Expenses")]
    public partial class ExpensesModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ExpenseName { get; set; } = "";
        public string ExpenseCategory { get; set; } = "";
        public double ExpenseCost { get; set; }
        public string ExpenseComments { get; set; } = "";
        public DateTime ExpenseDate { get; set; }

        [ObservableProperty]
        string expensePicturePath;


    }
}
