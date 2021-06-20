using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entitites
{
    public class Category : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
    }
}
