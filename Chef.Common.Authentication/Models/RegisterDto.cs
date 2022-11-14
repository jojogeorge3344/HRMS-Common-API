using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Authentication.Models;

public class RegisterDto
{
    [EmailAddress]
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
