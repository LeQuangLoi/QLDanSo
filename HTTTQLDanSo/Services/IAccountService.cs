using HTTTQLDanSo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync();

        Task<IEnumerable<AccountViewModel>> GetAllAccountsByNameAsync(string name);
    }
}