namespace Chef.DMS.Models;

//TODO - Refactor for DB Storage.
public class StorageOptions
{
    public const string FileStorage = "FileStorage";

    public string Type { get; set; }
    public string BasePath { get; set; }
}
