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

namespace University.Api.Controllers.Zystem
{
    //[Authorize]
    public class ZUG1Controller : BaseApiController<ZUG1Dto>
    {
        [Route("api/ZUG1/Save")]
        public HttpResponseMessage Save([FromBody]ZUG1Dto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                ZUG1Dao dao = new ZUG1Dao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }


        [Route("api/ZUG1/SaveDelete")]
        public HttpResponseMessage SaveDelete([FromBody] ZUG1Dto objInfo)
        {
            string strResult = string.Empty;
            ZUG1Dto dto = null;
            List<ZUG2Dto> lst = objInfo.lstZUG2;

            try
            {

                ZUG2Dao dao = new ZUG2Dao();
                strResult = dao.DeleteLineUpdateHeader(objInfo, lst);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return CreateHttpResponse(strResult);
        }

        [Route("api/ZUG1/SaveWithLine")]
        public HttpResponseMessage SaveWithLine([FromBody] ZUG1Dto objinfo)
        {
            string strResult = string.Empty;
            List<ZUSRDto> lst = objinfo.lstZUSR;

            try
            {
                ZUG1Dao dao = new ZUG1Dao();
                strResult = dao.SaveWithLine(objinfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return CreateHttpResponse(strResult);
        }

        [Route("api/ZUG1/OneData")]
        public HttpResponseMessage OneData([FromBody]ZUG1Dto objInfo)
        {
            ZUG1Dto objResult = null;
            string strResult = string.Empty;

            try
            {
                ZUG1Dao dao = new ZUG1Dao();
                objResult = dao.Get(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, objResult);
        }

    }
}