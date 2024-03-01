//using AndroidX.ViewPager.Widget;

namespace ExpendiMate
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            pageTitle.Text = Current.CurrentPage.Title;
        }
    }
}
