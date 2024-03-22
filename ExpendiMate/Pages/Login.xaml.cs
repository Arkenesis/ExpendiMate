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
        var frame = (Frame)sender;
        var button = (Button) frame.Content;
        Animation parent = new Animation();
        Animation fade = new Animation(x => button.Opacity = x, 1, 0, Easing.CubicIn);
        Animation size = new Animation(x => button.Scale = x, 1, 0, Easing.SpringIn);

        parent.Add(0, 0.9, fade);
        parent.Add(0, 1, size);

        parent.Commit(this, "ButtonAnimation", 16, 1000, null, AnimationEnd);

        var response = await viewModel.Login();
        if(response)
        {
            await DisplayAlert("Login", "Successfully log in into your account.", "Ok");
            App.Current.MainPage = new AppShell();

        }
        else
        {
            await DisplayAlert("Login", "Fail to log in, please try again later.", "Ok");
        }
    }
    private async void AnimationEnd(double x, bool y)
    {
        
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
            await DisplayAlert("Forgot Password", "Successfully sending reset password request to your email address.", "Ok");
        }
        else
        {
            await DisplayAlert("Forgot Password", "Fail to send reset password request to your email address.", "Ok");
        }
    }
}