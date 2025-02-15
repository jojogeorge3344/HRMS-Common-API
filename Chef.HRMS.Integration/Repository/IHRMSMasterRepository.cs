﻿using Chef.Common.Core.Repositories;
using Chef.Common.Models;
using Chef.HRMS.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.HRMS.Integration;

public interface IHRMSMasterRepository:IRepository
{
    Task<IEnumerable<PayGroup>> GetPaygroup(int Id);

    Task<IEnumerable<HRMSPayGroupPayRollComoponentDetails>> GetPayRollComponent();
}
