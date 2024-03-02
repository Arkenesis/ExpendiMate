using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Models
{
    internal class InstallmentModel
    {
        public string InstallmentName { get; set; } = string.Empty;
        public double InstallmentCost { get; set; }
        public DateTime InstallmentDate { get; set; }
        public double InstallmentSummary { get; set; }
    }
}
