using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using University.Dao.Zystem;
using University.Dto.Zystem;
using System.Web.Http.Cors;
using University.Api.Common;
using System.Data;
using System.IO;
using System.Web.Hosting;
using Microsoft.PowerBI.Api.Models;

namespace University.Api.Controllers.Zystem
{
    //[Authorize]
    public class ZAPPController : BaseApiController<ZAPPDto>
    {
        [Route("api/ZAPP/Save")]
        public HttpResponseMessage Save([FromBody]ZAPPDto objInfo)
        {
            string strResult = string.Empty;
            //testing ubah
            try
            {
                ZAPPDao dao = new ZAPPDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/ZAPP/OneData")]
        public HttpResponseMessage OneData([FromBody]ZAPPDto objInfo)
        {
            ZAPPDto objResult = new ZAPPDto();
            string strResult = string.Empty;
            
            try
            {
                ZAPPDao dao = new ZAPPDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZAPP/ReportSummaryPrimarySales")]
        public HttpResponseMessage DownloadSR230A([FromBody] ZAPPDto objInfo)
        {
            string strResult = string.Empty;
            string strDownloadRelativeUrl = string.Empty;
            DataTable dtt = null;

            //try
            //{
            //    //Get DataTable from query
            //    ZAPPDao dao = new ZAPPDao();
            //    objInfo.IsDownload = true;
            //    dtt = dao.GetDetailSR230(objInfo);

            //    //Download
            //    if (dtt != null)
            //    {
            //        strDownloadRelativeUrl = "~/FileDownload/Reports/LaporanKunjunganByMG2_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            //        FileInfo fInfoDownloadPath = new FileInfo(HostingEnvironment.MapPath(strDownloadRelativeUrl));

            //        ExportHelper exportHelper = new ExportHelper();
            //        exportHelper.ExportFile(dtt, ExportFileType.Excel, fInfoDownloadPath);

            //    }

            //    strResult = "";
            //}
            //catch (Exception ex)
            //{
            //    strResult = ex.Message;
            //}

            return CreateHttpResponse(strResult, strDownloadRelativeUrl);
        }

    }
}