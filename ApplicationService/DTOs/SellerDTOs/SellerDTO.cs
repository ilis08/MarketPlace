using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs.SellerDTOs;

public class SellerDTO
{
    [Required]
    public long Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [StringLength(500)]
    public string Description { get; set; }
    [Required]
    [Url]
    public string LogoUrl { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    [Url]
    public string Url { get; set; }
    [Required]
    public bool IsActive { get; set; } = true;
    [Required]
    public long UserId { get; set; }
}
