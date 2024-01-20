using System.Web.Mvc;

namespace HTTTQLDanSo.Controllers
{
    [Authorize(Roles = "CTV")]
    public class DocumentGuideController : Controller
    {
        // GET: DocumentGuide
        public ActionResult A0()
        {
            return View();
        }

        public ActionResult InformationReceipt()
        {
            return View();
        }

        public ActionResult YearlyCompare()
        {
            return View();
        }

        public ActionResult ContraceptiveAndMaternityMeasures()
        {
            return View();
        }

        public ActionResult EthnicList()
        {
            return View();
        }

        public ActionResult EducationLevelComparisonTable()
        {
            return View();
        }
    }
}