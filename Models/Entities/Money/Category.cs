using Home.Models.Entities.Other;

namespace Home.Models.Entities.Money
{
    public class Category
    {
        public Category()
        {
            Entries = new HashSet<Entry>();
        }

        /// <summary>
        /// Fields
        /// </summary>

        public string Name { get; set; } = string.Empty;
        public decimal Budget { get; set; }

        /// <summary>
        /// Relationships
        /// </summary>

        public int Id { get; set; }
        public int? SuperCategoryId { get; set; }
        public SuperCategory? SuperCategory { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}
