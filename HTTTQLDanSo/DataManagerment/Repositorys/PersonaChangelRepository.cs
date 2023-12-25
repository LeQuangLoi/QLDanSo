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
    }
}