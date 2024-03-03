using ExpendiMate.Models;
using ExpendiMate.ViewModels;

namespace ExpendiMate.Pages;

public partial class SetIncome : ContentPage
{
    UserModel model;

    //Pass the variable in
    public SetIncome(UserModel m)
    {
        model = m;
        BindingContext = model;
        InitializeComponent();
    }

    private async void SaveIncome(object sender, EventArgs e)
    {
        var model = (UserModel) BindingContext;
        if (model.Income < 0 )
        {
            await DisplayAlert("Warning", "You must enter a valid number.", "OK");
            return;
        }
        SetBudgetViewModel.Current.SaveAndUpdateUser(model);
        await Navigation.PopAsync();
    }
}