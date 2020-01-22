using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    public class UserGroup
    {
    
        public int UserId { get; set; }

        public int GroupId { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public User User { get; set; }

        public Group Group { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.






    }
}
