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
using University.Dao.Training;
using University.Dto;
using University.Dto.Training;

namespace University.Api.Controllers.Training
{
    //[Authorize]
    public class JurusanController : BaseApiController<JurusanDto>
    {
        [Route("api/Jurusan/Save")]
        public HttpResponseMessage Save([FromBody] JurusanDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                JurusanDao dao = new JurusanDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/Jurusan/OneData")]
        public HttpResponseMessage OneData([FromBody] JurusanDto objInfo)
        {
            JurusanDto objResult = null;
            string strResult = string.Empty;

            try
            {
                JurusanDao dao = new JurusanDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/Jurusan/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody] JurusanDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<JurusanDto> lst = null;
            string strResult = string.Empty;

            try
            {
                JurusanDao dao = new JurusanDao();
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
