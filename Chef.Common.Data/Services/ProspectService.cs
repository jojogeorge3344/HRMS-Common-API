using Chef.Common.Exceptions;

namespace Chef.Common.Data.Services;

public class ProspectService : AsyncService<Prospect>, IProspectService
{
    private readonly IProspectRepository prospectRepository;
    private readonly IMasterDataRepository masterDataRepository;

    public ProspectService(IProspectRepository prospectRepository, IMasterDataRepository masterDataRepository)
    {
        this.prospectRepository = prospectRepository;
        this.masterDataRepository = masterDataRepository;
    }

    public async new Task<IEnumerable<ProspectDto>> GetAllAsync()
    {
        IEnumerable<TaxJurisdiction> taxJurisdictions = await masterDataRepository.GetAllTaxJurisdiction();
        IEnumerable<ProspectDto> prospects = await prospectRepository.GetAllAsync();

        var data = from prospect in prospects
                   join taxJurisdiction in taxJurisdictions
                   on prospect.TaxJurisdictionId equals taxJurisdiction.Id into newdata
                   from newd in newdata.DefaultIfEmpty()
                   select new { prospect, newd };

        data.ToList().ForEach(x => x.prospect.taxJurisdictionCode = x.newd?.Code);

        return data.Select(x => x.prospect).ToList();
    }

    public async new Task<ProspectDto> GetAsync(int id)
    {
        return await prospectRepository.GetAsync(id);
    }

    public async Task<int> UpdateStatus(int prospectId, bool isAssigned)
    {
        return await prospectRepository.UpdateStatus(prospectId, isAssigned);
    }

    public async Task<bool> IsExistingProspectAsync(Prospect prospect)
         => await prospectRepository.IsExistingProspectAsync(prospect);

    public async Task<bool> IsCodeExist(string code)
    {
        return await prospectRepository.IsCodeExist(code);
    }

    public async Task<bool> IsTaxNoExist(long taxNo, int prospectId)
    {
        return await prospectRepository.IsTaxNoExist(taxNo, prospectId);
    }

    public async new Task<int> DeleteAsync(int id)
    {
        if (await prospectRepository.IsProspectUsed(id))
            throw new ResourceHasDependentException("Prospect already used.");

        return await prospectRepository.DeleteAsync(id);
    }
}

