using HTTTQLDanSo.Constants;
using System.Web.Mvc;

namespace HTTTQLDanSo.Controllers
{
    public class ExportTemplateFileController : Controller
    {
        // GET: ExportTemplateFile
        public ActionResult AreaList()
        {
            return View();
        }

        public ActionResult DownloadDocumentAreaList()
        {
            // Get the file path of the document
            return DownloadFile(DocumentTemplateNameConstant.AreaList);
        }

        public ActionResult A0CVT()
        {
            // Get the file path of the document
            return View();
        }

        public ActionResult P0CVT()
        {
            // Get the file path of the document
            return View();
        }

        public ActionResult ReportCTVMouthly()
        {
            // Get the file path of the document
            return View();
        }

        public ActionResult ReportCTVQuarterly()
        {
            // Get the file path of the document
            return View();
        }

        public ActionResult ReportCTVAnnualy()
        {
            // Get the file path of the document
            return View();
        }

        public ActionResult DownloadDocumentReportCVTMounthly()
        {
            // Get the file path of the document
            return DownloadFile(DocumentTemplateNameConstant.ReportCVTMounthly);
        }

        public ActionResult DownloadDocumentReportCVTQuarterly()
        {
            // Get the file path of the document
            return DownloadFile(DocumentTemplateNameConstant.ReportCVTQuarterly);
        }

        public ActionResult DownloadDocumentReportCVTAnnualy()
        {
            // Get the file path of the document
            return DownloadFile(DocumentTemplateNameConstant.ReportCVTAnnualy);
        }

        public ActionResult DownloadDocumentP0CVT()
        {
            // Get the file path of the document
            return DownloadFile(DocumentTemplateNameConstant.P0CVT);
        }

        public ActionResult DownloadDocumentA0CVT()
        {
            // Get the file path of the document
            return DownloadFile(DocumentTemplateNameConstant.A0CVT);
        }

        public ActionResult DownloadFile(string fileName)
        {
            // Get the file path based on the provided file name
            string filePath = Server.MapPath($"~/DocumentTemplates/{fileName}");

            // Check if the file exists
            if (System.IO.File.Exists(filePath))
            {
                // Read the file into a byte array
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Return the file as a stream
                return File(fileBytes, "application/octet-stream", fileName);
            }
            else
            {
                // If the file does not exist, return a file not found response or redirect to an error page
                return HttpNotFound("File not found");
            }
        }
    }
}