namespace ExpendiMate.Pages;

using ExpendiMate.ViewModels;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

public partial class SignUp : ContentPage
{

    SignUpViewModel viewModel;

    public SignUp()
	{

        NavigationPage.SetHasBackButton(this, false);
        viewModel = new SignUpViewModel();
        BindingContext = viewModel;
        InitializeComponent();
	}

    private async void SignUp_Clicked(object sender, EventArgs e)
    { 
        bool response = await viewModel.SignUp();
        if(response)
        {
            await DisplayAlert("Sign Up","Successfuly register on the services.","Ok");
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Sign Up", "Fail to register on the services.", "Ok");
        }
    }

    private async void LogInPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void Anonymous_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new AppShell();
    }

    private async void ForgotPassword_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Forgot Password", "Enter your e-mail address:");
        LoginViewModel viewModel = LoginViewModel.Current;
        bool response = await viewModel.ForgotPassword(result);

        if (response)
        {
            await DisplayAlert("Forgot Password", "Successfully sending reset password request to your email address.", "Ok");
        }
        else
        {
            await DisplayAlert("Forgot Password", "Fail to send reset password request to your email address.", "Ok");
        }
    }

}