using System.Reflection;
using gaweFirstSimpleNoteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gaweFirstSimpleNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePage : ContentPage
    {
        private readonly string _title;
        private readonly string _text;
        public AddNotePage(string title = "", string text = "")
        {
            InitializeComponent();
            _title = title;
            _text = text;
            SaveNoteToolbarItem.IconImageSource = ImageSource.FromResource("gaweFirstSimpleNoteApp.Icons.save.png",
                typeof(AddNotePage).GetTypeInfo().Assembly);
        }
        protected override void OnAppearing()
        {
            BindingContext = new AddNoteViewModel(){Text = _text, Title = _title};
            base.OnAppearing();
        }
    }
}