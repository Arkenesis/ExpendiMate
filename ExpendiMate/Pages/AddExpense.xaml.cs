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
        model.ExpenseDate = DateTime.Today;
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

    public void TodayButtonClicked(object sender, EventArgs e)
    {
        model.ExpenseDate = DateTime.Today;

        TodayButtonColor.BackgroundColor = Color.FromHex("#32df7f");
        YesterdayButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        TwoDaysAgoButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
    }

    public void YesterdayButtonClicked(object sender, EventArgs e)
    {
        DateTime today = DateTime.Today;
        model.ExpenseDate = today.AddDays(-1);

        TodayButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        YesterdayButtonColor.BackgroundColor = Color.FromHex("#32df7f");
        TwoDaysAgoButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
    }

    public void TwoDaysAgoButtonClicked(object sender, EventArgs e)
    {
        DateTime today = DateTime.Today;
        model.ExpenseDate = today.AddDays(-2);

        TodayButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        YesterdayButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        TwoDaysAgoButtonColor.BackgroundColor = Color.FromHex("#32df7f");
    }

    private async void ClearExpense(object sender, EventArgs e)
    {
        var model = (ExpensesModel)BindingContext;
        ExpensesViewModel.Current.DeleteExpenses(model);
        ExpensesViewModel.Current.UpdateView();
        await Navigation.PopToRootAsync();
    }
}