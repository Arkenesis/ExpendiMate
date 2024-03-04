using ExpendiMate.Models;
using ExpendiMate.Services;
using SQLite;
using System.ComponentModel;

namespace ExpendiMate.ViewModels
{
    internal class SetBudgetViewModel : ObservableObject
    {
        public static SetBudgetViewModel Current { get; set; }

        SQLiteConnection connection;

        public SetBudgetViewModel()
        {
            Current = this;
            connection = DatabaseServices.Connection;
            UpdateView();
        }

        //Get user record
        public UserModel User
        {
            get
            {
                var data = connection.Query<UserModel>("SELECT * FROM UserModel").ToList();
                if (data.Count == 0)
                {
                    var newData = new UserModel();
                    SaveAndUpdateUser(newData);
                    return newData;
                }
                else
                {
                    return data[0];
                }
            }
        }

        private double _income;
        public double Income
        {
            get { return _income; }
            set
            {
                if (_income != value)
                {
                    _income = value;
                    OnPropertyChanged(); // Raise PropertyChanged event
                }
            }
        }

        private double _budgetBalance;
        public double BudgetBalance
        {
            get { return _budgetBalance; }
            set
            {
                if (_budgetBalance != value)
                {
                    _budgetBalance = value;
                    OnPropertyChanged(); // Raise PropertyChanged event
                }
            }
        }

        private double _expenses;
        public double Expenses
        {
            get { return _expenses; }
            set
            {
                if (_expenses != value)
                {
                    _expenses = value;
                    OnPropertyChanged(); // Raise PropertyChanged event
                }
            }
        }

        private double _budgetPercent;
        public double BudgetPercent
        {
            get { return _budgetPercent; }
            set
            {
                if (_budgetPercent != value)
                {
                    _budgetPercent = value;
                    OnPropertyChanged(); // Raise PropertyChanged event
                }
            }
        }

        private double _budget;
        public double Budget
        {
            get { return _budget; }
            set
            {
                if (_budget != value)
                {
                    _budget = value;
                    OnPropertyChanged(); // Raise PropertyChanged event
                }
            }
        }


        private Color _buttonColor1;
        public Color ButtonColor1
        {
            get { return _buttonColor1; }
            set
            {
                if (_buttonColor1 != value)
                {
                    _buttonColor1 = value;
                    OnPropertyChanged(); // Raise PropertyChanged event
                }
            }
        }

        private Color _buttonColor2;
        public Color ButtonColor2
        {
            get { return _buttonColor2; }
            set
            {
                if (_buttonColor2 != value)
                {
                    _buttonColor2 = value;
                    OnPropertyChanged(); // Raise PropertyChanged event
                }
            }
        }

        private Color _buttonColor3;
        public Color ButtonColor3
        {
            get { return _buttonColor3; }
            set
            {
                if (_buttonColor3 != value)
                {
                    _buttonColor3 = value;
                    OnPropertyChanged(); // Raise PropertyChanged event
                }
            }
        }

        private Color _buttonColor4;
        public Color ButtonColor4
        {
            get { return _buttonColor4; }
            set
            {
                if (_buttonColor4 != value)
                {
                    _buttonColor4 = value;
                    OnPropertyChanged(); // Raise PropertyChanged event
                }
            }
        }

        public double getTotalExpense()
        {
            var data = connection.Query<ExpensesModel>("SELECT * FROM ExpensesModel").ToList();
            if (data.Count != 0)
            {
                var result = data.AsEnumerable().Sum(o => o.ExpenseCost);
                return result;
            }
            else
            {
                return 0;
            }
        }

        public void UpdateView()
        {
            Income = User.Income;
            BudgetPercent = User.BudgetPercent;
            Budget = (User.Income * User.BudgetPercent);
            Expenses = getTotalExpense();
            BudgetBalance = Budget - Expenses;
            if(BudgetPercent == 0.5 )
                ButtonColor1 = Colors.Green;
            else ButtonColor1 = Colors.DarkGray;
            if (BudgetPercent == 0.3)
                ButtonColor2 = Colors.Green;
            else ButtonColor2 = Colors.DarkGray;
            if (BudgetPercent == 0.2)
                ButtonColor3 = Colors.Green;
            else ButtonColor3 = Colors.DarkGray;
            if (BudgetPercent != 0.5 && BudgetPercent != 0.3 && BudgetPercent != 0.2)
                ButtonColor4 = Colors.Green;
            else ButtonColor4 = Colors.DarkGray;
        }

        public void SaveAndUpdateUser(UserModel model)
        {
            connection.Update(model);
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
