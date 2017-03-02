using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class StoresController : ApiController
    {
        // GET: api/Stores
        [RequireHttps]
        public IEnumerable<Store> GetAllStores()
        {
            return DataRuntime.RuntimeData.Stores;
        }

        // GET: api/Stores/5
        [RequireHttps]
        public Store GetStoreByID(int id)
        {
            return DataRuntime.RuntimeData.Stores.First(store => store.ID == id);
        }

        // Reserved For CORS
        [RequireHttps]
        public string Options()
        {
            return null;
        }
    }
}