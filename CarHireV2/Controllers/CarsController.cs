using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class CarsController : ApiController
    {
        // GET: api/Cars
        [RequireHttps]
        public IEnumerable<Car> GetAllCars()
        {
            return DataRuntime.RuntimeData.EnabledCars;
        }

        // GET: api/Cars/5
        [RequireHttps]
        public CarAPIModel GetCarByID(int id)
        {
            return new CarAPIModel(id);
        }

        // GET: api/Cars/5?commentType=0
        [RequireHttps]
        public IEnumerable<Comment> GetComments(int id, int commentType)
        {
            return CommonHelpers.RandomCommentList(
                DataRuntime.RuntimeData.Comments.Where(
                    comment => comment.Car.ID == id && (int) (comment.Type) == commentType).ToList(), 3);
        }
    }
}