using System.ComponentModel.DataAnnotations;

namespace Chef.DMS.Models;

public class FileMetaData : Model
{
    [Required]
    public int FileId { get; set; }

    [Required]
    public int CompanyId { get; set; }

    [Required]
    public int BranchId { get; set; }

    [Required]
    public string Module { get; set; }

    [Required]
    public string Feature { get; set; }

    public string RefNumber { get; set; }
}
