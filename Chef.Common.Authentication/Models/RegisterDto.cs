using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Authentication.Models;

public class RegisterDto
{
    [EmailAddress]
    [Required(ErrorMessage = "Email (Username) is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
