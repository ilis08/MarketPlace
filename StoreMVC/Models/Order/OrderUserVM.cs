using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class OrderUserVM
    {
        [BindNever]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
