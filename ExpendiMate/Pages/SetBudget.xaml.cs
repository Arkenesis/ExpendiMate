using ExpendiMate.Models;
using ExpendiMate.ViewModels;
using System.Reflection;

namespace ExpendiMate.Pages;

public partial class SetBudget : ContentPage
{

    SetBudgetViewModel viewModel;

    public SetBudget()
    {
        viewModel = new SetBudgetViewModel();
        BindingContext = viewModel;
        InitializeComponent();
    }

    private async void IncomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SetIncome(viewModel.User));
    }

    protected override void OnAppearing()
    {
        //The name of the property that we listen in ViewModel
        viewModel.UpdateView();
    }

    private async void SetBudget50(object sender, EventArgs e)
    {
        var model = viewModel.User;
        model.BudgetPercent = 0.5;
        SetBudgetViewModel.Current.SaveAndUpdateUser(model);
        ManualBudget.Text = "Manual";

        if (model.BudgetBalance < 0)
        {
            await DisplayAlert("Warning", "You have exceeded your budget!", "OK");
        }
    }
    private async void SetBudget30(object sender, EventArgs e)
    {
        var model = viewModel.User;
        model.BudgetPercent = 0.3;
        SetBudgetViewModel.Current.SaveAndUpdateUser(model);
        ManualBudget.Text = "Manual";

        if (model.BudgetBalance < 0)
        {
            await DisplayAlert("Warning", "You have exceeded your budget!", "OK");
        }
    }

    private async void SetBudget20(object sender, EventArgs e)
    {
        var model = viewModel.User;
        model.BudgetPercent = 0.2;
        SetBudgetViewModel.Current.SaveAndUpdateUser(model);
        ManualBudget.Text = "Manual";

        if (model.BudgetBalance < 0)
        {
            await DisplayAlert("Warning", "You have exceeded your budget!", "OK");
        }
    }

    private async void SetBudgetManual(object sender, EventArgs e)
    {
        var model = viewModel.User;

        string result = await DisplayPromptAsync("Budget Percentage", "Enter your budget percentage (1-100)");

        if (double.TryParse(result, out double budgetPercent))
        {
            if (budgetPercent > 0 && budgetPercent <= 100)
            {
                model.BudgetPercent = budgetPercent / 100;
                SetBudgetViewModel.Current.SaveAndUpdateUser(model);
                ManualBudget.Text = (model.BudgetPercent * 100).ToString() + "%";
            }
            else
            {
                await DisplayAlert("Income", "Budget percentage must be a number within 1-100", "OK");
            }
        }
        else
        {
            await DisplayAlert("Income", "Invalid input. Please enter a valid number.", "OK");
        }

        if (model.BudgetBalance < 0)
        {
            await DisplayAlert("Warning", "You have exceeded your budget!", "OK");
        }
    }
}

