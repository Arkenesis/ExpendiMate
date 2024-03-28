using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.Services;
using Microcharts;
using SkiaSharp;
using SQLite;
using System.Collections.ObjectModel;
using System.Globalization;


namespace ExpendiMate.ViewModels
{
    internal partial class AnalyticsViewModel : ObservableObject
    {
        public static AnalyticsViewModel Current { get; set; }

        SQLiteConnection connection;

        public AnalyticsViewModel()
        {
            Current = this;
            connection = DatabaseServices.Connection;
            UpdateView();
        }

        public ObservableCollection<ExpensesYearModel> ExpensesOrderByYears { get ; set ; } = new ();

        public void UpdateView(int year=0)
        {
            DateTime currYear = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime firstDayOfYear = currYear.AddYears(year).AddDays(-1);
            DateTime lastDayOfYear = firstDayOfYear.AddYears(1).AddDays(1);
            Year = lastDayOfYear.Year - 1;

            List<ExpensesModel> allExpenses = connection.Table<ExpensesModel>().Where(e => e.ExpenseDate >= firstDayOfYear && e.ExpenseDate <= lastDayOfYear).ToList();

            if(allExpenses.Capacity == 0)
            {
                ExpensesOrderByYears.Clear();
                List<ChartEntry> entries = new();
                var entry = new ChartEntry(0)
                {
                    Label = "In "+ lastDayOfYear.Year + ", there are no records of your expenses",
                    ValueLabel = "0",
                    Color = SKColor.Parse("#5DB075"),
                    ValueLabelColor = SKColor.Parse("#000000"),
                    TextColor = SKColor.Parse("#5DB075")
                };
                entries.Add(entry);
                createChart(entries);
            }
            else
            {
                var groupedExpenses = allExpenses.GroupBy(e => e.ExpenseDate.Month);

                var total = allExpenses.Sum(item => item.ExpenseCost);

                ExpensesOrderByYears.Clear();

                List<ChartEntry> entries = new();

                foreach (var group in groupedExpenses)
                {
                    string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key);
                    ExpensesYearModel model = new ExpensesYearModel(monthName, group.ToList());

                    // Calculate Total
                    List<ExpensesModel> expensesDetailsList = group.ToList();
                    double sum = 0;
                    foreach (var item in expensesDetailsList)
                    {
                        sum = sum + item.ExpenseCost;
                    }
                    model.Total = sum;

                    // Calculate Percentage
                    model.Percentage = sum / total;

                    ExpensesOrderByYears.Add(model);

                    // Create Chart Data
                    var entry = new ChartEntry((int)sum)
                    {
                        Label = monthName,
                        ValueLabel = "$" + sum,
                        Color = SKColor.Parse("#5DB075"),
                        ValueLabelColor = SKColor.Parse("#000000"),
                        TextColor = SKColor.Parse("#5DB075")
                    };
                    entries.Add(entry);
                }
                createChart(entries);
            }
        }

        [ObservableProperty]
        int year;

        [ObservableProperty]
        BarChart item;

        public void createChart(IEnumerable<ChartEntry> entries)
        {
            Item = new BarChart {
                Entries = entries,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                ValueLabelOption = ValueLabelOption.TopOfElement
            };
        }
    }
}
