using ExpendiMate.Models;

namespace ExpendiMate.Pages;

public partial class AddExpense : ContentPage
{
	public List<MainViewModel> Expense { get; set; }
	public AddExpense()
	{
		Expense = new List<MainViewModel>();
		Expense.Add(new MainViewModel()
		{
			ExpenseName = "",
			ExpenseCategory = "",
			ExpenseCost = 0,
			ExpenseComments = "",
			ExpenseDate = DateTime.Today,
			ExpensePicturePath = string.Empty
		});

		BindingContext = this;

		InitializeComponent();
	}
}