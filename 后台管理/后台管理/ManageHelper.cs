using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 后台管理
{
    public static class ManageHelper
    {
        private static readonly X509Certificate2 Certificate = new X509Certificate2(
            "CarHire.pfx",
            "ilovewindows8*",
            X509KeyStorageFlags.Exportable);

        private static CookieContainer _cookie = new CookieContainer();

        public static void Verify()
        {
            var retriedTimes = 0;
            retry:
            var question = RequestURL("https://www.zuchefw.com/Admin/GetQuestion");
            if (question == "管理客户端已通过验证，无需重复验证") return;
            var answer = RSADecrypt(question);
            var result = RequestURL("https://www.zuchefw.com/Admin/AnswerQuestion?answer=" + answer);
            switch (result)
            {
                case "Succeeded":
                    break;
                default:
                    if (retriedTimes >= 3)
                    {
                        throw new CryptographicException("验证失败次数超限");
                    }
                    retriedTimes++;
                    goto retry;
            }
        }

        public static void RefreshServer()
        {
            var retriedTimes = 0;
            retry:
            var result = RequestURL("https://www.zuchefw.com/Admin/RefreshServer");
            switch (result)
            {
                case "Succeeded":
                    break;
                default:
                    if (retriedTimes >= 3)
                    {
                        throw new IOException("服务器刷新失败");
                    }
                    retriedTimes++;
                    Verify();
                    goto retry;
            }
        }

        public static void ChangeOrderCondition(int id, int condition)
        {
            var retriedTimes = 0;
            retry:
            var result = RequestURL("https://www.zuchefw.com/Admin/ChangeOrderCondition?id=" + id.ToString() + "&condition=" + condition.ToString());
            switch (result)
            {
                case "Succeeded":
                    break;
                default:
                    if (retriedTimes >= 3)
                    {
                        throw new IOException("租车订单状态更改失败");
                    }
                    retriedTimes++;
                    Verify();
                    goto retry;
            }
        }
        public static void ChangePlaneOrderCondition(int id, int condition)
        {
            var retriedTimes = 0;
            retry:
            var result = RequestURL("https://www.zuchefw.com/Admin/ChangePlaneOrderCondition?id=" + id.ToString() + "&condition=" + condition.ToString());
            switch (result)
            {
                case "Succeeded":
                    break;
                default:
                    if (retriedTimes >= 3)
                    {
                        throw new IOException("接机订单状态更改失败");
                    }
                    retriedTimes++;
                    Verify();
                    goto retry;
            }
        }
        private static string RequestURL(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = _cookie;
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var responseReader = new StreamReader(responseStream);
            var result = responseReader.ReadToEnd();
            response.Close();
            responseStream.Close();
            responseReader.Close();
            return result;
        }

        private static string RSADecrypt(string input)
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
    }
}
