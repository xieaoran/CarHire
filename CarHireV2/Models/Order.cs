using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CarHireV2.Models
{
    public enum OrderCondition
    {
        PayWaiting,
        PaySuccess,
        ConfirmSuccess,
        HireSuccess,
        ReturnSuccess,
        Cancelled
    }

    public enum PlaneOrderCondition
    {
        ConfirmWaiting,
        ConfirmSuccess,
        ArrivalSuccess,
        Cancelled
    }

    public class Order : IDataType
    {
        public Order()
        {
            // Reserved for DataContext
        }

        public Order(
            User user,
            Car car,
            string alipayNumber,
            DateTime dateTimeStart,
            Store store,
            bool needDriver,
            bool manualConfirm,
            string note)
        {
            User = user;
            Car = car;
            AliPayNumber = alipayNumber;
            DateTimeStart = dateTimeStart;
            Store = store;
            NeedDriver = needDriver;
            NeedManualConfirm = manualConfirm;
            Note = note;
            Condition = OrderCondition.PayWaiting;
            DateTimeCreated = DateTime.Now;
            Comment = null;
        }

        public OrderCondition Condition { get; private set; }
        public User User { get; private set; }
        public Car Car { get; private set; }
        public string AliPayNumber { get; private set; }
        public DateTime DateTimeCreated { get; private set; }
        public DateTime DateTimeStart { get; private set; }
        public Store Store { get; private set; }
        public bool NeedDriver { get; private set; }
        public bool NeedManualConfirm { get; private set; }

        [StringLength(50)]
        public string Note { get; private set; }

        public Comment Comment { get; private set; }

        [Key]
        public int ID { get; private set; }

        public void Pay(string alipayNumber)
        {
            AliPayNumber = alipayNumber;
            Condition = OrderCondition.PaySuccess;
            DataRuntime.RuntimeData.DataContext.SaveChanges();
        }

        public void ChangeCondition(int condition)
        {
            Condition = (OrderCondition) condition;
        }

        public string BuildAlipayURL()
        {
            var aliParamsDictionary = new SortedDictionary<string, string>
            {
                {"partner", AlipayParams.PartnerID},
                {"seller_id", AlipayParams.PartnerID},
                {"seller_email", AlipayParams.SellerEmail},
                {"_input_charset", AlipayParams.InputCharset},
                {"service", "create_partner_trade_by_buyer"},
                {"payment_type", "1"},
                {"notify_url", AlipayParams.NotifyURL},
                {"return_url", AlipayParams.ReturnURL},
                {"out_trade_no", ID.ToString()},
                {"subject", "泓驰租车订单" + ID.ToString().PadLeft(8, '0')},
                {"price", Car.Deposit.ToString()},
                {"quantity", "1"},
                {"logistics_fee", "0"},
                {"logistics_type", "EXPRESS"},
                {"logistics_payment", "SELLER_PAY"},
                {"body", Car.BasicInfo},
                {"show_url", AlipayParams.ShowURL + Car.ID},
                {"receive_name", User.Name},
                {"receive_mobile", User.CellPhoneNumber.ToString()}
            };
            var aliParamsString = CommonHelpers.ConnectParamsToURL(aliParamsDictionary);
            var aliParamsSign = CommonHelpers.AlipaySign(aliParamsString);
            aliParamsString += "&sign=" + aliParamsSign + "&sign_type=" + AlipayParams.SignType;
            return HttpUtility.UrlPathEncode(AlipayParams.Gateway + aliParamsString);
        }

        public void Cancel()
        {
            Condition = OrderCondition.Cancelled;
        }

        public void CommentOrder(Comment comment)
        {
            Comment = comment;
        }

        public void RemoveComment()
        {
            Comment = null;
        }
    }

    public class PlaneOrder : IDataType
    {
        public PlaneOrder()
        {
            // Reserved for DataContext
        }

        public PlaneOrder(
            User user,
            Airport airport,
            DateTime dateTimePlane,
            string note)
        {
            User = user;
            Airport = airport;
            DateTimePlane = dateTimePlane;
            Note = note;
            Condition = PlaneOrderCondition.ConfirmWaiting;
            DateTimeCreated = DateTime.Now;
        }

        public PlaneOrderCondition Condition { get; private set; }
        public User User { get; private set; }
        public Airport Airport { get; private set; }
        public DateTime DateTimeCreated { get; private set; }
        public DateTime DateTimePlane { get; private set; }

        [StringLength(50)]
        public string Note { get; private set; }

        [Key]
        public int ID { get; private set; }

        public void ChangeCondition(int condition)
        {
            Condition = (PlaneOrderCondition) condition;
        }

        public bool Cancel()
        {
            Condition = PlaneOrderCondition.Cancelled;
            return true;
        }
    }
}