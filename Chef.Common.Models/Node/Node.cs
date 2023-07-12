using Chef.Common.Core;

namespace Chef.Common.Models;

public class Node : Model
{
    public string Code { get; set; }

    public string Name { get; set; }

    public int Level { get; set; }

    public int ParentId { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public string ModuleName { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public int TotalDocumentCount { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public string SubModuleName { get; set; }
}
