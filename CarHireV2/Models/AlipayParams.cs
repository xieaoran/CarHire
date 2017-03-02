namespace CarHireV2.Models
{
    public static class AlipayParams
    {
        public static string Gateway = "https://mapi.alipay.com/gateway.do?";
        public static string PartnerID = "2088002249838701";
        public static string Key = "pq9ka836e6v9wnf836kyz28tdkq8wgv1";
        public static string InputCharset = "utf-8";
        public static string SignType = "MD5";
        public static string SellerEmail = "affeng@163.com";
        public static string ReturnURL = "https://www.zuchefw.com/Order/PayResult";
        public static string NotifyURL = "https://www.zuchefw.com/Order/PayNotify";
        public static string ShowURL = "http://www.zuchefw.com/Info/Car?detailCarType=";
    }
}