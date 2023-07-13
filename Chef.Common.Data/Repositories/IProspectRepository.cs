using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Repositories;

public interface IProspectRepository : IGenericRepository<Prospect>
{
    //Task<int> UpdateProspectStatus(int prospectId, bool isAssigned);
    //Task<int> DeleteProspect(int prospectId);
    //Task<IEnumerable<CustomerDto>> GetAllCustomer();
    ////Task<IEnumerable<TaxJurisdictionDto>> GetAllTaxJurisdiction();
    //Task<IEnumerable<ProspectDto>> GetAll();
    new Task<IEnumerable<ProspectDto>> GetAsync(int id);

    Task<IEnumerable<ProspectDto>> GetAll();
    //Task<IEnumerable<Prospect>> GetAllProspect();

    Task<int> GetExistingProspectAsync(Prospect obj);
    Task<int> GetEditExistingProspectAsync(Prospect prospect);
    Task<bool> IsCodeExist(string code);
    Task<bool> IsTaxNoExist(long taxNo);

}

