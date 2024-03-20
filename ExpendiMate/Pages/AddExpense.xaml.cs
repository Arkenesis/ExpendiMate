using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.ViewModels;
using Microsoft.Maui;

namespace ExpendiMate.Pages;

public partial class AddExpense : ContentPage
{
    ExpensesModel model;
    //Pass the variable in
    public AddExpense(ExpensesModel m)
    {
        model = m;
        BindingContext = model;
        if (model.ExpenseCategory == "")
        {
            if (CategoryModel.CategoryList != null)
                model.ExpenseCategory = CategoryModel.CategoryList[0];
        }
        if (model.ExpenseDate == DateTime.MinValue)
        {
            model.ExpenseDate = DateTime.Now;
        }
        InitializeComponent();
    }

    private async void AddExpenses(object sender, EventArgs e)
    {
        var model = (ExpensesModel) BindingContext;
        if (model.ExpenseName == null || model.ExpenseName == "")
        {
            await DisplayAlert("Warning", "You must enter expense name.", "OK");
            return;
        }
        if (model.ExpenseCategory == null || model.ExpenseCategory == "")
        {
            await DisplayAlert("Warning", "You must enter expense category", "OK");
            return;
        }
        if (model.ExpenseCost <= 0)
        {
            await DisplayAlert("Warning", "You must enter positive number & bigger than 0.", "OK");
            return;
        }
        ExpensesViewModel.Current.SaveExpenses(model);
        ExpensesViewModel.Current.UpdateView();
        //ExpensesViewModelFirebase.Current.SaveExpenses(model);
        await Navigation.PopToRootAsync();
    }

    private async void AddPhoto(object sender, EventArgs e)
    {
        FileResult photo = null;

        PermissionStatus status = await GetMediaPermission();
        if (status == PermissionStatus.Granted)
        {
            photo = await MediaPicker.Default.PickPhotoAsync();
        }

        if (photo != null)
        {
            string imagesDir = System.IO.Path.Combine(FileSystem.CacheDirectory, "images");
            System.Diagnostics.Debug.WriteLine(imagesDir);
            //Create folder
            System.IO.Directory.CreateDirectory(imagesDir);
            //Create file path
            var newFile = Path.Combine(imagesDir, photo.FileName);
            //Create photo
            using (var stream = await photo.OpenReadAsync())
            //Save the photo to file path
            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);
            }
            model.ExpensePicturePath = newFile;
            OnPropertyChanged("ExpensePicturePath");
        }
    }

    public async Task<PermissionStatus> GetMediaPermission()
    {
        PermissionStatus status = await Permissions.RequestAsync<Permissions.StorageRead>();

        if (status == PermissionStatus.Granted)
            return status;

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            // Prompt the user to turn on in settings
            // On iOS once a permission has been denied it may not be requested again from the application
            await DisplayAlert("Warning", "You need to manually enable camera access for this app in settings.", "OK");
            return status;
        }

        if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
        {
            // Prompt the user with additional information as to why the permission is needed
            await DisplayAlert("Warning", "This app requires camera access to add photos for your character", "OK");
        }

        status = await Permissions.RequestAsync<Permissions.StorageRead>();

        return status;
    }

    public void TodayButtonClicked(object sender, EventArgs e)
    {
        model.ExpenseDate = DateTime.Today;

        TodayButtonColor.BackgroundColor = Color.FromHex("#32df7f");
        YesterdayButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        TwoDaysAgoButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
    }

    public void YesterdayButtonClicked(object sender, EventArgs e)
    {
        DateTime today = DateTime.Today;
        model.ExpenseDate = today.AddDays(-1);

        TodayButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        YesterdayButtonColor.BackgroundColor = Color.FromHex("#32df7f");
        TwoDaysAgoButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
    }

    public void TwoDaysAgoButtonClicked(object sender, EventArgs e)
    {
        DateTime today = DateTime.Today;
        model.ExpenseDate = today.AddDays(-2);

        TodayButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        YesterdayButtonColor.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        TwoDaysAgoButtonColor.BackgroundColor = Color.FromHex("#32df7f");
    }

    private async void Delete(object sender, EventArgs e)
    {
        var model = (ExpensesModel)BindingContext;
        ExpensesViewModel.Current.DeleteExpenses(model);
        ExpensesViewModel.Current.UpdateView();
        await Navigation.PopToRootAsync();
    }
}