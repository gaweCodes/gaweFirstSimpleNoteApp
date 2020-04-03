using System;
using System.ComponentModel.DataAnnotations;
using GaweNotesApi.Validations;

namespace GaweNotesApi.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [DateTimeOffsetNotInFutureValidation(ErrorMessage = "The change date must be older then the current date")]
        public DateTimeOffset LastModifiedAt { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
    }
}
