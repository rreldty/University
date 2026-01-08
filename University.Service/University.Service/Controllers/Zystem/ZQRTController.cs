using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using University.Api.Common;
using System.Data;
using System.Web.Http.Results;
using University.Dto.Zystem;
using University.Dao.Zystem;
using System.IO;
using System.Web.Hosting;
using System.Net.Http.Headers;

namespace University.Api.Controllers.Zystem
{
    //[Authorize]
    public class ZQRTController : BaseApiController<ZQRTDto>
    {
        [Route("api/ZQRT/Save")]
        public HttpResponseMessage Save([FromBody] ZQRTDto objInfo)
        {
            string strResult = string.Empty;
            ZQRTDto objResult = null;

            //try
            //{
            //    ZQRTDao dao = new ZQRTDao();
            //    strResult = dao.Save(objInfo, out string strZQRNO);
            //}
            //catch (Exception ex)
            //{
            //    strResult = ex.Message;
            //}

            //return CreateHttpResponse(strResult);

            try
            {
                ZQRTDao dao = new ZQRTDao();
                strResult = dao.Save(objInfo, out string strZQQRNO);

                if (string.IsNullOrEmpty(strResult))
                {
                    objInfo.ZQQRNO = strZQQRNO;
                    objResult = dao.Get(objInfo);
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }


            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZQRT/OneData")]
        public HttpResponseMessage OneData([FromBody] ZQRTDto objInfo)
        {
            ZQRTDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZQRTDao dao = new ZQRTDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

         [Route("api/ZQRT/List")]
        public HttpResponseMessage List([FromBody]ZQRTDto objInfo)
        {
            List<ZQRTDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZQRTDao dao = new ZQRTDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        //[Route("api/ZQRT/ListPagingUser")]
        //public HttpResponseMessage ListPaging([FromBody]ZQRTDto objInfo)
        //{
        //    List<ZQRTDto> lst = null;
        //    string strResult = string.Empty;

        //    int intTotalPage = 0;
        //    int intTotalRecord = 0;

        //    try
        //    {
        //        ZQRTDao dao = new ZQRTDao();
        //        lst = dao.GetListPagingUser(out intTotalPage, out intTotalRecord, objInfo, objInfo.PageNumber, objInfo.PageSize);
        //    }
        //    catch (Exception ex)
        //    {
        //        strResult = ex.Message;
        //    }

        //    return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        //}

        [Route("api/ZQRT/ListQueryTools")]
        public HttpResponseMessage ListQueryTools([FromBody] ZQRTDto objInfo)
        {
            DataTable dttSource = null;
            string strResult = string.Empty;
            int intTotalPage = 0;
            int intTotalRecord = 0;

            try
            {
                ZQRTDao dao = new ZQRTDao();
                dttSource = dao.GetDataTable(out strResult, objInfo.ZQQURY);
                //dttSource = dao.GetListQueryTools(objInfo, objInfo.PageNumber, objInfo.PageSize, out intTotalPage, out intTotalRecord);
                dttSource.AcceptChanges();
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateTableHttpResponse(strResult, dttSource, intTotalPage, intTotalRecord);
        }


        [Route("api/ZQRT/DownloadQueryTools")]
        public HttpResponseMessage DownloadQueryTools([FromBody] ZQRTDto objInfo)
        {
            string strResult = string.Empty;
            string strDownloadRelativeUrl = string.Empty;
            DataTable dtt = null;

            try
            {
                //Get DataTable from query
                ZQRTDao dao = new ZQRTDao();
                dtt = dao.GetDataTable(out strResult, objInfo.ZQQURY);

                //Download
                if (dtt != null)
                {
                    strDownloadRelativeUrl = "~/Download/" + objInfo.ZQQRNA;// +objInfo.ZQQRNA + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    FileInfo fInfoDownloadPath = new FileInfo(HostingEnvironment.MapPath(strDownloadRelativeUrl));

                    ExportHelper exportHelper = new ExportHelper();
                    exportHelper.ExportFile(dtt, ExportFileType.Excel, fInfoDownloadPath);
                    
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, strDownloadRelativeUrl);
        }

        [Route("api/ZQRT/Download")]
        public HttpResponseMessage Download([FromBody] ZQRTDto objInfo)
        {
            string strResult = string.Empty;
            string strDownloadRelativeUrl = string.Empty;

            strDownloadRelativeUrl = "~/FileDownload/Query Tools/" + objInfo.ZQQRNA + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            strDownloadRelativeUrl = Path.Combine(HostingEnvironment.MapPath(strDownloadRelativeUrl));
            
            try
            {
                DataTable dtt = null;

                //Get DataTable from query
                ZQRTDao dao = new ZQRTDao();
                dtt = dao.GetDataTable(out strResult, objInfo.ZQQURY);


                if (!File.Exists(strDownloadRelativeUrl))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "File not found.");
                }

                FileStream fileStream = File.OpenRead(strDownloadRelativeUrl);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(fileStream);

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = objInfo.ZQQRNA + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx"
                };

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

    }
}