#region Summary
//''''''''''''''''''''''''''''S U M M A R Y '''''''''''''''''''''''''''''
//'File Name     : ZLOGDao.cs
//'Author        : Vinno
//'Creation Date : 12/19/2014
//'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
#endregion

#region Reference
using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dto.Base;
using University.Dao.Base;
using University.Dto.Zystem;
#endregion

namespace University.Dao.Zystem
{
    public class ZLOGDao : BaseDao<ZLOGDto>
    {
        #region Constructor
        public ZLOGDao()
		{
			this.MainDataSource = DataSource.University;
		}
        #endregion

        #region Abstract Class Implementation
        protected override Mapper<ZLOGDto> GetMapper()
        {
            Mapper<ZLOGDto> mapDto = new MapZLOGDto();
            return mapDto;
        }
        #endregion

        #region Save
        public string Save(ZLOGDto obj)
        {
            if (!IsExists(obj))
            {
                return Insert(obj);
            }
            else
            {
                return Update(obj);
            }
        }
        #endregion

        #region Insert Data
        public string Insert(ZLOGDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("ZLCONO");
            lstField.Add("ZLBRNO");
            lstField.Add("ZLUSNO");
            lstField.Add("ZLLGDT");
            lstField.Add("ZLLGTM");
            lstField.Add("ZLLGTY");
            lstField.Add("ZLLGIP");
            lstField.Add("ZLREMA");
            lstField.Add("ZLSYST");
            lstField.Add("ZLSTAT");
            lstField.Add("ZLRCST");
            lstField.Add("ZLCRDT");
            lstField.Add("ZLCRTM");
            lstField.Add("ZLCRUS");
            lstField.Add("ZLCHDT");
            lstField.Add("ZLCHTM");
            lstField.Add("ZLCHUS");

            string strSql = this.GenerateStringInsert("ZLOG", lstField, obj);
            return this.ExecuteDbNonQuery(strSql);
        }
        #endregion

        #region Update Data
        public string Update(ZLOGDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("ZLLGTY");
            lstField.Add("ZLLGIP");
            lstField.Add("ZLREMA");
            lstField.Add("ZLSYST");
            lstField.Add("ZLSTAT");
            lstField.Add("ZLRCST");
            lstField.Add("ZLCHDT");
            lstField.Add("ZLCHTM");
            lstField.Add("ZLCHUS");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("ZLCONO");
            lstCondition.Add("ZLBRNO");
            lstCondition.Add("ZLUSNO");
            lstCondition.Add("ZLLGDT");
            lstCondition.Add("ZLLGTM");

            string strSql = this.GenerateStringUpdate("ZLOG", lstCondition, lstField, obj);
            return this.ExecuteDbNonQuery(strSql);
        }
        #endregion

        #region Delete Data
        public string Delete(ZLOGDto obj)
        {
            List<string> lstCondition = new List<string>();
            lstCondition.Add("ZLCONO");
            lstCondition.Add("ZLBRNO");
            lstCondition.Add("ZLUSNO");
            lstCondition.Add("ZLLGDT");
            lstCondition.Add("ZLLGTM");

            string strSql = this.GenerateStringDelete("ZLOG", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }
        #endregion

        #region Select Data
        public bool IsExists(ZLOGDto obj)
        {
            string strSql = "SELECT COUNT(*) FROM ZLOG WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.ZLCONO))
            {
                strSql += " AND ZLCONO = '" + obj.ZLCONO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZLBRNO))
            {
                strSql += " AND ZLBRNO = '" + obj.ZLBRNO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZLUSNO))
            {
                strSql += " AND ZLUSNO = '" + obj.ZLUSNO.Trim() + "' ";
            }

            if (obj.ZLLGDT != 0)
            {
                strSql += " AND ZLLGDT = " + obj.ZLLGDT.ToString() + " ";
            }

            if (obj.ZLLGTM != 0)
            {
                strSql += " AND ZLLGTM = " + obj.ZLLGTM.ToString() + " ";
            }

            Object _obj = this.ExecuteDbScalar(strSql);
            if (Convert.ToInt32(_obj) == 0)
            {
                return false;
            }
            return true;
        }

        public ZLOGDto Get(ZLOGDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("ZLCONO");
            lstField.Add("ZLBRNO");
            lstField.Add("ZLUSNO");
            lstField.Add("ZLLGDT");
            lstField.Add("ZLLGTM");
            lstField.Add("ZLLGTY");
            lstField.Add("ZLLGIP");
            lstField.Add("ZLREMA");
            lstField.Add("ZLSYST");
            lstField.Add("ZLSTAT");
            lstField.Add("ZLRCST");
            lstField.Add("ZLCRDT");
            lstField.Add("ZLCRTM");
            lstField.Add("ZLCRUS");
            lstField.Add("ZLCHDT");
            lstField.Add("ZLCHTM");
            lstField.Add("ZLCHUS");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("ZLCONO");
            lstCondition.Add("ZLBRNO");
            lstCondition.Add("ZLUSNO");
            lstCondition.Add("ZLLGDT");
            lstCondition.Add("ZLLGTM");

            string strSql = this.GenerateStringSelect("ZLOG", lstCondition, lstField, obj);
            ZLOGDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<ZLOGDto> GetList(ZLOGDto obj)
        {
            string strSql = "SELECT "
                    + " ZLCONO "
                    + " ,ZLBRNO "
                    + " ,ZLUSNO "
                    + " ,ZLLGDT "
                    + " ,ZLLGTM "
                    + " ,ZLLGTY "
                    + " ,ZLLGIP "
                    + " ,ZLREMA "
                    + " ,ZLSYST "
                    + " ,ZLSTAT "
                    + " ,ZLRCST "
                    + " ,ZLCRDT "
                    + " ,ZLCRTM "
                    + " ,ZLCRUS "
                    + " ,ZLCHDT "
                    + " ,ZLCHTM "
                    + " ,ZLCHUS "
                    + " FROM ZLOG "
                    + " WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.ZLCONO))
            {
                strSql += " AND ZLCONO = '" + obj.ZLCONO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZLBRNO))
            {
                strSql += " AND ZLBRNO = '" + obj.ZLBRNO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZLUSNO))
            {
                strSql += " AND ZLUSNO = '" + obj.ZLUSNO.Trim() + "' ";
            }

            if (obj.ZLLGDT != 0)
            {
                strSql += " AND ZLLGDT = " + obj.ZLLGDT.ToString() + " ";
            }

            if (obj.ZLLGTM != 0)
            {
                strSql += " AND ZLLGTM = " + obj.ZLLGTM.ToString() + " ";
            }

            List<ZLOGDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZLOGDto> GetListPaging(out int intTotalPage, out int intTotalRecord, ZLOGDto obj, int intPageNumber, int intPageSize)
        {
            string strSql = "SELECT "
                    + " ZLCONO "
                    + " ,ZLBRNO "
                    + " ,ZLUSNO "
                    + " ,ZLLGDT "
                    + " ,ZLLGTM "
                    + " ,ZLLGTY "
                    + " ,ZLLGIP "
                    + " ,ZLREMA "
                    + " ,ZLSYST "
                    + " ,ZLSTAT "
                    + " ,ZLRCST "
                    + " ,ZLCRDT "
                    + " ,ZLCRTM "
                    + " ,ZLCRUS "
                    + " ,ZLCHDT "
                    + " ,ZLCHTM "
                    + " ,ZLCHUS "
                    + " ,ZUUSNA "
                    + " ,ZHUGNO "
                    + " FROM ZLOG "
					+ " LEFT JOIN ZUSR ON 1=1"
					+ "     AND ZUUSNO = ZLUSNO "
                    + " JOIN ZUG2 ON 1=1 "
                    + " AND ZHCONO = ZLCONO "
                    + " AND ZHBRNO = ZLBRNO "
                    + " AND ZHUSNO = ZLUSNO "
                    + " WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.ZLCONO))
            {
                strSql += " AND ZLCONO = '" + obj.ZLCONO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZLBRNO))
            {
                strSql += " AND ZLBRNO = '" + obj.ZLBRNO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZLUSNO))
            {
                strSql += " AND ZLUSNO = '" + obj.ZLUSNO.Trim() + "' ";
            }

			if (!string.IsNullOrEmpty(obj.ZHUGNO))
			{
				strSql += " AND ZHUGNO = '" + obj.ZHUGNO.Trim() + "' ";
			}

			if (obj.ZLLGDTFr != 0)
            {
                strSql += " AND ZLLGDT >= " + obj.ZLLGDTFr.ToString() + " ";
            }

            if (obj.ZLLGDTTo != 0)
            {
                strSql += " AND ZLLGDT <= " + obj.ZLLGDTTo.ToString() + " ";
            }

            List<ZLOGDto> dto = this.ExecutePaging(strSql, "ZLCONO, ZLBRNO, ZLUSNO, ZLLGDT, ZLLGTM", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public ZLOGDto GetUserLastStatus(ZLOGDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZLCONO "
                    + ", ZLBRNO "
                    + ", ZLUSNO "
                    + ", ZLLGDT "
                    + ", ZLLGTM "
                    + ", ZLLGTY "
                    + ", ZLLGIP "
                    + ", ZLREMA "
                    + ", ZLSYST "
                    + ", ZLSTAT "
                    + ", ZLRCST "
                    + ", ZLCRDT "
                    + ", ZLCRTM "
                    + ", ZLCRUS "
                    + ", ZLCHDT "
                    + ", ZLCHTM "
                    + ", ZLCHUS "
                    + "FROM ZLOG "
                    + "WHERE 1=1 ";

            if (obj.ZLCONO != null && obj.ZLCONO != String.Empty)
            {
                strSql += "AND ZLCONO = '" + obj.ZLCONO.Trim() + "' ";
            }

            if (obj.ZLBRNO != null && obj.ZLBRNO != String.Empty)
            {
                strSql += "AND ZLBRNO = '" + obj.ZLBRNO.Trim() + "' ";
            }

            if (obj.ZLUSNO != null && obj.ZLUSNO != String.Empty)
            {
                strSql += "AND ZLUSNO = '" + obj.ZLUSNO.Trim() + "' ";
            }

            strSql += "ORDER BY ((ZLLGDT*1000000)+ZLLGTM) DESC ";

            ZLOGDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        #endregion
    }
}
