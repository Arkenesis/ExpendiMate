using ExpendiMate.Models;
using ExpendiMate.ViewModels;

namespace ExpendiMate.Pages;
public partial class Category : ContentPage
{
    CategoryViewModel model;

    string categoryName;

    public Category(ExpensesCategoryModel m)
	{
        Title = m.Name;
        model = new CategoryViewModel(m);
        categoryName = m.Name;
        BindingContext = model;
        InitializeComponent();
    }

    public Category(ExpensesYearModel m)
    {
        Title = m.Name;
        model = new CategoryViewModel(m);
        model.IconColor = "#68DE68";
        categoryName = m.Name;
        BindingContext = model;
        InitializeComponent();
    }


    private void ExpenseButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddExpense(new ExpensesModel() { ExpenseCategory = categoryName, ExpenseDate = DateTime.Now}));
    }

    private void CategoryItem_Tapped(object sender, TappedEventArgs e)
    {
        var frame = (Frame) sender;
        var data = (ExpensesModel)frame.BindingContext;
        Navigation.PushAsync(new AddExpense(data));
    }

}