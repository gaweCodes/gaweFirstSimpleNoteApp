using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using gaweFirstSimpleNoteApp.Models;
using gaweFirstSimpleNoteApp.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class EditNoteViewModel : BaseViewModel
    {
        private readonly NoteService _noteService = new NoteService(((User)Application.Current.Properties["user"]).JwtToken);
        private readonly Guid _noteId;
        private Note _note;
        public ICommand UpdateNote { get; }
        public EditNoteViewModel(Guid noteId)
        {
            _noteId = noteId;
            UpdateNote = new Command(async () =>
                {
                    _note.Title = Title;
                    _note.Text = Text;
                    if (!await ValidateData()) return;
                    _note.LastModifiedAt = DateTimeOffset.Now;
                    var data = JsonConvert.SerializeObject(_note);
                    Thread.Sleep(1000);
                    var result = await _noteService.Update(data);
                    if(result.StartsWith("ERROR"))
                        await Application.Current.MainPage.DisplayAlert("No data found",
                            "We were not able to update the data. Please check your internet connection! " + result, "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            );
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
            _note = JsonConvert.DeserializeObject<Note>(noteString);
            Title = _note.Title;
            Text = _note.Text;
        }
    }
}
