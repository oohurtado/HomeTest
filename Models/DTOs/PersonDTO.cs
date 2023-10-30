using System.ComponentModel.DataAnnotations;

namespace Home.Models.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class PersonEditorDTO
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string? FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string? LastName { get; set; }
    }
}
