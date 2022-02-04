using ApplicationService.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.ViewModels
{
    public class ProductVM
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Date of release")]
        [DataType(DataType.Date)]
        public DateTime? Release { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public CategoryVM Category { get; set; }

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
