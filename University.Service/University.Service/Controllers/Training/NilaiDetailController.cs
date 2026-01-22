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
    public class NilaiDetailController : BaseApiController<NilaiDetailDto>
    {
        [HttpPost]
        [Route("api/NilaiDetail/Save")]
        public HttpResponseMessage Save([FromBody] NilaiDetailDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                NilaiDetailDao dao = new NilaiDetailDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [HttpPost]
        [Route("api/NilaiDetail/Delete")]
        public HttpResponseMessage Delete([FromBody] NilaiDetailDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                NilaiDetailDao dao = new NilaiDetailDao();
                strResult = dao.Delete(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [HttpPost]
        [Route("api/NilaiDetail/OneData")]
        public HttpResponseMessage OneData([FromBody] NilaiDetailDto objInfo)
        {
            NilaiDetailDto objResult = null;
            string strResult = string.Empty;

            try
            {
                NilaiDetailDao dao = new NilaiDetailDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [HttpPost]
        [Route("api/NilaiDetail/List")]
        public HttpResponseMessage List([FromBody] NilaiDetailDto objInfo)
        {
            List<NilaiDetailDto> lst = null;
            string strResult = string.Empty;

            try
            {
                NilaiDetailDao dao = new NilaiDetailDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [HttpPost]
        [Route("api/NilaiDetail/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody] NilaiDetailDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<NilaiDetailDto> lst = null;
            string strResult = string.Empty;

            try
            {
                NilaiDetailDao dao = new NilaiDetailDao();
                lst = dao.GetListPaging(objInfo, objInfo.PageNumber, objInfo.PageSize, out intTotalPage, out intTotalRecord);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

        [HttpPost]
        [Route("api/NilaiDetail/GetListMataKuliah")]
        public HttpResponseMessage GetListMataKuliah([FromBody] NilaiDetailDto objInfo)
        {
            List<NilaiDetailDto> lst = null;
            string strResult = string.Empty;

            try
            {
                NilaiDetailDao dao = new NilaiDetailDao();
                // Assuming kode_jurusan is passed in a field - we need to add this to DTO
                lst = dao.GetListMataKuliah(objInfo.nim, objInfo.semester, objInfo.kode_jurusan);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }
    }
}
