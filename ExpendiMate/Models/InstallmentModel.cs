using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Models
{
    //Installment
    public class InstallmentModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string InstallmentName { get; set; } = "";
        public double InstallmentCost { get; set; }
        public DateTime InstallmentDate { get; set; }
        public double InstallmentSummary { get; set; }
    }
}
