using System.ComponentModel.DataAnnotations;

namespace BlazorIS.Pages.Create;

public class InputModel
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? Name { get; set; }
    public string? Email { get; set; }

    public string? ReturnUrl { get; set; }

    public string? Button { get; set; }

    public string? RoleName { get; set; }
}
