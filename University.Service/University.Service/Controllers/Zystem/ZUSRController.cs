using System.Collections.Generic;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using University.Dao.Zystem;
using University.Dto.Zystem;
using University.Api.Common;
using System;

namespace University.Service.Controllers.Zystem
{
    //[Authorize]
    public class ZUSRController : BaseApiController<ZUSRDto>
    {
        [Route("api/ZUSR/Save")]
        public HttpResponseMessage Save([FromBody] ZUSRDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/ZUSR/ChangePassword")]
        public HttpResponseMessage ChangePassword([FromBody] ZUSRDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                strResult = dao.ChangePassword(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }


        [Route("api/ZUSR/OneData")]
        public HttpResponseMessage OneData([FromBody] ZUSRDto objInfo)
        {
            ZUSRDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                objResult = dao.GetwithZUG(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZUSR/List")]
        public HttpResponseMessage List([FromBody] ZUSRDto objInfo)
        {
            List<ZUSRDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [Route("api/ZUSR/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody] ZUSRDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<ZUSRDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                lst = dao.GetListPaging(out intTotalPage, out intTotalRecord, objInfo, objInfo.PageNumber, objInfo.PageSize);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

        [Route("api/ZUSR/ListPagingNotInUserGroup")]
        public HttpResponseMessage ListPagingNotInUserGroup([FromBody] ZUSRDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<ZUSRDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                lst = dao.GetListPagingNotInUserGroup(out intTotalPage, out intTotalRecord, objInfo, objInfo.PageNumber, objInfo.PageSize);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

        [Route("api/ZUSR/Login")]
        public HttpResponseMessage Login([FromBody] ZUSRDto objInfo)
        {
            ZUSRDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                objResult = dao.CheckUserLogin(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

       


        [Route("api/ZUSR/SearchList")]
        public HttpResponseMessage ListSearch([FromBody] ZUSRDto objInfo)
        {
            List<ZUSRDto> lst = null;
            string strResult = string.Empty;

            int intTotalPage = 0;
            int intTotalRecord = 0;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                lst = dao.GetListSearch(out intTotalPage, out intTotalRecord, objInfo, objInfo.PageNumber, objInfo.PageSize);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

        [Route("api/ZUSR/SubmitData")]
        public HttpResponseMessage SubmitData([FromBody] ZUSRDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                strResult = dao.SubmitData(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/ZUSR/ListData")]
        public HttpResponseMessage ListData([FromBody] ZUSRDto objInfo)
        {
            List<ZUSRDto> lst = null;
            string strResult = string.Empty;

            //int intTotalPage = 0;
            //int intTotalRecord = 0;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                lst = dao.GetListPIC(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [Route("api/ZUSR/ListDataPIC")]
        public HttpResponseMessage ListDataPIC([FromBody] ZUSRDto objInfo)
        {
            List<ZUSRDto> lst = null;
            string strResult = string.Empty;

            //int intTotalPage = 0;
            //int intTotalRecord = 0;

            try
            {
                ZUSRDao dao = new ZUSRDao();
                lst = dao.GetListDataPIC(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }
    }
}