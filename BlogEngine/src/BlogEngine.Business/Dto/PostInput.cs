using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Business.Dto
{
    public class PostInput
    {
        [Required]
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Slug { get; set; }
        public bool? IsPublished { get; set; }
        public List<int>? TagIds { get; }
        [Required]
        public DateTime? PostedOn { get; set; }
        [Required]
        public int? AuthorId { get; set; }
    }
}
