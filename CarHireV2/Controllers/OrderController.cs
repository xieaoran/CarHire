using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarHireV2.Models;

namespace CarHireV2.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [RequireHttps]
        public ActionResult Select()
        {
            var selectedCarTypeCookie = HttpContext.Request.Cookies["selectedCarType"];
            var runtimeSelect = selectedCarTypeCookie != null
                ? new SelectViewModel(int.Parse(selectedCarTypeCookie.Value))
                : new SelectViewModel(null);
            return View(runtimeSelect);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult SelectOrder(int carID, string dateStart, string timeStart, int storeID, bool needDriver,
            bool manualConfirm, string note)
        {
            var x = Request.Form;
            var selectOrderResult = new Dictionary<string, object>();
            var userIDObject = Session["LoggedInUserID"];
            if (userIDObject == null)
            {
                selectOrderResult.Add("Succeeded", false);
                selectOrderResult.Add("Error", "用户尚未登录或登录已过期，请登录后再试");
                return Json(selectOrderResult, JsonRequestBehavior.DenyGet);
            }
            var userID = (int)(userIDObject);
            var orderUser = DataRuntime.RuntimeData.Users.First(user => user.ID == userID);
            var orderCar = DataRuntime.RuntimeData.EnabledCars.First(car => car.ID == carID);
            var orderStore = DataRuntime.RuntimeData.Stores.First(store => store.ID == storeID);
            var orderDateTime = CommonHelpers.ParseDateTime(dateStart, timeStart);
            var newOrder = new Order(orderUser, orderCar, null, orderDateTime, orderStore, needDriver, manualConfirm,
                note);
            try
            {
                DataRuntime.RuntimeData.DataContext.Orders.Add(newOrder);
                DataRuntime.RuntimeData.DataContext.SaveChanges();
                DataRuntime.RuntimeData.Orders = DataRuntime.RuntimeData.DataContext.Orders.ToList();
                selectOrderResult.Add("Succeeded", true);
                selectOrderResult.Add("OrderID", newOrder.ID);
                selectOrderResult.Add("Deposit", orderCar.Deposit);
            }
            catch (Exception e)
            {
                selectOrderResult.Add("Succeeded", false);
                selectOrderResult.Add("Error", e.Message);
            }
            return Json(selectOrderResult, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        [RequireHttps]
        public ActionResult PayOrder(int orderID)
        {
            var userIDObject = Session["LoggedInUserID"];
            if (userIDObject == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var userID = (int)(userIDObject);
            try
            {
                var toPayOrder = DataRuntime.RuntimeData.Orders.First(order => order.ID == orderID);
                if (toPayOrder.User.ID != userID)
                {
                    return RedirectToAction("UserPanel", "User");
                }
                return Redirect(toPayOrder.BuildAlipayURL());
            }
            catch
            {
                return RedirectToAction("UserPanel", "User");
            }
        }

        [HttpGet]
        [RequireHttps]
        public ActionResult PayResult()
        {
            var data = Request.QueryString.AllKeys.ToDictionary(key => key, key => Request.QueryString[key]);
            try
            {
                if (CommonHelpers.CheckFromAlipay(data))
                {
                    var paidOrder =
                        DataRuntime.RuntimeData.Orders.First(
                            order => order.ID == int.Parse(data["out_trade_no"]));
                    if (paidOrder.Condition == OrderCondition.PayWaiting) paidOrder.Pay(data["trade_no"]);
                }
                var userIDObject = Session["LoggedInUserID"];
                return userIDObject == null
                    ? RedirectToAction("Index", "Home")
                    : RedirectToAction("UserPanel", "User");
            }
            catch
            {
                var userIDObject = Session["LoggedInUserID"];
                return userIDObject == null
                    ? RedirectToAction("Index", "Home")
                    : RedirectToAction("UserPanel", "User");
            }

        }

        [HttpPost]
        [RequireHttps]
        public ActionResult PayNotify()
        {
            var data = Request.Form.AllKeys.ToDictionary(key => key, key => Request.Form[key]);
            try
            {
                if (!CommonHelpers.CheckFromAlipay(data)) return Content("fail");
                var paidOrder =
                    DataRuntime.RuntimeData.Orders.First(
                        order => order.ID == int.Parse(data["out_trade_no"]));
                if (paidOrder.Condition == OrderCondition.PayWaiting) paidOrder.Pay(data["trade_no"]);
                return Content("success");
            }
            catch
            {
                return Content("fail");
            }
        }

        [RequireHttps]
        public ActionResult Plane()
        {
            return View(DataRuntime.RuntimePlane);
        }

        [HttpPost]
        [RequireHttps]
        public ActionResult PlaneOrder(int airportID, string datePlane, string timePlane, string note)
        {
            var planeOrderResult = new Dictionary<string, object>();
            var userIDObject = Session["LoggedInUserID"];
            if (userIDObject == null)
            {
                planeOrderResult.Add("Succeeded", false);
                planeOrderResult.Add("Error", "用户尚未登录或登录已过期，请登录后再试");
                return Json(planeOrderResult, JsonRequestBehavior.DenyGet);
            }
            var userID = (int)(userIDObject);
            var orderUser = DataRuntime.RuntimeData.Users.First(user => user.ID == userID);
            var orderAirport = DataRuntime.RuntimeData.Airports.First(airport => airport.ID == airportID);
            var orderDateTime = CommonHelpers.ParseDateTime(datePlane, timePlane);
            var newOrder = new PlaneOrder(orderUser, orderAirport, orderDateTime, note);
            try
            {
                DataRuntime.RuntimeData.DataContext.PlaneOrders.Add(newOrder);
                DataRuntime.RuntimeData.DataContext.SaveChanges();
                DataRuntime.RuntimeData.PlaneOrders = DataRuntime.RuntimeData.DataContext.PlaneOrders.ToList();
                planeOrderResult.Add("Succeeded", true);
                planeOrderResult.Add("OrderID", newOrder.ID);
            }
            catch (Exception e)
            {
                planeOrderResult.Add("Succeeded", false);
                planeOrderResult.Add("Error", e.Message);
            }
            return Json(planeOrderResult, JsonRequestBehavior.DenyGet);
        }
    }
}