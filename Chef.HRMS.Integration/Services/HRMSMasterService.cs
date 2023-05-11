using Chef.HRMS.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.HRMS.Integration;

public class HRMSMasterService : IHRMSMasterService
{
    private readonly IHRMSMasterRepository hRMSMasterRepository;

    public HRMSMasterService(IHRMSMasterRepository hRMSMasterRepository)
    {
        this.hRMSMasterRepository = hRMSMasterRepository;
    }
    public async Task<IEnumerable<PayGroup>> GetPaygroup()
    {
        return await hRMSMasterRepository.GetPaygroup();
    }

    public async Task<IEnumerable<PayRollComponentViewModel>> GetPayRollComponent()
    {
        return await hRMSMasterRepository.GetPayRollComponent();
    }
}
