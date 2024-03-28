using Microsoft.Maui.Storage;

namespace ExpendiMate.Pages;

using ExpendiMate.ViewModels;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

public partial class AccountSettings : ContentPage
{
    LoginViewModel viewModel;

    public AccountSettings()
    {
        InitializeComponent();
    }
    protected async override void OnAppearing()
    {
        if (viewModel == null)
        {
            if (LoginViewModel.Current.User == null)
            {
                await DisplayAlert("Login", "Please log in into the account before changing the account settings.", "Ok");
            }
            else
            {
                viewModel = LoginViewModel.Current;
                BindingContext = viewModel;
                await viewModel.getLastUpLoad();
            }
        }
        else
        {
            if (LoginViewModel.Current.User != null)
            {
                await viewModel.getLastUpLoad();
            }
        }


    }

    private async void SaveChanges_Clicked(object sender, EventArgs e)
    {
        if (viewModel == null) return;
        var response = await viewModel.SaveChanges();
        if (response)
        {
            await DisplayAlert("Login", "Successfully change your account details.", "Ok");
        }
        else
        {
            await DisplayAlert("Login", "Failed to change your account details.", "Ok");
        }
    }

    private async void DiscardChanges_Clicked(object sender, EventArgs e)
    {
        if (viewModel == null) return;
        viewModel.NewName = viewModel.User.User.Info.DisplayName;
        viewModel.NewPassword = viewModel.Password;
    }

    private async void Upload_Clicked(object sender, EventArgs e)
    {
        if (viewModel == null) return;
        bool response = await viewModel.Upload();
        if (response)
        {
            await viewModel.getLastUpLoad();
            await DisplayAlert("Upload Successful", "Your data has been successfully saved!", "OK");
        }
        else
        {
            await DisplayAlert("Upload Fail", "Please ensure you have a stable internet connection.", "OK");
        }
    }

    private async void Restore_Clicked(object sender, EventArgs e)
    {
        if (viewModel == null) return;
        bool answer = await DisplayAlert("Restore", "Would you like to restore your data? It will overwrite your current data.", "Yes", "No");
        if (answer)
        {
            bool response = await viewModel.Restore();
            if (response)
            {
                await DisplayAlert("Restore Successfuly.", "Please restart the application for changes to take effect.", "OK");
                Application.Current.Quit();
            }
            else
            {
                await DisplayAlert("Restore Fail.", "There is no backup data available.", "OK");
            }
        }
    }

    private async void Logout_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Logout", "You have been logged out", "Ok");
        App.Current.MainPage = new Login();
    }
}