namespace Chef.Common.Data.Repositories;

public interface IProspectRepository : IGenericRepository<Prospect>
{
    new Task<IEnumerable<ProspectDto>> GetAsync(int id);
    Task<IEnumerable<ProspectDto>> GetAll();
    Task<bool> IsExistingProspectAsync(Prospect prospect);
    Task<bool> IsCodeExist(string code);
    Task<bool> IsTaxNoExist(long taxNo);
    Task<int> UpdateStatus(int prospectId, bool isAssigned);
}

