using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SecretSanta.Business
{
    public class User
    {
    
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public List<Gift> Gifts { get; }

        public User(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Gifts = new List<Gift>();

        }
    }
}