using System;
using System.Reflection;
using gaweFirstSimpleNoteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gaweFirstSimpleNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNotePage : ContentPage
    {
        private readonly Guid _noteId;
        public EditNotePage(Guid noteId)
        {
            _noteId = noteId;
            InitializeComponent();
            SaveNoteToolbarItem.IconImageSource = ImageSource.FromResource("gaweFirstSimpleNoteApp.Icons.save.png",
                typeof(EditNotePage).GetTypeInfo().Assembly);
        }
        protected override async void OnAppearing()
        {
            var viewModel = new EditNoteViewModel(_noteId);
            await viewModel.LoadData();
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}