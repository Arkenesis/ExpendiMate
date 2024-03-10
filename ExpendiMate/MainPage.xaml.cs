using ExpendiMate.Models;
using ExpendiMate.Pages;
using ExpendiMate.ViewModels;

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
        //protected override void OnAppearing()
        //{
        //    //The name of the property that we listen in ViewModel
        //    viewModel.OnPropertyChanged("Expenses");
        //    viewModel.OnPropertyChanged("ExpensesByCategory");
        //}

        private void ExpenseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddExpense(new ExpensesModel()));
        }

    }
}
