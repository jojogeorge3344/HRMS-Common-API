namespace Chef.Common.Authentication.Models;

public class UserModule : Model
{
    public string Username { get; set; }
    public ModuleType Module { get; set; }
}

