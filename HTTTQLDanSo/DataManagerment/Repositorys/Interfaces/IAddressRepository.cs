using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddressesByWorkerIdAsync(int wokerId);

        Task<IEnumerable<Address>> GetAddressesByRegionIdAsync(string RegionId);
    }
}