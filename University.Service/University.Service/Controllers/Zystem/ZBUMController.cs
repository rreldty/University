using System.Collections.Generic;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using University.Dao.Zystem;
using University.Dto.Zystem;
using University.Api.Common;
using System;

namespace University.Api.Controllers.Zystem
{
    //[Authorize]
    public class ZBUMController : BaseApiController<ZBUMDto>
    {
        [Route("api/ZBUM/Save")]
        public HttpResponseMessage Save([FromBody]ZBUMDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                ZBUMDao dao = new ZBUMDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/ZBUM/OneData")]
        public HttpResponseMessage OneData([FromBody]ZBUMDto objInfo)
        {
            ZBUMDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZBUMDao dao = new ZBUMDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZBUM/List")]
        public HttpResponseMessage List([FromBody]ZBUMDto objInfo)
        {
            List<ZBUMDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZBUMDao dao = new ZBUMDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [Route("api/ZBUM/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody]ZBUMDto objInfo)
        {
            List<ZBUMDto> lst = null;
            string strResult = string.Empty;

            int intTotalPage = 0;
            int intTotalRecord = 0;

            try
            {
                ZBUMDao dao = new ZBUMDao();
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