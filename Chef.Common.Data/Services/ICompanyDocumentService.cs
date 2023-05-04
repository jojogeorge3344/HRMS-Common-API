﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Services;

public interface ICompanyDocumentService:IAsyncService<ComapnyDocuments>
{
    Task<int> Insert(ComapnyDocuments comapnyDocuments);
}
