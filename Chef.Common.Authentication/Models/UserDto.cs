﻿namespace Chef.Common.Authentication.Models;

public class UserDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TimeZone { get; set; }
    public string Email { get; set; }
    public int BranchId { get; set; }
    public bool IsActive { get; set; }
}