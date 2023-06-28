using Chef.Common.Services;
using Chef.Trading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Services;

public interface IProspectService : IAsyncService<Prospect>
{
    //Task<int> UpdateProspectStatus(int prospectId, bool isAssigned);
    //Task<IEnumerable<ProspectDto>> GetAllAsync();
    new Task<int> InsertAsync(Prospect prospect);
    //Task<IEnumerable<CustomerDto>> GetAllCustomer();
    //Task<IEnumerable<TaxJurisdictionDto>> GetAllTaxJurisdiction();
    new Task<IEnumerable<ProspectDto>> GetAsync(int id);

    Task<IEnumerable<ProspectDto>> GetAll();

    Task<int> GetExistingProspectAsync(Prospect obj);

    Task<int> GetEditExistingProspectAsync(Prospect prospect);

    Task<int> IsCodeExist(string code);
    Task<int> IsTaxNoExist(int taxNo);

}
