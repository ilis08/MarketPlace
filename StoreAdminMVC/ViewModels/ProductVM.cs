using ApplicationService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAdminMVC.ViewModels
{
    public class ProductVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, enter the product name.")]
        [StringLength(40)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please, enter the description.")]
        [StringLength(100)]
        public string Description { get; set; }
        [Display(Name = "Date of release")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please, enter the release date")]
        public DateTime? Release { get; set; }
        [Required(ErrorMessage = "Please, enter the price")]
        [Range(1, 999999.0)]
        public double Price { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please, choose a category")]
        public int CategoryId { get; set; }
        public CategoryVM Category { get; set; }

        public SelectList CategoryList { get; set; }

        public ProductVM()
        {

        }

        public ProductVM(ProductDTO productDto)
        {
            Id = productDto.Id;
            ProductName = productDto.ProductName;
            Description = productDto.Description;
            Release = productDto.Release;
            Price = productDto.Price;
            Image = productDto.Image;
            CategoryId = productDto.Category.Id;

            Category = new CategoryVM
            {
                Id = productDto.Category.Id,
                Title = productDto.Category.Title,
                Description = productDto.Category.Description
            };
        }
    }
}
