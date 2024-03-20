using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.Services;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using Microcharts;
using SkiaSharp;
using SQLite;
using System.Collections.ObjectModel;
using System.Globalization;
using LiveChartsCore;
using LiveChartsCore.Drawing;


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
            ExpensesOrderByYears = new List<ExpensesYearModel>();
            Series = new List<ISeries>();
            XAxis = new List<Axis>();
            YAxis = new List<Axis>();
            UpdateView();
        }
        [ObservableProperty]
        List<ExpensesYearModel> ?expensesOrderByYears;

        [ObservableProperty]
        List<ISeries> series;

        [ObservableProperty]
        List<Axis> xAxis;

        [ObservableProperty]
        List<Axis> yAxis;
        public void UpdateView(int year=0)
        {

            DateTime currYear = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime firstDayOfYear = currYear.AddYears(year).AddDays(-1);
            DateTime lastDayOfYear = firstDayOfYear.AddYears(1).AddDays(1);
            Year = lastDayOfYear.Year - 1;

            List<ExpensesModel> allExpenses = connection.Table<ExpensesModel>().Where(e => e.ExpenseDate >= firstDayOfYear && e.ExpenseDate <= lastDayOfYear).ToList();

            var result = new List<ExpensesYearModel>();

            if (allExpenses.Capacity == 0)
            {
                ExpensesOrderByYears = null;
                Series = new List<ISeries>();
                XAxis = new List<Axis>();
                YAxis = new List<Axis>();
            }
            else
            {
                var groupedExpenses = allExpenses.GroupBy(e => e.ExpenseDate.Month);

                var total = allExpenses.Sum(item => item.ExpenseCost);

                List<double> values = new();
                List<string> x = new();

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

                    // Add items to the expenses year list
                    result.Add(model);

                    // Chart Labels = january/feb/mar
                    x.Add(model.Name);

                    // Chart column values
                    values.Add(model.Total);
                }
                createChart(values, x);
                ExpensesOrderByYears = result;
            }

        }

        [ObservableProperty]
        int year;

        public LiveChartsCore.Measure.Margin Margin { get; set; } = new LiveChartsCore.Measure.Margin(0,0,0,75);

        public void createChart(List<double> _values, List<string> x)
        {

            ColumnSeries<double> temp = new();
            temp.DataLabelsSize = 10;
            temp.Rx = 50;
            temp.Ry = 50;
            temp.MaxBarWidth = 18;
            temp.Padding = 0;
            temp.Values = _values;

            List<Axis> xAxes = new();
            var temp2 = new Axis();
            temp2.LabelsRotation = 90;
            temp2.Labels = x;
            temp2.Position = LiveChartsCore.Measure.AxisPosition.Start;
            xAxes.Add(temp2);

            List<Axis> yAxes = new();
            var temp3 = new Axis();
            temp3.IsVisible = false;
            yAxes.Add(temp3);

            List<ISeries> tempSeries = [temp];

            YAxis = yAxes;
            XAxis = xAxes;
            Series = tempSeries;
        }

    }
}
