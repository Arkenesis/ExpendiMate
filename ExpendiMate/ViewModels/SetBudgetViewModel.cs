using ExpendiMate.Models;
using ExpendiMate.Services;
using SQLite;

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

        public double BudgetBalance
        {
            get
            {
                return User.Income - Expenses;
            }
        }

        public double Expenses
        {
            get
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
        }

        public void SaveAndUpdateUser(UserModel model)
        {
            connection.Update(model);
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
