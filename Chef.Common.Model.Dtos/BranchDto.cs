namespace Chef.Common.Model.Dtos;

public class BranchDto
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? CompanyId { get; set; }
    public string? CompanyCode { get; set; }
}

public class UserBranchDto
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public bool IsDefault { get; set; }
}

