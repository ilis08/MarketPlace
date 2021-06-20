using Data.Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationService.DTOs
{
    public class PhoneDTO
    {
        public long Id { get; set; }
        [Required]
        public string PhoneName { get; set; }
        public string Description { get; set; }
        public DateTime? Release { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }


        public long CategoryId { get; set; }
        public virtual CategoryDTO Category { get; set; }
    }
}
