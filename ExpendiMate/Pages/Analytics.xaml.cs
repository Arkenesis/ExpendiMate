using ExpendiMate.Models;
using ExpendiMate.ViewModels;

namespace ExpendiMate.Pages;

public partial class Analytics : ContentPage
{
    AnalyticsViewModel viewModel;
    bool initCompleted = false;

    int year = 0;
    public Analytics()
	{
        viewModel = new AnalyticsViewModel();
        BindingContext = viewModel;
		InitializeComponent();
        initCompleted = true;
	}

    private void Frame_Tapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var years = (ExpensesYearModel) frame.BindingContext;
        Navigation.PushAsync(new Category(years));
    }

    protected override void OnAppearing()
    {
        if(initCompleted)
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


    //https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
}