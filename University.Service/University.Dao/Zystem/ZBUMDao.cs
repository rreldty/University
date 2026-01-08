using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dto.Base;
using University.Dao.Base;
using University.Dto.Zystem;

namespace University.Dao.Zystem
{
    public class ZBUMDao : BaseDao<ZBUMDto>
    {
        #region Constructor
        
        public ZBUMDao()
        {
            this.MainDataSource = DataSource.University;
        }
        
        #endregion

        #region Abstract Class Implementation
        
        protected override Mapper<ZBUMDto> GetMapper()
        {
            Mapper<ZBUMDto> mapDto = new MapZBUMDto();
            return mapDto;
        }
        
        #endregion

        #region Save Data

        public string ScriptInsert(ZBUMDto obj)
        {
            string[] strField = new string[13];
            strField[0] = "ZVCONO";
            strField[1] = "ZVBRNO";
            strField[2] = "ZVUSNO";
            strField[3] = "ZVREMA";
            strField[4] = "ZVSYST";
            strField[5] = "ZVSTAT";
            strField[6] = "ZVRCST";
            strField[7] = "ZVCRDT";
            strField[8] = "ZVCRTM";
            strField[9] = "ZVCRUS";
            strField[10] = "ZVCHDT";
            strField[11] = "ZVCHTM";
            strField[12] = "ZVCHUS";

            return this.GenerateStringInsert("ZBUM", strField, obj);
        }

        public string ScriptUpdate(ZBUMDto obj)
        {
            string[] strField = new string[7];
            strField[0] = "ZVREMA";
            strField[1] = "ZVSYST";
            strField[2] = "ZVSTAT";
            strField[3] = "ZVRCST";
            strField[4] = "ZVCHDT";
            strField[5] = "ZVCHTM";
            strField[6] = "ZVCHUS";

            string[] strCondition = new string[3];
            strCondition[0] = "ZVCONO";
            strCondition[1] = "ZVBRNO";
            strCondition[2] = "ZVUSNO";

            return this.GenerateStringUpdate("ZBUM", strCondition, strField, obj);
        }

        public string Save(ZBUMDto obj)
        {
            obj.ZVSYST = BaseMethod.SystReady;
            obj.ZVSTAT = BaseMethod.StatDraft;
            obj.ZVCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZVCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZVCHDT = obj.ZVCRDT;
            obj.ZVCHTM = obj.ZVCRTM;

            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data
        
        public string Delete(ZBUMDto obj)
        {
            string[] strCondition = new string[3];
            strCondition[0] = "ZVCONO";
            strCondition[1] = "ZVBRNO";
            strCondition[2] = "ZVUSNO";

            string strSql = this.GenerateStringDelete("ZBUM", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Get Data

        public bool IsExists(ZBUMDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZBUM "
                            + " WHERE 1=1 "
                            + " AND ZVCONO = '" + obj.ZVCONO.Trim() + "'"
                            + " AND ZVBRNO = '" + obj.ZVBRNO.Trim() + "'"
                            + " AND ZVUSNO = '" + obj.ZVUSNO.Trim() + "' "
                            + " )"
                            + " THEN 1 ELSE 0 END"
                            + "";

            Object _obj = this.ExecuteDbScalar(strSql);

            if (_obj == DBNull.Value)
            {
                return false;
            }
            else
            {
                if (Convert.ToInt32(_obj) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public ZBUMDto Get(ZBUMDto obj)
        {
            string[] strField = new string[13];
            strField[0] = "ZVCONO";
            strField[1] = "ZVBRNO";
            strField[2] = "ZVUSNO";
            strField[3] = "ZVREMA";
            strField[4] = "ZVSYST";
            strField[5] = "ZVSTAT";
            strField[6] = "ZVRCST";
            strField[7] = "ZVCRDT";
            strField[8] = "ZVCRTM";
            strField[9] = "ZVCRUS";
            strField[10] = "ZVCHDT";
            strField[11] = "ZVCHTM";
            strField[12] = "ZVCHUS";

            string[] strCondition = new string[3];
            strCondition[0] = "ZVCONO";
            strCondition[1] = "ZVBRNO";
            strCondition[2] = "ZVUSNO";

            string strSql = this.GenerateStringSelect("ZBUM", strCondition, strField, obj);
            ZBUMDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZBUMDto GetFirst(ZBUMDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + " FROM ZBUM "
                    + " WHERE 1=1 "
                    + " ORDER BY ZVCONO ASC"
                    + "";

            ZBUMDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZBUMDto GetPrevious(ZBUMDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + " FROM ZBUM "
                    + " WHERE 1=1 "
                    + " AND ZVCONO = '" + obj.ZVCONO.Trim() + "' "
                    + " AND ZVBRNO = '" + obj.ZVBRNO.Trim() + "' "
                    + " AND ZVUSNO < '" + obj.ZVUSNO.Trim() + "' "
                    + " ORDER BY ZVUSNO DESC"
                    + "";

            ZBUMDto dto = this.ExecuteQueryOne(strSql);
            if (dto == null)
            {
                ZBUMDto obj2 = null;
                GetPrevBRNO(out obj2, obj);
                dto = obj2;
                if (dto == null)
                {
                    GetPrevCONO(out obj2, obj);
                    dto = obj2;
                }
                return dto;
            }
            else
                return dto;
        }

        public ZBUMDto GetPrevBRNO(out ZBUMDto obj2, ZBUMDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + " FROM ZBUM "
                    + " WHERE 1=1 "
                    + " AND ZVCONO = '" + obj.ZVCONO.Trim() + "' "
                    + " AND ZVBRNO < '" + obj.ZVBRNO.Trim() + "' "
                    + " ORDER BY ZVBRNO DESC, ZVUSNO DESC"
                    + "";
            ZBUMDto dto = this.ExecuteQueryOne(strSql);
            obj2 = dto;
            return dto;
        }

        public ZBUMDto GetPrevCONO(out ZBUMDto obj2, ZBUMDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + " FROM ZBUM "
                    + " WHERE 1=1 "
                    + " AND ZVCONO < '" + obj.ZVCONO.Trim() + "' "
                    + " ORDER BY ZVBRNO DESC, ZVUSNO DESC"
                    + "";
            ZBUMDto dto = this.ExecuteQueryOne(strSql);
            obj2 = dto;
            return dto;
        }

        public ZBUMDto GetNext(ZBUMDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + "FROM ZBUM "
                    + " WHERE 1=1 "
                    + " AND ZVCONO = '" + obj.ZVCONO.Trim() + "' "
                    + " AND ZVBRNO = '" + obj.ZVBRNO.Trim() + "' "
                    + " AND ZVUSNO > '" + obj.ZVUSNO.Trim() + "' "
                    + " ORDER BY ZVUSNO ASC"
                    + "";

            ZBUMDto dto = this.ExecuteQueryOne(strSql);
            if (dto == null)
            {
                ZBUMDto obj2 = null;
                GetNextBRNO(out obj2, obj);
                dto = obj2;
                if (dto == null)
                {
                    GetNextCONO(out obj2, obj);
                    dto = obj2;
                }
                return dto;
            }
            else
                return dto;
        }

        public ZBUMDto GetNextBRNO(out ZBUMDto obj2, ZBUMDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + " FROM ZBUM "
                    + " WHERE 1=1 "
                    + " AND ZVCONO = '" + obj.ZVCONO.Trim() + "' "
                    + " AND ZVBRNO > '" + obj.ZVBRNO.Trim() + "' "
                    + " ORDER BY ZVCONO, ZVBRNO, ZVUSNO ASC"
                    + "";
            ZBUMDto dto = this.ExecuteQueryOne(strSql);
            obj2 = dto;
            return dto;
        }

        public ZBUMDto GetNextCONO(out ZBUMDto obj2, ZBUMDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + " FROM ZBUM "
                    + " WHERE 1=1 "
                    + " AND ZVCONO > '" + obj.ZVCONO.Trim() + "' "
                    + " ORDER BY ZVCONO, ZVBRNO, ZVUSNO ASC"
                    + "";
            ZBUMDto dto = this.ExecuteQueryOne(strSql);
            obj2 = dto;
            return dto;
        }

        public ZBUMDto GetLast(ZBUMDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + "FROM ZBUM "
                    + " WHERE 1=1 "
                    //+ " AND ZVCONO = '" + obj.ZVCONO.Trim() + "' "
                    //+ " AND ZVBRNO = '" + obj.ZVBRNO.Trim() + "' "
                    + " ORDER BY ZVCONO DESC"
                    + "";

            ZBUMDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<ZBUMDto> GetList(ZBUMDto obj)
        {
            string strSql = "SELECT "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + ", ZBBRNA "
                    + "FROM ZBUM "
                    + "JOIN ZBRC ON ZVCONO = ZBCONO AND ZVBRNO = ZBBRNO "
                    + "WHERE 1=1 ";

            if (obj.ZVCONO != null && obj.ZVCONO != String.Empty)
            {
                strSql += "AND ZVCONO = '" + obj.ZVCONO.Trim() + "' ";
            }

            if (obj.ZVBRNO != null && obj.ZVBRNO != String.Empty)
            {
                strSql += "AND ZVBRNO = '" + obj.ZVBRNO.Trim() + "' ";
            }

            if (obj.ZVUSNO != null && obj.ZVUSNO != String.Empty)
            {
                strSql += "AND ZVUSNO = '" + obj.ZVUSNO.Trim() + "' ";
            }

            List<ZBUMDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZBUMDto> GetListPaging(ZBUMDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                    + " ZVCONO "
                    + ", ZVBRNO "
                    + ", ZVUSNO "
                    + ", ZVREMA "
                    + ", ZVSYST "
                    + ", ZVSTAT "
                    + ", ZVRCST "
                    + ", ZVCRDT "
                    + ", ZVCRTM "
                    + ", ZVCRUS "
                    + ", ZVCHDT "
                    + ", ZVCHTM "
                    + ", ZVCHUS "
                    + ", ZBBRNA "
                    + "FROM ZBUM "
                    + "JOIN ZBRC ON ZVCONO = ZBCONO AND ZVBRNO = ZBBRNO "
                    + "WHERE 1=1 ";

            if (obj.ZVCONO != null && obj.ZVCONO != String.Empty)
            {
                strSql += "AND ZVCONO = '" + obj.ZVCONO.Trim() + "' ";
            }

            if (obj.ZVBRNO != null && obj.ZVBRNO != String.Empty)
            {
                strSql += "AND ZVBRNO = '" + obj.ZVBRNO.Trim() + "' ";
            }

            if (obj.ZVUSNO != null && obj.ZVUSNO != String.Empty)
            {
                strSql += "AND ZVUSNO = '" + obj.ZVUSNO.Trim() + "' ";
            }

            List<ZBUMDto> dto = this.ExecutePaging(strSql, "ZVCONO, ZVBRNO, ZVUSNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        #endregion
    }
}