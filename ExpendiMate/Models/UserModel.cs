using ExpendiMate.Pages;
using ExpendiMate.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Models
{
    //Income
    [Table("Users")]
    public class UserModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("income")]
        public double Income { get; set; }
        [Column("budgetpercent")]
        public double BudgetPercent { get; set; }
        [Column("budget")]
        public double Budget
        {
            get
            {
                return (double)Income / BudgetPercent;
            }
        }
    }
}
