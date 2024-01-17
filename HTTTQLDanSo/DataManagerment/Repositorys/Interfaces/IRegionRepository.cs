using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys.Interfaces
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetRegionsByParrentIdAsync(string parrentId);

        Task<Region> GetRegionByRegionIdAsync(string regionId);

        Task<IEnumerable<Region>> GetAllRegionsAsync();

        Task<IEnumerable<PhieuThuTinReport>> GetPhieuThuTinReportAsync(string region_ID, string address_ID, string fromHouseHold, string toHouseHold);

        Task<IEnumerable<PhieuThuTinReport2>> GetPhieuThuTinByHouseHoldIDAsync(string region_ID, string address_ID);
    }
}