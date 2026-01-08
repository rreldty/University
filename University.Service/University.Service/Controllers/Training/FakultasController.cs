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
    public class FakultasController : BaseApiController<FakultasDto>
    {
        [Route("api/Fakultas/Save")]
        public HttpResponseMessage Save([FromBody] FakultasDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                FakultasDao dao = new FakultasDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/Fakultas/OneData")]
        public HttpResponseMessage OneData([FromBody] FakultasDto objInfo)
        {
            FakultasDto objResult = null;
            string strResult = string.Empty;

            try
            {
                FakultasDao dao = new FakultasDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/Fakultas/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody] FakultasDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<FakultasDto> lst = null;
            string strResult = string.Empty;

            try
            {
                FakultasDao dao = new FakultasDao();
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