using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zervo.Extensions
{
    public static class UtilityExtensions
    {
        public static int TryParseInt(this string text)
        {
            int value;
            if (int.TryParse(text, out value))
            {
                return value;
            }
            return 0;
        }

    }
}
