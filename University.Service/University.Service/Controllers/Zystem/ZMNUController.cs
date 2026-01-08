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
    public class ZMNUController : BaseApiController<ZMNUDto>
    {
        [Route("api/ZMNU/Save")]
        public HttpResponseMessage Save([FromBody]ZMNUDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                ZMNUDao dao = new ZMNUDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/ZMNU/GetData/{strCONO}/{strBRNO}/{strAPNO}/{strPGNO}")]
        public HttpResponseMessage GetData(string strCONO, string strBRNO, string strAPNO, string strPGNO)
        {
            ZMNUDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZMNUDto objInfo = new ZMNUDto()
                {
                    ZMCONO = strCONO,
                    ZMBRNO = strBRNO,
                    ZMAPNO = strAPNO,
                    ZMPGNO = strPGNO
                };

                ZMNUDao dao = new ZMNUDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZMNU/OneData")]
        public HttpResponseMessage OneData([FromBody]ZMNUDto objInfo)
        {
            ZMNUDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZMNUDao dao = new ZMNUDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZMNU/List")]
        public HttpResponseMessage List([FromBody]ZMNUDto objInfo)
        {
            List<ZMNUDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZMNUDao dao = new ZMNUDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [Route("api/ZMNU/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody]ZMNUDto objInfo)
        {
            List<ZMNUDto> lst = null;
            string strResult = string.Empty;

            int intTotalPage = 0;
            int intTotalRecord = 0;

            try
            {
                ZMNUDao dao = new ZMNUDao();
                lst = dao.GetListPaging( objInfo, objInfo.PageNumber, objInfo.PageSize, out intTotalPage, out intTotalRecord);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

        [Route("api/ZMNU/listPagingNotInUserAuthority")]
        public HttpResponseMessage listPagingNotInUserAuthority([FromBody]ZMNUDto objInfo)
        {
            List<ZMNUDto> lst = null;
            string strResult = string.Empty;

            int intTotalPage = 0;
            int intTotalRecord = 0;

            try
            {
                ZMNUDao dao = new ZMNUDao();
                lst = dao.GetlistPagingNotInUserAuthority(objInfo, objInfo.PageNumber, objInfo.PageSize, out intTotalPage, out intTotalRecord);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }
    }
}