using System;
using SQLite;

namespace gaweFirstSimpleNoteApp.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Title { get; set; }
        [NotNull]
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
