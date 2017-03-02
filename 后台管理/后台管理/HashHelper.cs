using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 后台管理
{
    public static class HashHelper
    {
        public static string HashFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5Hash = md5.ComputeHash(file);
            file.Close();
            StringBuilder md5String = new StringBuilder();
            for (int index = 0; index < md5Hash.Length; index++)
            {
                md5String.Append(md5Hash[index].ToString("x2"));
            }
            return md5String.ToString();
        }
    }
}
