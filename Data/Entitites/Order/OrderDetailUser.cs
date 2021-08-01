using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Data.Entitites
{
    public class OrderDetailUser
    {
        [BindNever]
        public int Id { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public int PhoneNumber { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
