using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Services;

public interface ICompanyDocumentService:IAsyncService<CompanyDocuments>
{
    Task<int> Insert(CompanyDocuments companyDocuments);

    Task<int> Update(CompanyDocuments companyDocuments);

    Task<IEnumerable<CompanyDocuments>> GetCompanyDocumentDetails(int companyId);

    Task<int> Delete(int id);
}
