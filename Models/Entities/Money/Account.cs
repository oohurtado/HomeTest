using Home.Models.Entities.Other;

namespace Home.Models.Entities.Money
{
    public class Account
    {
        public Account()
        {
            Entries = new HashSet<Entry>();
            AccountActivities = new HashSet<AccountActivity>();
        }
        /// <summary>
        /// Fields
        /// </summary>

        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;

        /// <summary>
        /// Relationships
        /// </summary>

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<Entry> Entries { get; set; }
        public ICollection<AccountActivity> AccountActivities { get; set; }
    }
}
