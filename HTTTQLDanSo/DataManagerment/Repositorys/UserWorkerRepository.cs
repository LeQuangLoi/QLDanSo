using Dapper;
using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys.Interfaces
{
    public class UserWorkerRepository : SqlRepository, IUserWorkerRepository
    {
        public UserWorkerRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<bool> AddUserWorkerAsync(IEnumerable<UserWorker> UserWorkers)
        {
            using (var connection = this.CreateConnection())
            {
                if (UserWorkers == null || !UserWorkers.Any())
                {
                    return false;
                }

                string query = "INSERT INTO UserWorkers (WorkerId, UserId) VALUES (@WorkerId, @UserId)";
                await connection.ExecuteAsync(query, UserWorkers);

                return true;
            }
        }

        public async Task<IEnumerable<UserWorker>> GetUserWorkerByUserIdAsync(string userId)
        {
            using (var connection = this.CreateConnection())
            {
                const string query = @"
                        SELECT
                              uw.WorkerId
                            , uw.UserId
                            , Ad.Address_ID
                            , Ad.Full_Address
                            , Ad.Notes
                            , au.LastName + ' ' + au.FirstName AS FieldWorker_Name
                        FROM UserWorkers uw
                        JOIN Address Ad ON uw.WorkerId = ad.FieldWorker_ID
                        JOIN AspNetUsers au ON uw.UserId=au.Id AND au.RegionID= ad.Region_ID
                        WHERE uw.[UserId] =  @userId";

                return await connection.QueryAsync<UserWorker>(query, new { userId });
            }
        }

        public async Task<bool> UpdateUserWorkerAsync(IEnumerable<UserWorker> UserWorkers)
        {
            using (var connection = this.CreateConnection())
            {
                if (UserWorkers == null || !UserWorkers.Any())
                {
                    return false;
                }

                string deleteQuery = "DELETE UserWorkers WHERE UserId = @userId";
                await connection.ExecuteAsync(deleteQuery, new { userId = UserWorkers.First().UserId });

                string query = "INSERT INTO UserWorkers (WorkerId, UserId) VALUES (@WorkerId, @UserId)";
                await connection.ExecuteAsync(query, UserWorkers);

                return true;
            }
        }
    }
}