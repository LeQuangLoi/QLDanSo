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
                STRING_AGG(role.Name, ', ') AS AllRoles,
                u.WorkerId,
                a.Address_Name AS WorkName,
                u.RegionID,
                r.Region_Name AS RegionName
            FROM
                AspNetUsers u
            JOIN
                AspNetUserRoles ur ON u.Id = ur.UserId
            JOIN
                AspNetRoles role ON role.Id = ur.RoleId
            JOIN
                Address a ON u.WorkerId= a.FieldWorker_ID
            JOIN
                Region r ON u.RegionID= r.Region_ID
            GROUP BY
                u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.UserName, u.WorkerId, a.Address_Name, u.RegionID, r.Region_Name";

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<AccountViewModel>(query);
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
                    a.Address_Name AS WorkName,
                    u.RegionID,
                    r.Region_Name AS RegionName
                FROM
                    AspNetUsers u
                JOIN
                    AspNetUserRoles ur ON u.Id = ur.UserId
                JOIN
                    AspNetRoles role ON role.Id = ur.RoleId
                JOIN
                    Address a ON u.WorkerId= a.FieldWorker_ID
                JOIN
                    Region r ON u.RegionID= r.Region_ID
                WHERE FirstName LIKE @name OR LastName LIKE @name
                GROUP BY
                    u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.UserName, u.WorkerId, a.Address_Name, u.RegionID, r.Region_Name";

            var parameters = new { name = $"%{name}%" };
            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<AccountViewModel>(query, parameters);
            }
        }
    }
}