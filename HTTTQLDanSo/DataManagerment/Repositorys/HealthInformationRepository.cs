using Dapper;
using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public class HealthInformationRepository : SqlRepository, IHealthInformationRepository
    {
        public HealthInformationRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<IEnumerable<PersonalData>> GetPersonalInformationAsync(int householdID, string regionID, int year)
        {
            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<PersonalData>("dn_Personal_LoadFP", new { iHouseHold_ID = householdID, sRegion_ID = regionID, iYear = year }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GenerateHealth>> GetGenerateHealthInformationAsync(int personalID, string regionID)
        {
            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<GenerateHealth>("dn_Select", new { sTable = "GenerateHealth", sField = "*, '' as Gen_Name, '' as Cause, '' as POB", sCondition = $"Personal_ID = {personalID} AND Region_ID = '{regionID}'" }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<FamilyPlanningHistory>> GetFamilyPlanningHistoryAsync(int personalID, string regionID)
        {
            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<FamilyPlanningHistory>("dn_Select", new { sTable = "FamilyPlanningHistory", sField = "*", sCondition = $"Personal_ID = {personalID} AND Region_ID = '{regionID}'" }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}