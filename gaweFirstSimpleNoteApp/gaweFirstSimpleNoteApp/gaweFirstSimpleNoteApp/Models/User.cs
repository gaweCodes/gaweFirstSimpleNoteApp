using System;

namespace gaweFirstSimpleNoteApp.Models
{
    public class User
    {
        public string Fullname { get; set; }
        public Guid Id { get; set; }
        public string JwtToken { get; set; }
    }
}
