using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public interface IPersonalChangeRepository
    {
        Task<IEnumerable<PersonalChange>> GetPersonalChangeByPersonalIDAndRegionIdAsync(string personalID, string regionId);
    }
}