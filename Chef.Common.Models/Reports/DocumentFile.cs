using System;

namespace Chef.Common.Models;

[Serializable]
public class DocumentFile
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public string Type { get; set; }

    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public string Module { get; set; }
    public string Feature { get; set; }
    public string RefNumber { get; set; }

    public byte[] Content { get; set; }
}
