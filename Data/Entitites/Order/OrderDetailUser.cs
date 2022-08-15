using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Data.Entitites
{
    public class OrderDetailUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? Name { get; set; }

        [Phone]
        [Required]
        public string? PhoneNumber { get; set; }

        [BindNever]
        public IEnumerable<Order>? Order { get; set; }
    }
}
