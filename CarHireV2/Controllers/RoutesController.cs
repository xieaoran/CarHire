using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class RoutesController : ApiController
    {
        // GET: api/Routes
        [RequireHttps]
        public IEnumerable<Route> GetAllRoutes()
        {
            return DataRuntime.RuntimeData.Routes;
        }

        // GET: api/Routes/5
        [RequireHttps]
        public Route GetRouteByID(int id)
        {
            return DataRuntime.RuntimeData.Routes.First(route => route.ID == id);
        }

        // Reserved For CORS
        [RequireHttps]
        public string Options()
        {
            return null;
        }
    }
}