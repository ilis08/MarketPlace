using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels;

public class LoginRequest
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
