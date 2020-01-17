using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BlogEngine.Data
{
    public class Post : FingerPrintEntityBase
    {
        public DateTime PostedOn { get; set; }
        public bool IsPublished { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        
        public List<Comment> Comments { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public string Slug { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
