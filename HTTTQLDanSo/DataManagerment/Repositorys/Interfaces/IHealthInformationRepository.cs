using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys.Interfaces
{
    public interface IHealthInformationRepository
    {
        Task<IEnumerable<PersonalData>> GetPersonalInformationAsync(string householdID, string regionID, int year);

        Task<IEnumerable<PersonalData>> GetPersonalMotherInformationAsync(string householdID, string regionID);

        Task<IEnumerable<GenerateHealth>> GetGenerateHealthInformationAsync(string personalID, string regionID);

        Task<IEnumerable<FamilyPlanningHistory>> GetFamilyPlanningHistoryAsync(string personalID, string regionID);
    }
}