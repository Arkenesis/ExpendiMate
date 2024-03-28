namespace ExpendiMate.Pages;

using ExpendiMate.ViewModels;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

public partial class Login : ContentPage
{

    LoginViewModel viewModel;

    public Login()
    {
        viewModel = new LoginViewModel();
        BindingContext = viewModel;
        InitializeComponent();
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        var response = await viewModel.Login();
        if (response)
        {
            await DisplayAlert("Login", "Login successful", "Ok");
            App.Current.MainPage = new AppShell();

        }
        else
        {
            await DisplayAlert("Login", "Login failed. Please check that your email or password is correct.", "Ok");
        }
    }

    private void Anonymous_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new AppShell();
    }


    private async void SingUpPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new SignUp());
    }

    private async void ForgotPassword_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Forgot Password", "Enter your e-mail address:");
        bool response = await viewModel.ForgotPassword(result);

        if (response)
        {
            await DisplayAlert("Forgot Password", "Password change request sent to your email.", "Ok");
        }
        else
        {
            await DisplayAlert("Forgot Password", "Fail to send reset password request to your email address.", "Ok");
        }
    }
}