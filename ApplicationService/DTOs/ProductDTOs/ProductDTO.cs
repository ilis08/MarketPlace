using ApplicationService.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationService.DTOs.ProductDTOs;

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
    [AllowedFileFormatsForImage(new string[] { ".jpg", ".png", ".jpeg" })]
    public IFormFile ImageFile { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
}
