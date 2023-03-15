using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entitites
{
    public class Review : BaseEntity
    {
        [Required]
        public RatingEnum Rating { get; set; }
        [MinLength(1)]
        [MaxLength(4000)]
        public string? Comment { get; set; }
        [Required]
        public long ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public long UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

    public enum RatingEnum
    {
        OneStar = 1,
        TwoStars = 2,
        ThreeStars = 3,
        FourStars = 4,
        FiveStars = 5
    }
}
