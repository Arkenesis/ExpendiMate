using ExpendiMate.Models;
using ExpendiMate.ViewModels;

namespace ExpendiMate.Pages;

public partial class SetInstallment : ContentPage
{
    InstallmentModel model;

    //Pass the variable in
    public SetInstallment(InstallmentModel m)
    {
        model = m;
        BindingContext = model;
        InitializeComponent();
    }

    private async void SaveInstallment(object sender, EventArgs e)
    {
        var model = (InstallmentModel) BindingContext;
        InstallmentViewModel.Current.SaveAndUpdate(model);
        await Navigation.PopAsync();
    }

}