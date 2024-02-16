using Dapper;
using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using HTTTQLDanSo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public class AccountRepository : SqlRepository, IAccountRepository
    {
        public AccountRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync()
        {
            const string query = @"
            SELECT
                u.Id,
                u.FirstName,
                u.LastName,
                u.PhoneNumber,
                u.UserName,
                STUFF((SELECT ', ' + role.Name
                       FROM AspNetUserRoles ur
                JOIN AspNetRoles role ON role.Id = ur.RoleId
                WHERE ur.UserId = u.Id
                   FOR XML PATH('')), 1, 2, '') AS AllRoles,
                STUFF((SELECT ' |  ' + ad.Full_Address
                       FROM Address ad
					   JOIN UserWorkers uw on ad.FieldWorker_ID= uw.WorkerId
                    WHERE uw.UserId = u.Id  AND ad.Region_ID= u.RegionID
                   FOR XML PATH('')), 1, 2, '') AS AllAddress,
            u.RegionID,
            r.Region_Name AS RegionName
            FROM
                AspNetUsers u
            JOIN
                Region r ON u.RegionID = r.Region_ID
            GROUP BY
                u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.UserName, u.RegionID, r.Region_Name";

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<AccountViewModel>(query);
            }
        }

        public async Task<AccountViewModel> GetAccountsByIdAsync(string id)
        {
            const string query = @"
                  SELECT
                    u.Id,
                    u.FirstName,
                    u.LastName,
					pr.Region_Name as ProvinName,
					di.Region_ID as DistrictId,
					di.Region_Name as DistrictName,
                    u.RegionID,
                    r.Region_Name AS RegionName,
                    STUFF((SELECT ' |  ' + ad.Full_Address
                       FROM ViewAddress ad
					   JOIN UserWorkers uw on ad.FieldWorker_ID= uw.WorkerId
                    WHERE uw.UserId = u.Id AND ad.Region_ID= u.RegionID
                   FOR XML PATH('')), 1, 2, '') AS Full_Address
                FROM
                    AspNetUsers u
                JOIN
                    Region r ON u.RegionID = r.Region_ID
				LEFT JOIN Region di on u.DistrictId = di.Region_ID
				LEFT JOIN Region pr on u.ProvinId = pr.Region_ID
                WHERE u.Id = @id";

            var parameters = new { id = $"{id}" };
            using (var connection = this.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<AccountViewModel>(query, parameters);
            }
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccountsByNameAsync(string name)
        {
            const string query = @"
                SELECT
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.PhoneNumber,
                    u.UserName,
                    STRING_AGG(role.Name, ', ') AS AllRoles,
                    u.WorkerId,
                    u.RegionID,
                    r.Region_Name AS RegionName
                FROM
                    AspNetUsers u
                JOIN
                    AspNetUserRoles ur ON u.Id = ur.UserId
                JOIN
                    AspNetRoles role ON role.Id = ur.RoleId
                JOIN
                    Region r ON u.RegionID= r.Region_ID
                WHERE FirstName LIKE @name OR LastName LIKE @name
                GROUP BY
                    u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.UserName, u.RegionID, r.Region_Name";

            var parameters = new { name = $"%{name}%" };
            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<AccountViewModel>(query, parameters);
            }
        }
    }
}