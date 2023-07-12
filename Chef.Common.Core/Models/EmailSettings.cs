namespace Chef.Common.Core;

public class EmailSettings : Model
{
    public string MailServer { get; set; } = "smtp.office365.com";
    public int MailPort { get; set; } = 25;
    public string SenderName { get; set; } = "athira";
    public string SenderEmail { get; set; } = "athirak@thomsuninfocare.com";
    public string Password { get; set; } = "Aswina@1";
}
