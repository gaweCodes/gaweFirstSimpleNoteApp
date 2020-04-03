using gaweFirstSimpleNoteApp.Views;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SignInPage());
        }
    }
}
