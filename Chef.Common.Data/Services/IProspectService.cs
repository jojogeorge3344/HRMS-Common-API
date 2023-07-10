namespace Chef.Common.Data.Services;

public interface IProspectService : IAsyncService<Prospect>
{
    Task<bool> IsExistingProspectAsync(Prospect prospect);
    Task<bool> IsCodeExist(string code);
    Task<bool> IsTaxNoExist(long taxNo);
    Task<int> UpdateStatus(int prospectId, bool isAssigned);
}
