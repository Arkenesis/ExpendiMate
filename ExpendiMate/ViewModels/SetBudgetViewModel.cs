using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.Services;
using SQLite;

namespace ExpendiMate.ViewModels
{
    internal partial class SetBudgetViewModel : ObservableObject
    {
        public static SetBudgetViewModel Current { get; set; }

        SQLiteConnection connection;

        //Get user record
        [ObservableProperty]
        private UserModel _user;

        [ObservableProperty]
        private Color _buttonColor1;

        [ObservableProperty]
        private Color _buttonColor2;

        [ObservableProperty]
        private Color _buttonColor3;

        [ObservableProperty]
        private Color _buttonColor4;

        [ObservableProperty]
        private Color _balanceColor;

        public SetBudgetViewModel()
        {
            Current = this;
            connection = DatabaseServices.Connection;
            UpdateView();
        }

        public void UpdateView()
        {
            var data = connection.Table<UserModel>().ToList().FirstOrDefault();
            if (data == null)
            {
                var newData = new UserModel();
                SetBudgetViewModel.Current.SaveAndUpdateUser(newData);
                User = newData;
            }
            else
            {
                User = data;
            }

            User.Budget = (User.Income * User.BudgetPercent);
            User.Expenses = getTotalExpense();
            User.BudgetBalance = User.Budget - User.Expenses;

            if (User.BudgetPercent == 0.5 )
                ButtonColor1 = new Color(50, 223, 127);
            else 
                ButtonColor1 = new Color(0, 0, 0,0);

            if (User.BudgetPercent == 0.3)
                ButtonColor2 = new Color(50, 223, 127);
            else 
                ButtonColor2 = new Color(0, 0, 0, 0);

            if (User.BudgetPercent == 0.2)
                ButtonColor3 = new Color(50, 223, 127);
            else 
                ButtonColor3 = new Color(0, 0, 0, 0);

            if (User.BudgetPercent != 0.5 && User.BudgetPercent != 0.3 && User.BudgetPercent != 0.2)
                ButtonColor4 = new Color(50, 223, 127); // Green
            else
                ButtonColor4 = new Color(0, 0, 0, 0); //Transparent

            if (User.BudgetBalance < 0)
            {
                BalanceColor = new Color(200, 0, 0);
            }
            else
            {
                BalanceColor = new Color(255, 255, 255);
            }
        }
        public double getTotalExpense()
        {
            var data = connection.Table<ExpensesModel>().ToList();
            if (data.Count != 0)
            {
                return data.AsEnumerable().Sum(i => i.ExpenseCost);
            }
            else
            {
                return 0;
            }
        }

        public void SaveAndUpdateUser(UserModel model)
        {
            if (model.Id > 0)
            {
                model.Budget = (model.Income * model.BudgetPercent);
                model.Expenses = getTotalExpense();
                model.BudgetBalance = model.Budget - model.Expenses;
                connection.Update(model);
            }
            else
            {
                connection.Insert(model);
            }
            UpdateView();
        }

        public void DeleteUser(UserModel model)
        {
            //If it has an Id, then we can delete it 
            if (model.Id > 0)
            {
                connection.Delete(model);
            }
        }
    }
}
