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
    public class KRSHeaderController : BaseApiController<KRSHeaderDto>
    {
        [Route("api/KRSHeader/Save")]
        public HttpResponseMessage Save([FromBody] KRSHeaderDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                KRSHeaderDao dao = new KRSHeaderDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/KRSHeader/Update")]
        public HttpResponseMessage Update([FromBody] KRSHeaderDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                KRSHeaderDao dao = new KRSHeaderDao();
                KRSDetailDao detailDao = new KRSDetailDao();

                // Delete selected details
                if (objInfo.Details != null && objInfo.Details.Count > 0)
                {
                    foreach (KRSDetailDto detail in objInfo.Details)
                    {
                        strResult = detailDao.Delete(detail);
                        if (!string.IsNullOrEmpty(strResult))
                        {
                            break;
                        }
                    }
                }

                // Recalculate total SKS
                if (string.IsNullOrEmpty(strResult))
                {
                    strResult = dao.Calculate(objInfo);
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/KRSHeader/Delete")]
        public HttpResponseMessage Delete([FromBody] KRSHeaderDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                KRSHeaderDao dao = new KRSHeaderDao();
                strResult = dao.Delete(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

        [Route("api/KRSHeader/OneData")]
        public HttpResponseMessage OneData([FromBody] KRSHeaderDto objInfo)
        {
            KRSHeaderDto objResult = null;
            string strResult = string.Empty;

            try
            {
                KRSHeaderDao dao = new KRSHeaderDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/KRSHeader/List")]
        public HttpResponseMessage List([FromBody] KRSHeaderDto objInfo)
        {
            List<KRSHeaderDto> lst = null;
            string strResult = string.Empty;

            try
            {
                KRSHeaderDao dao = new KRSHeaderDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [Route("api/KRSHeader/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody] KRSHeaderDto objInfo)
        {
            int intTotalPage = 0;
            int intTotalRecord = 0;

            List<KRSHeaderDto> lst = null;
            string strResult = string.Empty;

            try
            {
                KRSHeaderDao dao = new KRSHeaderDao();
                lst = dao.GetListPaging(objInfo, objInfo.PageNumber, objInfo.PageSize, out intTotalPage, out intTotalRecord);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

        [Route("api/KRSHeader/ReportKRS")]
        public HttpResponseMessage ReportKRS([FromBody] KRSHeaderDto objInfo)
        {
            List<KRSHeaderDto> lst = null;
            string strResult = string.Empty;

            try
            {
                KRSHeaderDao dao = new KRSHeaderDao();
                lst = dao.GetReportKRS(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }
    }
}
