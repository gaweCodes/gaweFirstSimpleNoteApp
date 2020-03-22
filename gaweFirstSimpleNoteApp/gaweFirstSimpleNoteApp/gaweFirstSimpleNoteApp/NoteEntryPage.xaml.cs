using System;
using System.IO;
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
            if (string.IsNullOrWhiteSpace(note.FilePath))
            {
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.gaweNotes.txt");
                File.WriteAllText(filename, note.Text);
            }
            else
                File.WriteAllText(note.FilePath, note.Text);
            await Navigation.PopAsync();
        }
        private async void DeleteNote(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            if (File.Exists(note.FilePath)) File.Delete(note.FilePath);
            await Navigation.PopAsync();
        }
    }
}