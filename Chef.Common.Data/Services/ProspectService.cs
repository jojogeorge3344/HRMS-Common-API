namespace Chef.Common.Data.Services;

public class ProspectService : AsyncService<Prospect>, IProspectService
{
    private readonly IProspectRepository prospectRepository;

    public ProspectService(IProspectRepository prospectRepository)
    {
        this.prospectRepository = prospectRepository;
    }

    public async Task<int> UpdateStatus(int prospectId, bool isAssigned)
    {
        return await prospectRepository.UpdateStatus(prospectId, isAssigned);
    }

    public async Task<bool> IsExistingProspectAsync(Prospect prospect)
         => await prospectRepository.IsExistingProspectAsync(prospect);

    public async Task<bool> IsCodeExist( string code)
    {
        return await prospectRepository.IsCodeExist(code);
    }

    public async Task<bool> IsTaxNoExist(long taxNo)
    {
        return await prospectRepository.IsTaxNoExist(taxNo);
    }
}

