using System.ComponentModel.DataAnnotations;

namespace BlazorAU.ViewModels;

public class LoginViewModel
{
    [Required]
    [StringLength(50, ErrorMessage = "Too long.")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
