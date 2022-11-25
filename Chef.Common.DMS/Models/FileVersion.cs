using SqlKata;

namespace Chef.DMS.Models;

public class FileVersion : Model
{
    [Required]
    public int FileId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Path { get; set; }

    public string Type { get; set; }

    public float Size { get; set; }

    public int Version { get; set; }

    [Skip(true)]
    [Write(false)]
    [Ignore]
    public byte[] Content { get; set; }
}
