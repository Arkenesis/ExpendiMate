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
        if (viewModel.Name == "")
        {
            await DisplayAlert("Sign Up", "You are required to enter your username.", "Ok");
            return;
        }
        if (viewModel.Password == "")
        {
            await DisplayAlert("Sign Up", "You are required to fill in the password.", "Ok");
            return;
        }
        if (viewModel.Email == "")
        {
            await DisplayAlert("Sign Up", "You are required to register with valid email address", "Ok");
            return;
        }
        bool response = await viewModel.SignUp();
        if (response)
        {
            await DisplayAlert("Sign Up", "You have been successfully registered!", "Ok");
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Sign Up", "Error: Account registration failed, please try again.", "Ok");
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
            await DisplayAlert("Forgot Password", "Password reset request to your email address.", "Ok");
        }
        else
        {
            await DisplayAlert("Forgot Password", "Fail to send reset password request to your email address.", "Ok");
        }
    }

}