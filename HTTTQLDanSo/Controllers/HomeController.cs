using HTTTQLDanSo.Extensions;
using HTTTQLDanSo.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HTTTQLDanSo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _iAccountService;

        public HomeController(IAccountService iAccountService)
        {
            _iAccountService = iAccountService;
        }

        public async Task<ActionResult> Index()
        {
            var userId = IdentityExtensions.GetUserId(HttpContext.User.Identity);
            var account = await _iAccountService.GetAccountByIdAsync(userId);

            return View(account);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}