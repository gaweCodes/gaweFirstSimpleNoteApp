using System;
using gaweFirstSimpleNoteApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gaweFirstSimpleNoteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEntryPage : ContentPage
    {
        public NoteEntryPage()
        {
            InitializeComponent();
        }
        private async void SaveNote(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTimeOffset.Now;
            await App.Database.SaveNoteAsync(note);
            await Navigation.PopAsync();
        }
        private async void DeleteNote(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            if(!await DisplayAlert("GaweNotes", $"Are you sure you want to delete the {note.Title} note?", "Yes", "Cancel")) return;
            await App.Database.DeleteNoteAsync(note);
            await Navigation.PopAsync();
        }
    }
}