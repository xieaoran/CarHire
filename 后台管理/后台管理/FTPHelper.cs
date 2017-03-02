using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using System.Text;
using System.Threading.Tasks;

namespace 后台管理
{
    public static class FTPHelper
    {
        private static readonly FtpClient FTPClient = new FtpClient
        {
            Credentials = new NetworkCredential("admin", "ilovewindows8*"),
            DataConnectionType = FtpDataConnectionType.AutoPassive,
            Encoding = Encoding.UTF8,
            Host = "www.zuchefw.com",
            Port = 233,
            EncryptionMode = FtpEncryptionMode.Explicit
        };

        public static void Connect()
        {
            FTPClient.Connect();
        }

        public static void AddFile(string fileName, Stream localFileStream)
        {
            try
            {
                if (FTPClient.IsConnected == false) FTPClient.Connect();
                if (FTPClient.FileExists(fileName)) return;
                var ftpStream = FTPClient.OpenWrite(fileName, FtpDataType.Binary);
                localFileStream.CopyTo(ftpStream);
                localFileStream.Close();
                ftpStream.Close();
            }
            catch (Exception)
            {
                return;
            }
        }

        public static void DeleteFile(string fileName)
        {
            if (FTPClient.IsConnected == false) FTPClient.Connect();
            if (FTPClient.FileExists(fileName)) FTPClient.DeleteFile(fileName);
        }
    }
}
