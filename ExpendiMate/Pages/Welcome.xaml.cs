namespace ExpendiMate.Pages;

using ExpendiMate.ViewModels;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

public partial class Welcome : ContentPage
{
    public Welcome()
	{
        InitializeComponent();
	}

    private async void Welcome_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new Login();
    }

}