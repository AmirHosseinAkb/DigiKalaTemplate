using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Security
{
    public static class EmailValidator
    {
        public static bool IsEmail(this string value)
        {
            try
            {
                var email = new System.Net.Mail.MailAddress(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
