using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Security.Cryptography;

using University.Dao.Zystem;
using University.Dto.Zystem;
using MISStandarized.Cryptography;

namespace University.Dao.Base
{
    public class BaseMethod
    {
        #region Enumeration

        public enum FormatType
        {
            Amount,
            Factor,
            Percent,
            Price,
            Rate,
            Quantity,
            Unit
        }

        #endregion

        #region DateTime Convertion

        public static decimal DateToNumeric(DateTime _prmDate)
        {
            string _day = String.Empty, _month = String.Empty, _year = String.Empty;
            string _strDate = String.Empty;

            _day = _prmDate.ToString("dd").Trim();
            _month = _prmDate.ToString("MM").Trim();
            _year = _prmDate.ToString("yyyy").Trim();

            _strDate = _year + _month + _day;

            return Convert.ToDecimal(_strDate);
        }

        public static string NumericToDateString(decimal _prmDate)
        {
            if (_prmDate != 0)
            {
                string strDate = Convert.ToString(_prmDate);

                int iYear = Convert.ToInt32(strDate.Substring(0, 4));
                int iMonth = Convert.ToInt32(strDate.Substring(4, 2));
                int iDay = Convert.ToInt32(strDate.Substring(6, 2));

                DateTime _date;

                _date = new DateTime(iYear, iMonth, iDay);

                return _date.ToString("dd-MM-yyyy").Trim();
            }
            else
            {
                return String.Empty;
            }
        }

        public static DateTime NumericToDate(decimal _prmDate)
        {
            if (_prmDate != 0)
            {
                string strDate = Convert.ToString(_prmDate);

                int iYear = Convert.ToInt32(strDate.Substring(0, 4));
                int iMonth = Convert.ToInt32(strDate.Substring(4, 2));
                int iDay = Convert.ToInt32(strDate.Substring(6, 2));

                DateTime _date;

                _date = new DateTime(iYear, iMonth, iDay);

                return _date;
            }
            else
            {
                return DateTime.Today;
            }
        }

        public static decimal TimeToNumeric(DateTime _prmDate)
        {
            string _hour = String.Empty, _minute = String.Empty, _second = String.Empty;
            string _strTime = String.Empty;

            _hour = _prmDate.ToString("HH").Trim();
            _minute = _prmDate.ToString("mm").Trim();
            _second = _prmDate.ToString("ss").Trim();

            _strTime = _hour + _minute + _second;

            return Convert.ToDecimal(_strTime);
        }

        public static string NumericToTimeString(decimal _prmTime)
        {
            string strTime = string.Empty;

            strTime = "000000" + _prmTime.ToString();
            strTime = strTime.Substring(strTime.Length - 6, 6);

            string strHour = strTime.Substring(0, 2);
            string strMinute = strTime.Substring(2, 2);
            string strSecond = strTime.Substring(4, 2);

            strTime = strHour + ":" + strMinute + ":" + strSecond;

            return strTime;
        }

        public static string NumericToDateTimeString(decimal _prmDate)
        {
            if (_prmDate != 0)
            {
                string strDate = Convert.ToString(_prmDate);

                int iYear = Convert.ToInt32(strDate.Substring(0, 4));
                int iMonth = Convert.ToInt32(strDate.Substring(4, 2));
                int iDay = Convert.ToInt32(strDate.Substring(6, 2));
                int iHour = Convert.ToInt32(strDate.Substring(8, 2));
                int iMinute = Convert.ToInt32(strDate.Substring(10, 2));
                int iSecond = Convert.ToInt32(strDate.Substring(12, 2));

                DateTime _date;

                _date = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);

                return _date.ToString("dd-MM-yyyy HH:mm:ss").Trim();
            }
            else
            {
                return String.Empty;
            }
        }

        public static string DecimalToTimeString(decimal _prmTime)
        {
            string strTime = string.Empty;

            if (_prmTime != 0)
            {
                strTime = "000000" + _prmTime.ToString();
                strTime = strTime.Substring(strTime.Length - 6, 6);

                string strHour = strTime.Substring(0, 2);
                string strMinute = strTime.Substring(2, 2);

                strTime = strHour + ":" + strMinute;
            }

            return strTime;
        }

        #endregion

        #region Others
        public static bool IsNumeric(string _val)
        {
            decimal _dec = 0;

            return decimal.TryParse(_val, out _dec);
        }

        public static string EncryptText(string _text)
        {
            Encryption enc = new Encryption();

            return enc.UrlEncryption(_text);
        }

        public static string DecryptText(string _text)
        {
            Encryption enc = new Encryption();

            return enc.UrlDecryption(_text);
        }

        public static string SendMail(string _msgTo, string _msgFrom, string _msgSbj, string _msgBody, string _msgCC)
        {
            MailMessage _mailMsg = new MailMessage();
            string _smtpServer = Config.SMTPServer.Trim();
            int _smtpPort = 25;

            if (Config.SMTPPort.Trim() != String.Empty && IsNumeric(Config.SMTPPort.Trim()))
                _smtpPort = Convert.ToInt32(Config.SMTPPort.Trim());

            try
            {
                _mailMsg.To.Add(new MailAddress(_msgTo.Trim()));
                _mailMsg.From = new MailAddress(_msgFrom.Trim());
                _mailMsg.Subject = _msgSbj;
                _mailMsg.Body = _msgBody;
                _mailMsg.IsBodyHtml = Config.MailHtml;
                _mailMsg.Priority = MailPriority.High;

                if (_msgCC != null)
                {
                    if (_msgCC.Trim() != String.Empty)
                    {
                        string[] _cc = _msgCC.Split(new char[] { ';' });

                        foreach (string _thisCC in _cc)
                        {
                            _mailMsg.CC.Add(new MailAddress(_thisCC.Trim()));
                        }
                    }
                }

                SmtpClient _smtp = new SmtpClient(_smtpServer, _smtpPort);
                _smtp.UseDefaultCredentials = true;
                _smtp.Send(_mailMsg);

                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Variable

        public static string GetVariableValue(string strZRVANO)
        {
            string strZRVAVL = string.Empty;

            ZVARDao dao = new ZVARDao();
            ZVARDto objInfo = new ZVARDto();
            objInfo.ZRCONO = string.Empty;
            objInfo.ZRBRNO = string.Empty;
            objInfo.ZRVANO = strZRVANO;

            ZVARDto obj = dao.Get(objInfo);

            if (obj != null)
                strZRVAVL = obj.ZRVAVL;

            return strZRVAVL;
        }

        public static string GetVariableValue(string strZRCONO, string strZRBRNO, string strZRVANO)
        {
            string strZRVAVL = string.Empty;

            ZVARDao dao = new ZVARDao();
            ZVARDto objInfo = new ZVARDto();
            objInfo.ZRCONO = strZRCONO;
            objInfo.ZRBRNO = strZRBRNO;
            objInfo.ZRVANO = strZRVANO;

            ZVARDto obj = dao.Get(objInfo);

            if (obj != null)
                strZRVAVL = obj.ZRVAVL;

            return strZRVAVL;
        }

        public static string GetVariableName(string strZRVANO)
        {
            string strZRVANA = string.Empty;

            ZVARDao dao = new ZVARDao();
            ZVARDto objInfo = new ZVARDto();
            objInfo.ZRCONO = string.Empty;
            objInfo.ZRBRNO = string.Empty;
            objInfo.ZRVANO = strZRVANO;

            ZVARDto obj = dao.Get(objInfo);

            if (obj != null)
                strZRVANA = obj.ZRVANA;

            return strZRVANA;
        }

        public static string GetVariableName(string strZRVATY, string strZRVAVL)
        {
            string strZRVANA = string.Empty;

            ZVARDao dao = new ZVARDao();
            ZVARDto objInfo = new ZVARDto();
            objInfo.ZRCONO = string.Empty;
            objInfo.ZRBRNO = string.Empty;
            objInfo.ZRVATY = strZRVATY;
            objInfo.ZRVAVL = strZRVAVL;

            ZVARDto obj = dao.Get(objInfo);

            if (obj != null)
                strZRVANA = obj.ZRVANA;

            return strZRVANA;
        }

        #endregion

        #region Warehouse Data

        //public static decimal GetWarehouseDate(string strCONO, string strBRNO, string strWHNO = "")
        //{
        //    IWHSDao dao = new IWHSDao();
        //    return dao.GetDate(strCONO, strBRNO, strWHNO, false);
        //}

        //public static decimal GetPrincipalDate()
        //{
        //    IWHSDao dao = new IWHSDao();
        //    return dao.GetPrincipalDate();
        //}

        //public static string CheckPeriod(decimal decDate, string strCONO, string strBRNO, string strWHNO)
        //{
        //    string strResult = string.Empty;
        //    decimal strPeriod = BaseMethod.GetWarehouseDate(strCONO, strBRNO, strWHNO);

        //    if (strPeriod != 0)
        //    {
        //        if (strPeriod > decDate)
        //        {
        //            strResult = "Invalid Date. Period is Closed";
        //        }
        //    }

        //    return strResult;
        //}

        #endregion

        #region Server Date Base on Database Date

        public static string GetServerDateTime()
        {
            string strDateTime = "00000000000000";
            string strSql = "SELECT CAST(CAST(CONVERT(VARCHAR, GETDATE(), 112) AS NUMERIC(20,0)) * 1000000 + CAST(REPLACE(CONVERT(VARCHAR, GETDATE(), 108), ':', '') AS NUMERIC(6,0)) AS VARCHAR) AS SSDT";

            QueryDao dao = new QueryDao();
            object _obj = dao.ExecuteScalar(strSql);

            if (_obj != null)
            {
                try
                {
                    strDateTime = _obj.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return strDateTime;
        }

        #endregion

        #region Application Settings

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

        #endregion

        #region Decimal Length

        public static int DecimalLength(FormatType NumericFormat)
        {
            int intLength = 0;

            switch (NumericFormat)
            {
                case FormatType.Amount:
                    intLength = AmountDecimalLength;
                    break;
                case FormatType.Factor:
                    intLength = FactorDecimalLength;
                    break;
                case FormatType.Percent:
                    intLength = PercentDecimalLength;
                    break;
                case FormatType.Price:
                    intLength = PriceDecimalLength;
                    break;
                case FormatType.Quantity:
                    intLength = QuantityDecimalLength;
                    break;
                case FormatType.Rate:
                    intLength = RateDecimalLength;
                    break;
                case FormatType.Unit:
                    intLength = UnitDecimalLength;
                    break;
                default:
                    intLength = AmountDecimalLength;
                    break;
            }

            return intLength;
        }

        public static string AmountFormat
        {
            get
            {
                string strFormat = "#,##0";

                if (DecimalLength(FormatType.Amount) > 0)
                {
                    strFormat += ".";

                    int i = 0;

                    for (i = 0; i < DecimalLength(FormatType.Amount); i++)
                    {
                        strFormat += "0";
                    }
                }

                return strFormat;
            }
        }

        public static string FactorFormat
        {
            get
            {
                string strFormat = "#,##0";

                if (DecimalLength(FormatType.Factor) > 0)
                {
                    strFormat += ".";

                    int i = 0;

                    for (i = 0; i < DecimalLength(FormatType.Factor); i++)
                    {
                        strFormat += "0";
                    }
                }

                return strFormat;
            }
        }

        public static string PercentFormat
        {
            get
            {
                string strFormat = "#,##0";

                if (DecimalLength(FormatType.Percent) > 0)
                {
                    strFormat += ".";

                    int i = 0;

                    for (i = 0; i < DecimalLength(FormatType.Percent); i++)
                    {
                        strFormat += "0";
                    }
                }

                return strFormat;
            }
        }

        public static string PriceFormat
        {
            get
            {
                string strFormat = "#,##0";

                if (DecimalLength(FormatType.Price) > 0)
                {
                    strFormat += ".";

                    int i = 0;

                    for (i = 0; i < DecimalLength(FormatType.Price); i++)
                    {
                        strFormat += "0";
                    }
                }

                return strFormat;
            }
        }

        public static string RateFormat
        {
            get
            {
                string strFormat = "#,##0";

                if (DecimalLength(FormatType.Rate) > 0)
                {
                    strFormat += ".";

                    int i = 0;

                    for (i = 0; i < DecimalLength(FormatType.Rate); i++)
                    {
                        strFormat += "0";
                    }
                }

                return strFormat;
            }
        }

        public static string QuantityFormat
        {
            get
            {
                string strFormat = "#,##0";

                if (DecimalLength(FormatType.Quantity) > 0)
                {
                    strFormat += ".";

                    int i = 0;

                    for (i = 0; i < DecimalLength(FormatType.Quantity); i++)
                    {
                        strFormat += "0";
                    }
                }

                return strFormat;
            }
        }

        public static string UnitFormat
        {
            get
            {
                string strFormat = "#,##0";

                if (DecimalLength(FormatType.Unit) > 0)
                {
                    strFormat += ".";

                    int i = 0;

                    for (i = 0; i < DecimalLength(FormatType.Unit); i++)
                    {
                        strFormat += "0";
                    }
                }

                return strFormat;
            }
        }

        #endregion

        #region Properties

        public static string DocStatusDraft
        {
            get { return "D"; }
            //get { return GetVariableValue("DCST_DRAFT"); }
        }

        public static string DocStatusOpen
        {
            get { return "O"; }
            //get { return GetVariableValue("DCST_OPEN"); }
        }

        public static string DocStatusFinish
        {
            get { return "F"; }
            //get { return GetVariableValue("DCST_FINISH"); }
        }

        public static string DocStatusClose
        {
            get { return "C"; }
            //get { return GetVariableValue("DCST_CLOSE"); }
        }

        public static string DocStatusCancel
        {
            get { return "E"; }
            //get { return GetVariableValue("DCST_CANCEL"); }
        }

        public static string MaskFormat
        {
            get { return "0.000.0000"; }
            //get { return GetVariableValue("MASK_FORMAT"); }
        }

        public static string MaskSeparator
        {
            get { return "."; }
            //get { return GetVariableValue("MASK_SEPARATOR"); }
        }

        public static string UMNO_1
        {
            get { return "1"; }
            //get { return GetVariableValue("UMNO_1"); }
        }

        public static string UMNO_2
        {
            get { return "2"; }
            //get { return GetVariableValue("UMNO_2"); }
        }

        public static string UMNO_3
        {
            get { return "3"; }
            //get { return GetVariableValue("UMNO_3"); }
        }

        public static string StatDraft
        {
            get { return "10"; }
            //get { return GetVariableValue("STAT_DRAFT"); }
        }
        public static string StatASMEN
        {
            get { return "20"; }
            //get { return GetVariableValue("STAT_DRAFT"); }
        }
        public static string StatASM
        {
            get { return "30"; }
            //get { return GetVariableValue("STAT_DRAFT"); }
        }
        public static string StatRSM
        {
            get { return "40"; }
            //get { return GetVariableValue("STAT_DRAFT"); }
        }
        public static string StatReject
        {
            get { return "99"; }
            //get { return GetVariableValue("STAT_DRAFT"); }
        }
        public static string StatRevise
        {
            get { return "50"; }
            //get { return GetVariableValue("STAT_DRAFT"); }
        }
        public static string AVSTApprove
        {
            get { return "AV"; }
        }
        public static string AVSTWaitApprove
        {
            get { return "WA"; }
        }
        public static string AVSTReject
        {
            get { return "RJ"; }
        }

        public static string AVSTRevise
        {
            get { return "RV"; }
        }

        public static string SystReady
        {
            get { return "RD"; }
            //get { return GetVariableValue("SYST_READY"); }
        }

        public static decimal RecordStatusActive
        {
            get { return 1; }
            //get { return Convert.ToDecimal(GetVariableValue("RCST_ACTIVE")); }
        }

        public static decimal RecordStatusInactive
        {
            get { return 0; }
            //get { return Convert.ToDecimal(GetVariableValue("RCST_ACTIVE")); }
        }

        #endregion


        #region Login
        public static string SuperAdminId
        {
            get
            {
                return "Morat";
            }
        }

        public static string SuperAdminPwd
        {
            get
            {
                return "M15@dmin";
            }
        }

        public static string SysadminId
        {
            get
            {
                return "sysadmin";
            }
        }
        #endregion
    }
}
