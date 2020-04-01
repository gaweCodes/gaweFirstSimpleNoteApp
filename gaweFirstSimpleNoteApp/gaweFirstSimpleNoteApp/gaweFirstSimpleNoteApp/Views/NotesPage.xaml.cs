using System.Reflection;
using gaweFirstSimpleNoteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gaweFirstSimpleNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
            AddNoteToolbarItem.IconImageSource = ImageSource.FromResource("gaweFirstSimpleNoteApp.Icons.add.png",
                typeof(NotesPage).GetTypeInfo().Assembly);
        }
        protected override void OnAppearing()
        {
            BindingContext = new NotesViewModel();
            base.OnAppearing();
        }
    }
}