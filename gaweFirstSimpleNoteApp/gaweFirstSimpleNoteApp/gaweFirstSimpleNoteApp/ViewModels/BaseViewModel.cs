using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using gaweFirstSimpleNoteApp.Models;
using gaweFirstSimpleNoteApp.Services;
using gaweFirstSimpleNoteApp.Views;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            var luisService = new LuisService();
            DoTask = new Command(async () =>
            {
                var intent = await luisService.GetRecognizedIntent();
                switch (intent.TopScoringIntent.Intent)
                {
                    case "Note.Create":
                        switch (intent.Entities.Count)
                        {
                            case 1 when intent.Entities.First().Type == "Note.Title":
                                await Application.Current.MainPage.Navigation.PushAsync(new AddNotePage(intent.Entities.First().Value));
                                break;
                            case 2 when intent.Entities.Any(x => x.Type == "Note.Title") && intent.Entities.Any(x => x.Type == "Note.Text"):
                                await Application.Current.MainPage.Navigation.PushAsync(
                                    new AddNotePage(intent.Entities.First(x => x.Type == "Note.Title").Value,
                                        intent.Entities.First(a => a.Type == "Note.Text").Value));
                                break;
                            default:
                                await Application.Current.MainPage.Navigation.PushAsync(new AddNotePage());
                                break;
                        }
                        break;
                    case "Note.Open":
                        if (intent.Entities.Count == 1 && intent.Entities.First().Type == "Note.Title")
                        {
                            var noteService = new NoteService(((User)Application.Current.Properties["user"]).JwtToken);
                            var notes = JsonConvert.DeserializeObject<List<Note>>(await noteService.GetAll(((User)Application.Current.Properties["user"]).Id));
                            var foundNote = notes.SingleOrDefault(x => x.Title == intent.Entities.First().Value);
                            if (foundNote == default)
                                await TextToSpeech.SpeakAsync("The note was not in the database.");
                            else
                                await Application.Current.MainPage.Navigation.PushAsync(
                                    new NoteEntryPage(foundNote.Id));
                        }
                        break;
                    // Update
                    // Delete
                    case "None":
                        return;
                    default: return;
                }
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public async Task<bool> ValidateData()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                await Application.Current.MainPage.DisplayAlert("Invalid data", "You must enter a valid title.", "OK");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Text))
            {
                await Application.Current.MainPage.DisplayAlert("Invalid data", "You must enter a valid note text.", "OK");
                return false;
            }
            return true;
        }
        public ICommand DoTask { get; }
    }
}
