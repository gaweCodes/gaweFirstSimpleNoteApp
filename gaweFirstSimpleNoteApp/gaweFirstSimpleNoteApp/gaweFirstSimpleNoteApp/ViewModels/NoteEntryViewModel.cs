using gaweFirstSimpleNoteApp.Views;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class NoteEntryViewModel : BaseViewModel
    {
        public Command EditNote { get; }
        public Command DeleteNote { get; }
        public NoteEntryViewModel(int noteId)
        {
            var note = App.Database.GetNoteAsync(noteId).Result;
            Title = note.Title;
            Text = note.Text;
            EditNote = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new EditNotePage(noteId)));
            DeleteNote = new Command(async () =>
            {
                var result = await Application.Current.MainPage.DisplayAlert("Delete Note",
                    "Are you sure you want to delete the note?", "Yes", "No");
                if (result) await App.Database.DeleteNoteAsync(note);
                await Application.Current.MainPage.Navigation.PopAsync();
            });
        }
    }
}
