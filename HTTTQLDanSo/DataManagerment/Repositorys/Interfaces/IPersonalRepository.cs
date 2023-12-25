using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public interface IPersonalRepository
    {
        Task<IEnumerable<PersonalInfo>> GetPersonalByHouseHoldIDAndRegionIdAndpersonStatussAsync(string houseHoldID, string regionId, IEnumerable<string> personStatuss);

        Task<IEnumerable<FamilyPlanningHistory>> GetFamilyPlanningHistoryAsync(int personalId, string regionId);
    }
}