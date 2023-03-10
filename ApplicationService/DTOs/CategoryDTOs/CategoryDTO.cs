using System.ComponentModel.DataAnnotations;

namespace ApplicationService.DTOs
{
    public class CategoryDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Category Title is a required field.")]
        [MinLength(2, ErrorMessage = "Minimum length for the Title is 2 character.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Title is 20 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Category Description is a required field.")]
        [MinLength(5, ErrorMessage = "Minimum length for the Title is 5 character.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Title is 100 characters")]
        public string Description { get; set; }
    }
}
