using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class AirportsController : ApiController
    {
        // GET: api/Airports
        [RequireHttps]
        public IEnumerable<Airport> GetAllAirports()
        {
            return DataRuntime.RuntimeData.Airports;
        }

        // GET: api/Airports/5
        [RequireHttps]
        public Airport GetAirportByID(int id)
        {
            return DataRuntime.RuntimeData.Airports.First(airport => airport.ID == id);
        }
    }
}