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
    //Installment
    [Table("Installment")]
    public partial class InstallmentModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ObservableProperty]
        private string installmentName = string.Empty;

        [ObservableProperty]
        private double installmentCost = 0;

        [ObservableProperty]
        private DateTime installmentDate;

        [ObservableProperty]
        private bool installmentIsActivated = false;
    }
}
