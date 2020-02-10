using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Business.Dto
{
    public class PostInput
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public int? AuthorId { get; set; }
    }
}
