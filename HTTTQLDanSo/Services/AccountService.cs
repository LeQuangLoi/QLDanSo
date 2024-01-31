using HTTTQLDanSo.Constants;
using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.DataManagerment.Repositorys.Interfaces;
using HTTTQLDanSo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
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

        public async Task<List<System.Web.Mvc.SelectListItem>> GetAllRegionsAsync()
        {
            var address = await _iRegionRepository.GetAllRegionsAsync();
            if (address == null || !address.Any())
            {
                return Enumerable.Empty<System.Web.Mvc.SelectListItem>().ToList();
            }
            return address.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList();
        }

        public List<RoleViewModel> GetAllRole()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roles = roleManager.Roles.ToList();
            return roles.Select(x => new RoleViewModel { RoleName = x.Name, RoleId = x.Id.ToString() }).ToList();
        }

        public async Task<IEnumerable<System.Web.Mvc.SelectListItem>> GetAllAddressByRegionIdAsync(string regionId)
        {
            var address = await _iAddressRepository.GetAddressesByRegionIdAsync(regionId);
            if (address == null || !address.Any())
            {
                return Enumerable.Empty<System.Web.Mvc.SelectListItem>();
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
                Provinces = provinces.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList(),
                Regions = await GetAllRegionsAsync(),
                Workers = Enumerable.Empty<System.Web.Mvc.SelectListItem>().ToList(),
                SelectedRoleNames = new List<string>()
            };
        }

        public ModelStateDictionary ValidateRegisterAccountAsync(RegisterAccountViewModel registerAccountViewModel, ModelStateDictionary modelState)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = _signInManager.UserManager.Users.FirstOrDefault(x => x.PhoneNumber == registerAccountViewModel.PhoneNumber);
            if (user != null)
            {
                modelState.AddModelError("PhoneNumber", "Số điện thoại đã tồn tại");
            }

            return modelState;
        }

        public async Task<bool> DeleteUserByUserIdAsync(string userId)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = await manager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await manager.DeleteAsync(user);

                return result.Succeeded;
            }
            else
            {
                return false;
            }
        }

        public async Task<Tuple<bool, RegisterAccountViewModel>> RegisterAccountAsync(RegisterAccountViewModel registerAccountViewModel)
        {
            using (var context = new ApplicationDbContext())
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var defaultPassword = System.Configuration.ConfigurationManager.AppSettings[AppSettings.DefaultPassword];
                if (string.IsNullOrEmpty(defaultPassword))
                {
                    defaultPassword = _defaultPassword;
                }

                int.TryParse(registerAccountViewModel.WorkerId, out int workerId);
                var result = await manager.CreateAsync(new ApplicationUser
                {
                    FirstName = registerAccountViewModel.FirstName,
                    LastName = registerAccountViewModel.LastName,
                    PhoneNumber = registerAccountViewModel.PhoneNumber,
                    UserName = registerAccountViewModel.PhoneNumber,
                    ProvinId = registerAccountViewModel.ProvinId,
                    DistrictId = registerAccountViewModel.DistrictId,
                    RegionID = registerAccountViewModel.RegionID,
                    WorkerId = workerId,
                }, defaultPassword);

                var provinces = await _iRegionRepository.GetRegionsByParrentIdAsync(_regionIdLevel1);
                var districts = await _iRegionRepository.GetRegionsByParrentIdAsync(registerAccountViewModel.ProvinId);
                var regions = await _iRegionRepository.GetRegionsByParrentIdAsync(registerAccountViewModel.DistrictId);
                var workers = await _iAddressRepository.GetAddressesByRegionIdAsync(registerAccountViewModel.RegionID);

                registerAccountViewModel.Roles = GetAllRole();
                registerAccountViewModel.Provinces = provinces.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList();
                registerAccountViewModel.Districts = districts.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList();
                registerAccountViewModel.Regions = regions.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList();
                registerAccountViewModel.Workers = workers.Select(x => new SelectListItem { Text = x.Full_Address, Value = x.FieldWorker_ID.ToString() }).ToList();
                if (result == null || !result.Succeeded)
                {
                    return new Tuple<bool, RegisterAccountViewModel>(false, registerAccountViewModel);
                }

                var adminUser = manager.Users.FirstOrDefault(x => x.PhoneNumber == registerAccountViewModel.PhoneNumber);

                if (adminUser != null && registerAccountViewModel.SelectedRoleNames != null && registerAccountViewModel.SelectedRoleNames.Any())
                {
                    foreach (var roleName in registerAccountViewModel.SelectedRoleNames)
                    {
                        if (!string.IsNullOrEmpty(roleName))

                        {
                            manager.AddToRoles(adminUser.Id, new string[] { roleName });
                        }
                    }
                }

                return new Tuple<bool, RegisterAccountViewModel>(true, registerAccountViewModel);
            }
        }

        public ApplicationUser GetUserByPhoneNumber(string phoneNumber)
        {
            return _signInManager.UserManager.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<Tuple<ModelStateDictionary, EditAccountViewModel>> UpdateAccountAccountByIdAsync(EditAccountViewModel editAccountViewModel, ModelStateDictionary modelState)
        {
            using (var context = new ApplicationDbContext())
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var user = await manager.FindByIdAsync(editAccountViewModel.Id);
                if (user == null)
                {
                    return new Tuple<ModelStateDictionary, EditAccountViewModel>(modelState, editAccountViewModel);
                }
                var users = _signInManager.UserManager.Users.Where(x => x.PhoneNumber == editAccountViewModel.PhoneNumber && x.Id != user.Id);
                if (users != null && users.Any())
                {
                    modelState.AddModelError("PhoneNumber", "Số điện thoại đã được đăng ký với 1 tài khoản khác");
                }

                var provinces = await _iRegionRepository.GetRegionsByParrentIdAsync(_regionIdLevel1);
                var districts = await _iRegionRepository.GetRegionsByParrentIdAsync(user.ProvinId);
                var regions = await _iRegionRepository.GetRegionsByParrentIdAsync(user.DistrictId);
                var workers = await _iAddressRepository.GetAddressesByRegionIdAsync(user.RegionID);
                if (!modelState.IsValid)
                {
                    return new Tuple<ModelStateDictionary, EditAccountViewModel>(modelState, new EditAccountViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        SelectedRoleNames = editAccountViewModel.SelectedRoleNames,
                        ProvinId = user.ProvinId,
                        DistrictId = user.DistrictId,
                        RegionID = user.RegionID,
                        WorkerId = user.WorkerId?.ToString(),
                        Roles = GetAllRole(),
                        Provinces = provinces.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList(),
                        Districts = districts.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList(),
                        Regions = regions.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList(),
                        Workers = workers.Select(x => new SelectListItem { Text = x.Full_Address, Value = x.FieldWorker_ID.ToString() }).ToList(),
                    });
                }
                int.TryParse(editAccountViewModel.WorkerId, out int workerId);
                user.FirstName = editAccountViewModel.FirstName;
                user.LastName = editAccountViewModel.LastName;
                user.PhoneNumber = editAccountViewModel.PhoneNumber;
                user.UserName = editAccountViewModel.PhoneNumber;
                user.ProvinId = editAccountViewModel.ProvinId;
                user.DistrictId = editAccountViewModel.DistrictId;
                user.RegionID = editAccountViewModel.RegionID;
                user.WorkerId = workerId;

                var oldRoleNames = await manager.GetRolesAsync(user.Id);
                if (oldRoleNames != null && oldRoleNames.Any())
                {
                    foreach (var roleName in oldRoleNames)
                    {
                        await manager.RemoveFromRoleAsync(user.Id, roleName);
                    }
                }

                if (editAccountViewModel.SelectedRoleNames != null && editAccountViewModel.SelectedRoleNames.Any())
                {
                    foreach (var roleName in editAccountViewModel.SelectedRoleNames)
                    {
                        if (!string.IsNullOrEmpty(roleName))
                        {
                            manager.AddToRoles(user.Id, new string[] { roleName });
                        }
                    }
                }

                await manager.UpdateAsync(user);

                return new Tuple<ModelStateDictionary, EditAccountViewModel>(modelState, new EditAccountViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    SelectedRoleNames = editAccountViewModel.SelectedRoleNames,
                    ProvinId = user.ProvinId,
                    DistrictId = user.DistrictId,
                    RegionID = user.RegionID,
                    WorkerId = user.WorkerId?.ToString(),
                    Roles = GetAllRole(),
                    Provinces = provinces.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList(),
                    Districts = districts.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList(),
                    Regions = regions.Select(x => new SelectListItem { Text = x.Region_Name, Value = x.Region_ID.ToString() }).ToList(),
                    Workers = workers.Select(x => new SelectListItem { Text = x.Full_Address, Value = x.FieldWorker_ID.ToString() }).ToList(),
                });
            }
        }

        public async Task<AccountViewModel> GetAccountByIdAsync(string id)
        {
            return await _iAccountRepository.GetAllAccountsByIdAsync(id);
        }
    }
}