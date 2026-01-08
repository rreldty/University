using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections;
using System.Data;
using University.Dto.Base;
using System.IO;
using System.Net.Http.Headers;

namespace University.Api.Common
{
    public class BaseApiController<T> : ApiController
    {
        #region DataTable
        public HttpResponseMessage CreateLookupHttpResponse(string strResult, DataTable dttResult, int intTotalPage, int intTotalRecord, string strDecimalColumn, string strColumnWidth, string strWindowSize)
        {
            if (string.IsNullOrEmpty(strResult))
            {
                LookupDto objLookup = null;

                if (dttResult != null)
                {
                    List<LookupHeaderDto> lstHeader = new List<LookupHeaderDto>();
                    for (int a = 0; a < dttResult.Columns.Count; a++)
                    {
                        string strMatch = a.ToString() + "-";
                        string strFormat = "";
                        string strWidth = "";

                        if (!string.IsNullOrEmpty(strDecimalColumn))
                        {
                            List<string> lstDecimalColumn = strDecimalColumn.Split(';').ToList();
                            strFormat = lstDecimalColumn.Find(s => s.StartsWith(strMatch)) ?? "";
                            strFormat = strFormat.Replace(strMatch, "");
                        }

                        if (!string.IsNullOrEmpty(strColumnWidth))
                        {
                            List<string> lstColumnWidth = strColumnWidth.Split(';').ToList();
                            strWidth = lstColumnWidth.Find(s => s.StartsWith(strMatch)) ?? "";
                            strWidth = strWidth.Replace(strMatch, "");
                        }

                        LookupHeaderDto objHeader = new LookupHeaderDto();
                        objHeader.HeaderName = dttResult.Columns[a].ColumnName;
                        objHeader.HeaderType = dttResult.Columns[a].DataType.ToString().Replace("System.", "");
                        objHeader.HeaderFormat = strFormat;
                        objHeader.HeaderWidth = strWidth;

                        lstHeader.Add(objHeader);
                    }
                    objLookup = new LookupDto();
                    objLookup.TotalPage = intTotalPage;
                    objLookup.TotalRecord = intTotalRecord;
                    objLookup.Headers = lstHeader;
                    objLookup.Rows = dttResult;
                    objLookup.WindowSize = strWindowSize;
                }

                return Request.CreateResponse(HttpStatusCode.OK, objLookup);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, strResult);
            }
        }

        public HttpResponseMessage CreateTableHttpResponse(string strResult, DataTable dttResult, int intTotalPage, int intTotalRecord)
        {
            if (dttResult != null && dttResult.Rows.Count > 0)
            {
                string strColumnName = string.Empty;
                string strDecimalColumn = string.Empty;

                foreach (DataColumn dtc in dttResult.Columns)
                {
                    if (!string.IsNullOrEmpty(strColumnName))
                    {
                        strColumnName += ";";
                    }

                    strColumnName += dtc.ColumnName;

                    if (dtc.DataType == Type.GetType("System.Decimal") || dtc.DataType == Type.GetType("System.Int32"))
                    {
                        if (!string.IsNullOrEmpty(strDecimalColumn))
                        {
                            strDecimalColumn += ";";
                        }

                        string strColumnSum = dttResult.Compute("SUM([" + dtc.ColumnName + "])","").ToString();
                        strDecimalColumn += dtc.ColumnName + "|" + strColumnSum;

                    }
                }

                dttResult.TableName = "tblResult";

                dttResult.Columns.Add(new DataColumn("TotalPage", typeof(int)));
                dttResult.Columns.Add(new DataColumn("TotalRecord", typeof(int)));
                dttResult.Columns.Add(new DataColumn("DecimalColumn", typeof(string)));
                dttResult.Columns.Add(new DataColumn("ColumnName", typeof(string)));

                dttResult.Rows[0]["TotalPage"] = intTotalPage;
                dttResult.Rows[0]["TotalRecord"] = intTotalRecord;                
                dttResult.Rows[0]["DecimalColumn"] = strDecimalColumn;
                dttResult.Rows[0]["ColumnName"] = strColumnName;

                dttResult.AcceptChanges();
            }

            if (string.IsNullOrEmpty(strResult))
            {
                return Request.CreateResponse(HttpStatusCode.OK, dttResult);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, strResult);
            }
        }
        #endregion

        #region Others
        public HttpResponseMessage CreateHttpResponse(string strResult, List<T> lstResult, int intTotalPage, int intTotalRecord)
        {
            if (lstResult != null && lstResult.Count > 0)
            {
                T objT = lstResult[0];
                SetPropertyValue(objT, "TotalPage", intTotalPage);
                SetPropertyValue(objT, "TotalRecord", intTotalRecord);
                SetPropertyValue(objT, "DecimalColumn", "");
                SetPropertyValue(objT, "ColumnName", "");
            }

            if (string.IsNullOrEmpty(strResult))
            {
                return Request.CreateResponse(HttpStatusCode.OK, lstResult);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, strResult);
            }
        }

        public HttpResponseMessage CreateHttpResponse(string strResult, object objResult)
        {
            if (string.IsNullOrEmpty(strResult))
            {
                if (IsDataTable(objResult))
                {
                    DataTable dtResult = objResult as DataTable;
                    return CreateTableHttpResponse(strResult, dtResult, 0, 0);
                }

                return Request.CreateResponse(HttpStatusCode.OK, objResult);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, strResult);
            }
        }

        public HttpResponseMessage CreateHttpResponse(string strResult)
        {
            if (string.IsNullOrEmpty(strResult))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, strResult);
            }
        }

        //public HttpResponseMessage CreateFileHttpResponse(string strResult, string strFileName, MemoryStream objStream)
        //{
        //    if (string.IsNullOrEmpty(strResult))
        //    {
        //        var result = new HttpResponseMessage(HttpStatusCode.OK)
        //        {
        //            Content = new ByteArrayContent(objStream.ToArray())
        //        };

        //        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = strFileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx" };

        //        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        //        return result;
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, strResult);
        //    }
        //}

        //public HttpResponseMessage CreateHttpResponse(string strResult, List<T> lstResult)
        //{
        //    if (string.IsNullOrEmpty(strResult))
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, lstResult);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, strResult);
        //    }
        //}

        //public HttpResponseMessage CreateHttpResponse(string strResult, T objResult)
        //{
        //    if (string.IsNullOrEmpty(strResult))
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, objResult);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, strResult);
        //    }
        //}

        #endregion

        #region Methods
        void SetPropertyValue(object obj, string strPropertyName, object objValue)
        {
            Type t = obj.GetType();
            System.Reflection.PropertyInfo prInfo = t.GetProperty(strPropertyName);

            if ((prInfo != null) && prInfo.CanWrite)
            {
                prInfo.SetValue(obj, objValue, null);
            }
        }

        bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        bool IsDictionary(object o)
        {
            if (o == null) return false;
            return o is IDictionary &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }

        bool IsDataTable(object o)
        {
            if (o == null) return false;
            return o is DataTable;
        }
        #endregion
    }
}