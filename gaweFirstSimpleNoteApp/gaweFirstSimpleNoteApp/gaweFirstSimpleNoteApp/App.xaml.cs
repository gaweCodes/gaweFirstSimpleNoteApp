using System;
using System.IO;
using gaweFirstSimpleNoteApp.Data;
using gaweFirstSimpleNoteApp.Views;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp
{
    public partial class App : Application
    {
        // This also bad: NEVER do a singleton. But for demo I will do not change it.
        // See https://gaebster.ch/singleton/
        private static NoteDatabase _database;
        public static NoteDatabase Database =>
            _database ?? (_database = new NoteDatabase(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3")));
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new NotesPage());
        }
    }
}
