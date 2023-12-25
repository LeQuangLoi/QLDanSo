using Dapper;
using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys.Interfaces
{
    public class PersonalRepository : SqlRepository, IPersonalRepository
    {
        public PersonalRepository(IDatabaseFactory dataFactory) : base(dataFactory)
        {
        }

        public async Task<IEnumerable<PersonalInfo>> GetPersonalByHouseHoldIDAndRegionIdAndpersonStatussAsync(string houseHoldID, string regionId, IEnumerable<string> personStatuss)
        {
            const string query = @"
               SELECT
                    HouseHold_ID,
                    Personal_ID,
                    Last_Name + ' ' + First_Name AS Full_Name,
                    DateOfBirth AS DateOfBirth,
                    SUBSTRING(DateOfBirth, 7, 4) + SUBSTRING(DateOfBirth, 3, 2) + SUBSTRING(DateOfBirth, 1, 2) as DOB,
                    Personal.Sex_ID AS Sex_ID,
                    Sex.Sex_Name AS Sex_Name,
                    Personal.Relation_Code AS Relation_Code,
                    Relation.Relation_Name AS Relation_Name,
                    Personal.Residence_Code AS Residence_Code,
                    ResidenceStatus.Residence_Name AS Residence_Name,
                    Personal.Technical_Code AS Technical_Code,
                    Technical.Technical_Name AS Technical_Name,
                    Personal.Marital_Code AS Marital_Code,
                    MaritalStatus.Marital_Name AS Marital_Name,
                    Personal.Ethnic_Code AS Ethnic_Code,
                    Ethnic.Ethnic_Name AS Ethnic_Name,
                    Personal.Education_Code AS Education_Code,
                    Invalid_Code,
                    Education_Level,
                    Education.Education_Name AS Education_Name,
                    Person_Status,
                    Mother_ID,
                    Birth_Number,
                    Start_Date,
                    ISNULL(Generate_ID, 0) as Generate_ID,
                    Cccd_Bhyt_Code,
                    Change_Date,
                    inbreeding
                FROM
                    Personal
                JOIN
                    Relation ON Personal.Relation_Code = Relation.Relation_Code
                JOIN
                    Ethnic ON Personal.Ethnic_Code = Ethnic.Ethnic_Code
                JOIN
                    Sex ON Personal.Sex_ID = Sex.Sex_ID
                JOIN
                    Technical ON Personal.Technical_Code = Technical.Technical_Code
                JOIN
                    Education ON Personal.Education_Code = Education.Education_Code
                JOIN
                    MaritalStatus ON Personal.Marital_Code = MaritalStatus.Marital_Code
                JOIN
                    ResidenceStatus ON Personal.Residence_Code = ResidenceStatus.Residence_Code

                WHERE HouseHold_ID = @houseHoldID AND [Region_ID] = @regionId AND Person_Status NOT IN @personStatuss
                ORDER BY Relation_Code, DOB";

            using (var connection = this.CreateConnection())
            {
                try
                {
                    return await connection.QueryAsync<PersonalInfo>(query, new { houseHoldID, regionId, personStatuss });
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<FamilyPlanningHistory>> GetFamilyPlanningHistoryAsync(int personalId, string regionId)
        {
            using (var connection = this.CreateConnection())
            {
                try
                {
                    const string query = @"
                        SELECT [FPHistory_ID]
                          ,[Contra_Date]
                          ,[Contraceptive_Code]
                          ,[Export_Status]
                          ,[Region_ID]
                          ,[Personal_ID]
                          ,[User_ID]
                          ,[Date_Update]
                        FROM [dbo].[FamilyPlanningHistory]
                        WHERE [Personal_ID] = @personalId AND Region_ID = @regionId";
                    //ORDER BY HouseHold_Code";

                    return await connection.QueryAsync<FamilyPlanningHistory>(query, new { personalId, regionId });
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}