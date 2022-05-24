using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Security
{
    public  class PasswordHasher
    {
        public static string HashPasswordMD5(string password)
        {
            Byte[] originallPass;
            Byte[] encodedPass;

            MD5 md5;
            md5=new MD5CryptoServiceProvider();
            originallPass = ASCIIEncoding.Default.GetBytes(password);
            encodedPass = md5.ComputeHash(originallPass);
            return BitConverter.ToString(encodedPass);
        }
    }
}
