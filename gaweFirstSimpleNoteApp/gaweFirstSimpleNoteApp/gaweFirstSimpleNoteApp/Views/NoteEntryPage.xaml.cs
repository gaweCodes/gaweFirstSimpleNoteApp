using System;
using System.Reflection;
using gaweFirstSimpleNoteApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gaweFirstSimpleNoteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEntryPage : ContentPage
    {
        private readonly Guid _noteId;
        public NoteEntryPage(Guid noteId)
        {
            _noteId = noteId;
            InitializeComponent();
            EditNoteToolbarItem.IconImageSource = ImageSource.FromResource("gaweFirstSimpleNoteApp.Icons.edit.png",
                typeof(NoteEntryPage).GetTypeInfo().Assembly);
            DeleteNoteToolbarItem.IconImageSource = ImageSource.FromResource("gaweFirstSimpleNoteApp.Icons.delete.png",
                typeof(NoteEntryPage).GetTypeInfo().Assembly);
        }
        protected override async void OnAppearing()
        {
            var viewModel = new NoteEntryViewModel(_noteId);
            await viewModel.LoadData();
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}