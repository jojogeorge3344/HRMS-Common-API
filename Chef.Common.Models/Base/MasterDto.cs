using System;

namespace Chef.Common.Models;

public class MasterDataDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class BranchDto : MasterDataDto
{
}

public class CountryDto : MasterDataDto
{
}