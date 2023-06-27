using Chef.Common.Repositories;
using Chef.Finance.Models;
using Chef.Trading.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Repositories;

public class ProspectRepository : TenantRepository<Prospect>, IProspectRepository
{
    public ProspectRepository(IHttpContextAccessor httpContextAccessor, ITenantConnectionFactory session): base(httpContextAccessor, session)
    {

    }

    //public async Task<int> UpdateProspectStatus(int prospectId, bool isAssigned)
    //{
    //    var sqlQuery = @"UPDATE trading.prospect SET isassigned = @isAssigned WHERE id = @prospectId;";
    //    return await DatabaseSession.ExecuteAsync(sqlQuery, new { prospectId, isassigned = @isAssigned });
    //}

    //public async Task<int> DeleteProspect(int prospectId)
    //{
    //    var sqlQuery = @"UPDATE trading.prospect SET isarchived = true WHERE id = @prospectId;";
    //    return await DatabaseSession.ExecuteAsync(sqlQuery, new { prospectId});
    //}

    //public async Task<IEnumerable<TaxJurisdictionDto>> GetAllTaxJurisdiction()
    //{
    //    var sqlQuery = @"select id,taxjurisdictioncode,taxname from common.taxjurisdiction where isarchived=false;";
    //    var result = await DatabaseSession.QueryAsync<TaxJurisdictionDto>(sqlQuery);
    //    return result;
    //}

    //public async Task<IEnumerable<CustomerDto>> GetAllCustomer()
    //{
    //    var sqlQuery = @"select id,code,name,businesspartnerid from trading.businesspartnerconfiguration where businesspartnergrouptype=1 and isarchived=false;";
    //    var result = await DatabaseSession.QueryAsync<CustomerDto>(sqlQuery);
    //    return result;
    //}

    //public async Task<IEnumerable<ProspectDto>> GetAll()
    //{
    //    var sqlQuery = @"SELECT pt.id, pt.prospectcode, pt.prospectname, pt.addressline1, pt.contactperson, pt.contactnumber, pt.email, pt.isassigned, 
    //                                pt.businesspartnerid,pt.isarchived, pt.isactive, pt.taxjurisdictionid, pt.currency, pt.bptype, pt.addressline2, 
    //                                pt.cityid, pt.cityname, pt.stateid, pt.statename, pt.countryid, pt.countryname, pt.zipcode, pt.faxno,
    //                                tj.taxjurisdictioncode,tj.taxname,bp.name,pt.createddate
    //                                FROM trading.prospect pt left join common.taxjurisdiction tj on pt.taxjurisdictionid=tj.id
    //                                left join trading.businesspartnerconfiguration bp on  pt.businesspartnerid=bp.id where pt.isactive=true and pt.isarchived=false
    //                                ORDER BY pt.createddate DESC";
    //    var result = await DatabaseSession.QueryAsync<ProspectDto>(sqlQuery);
    //    return result;
    //}
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
    //public async Task<IEnumerable<Prospect>> GetAllProspect()
    //{

    //    string strquery = string.Format(@"select
    //                            id,prospectcode,prospectname,addressline1,contactperson,contactnumber,email,isassigned,
    //                            businesspartnerid,createddate,modifieddate,createdby,modifiedby,isarchived,isactive,
    //                            taxjurisdictionid,currency,bptype,addressline2,cityid,cityname,stateid,statename,
    //                            countryid,countryname,zipcode,faxno
    //               from trading.prospect where isarchived = false");
    //    return await DatabaseSession.QueryAsync<Prospect>(strquery);
    //}

    public async Task<int> GetExistingProspectAsync(Prospect obj)
    {
        string prospectcode = obj.ProspectCode;
        string prospectname = obj.ProspectName;
        string query = string.Format("select count(*) from common.prospect itc  where itc.isarchived=false  and (lower(TRIM(itc.prospectcode)) = '{0}' OR  lower(TRIM(itc.prospectname)) = '{1}')", prospectcode.ToLower().Trim(), prospectname.ToLower().Trim());
        var result = await DatabaseSession.QueryFirstOrDefaultAsync<int>(query);
        return result;
    }

    public async Task<int> GetEditExistingProspectAsync(Prospect prospect)
    {
        int id = prospect.Id;
        string prospectcode = prospect.ProspectCode;
        string prospectname = prospect.ProspectName;
        string query = string.Format("select count(*) from trading.prospect itc  where itc.isarchived=false and itc.id != {2} and (lower(TRIM(itc.prospectcode)) = '{0}' OR  lower(TRIM(itc.prospectname)) = '{1}')", prospectcode.ToLower().Trim(), prospectname.ToLower().Trim(), id);
        var result = await DatabaseSession.QueryFirstOrDefaultAsync<int>(query);
        return result;
    }

    public async Task<int> CheckExistingCode(string code)
    {
        code = code.ToUpper();
        string sql = @"select itc.id from common.prospect itc  
                            where itc.isarchived=false and UPPER(itc.prospectcode)=@code;";
        var data = await Connection.QueryFirstOrDefaultAsync<Prospect>(sql, new { code });
        if (data != null)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public async Task<int> CheckExistingTaxNo(int taxNo)
    {
       // taxNo = taxNo.ToUpper();
        string sql = @"select itc.id from common.prospect itc  
                            where itc.isarchived=false and itc.taxno=@taxNo;";
        var data = await Connection.QueryFirstOrDefaultAsync<Prospect>(sql, new { taxNo });
        if (data != null)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
