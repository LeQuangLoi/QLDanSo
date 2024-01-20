using HTTTQLDanSo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Models.AccountViewModel>> GetAllAccountsAsync();

        Task<AccountViewModel> GetAllAccountsByIdAsync(string id);

        Task<IEnumerable<AccountViewModel>> GetAllAccountsByNameAsync(string name);
    }
}