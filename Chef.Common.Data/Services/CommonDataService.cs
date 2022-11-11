namespace Chef.Common.Data.Services;

public class CommonDataService : ICommonDataService
{
    private readonly ICommonDataRepository commonDataRepository;

    public CommonDataService(ICommonDataRepository commonDataRepository)
    {
        this.commonDataRepository = commonDataRepository;
    }
}

