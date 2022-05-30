using System;
using System.Linq;

namespace TemplateDomain.Common
{
    public static class IdUtils
    {
        public static long ToInt64(string id) => Convert.ToInt64(id.Split('-').Last());
    }
}
