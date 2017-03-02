using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class HomeController : Controller
    {
        [RequireHttps]
        public ActionResult Index()
        {
            return View(DataRuntime.RuntimeIndex);
        }
        [RequireHttps]
        public ActionResult About()
        {
            return View();
        }
    }
}