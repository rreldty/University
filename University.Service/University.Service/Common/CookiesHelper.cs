using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.Api.Common
{
    public class CookiesHelper
    {
        public static string USNO
        {
            get { return readGlobalCookie("USNO"); }
            set { saveGlobalCookie("USNO", value); }
        }

        public static string USNA
        {
            get { return readGlobalCookie("USNA"); }
            set { saveGlobalCookie("USNA", value); }
        }

        public static string NICK
        {
            get { return readGlobalCookie("NICK"); }
            set { saveGlobalCookie("NICK", value); }
        }

        public static string USTY
        {
            get { return readGlobalCookie("USTY"); }
            set { saveGlobalCookie("USTY", value); }
        }

        public static string CONO
        {
            get { return readGlobalCookie("CONO"); }
            set { saveGlobalCookie("CONO", value); }
        }

        public static string CONA
        {
            get { return readGlobalCookie("CONA"); }
            set { saveGlobalCookie("CONA", value); }
        }

        public static string BRNO
        {
            get { return readGlobalCookie("BRNO"); }
            set { saveGlobalCookie("BRNO", value); }
        }

        public static string BRNA
        {
            get { return readGlobalCookie("BRNA"); }
            set { saveGlobalCookie("BRNA", value); }
        }

        public static string APNO
        {
            get { return readGlobalCookie("APNO"); }
            set { saveGlobalCookie("APNO", value); }
        }

        public static string APNA
        {
            get { return readGlobalCookie("APNA"); }
            set { saveGlobalCookie("APNA", value); }
        }

        public static string YEAR
        {
            get { return readGlobalCookie("YEAR"); }
            set { saveGlobalCookie("YEAR", value); }
        }

        public static string MONT
        {
            get { return readGlobalCookie("MONT"); }
            set { saveGlobalCookie("MONT", value); }
        }

        public static string WEEK
        {
            get { return readGlobalCookie("WEEK"); }
            set { saveGlobalCookie("WEEK", value); }
        }

        #region Methods
        static void saveGlobalCookie(string key, string val)
        {
            //Session
            HttpContext.Current.Session[key] = val;


            //Cookie
            //HttpCookie aglisGlobalInfo = HttpContext.Current.Request.Cookies["aglisGlobalInfo"];

            //if (aglisGlobalInfo == null)
            //    aglisGlobalInfo = new HttpCookie("aglisGlobalInfo");

            //if (aglisGlobalInfo[key] != null)
            //    aglisGlobalInfo[key] = val;
            //else
            //    aglisGlobalInfo.Values.Add(key, val);

            //aglisGlobalInfo.Expires = DateTime.Now.AddDays(1);
            //HttpContext.Current.Response.Cookies.Add(aglisGlobalInfo);
        }

        static string readGlobalCookie(string key)
        {
            //Session
            if (HttpContext.Current.Session[key] != null)
                return HttpContext.Current.Session[key].ToString();
            else
                return string.Empty;

            //Cookie
            //HttpCookie aglisGlobalInfo = HttpContext.Current.Request.Cookies["aglisGlobalInfo"];
            //if (aglisGlobalInfo != null)
            //{
            //    if (aglisGlobalInfo[key] != null)
            //        return aglisGlobalInfo[key].ToString();
            //}

            //return string.Empty;
        }

        #endregion
    }
}