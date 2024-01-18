using HTTTQLDanSo.Constants;
using HTTTQLDanSo.Models;
using HTTTQLDanSo.Services;
using PagedList;
using System;
using System.Linq;
using System.Net;
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

        [HttpGet]
        public async Task<ActionResult> Detail(string id)
        {
            var account = await _iAccountService.GetAccountByIdAsync(id);

            return View(account);
        }

        [HttpPost]
        public async Task<ActionResult> Detail(EditAccountViewModel editAccountViewModel)
        {
            ViewData["ErrorMsg"] = null;
            ViewData["SuccessMsg"] = null;
            var (modelState, account) = await _iAccountService.UpdateAccountAccountByIdAsync(editAccountViewModel, ModelState);
            if (!modelState.IsValid)
            {
                account.PhoneNumber = editAccountViewModel.PhoneNumber;
                account.FirstName = editAccountViewModel.FirstName;
                account.LastName = editAccountViewModel.LastName;
                ViewData["ErrorMsg"] = "Cập nhật account thất bại!";
            }
            else
            {
                ViewData["SuccessMsg"] = "Cập nhật account thành công!";
            }

            return View("Detail", account);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string userId)
        {
            try
            {
                await _iAccountService.DeleteUserByUserIdAsync(userId);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, $"An error occurred while deleting the user: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Exclude = "Provinces,Districts")] RegisterAccountViewModel registerAccountViewModel)
        {
            ViewData["ErrorMsg"] = null;
            ViewData["SuccessMsg"] = null;
            var modelState = _iAccountService.ValidateRegisterAccountAsync(registerAccountViewModel, ModelState);
            if (!modelState.IsValid || !ModelState.IsValid)
            {
                ViewData["ErrorMsg"] = "Tạo mới account thất bại!";
            }
            var (success, account) = await _iAccountService.RegisterAccountAsync(registerAccountViewModel);
            if (success)
            {
                ViewData["SuccessMsg"] = "Tạo mới account thành công!";
            }

            return View("Create", account);
        }
    }
}