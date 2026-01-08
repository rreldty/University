using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dao.Base;
using University.Dto.Zystem;
using University.Dto.Base;

namespace University.Dao.Zystem
{
    public class ZPGMDao : BaseDao<ZPGMDto>
    {
        #region "Constructor"

        public ZPGMDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region "Abstract Class Implementation"

        protected override Mapper<ZPGMDto> GetMapper()
        {
            Mapper<ZPGMDto> mapDto = new ZPGMMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(ZPGMDto obj)
        {
            string[] strField = new string[16];
            strField[0] = "ZPCONO";
            strField[1] = "ZPBRNO";
            strField[2] = "ZPAPNO";
            strField[3] = "ZPPGNO";
            strField[4] = "ZPPGNA";
            strField[5] = "ZPPURL";
            strField[6] = "ZPREMA";
            strField[7] = "ZPSYST";
            strField[8] = "ZPSTAT";
            strField[9] = "ZPRCST";
            strField[10] = "ZPCRDT";
            strField[11] = "ZPCRTM";
            strField[12] = "ZPCRUS";
            strField[13] = "ZPCHDT";
            strField[14] = "ZPCHTM";
            strField[15] = "ZPCHUS";

            return this.GenerateStringInsert("ZPGM", strField, obj);
        }

        public string ScriptUpdate(ZPGMDto obj)
        {
            string[] strField = new string[9];
            strField[0] = "ZPPGNA";
            strField[1] = "ZPPURL";
            strField[2] = "ZPREMA";
            strField[3] = "ZPSYST";
            strField[4] = "ZPSTAT";
            strField[5] = "ZPRCST";
            strField[6] = "ZPCHDT";
            strField[7] = "ZPCHTM";
            strField[8] = "ZPCHUS";

            string[] strCondition = new string[4];
            strCondition[0] = "ZPCONO";
            strCondition[1] = "ZPBRNO";
            strCondition[2] = "ZPAPNO";
            strCondition[3] = "ZPPGNO";

            return this.GenerateStringUpdate("ZPGM", strCondition, strField, obj);
        }

        public string Save(ZPGMDto obj)
        {
            obj.ZPSYST = BaseMethod.SystReady;
            obj.ZPSTAT = BaseMethod.StatDraft;
            obj.ZPCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZPCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZPCHDT = obj.ZPCRDT;
            obj.ZPCHTM = obj.ZPCRTM;

            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data
        public string Delete(ZPGMDto obj)
        {
            string[] strCondition = new string[4];
            strCondition[0] = "ZPCONO";
            strCondition[1] = "ZPBRNO";
            strCondition[2] = "ZPAPNO";
            strCondition[3] = "ZPPGNO";

            string strSql = this.GenerateStringDelete("ZPGM", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }
        #endregion

        #region Select Data

        public bool IsExists(ZPGMDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZPGM "
                            + " WHERE 1=1 "
                            + " AND ZPCONO = '" + obj.ZPCONO.Trim() + "'"
                            + " AND ZPBRNO = '" + obj.ZPBRNO.Trim() + "'"
                            + " AND ZPAPNO = '" + obj.ZPAPNO.Trim() + "'"
                            + " AND ZPPGNO = '" + obj.ZPPGNO.Trim() + "'"
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

        public ZPGMDto Get(ZPGMDto obj)
        {
            string[] strField = new string[16];
            strField[0] = "ZPCONO";
            strField[1] = "ZPBRNO";
            strField[2] = "ZPAPNO";
            strField[3] = "ZPPGNO";
            strField[4] = "ZPPGNA";
            strField[5] = "ZPPURL";
            strField[6] = "ZPREMA";
            strField[7] = "ZPSYST";
            strField[8] = "ZPSTAT";
            strField[9] = "ZPRCST";
            strField[10] = "ZPCRDT";
            strField[11] = "ZPCRTM";
            strField[12] = "ZPCRUS";
            strField[13] = "ZPCHDT";
            strField[14] = "ZPCHTM";
            strField[15] = "ZPCHUS";

            string[] strCondition = new string[4];
            strCondition[0] = "ZPCONO";
            strCondition[1] = "ZPBRNO";
            strCondition[2] = "ZPAPNO";
            strCondition[3] = "ZPPGNO";

            string strSql = this.GenerateStringSelect("ZPGM", strCondition, strField, obj);
            ZPGMDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZPGMDto GetFirst(ZPGMDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + " ZPCONO "
                    + ", ZPBRNO "
                    + ", ZPAPNO "
                    + ", ZPPGNO "
                    + ", ZPPGNA "
                    + ", ZPPURL "
                    + ", ZPREMA "
                    + ", ZPSYST "
                    + ", ZPSTAT "
                    + ", ZPRCST "
                    + ", ZPCRDT "
                    + ", ZPCRTM "
                    + ", ZPCRUS "
                    + ", ZPCHDT "
                    + ", ZPCHTM "
                    + ", ZPCHUS "
                    + "FROM ZPGM "
                    + "WHERE 1=1 "
                    + "AND ZPCONO = '" + obj.ZPCONO.Trim() + "' "
                    + "AND ZPBRNO = '" + obj.ZPBRNO.Trim() + "' "
                    + "ORDER BY ZPCONO, ZPBRNO, ZPPGNO"
                    + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZPGMDto GetPrevious(ZPGMDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + " ZPCONO "
                    + ", ZPBRNO "
                    + ", ZPAPNO "
                    + ", ZPPGNO "
                    + ", ZPPGNA "
                    + ", ZPPURL "
                    + ", ZPREMA "
                    + ", ZPSYST "
                    + ", ZPSTAT "
                    + ", ZPRCST "
                    + ", ZPCRDT "
                    + ", ZPCRTM "
                    + ", ZPCRUS "
                    + ", ZPCHDT "
                    + ", ZPCHTM "
                    + ", ZPCHUS "
                    + "FROM ZPGM "
                    + "WHERE 1=1 "
                    + "AND ZPCONO = '" + obj.ZPCONO.Trim() + "' "
                    + "AND ZPBRNO = '" + obj.ZPBRNO.Trim() + "' "
                    + "AND ZPPGNO < '" + obj.ZPPGNO.Trim() + "' "
                    + "ORDER BY ZPCONO, ZPBRNO, ZPPGNO DESC"
                    + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZPGMDto GetNext(ZPGMDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + " ZPCONO "
                    + ", ZPBRNO "
                    + ", ZPAPNO "
                    + ", ZPPGNO "
                    + ", ZPPGNA "
                    + ", ZPPURL "
                    + ", ZPREMA "
                    + ", ZPSYST "
                    + ", ZPSTAT "
                    + ", ZPRCST "
                    + ", ZPCRDT "
                    + ", ZPCRTM "
                    + ", ZPCRUS "
                    + ", ZPCHDT "
                    + ", ZPCHTM "
                    + ", ZPCHUS "
                    + "FROM ZPGM "
                    + "WHERE 1=1 "
                    + "AND ZPCONO = '" + obj.ZPCONO.Trim() + "' "
                    + "AND ZPBRNO = '" + obj.ZPBRNO.Trim() + "' "
                    + "AND ZPPGNO > '" + obj.ZPPGNO.Trim() + "' "
                    + "ORDER BY ZPCONO, ZPBRNO, ZPPGNO ASC"
                    + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZPGMDto GetLast(ZPGMDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + " ZPCONO "
                    + ", ZPBRNO "
                    + ", ZPAPNO "
                    + ", ZPPGNO "
                    + ", ZPPGNA "
                    + ", ZPPURL "
                    + ", ZPREMA "
                    + ", ZPSYST "
                    + ", ZPSTAT "
                    + ", ZPRCST "
                    + ", ZPCRDT "
                    + ", ZPCRTM "
                    + ", ZPCRUS "
                    + ", ZPCHDT "
                    + ", ZPCHTM "
                    + ", ZPCHUS "
                    + "FROM ZPGM "
                    + "WHERE 1=1 "
                    + "AND ZPCONO = '" + obj.ZPCONO.Trim() + "' "
                    + "AND ZPBRNO = '" + obj.ZPBRNO.Trim() + "' "
                    + "ORDER BY ZPCONO, ZPBRNO, ZPPGNO DESC"
                    + "";

            return this.ExecuteQueryOne(strSql);
        }


        public List<ZPGMDto> GetList(ZPGMDto obj)
        {
            string strSql = "SELECT "
                    + " ZPCONO "
                    + ", ZPBRNO "
                    + ", ZPAPNO "
                    + ", ZPPGNO "
                    + ", ZPPGNA "
                    + ", ZPPURL "
                    + ", ZPREMA "
                    + ", ZPSYST "
                    + ", ZPSTAT "
                    + ", ZPRCST "
                    + ", ZPCRDT "
                    + ", ZPCRTM "
                    + ", ZPCRUS "
                    + ", ZPCHDT "
                    + ", ZPCHTM "
                    + ", ZPCHUS "
                    + "FROM ZPGM WHERE 1=1 ";

            if (obj.ZPCONO != null && obj.ZPCONO != String.Empty)
            {
                strSql += "AND ZPCONO = '" + obj.ZPCONO.Trim() + "' ";
            }

            if (obj.ZPBRNO != null && obj.ZPBRNO != String.Empty)
            {
                strSql += "AND ZPBRNO = '" + obj.ZPBRNO.Trim() + "' ";
            }

            if (obj.ZPAPNO != null && obj.ZPAPNO != String.Empty)
            {
                strSql += "AND ZPAPNO = '" + obj.ZPAPNO.Trim() + "' ";
            }

            if (obj.ZPPGNO != null && obj.ZPPGNO != String.Empty)
            {
                strSql += "AND ZPPGNO LIKE '%" + obj.ZPPGNO.Trim() + "%' ";
            }

            if (obj.ZPPGNA != null && obj.ZPPGNA != String.Empty)
            {
                strSql += "AND ZPPGNA LIKE '%" + obj.ZPPGNA.Trim() + "%' ";
            }

            List<ZPGMDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }
        public List<ZPGMDto> GetListPaging(ZPGMDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                    + " ZPCONO "
                    + ", ZPBRNO "
                    + ", ZPAPNO "
                    + ", ZPPGNO "
                    + ", ZPPGNA "
                    + ", ZPPURL "
                    + ", ZPREMA "
                    + ", ZPSYST "
                    + ", ZPSTAT "
                    + ", ZPRCST "
                    + ", ZPCRDT "
                    + ", ZPCRTM "
                    + ", ZPCRUS "
                    + ", ZPCHDT "
                    + ", ZPCHTM "
                    + ", ZPCHUS "
                    + "FROM ZPGM WHERE 1=1 ";

            if (obj.ZPCONO != null && obj.ZPCONO != String.Empty)
            {
                strSql += "AND ZPCONO = '" + obj.ZPCONO.Trim() + "' ";
            }

            if (obj.ZPBRNO != null && obj.ZPBRNO != String.Empty)
            {
                strSql += "AND ZPBRNO = '" + obj.ZPBRNO.Trim() + "' ";
            }

            if (obj.ZPAPNO != null && obj.ZPAPNO != String.Empty)
            {
                strSql += "AND ZPAPNO = '" + obj.ZPAPNO.Trim() + "' ";
            }

            if (obj.ZPPGNO != null && obj.ZPPGNO != String.Empty)
            {
                strSql += "AND ZPPGNO LIKE '%" + obj.ZPPGNO.Trim() + "%' ";
            }

            if (obj.ZPPGNA != null && obj.ZPPGNA != String.Empty)
            {
                strSql += "AND ZPPGNA LIKE '%" + obj.ZPPGNA.Trim() + "%' ";
            }

            List<ZPGMDto> dto = this.ExecutePaging(strSql, "ZPCONO, ZPBRNO, ZPAPNO, ZPPGNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }
        #endregion

    }
}
