using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;

using System.Data;
using MultipartDataMediaFormatter.Infrastructure;
using System.Web.Hosting;
using System.Globalization;

using University.Dao.Base;
using University.Dto.Base;
using University.Dao.Zystem;
using University.Dto.Zystem;

using University.Api.Common;
using University.Dao.Entity;
using Microsoft.PowerBI.Api.Models;
using System.Reflection;
using System.Web.UI;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using University.Service.Common;
using Microsoft.Identity.Client;

namespace University.Service.Controllers.Base
{
    public class BaseController : BaseApiController<LookupDto>
    {
        [Route("api/Base/GetDateTime")]
        public HttpResponseMessage GetDateTime()
        {
            BaseDto objResult = null;
            string strResult = string.Empty;

            try
            {
                CommonMethod.SetCultureInfo();

                objResult = new BaseDto()
                {
                    DTTM = BaseMethod.GetServerDateTime()
                };
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        ////[Authorize]
        [Route("api/Base/ListComboBox")]
        public HttpResponseMessage ListComboBox([FromBody] EntityDto obj)
        {
            List<DDLDto> lst = null;
            string strResult = string.Empty;

            try
            {
                DDLDao dao = new DDLDao();
                lst = dao.GetList(obj.Entity, obj.Filter, obj.KeyCode, obj.FilterMultiselect);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        //[Authorize]
        [Route("api/Base/ListLookup")]
        public HttpResponseMessage ListLookup([FromBody] EntityDto obj)
        {
            string strResult = string.Empty;
            int intTotalPage = 0;
            int intTotalRecord = 0;
            string strWindowSize = "";
            string strDecimalColumn = "";
            string strColumnWidth = "";

            LookUpDao dao = new LookUpDao();
            DataTable dtt = null;

            try
            {
                dtt = dao.GetDataTable(obj, out intTotalPage, out intTotalRecord, out strWindowSize, out strDecimalColumn, out strColumnWidth);

                if (dtt == null)
                {
                    //IF Null then Get Column Only
                    dtt = dao.GetSingleData(obj.Entity, obj.Filter, obj.KeyCode, out strWindowSize, out strDecimalColumn, out strColumnWidth, false);
                    dtt.Rows.Clear();
                    dtt.AcceptChanges();

                    intTotalPage = 0;
                    intTotalRecord = 0;
                }


                if (string.IsNullOrEmpty(strColumnWidth))
                {
                    if (dtt.Columns.Count == 3)
                    {
                        strColumnWidth = "1-*;2-100";
                    }
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateLookupHttpResponse(strResult, dtt, intTotalPage, intTotalRecord, strDecimalColumn, strColumnWidth, strWindowSize);
        }

        //[Authorize]
        [Route("api/Base/ListLookupOne")]
        public HttpResponseMessage ListLookupOne([FromBody] EntityDto obj)
        {
            string strResult = string.Empty;
            int intTotalPage = 1;
            int intTotalRecord = 1;
            string strWindowSize = "";
            string strDecimalColumn = "";
            string strColumnWidth = "";

            LookUpDao dao = new LookUpDao();
            DataTable dtt = null;

            try
            {
                dtt = dao.GetSingleData(obj.Entity, obj.Filter, obj.KeyCode, out strWindowSize, out strDecimalColumn, out strColumnWidth, true);

            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateLookupHttpResponse(strResult, dtt, intTotalPage, intTotalRecord, strDecimalColumn, strColumnWidth, strWindowSize);
        }

        //[Authorize]
        [Route("api/Base/UploadFile")]
        public void UploadFile(FormData formData)
        {
            HttpFile file;
            formData.TryGetValue("file", CultureInfo.CurrentCulture, out file);

            if (file != null)
            {
                string strPath = Path.Combine(HostingEnvironment.MapPath("~/Upload/Images"),file.FileName);
                byte[] buffer = file.Buffer;
                File.WriteAllBytes(strPath, buffer);
            }
        }

        //[Authorize]
        [Route("api/Base/GeneralUpload")]
        public HttpResponseMessage GeneralUpload(FormData formData)
        {
            string strResult = string.Empty;
            DataTable dttSource = null;

            try
            {
                HttpFile file;
                formData.TryGetValue("file", CultureInfo.CurrentCulture, out file);

                if (file != null)
                {
                    //Write Uploaded File
                    string strPath = Path.Combine(HostingEnvironment.MapPath("~/Upload/GeneralUpload"), file.FileName);
                    string strDir = Path.GetDirectoryName(strPath);
                    if (!Directory.Exists(strDir))
                    {
                        Directory.CreateDirectory(strDir);
                    }
                    if (File.Exists(strPath))
                    {
                        File.Delete(strPath);
                    }
                    byte[] buffer = file.Buffer;
                    File.WriteAllBytes(strPath, buffer);

                    //Read File
                    if (File.Exists(strPath))
                    {
                        ExcelPackage excel = new ExcelPackage(new FileInfo(strPath));
                        ExcelWorksheet workSheet = excel.Workbook.Worksheets[1];

                        bool IsTableExist = false;

                        string strTableName = workSheet.Cells["A2"].Value.ToString();

                        UploadDao dao = new UploadDao();
                        IsTableExist = dao.IsTableExists(strTableName);

                        if (!IsTableExist)
                        {
                            throw new Exception("Table " + strTableName + " not found");
                        }

                        if (IsTableExist)
                        {
                            dttSource = new DataTable();

                            int intTotalCol = workSheet.Dimension.End.Column;
                            int intTotalRow = workSheet.Dimension.End.Row;

                            for (var c = 1; c <= intTotalCol; c++)
                            {
                                string strColumnName = workSheet.Cells[3, c].Value.ToString();
                                string strColumnDscr = workSheet.Cells[4, c].Value.ToString();
                                string strColumnType = workSheet.Cells[5, c].Value.ToString();

                                if (strColumnName.Length == 6)
                                {
                                    // Add Column
                                    DataColumn dc = new DataColumn();
                                    dc.ColumnName = strColumnName;
                                    dc.DataType = strColumnType == "NUMERIC" ? Type.GetType("System.Decimal") : Type.GetType("System.String");
                                    dc.Caption = strColumnDscr;

                                    dttSource.Columns.Add(dc);
                                }
                                else 
                                {
                                    throw new Exception("Column " + strColumnName + " not valid");
                                }
                            }

                            DataColumn dc1 = new DataColumn();
                            dc1.ColumnName = "StatusCode";
                            dc1.DataType = Type.GetType("System.String");
                            dttSource.Columns.Add(dc1);

                            DataColumn dc2 = new DataColumn();
                            dc2.ColumnName = "Message";
                            dc2.DataType = Type.GetType("System.String");
                            dttSource.Columns.Add(dc2);

                            dttSource.AcceptChanges();

                            for (var r = 8; r <= intTotalRow; r++)
                            {
                                ExcelRange cellRange = workSheet.Cells[r, 1, r, intTotalCol];
                                bool isRowEmpty = cellRange.All(c => c.Value == null);

                                if (!isRowEmpty) 
                                {
                                    List<string> lstMessage = new List<string>();
                                    DataRow dr = dttSource.NewRow();

                                    for (var c = 1; c <= intTotalCol; c++)
                                    {
                                        string strColumnDscr = (workSheet.Cells[4, c].Value ?? "").ToString();
                                        bool isNumeric = (workSheet.Cells[5, c].Value ?? "").ToString().ToUpper() == "NUMERIC";
                                        bool isKey = (workSheet.Cells[6, c].Value ?? "").ToString().ToUpper() == "P";
                                        bool isMandatory = (workSheet.Cells[7, c].Value ?? "").ToString().ToUpper() == "M";
                                        string strValue = (workSheet.Cells[r, c].Value ?? "").ToString();

                                        if (isNumeric)
                                        {
                                            dr[c - 1] = Convert.ToDecimal(strValue.Trim().Length > 0 ? strValue : "0");
                                        }
                                        else
                                        {
                                            dr[c - 1] = strValue;

                                            if (isMandatory)
                                            {
                                                if (strValue.Trim().Length == 0)
                                                {
                                                    lstMessage.Add("[" + workSheet.Cells[r, c].Address + "] " + strColumnDscr + " is mandatory");
                                                }
                                            }
                                        }
                                    }

                                    dr["Process Status"] = "S";
                                    dr["Process Message"] = "";

                                    string strMessage = String.Join(", ", lstMessage);
                                    if (strMessage.Trim().Length > 0)
                                    {
                                        dr["Process Status"] = "E";
                                        dr["Process Message"] = strMessage;
                                    }

                                    dttSource.Rows.Add(dr);
                                    dttSource.AcceptChanges();
                                }
                                
                            }
                        }

                    }
                }
            }
            catch (Exception ex) 
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, dttSource);

        }

        //[Authorize]
        [Route("api/Base/QuerySearch")]
        public HttpResponseMessage QuerySearch([FromBody] EntityDto obj)
        {
            string strResult = string.Empty;
            int intTotalPage = 0;
            int intTotalRecord = 0;

            string strWindowSize = "";
            string strDecimalColumn = "";
            string strColumnWidth = "";

            LookUpDao dao = new LookUpDao();
            DataTable dtt = null;

            try
            {
                dtt = dao.GetDataTable(obj, out intTotalPage, out intTotalRecord, out strWindowSize, out strDecimalColumn, out strColumnWidth);

                if (dtt == null)
                {
                    //IF Null then Get Column Only
                    dtt = dao.GetSingleData(obj.Entity, obj.Filter, obj.KeyCode, out strWindowSize, out strDecimalColumn, out strColumnWidth, false);
                    dtt.Rows.Clear();
                    dtt.AcceptChanges();

                    intTotalPage = 0;
                    intTotalRecord = 0;
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateLookupHttpResponse(strResult, dtt, intTotalPage, intTotalRecord, strDecimalColumn, strColumnWidth, strWindowSize);
        }

        //[Authorize]
        [Route("api/Base/QueryDownload")]
        public HttpResponseMessage QueryDownload([FromBody] EntityDto obj)
        {
            string strResult = string.Empty;

            string strDownloadRelativeUrl = string.Empty;

            try
            {
                //string strPath = HostingEnvironment.MapPath("~/Assets/SQLScript/" + obj.Entity + ".sql");

                QueryDao dao = new QueryDao();
                DataTable dtQuery = dao.ExecuteSPDataTableDownload(obj.Parameters, obj.Entity);

                //DataTable dtQuery = daoQuery.ExecutePathDataTable(obj.Parameters, strPath);

                if (dtQuery != null)
                {
                    foreach (DataColumn col in dtQuery.Columns)
                    {
                        col.ColumnName = col.ColumnName.Replace("[", "").Replace("]", "");
                    }
                    dtQuery.AcceptChanges();


                    strDownloadRelativeUrl = "~/Download/" + obj.FileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    FileInfo fInfoDownloadPath = new FileInfo(HostingEnvironment.MapPath(strDownloadRelativeUrl));
                    if (!Directory.Exists(fInfoDownloadPath.DirectoryName))
                    {
                        Directory.CreateDirectory(fInfoDownloadPath.DirectoryName);
                    }

                    List<FileInfo> lstFileInfo = new DirectoryInfo(fInfoDownloadPath.DirectoryName).GetFiles().Where(f => f.CreationTime.Date < DateTime.Now.AddDays(-1).Date || f.LastWriteTime.Date < DateTime.Now.AddDays(-1).Date).ToList();
                    foreach (FileInfo fi in lstFileInfo)
                    {
                        fi.Delete();
                    }

                    ExportHelper exportHelper = new ExportHelper();
                    exportHelper.ExportFile(dtQuery, ExportFileType.Excel, fInfoDownloadPath);
                }

            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, strDownloadRelativeUrl);
        }

        //[Authorize]
        [Route("api/Base/QuerySPSearch")]
        public HttpResponseMessage QuerySPSearch([FromBody] EntityDto obj)
        {
            string strResult = string.Empty;
            int intTotalPage = 0;
            int intTotalRecord = 0;

            QueryDao dao = new QueryDao();
            DataTable dtt = null;

            try
            {
                dtt = dao.ExecuteSPDataTablePaging(out intTotalPage, out intTotalRecord, obj.Parameters, obj);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateTableHttpResponse(strResult, dtt, intTotalPage, intTotalRecord);
        }

        ////[Authorize]
        //[Route("api/Base/QuerySPDownload")]
        //public HttpResponseMessage QuerySPDownload([FromBody] EntityDto obj)
        //{
        //    string strResult = string.Empty;
        //    QueryDao dao = new QueryDao();
        //    string strDownloadRelativeUrl = string.Empty;

        //    try
        //    {
        //        DataTable dtQuery = dao.ExecuteSPDataTableDownload(obj.Parameters, obj.Entity);

        //        //replace nama field menjadi lable name berdasarkan zdic
        //        ZDICDao daoZD = new ZDICDao();
        //        List<ZDICDto> lstZD = daoZD.GetList(new ZDICDto()
        //        {
        //            ZDCONO = string.Empty,
        //            ZDBRNO = string.Empty,
        //            ZDLGNO = "EN",
        //            ZDDITY = "R",
        //        });

        //        if (dtQuery != null)
        //        {
        //            foreach (DataColumn col in dtQuery.Columns)
        //            {
        //                string strColumnName = col.ColumnName.Replace("[", "").Replace("]", "");
        //                //lstZD = lstZD.Where(x=> x.ZDFIEL.Contains(strColumnName)).ToList();

        //                ZDICDto objDict = new ZDICDto();

        //                if (strColumnName.Length > 4)
        //                {
        //                     objDict = lstZD.SingleOrDefault(x => x.ZDFIEL.Contains(strColumnName.Substring(2)));
        //                }
        //                else
        //                {
        //                     objDict = lstZD.SingleOrDefault(x => x.ZDFIEL == strColumnName);
        //                }

        //                col.ColumnName = strColumnName;
        //                if (objDict != null)
        //                {
        //                    col.ColumnName = objDict.ZDLABL;
        //                }   
        //            }
        //            dtQuery.AcceptChanges();

        //            string strExtension = string.Empty;
        //            switch (obj.FileType.ToLower())
        //            {
        //                case "excel":
        //                    {
        //                        strExtension = "xlsx";
        //                        break;
        //                    }
        //                case "csv":
        //                    {
        //                        strExtension = "csv";
        //                        break;
        //                    }
        //                default:
        //                    {
        //                        break;
        //                    }
        //            }

        //            strDownloadRelativeUrl = "~/Download/" + obj.FileName;
        //            FileInfo fInfoDownloadPath = new FileInfo(HostingEnvironment.MapPath(strDownloadRelativeUrl));
        //            if (!Directory.Exists(fInfoDownloadPath.DirectoryName))
        //            {
        //                Directory.CreateDirectory(fInfoDownloadPath.DirectoryName);
        //            }

        //            List<FileInfo> lstFileInfo = new DirectoryInfo(fInfoDownloadPath.DirectoryName).GetFiles().Where(f => f.CreationTime.Date < DateTime.Now.AddDays(-1).Date || f.LastWriteTime.Date < DateTime.Now.AddDays(-1).Date).ToList();
        //            foreach (FileInfo fi in lstFileInfo)
        //            {
        //                fi.Delete();
        //            }

        //            ExportHelper exportHelper = new ExportHelper();
        //            switch (obj.FileType.ToLower())
        //            {
        //                case "excel":
        //                    {
        //                        exportHelper.ExportFile(dtQuery, ExportFileType.Excel, fInfoDownloadPath);
        //                        break;
        //                    }
        //                case "csv":
        //                    {
        //                        exportHelper.ExportFile(dtQuery, ExportFileType.CSV, fInfoDownloadPath);
        //                        break;
        //                    }
        //                default:
        //                    {
        //                        break;
        //                    }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        strResult = ex.Message;
        //    }

        //    return CreateHttpResponse(strResult, strDownloadRelativeUrl);
        //}

        //[Authorize]
        [Route("api/Base/DaxSearch")]
        public HttpResponseMessage DaxSearch([FromBody] EntityDto obj)
        {
            string strResult = string.Empty;
            int intTotalPage = 0;
            int intTotalRecord = 0;

            string strWindowSize = "";
            string strDecimalColumn = "";
            string strColumnWidth = "";

            LookUpDao dao = new LookUpDao();
            DataTable dtt = null;

            try
            {
                dtt = dao.GetDataTable(obj, out intTotalPage, out intTotalRecord, out strWindowSize, out strDecimalColumn, out strColumnWidth);

                if (dtt == null)
                {
                    //IF Null then Get Column Only
                    dtt = dao.GetSingleData(obj.Entity, obj.Filter, obj.KeyCode, out strWindowSize, out strDecimalColumn, out strColumnWidth, false);
                    dtt.Rows.Clear();
                    dtt.AcceptChanges();

                    intTotalPage = 0;
                    intTotalRecord = 0;
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateLookupHttpResponse(strResult, dtt, intTotalPage, intTotalRecord, strDecimalColumn, strColumnWidth, strWindowSize);
        }

        //[Authorize]
        [Route("api/Base/DaxDownload")]
        public HttpResponseMessage DaxDownload([FromBody] EntityDto obj)
        {
            string strResult = string.Empty;

            string strDownloadRelativeUrl = string.Empty;

            try
            {
                string strPath = HostingEnvironment.MapPath("~/Assets/DaxScript/" + obj.Entity + ".dax");

                AnalysisServiceDao daoSSAS = new AnalysisServiceDao();
                DataTable dtSSAS = daoSSAS.ExecutePathDataTable(obj.Parameters, strPath);

                if (dtSSAS != null)
                {
                    foreach (DataColumn col in dtSSAS.Columns)
                    {
                        col.ColumnName = col.ColumnName.Replace("[", "").Replace("]", "");
                    }
                    dtSSAS.AcceptChanges();

                    string strExtension = string.Empty;
                    switch (obj.FileType.ToLower())
                    {
                        case "excel":
                            {
                                strExtension = "xlsx";
                                break;
                            }
                        case "csv":
                            {
                                strExtension = "csv";
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    strDownloadRelativeUrl = "~/Download/" + obj.FileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "." + strExtension;
                    FileInfo fInfoDownloadPath = new FileInfo(HostingEnvironment.MapPath(strDownloadRelativeUrl));
                    if (!Directory.Exists(fInfoDownloadPath.DirectoryName))
                    {
                        Directory.CreateDirectory(fInfoDownloadPath.DirectoryName);
                    }

                    List<FileInfo> lstFileInfo = new DirectoryInfo(fInfoDownloadPath.DirectoryName).GetFiles().Where(f => f.CreationTime.Date < DateTime.Now.AddDays(-1).Date || f.LastWriteTime.Date < DateTime.Now.AddDays(-1).Date).ToList();
                    foreach (FileInfo fi in lstFileInfo)
                    {
                        fi.Delete();
                    }

                    ExportHelper exportHelper = new ExportHelper();
                    switch (obj.FileType.ToLower()) 
                    {
                        case "excel": 
                            {
                                exportHelper.ExportFile(dtSSAS, ExportFileType.Excel, fInfoDownloadPath);
                                break;
                            }
                        case "csv":
                            {
                                exportHelper.ExportFile(dtSSAS, ExportFileType.CSV, fInfoDownloadPath);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, strDownloadRelativeUrl);
        }

        //[Authorize]
        [Route("api/Base/GetVariableValue/{strZRVANO}")]
        public HttpResponseMessage GetVariableValue(string strZRVANO)
        {
            string strResult = string.Empty;
            string strZRVAVL = string.Empty;

            try
            {
                ZVARDao dao = new ZVARDao();
                ZVARDto objInfo = new ZVARDto();
                objInfo.ZRCONO = string.Empty;
                objInfo.ZRBRNO = string.Empty;
                objInfo.ZRVANO = strZRVANO;

                ZVARDto obj = dao.Get(objInfo);

                if (obj != null)
                    strZRVAVL = obj.ZRVAVL;
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, strZRVAVL);
        }

        //[Authorize]
        [Route("api/Base/GetVariable/{strZRVANO}")]
        public HttpResponseMessage GetVariable(string strZRVANO)
        {
            string strResult = string.Empty;
            ZVARDto obj = null;

            try
            {
                ZVARDao dao = new ZVARDao();
                ZVARDto objInfo = new ZVARDto();
                objInfo.ZRCONO = string.Empty;
                objInfo.ZRBRNO = string.Empty;
                objInfo.ZRVANO = strZRVANO;

                obj = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, obj);
        }
    }
}
