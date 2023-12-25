using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys.Interfaces
{
    public interface IFamilyMemberRepository
    {
        Task<IEnumerable<FamilyMember>> GetFamilyMemberAsync(string HouseHold_ID, string region_ID, string mother_ID);
    }
}