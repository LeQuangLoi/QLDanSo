using Dapper;
using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public class FamilyMemberRepository : SqlRepository, IFamilyMemberRepository
    {
        public FamilyMemberRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<IEnumerable<FamilyMember>> GetFamilyMemberAsync(string houseHold_ID, string region_ID, string mother_ID)
        {
            using (var connection = this.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<FamilyMember>("dn_Personal_Mother",
                        new { iHouseHold_ID = houseHold_ID, sRegion_ID = region_ID, iMother_ID = mother_ID },
                        commandType: CommandType.StoredProcedure);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}