namespace Home.Models.Entities.Other
{
    public class Activity
    {
        /// <summary>
        /// Fields
        /// </summary>

        public string Tag { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan? Time { get; set; }
        public bool IsDone { get; set; }

        /// <summary>
        /// Relationships
        /// </summary>

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
