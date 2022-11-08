using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Authentication.Models;

public class RegisterModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Email (Username) is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
