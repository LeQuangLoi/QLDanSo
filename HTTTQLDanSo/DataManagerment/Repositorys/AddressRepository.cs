using Dapper;
using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public class AddressRepository : SqlRepository, IAddressRepository
    {
        public AddressRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<IEnumerable<Address>> GetAddressesByWorkerIdAsync(int wokerId)
        {
            const string query = @"
            SELECT  Address_ID
                , Full_Address
                , Notes
                , FieldWorker_Name
                FROM viewAddress
            WHERE [FieldWorker_ID] = @wokerId
            ORDER BY Full_Address"
            ;

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<Address>(query, new { wokerId });
            }
        }

        public async Task<IEnumerable<Address>> GetAddressesByRegionIdAsync(string regionId)
        {
            const string query = @"
            SELECT
                FieldWorker_ID
                , Address_ID
                , Full_Address
                , Notes
                , FieldWorker_Name
                FROM viewAddress
            WHERE [Region_ID] = @regionId
            ORDER BY Full_Address"
            ;

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<Address>(query, new { regionId });
            }
        }
    }
}