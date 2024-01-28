using Dapper;
using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public class HealthInformationRepository : SqlRepository, IHealthInformationRepository
    {
        public HealthInformationRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<IEnumerable<PersonalData>> GetPersonalInformationAsync(string householdID, string regionID, int year)
        {
            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<PersonalData>("dn_Personal_LoadFP", new { iHouseHold_ID = householdID, sRegion_ID = regionID, iYear = year }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PersonalData>> GetPersonalMotherInformationAsync(string householdID, string regionID)
        {
            const string query = @"
            SELECT Personal_ID, Last_Name + ' ' + First_Name as Full_Name FROM Personal
            WHERE HouseHold_ID = @houseHoldID AND [Region_ID] = @regionId
	            AND SEX_ID = 1 AND [dbo].Age_Calculate(DateOfBirth, getdate()) BETWEEN 15 AND 49";

            using (var connection = this.CreateConnection())
            {
                return await connection.QueryAsync<PersonalData>(query, new { householdID, regionID });
            }
        }

        public async Task<IEnumerable<GenerateHealth>> GetGenerateHealthInformationAsync(string personalID, string regionID)
        {
            try
            {
                const string query = @"
                 SELECT  CreatedDate
                        ,Gen_Date
                        ,Generate_Code
                        ,PlaceOfBirth
                        ,Birth_Number
                        ,Deliver
                        ,Date_SLSS
                        ,Result_SLSS
                        ,Date_SLTS1
                        ,Result_SLTS1
                        ,Date_SLTS2
                        ,Result_SLTS2
                FROM GenerateHealth
                WHERE  Personal_ID = @personalID AND Region_ID = @regionID ";

                using (var connection = this.CreateConnection())
                {
                    return await connection.QueryAsync<GenerateHealth>(query, new { personalID, regionID });
                }
            }
            catch (System.Exception ex)
            {
                return Enumerable.Empty<GenerateHealth>();
            }
        }

        public async Task<IEnumerable<FamilyPlanningHistory>> GetFamilyPlanningHistoryAsync(string personalID, string regionID)
        {
            try
            {
                const string query = @"
                 SELECT
                  FOH.FPHistory_ID
                , FOH.Personal_ID
                , FOH.Region_ID
                , FOH.Contra_Date AS Contra_Date
                , FOH.Export_Status
                , FOH.Contraceptive_Code
			    , FOH.Date_Update
                , CM.Contraceptive_Name AS Contraceptive_Name
                FROM FamilyPlanningHistory  FOH
                LEFT JOIN ContraceptiveMethod CM ON FOH.Contraceptive_Code=CM.Contraceptive_Code
                WHERE FOH.Personal_ID = @personalID AND FOH.Region_ID= @regionID";

                using (var connection = this.CreateConnection())
                {
                    return await connection.QueryAsync<FamilyPlanningHistory>(query, new { personalID, regionID });
                }
            }
            catch (System.Exception ex)
            {
                return Enumerable.Empty<FamilyPlanningHistory>();
            }
        }
    }
}