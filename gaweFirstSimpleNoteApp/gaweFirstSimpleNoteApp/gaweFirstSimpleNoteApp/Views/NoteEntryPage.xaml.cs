using System.Reflection;
using gaweFirstSimpleNoteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gaweFirstSimpleNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEntryPage : ContentPage
    {
        private readonly int _noteId;
        public NoteEntryPage(int noteId)
        {
            _noteId = noteId;
            InitializeComponent();
            EditNoteToolbarItem.IconImageSource = ImageSource.FromResource("gaweFirstSimpleNoteApp.Icons.edit.png",
                typeof(NoteEntryPage).GetTypeInfo().Assembly);
            DeleteNoteToolbarItem.IconImageSource = ImageSource.FromResource("gaweFirstSimpleNoteApp.Icons.delete.png",
                typeof(NoteEntryPage).GetTypeInfo().Assembly);
        }
        protected override void OnAppearing()
        {
            BindingContext = new NoteEntryViewModel(_noteId);
            base.OnAppearing();
        }
    }
}