using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class EditNoteViewModel : BaseViewModel
    {
        public ICommand UpdateNote { get; }
        public EditNoteViewModel(int noteId)
        {
            var note = App.Database.GetNoteAsync(noteId).Result;
            Title = note.Title;
            Text = note.Text;
            UpdateNote = new Command(async () =>
                {
                    note.Title = Title;
                    note.Text = Text;
                    if (!await ValidateData()) return;
                    note.Date = DateTimeOffset.Now;
                    await App.Database.SaveNoteAsync(note);
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            );
        }
    }
}
