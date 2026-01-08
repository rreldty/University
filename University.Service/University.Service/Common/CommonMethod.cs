using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading;
using System.Net.Http;
using System.IO;

using University.Dao.Base;

namespace University.Service.Common
{
    public static class CommonMethod
    {
        public static void SetCultureInfo()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = Config.DateFormat;

            CultureInfo ci = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = Config.DateFormat;
            Thread.CurrentThread.CurrentCulture = ci;
        }
    }
}