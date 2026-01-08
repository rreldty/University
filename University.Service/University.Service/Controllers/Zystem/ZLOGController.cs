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

namespace University.Api.Controllers.Zystem
{
	//[Authorize]
	public class ZLOGController : BaseApiController<ZLOGDto>
	{
		[Route("api/ZLOG/Save")]
		public HttpResponseMessage Save([FromBody] ZLOGDto objInfo)
		{
			string strResult = string.Empty;

			try
			{
				ZLOGDao dao = new ZLOGDao();
				strResult = dao.Save(objInfo);
			}
			catch (Exception ex)
			{
				strResult = ex.Message;
			}

			return CreateHttpResponse(strResult);
		}


		[Route("api/ZLOG/OneData")]
		public HttpResponseMessage OneData([FromBody] ZLOGDto objInfo)
		{
			ZLOGDto objResult = null;
			string strResult = string.Empty;

			try
			{
				ZLOGDao dao = new ZLOGDao();
				objResult = dao.Get(objInfo);
			}
			catch (Exception ex)
			{
				strResult = ex.Message;
			}

			return CreateHttpResponse(strResult, objResult);
		}

		[Route("api/ZLOG/ListPaging")]
		public HttpResponseMessage ListPaging([FromBody] ZLOGDto objInfo)
		{
			int intTotalPage = 0;
			int intTotalRecord = 0;

			List<ZLOGDto> lst = null;
			string strResult = string.Empty;

			try
			{
				ZLOGDao dao = new ZLOGDao();
				lst = dao.GetListPaging(out intTotalPage, out intTotalRecord, objInfo, objInfo.PageNumber, objInfo.PageSize);
			}
			catch (Exception ex)
			{
				strResult = ex.Message;
			}

			return CreateHttpResponse(strResult, lst, intTotalPage, intTotalRecord);
		}

	}
}