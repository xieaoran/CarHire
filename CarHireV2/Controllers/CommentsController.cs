using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class CommentsController : ApiController
    {
        // GET: api/Comments
        [RequireHttps]
        public IEnumerable<Comment> GetAllComments()
        {
            return DataRuntime.RuntimeData.Comments;
        }

        // GET: api/Comments/5
        [RequireHttps]
        public IEnumerable<Comment> GetCommentByID(int id)
        {
            return DataRuntime.RuntimeData.Comments.Where(
                comment => comment.ID == id);
        }

        [RequireHttps]
        public Comment GetCommentByOrderID(int orderID)
        {
            return DataRuntime.RuntimeData.Orders.First(
                order => order.ID == orderID).Comment;
        }
    }
}