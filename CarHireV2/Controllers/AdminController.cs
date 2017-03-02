using System;
using System.Linq;
using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class AdminController : Controller
    {
        [RequireHttps]
        public ActionResult GetQuestion()
        {
            if (Session["AdminVerified"] != null) return Content("管理客户端已通过验证，无需重复验证");
            var question = Guid.NewGuid().ToString();
            Session["AdminQuestion"] = question;
            return Content(CommonHelpers.RSAEncrypt(question));
        }

        [RequireHttps]
        public ActionResult AnswerQuestion(string answer)
        {
            if (Session["AdminVerified"] != null) return Content("管理客户端已通过验证，无需重复验证");
            var questionObject = Session["AdminQuestion"];
            if (questionObject == null) return Content("尚未提出验证问题");
            if ((string) questionObject != answer)
            {
                Session.Remove("AdminQuestion");
                return Content("验证失败，请重新生成验证问题");
            }
            Session["AdminVerified"] = true;
            return Content("Succeeded");
        }

        [RequireHttps]
        public ActionResult RefreshServer()
        {
            if (Session["AdminVerified"] == null) return Content("管理客户端未通过验证或验证已过期");
            try
            {
                DataRuntime.RuntimeData = new DataLists();
                DataRuntime.RuntimeProperty = DataRuntime.RuntimeData.GlobalProperties.First();
                DataRuntime.RuntimeIndex = new IndexViewModel();
                DataRuntime.RuntimePlane = new PlaneViewModel();
                DataRuntime.RuntimeMaxValues = new MaxValues();
                return Content("Succeeded");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [RequireHttps]
        public ActionResult ChangeOrderCondition(int id, int condition)
        {
            if (Session["AdminVerified"] == null) return Content("管理客户端未通过验证或验证已过期");
            try
            {
                var currentOrder = DataRuntime.RuntimeData.Orders.First(order => order.ID == id);
                currentOrder.ChangeCondition(condition);
                DataRuntime.RuntimeData.DataContext.SaveChanges();
                return Content("Succeeded");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [RequireHttps]
        public ActionResult ChangePlaneOrderCondition(int id, int condition)
        {
            if (Session["AdminVerified"] == null) return Content("管理客户端未通过验证或验证已过期");
            try
            {
                var currentPlaneOrder = DataRuntime.RuntimeData.PlaneOrders.First(order => order.ID == id);
                currentPlaneOrder.ChangeCondition(condition);
                DataRuntime.RuntimeData.DataContext.SaveChanges();
                return Content("Succeeded");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [RequireHttps]
        public ActionResult DeleteComment(int id)
        {
            if (Session["AdminVerified"] == null) return Content("管理客户端未通过验证或验证已过期");
            try
            {
                var currentComment = DataRuntime.RuntimeData.Comments.First(comment => comment.ID == id);
                var commentOrder = DataRuntime.RuntimeData.Orders.First(order => order.Comment == currentComment);
                commentOrder.RemoveComment();
                DataRuntime.RuntimeData.DataContext.Comments.Remove(currentComment);
                DataRuntime.RuntimeData.DataContext.SaveChanges();
                DataRuntime.RuntimeData.Comments = DataRuntime.RuntimeData.DataContext.Comments.ToList();
                return Content("Succeeded");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}