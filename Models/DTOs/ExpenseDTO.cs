using System.ComponentModel.DataAnnotations;

namespace Home.Models.DTOs
{    
    public class SuperCategoryEditorDTO
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Is  regular")]
        [Required(ErrorMessage = "This field is mandatory")]
        public bool? IsRegular { get; set; }

        [Display(Name = "Is service")]
        [Required(ErrorMessage = "This field is mandatory")]
        public bool? IsService { get; set; }
    }

    public class CategoryEditorDTO
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Super category")]
        [Required(ErrorMessage = "This field is mandatory")]
        public int? SuperCategoryId { get; set; }

        [Display(Name = "Budget")]
        [Required(ErrorMessage = "This field is mandatory")]
        public decimal? Budget { get; set; }
    }

    public class AccountEditorDTO
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "This field is mandatory")]
        public decimal Amount { get; set; }

        [Display(Name = "Description")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string? Description { get; set; } = string.Empty;

        [Display(Name = "Owner")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string Owner { get; set; } = string.Empty;
    }

    public class AccountMoneyEditorDTO
    {
        [Display(Name = "Account")]
        [Required(ErrorMessage = "This field is mandatory")]
        public int AccountId { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "This field is mandatory")]
        public decimal Amount { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "This field is mandatory")]
        public DateTime Date { get; set; }
    }

    public class EntryEditorDTO
    {
        [Display(Name = "Amount")]
        [Required(ErrorMessage = "This field is mandatory")]
        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Amount { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "This field is mandatory")]
        public DateTime Date { get; set; }

        [Display(Name = "Description")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string? Description { get; set; } = string.Empty;

        [Display(Name = "Category")]
        [Required(ErrorMessage = "This field is mandatory")]
        public int CategoryId { get; set; }

        [Display(Name = "Account")]
        [Required(ErrorMessage = "This field is mandatory")]
        public int AccountId { get; set; }
    }

    public class Expenses
    {
        public int? SuperCategoryId { get; set; }
        public string SuperCategoryName { get; set; } = string.Empty;
        public bool? SuperCategoryIsRegular { get; set; }
        public bool? SuperCategoryIsService { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal? CategoryBudget { get; set; }

        public int? EntryId { get; set; }
        public decimal? EntryAmount { get; set; }
        public DateTime? EntryDate { get; set; }
        public string EntryDescription { get; set; } = string.Empty;
        public int EntryCategoryId { get; set; }
        public int EntryAccountId { get; set; }
    }

    public class SuperCategoryDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? IsRegular { get; set; }
        public bool? IsService { get; set; }

        public List<CategoryDTO>? Categories { get; set; }
        public decimal? Total { get; set; }
        public decimal? Budget { get; set; }
    }

    public class CategoryDTO
    {
        public int? Id { get; set; }
        public int? SuperCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Budget { get; set; }

        public List<EntryDTO>? Entries { get; set; }
        public decimal? Total { get; set; }
    }

    public class EntryDTO
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public int? AccountId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class AccountDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}
