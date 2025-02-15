﻿namespace Chef.Common.Data.Repositories;

public interface IProspectRepository : IGenericRepository<Prospect>
{
    new Task<ProspectDto> GetAsync(int id);
    new Task<IEnumerable<ProspectDto>> GetAllAsync();
    Task<bool> IsExistingProspectAsync(Prospect prospect);
    Task<bool> IsCodeExist(string code);
    Task<bool> IsTaxNoExist(long taxNo, int prospectId);
    Task<int> UpdateStatus(int prospectId, bool isAssigned);
    Task<bool> IsProspectUsed(int prospectId);
}

