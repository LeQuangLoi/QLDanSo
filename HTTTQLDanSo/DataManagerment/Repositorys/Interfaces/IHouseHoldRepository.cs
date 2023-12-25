using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public interface IHouseHoldRepository
    {
        Task<IEnumerable<HouseHold>> GetHouseHoldByRegionIdAndAddressIDAndHouseHoldStatusAsync(string regionId, string addressID, IEnumerable<string> HouseHoldStatus);
    }
}