using System.Collections.Generic;

namespace Entity
{
    public class User : BaseEntity
    {
        public User()
        {
            Jobs = new List<Job>();
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual List<Job> Jobs { get; set; }
    }
}
