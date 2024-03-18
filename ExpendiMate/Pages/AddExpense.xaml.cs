using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.ViewModels;

namespace ExpendiMate.Pages;

public partial class AddExpense : ContentPage
{
    ExpensesModel model;

    //Pass the variable in
    public AddExpense(ExpensesModel m)
    {
        model = m;
        BindingContext = model;
        InitializeComponent();
    }

    private async void AddExpenses(object sender, EventArgs e)
    {
        var model = (ExpensesModel) BindingContext;
        if (model.ExpenseName == null )
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

    private async void ClearAllExpenses(object sender, EventArgs e)
    {
        ExpensesViewModel.Current.DeleteAllData();
        ExpensesViewModel.Current.UpdateView();
        await Navigation.PopToRootAsync();
    }

    private async void AddPhoto(object sender, EventArgs e)
    {
        FileResult photo = null;

        PermissionStatus status = await GetMediaPermission();
        if (status == PermissionStatus.Granted)
        {
            photo = await MediaPicker.PickPhotoAsync();
        }

        if (photo != null)
        {
            model.ExpensePicturePath = photo.FullPath;
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
}