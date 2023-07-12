using Dapper;
using SqlKata;

namespace Chef.Common.Data.Repositories;

public class ProspectRepository : TenantRepository<Prospect>, IProspectRepository
{
    public ProspectRepository(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session): base(httpContextAccessor, session)
    {

    }

    public async Task<int> UpdateStatus(int prospectId, bool isAssigned)
    {
        return await QueryFactory
                    .Query<Prospect>()
                    .Where("id", prospectId)
                    .UpdateDefaults()
                    .UpdateAsync(new
                    {
                        isassigned = isAssigned
                    });
    }
    public async new Task<ProspectDto> GetAsync(int id)
    {
        return await QueryFactory
                    .Query("common.prospect AS pt")
                    .Select("pt.*", "tj.{taxjurisdictioncode, taxname}", "bp.name")
                    .LeftJoin("common.taxjurisdiction AS tj", "pt.taxjurisdictionid", "tj.id")
                    .LeftJoin("finance.businesspartnerconfiguration AS bp", "pt.businesspartnerid", "bp.id")
                    .WhereFalse("pt.isarchived")
                    .Where("pt.id", id)
                    .FirstOrDefaultAsync<ProspectDto>();
    }

    public async new Task<IEnumerable<ProspectDto>> GetAllAsync()
    {
        return await QueryFactory
                    .Query("common.prospect AS pt")
                    .Select("pt.*", "tj.{taxjurisdictioncode, taxname}", "bp.name")
                    .LeftJoin("common.taxjurisdiction AS tj", "pt.taxjurisdictionid", "tj.id")
                    .LeftJoin("finance.businesspartnerconfiguration AS bp", "pt.businesspartnerid", "bp.id")
                    .WhereFalse("pt.isarchived")
                    .OrderByDesc("pt.createddate")
                    .GetAsync<ProspectDto>();
    }

    public async Task<bool> IsExistingProspectAsync(Prospect prospect)
    {
        Query query = QueryFactory
                     .Query<Prospect>()
                     .WhereNotArchived()
                     .Where(q => q.Where("LOWER(TRIM(prospectcode))", prospect.ProspectCode.ToLower().Trim()).OrWhere("LOWER(TRIM(prospectname))", prospect.ProspectName.ToLower().Trim()));

        if (prospect.Id > 0)
            query.Where("id", "!=", prospect.Id);

        return await query.CountAsync<int>() > 0;
    }

    public async Task<bool> IsCodeExist(string code)
    {
        return await QueryFactory
                    .Query<Prospect>()
                    .Where("prospectcode", code.ToUpper())
                    .WhereNotArchived()
                    .CountAsync<int>() > 0;
    }

    public async Task<bool> IsTaxNoExist(long taxNo, int prospectId)
    {
        return await QueryFactory
                    .Query<Prospect>()
                    .Where("taxno", taxNo)
                    .Where("id", "!=", prospectId)
                    .WhereNotArchived()
                    .CountAsync<int>() > 0;
    }

    public async Task<bool> IsProspectUsed(int prospectId)
    {
        Query pettyCashExpenseDetail = QueryFactory
                                      .Query("finance.pettycashexpensesdetail")
                                      .Select("id")
                                      .WhereNotArchived()
                                      .WhereTrue("isprospect")
                                      .Where("supplierid", prospectId);

        Query onetimepaymentdetail = QueryFactory
                                    .Query("finance.onetimepaymentdetail")
                                    .Select("id")
                                    .WhereNotArchived()
                                    .Where("prospectid", prospectId);
        return await pettyCashExpenseDetail.Union(onetimepaymentdetail).CountAsync<bool>();
    }
}
