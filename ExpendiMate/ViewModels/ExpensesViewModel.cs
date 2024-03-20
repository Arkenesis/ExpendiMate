using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.Services;
using SkiaSharp;
using SQLite;
using Microcharts;
using Newtonsoft.Json;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;


namespace ExpendiMate.ViewModels
{
    internal partial class ExpensesViewModel : ObservableObject
    {
        public static ExpensesViewModel Current { get; set; }

        SQLiteConnection connection;

        public ExpensesViewModel()
        {
            Current = this;
            connection = DatabaseServices.Connection;
            UpdateView();
        }
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

        [ObservableProperty]
        List<ExpensesCategoryModel> expensesByCategory = new();

        [ObservableProperty]
        double total;

        [ObservableProperty]
        string inBetween;

        [ObservableProperty]
        string selectedDate;

        [ObservableProperty]
        List<ISeries> series;

        public void UpdateView(string input="")
        {
            //Date Format
            //https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
            var timeRange = DateTime.Today;
            string result = ""+timeRange.ToString("d MMMM yyyy");
            List<ExpensesModel> allExpenses;

            if (input == "Week")
            {
                DateTime temp = DateTime.Today.AddDays(-7);
                result =  temp.ToString("d MMMM") + " - " + timeRange.ToString("d MMMM");
                allExpenses = connection.Table<ExpensesModel>().Where(i => i.ExpenseDate >= temp && timeRange >= i.ExpenseDate).ToList();
                SelectedDate = "Week";
            }
            else if (input == "Month")
            {
                DateTime temp = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime lastDayOfTheMonth = temp.AddMonths(1).AddDays(-1);
                result = temp.ToString("d MMMM") + " - " + lastDayOfTheMonth.ToString("d MMMM");
                allExpenses = connection.Table<ExpensesModel>().Where(i => i.ExpenseDate >= temp && lastDayOfTheMonth >= i.ExpenseDate).ToList();
                SelectedDate = "Month";
            }
            else if (input == "Year")
            {
                DateTime temp = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime lastDayOfTheYear = temp.AddYears(1).AddDays(-1);
                result = temp.ToString("d MMMM yyyy") + " - " + lastDayOfTheYear.ToString("d MMMM yyyy");
                allExpenses = connection.Table<ExpensesModel>().Where(i => i.ExpenseDate >= temp && lastDayOfTheYear >= i.ExpenseDate).ToList();
                SelectedDate = "Year";
            }
            else
            {
                allExpenses = connection.Table<ExpensesModel>().Where(i => i.ExpenseDate >= timeRange).ToList();
            }
            InBetween = result;

            // Group expenses by category
            // Key: Category Name
            // Value: Category List
            var groupedExpenses = allExpenses.GroupBy(e => e.ExpenseCategory);

            // Clear the existing items
            ExpensesByCategory.Clear();

            // Calculate total of all categories
            Total = allExpenses.Sum(item => item.ExpenseCost);

            List<ISeries> pies = new();
            List<ExpensesCategoryModel> _expensesByCategory = new();
            // Add into Observable (Observable allows display data on the UI)
            foreach (var group in groupedExpenses)
            {
                //Create new object to store data
                ExpensesCategoryModel categoryModel = new ExpensesCategoryModel(group.Key, group.ToList());

                // Calculate category Cost
                List<ExpensesModel> expensesDetailsList = group.ToList();
                categoryModel.Total = expensesDetailsList.Sum(i =>  i.ExpenseCost);

                // Set color
                if (CategoryModel.Category.TryGetValue(group.Key, out string outputColor))
                {
                    categoryModel.IconColor = outputColor;
                    if (categoryModel.IconColor == "") categoryModel.IconColor = "#68DE68";
                }

                // Calculate percentage
                categoryModel.Percentage = categoryModel.Total / Total * 100;

                // Add item to list
                _expensesByCategory.Add(categoryModel);

                // Create Chart Data
                var pie = new PieSeries<double>()
                {
                    Name = categoryModel.Name,
                    DataLabelsFormatter = item => categoryModel.Name,
                    Values = new double[] { categoryModel.Total },
                    DataLabelsPaint = new SolidColorPaint(SKColors.White),
                    DataLabelsSize = 14,
                    Fill = new SolidColorPaint(SKColor.Parse(categoryModel.IconColor)),
                    InnerRadius = 50
                };
                pies.Add(pie);
            }
            ExpensesByCategory = _expensesByCategory;
            Series = pies;
        }

        public void DeleteAllData()
        {
            connection.DeleteAll<ExpensesModel>();
            connection.DeleteAll<UserModel>();
            connection.DeleteAll<InstallmentModel>();
        }

    }
}
