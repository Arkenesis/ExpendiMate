using ExpendiMate.Models;
using ExpendiMate.ViewModels;

namespace ExpendiMate.Pages;

public partial class Analytics : ContentPage
{
    AnalyticsViewModel viewModel;

    int year = 0;
    public Analytics()
	{
        viewModel = new AnalyticsViewModel();
        BindingContext = viewModel;
		InitializeComponent();
	}

    private void Frame_Tapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var years = (ExpensesYearModel) frame.BindingContext;
        Navigation.PushAsync(new Category(years));
    }

    protected override void OnAppearing()
    {
        viewModel.UpdateView(year);
    }

    private void PlusYear(object sender, EventArgs e)
    {
        year++;
        viewModel.UpdateView(year);
    }

    private void MinusYear(object sender, EventArgs e)
    {
        year--;
        viewModel.UpdateView(year);
    }
}