using ExpendiMate.Pages;

namespace ExpendiMate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ExpenseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddExpense());
        }
    }
}
