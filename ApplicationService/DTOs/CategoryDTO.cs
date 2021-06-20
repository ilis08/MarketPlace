using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationService.DTOs
{
    public class CategoryDTO
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
