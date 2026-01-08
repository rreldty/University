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

namespace University.Api.Controllers.Zystem
{
    //[Authorize]
    public class ZUG2Controller : BaseApiController<ZUG2Dto>
    {
        [Route("api/ZUG2/Save")]
        public HttpResponseMessage Save([FromBody]ZUG2Dto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                ZUG2Dao dao = new ZUG2Dao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        
        [Route("api/ZUG2/OneData")]
        public HttpResponseMessage OneData([FromBody]ZUG2Dto objInfo)
        {
            ZUG2Dto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZUG2Dao dao = new ZUG2Dao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZUG2/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody]ZUG2Dto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<ZUG2Dto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZUG2Dao dao = new ZUG2Dao();
                lst = dao.GetListPaging(out intTotalPage, out intTotalRecord, objInfo, objInfo.PageNumber, objInfo.PageSize);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

        [Route("api/ZUG2/TablePaging")]
        public HttpResponseMessage TablePaging([FromBody] ZUG2Dto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            DataTable lst = null;
            string strResult = string.Empty;

            try
            {
                ZUG2Dao dao = new ZUG2Dao();
                 lst = dao.GetTablePaging(out intTotalPage, out intTotalRecord, objInfo, objInfo.PageNumber, objInfo.PageSize);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateTableHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

    }
}