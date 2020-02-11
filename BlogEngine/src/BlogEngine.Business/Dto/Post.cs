namespace BlogEngine.Business.Dto
{
    public class Post : PostInput
    {
        public int Id { get; set; }
        public Author Author { get; set; }

        private Post(int id, Author author)
        {
            Id = id;
            Author = author;
        }
    }
}
