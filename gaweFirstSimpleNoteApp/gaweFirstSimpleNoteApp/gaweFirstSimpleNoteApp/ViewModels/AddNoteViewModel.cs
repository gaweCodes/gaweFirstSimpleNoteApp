using System;
using System.Windows.Input;
using gaweFirstSimpleNoteApp.Models;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class AddNoteViewModel : BaseViewModel
    {
        public ICommand SaveNote { get; }
        public AddNoteViewModel() => SaveNote = new Command(async () =>
            {
                if (!await ValidateData()) return;
                await App.Database.SaveNoteAsync(new Note {Date = DateTimeOffset.Now, Text = Text, Title = Title});
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        );
    }
}
