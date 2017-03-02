using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        [RequireHttps]
        public ActionResult Car(int? detailCarType)
        {
            if (detailCarType != null) ViewData["DetailCarType"] = detailCarType;
            return View(DataRuntime.RuntimeData.EnabledCars);
        }
    }
}