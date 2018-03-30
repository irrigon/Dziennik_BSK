using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dziennik_BSK.Extensions
{
    public static class Extension
    {
        public static int ToInt32(this string str) {
            return Int32.Parse(str);
        }
    }
}
