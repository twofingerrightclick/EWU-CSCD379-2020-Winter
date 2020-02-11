using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Dto
{
    public class Group: GroupInput, IEntity
    {
        public int Id { get; set; }
        public IList<UserGroup> UserGroups { get; } = new List<UserGroup>();
    }
}
