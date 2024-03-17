using ExpendiMate.Models;
using ExpendiMate.ViewModels;

namespace ExpendiMate.Pages;

public partial class AddExpense : ContentPage
{
    ExpensesModel model;

    //Pass the variable in
    public AddExpense(ExpensesModel m)
    {
        model = m;
        BindingContext = model;
        InitializeComponent();
    }

    private async void AddExpenses(object sender, EventArgs e)
    {
        var model = (ExpensesModel) BindingContext;
        if (model.ExpenseName == null )
        {
            await DisplayAlert("Warning", "You must enter expense name.", "OK");
            return;
        }
        if (model.ExpenseCategory == null || model.ExpenseCategory == "")
        {
            await DisplayAlert("Warning", "You must enter expense category", "OK");
            return;
        }
        if (model.ExpenseCost <= 0)
        {
            await DisplayAlert("Warning", "You must enter positive number & bigger than 0.", "OK");
            return;
        }
        ExpensesViewModel.Current.SaveExpenses(model);
        ExpensesViewModel.Current.UpdateView();
        await Navigation.PopToRootAsync();
    }

    private void CalculateDate()
    {
        DateTime today = DateTime.Today;
        DateTime yesterday = today.AddDays(-1);
        DateTime twoDaysAgo = today.AddDays(-2);
    }

    private async void ClearAllExpenses(object sender, EventArgs e)
    {
        ExpensesViewModel.Current.DeleteAllData();
        ExpensesViewModel.Current.UpdateView();
        await Navigation.PopToRootAsync();
    }
}