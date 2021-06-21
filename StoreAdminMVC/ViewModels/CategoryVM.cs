using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAdminMVC.ViewModels
{
    public class CategoryVM
    {
        public long Id { get; set; }
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
