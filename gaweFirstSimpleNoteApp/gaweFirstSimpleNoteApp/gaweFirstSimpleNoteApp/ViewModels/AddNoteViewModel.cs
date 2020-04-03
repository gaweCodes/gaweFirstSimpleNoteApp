using System;
using System.Threading;
using System.Windows.Input;
using gaweFirstSimpleNoteApp.Models;
using gaweFirstSimpleNoteApp.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class AddNoteViewModel : BaseViewModel
    {
        private readonly NoteService _noteService = new NoteService(((User)Application.Current.Properties["user"]).JwtToken);
        public ICommand SaveNote { get; }
        public AddNoteViewModel() => SaveNote = new Command(async () =>
            {
                if (!await ValidateData()) return;
                var noteString = JsonConvert.SerializeObject(new Note
                {
                    Id = Guid.NewGuid(),
                    LastModifiedAt = DateTimeOffset.Now,
                    Text = Text,
                    Title = Title,
                    UserId = ((User) Application.Current.Properties["user"]).Id
                });
                Thread.Sleep(200);
                var result = await _noteService.Add(noteString);
                if (result.StartsWith("ERROR"))
                    await Application.Current.MainPage.DisplayAlert("No data found",
                        "We were not able to add note. Please check your internet connection! " + result, "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        );
    }
}
