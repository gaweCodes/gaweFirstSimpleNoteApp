using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "gaweNotes.txt");
        public MainPage()
        {
            InitializeComponent();
            if (File.Exists(_fileName)) NoteTextEditor.Text = File.ReadAllText(_fileName);
        }
        private void SaveNote(object sender, EventArgs e) => File.WriteAllText(_fileName, NoteTextEditor.Text);
        private void DeleteNote(object sender, EventArgs e)
        {
            if (File.Exists(_fileName)) File.Delete(_fileName);
            NoteTextEditor.Text = string.Empty;
        }
    }
}
