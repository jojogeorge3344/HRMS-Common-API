namespace Chef.Common.Authentication.Models;

public class Permission : Model
{
    public string Controller { get; set; }
    public string Action { get; set; }
    public int GroupId { get; set; }
}
