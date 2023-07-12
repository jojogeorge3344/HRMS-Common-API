using Chef.Common.Core;
using Chef.Common.Core.Extensions;
using Chef.Common.Core.Repositories;
using Chef.Common.Models;
using Chef.Common.Repositories;
using Chef.HRMS.Integration.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using SqlKata;
using SqlKata.Execution;

namespace Chef.HRMS.Integration;

public class HRMSMasterRepository : TenantRepository<Model>, IHRMSMasterRepository
{
    public HRMSMasterRepository(
       IHttpContextAccessor httpContextAccessor,
       ITenantConnectionFactory tenantConnectionFactory)
       : base(httpContextAccessor, tenantConnectionFactory)
    {
    }
    public async Task<IEnumerable<PayGroup>> GetPaygroup(int Id)
    {
        return await QueryFactory
             .Query<PayGroup>()
             .Select("id", "name", "code")
             .WhereNotIn("id", new Query("common.HRMSPaygroupConfiguration")
                         .Join("hrms.paygroup", "hrms.paygroup.id", "common.HRMSPaygroupConfiguration.paygroupid")
                         .Where("hrms.paygroup.id","!=", Id)
                         .Where("common.HRMSPaygroupConfiguration.isarchived", false)
                         .Select("common.HRMSPaygroupConfiguration.paygroupid"))
             .WhereNotArchived()
             .GetAsync<PayGroup>();
    }

    public async Task<IEnumerable<HRMSPayGroupPayRollComoponentDetails>> GetPayRollComponent()
    {
        return await QueryFactory
            .Query<PayrollComponent>()
            .Select("payrollcomponent.id as ComponentId", "payrollcomponent.name as ComponentName", "payrollcomponent.shortcode as ComponentCode", "benefittypes.id as ComponentTypeId", "benefittypes.name as ComponentTypeName")
            .Join<BenefitTypes>("payrollcomponent.payrollcomponenttype", "benefittypes.id", "=")
            .WhereNot("payrollcomponent.isarchived", true)
            .WhereNot("benefittypes.isarchived", true)
            .GetAsync<HRMSPayGroupPayRollComoponentDetails>();
    }
}
