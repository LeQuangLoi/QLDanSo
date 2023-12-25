using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.Services
{
    public interface IHouseHoldService
    {
        Task<Region> GetRegionByRegionIdAsync(string regionId);

        Task<IEnumerable<Region>> GetRegionsByParrentIdAsync(string parrentId);

        Task<IEnumerable<Address>> GetAddressesByRegionIdAsync(string regionId);

        Task<IEnumerable<Address>> GetAddressesByWorkerIdAsync(int? workerId);

        Task<IEnumerable<HouseHold>> GetHouseHoldByHouseHoldIDAndRegionIdAndStatusAsync(string regionId, string addressID, IEnumerable<string> houseHoldStatus);

        Task<IEnumerable<PersonalInfo>> GetPersonalByHouseHoldIDAndRegionIdAndpersonStatussAsync(string houseHoldID, string regionId, IEnumerable<string> personStatuss);

        Task<IEnumerable<PersonalChange>> GetPersonalChangeByPersonalIDAndRegionIdAsync(string personalID, string regionId);

        Task<IEnumerable<FamilyMember>> GetFamilyMemberAsync(string houseHoldID, string regionId, string mother_ID);
    }
}