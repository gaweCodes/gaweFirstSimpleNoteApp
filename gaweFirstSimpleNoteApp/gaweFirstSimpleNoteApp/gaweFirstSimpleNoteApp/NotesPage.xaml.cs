using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var notes = new List<Note>();
            var files = Directory.EnumerateFiles(App.FolderPath, "*.gaweNotes.txt");
            Parallel.ForEach(files, file =>
            {
                var newNote = new Note
                {
                    Date = File.GetCreationTime(file),
                    Text = File.ReadAllText(file),
                    FileName = Path.GetFileNameWithoutExtension(file),
                    FilePath = file
                };
                notes.Add(newNote);
            });
            listView.ItemsSource = notes.OrderBy(d => d.Date).ToList();
        }
        private async void AddNote(object sender, EventArgs e) => await Navigation.PushAsync(new NoteEntryPage
        {
            BindingContext = new Note()
        });
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