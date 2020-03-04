using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Business.Dto
{
    public class AuthorInput
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
