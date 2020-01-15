using System.Collections.Generic;

namespace BlogEngine.Data
{
    public class Author : FingerPrintEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Post> Posts { get; set; }
    }
}