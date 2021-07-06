using Data.Entitites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationService.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateTime? Release { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; } 
        public int CategoryId { get; set; }

        public virtual CategoryDTO Category { get; set; }
    }
}
