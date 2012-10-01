using System.Collections.Generic;

namespace bbv.Models
{
    public class User
    {
        public User()
        {
            this.Nicks = new List<string>();
            this.Hobbies = new List<string>();
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public List<string> Nicks { get; private set; }

        public List<string> Hobbies { get; private set; } 
    }
}