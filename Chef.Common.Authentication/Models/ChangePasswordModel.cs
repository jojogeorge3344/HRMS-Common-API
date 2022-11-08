using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Authentication.Models;

public class ChangePasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string CurrentPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
}

