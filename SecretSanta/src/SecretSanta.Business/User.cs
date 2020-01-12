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
        [AllowNull]
        public List<Gift> Gifts { get; set; }

        public User(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
<<<<<<< refs/remotes/IntelliTect-Samples/master
<<<<<<< refs/remotes/IntelliTect-Samples/master
<<<<<<< refs/remotes/IntelliTect-Samples/master
            Gifts = gifts ?? new List<Gift>();
=======
            Gifts = gifts ?? throw new ArgumentNullException(nameof(gifts));
>>>>>>> added props and constructors. need to handle prop nulls
=======
            Gifts = gifts ?? new List<Gift>();
>>>>>>> added a test for gift properties using reflection
=======
            Gifts = new List<Gift>();
>>>>>>> added null tests for Gift and User Constructors
        }
    }
}