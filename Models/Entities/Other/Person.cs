namespace Home.Models.Entities.Other
{
    public class Person
    {
        public Person()
        {
            Events = new HashSet<Event>();
        }

        /// <summary>
        /// Fields
        /// </summary>

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        /// <summary>
        /// Relationships
        /// </summary>

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<Event> Events { get; set; }
    }
}
