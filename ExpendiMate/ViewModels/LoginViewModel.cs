using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Services;
using ExpendiMate.Models;
using Firebase.Auth;
using Firebase.Storage;
using System.Net;


namespace ExpendiMate.ViewModels
{
    partial class LoginViewModel : ObservableObject
    {

        public static LoginViewModel Current { get; set; }

        public LoginViewModel()
        {
            Current = this;

            Email = Preferences.Default.Get<string>("EMAIL_P", "");
            Password = Preferences.Default.Get<string>("PASSWORD_P", "");
            RememberMe = Preferences.Default.Get<bool>("REMEMBER_ME_P", false);

        }

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        bool rememberMe;

        [ObservableProperty]
        UserCredential user;

        public async Task<bool> Login()
        {

            var client = new FirebaseAuthClient(FirebaseModel.config);

            try
            {
                IsBusy = true;
                var response = await client.SignInWithEmailAndPasswordAsync(Email, Password);

                User = response;
                NewPassword = Password;
                NewName = User.User.Info.DisplayName;
                if (RememberMe)
                {
                    Preferences.Set("EMAIL_P", Email);
                    Preferences.Set("PASSWORD_P", Password);
                    Preferences.Set("REMEMBER_ME_P", true);
                }
                else
                {
                    Preferences.Default.Remove("EMAIL_P");
                    Preferences.Default.Remove("PASSWORD_P");
                    Preferences.Default.Remove("REMEMBER_ME_P");
                }
            }
            catch (FirebaseAuthHttpException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                IsBusy = false;
                return false;
            }
            IsBusy = false;
            return true;
        }

        public async Task<bool> ForgotPassword(string email)
        {

            var client = new FirebaseAuthClient(FirebaseModel.config);

            try
            {
                IsBusy = true;
                await client.ResetEmailPasswordAsync(email);
            }
            catch (FirebaseAuthHttpException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                IsBusy = false;
                return false;
            }

            IsBusy = false;
            return true;

        }


        [ObservableProperty]
        double progressValue;

        [ObservableProperty]
        string downloadUrl;

        public async Task<bool> Upload()
        {
            ProgressValue = 0.00;
            try
            {
                IsBusy = true;
                //1. File path
                //2. What type of operation? Create Delete Open Overwrite
                //3. What specific operation can perform? read or write
                //4. Do you mind sharing the process with others?
                //   By default, 1 file can only opened by 1 process
                //   If specific FileShare, it allows the process to be shared with others, of course concurrent issues you have to settle by your logic.
                FileStream fs = File.Open(DatabaseServices.DatabaseFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                var task = new FirebaseStorage("courseplanner-192be.appspot.com")
                            .Child("expenses")
                            .Child(User.User.Uid)
                            .Child("expenses.sqlite")
                            .PutAsync(fs, CancellationToken.None, "application/sql");

                task.Progress.ProgressChanged += (s, e) => ProgressValue = e.Percentage;

                var response = await task;
                if (response != "")
                {
                    System.Diagnostics.Debug.WriteLine("Download link:" + response);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                IsBusy = false;
                return false;
            }

            IsBusy = false;
            return true;
        }

        [ObservableProperty]
        string newName;

        [ObservableProperty]
        string newPassword;

        public async Task<bool> SaveChanges()
        {
            try
            {
                IsBusy = true;
                var temp = User;
                await User.User.ChangeDisplayNameAsync(NewName);
                await User.User.ChangePasswordAsync(NewPassword);
            }
            catch (FirebaseAuthHttpException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                IsBusy = false;
                return false;
            }
            IsBusy = false;
            return true;

        }

        [ObservableProperty]
        string lastUploadData;

        [ObservableProperty]
        double fileSize;
        public async Task getLastUpLoad()
        {
            try
            {
                IsBusy = false;
                var task = await new FirebaseStorage("courseplanner-192be.appspot.com")
                 .Child("expenses")
                 .Child(User.User.Uid)
                 .Child("expenses.sqlite")
                 .GetMetaDataAsync();
                var time = task.Updated;
                LastUploadData = time.ToLocalTime().ToString(); //Sync with singapore time zone;

                var size = task.Size / 1024;
                FileSize = (double)size;
            }
            catch (FirebaseStorageException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                LastUploadData = "You don't have any backup data.";
                IsBusy = false;
            }
            catch (Exception ex)
            {
                LastUploadData = "Contact admin for support.";
                IsBusy = false;
            }
        }

        public async Task<bool> Restore()
        {
            try
            {
                IsBusy = true;
                FileStream fs = File.Open(DatabaseServices.DatabaseFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                var path = new FirebaseStorage("courseplanner-192be.appspot.com").Child("expenses").Child(User.User.Uid).Child("expenses.sqlite");
                var url = await path.GetDownloadUrlAsync();

                //Close database connection, if not may have issues when try to overwrite
                DatabaseServices.closeConnection();

                //Download the file
                var file = DownloadFromUrl(url, DatabaseServices.DatabaseFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                IsBusy = false;
                return false;
            }
            IsBusy = false;
            return true;
        }

        private bool DownloadFromUrl(string url, string path)
        {
            try
            {
                using (var client = new WebClient())
                {
                    IsBusy = true;
                    client.DownloadFile(url, path);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                IsBusy = false;
                return false;
            }
            IsBusy = false;
            return true;
        }

        [ObservableProperty]
        bool isBusy = false;

        [ObservableProperty]
        bool hideCredentials = true;
    }
}