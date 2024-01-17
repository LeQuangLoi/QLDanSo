using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HTTTQLDanSo.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync();

        Task<IEnumerable<AccountViewModel>> GetAllAccountsByNameAsync(string name);

        Task<IEnumerable<Region>> GetRegionsByParrentIdAsync(string provinceId);

        Task<IEnumerable<Address>> GetAddressByRegionIdAsync(string regionId);

        Task<RegisterAccountViewModel> GetRegisterAccountViewModelAsync();

        ModelStateDictionary ValidateRegisterAccountAsync(RegisterAccountViewModel registerAccountViewModel);

        Task<RegisterAccountViewModel> RegisterAccountAsync(RegisterAccountViewModel registerAccountViewModel);
    }
}