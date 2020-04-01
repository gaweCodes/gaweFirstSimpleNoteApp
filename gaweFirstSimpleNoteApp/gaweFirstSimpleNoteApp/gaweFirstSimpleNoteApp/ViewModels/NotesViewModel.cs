using System.Collections.ObjectModel;
using System.Windows.Input;
using gaweFirstSimpleNoteApp.Models;
using gaweFirstSimpleNoteApp.Views;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
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
        public ObservableCollection<Note> Notes { get; }
        public ICommand AddNote { get; }
        public ICommand ChangedSelection { get; }
        public NotesViewModel()
        {
            Notes = new ObservableCollection<Note>(App.Database.GetNotesAsync().Result);
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
    }
}
