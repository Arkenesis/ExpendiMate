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

        public async Task<bool> SignUp()
        {

            var client = new FirebaseAuthClient(FirebaseModel.config);

            try
            {
                var response = await client.CreateUserWithEmailAndPasswordAsync(Email, Password);
            }
            catch(FirebaseAuthHttpException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
            return true;

        }

    }
}
