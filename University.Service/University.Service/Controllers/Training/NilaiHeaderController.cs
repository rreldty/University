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
    public class NilaiHeaderController : BaseApiController<NilaiHeaderDto>
    {
        [HttpPost]
        [Route("api/NilaiHeader/Save")]
        public HttpResponseMessage Save([FromBody] NilaiHeaderDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                NilaiHeaderDao dao = new NilaiHeaderDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [HttpPost]
        [Route("api/NilaiHeader/Update")]
        public HttpResponseMessage Update([FromBody] NilaiHeaderDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                NilaiHeaderDao dao = new NilaiHeaderDao();
                strResult = dao.Update(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [HttpPost]
        [Route("api/NilaiHeader/Delete")]
        public HttpResponseMessage Delete([FromBody] NilaiHeaderDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                NilaiHeaderDao dao = new NilaiHeaderDao();
                strResult = dao.Delete(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [HttpPost]
        [Route("api/NilaiHeader/OneData")]
        public HttpResponseMessage OneData([FromBody] NilaiHeaderDto objInfo)
        {
            NilaiHeaderDto objResult = null;
            string strResult = string.Empty;

            try
            {
                NilaiHeaderDao dao = new NilaiHeaderDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [HttpPost]
        [Route("api/NilaiHeader/List")]
        public HttpResponseMessage List([FromBody] NilaiHeaderDto objInfo)
        {
            List<NilaiHeaderDto> lst = null;
            string strResult = string.Empty;

            try
            {
                NilaiHeaderDao dao = new NilaiHeaderDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [HttpPost]
        [Route("api/NilaiHeader/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody] NilaiHeaderDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<NilaiHeaderDto> lst = null;
            string strResult = string.Empty;

            try
            {
                NilaiHeaderDao dao = new NilaiHeaderDao();
                lst = dao.GetListPaging(objInfo, objInfo.PageNumber, objInfo.PageSize, out intTotalPage, out intTotalRecord);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

        [HttpPost]
        [Route("api/NilaiHeader/ReportNilai")]
        public HttpResponseMessage ReportNilai([FromBody] NilaiHeaderDto objInfo)
        {
            List<NilaiHeaderDto> lst = null;
            string strResult = string.Empty;

            try
            {
                NilaiHeaderDao dao = new NilaiHeaderDao();
                lst = dao.GetReportNilai(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [HttpPost]
        [Route("api/NilaiHeader/GetDataMahasiswa")]
        public HttpResponseMessage GetDataMahasiswa([FromBody] NilaiHeaderDto objInfo)
        {
            NilaiHeaderDto objResult = null;
            string strResult = string.Empty;

            try
            {
                NilaiHeaderDao dao = new NilaiHeaderDao();
                objResult = dao.GetDataMahasiswa(objInfo.nim, objInfo.semester);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [HttpPost]
        [Route("api/NilaiHeader/Calculate")]
        public HttpResponseMessage Calculate([FromBody] NilaiHeaderDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                NilaiHeaderDao dao = new NilaiHeaderDao();
                strResult = dao.Calculate(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }
    }
}
