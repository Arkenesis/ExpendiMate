using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace ExpendiMate.Models
{
    //Income
    [Table("Users")]
    public partial class UserModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ObservableProperty]
        private double income;
        [ObservableProperty]
        private double budgetPercent;
        [ObservableProperty]
        private double budget;
        [ObservableProperty]
        private double expenses;
        [ObservableProperty]
        private double budgetBalance;
    }
}
