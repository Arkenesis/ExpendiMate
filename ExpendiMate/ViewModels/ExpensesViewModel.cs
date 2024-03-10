using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.Services;
using SQLite;
using System.Collections.ObjectModel;

namespace ExpendiMate.ViewModels
{
    internal class ExpensesViewModel : ObservableObject
    {
        public static ExpensesViewModel Current { get; set; }

        SQLiteConnection connection;

        public ExpensesViewModel()
        {
            Current = this;
            connection = DatabaseServices.Connection;
            UpdateExpensesByCategory();
        }

        //Get every expense record
        public List<ExpensesModel> Expenses
        {
            get
            {
                UpdateExpensesByCategory();
                return connection.Table<ExpensesModel>().ToList();
            }
        }

        public ObservableCollection<ExpensesCategoryModel> ExpensesByCategory
        { 
            get ; 
            set ; 
        } = new ();

        public void SaveExpenses(ExpensesModel model)
        {
            //If it has an Id, then it already exists and we can update it 
            if (model.Id > 0)
            {
                connection.Update(model);
            }
            //If not, it's new and we need to add it 
            else
            {
                connection.Insert(model);
            }
        }
        public void DeleteExpenses(ExpensesModel model)
        {
            //If it has an Id, then we can delete it 
            if (model.Id > 0)
            {
                connection.Delete(model);
            }
        }

        public void UpdateExpensesByCategory()
        {
            // Clearing the collection moved to the constructor to avoid repeated clearing

            // Get all expenses
            List<ExpensesModel> allExpenses = connection.Table<ExpensesModel>().ToList();

            // Group expenses by category
            var groupedExpenses = allExpenses.GroupBy(e => e.ExpenseCategory);

            // Clear the existing items
            ExpensesByCategory.Clear();

            // Add grouped expenses to ExpensesByCategory
            foreach (var group in groupedExpenses)
            {
                var categoryModel = new ExpensesCategoryModel(group.Key, group.ToList());
                ExpensesByCategory.Add(categoryModel);
            }
        }

        public void DeleteAllData()
        {
            connection.DeleteAll<ExpensesModel>();
            connection.DeleteAll<UserModel>();
            connection.DeleteAll<InstallmentModel>();
        }

    }
}
