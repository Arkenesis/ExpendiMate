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
        if(viewModel == null)
        {
            if (LoginViewModel.Current.User == null)
            {
                await DisplayAlert("Login", "Log in into the account before changing the account settings.", "Ok");
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
            await DisplayAlert("Login", "Fail to change your account details.", "Ok");
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
        if(response)
        {
            await viewModel.getLastUpLoad();
            await DisplayAlert("Upload Successfuly.", "Best practice as an engineer tho.", "OK");
        }
        else
        {
            await DisplayAlert("Upload Fail", "Pls ensure you have stable internet connection.", "OK");
        }
    }

    private async void Restore_Clicked(object sender, EventArgs e)
    {
        if (viewModel == null) return;
        bool answer = await DisplayAlert("Restore", "Would you like to restore data from online backup? It will overwrite your current data.", "Yes", "No");
        if(answer)
        {
            bool response = await viewModel.Restore();
            if (response)
            {
                await DisplayAlert("Restore Successfuly.", "The application required restart to take effect.", "OK");
                Application.Current.Quit();
            }
            else
            {
                await DisplayAlert("Restore Fail.", "Pls ensure your backup data is available!", "OK");
            }
        }
    }





}