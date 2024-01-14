using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using HTTTQLDanSo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _iAccountRepository;

        public AccountService(IAccountRepository iAccountRepository)
        {
            _iAccountRepository = iAccountRepository;
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync()
        {
            return await _iAccountRepository.GetAllAccountsAsync();
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccountsByNameAsync(string name)
        {
            return await _iAccountRepository.GetAllAccountsByNameAsync(name);
        }
    }
}