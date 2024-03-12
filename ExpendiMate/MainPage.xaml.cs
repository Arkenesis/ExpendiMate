using ExpendiMate.Models;
using ExpendiMate.Pages;
using ExpendiMate.ViewModels;
using Microcharts;
using Microcharts.Maui;
using System.Collections.Generic;
using System.Diagnostics;

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
            viewModel.UpdateView();
            base.OnAppearing();
            viewModel.createChart();
            ExpensesChart.Chart = viewModel.Item;

            if (!ExpensesChart.Chart.IsAnimating)
                ExpensesChart.Chart.AnimateAsync(true).ConfigureAwait(false);
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
            string res = label.Text;
            viewModel.setExpenses(res);
        }
    }
}
