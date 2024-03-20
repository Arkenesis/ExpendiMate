using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.Services;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Firebase.Storage;
using System.Text;


namespace ExpendiMate.ViewModels
{
    internal partial class SignUpViewModel : ObservableObject
    {
        public static SignUpViewModel Current { get; set; }

        public SignUpViewModel()
        {
            Current = this;
        }

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        bool hideCredentials = true;

        public async Task<bool> SignUp()
        {

            var client = new FirebaseAuthClient(FirebaseModel.config);

            try
            {
                IsBusy = true;
                var response = await client.CreateUserWithEmailAndPasswordAsync(Email, Password, Name);
            }
            catch(FirebaseAuthHttpException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                IsBusy = false;
                return false;
            }
            IsBusy = false;
            return true;

        }

    }
}
