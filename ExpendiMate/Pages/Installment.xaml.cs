using ExpendiMate.Models;
using ExpendiMate.Services.PartialMethods;
using ExpendiMate.ViewModels;

namespace ExpendiMate.Pages;
public partial class Installment : ContentPage
{
	InstallmentViewModel viewModel;
    bool isInitializing = true;
    public Installment()
	{
		viewModel = new InstallmentViewModel();
		BindingContext = viewModel;
        InitializeComponent();
        isInitializing = false;
    }

    private void InstallmentButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SetInstallment(new InstallmentModel()));
    }

    private void InstallmentItem_Clicked(object sender, ItemTappedEventArgs e)
    {
        InstallmentModel tapped = (InstallmentModel) e.Item;
        Navigation.PushAsync(new SetInstallment(tapped));
    }

    private void ToggleInstallment(object sender, ToggledEventArgs e)
    {
        if (isInitializing)
        {
            return;
        }
        //Issues   : Initialization of switch button in Installment Pages fires the "Toggled event", conflict with the ListView which crash the program.
        //Solution : Check the value of BindingContext, only fires the update function if the user toggle the switch.
        Switch toggleSwitch = (Switch) sender;
        InstallmentModel item = (InstallmentModel) toggleSwitch.BindingContext;
        if (item == null) // Issues: Creating new installment fires the event
        {
            return;       // Solution: Just stop it.
        }
        if(item.InstallmentIsActivated != e.Value)
        {
            item.InstallmentIsActivated = e.Value;
            viewModel.SaveAndUpdate(item);

        }
    }

    //The name of the property that we listen in ViewModel
    protected override void OnAppearing()
    {
        viewModel.UpdateView();
    }
}