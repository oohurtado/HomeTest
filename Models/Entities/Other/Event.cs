using System.Security.Cryptography;
using System;
using Home.Source.Common;

namespace Home.Models.Entities.Other
{
    public class Event
    {
        /// <summary>
        /// Fields
        /// </summary>

        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int? ReminderTime { get; set; }
        public bool RunningTime { get; set; }
        public bool RemainingTime { get; set; }

        /// <summary>
        /// Relationships
        /// </summary>

        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
    }
}
