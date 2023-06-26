namespace Chef.Common.Data.Services;

public class ProspectService : AsyncService<Prospect>, IProspectService
{
    private readonly IProspectRepository prospectRepository;

    public ProspectService(IProspectRepository prospectRepository)
    {
        this.prospectRepository = prospectRepository;
    }

    //public async Task<int> DeleteAsync(int id)
    //{
    //    return await prospectRepository.DeleteProspect(id);
    //}

    //public async Task<IEnumerable<ProspectDto>> GetAllAsync()
    //{
    //    return await prospectRepository.GetAll();
    //}

    public async new Task<IEnumerable<ProspectDto>> GetAsync(int id)
    {
        return await prospectRepository.GetAsync(id);
    }



    public async new Task<int> InsertAsync(Prospect prospect)
    {
        return await prospectRepository.InsertAsync(prospect);

    }

    public async new Task<int> UpdateAsync(Prospect prospect)
    {
        return await prospectRepository.UpdateAsync(prospect);
    }

    //public async Task<int> UpdateProspectStatus(int prospectId, bool isAssigned)
    //{
    //    return await prospectRepository.UpdateProspectStatus(prospectId, isAssigned);
    //}

    //public async Task<IEnumerable<CustomerDto>> GetAllCustomer()
    //{
    //    var result = await this.prospectRepository.GetAllCustomer();
    //    return result;
    //}


    //public async Task<IEnumerable<TaxJurisdictionDto>> GetAllTaxJurisdiction()
    //{
    //        var result = await this.prospectRepository.GetAllTaxJurisdiction();
    //        return result;
    //}

    public async Task<IEnumerable<ProspectDto>> GetAll()
    {
        return await prospectRepository.GetAll();
    }

    public async Task<int> GetExistingProspectAsync(Prospect obj)
         => await prospectRepository.GetExistingProspectAsync(obj);

    public async Task<int> GetEditExistingProspectAsync(Prospect prospect)
         => await prospectRepository.GetEditExistingProspectAsync(prospect);

    public async Task<int> CheckExistingProspectCode( string code)
    {
        return await prospectRepository.CheckExistingProspectCode(code);
    }
    public async Task<int> CheckExistingTaxNo(int taxNo)
    {
        return await prospectRepository.CheckExistingTaxNo(taxNo);
    }
}

