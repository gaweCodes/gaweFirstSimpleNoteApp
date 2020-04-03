using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GaweNotesApi.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public IEnumerable<Note> Notes { get; set; } = new List<Note>();
    }
}
