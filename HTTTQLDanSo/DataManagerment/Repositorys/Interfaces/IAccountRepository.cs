using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Models.AccountViewModel>> GetAllAccountsAsync();

        Task<IEnumerable<Models.AccountViewModel>> GetAllAccountsByNameAsync(string name);
    }
}