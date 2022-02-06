using Data.CustomAttributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationService.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime? Release { get; set; }
        [Required]
        public double Price { get; set; }
        public string Image { get; set; }

        [NotMapped]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }

        public virtual CategoryDTO Category { get; set; }
    }
}
