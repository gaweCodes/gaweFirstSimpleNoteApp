using System;

namespace gaweFirstSimpleNoteApp.Models
{
    public class Note
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
