using HTTTQLDanSo.Constants;
using HTTTQLDanSo.Models;
using HTTTQLDanSo.Services;
using PagedList;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HTTTQLDanSo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerAccountController : Controller
    {
        private readonly IAccountService _iAccountService;
        private readonly int _pageSize;

        public ManagerAccountController(IAccountService iAccountService)
        {
            _iAccountService = iAccountService;
            if (int.TryParse(System.Configuration.ConfigurationManager.AppSettings[AppSettings.PageSize].ToString(), out int pageSize))
            {
                _pageSize = pageSize;
            }
        }

        // GET: ManagerAccount
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "fistname_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var accounts = await _iAccountService.GetAllAccountsAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = await _iAccountService.GetAllAccountsByNameAsync(searchString);
            }

            switch (sortOrder)
            {
                case "fistname_desc":
                    accounts = accounts.OrderByDescending(s => s.FirstName);
                    break;

                case "region_desc":
                    accounts = accounts.OrderByDescending(s => s.RegionName);
                    break;

                default:  // last ascending
                    accounts = accounts.OrderBy(s => s.LastName);
                    break;
            }

            int pageNumber = (page ?? 1);
            return View(accounts.ToPagedList(pageNumber, _pageSize));
        }

        [HttpGet]
        public async Task<ActionResult> GetRegions(string provinceId)
        {
            var accounts = await _iAccountService.GetRegionsByParrentIdAsync(provinceId);
            var selectItems = new SelectList(accounts, "Region_ID", "Region_Name"); ;
            return Json(selectItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetAddresss(string regionId)
        {
            var accounts = await _iAccountService.GetAddressByRegionIdAsync(regionId);
            var selectItems = new SelectList(accounts, "FieldWorker_ID", "Full_Address"); ;
            return Json(selectItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var account = await _iAccountService.GetRegisterAccountViewModelAsync();

            return View(account);
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Exclude = "Provinces,Districts")] RegisterAccountViewModel registerAccountViewModel)
        {
            var account = await _iAccountService.GetRegisterAccountViewModelAsync();

            var modelState = _iAccountService.ValidateRegisterAccountAsync(registerAccountViewModel);
            if (!modelState.IsValid || !ModelState.IsValid)
            {
                account.PhoneNumber = registerAccountViewModel.PhoneNumber;
                account.FirstName = registerAccountViewModel.FirstName;
                account.LastName = registerAccountViewModel.LastName;
                foreach (var key in modelState.Keys)
                {
                    foreach (var error in modelState[key].Errors)
                    {
                        ModelState.AddModelError(key, error.ErrorMessage);
                    }
                }

                // Return the view with the modified ModelState for error display
                return View(account);
            }

            await _iAccountService.RegisterAccountAsync(registerAccountViewModel);
            ViewData["SuccessMsg"] = "Tạo mới account thành công!";
            return View("Create", account);
        }
    }
}