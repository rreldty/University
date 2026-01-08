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
    public class MataKuliahController : BaseApiController<MataKuliahDto>
    {
        [Route("api/MataKuliah/Save")]
        public HttpResponseMessage Save([FromBody] MataKuliahDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                MataKuliahDao dao = new MataKuliahDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/MataKuliah/OneData")]
        public HttpResponseMessage OneData([FromBody] MataKuliahDto objInfo)
        {
            MataKuliahDto objResult = null;
            string strResult = string.Empty;

            try
            {
                MataKuliahDao dao = new MataKuliahDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/MataKuliah/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody] MataKuliahDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<MataKuliahDto> lst = null;
            string strResult = string.Empty;

            try
            {
                MataKuliahDao dao = new MataKuliahDao();
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
