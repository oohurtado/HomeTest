using Home.Source.Common;
using System.ComponentModel.DataAnnotations;

namespace Home.Models.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int? ReminderTime { get; set; }
        public bool RunningTime { get; set; }
        public bool RemainingTime { get; set; }
    }

    public class EventEditorDTO
    {
        [Display(Name = "Description")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string? Description { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "This field is mandatory")]
        public DateTime Date { get; set; }

        [Display(Name = "Time")]
        public TimeSpan? Time { get; set; }

        [Display(Name = "Reminder time")]
        public int? ReminderTime { get; set; }

        [Display(Name = "Running time")]
        [Required(ErrorMessage = "This field is mandatory")]
        public bool RunningTime { get; set; }

        [Display(Name = "Remaining time")]
        [Required(ErrorMessage = "This field is mandatory")]
        public bool RemainingTime { get; set; }
    }
}
