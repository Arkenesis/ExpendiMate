using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Models
{
    public class ExpenseItemModel
    {
        public int ExpenseId { get; set; }
        public string ExpenseName { get; set; } = string.Empty;
        public double ExpenseCost { get; set; }
        public string ExpenseComments { get; set; } = string.Empty;
        public DateTime ExpenseDate { get; set; }
        public string ExpensePicturePath { get; set; } = string.Empty;
    }
}
