using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace CarHireV2.Models
{
    public static class CommonHelpers
    {
        private static readonly X509Certificate2 Certificate = new X509Certificate2(
            HttpContext.Current.Server.MapPath(@"/Certificate/CarHire.pfx"),
            "ilovewindows8*",
            X509KeyStorageFlags.Exportable);

        public static string RSAEncrypt(string input)
        {
            var encryptProvider = new RSACryptoServiceProvider();
            var xmlPublic = Certificate.PublicKey.Key.ToXmlString(false);
            encryptProvider.FromXmlString(xmlPublic);
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var bufferSize = (encryptProvider.KeySize / 8) - 11;
            var buffer = new byte[bufferSize];
            var inputStream = new MemoryStream(inputBytes);
            var outputStream = new MemoryStream();
            while (true)
            {
                var readOnce = inputStream.Read(buffer, 0, bufferSize);
                if (readOnce <= 0) break;
                var temp = new byte[readOnce];
                Array.Copy(buffer, 0, temp, 0, readOnce);
                var tempEncrypted = encryptProvider.Encrypt(temp, false);
                outputStream.Write(tempEncrypted, 0, tempEncrypted.Length);
            }
            inputStream.Dispose();
            var output = outputStream.ToArray();
            outputStream.Dispose();
            encryptProvider.Dispose();
            return Convert.ToBase64String(output);
        }

        public static string RSADecrypt(string input)
        {
            var decryptProvider = new RSACryptoServiceProvider();
            var xmlPrivate = Certificate.PrivateKey.ToXmlString(true);
            decryptProvider.FromXmlString(xmlPrivate);
            var inputBytes = Convert.FromBase64String(input);
            var bufferSize = decryptProvider.KeySize / 8;
            var buffer = new byte[bufferSize];
            var inputStream = new MemoryStream(inputBytes);
            var outputStream = new MemoryStream();
            while (true)
            {
                var readOnce = inputStream.Read(buffer, 0, bufferSize);
                if (readOnce <= 0) break;
                var temp = new byte[readOnce];
                Array.Copy(buffer, 0, temp, 0, readOnce);
                var tempEncrypted = decryptProvider.Decrypt(temp, false);
                outputStream.Write(tempEncrypted, 0, tempEncrypted.Length);
            }
            inputStream.Dispose();
            var output = outputStream.ToArray();
            outputStream.Dispose();
            decryptProvider.Dispose();
            return Encoding.UTF8.GetString(output);
        }

        public static string AlipaySign(string input)
        {
            var outputBuilder = new StringBuilder(32);
            var toSign = input + AlipayParams.Key;
            var md5Provider = new MD5CryptoServiceProvider();
            var md5 = md5Provider.ComputeHash(Encoding.UTF8.GetBytes(toSign));
            foreach (var md5Byte in md5)
            {
                outputBuilder.Append(md5Byte.ToString("x").PadLeft(2, '0'));
            }
            return outputBuilder.ToString();
        }
        public static List<Car> RandomCarList(List<Car> list, int? bannedID, int count)
        {
            var randomList = new List<Car>(count);
            if (list.Count <= count)
            {
                randomList = list;
                randomList.RemoveAll(car => car.ID == bannedID);
                return randomList;
            }
            var random = new Random();
            var randomNumbers = new int[count];
            for (var index = 0; index < count; index++)
            {
                var nowNumber = random.Next(list.Count);
                while (randomNumbers.Contains(nowNumber) || nowNumber == bannedID)
                {
                    nowNumber = random.Next(list.Count);
                }
                randomNumbers[index] = nowNumber;
                randomList.Add(list[nowNumber]);
            }
            return randomList;
        }

        public static List<Comment> RandomCommentList(List<Comment> list, int count)
        {
            var randomList = new List<Comment>(count);
            if (list.Count <= count)
            {
                randomList = list;
                return randomList;
            }
            var random = new Random();
            var randomNumbers = new int[count];
            for (var index = 0; index < count; index++)
            {
                var nowNumber = random.Next(list.Count);
                while (randomNumbers.Contains(nowNumber))
                {
                    nowNumber = random.Next(list.Count);
                }
                randomNumbers[index] = nowNumber;
                randomList.Add(list[nowNumber]);
            }
            return randomList;
        }

        public static string GetDate(DateTime input)
        {
            const string dateFormat = "yyyy年MM月dd日";
            return input.ToString(dateFormat);
        }

        public static string GetTime(DateTime input)
        {
            const string timeFormat = "HH:mm";
            return input.ToString(timeFormat);
        }

        public static DateTime ParseDateTime(string date, string time)
        {
            var timeRegEX = new Regex(@"^([01]\d|[2][0-3]|\d)(\D+)([0-5]\d)$");
            time = timeRegEX.Replace(time, "$1:$3");
            return DateTime.Parse(date + " " + time);
        }

        public static string ConnectParamsToURL(SortedDictionary<string, string> paramsDictionary)
        {
            var paramStrings = paramsDictionary.Select(singleParam => singleParam.Key + "=" + singleParam.Value);
            return string.Join("&", paramStrings);
        }

        public static bool CheckFromAlipay(Dictionary<string,string> alipayResults)
        {
            var checkSign =
                new SortedDictionary<string, string>(alipayResults);
            var signType = alipayResults["sign_type"];
            var sign = alipayResults["sign"];
            checkSign.Remove("sign");
            checkSign.Remove("sign_type");
            var checkSignString = ConnectParamsToURL(checkSign);
            var notifyID = alipayResults["notify_id"];
            var tradeStatus = alipayResults["trade_status"];
            string verifyResult;
            using (var verifyClient = new HttpClient())
            {
                verifyResult =
                    verifyClient.GetStringAsync(AlipayParams.Gateway + "service=notify_verify&partner=" +
                                                AlipayParams.PartnerID + "&notify_id=" + notifyID).Result;
            }
            return (signType.ToUpper() == AlipayParams.SignType) &&
                   (AlipaySign(checkSignString) == sign) &&
                   (tradeStatus == "WAIT_SELLER_SEND_GOODS" ||
                    tradeStatus == "WAIT_BUYER_CONFIRM_GOODS" ||
                    tradeStatus == "TRADE_SUCCESS" ||
                    tradeStatus == "TRADE_FINISHED") &&
                   (verifyResult == "true");
        }
    }
}