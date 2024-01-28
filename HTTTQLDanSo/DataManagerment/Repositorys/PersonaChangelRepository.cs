using Dapper;
using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys.Interfaces
{
    public class PersonalChangeRepository : SqlRepository, IPersonalChangeRepository
    {
        public PersonalChangeRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<IEnumerable<PersonalChange>> GetPersonalChangeByPersonalIDAndRegionIdAsync(string personalID, string regionId)
        {
            const string query = @"
            SELECT
                Personal_ID,
                Full_Name,
                viewChange.ChangeType_Code AS ChangeType_Code,
                ChangeType_Name,
                Change_Date,
                Change_ID,
                Come_date
            FROM
                viewChange
            JOIN
                ChangeType ON viewChange.ChangeType_Code = ChangeType.ChangeType_Code
            WHERE [Personal_ID] = @personalID AND [Region_ID] = @regionId";

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<PersonalChange>(query, new { personalID, regionId });
            }
        }

        public async Task<IEnumerable<PersonalChange>> GetPersonalChangeByHouseHoldIDAndRegionIdAsync(int houseHoldID, string regionId)
        {
            const string query = @"
            SELECT
                     viewChange.Personal_ID, viewChange.Full_Name, viewChange.ChangeType_Code, viewChange.Change_Date, viewChange.Change_ID, viewChange.Come_date
                FROM
                    Personal
				JOIN viewChange ON viewChange.Personal_ID=Personal.Personal_ID
                WHERE HouseHold_ID = @houseHoldID AND Personal.[Region_ID] = @regionId AND Person_Status NOT IN (@personStatuss1,@personStatuss2)
            WHERE [Personal_ID] = @personalID AND [Region_ID] = @regionId";

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<PersonalChange>(query, new { houseHoldID, regionId });
            }
        }
    }
}