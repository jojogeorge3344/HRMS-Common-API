using Chef.Common.Model.Dtos;
using Chef.Common.Models;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace Chef.Common.Repositories
{
    public class CommonBranchRepository : GenericRepository<Branch>, ICommonBranchRepository
    {
        public CommonBranchRepository(
            IHttpContextAccessor httpContextAccessor,
            DbSession session) : base(httpContextAccessor, session) { }

        public async Task<BranchDto> Get(int id)
        {
            var sql = @"SELECT 
                              id, 
                              code, 
                              name,
                              companyId,
                              companycode
                            FROM 
                              common.branch 
                            WHERE
                              id = @id
                            AND
                              isarchived = false
                            ORDER BY 
                              name";

            return await Connection.QueryFirstOrDefaultAsync<BranchDto>(sql, new { id });
        }

        public async Task<BranchDto> Get(string code)
        {
            var sql = @"SELECT 
                              id, 
                              code, 
                              name 
                            FROM 
                              common.branch 
                            WHERE
                              code = @code
                            AND
                              isarchived = false
                            ORDER BY 
                              name";

            return await Connection.QueryFirstOrDefaultAsync<BranchDto>(sql, new { code });

        }

        public async Task<IEnumerable<BranchDto>> GetAll()
        {
            var sql = @"SELECT 
                              id, 
                              code, 
                              name 
                            FROM 
                              common.branch 
                            WHERE 
                              isarchived = false 
                            ORDER BY 
                              name";

            return await Connection.QueryAsync<BranchDto>(sql, new { });
        }

        public async Task<IEnumerable<UserBranchDto>> GetAllUserBranches(int userId)
        {
            var sql = @"SELECT
                              ubr.userid,
                              br.id, 
                              br.code, 
                              br.name, 
                              ubr.isdefault 
                            FROM 
                              common.userbranch ubr 
                              JOIN common.branch br ON ubr.branchid = br.id 
                            WHERE 
                              ubr.userid = userId
                            AND br.isarchived = false";

            return await Connection.QueryAsync<UserBranchDto>(sql, new { userId });
        }
    }
}