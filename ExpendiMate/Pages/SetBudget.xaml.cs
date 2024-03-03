using ExpendiMate.ViewModels;

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

    //Update the view if property change
    protected override void OnAppearing()
    {
        viewModel.OnPropertyChanged("User");
    }

    private async void IncomeClicked(object sender, EventArgs e)
    {

        Navigation.PushAsync(new SetIncome(viewModel.User));

        //string result = await DisplayPromptAsync("Income", "Enter your income amount...");

        //double incomeAmount = Int32.Parse(result);
        //if (incomeAmount < 0)
        //{
        //    await DisplayAlert("Income", "Income amount must be positive number and greater than zero.", "Yes");
        //    return;
        //}
        //else
        //{
        //    var model = viewModel.User;
        //    model.Income = incomeAmount;
        //    SetBudgetViewModel.Current.SaveAndUpdateUser(model);
        //}
    }
    //private async void IncomeClicked(object sender, EventArgs e)
    //{
    //    string result = await DisplayPromptAsync("Income", "Enter your income amount...");

    //    if (double.TryParse(result, out double incomeAmount))
    //    {
    //        if (incomeAmount >= 0)
    //        {
    //            var model = viewModel.User;
    //            model.Income = incomeAmount;
    //            viewModel.SaveAndUpdateUser(model);
    //        }
    //        else
    //        {
    //            await DisplayAlert("Income", "Income amount must be a positive number.", "OK");
    //        }
    //    }
    //    else
    //    {
    //        await DisplayAlert("Income", "Invalid input. Please enter a valid number.", "OK");
    //    }
    //}
}

