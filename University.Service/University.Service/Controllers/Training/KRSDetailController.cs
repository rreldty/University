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
    public class KRSDetailController : BaseApiController<KRSDetailDto>
    {
        [Route("api/KRSDetail/Save")]
        public HttpResponseMessage Save([FromBody] KRSDetailDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                KRSDetailDao dao = new KRSDetailDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/KRSDetail/Delete")]
        public HttpResponseMessage Delete([FromBody] KRSDetailDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                KRSDetailDao dao = new KRSDetailDao();
                strResult = dao.Delete(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/KRSDetail/OneData")]
        public HttpResponseMessage OneData([FromBody] KRSDetailDto objInfo)
        {
            KRSDetailDto objResult = null;
            string strResult = string.Empty;

            try
            {
                KRSDetailDao dao = new KRSDetailDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/KRSDetail/List")]
        public HttpResponseMessage List([FromBody] KRSDetailDto objInfo)
        {
            List<KRSDetailDto> lst = null;
            string strResult = string.Empty;

            try
            {
                KRSDetailDao dao = new KRSDetailDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [Route("api/KRSDetail/ListMataKuliah")]
        public HttpResponseMessage ListMataKuliah([FromBody] KRSDetailDto objInfo)
        {
            List<KRSDetailDto> lst = null;
            string strResult = string.Empty;

            try
            {
                KRSDetailDao dao = new KRSDetailDao();
                lst = dao.GetListMataKuliah(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [Route("api/KRSDetail/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody] KRSDetailDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<KRSDetailDto> lst = null;
            string strResult = string.Empty;

            try
            {
                KRSDetailDao dao = new KRSDetailDao();
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
