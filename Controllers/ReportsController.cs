using ATMMonitoringSystem.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ATMMonitoringSystem.Controllers
{
    public class ReportsController : Controller
    {
        private ATMContext db = new ATMContext();

        public ActionResult ATMReport()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Reports/ATMReport.rpt"));
            rd.SetDataSource(db.ATMs.ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf", "ATMReport.pdf");
        }
    }
}
