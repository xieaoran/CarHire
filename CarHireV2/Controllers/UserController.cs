using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class UserController : Controller
    {
        [RequireHttps]
        public ActionResult UserPanel()
        {
            var userIDObject = Session["LoggedInUserID"];
            if (userIDObject == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var userID = (int) (userIDObject);
            var runtimeUserPanel = new UserPanelViewModel(userID);
            return View(runtimeUserPanel);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult Register(string email, string name,
            string password, string confirmPassword, string cellPhoneNumberStr)
        {
            var regResult = new Dictionary<string, object>();
            var checkExist = DataRuntime.RuntimeData.Users.Where(user => user.Email == email);
            if (checkExist.Any())
            {
                regResult.Add("Succeeded", false);
                regResult.Add("Error", "该电子邮箱已被注册");
            }
            else if (password != confirmPassword)
            {
                regResult.Add("Succeeded", false);
                regResult.Add("Error", "两次输入的密码不一致");
            }
            else
            {
                long cellPhoneNumber;
                if (!long.TryParse(cellPhoneNumberStr, out cellPhoneNumber))
                {
                    regResult.Add("Succeeded", false);
                    regResult.Add("Error", "用户信息格式不正确");
                    return Json(regResult, JsonRequestBehavior.DenyGet);
                }
                var newUser = new User(email, password, name, cellPhoneNumber);
                try
                {
                    DataRuntime.RuntimeData.DataContext.Users.Add(newUser);
                    DataRuntime.RuntimeData.DataContext.SaveChanges();
                    DataRuntime.RuntimeData.Users = DataRuntime.RuntimeData.DataContext.Users.ToList();
                    regResult.Add("Succeeded", true);
                    regResult.Add("NewUserName", newUser.Name);
                    Session["LoggedInUserID"] = newUser.ID;
                    Session["LoggedInUserName"] = newUser.Name;
                }
                catch (Exception e)
                {
                    regResult.Add("Succeeded", false);
                    regResult.Add("Error", e.GetType() == typeof (DbEntityValidationException)
                        ? "用户信息格式不正确"
                        : e.Message);
                }
            }
            return Json(regResult, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult Login(string email, string password)
        {
            var loginResult = new Dictionary<string, object>();
            var selectUser = DataRuntime.RuntimeData.Users.Where(user => user.Email == email).ToList();
            if (!selectUser.Any())
            {
                loginResult.Add("Succeeded", false);
                loginResult.Add("Error", "该电子邮箱尚未注册");
                return Json(loginResult, JsonRequestBehavior.DenyGet);
            }
            var loginUser = selectUser.First();
            if (loginUser.CheckPassword(password))
            {
                loginResult.Add("Succeeded", true);
                loginResult.Add("LoggedInUserName", loginUser.Name);
                Session["LoggedInUserID"] = loginUser.ID;
                Session["LoggedInUserName"] = loginUser.Name;
            }
            else
            {
                loginResult.Add("Succeeded", false);
                loginResult.Add("Error", "密码不正确，请核对您的密码");
            }
            return Json(loginResult, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult LogOut()
        {
            var logOutResult = new Dictionary<string, object>();
            Session.Abandon();
            logOutResult.Add("Succeeded", true);
            return Json(logOutResult, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult ChangePassword(string changePassword, string changeConfirmPassword)
        {
            var changeResult = new Dictionary<string, object>();
            var userIDObject = Session["LoggedInUserID"];
            if (userIDObject == null)
            {
                changeResult.Add("Succeeded", false);
                changeResult.Add("Error", "用户尚未登录或登录已过期，请登录后再试");
                return Json(changeResult, JsonRequestBehavior.DenyGet);
            }
            var currentUserID = (int) (userIDObject);
            var currentUser = DataRuntime.RuntimeData.Users.First(user => user.ID == currentUserID);
            if (changePassword != changeConfirmPassword)
            {
                changeResult.Add("Succeeded", false);
                changeResult.Add("Error", "两次输入的密码不一致");
            }
            else
            {
                try
                {
                    currentUser.ChangePassword(changePassword);
                    DataRuntime.RuntimeData.DataContext.SaveChanges();
                    changeResult.Add("Succeeded", true);
                }
                catch (Exception e)
                {
                    changeResult.Add("Succeeded", false);
                    changeResult.Add("Error", e.Message);
                }
            }
            return Json(changeResult, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult ChangeInfo(string name, string cellPhoneNumberStr)
        {
            var infoResult = new Dictionary<string, object>();
            var userIDObject = Session["LoggedInUserID"];
            if (userIDObject == null)
            {
                infoResult.Add("Succeeded", false);
                infoResult.Add("Error", "用户尚未登录或登录已过期，请登录后再试");
                return Json(infoResult, JsonRequestBehavior.DenyGet);
            }
            var currentUserID = (int) (userIDObject);
            var currentUser = DataRuntime.RuntimeData.Users.First(user => user.ID == currentUserID);
            try
            {
                currentUser.ChangeInfo(name, cellPhoneNumberStr);
                DataRuntime.RuntimeData.DataContext.SaveChanges();
                infoResult.Add("Succeeded", true);
            }
            catch (Exception e)
            {
                infoResult.Add("Succeeded", false);
                infoResult.Add("Error", e.GetType() == typeof (DbEntityValidationException)
                    ? "用户信息格式不正确"
                    : e.Message);
            }
            return Json(infoResult, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult Comment(int orderID, int rating, string content)
        {
            var commentResult = new Dictionary<string, object>();
            try
            {
                var currentOrder = DataRuntime.RuntimeData.Orders.First(
                    order => order.ID == orderID);
                if (currentOrder.Comment != null)
                {
                    currentOrder.Comment.UpdateComment(rating, content);
                    DataRuntime.RuntimeData.DataContext.SaveChanges();
                }
                else
                {
                    var newComment = new Comment(currentOrder.User, currentOrder.Car, rating, content);
                    DataRuntime.RuntimeData.DataContext.Comments.Add(newComment);
                    currentOrder.CommentOrder(newComment);
                    DataRuntime.RuntimeData.DataContext.SaveChanges();
                    DataRuntime.RuntimeData.Comments = DataRuntime.RuntimeData.DataContext.Comments.ToList();
                }

                commentResult.Add("Succeeded", true);
            }
            catch (Exception e)
            {
                commentResult.Add("Succeeded", false);
                commentResult.Add("Error", e.Message);
            }
            return Json(commentResult, JsonRequestBehavior.DenyGet);
        }
    }
}