using System;

namespace gaweFirstSimpleNoteApp.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
        public Guid UserId { get; set; }
    }
}
