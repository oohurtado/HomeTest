using Home.Models.Entities.Other;

namespace Home.Models.Entities.Money
{
    public class SuperCategory
    {
        public SuperCategory()
        {
            Categories = new HashSet<Category>();
        }

        /// <summary>
        /// Fields
        /// </summary>

        public string Name { get; set; } = string.Empty;
        public bool IsRegular { get; set; }
        public bool IsService { get; set; }


        /// <summary>
        /// Relationships
        /// </summary>

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<Category> Categories { get; set; }
    }
}
