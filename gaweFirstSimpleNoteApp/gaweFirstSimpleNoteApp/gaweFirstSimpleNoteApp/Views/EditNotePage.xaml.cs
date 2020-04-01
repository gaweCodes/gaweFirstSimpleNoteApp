using System.Reflection;
using gaweFirstSimpleNoteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gaweFirstSimpleNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNotePage : ContentPage
    {
        private readonly int _noteId;
        public EditNotePage(int noteId)
        {
            _noteId = noteId;
            InitializeComponent();
            SaveNoteToolbarItem.IconImageSource = ImageSource.FromResource("gaweFirstSimpleNoteApp.Icons.save.png",
                typeof(EditNotePage).GetTypeInfo().Assembly);
        }
        protected override void OnAppearing()
        {
            BindingContext = new EditNoteViewModel(_noteId);
            base.OnAppearing();
        }
    }
}