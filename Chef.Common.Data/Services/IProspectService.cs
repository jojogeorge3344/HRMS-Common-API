namespace Chef.Common.Data.Services;

public interface IProspectService : IAsyncService<Prospect>
{
    new Task<IEnumerable<ProspectDto>> GetAllAsync();
    new Task<ProspectDto> GetAsync(int id);
    Task<bool> IsExistingProspectAsync(Prospect prospect);
    Task<bool> IsCodeExist(string code);
    Task<bool> IsTaxNoExist(long taxNo, int prospectId);
    Task<int> UpdateStatus(int prospectId, bool isAssigned);
}
