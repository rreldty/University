using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using University.Dao.Zystem;
using University.Dto.Zystem;
using University.Api.Common;
using System.Data;

namespace University.Api.Controllers.Zystem
{
    //[Authorize]
    public class ZVARController : BaseApiController<ZVARDto>
    {
		[Route("api/ZVAR/Save")]
		public HttpResponseMessage Save([FromBody]ZVARDto objInfo)
		{
            string strResult = string.Empty;

            try
            {
                ZVARDao dao = new ZVARDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

		[Route("api/ZVAR/GetList/{strVATY}")]
        public HttpResponseMessage GetList(string strVATY)
        {
            List<ZVARDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZVARDto objInfo = new ZVARDto()
                {
                    ZRCONO = string.Empty,
                    ZRBRNO = string.Empty,
                    ZRVATY = strVATY
                };

                ZVARDao dao = new ZVARDao();
                lst = dao.GetList(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

		[Route("api/ZVAR/OneData")]
		public HttpResponseMessage OneData([FromBody]ZVARDto objInfo)
		{
            ZVARDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZVARDao dao = new ZVARDao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        [Route("api/ZVAR/UrlByVANO")]
        public HttpResponseMessage UrlByVANO([FromBody]ZVARDto objInfo)
        {
            ZVARDto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZVARDao dao = new ZVARDao();
                objResult = dao.UrlByVANO(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

        

    }
}