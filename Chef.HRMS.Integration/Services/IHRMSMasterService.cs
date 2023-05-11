using Chef.Common.Core.Services;
using Chef.HRMS.Integration.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.HRMS.Integration;

public interface IHRMSMasterService:IBaseService
{
    Task<IEnumerable<PayGroup>> GetPaygroup();
    Task<IEnumerable<PayRollComponentViewModel>> GetPayRollComponent();
}
