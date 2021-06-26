using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAdminMVC.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public CategoryVM()
        {

        }

        public CategoryVM(CategoryDTO categoryDto)
        {
            Id = categoryDto.Id;
            Title = categoryDto.Title;
            Description = categoryDto.Description;
        }
    }
}
