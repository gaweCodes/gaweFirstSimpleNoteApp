using System;
using gaweFirstSimpleNoteApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gaweFirstSimpleNoteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            NotesListView.ItemsSource = await App.Database.GetNotesAsync();
        }
        private async void AddNote(object sender, EventArgs e)
        {
            var title = await DisplayPromptAsync("Note Title", "enter a note title", "Done", "Cancel", "Note title");
            if (string.IsNullOrWhiteSpace(title)) return;
            await Navigation.PushAsync(new NoteEntryPage
            {
                BindingContext = new Note { Title = title }
            });
        }
        private async void NoteSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            await Navigation.PushAsync(new NoteEntryPage
            {
                BindingContext = e.SelectedItem as Note
            });
        }
    }
}