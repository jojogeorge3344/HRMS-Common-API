﻿using Chef.Common.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Repositories;

public interface ICompanyDocumentRepostory: IGenericRepository<CompanyDocuments>
{
    Task<IEnumerable<CompanyDocuments>> GetCompanyDocuments(int id);
}
