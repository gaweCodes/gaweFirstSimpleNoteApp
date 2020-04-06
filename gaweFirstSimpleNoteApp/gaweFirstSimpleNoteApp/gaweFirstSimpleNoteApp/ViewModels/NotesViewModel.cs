using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using gaweFirstSimpleNoteApp.Models;
using gaweFirstSimpleNoteApp.Services;
using gaweFirstSimpleNoteApp.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        private readonly NoteService _noteService = new NoteService(((User)Application.Current.Properties["user"]).JwtToken);
        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }
        public string UserGreeting { get; }
        public ObservableCollection<Note> Notes { get; }
        public ICommand AddNote { get; }
        public ICommand ChangedSelection { get; }
        public NotesViewModel()
        {
            UserGreeting =
                $"Hello, {((User)Application.Current.Properties["user"]).Fullname}!";
            Notes = new ObservableCollection<Note>();
            AddNote = new Command(async () =>
                await Application.Current.MainPage.Navigation.PushAsync(new AddNotePage()));
            ChangedSelection = new Command(async () =>
            {
                if(SelectedNote == null) return;
                var noteEntryPage = new NoteEntryPage(SelectedNote.Id);
                await Application.Current.MainPage.Navigation.PushAsync(noteEntryPage);
                SelectedNote = null;
            });
        }
        public async Task LoadNotes()
        {
            var notesString = await _noteService.GetAll(((User)Application.Current.Properties["user"]).Id);
            if (notesString.StartsWith("ERROR"))
            {
                await Application.Current.MainPage.DisplayAlert("No data found",
                    "We were not able to load data. Please check your internet connection! " + notesString, "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            var notesList = JsonConvert.DeserializeObject<List<Note>>(notesString);
            notesList.ForEach(note => Notes.Add(note));
        }
    }
}
