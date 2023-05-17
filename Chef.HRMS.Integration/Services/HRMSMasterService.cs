using Chef.Common.Models;
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
    public async Task<IEnumerable<PayGroup>> GetPaygroup(int Id)
    {
        return await hRMSMasterRepository.GetPaygroup(Id);
    }

    public async Task<IEnumerable<HRMSPayGroupPayRollComoponentDetails>> GetPayRollComponent()
    {
        return await hRMSMasterRepository.GetPayRollComponent();
    }
}
