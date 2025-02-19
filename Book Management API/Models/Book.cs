using Book_Management_API.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Book_Management_API.Models
{
    public class Book
    {
        [NotSetId]
        public int Id { get; set; }
        [Required]
        [StringLength(450)]
        public string Title { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Publication year must be a positive number.")]
        [NotFutureYear]
        public int PublicationYear { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Author name must be between 2 and 100 characters.")]
        public string AuthorName { get; set; } = string.Empty;

        public int ViewsCount { get; set; } = 0;

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }
}

