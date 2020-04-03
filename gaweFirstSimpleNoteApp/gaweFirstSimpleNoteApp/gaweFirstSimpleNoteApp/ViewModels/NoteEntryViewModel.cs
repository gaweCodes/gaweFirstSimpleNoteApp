using System;
using System.Threading.Tasks;
using gaweFirstSimpleNoteApp.Models;
using gaweFirstSimpleNoteApp.Services;
using gaweFirstSimpleNoteApp.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class NoteEntryViewModel : BaseViewModel
    {
        private readonly NoteService _noteService = new NoteService(((User)Application.Current.Properties["user"]).JwtToken);
        private readonly Guid _noteId;
        public Command EditNote { get; }
        public Command DeleteNote { get; }
        public NoteEntryViewModel(Guid noteId)
        {
            _noteId = noteId;
            EditNote = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new EditNotePage(noteId)));
            DeleteNote = new Command(async () =>
            {
                var result = await Application.Current.MainPage.DisplayAlert("Delete Note",
                    "Are you sure you want to delete the note?", "Yes", "No");
                if (result)
                {
                    var deleteResult = await _noteService.Delete(noteId);
                    if (deleteResult.StartsWith("ERROR"))
                    {
                        await Application.Current.MainPage.DisplayAlert("Delete failed",
                            "An error occured during delete: " + deleteResult, "OK");
                    }
                }
                await Application.Current.MainPage.Navigation.PopAsync();
            });
        }
        public async Task LoadData()
        {
            var noteString = await _noteService.GetById(_noteId);
            if (noteString.StartsWith("ERROR"))
            {
                await Application.Current.MainPage.DisplayAlert("No data found",
                    "We were not able to load data. Please check your internet connection! " + noteString, "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            var note = JsonConvert.DeserializeObject<Note>(noteString);
            Title = note.Title;
            Text = note.Text;
        }
    }
}
