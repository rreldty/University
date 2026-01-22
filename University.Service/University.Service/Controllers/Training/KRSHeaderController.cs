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
    public class KrsHeaderController : BaseApiController<KrsHeaderDto>
    {
        [Route("api/KrsHeader/Save")]
        public HttpResponseMessage Save([FromBody] KrsHeaderDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                KrsHeaderDao dao = new KrsHeaderDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/KrsHeader/OneData")]
        public HttpResponseMessage OneData([FromBody] KrsHeaderDto objInfo)
        {
            KrsHeaderDto objResult = null;
            string strResult = string.Empty;

            try
            {
                KrsHeaderDao dao = new KrsHeaderDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/KrsHeader/OneDataMahasiswa")]
        public HttpResponseMessage OneDataMahasiswa([FromBody] KrsHeaderDto objInfo)
        {
            KrsHeaderDto objResult = null;
            string strResult = string.Empty;

            try
            {
                KrsHeaderDao dao = new KrsHeaderDao();
                objResult = dao.GetDataMahasiswa(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/KrsHeader/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody] KrsHeaderDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<KrsHeaderDto> lst = null;
            string strResult = string.Empty;

            try
            {
                KrsHeaderDao dao = new KrsHeaderDao();
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