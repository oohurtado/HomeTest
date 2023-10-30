using Home.Models.Entities.Money;
using Microsoft.AspNetCore.Identity;

namespace Home.Models.Entities.Other
{
    public class User : IdentityUser
    {
        public User()
        {
            People = new HashSet<Person>();
            Activities = new HashSet<Activity>();
            Accounts = new HashSet<Account>();            
            Entries = new HashSet<Entry>();
            SuperCategories = new HashSet<SuperCategory>();
        }

        /// <summary>
        /// Fields
        /// </summary>

        // ...

        /// <summary>
        /// Relationships
        /// </summary>

        public ICollection<Person> People { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public ICollection<SuperCategory> SuperCategories { get; set; }
    }
}
