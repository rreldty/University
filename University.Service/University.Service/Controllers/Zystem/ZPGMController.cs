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

namespace University.Api.Controllers.Zystem
{
    //[Authorize]
    public class ZPGMController : BaseApiController<ZPGMDto>
    {
        [Route("api/ZPGM/Save")]
        public HttpResponseMessage Save([FromBody]ZPGMDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                ZPGMDao dao = new ZPGMDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/ZPGM/GetData/{strCONO}/{strBRNO}/{strAPNO}/{strPGNO}")]
        public HttpResponseMessage GetData(string strCONO, string strBRNO, string strAPNO, string strPGNO)
        {
            ZPGMDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZPGMDto objInfo = new ZPGMDto()
                {
                    ZPCONO = strCONO,
                    ZPBRNO = strBRNO,
                    ZPAPNO = strAPNO,
                    ZPPGNO = strPGNO
                };

                ZPGMDao dao = new ZPGMDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZPGM/OneData")]
        public HttpResponseMessage OneData([FromBody]ZPGMDto objInfo)
        {
            ZPGMDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZPGMDao dao = new ZPGMDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZPGM/List")]
        public HttpResponseMessage List([FromBody]ZPGMDto objInfo)
        {
            List<ZPGMDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZPGMDao dao = new ZPGMDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [Route("api/ZPGM/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody]ZPGMDto objInfo)
        {
            List<ZPGMDto> lst = null;
            string strResult = string.Empty;

            int intTotalPage = 0;
            int intTotalRecord = 0;

            try
            {
                ZPGMDao dao = new ZPGMDao();
                lst = dao.GetListPaging(objInfo, objInfo.PageNumber, objInfo.PageSize, out intTotalPage, out intTotalRecord);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

    }
}