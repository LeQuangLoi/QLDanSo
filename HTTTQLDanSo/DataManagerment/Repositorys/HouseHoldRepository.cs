using Dapper;
using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public class HouseHoldRepository : SqlRepository, IHouseHoldRepository
    {
        public HouseHoldRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<IEnumerable<HouseHold>> GetHouseHoldByRegionIdAndAddressIDAndHouseHoldStatusAsync(string regionId, string addressID, IEnumerable<string> HouseHoldStatus)
        {
            const string query = @"
            SELECT  Address_ID, HouseHold_ID,HouseHold_Code,HouseHold_Number,Region_ID,IsBigHouseHold,HouseHold_Status,Notes,IsNull(IsChecked,0) as IsChecked
                FROM HouseHold
            WHERE [Region_ID] = @regionId AND Address_ID = @addressID AND HouseHold_Status NOT IN @HouseHoldStatus";
            //ORDER BY HouseHold_Code";

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<HouseHold>(query, new { regionId, addressID, HouseHoldStatus });
            }
        }
    }
}