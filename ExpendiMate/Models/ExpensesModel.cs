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
        public string ExpenseName { get; set; } = string.Empty;
        public string ExpenseCategory { get; set; } = string.Empty;
        public double ExpenseCost { get; set; }
        public string ExpenseComments { get; set; } = string.Empty;
        public DateTime ExpenseDate { get; set; }

        public string TodayText
        {
            get
            {
                DateTime today = DateTime.Today;
                return today.ToString("dd/MM") + "\n Today";
            }
        }

        public string YesterdayText
        {
            get
            {
                DateTime today = DateTime.Today;
                DateTime yesterday = today.AddDays(-1);
                return yesterday.ToString("dd/MM") + "\n Yesterday";
            }
        }
        public string TwoDaysAgoText
        {
            get
            {
                DateTime today = DateTime.Today;
                DateTime twoDaysAgo = today.AddDays(-2);
                return twoDaysAgo.ToString("dd/MM") + "\n 2 days ago";
            }
        }

        [ObservableProperty]
        string expensePicturePath;
    }
}
