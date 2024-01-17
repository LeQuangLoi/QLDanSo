using HTTTQLDanSo.Constants;
using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using HTTTQLDanSo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HTTTQLDanSo.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _iAccountRepository;
        private readonly IRegionRepository _iRegionRepository;
        private readonly IAddressRepository _iAddressRepository;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private const string _defaultPassword = "Abc@123";
        private const string _regionIdLevel1 = "0000000000";

        public AccountService(IAccountRepository iAccountRepository, IRegionRepository iRegionRepository, IAddressRepository iAddressRepository, ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            _iAccountRepository = iAccountRepository;
            _iRegionRepository = iRegionRepository;
            _iAddressRepository = iAddressRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync()
        {
            return await _iAccountRepository.GetAllAccountsAsync();
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccountsByNameAsync(string name)
        {
            return await _iAccountRepository.GetAllAccountsByNameAsync(name);
        }

        public async Task<List<SelectListItem>> GetAllRegionsAsync()
        {
            var address = await _iRegionRepository.GetAllRegionsAsync();
            if (address == null || !address.Any())
            {
                return Enumerable.Empty<SelectListItem>().ToList();
            }
            return address.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList();
        }

        public List<SelectListItem> GetAllRole()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roles = roleManager.Roles.ToList();
            return roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAddressByRegionIdAsync(string regionId)
        {
            var address = await _iAddressRepository.GetAddressesByRegionIdAsync(regionId);
            if (address == null || !address.Any())
            {
                return Enumerable.Empty<SelectListItem>();
            }
            return address.Select(x => new SelectListItem { Text = x.Full_Address, Value = x.Address_ID.ToString() }).ToList();
        }

        public async Task<IEnumerable<Region>> GetRegionsByParrentIdAsync(string provinceId)
        {
            var provinces = await _iRegionRepository.GetRegionsByParrentIdAsync(provinceId);
            return provinces;
        }

        public async Task<IEnumerable<Address>> GetAddressByRegionIdAsync(string regionId)
        {
            var address = await _iAddressRepository.GetAddressesByRegionIdAsync(regionId);
            return address;
        }

        public async Task<RegisterAccountViewModel> GetRegisterAccountViewModelAsync()
        {
            var provinces = await _iRegionRepository.GetRegionsByParrentIdAsync(_regionIdLevel1);

            return new RegisterAccountViewModel
            {
                Roles = GetAllRole(),
                Workers = Enumerable.Empty<SelectListItem>().ToList(),
                Provinces = provinces.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList(),
                Regions = await GetAllRegionsAsync()
            };
        }

        public ModelStateDictionary ValidateRegisterAccountAsync(RegisterAccountViewModel registerAccountViewModel)
        {
            var modelState = new ModelStateDictionary();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = _signInManager.UserManager.Users.FirstOrDefault(x => x.PhoneNumber == registerAccountViewModel.PhoneNumber);
            if (user != null)
            {
                modelState.AddModelError("PhoneNumber", "Số điện thoại đã tồn tại");
            }

            return modelState;
        }

        public async Task<RegisterAccountViewModel> RegisterAccountAsync(RegisterAccountViewModel registerAccountViewModel)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var defaultPassword = System.Configuration.ConfigurationManager.AppSettings[AppSettings.DefaultPassword];
            if (string.IsNullOrEmpty(defaultPassword))
            {
                defaultPassword = _defaultPassword;
            }
            int? workerId = null;
            if (int.TryParse(registerAccountViewModel.WorkerId, out int _workerId))
            {
                workerId = _workerId;
            }
            var result = await manager.CreateAsync(new ApplicationUser
            {
                FirstName = registerAccountViewModel.FirstName,
                LastName = registerAccountViewModel.LastName,
                PhoneNumber = registerAccountViewModel.PhoneNumber,
                UserName = registerAccountViewModel.PhoneNumber,
                RegionID = registerAccountViewModel.RegionID,
                WorkerId = workerId,
            }, defaultPassword);

            if (result == null)
            {
                return null;
            }
            return registerAccountViewModel;
        }
    }
}