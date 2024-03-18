//using CommunityToolkit.Mvvm.ComponentModel;
//using ExpendiMate.Models;
//using ExpendiMate.Services;
//using Firebase.Auth;
//using Firebase.Database;
//using Firebase.Database.Offline;
//using Firebase.Database.Query;
//using Microcharts;
//using Microsoft.Maui.Storage;
//using SkiaSharp;
//using SQLite;
//using System.Collections.ObjectModel;
//using System.Diagnostics;
//using System.Reactive.Linq;
//using System.Xml;


//namespace ExpendiMate.ViewModels
//{
//    internal partial class ExpensesViewModelFirebase : ObservableObject
//    {
//        public static ExpensesViewModelFirebase Current { get; set; }

//        RealtimeDatabase<ExpensesModel> connection;
//        User user;

//        public ExpensesViewModelFirebase()
//        {
//            Current = this;
//            connection = FirebaseServices.Connection;
//            user = LoginViewModel.Current.User.User;
//            UpdateView();
//        }

//        public ObservableCollection<ExpensesCategoryModel> ExpensesByCategory { get ; set ; } = new ();

//        public async Task<bool> SaveExpenses(ExpensesModel model)
//        {
//            try
//            {
//                await FirebaseServices.AddItem(model);
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine(ex);
//                return false;
//            }

//            return true;
//        }
//        //public void DeleteExpenses(ExpensesModel model)
//        //{
//        //    //If it has an Id, then we can delete it 
//        //    if (model.Id > 0)
//        //    {
//        //        connection.Delete(model);
//        //    }
//        //}

//        // Total Cost of all the expenses
//        [ObservableProperty]
//        double total;

//        [ObservableProperty]
//        string inBetween;

//        [ObservableProperty]
//        string selectedDate;

//        public async void UpdateView(string input="")
//        {
//            //Date Format
//            //https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
//            var timeRange = DateTime.Today;
//            string result = ""+timeRange.ToString("d MMMM yyyy");

//            //if (input == "Week")
//            //{
//            //    DateTime temp = DateTime.Today.AddDays(-7);
//            //    result =  temp.ToString("d MMMM") + " - " + timeRange.ToString("d MMMM");
//            //    allExpenses = connection.Table<ExpensesModel>().Where(i => i.ExpenseDate >= temp && timeRange >= i.ExpenseDate).ToList();
//            //    SelectedDate = "Week";
//            //}
//            //else if (input == "Month")
//            //{
//            //    DateTime temp = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
//            //    DateTime lastDayOfTheMonth = temp.AddMonths(1).AddDays(-1);
//            //    result = temp.ToString("d MMMM") + " - " + lastDayOfTheMonth.ToString("d MMMM");
//            //    allExpenses = connection.Table<ExpensesModel>().Where(i => i.ExpenseDate >= temp && lastDayOfTheMonth >= i.ExpenseDate).ToList();
//            //    SelectedDate = "Month";
//            //}
//            //else if (input == "Year")
//            //{
//            //    DateTime temp = new DateTime(DateTime.Today.Year, 1, 1);
//            //    DateTime lastDayOfTheYear = temp.AddYears(1).AddDays(-1);
//            //    result = temp.ToString("d MMMM yyyy") + " - " + lastDayOfTheYear.ToString("d MMMM yyyy");
//            //    allExpenses = connection.Table<ExpensesModel>().Where(i => i.ExpenseDate >= temp && lastDayOfTheYear >= i.ExpenseDate).ToList();
//            //    SelectedDate = "Year";
//            //}


            


//            try
//            {
//                var e = FirebaseServices.Connection
//                .AsObservable()
//                .Where(expense => !string.IsNullOrEmpty(expense.Key))
//                .Select(expense => new { expense })
//                .DistinctUntilChanged(pair => pair.expense.Key);

//                e.Subscribe(async pair =>
//                {
//                    // Create a new ExpensesModel object from the expense received
//                    var data = new ExpensesModel() { 
//                        ExpenseName = pair.expense.Object.ExpenseName,
//                        ExpenseCost = pair.expense.Object.ExpenseCost,
//                        ExpenseDate = pair.expense.Object.ExpenseDate,
//                        ExpenseCategory = pair.expense.Object.ExpenseCategory
//                    };

//                    await FirebaseServices.AddItem(data);
//                });


//                var allExpenses = FirebaseServices.GetAllItems();
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine(ex);

//            }
            

//            //InBetween = result;



//            //// Group expenses by category
//            //// Key: Category Name
//            //// Value: Category List
//            //var groupedExpenses = allExpenses.GroupBy(e => e.ExpenseCategory);

//            //// Clear the existing items
//            //ExpensesByCategory.Clear();

//            //// Calculate total of all categories
//            //Total = allExpenses.Sum(item => item.ExpenseCost);

//            //List<ChartEntry> entries = new();

//            //// Add into ObservableCollection
//            //// ObservableCollection allows display data on the UI
//            //foreach (var group in groupedExpenses)
//            //{
//            //    //Create new object to store data
//            //    ExpensesCategoryModel categoryModel = new ExpensesCategoryModel(group.Key, group.ToList());

//            //    // Calculate category Cost
//            //    List<ExpensesModel> expensesDetailsList = group.ToList();
//            //    double sum = 0;
//            //    foreach (var item in expensesDetailsList)
//            //    {
//            //        sum = sum + item.ExpenseCost;
//            //    }
//            //    categoryModel.Total = sum;

//            //    // Set color
//            //    if (CategoryModel.Category.TryGetValue(group.Key, out string outputColor))
//            //    {
//            //        categoryModel.IconColor = outputColor;
//            //        if (categoryModel.IconColor == "") categoryModel.IconColor = "#68DE68";
//            //    }

//            //    // Calculate percentage
//            //    categoryModel.Percentage = sum / Total * 100;

//            //    // Create Chart Data
//            //    var entry = new ChartEntry((int)sum)
//            //    { Label = categoryModel.Name,
//            //        ValueLabel = "$" + sum,
//            //        Color = SKColor.Parse(categoryModel.IconColor),
//            //        ValueLabelColor = SKColor.Parse("#FFFFFF"),
//            //        OtherColor = SKColor.Parse(categoryModel.IconColor)
//            //    };
//            //    entries.Add(entry);
//            //    ExpensesByCategory.Add(categoryModel);
//            //}
//            //createChart(entries);
//            //OnPropertyChanged(nameof(Item));
//        }

//        public void DeleteAllData()
//        {
//            for(var i = 0; i< 100; i++)
//                FirebaseServices.DeleteItem(""+i);
//        }

//        [ObservableProperty]
//        DonutChart item;

//        public void createChart(IEnumerable<ChartEntry> entries)
//        {
//            Item = new DonutChart { 
//                Entries = entries, 
//                BackgroundColor = SKColor.Parse("#4b4b4b"), 
//                LabelColor = new SKColor(255, 255, 255),
//                AnimationDuration = new TimeSpan(0,0,3),
//                HoleRadius = 0.8F,
//                LabelMode = LabelMode.LeftAndRight
//            };
//        }

//    }
//}
