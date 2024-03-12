using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.Services;
using Microcharts;
using SkiaSharp;
using SQLite;
using System.Collections.Generic;
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

        public List<ExpensesModel> ExpensesByWeek
        {
            get
            {
                UpdateExpensesByCategory();
                return connection.Query<ExpensesModel>("SELECT * FROM Expenses WHERE ExpenseDate >= DATE('now', '-7 days')").ToList();
            }
        }

        public List<ExpensesModel> ExpensesByMonth
        {
            get
            {
                UpdateExpensesByCategory();
                return connection.Table<ExpensesModel>().ToList();
            }
        }

        public ObservableCollection<ExpensesCategoryModel> ExpensesByCategory { get ; set ; } = new ();

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
            //List<ExpensesModel> allExpenses = connection.Table<ExpensesModel>().ToList();
            var sevenDaysAgo = DateTime.Today.AddDays(-7);
            List<ExpensesModel> allExpenses = connection.Table<ExpensesModel>().Where(i => i.ExpenseDate >= sevenDaysAgo).ToList();

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


        public DonutChart Item { get; set; }

        public void createChart()
        {
            if (Item != null) return;
            var entries = new[]
            {
                new ChartEntry(212)
                {
                    Label = "UWP",
                    ValueLabel = "112",
                    Color = SKColor.Parse("#2c3e50")
                },
                new ChartEntry(248)
                {
                    Label = "Android",
                    ValueLabel = "648",
                    Color = SKColor.Parse("#77d065")
                },
                new ChartEntry(128)
                {
                    Label = "iOS",
                    ValueLabel = "428",
                    Color = SKColor.Parse("#b455b6")
                },
                new ChartEntry(514)
                {
                    Label = "Forms",
                    ValueLabel = "214",
                    Color = SKColor.Parse("#3498db")
                }
            };
            Item = new DonutChart { 
                Entries = entries, 
                BackgroundColor = SKColor.Parse("#4b4b4b"), 
                LabelColor = new SKColor(255, 255, 255),
                AnimationDuration = new TimeSpan(0,0,3)
            };
        }

        public void UpdateView()
        {
            OnPropertyChanged(nameof(Expenses));
        }

        internal void setExpenses(string res)
        {
            throw new NotImplementedException();
        }
    }
}
