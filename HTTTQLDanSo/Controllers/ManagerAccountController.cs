using HTTTQLDanSo.Constants;
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
    }
}