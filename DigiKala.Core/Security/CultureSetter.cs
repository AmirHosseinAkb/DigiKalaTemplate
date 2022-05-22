using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Security
{
    public class CultureSetter
    {
        public static void SetCulture(string cultureName)
        {
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                var cultureInfo = new System.Globalization.CultureInfo(cultureName);
                System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
                System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
        }
    }
}
