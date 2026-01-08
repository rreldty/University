using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using University.Dto.Zystem;
using University.Dao.Zystem;
using University.Api.Common;

namespace University.Api.Controllers.Zystem
{
    //[Authorize]
    public class ZAUTController : BaseApiController<ZAUTDto>
    {

        [Route("api/ZAUT/ListPaging")]
        public HttpResponseMessage ListPaging([FromBody]ZAUTDto objInfo)
        {
            List<ZAUTDto> lst = null;
            string strResult = string.Empty;

            int intTotalPage = 0;
            int intTotalRecord = 0;

            try
            {
                ZAUTDao dao = new ZAUTDao();
                lst = dao.GetListPaging(objInfo, objInfo.PageNumber, objInfo.PageSize, out intTotalPage, out intTotalRecord);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
        }

        [Route("api/ZAUT/Save")]
        public HttpResponseMessage Save([FromBody]ZAUTDto objInfo)
        {
            string strResult = string.Empty;

            try
            {
                ZAUTDao dao = new ZAUTDao();
                strResult = dao.Save(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult);
        }

		[Route("api/ZAUT/SaveWithLine")]
		public HttpResponseMessage SaveWithLine([FromBody] ZAUTDto objinfo)
		{
			string strResult = string.Empty;
			List<ZMNUDto> lst = objinfo.lstZMNU;

			try
			{
				ZAUTDao dao = new ZAUTDao();
				strResult = dao.SaveWithLine(objinfo);
			}
			catch (Exception ex)
			{
				strResult = ex.Message;
			}
			return CreateHttpResponse(strResult);
		}


		[Route("api/ZAUT/ListMenu")]
        public HttpResponseMessage ListMenu([FromBody] ZAUTDto objInfo)
        {
            List<ZAUTDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZAUTDao dao = new ZAUTDao();
                lst = dao.GetListByUserIdApp(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }

        [Route("api/ZAUT/ListApplication")]
        public HttpResponseMessage ListApplication([FromBody] ZAUTDto objInfo)
        {
            List<ZAUTDto> lst = null;
            string strResult = string.Empty;

            try
            {
                ZAUTDao dao = new ZAUTDao();
                lst = dao.GetListApplication(objInfo);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return CreateHttpResponse(strResult, lst);
        }
    

		[Route("api/ZAUT/SaveDelete")]
		public HttpResponseMessage SaveDelete([FromBody] ZAUTDto objInfo)
		{
			string strResult = string.Empty;
			ZAUTDto dto = null;
			List<ZAUTDto> lst = objInfo.lstZAUT;
			try
			{

				ZAUTDao dao = new ZAUTDao();
				strResult = dao.DeleteLineUpdateHeader(objInfo, lst); 
			}
			catch (Exception ex)
			{
				strResult = ex.Message;
			}
			return CreateHttpResponse(strResult);
		}
	}

}