using System;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core.Logging;

public interface IAuditable
{
}

public class AuditLog
{
    [Key]
    public Int64 Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    public string TableName { get; set; }
    public string SchemaName { get; set; }
    public int TablePK { get; set; }
    public string Action { get; set; }
    public string Values { get; set; }
}

public static class AuditAction
{
    public const string Insert = "I";
    public const string Update = "U";
    public const string Delete = "D";
}
