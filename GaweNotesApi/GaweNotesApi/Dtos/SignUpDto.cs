using System.ComponentModel.DataAnnotations;

namespace GaweNotesApi.Dtos
{
    public class SignUpDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Firstname {get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
