using HTTTQLDanSo.Constants;
using HTTTQLDanSo.DataManagerment.DataModel;
using HTTTQLDanSo.Extensions;
using HTTTQLDanSo.Services;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HTTTQLDanSo.Controllers
{
    public class TestItemClass
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public double Money { get; set; }

        public string Address { get; set; }
    }

    [Authorize(Roles = "CTV")]
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

        private async Task<Stream> CreateExcelFileAsync(string houseHoldID, string addressName, string regionName, Stream stream = null)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
                {
                    excelPackage.Workbook.Properties.Author = "loilequang";
                    excelPackage.Workbook.Properties.Title = "DanhSachHo";
                    excelPackage.Workbook.Worksheets.Add("DanhSachHo");
                    var workSheet = excelPackage.Workbook.Worksheets[0];
                    var regionId = IdentityExtensions.GetRegionID(HttpContext.User.Identity);

                    var peronals = await _houseHoldService.GetPersonalByHouseHoldIDAndRegionIdAndpersonStatussAsync(houseHoldID, regionId, new List<string>() { "D", "G" });

                    var dataToExport = peronals.Select(x => new Personal
                    {
                        Code = string.Empty,
                        Education_Name = x.Education_Name,
                        Ethnic_Name = x.Ethnic_Name,
                        Marital_Name = x.Marital_Name,
                        Relation_Name = x.Relation_Name,
                        Residence_Name = x.Residence_Name,
                        Sex_Name = x.Sex_Name,
                        Technical_Name = x.Technical_Name,
                        DateOfBirth = x.DateOfBirth,
                        Full_Name = x.Full_Name,
                    });
                    workSheet.Cells["A1"].Value = "DANH SÁCH CÁ NHÂN";
                    workSheet.Cells["A1"].Style.Font.Bold = true;
                    workSheet.Cells["A2"].Value = $"Hộ số: {houseHoldID}, {addressName} , {regionName}";
                    workSheet.Cells["A2"].Style.Font.Bold = true;
                    workSheet.Cells["A3"].LoadFromCollection(dataToExport, true, TableStyles.Dark9);
                    excelPackage.Save();
                    return excelPackage.Stream;
                }
            }
            catch (Exception ex)
            {
                return stream;
            }
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(string houseHoldID, string addressName, string regionName)
        {
            // Gọi lại hàm để tạo file excel
            var stream = await CreateExcelFileAsync(houseHoldID, addressName, regionName);
            var buffer = stream as MemoryStream;
            var fileName = $"DanhSachHo_{houseHoldID}_{DateTime.UtcNow.ToLongTimeString()}";
            var header = $"attachment; filename={fileName}.xlsx";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", header);
            Response.BinaryWrite(buffer.ToArray());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");
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

            if (!page.HasValue || page.Value == 0)
            {
                page = 1;
            }
            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = peronals.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = peronals.OrderBy(x => x.HouseHold_Number).Skip(start).Take(_pageSize);
            List<HouseHold> listPost = new List<HouseHold>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetPersonalAsync(string searchText, string houseHoldID, string regionID, int? page)
        {
            var peronals = await _houseHoldService.GetPersonalByHouseHoldIDAndRegionIdAndpersonStatussAsync(houseHoldID, regionID, new List<string>() { "D", "G" });

            if (!page.HasValue || page.Value == 0)
            {
                page = 1;
            }
            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = peronals.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = peronals.OrderBy(x => x.Relation_Code).Skip(start).Take(_pageSize);
            List<PersonalInfo> listPost = new List<PersonalInfo>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetPersonalChangeAsync(string searchText, string personalId, string regionID, int? page)
        {
            var peronals = await _houseHoldService.GetPersonalChangeByPersonalIDAndRegionIdAsync(personalId, regionID);

            if (!page.HasValue || page.Value == 0)
            {
                page = 1;
            }
            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = peronals.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = peronals.OrderBy(x => x.Change_Date).Skip(start).Take(_pageSize);
            List<PersonalChange> listPost = new List<PersonalChange>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetPersonalMotherInformationAsync(string searchText, string personalId, string regionID, int? page)
        {
            var peronals = await _houseHoldService.GetPersonalMotherInformationAsync(personalId, regionID);

            if (!page.HasValue || page.Value == 0)
            {
                page = 1;
            }

            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = peronals.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = peronals.OrderBy(x => x.DateOfBirth).Skip(start).Take(_pageSize);
            List<PersonalData> listPost = new List<PersonalData>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetGenerateHealthInformationAsync(string searchText, string personalId, string regionID, int? page)
        {
            var peronals = await _houseHoldService.GetGenerateHealthInformationAsync(personalId, regionID);

            if (!page.HasValue || page.Value == 0)
            {
                page = 1;
            }

            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = peronals.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = peronals.OrderBy(x => x.Gen_Date).Skip(start).Take(_pageSize);
            List<GenerateHealth> listPost = new List<GenerateHealth>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetFamilyPlanningHistoryAsync(string searchText, string personalId, string regionID, int? page)
        {
            var peronals = await _houseHoldService.GetFamilyPlanningHistoryAsync(personalId, regionID);

            if (!page.HasValue || page.Value == 0)
            {
                page = 1;
            }

            int start = (int)(page - 1) * _pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = peronals.Count();
            float totalNumsize = (totalPage / (float)_pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = peronals.OrderBy(x => x.Contra_Date).Skip(start).Take(_pageSize);
            List<FamilyPlanningHistory> listPost = new List<FamilyPlanningHistory>();
            listPost = dataPost.ToList();
            return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetGetFamilyMemberAsync(string searchText, string houseHoldID, string regionId, string mother_ID, int? page)
        {
            var familyMembers = await _houseHoldService.GetFamilyMemberAsync(houseHoldID, regionId, mother_ID);

            if (!page.HasValue || page.Value == 0)
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