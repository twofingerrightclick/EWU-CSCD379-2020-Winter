using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class Gift
    {
   
        public int Id { get; }

        public string Title { get; private set; }
        public string Description { get; private set; }

        public string Url { get; private set; }

        public User User { get; private set; }

        public Gift(int id, string title, string description, string url, User user)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            User = user ?? throw new ArgumentNullException(nameof(user));
        
        }
    }
}
