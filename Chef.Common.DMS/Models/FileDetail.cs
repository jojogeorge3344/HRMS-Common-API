using System.ComponentModel.DataAnnotations;

namespace Chef.Common.DMS.Models;

public class FileDetail : Model
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Path { get; set; }

    public string Type { get; set; }

    //size in KB
    public int Size { get; set; }

    public int CurrentVersion { get; set; }

    [Skip(true)]
    [Write(false)]
    public byte[] Content { get; set; }
}
