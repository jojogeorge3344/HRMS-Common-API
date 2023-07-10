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
    public async new Task<IEnumerable<ProspectDto>> GetAsync(int id)
    {
        var sqlQuery = @"SELECT pt.id, pt.prospectcode, pt.prospectname, pt.addressline1, pt.contactperson, pt.contactnumber, pt.email, pt.isassigned, 
                                    pt.businesspartnerid,pt.isarchived, pt.isactive, pt.taxjurisdictionid, pt.currency, pt.bptype, pt.addressline2, 
                                    pt.cityid, pt.cityname, pt.stateid, pt.statename, pt.countryid, pt.countryname, pt.zipcode, pt.faxno,
                                    tj.taxjurisdictioncode,tj.taxname,bp.name
                                    FROM common.prospect pt left join common.taxjurisdiction tj on pt.taxjurisdictionid=tj.id
                                    left join finance.businesspartnerconfiguration bp on  pt.businesspartnerid=bp.id where pt.id=@id";
        var result = await DatabaseSession.QueryAsync<ProspectDto>(sqlQuery, new { id });
        return result;
    }

    public async Task<IEnumerable<ProspectDto>> GetAll()
    {
        var sqlQuery = @"SELECT pt.id, pt.prospectcode, pt.prospectname, pt.addressline1, pt.contactperson, pt.contactnumber, pt.email, pt.isassigned, 
                                    pt.businesspartnerid,pt.isarchived, pt.isactive, pt.taxjurisdictionid, pt.currency, pt.bptype, pt.addressline2, 
                                    pt.cityid, pt.cityname, pt.stateid, pt.statename, pt.countryid, pt.countryname, pt.zipcode, pt.faxno,
                                    tj.taxjurisdictioncode,tj.taxname,bp.name,pt.createddate
                                    FROM common.prospect pt left join common.taxjurisdiction tj on pt.taxjurisdictionid=tj.id
                                    left join finance.businesspartnerconfiguration bp on  pt.businesspartnerid=bp.id where pt.isarchived=false
                                    ORDER BY pt.createddate DESC";
        var result = await DatabaseSession.QueryAsync<ProspectDto>(sqlQuery);
        return result;
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
        code = code.ToUpper();

        return await QueryFactory
       .Query<Prospect>()
       .Where("prospectcode", code)
       .WhereNotArchived()
       .CountAsync<int>() > 0;
    }

    public async Task<bool> IsTaxNoExist(long taxNo)
    {
        return await QueryFactory
        .Query<Prospect>()
        .Where("taxno", taxNo)
        .WhereNotArchived()
        .CountAsync<int>() > 0;
    }
}
