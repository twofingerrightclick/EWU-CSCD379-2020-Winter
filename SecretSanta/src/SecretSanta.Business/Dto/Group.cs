using System.Collections.Generic;

namespace SecretSanta.Business.Dto
{
    public class Group : GroupInput, IEntity
    {
        public int Id { get; set; }
        public List<User> Users { get; } = new List<User>();
    }
}
