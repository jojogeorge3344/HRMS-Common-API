using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Authentication.Models;

public class LoginDto
{
    [Required(ErrorMessage = "Email (Username) is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}

