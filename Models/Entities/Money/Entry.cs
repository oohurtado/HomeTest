using Home.Models.Entities.Other;

namespace Home.Models.Entities.Money
{
    public class Entry
    {
        /// <summary>
        /// Fields
        /// </summary>

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Relationships
        /// </summary>

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
