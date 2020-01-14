namespace BlogEngine.Data
{
    public class Comment
    {
        public Post Post { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}