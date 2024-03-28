using ExpendiMate.Models;
using ExpendiMate.Pages;
using ExpendiMate.ViewModels;
using Microcharts;
using Microcharts.Maui;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace ExpendiMate
{
    public partial class MainPage : ContentPage
    {
        ExpensesViewModel viewModel;

        public MainPage()
        {
            viewModel = new ExpensesViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }


        //Update the view if property change
        protected override void OnAppearing()
        {
            //    The name of the property that we listen in 
            //    viewModel.OnPropertyChanged("ExpensesByCategory");
            base.OnAppearing();
            
            DayLabel.TextColor = Color.FromHex("#4b9460");
            DayLabel.TextDecorations = TextDecorations.Underline;
            resetLabel(WeekLabel);
            resetLabel(MonthLabel);
            resetLabel(YearLabel);
            viewModel.UpdateView();
        }

        private void ExpenseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddExpense(new ExpensesModel()));
        }

        private void Frame_Tapped(object sender, EventArgs e)
        {
            var frame = (Frame) sender;
            var category = (ExpensesCategoryModel)frame.BindingContext;
            List<ExpensesModel> list = category;
            System.Diagnostics.Debug.WriteLine($"Category Name: {category.Name}");
            foreach (var item in list)
            {
                System.Diagnostics.Debug.WriteLine($"Item Name: {item.ExpenseName}");
                System.Diagnostics.Debug.WriteLine($"Item Name: {item.ExpenseCost}");
                System.Diagnostics.Debug.WriteLine($"Item Name: {item.ExpenseDate}");
            }
            Navigation.PushAsync(new Category(category));
        }

        private void Data_Clicked(object sender, EventArgs e)
        {
            var frame = (Frame) sender;
            var label = (Label) frame.Content;
            label.TextColor = Color.FromHex("#4b9460");
            label.TextDecorations = TextDecorations.Underline;
            string res = label.Text;
            if(label.Text == "Day")
            {
                resetLabel(WeekLabel);
                resetLabel(MonthLabel);
                resetLabel(YearLabel);
            }
            if (label.Text == "Week")
            {
                resetLabel(DayLabel);
                resetLabel(MonthLabel);
                resetLabel(YearLabel);
            }
            if (label.Text == "Month")
            {
                resetLabel(DayLabel);
                resetLabel(WeekLabel);
                resetLabel(YearLabel);
            }
            if (label.Text == "Year")
            {
                resetLabel(DayLabel);
                resetLabel(WeekLabel);
                resetLabel(MonthLabel);
            }
            viewModel.UpdateView(res);
        }

        private void resetLabel(Label input)
        {
            input.TextColor = Color.Parse("White");
            input.TextDecorations = TextDecorations.None;
        }
    }
}
