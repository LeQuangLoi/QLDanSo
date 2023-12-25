using Dapper;
using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public class RegionRepository : SqlRepository, IRegionRepository
    {
        public RegionRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<IEnumerable<PhieuThuTinReport>> GetPhieuThuTinReportAsync(string region_ID, string address_ID, string fromHouseHold, string toHouseHold)
        {
            const string sql = @"[dbo].[sp_PTTRegion]";

            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@sRegion_ID", region_ID);
                parameters.Add("@sAddress_ID", address_ID);
                parameters.Add("@sFromHouseHold", fromHouseHold);
                parameters.Add("@sToHouseHold", toHouseHold);
                parameters.Add("@iErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await connection.QueryAsync<PhieuThuTinReport>(sql, parameters, commandType: CommandType.StoredProcedure);
                var errorCode = parameters.Get<int>("@iErrorCode");
                return result;
            }
        }

        public async Task<IEnumerable<PhieuThuTinReport2>> GetPhieuThuTinByHouseHoldIDAsync(string region_ID, string address_ID)
        {
            const string sql = @"[dbo].[dn_PTTByHouseHoldID]";

            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@sRegionID", region_ID);
                parameters.Add("@iHousehold_ID", address_ID);

                var result = await connection.QueryAsync<PhieuThuTinReport2>(sql, parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<Region> GetRegionByRegionIdAsync(string regionId)
        {
            const string query = @"
            SELECT [Region_ID]
                    ,[Region_Name]
                    ,[Parent]
                    ,[Area]
                    ,[Levels]
                    ,[Active]
                    ,[Selected]
                    ,[RegionID_Old]
                    ,[ID]
                FROM [dbo].[Region]
            WHERE [Region_ID] = @regionId";

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Region>(query, new { regionId });
            }
        }

        public async Task<IEnumerable<Region>> GetRegionsByParrentIdAsync(string parrentId)
        {
            const string query = @"
            SELECT [Region_ID]
                    ,[Region_Name]
                    ,[Parent]
                    ,[Area]
                    ,[Levels]
                    ,[Active]
                    ,[Selected]
                    ,[RegionID_Old]
                    ,[ID]
                FROM [dbo].[Region]
            WHERE [Parent] = @parrentId";

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<Region>(query, new { parrentId });
            }
        }
    }
}