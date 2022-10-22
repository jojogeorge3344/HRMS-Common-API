using Chef.Common.Models;
using Chef.Common.Repositories;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace Chef.Common.Authentication
{
    public class UserRoleRepository : GenericRepository<User>, IUserRoleRepository
    {
        public UserRoleRepository(IHttpContextAccessor httpContextAccessor, DbSession session) : base(httpContextAccessor, session)
        {
        }

        public async Task<IEnumerable<UserPermission>> GetUserNodesAsync(int id)
        {
            //TODO - check the table name
            string sql = @"SELECT  DISTINCT  cnp.nodename
                            FROM common.user u
                            JOIN common.userrole ur on ur.userid = u.id
                            JOIN common.role r on r.id = ur.roleid
		                    JOIN common.rolenodepermission cnp on r.id = cnp.roleid  where u.id = @id";

            return await Connection.QueryAsync<UserPermission>(sql, new { id });
        }

        public async Task<IEnumerable<UserRoleDto>> GetUserRolesByUserIdAndNodenameAsync(int id, string nodename)
        {
            string sql = @"SELECT  DISTINCT cnp.permissionname 
                            FROM common.user u
                            JOIN common.userrole ur on ur.userid = u.id
                            JOIN common.role r on r.id = ur.roleid
                            JOIN common.rolenodepermission cnp on r.id = cnp.roleid
                            WHERE u.id = @id and cnp.nodename = @nodename";

            return await Connection.QueryAsync<UserRoleDto>(sql, new { id, nodename });
        }

        public async Task<IEnumerable<UserMetaData>> GetUserRolesByUserIdAsync(int id)
        {
            //TODO - check the table name
            string sql = @"SELECT  DISTINCT
                                    firstname,
                                    u.lastname,
                                    ur.username,
                                    cnp.nodename,
                                    cnp.permissionname,
                                    u.id as userid 
                            FROM common.user u join common.userrole ur on ur.userid = u.id
                            JOIN common.role r on r.id = ur.roleid
		                    JOIN common.rolenodepermission cnp on r.id = cnp.roleid  where u.id = @id";

            return await Connection.QueryAsync<UserMetaData>(sql, new { id });
        }
    }
}



