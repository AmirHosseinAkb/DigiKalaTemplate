using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Generators
{
    public class NameGenerator
    {
        public static string GenerateUniqName()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
