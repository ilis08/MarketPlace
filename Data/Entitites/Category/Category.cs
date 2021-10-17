using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entitites
{
    public class Category : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Title { get; set; }
        [MinLength(5)]
        [MaxLength(100)]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
