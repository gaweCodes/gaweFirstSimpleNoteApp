using System;

namespace GaweNotesApi.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string JwtToken { get; set; }
    }
}
