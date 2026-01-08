using MISStandarized.Cryptography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;

namespace University.Dao.Base
{
    public class Config
    {
        //Remove any singleton instance
        public static readonly Config Instance = new Config();

        #region "Constructor"

        public Config()
        {
        }

        #endregion

        #region "Public Method"

        #region University


        private static ConnectionStringSettings UniversityConnectionSettings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["UniversityConnection"];
            }
        }

        public static string UniversityConnString
        {
            get
            {
                return UniversityConnectionSettings.ConnectionString.Trim();
            }
        }

        public static string UniversityProvider
        {
            get
            {
                return UniversityConnectionSettings.ProviderName.ToString().Trim();
            }
        }

        public static string UniversityProviderType
        {
            get
            {
                return ConfigurationManager.AppSettings["UniversityProviderType"].ToString().Trim();
            }
        }

        #endregion


        #region SSAS
        public static string SSASConnString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SSASConnection"].ConnectionString.ToString().Trim();
            }
        }
        #endregion


        #region SYNU

        private static string SYNUPrivateKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SYNUPrivateKey"].ToString().Trim();
            }
        }

        private static ConnectionStringSettings SYNUConnectionSettings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SYNUConnection"];
            }
        }

        public static string SYNUConnString
        {
            get
            {
                Encryption encObj = new Encryption();
                string conn = encObj.RsaDynamicDecryption(SYNUConnectionSettings.ConnectionString.ToString().Trim(), SYNUPrivateKey, Encryption.DynamicEncrypt.Symmetric);
                return conn;
            }
        }

        public static string SYNUProvider
        {
            get
            {
                return SYNUConnectionSettings.ProviderName.ToString().Trim();
            }
        }

        public static string SYNUProviderType
        {
            get
            {
                return ConfigurationManager.AppSettings["SYNUProviderType"].ToString().Trim();
            }
        }

        #endregion


        #region SYND

        private static string SYNDPrivateKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SYNDPrivateKey"].ToString().Trim();
            }
        }

        private static ConnectionStringSettings SYNDConnectionSettings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SYNDConnection"];
            }
        }

        public static string SYNDConnString
        {
            get
            {
                Encryption encObj = new Encryption();
                string conn = encObj.RsaDynamicDecryption(SYNDConnectionSettings.ConnectionString.ToString().Trim(), SYNDPrivateKey, Encryption.DynamicEncrypt.Symmetric);
                return conn;
            }
        }

        public static string SYNDProvider
        {
            get
            {
                return SYNDConnectionSettings.ProviderName.ToString().Trim();
            }
        }

        public static string SYNDProviderType
        {
            get
            {
                return ConfigurationManager.AppSettings["SYNDProviderType"].ToString().Trim();
            }
        }

        #endregion

        public static string ApplicationStatus
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationStatus"].ToString().Trim();
            }
        }

        public static string DateFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["DateFormat"].ToString().Trim();
            }
        }

        public static int AmountDecimalLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["AmountDecimalLength"].ToString().Trim());
            }
        }

        public static int FactorDecimalLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["FactorDecimalLength"].ToString().Trim());
            }
        }

        public static int PercentDecimalLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["PercentDecimalLength"].ToString().Trim());
            }
        }

        public static int PriceDecimalLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["PriceDecimalLength"].ToString().Trim());
            }
        }

        public static int RateDecimalLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["RateDecimalLength"].ToString().Trim());
            }
        }

        public static int QuantityDecimalLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["QuantityDecimalLength"].ToString().Trim());
            }
        }

        public static int UnitDecimalLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["UnitDecimalLength"].ToString().Trim());
            }
        }

        public static string SendGridApiId
        {
            get
            {
                return ConfigurationManager.AppSettings["SendGridApiId"].ToString().Trim();
            }
        }

        public static string SendGridApiSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["SendGridApiSecret"].ToString().Trim();
            }
        }

        public static string EmailFrom
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailFrom"].ToString().Trim();
            }
        }
        public static string GlobalDateFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["GlobalDateFormat"].ToString().Trim();
            }
        }

        public static string SQLDateFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["SQLDateFormat"].ToString().Trim();
            }
        }

        public static string SMTPServer
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTPServer"].ToString().Trim();
            }
        }

        public static string SMTPPort
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTPPort"].ToString().Trim();
            }
        }

        public static bool MailHtml
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["MailHtml"].ToString().Trim());
            }
        }

        #endregion
    }
}
