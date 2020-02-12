using System.Collections.Generic;

namespace SecretSanta.Business.Dto
{
    public class User : UserInput, IEntity
    {
        public int Id { get; set; }

        public List<Gift> Gifts { get; } = new List<Gift>();

        public List<Group> Groups { get; } = new List<Group>();
    }
}
