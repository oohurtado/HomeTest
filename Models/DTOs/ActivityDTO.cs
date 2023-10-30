using System.ComponentModel.DataAnnotations;

namespace Home.Models.DTOs
{
    public class ActivityDTO
    {
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan? Time { get; set; }
        public bool IsDone { get; set; }
    }

    public class ActivityEditorDTO
    {
        [Display(Name = "Tag")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string Tag { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Date")]
        [Required(ErrorMessage = "This field is mandatory")]
        public DateTime Date { get; set; }

        [Display(Name = "Time")]
        public TimeSpan? Time { get; set; }

        [Display(Name = "Is done")]
        public bool IsDone { get; set; }
    }

    public class ActivityStatusEditorDTO
    {
        [Display(Name = "Is done")]
        public bool IsDone { get; set; }
    }
}
