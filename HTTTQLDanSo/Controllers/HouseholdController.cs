using HTTTQLDanSo.Constants;
using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.Extensions;
using HTTTQLDanSo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HTTTQLDanSo.Controllers
{
    public class HouseholdController : Controller
    {
        private readonly IHouseHoldService _houseHoldService;
        private readonly int _pageSize;

        public HouseholdController(IHouseHoldService houseHoldService)
        {
            _houseHoldService = houseHoldService;
            if (int.TryParse(System.Configuration.ConfigurationManager.AppSettings[AppSettings.PageSize].ToString(), out int pageSize))
            {
                _pageSize = pageSize;
            }
        }

        // GET: Household
        public async Task<ActionResult> Index()
        {
            var regionId = IdentityExtensions.GetRegionID(HttpContext.User.Identity);

            var region = await _houseHoldService.GetRegionByRegionIdAsync(regionId);
            var regionName = region != null ? region.Region_Name : string.Empty;

            ViewBag.RegionId = regionId;
            ViewBag.RegionName = regionName;
            ViewBag.Address = await GetAddressesByWorkerIdAsync();

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetHouseholdAsync(string searchText, string regionID, string addressID, int? page)
        {
            var peronals = await _houseHoldService.GetHouseHoldByHouseHoldIDAndRegionIdAndStatusAsync(regionID, addressID, new List<string>() { "D", "G" });

            //if (!string.IsNullOrEmpty(houseHoldID) || !string.IsNullOrEmpty(regionID))
            //{
            //    ViewBag.txtSearch = txtSearch;
            //    data = data.Where(s => s.Title.Contains(txtSearch));
            //}

            if (page == 0)
            {
                page = 1;
            }
            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = peronals.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = peronals.OrderByDescending(x => x.HouseHold_Code).Skip(start).Take(_pageSize);
            List<HouseHold> listPost = new List<HouseHold>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetPersonalAsync(string searchText, string houseHoldID, string regionID, int? page)
        {
            var peronals = await _houseHoldService.GetPersonalByHouseHoldIDAndRegionIdAndpersonStatussAsync(houseHoldID, regionID, new List<string>() { "D", "G" });

            if (page == 0)
            {
                page = 1;
            }
            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = peronals.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = peronals.OrderByDescending(x => x.Relation_Code).Skip(start).Take(_pageSize);
            List<PersonalInfo> listPost = new List<PersonalInfo>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetPersonalChangeAsync(string searchText, string personalId, string regionID, int? page)
        {
            var peronals = await _houseHoldService.GetPersonalChangeByPersonalIDAndRegionIdAsync(personalId, regionID);

            if (page == 0)
            {
                page = 1;
            }
            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = peronals.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = peronals.OrderByDescending(x => x.Change_Date).Skip(start).Take(_pageSize);
            List<PersonalChange> listPost = new List<PersonalChange>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetGetFamilyMemberAsync(string searchText, string houseHoldID, string regionId, string mother_ID, int? page)
        {
            var familyMembers = await _houseHoldService.GetFamilyMemberAsync(houseHoldID, regionId, mother_ID);

            if (page == 0)
            {
                page = 1;
            }
            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = familyMembers.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = familyMembers.OrderByDescending(x => x.Personal_ID).Skip(start).Take(_pageSize);
            List<FamilyMember> listPost = new List<FamilyMember>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> LoadAddressesByRegionIdAsync(string regionId)
        {
            var address = await _houseHoldService.GetAddressesByRegionIdAsync(regionId);

            var selectList = new SelectList(address, "Address_ID", "Full_Address");

            return Json(selectList, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> LoadAddressesByWorkerIdAsync()
        {
            var workerId = IdentityExtensions.GetWorkerId(HttpContext.User.Identity);
            var address = await _houseHoldService.GetAddressesByWorkerIdAsync(workerId);

            var selectList = await GetAddressesByWorkerIdAsync();

            return Json(selectList, JsonRequestBehavior.AllowGet);
        }

        public async Task<SelectList> GetAddressesByWorkerIdAsync()
        {
            var workerId = IdentityExtensions.GetWorkerId(HttpContext.User.Identity);
            var address = await _houseHoldService.GetAddressesByWorkerIdAsync(workerId);

            return new SelectList(address, "Address_ID", "Full_Address");
        }

        // GET: Household/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Household/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Household/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Household/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Household/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Household/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Household/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}