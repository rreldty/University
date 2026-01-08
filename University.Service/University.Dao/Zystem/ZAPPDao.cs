using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dao.Base;
using University.Dto.Base;
using University.Dto.Zystem;
using System.Data;

namespace University.Dao.Zystem
{
    public class ZAPPDao : BaseDao<ZAPPDto>
    {
        #region Constructor

        public ZAPPDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<ZAPPDto> GetMapper()
        {
            Mapper<ZAPPDto> mapDto = new ZAPPMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(ZAPPDto obj)
        {
            string[] strField = new string[16];
            strField[0] = "ZACONO";
            strField[1] = "ZABRNO";
            strField[2] = "ZAAPNO";
            strField[3] = "ZAAPNA";
            strField[4] = "ZAAURL";
            strField[5] = "ZAIURL";
            strField[6] = "ZAREMA";
            strField[7] = "ZASYST";
            strField[8] = "ZASTAT";
            strField[9] = "ZARCST";
            strField[10] = "ZACRDT";
            strField[11] = "ZACRTM";
            strField[12] = "ZACRUS";
            strField[13] = "ZACHDT";
            strField[14] = "ZACHTM";
            strField[15] = "ZACHUS";

            return this.GenerateStringInsert("ZAPP", strField, obj);
        }

        public string ScriptUpdate(ZAPPDto obj)
        {
            string[] strField = new string[10];
            strField[0] = "ZAAPNA";
            strField[1] = "ZAAURL";
            strField[2] = "ZAIURL";
            strField[3] = "ZAREMA";
            strField[4] = "ZASYST";
            strField[5] = "ZASTAT";
            strField[6] = "ZARCST";
            strField[7] = "ZACHDT";
            strField[8] = "ZACHTM";
            strField[9] = "ZACHUS";

            string[] strCondition = new string[3];
            strCondition[0] = "ZACONO";
            strCondition[1] = "ZABRNO";
            strCondition[2] = "ZAAPNO";

            return this.GenerateStringUpdate("ZAPP", strCondition, strField, obj);
        }

        public string Save(ZAPPDto obj)
        {
            obj.ZASYST = BaseMethod.SystReady;
            obj.ZASTAT = BaseMethod.StatDraft;
            obj.ZACRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZACRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZACHDT = obj.ZACRDT;
            obj.ZACHTM = obj.ZACRTM;

            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data

        public string Delete(ZAPPDto obj)
        {
            string[] strCondition = new string[3];
            strCondition[0] = "ZACONO";
            strCondition[1] = "ZABRNO";
            strCondition[2] = "ZAAPNO";

            string strSql = this.GenerateStringDelete("ZAPP", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        //public ZAUTDto GetAPNO(ZAUTDto obj)
        public List<ZAPPDto> GetAPNO(ZAPPDto obj)
        {
            string strSql = "SELECT "
                            + "ZTUGNO "
                            + ",ZTAPNO "
                            + ",ZMMETY "
                            + ",ZTMENO "
                            + ",ZMMENA"
                            + ",ZPPURL"
                            + ",ZMPARM"
                            + "  FROM ZAUT "
                            + ""
                            + " left join ZUG2 on 1 = 1"
                           + " and ZHCONO = ZTCONO"
                           + " and ZHBRNO = ZTBRNO"
                           + " and ZHUGNO = ZTUGNO"
                           + " left join ZMNU on 1 = 1 "
                           + " And ZMCONO = ZTCONO "
                           + " And ZMBRNO = ZTBRNO " 
                           + " And ZMAPNO = ZTAPNO "
                           + " And ZMMENO = ZTMENO "
                           + " left join ZPGM on 1 = 1 "
                           + " And ZPCONO = ZMCONO "
                           + " And ZPBRNO = ZMBRNO "
                           + " And ZPAPNO = ZMAPNO "
                           + " And ZPPGNO = ZMPGNO "
                           + " WHERE 1=1 AND ZMMETY = 'D'"
                           + " AND ZHUSNO ='" + obj.ZHUSNO.Trim() + "' ";

            //return this.ExecuteQueryOne(strSql);
            List<ZAPPDto> dto = this.ExecuteQuery(strSql);
            return dto;

        }

        public bool IsExists(ZAPPDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZAPP "
                            + " WHERE 1=1 "
                            + " AND ZACONO = '" + obj.ZACONO.Trim() + "'"
                            + " AND ZABRNO = '" + obj.ZABRNO.Trim() + "'"
                            + " AND ZAAPNO = '" + obj.ZAAPNO.Trim() + "' "
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

        public ZAPPDto Get(ZAPPDto obj)
        {
            string[] strField = new string[18];
            strField[0] = "ZACONO";
            strField[1] = "ZABRNO";
            strField[2] = "ZAAPNO";
            strField[3] = "ZAAPNA";
            strField[4] = "ZAAURL";
            strField[5] = "ZAIURL";
            strField[6] = "ZAACLR";
            strField[7] = "ZAAPSQ";
            strField[8] = "ZAREMA";
            strField[9] = "ZASYST";
            strField[10] = "ZASTAT";
            strField[11] = "ZARCST";
            strField[12] = "ZACRDT";
            strField[13] = "ZACRTM";
            strField[14] = "ZACRUS";
            strField[15] = "ZACHDT";
            strField[16] = "ZACHTM";
            strField[17] = "ZACHUS";

            string[] strCondition = new string[3];
            strCondition[0] = "ZACONO";
            strCondition[1] = "ZABRNO";
            strCondition[2] = "ZAAPNO";

            string strSql = this.GenerateStringSelect("ZAPP", strCondition, strField, obj);
            ZAPPDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZAPPDto GetFirst(ZAPPDto obj)
        {
            string strSql = "SELECT TOP 1"
                            + " ZACONO "
                            + ", ZABRNO "
                            + ", ZAAPNO "
                            + ", ZAAPNA "
                            + ", ZAAURL "
                            + ", ZAIURL "
                            + ", ZAACLR "
                            + ", ZAAPSQ "
                            + ", ZAREMA "
                            + ", ZASYST "
                            + ", ZASTAT "
                            + ", ZARCST "
                            + ", ZACRDT "
                            + ", ZACRTM "
                            + ", ZACRUS "
                            + ", ZACHDT "
                            + ", ZACHTM "
                            + ", ZACHUS "
                            + " FROM ZAPP "
                            + " WHERE 1=1"
                            + " AND ZACONO = '" + obj.ZACONO.Trim() + "' "
                            + " AND ZABRNO = '" + obj.ZABRNO.Trim() + "' "
                            + " ORDER BY ZAAPNO ASC"
                            + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZAPPDto GetPrevious(ZAPPDto obj)
        {
            string strSql = "SELECT TOP 1"
                            + " ZACONO "
                            + ", ZABRNO "
                            + ", ZAAPNO "
                            + ", ZAAPNA "
                            + ", ZAAURL "
                            + ", ZAIURL "
                            + ", ZAACLR "
                            + ", ZAAPSQ "
                            + ", ZAREMA "
                            + ", ZASYST "
                            + ", ZASTAT "
                            + ", ZARCST "
                            + ", ZACRDT "
                            + ", ZACRTM "
                            + ", ZACRUS "
                            + ", ZACHDT "
                            + ", ZACHTM "
                            + ", ZACHUS "
                            + " FROM ZAPP "
                            + " WHERE 1=1"
                            + " AND ZACONO = '" + obj.ZACONO.Trim() + "' "
                            + " AND ZABRNO = '" + obj.ZABRNO.Trim() + "' "
                            + " AND ZAAPNO < '" + obj.ZAAPNO.Trim() + "' "
                            + " ORDER BY ZAAPNO DESC"
                            + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZAPPDto GetNext(ZAPPDto obj)
        {
            string strSql = "SELECT TOP 1"
                            + " ZACONO "
                            + ", ZABRNO "
                            + ", ZAAPNO "
                            + ", ZAAPNA "
                            + ", ZAAURL "
                            + ", ZAIURL "
                            + ", ZAACLR "
                            + ", ZAAPSQ "
                            + ", ZAREMA "
                            + ", ZASYST "
                            + ", ZASTAT "
                            + ", ZARCST "
                            + ", ZACRDT "
                            + ", ZACRTM "
                            + ", ZACRUS "
                            + ", ZACHDT "
                            + ", ZACHTM "
                            + ", ZACHUS "
                            + " FROM ZAPP "
                            + " WHERE 1=1"
                            + " AND ZACONO = '" + obj.ZACONO.Trim() + "' "
                            + " AND ZABRNO = '" + obj.ZABRNO.Trim() + "' "
                            + " AND ZAAPNO > '" + obj.ZAAPNO.Trim() + "' "
                            + " ORDER BY ZAAPNO ASC"
                            + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZAPPDto GetLast(ZAPPDto obj)
        {
            string strSql = "SELECT TOP 1"
                            + " ZACONO "
                            + ", ZABRNO "
                            + ", ZAAPNO "
                            + ", ZAAPNA "
                            + ", ZAAURL "
                            + ", ZAIURL "
                            + ", ZAACLR "
                            + ", ZAAPSQ "
                            + ", ZAREMA "
                            + ", ZASYST "
                            + ", ZASTAT "
                            + ", ZARCST "
                            + ", ZACRDT "
                            + ", ZACRTM "
                            + ", ZACRUS "
                            + ", ZACHDT "
                            + ", ZACHTM "
                            + ", ZACHUS "
                            + " FROM ZAPP "
                            + " WHERE 1=1"
                            + " AND ZACONO = '" + obj.ZACONO.Trim() + "' "
                            + " AND ZABRNO = '" + obj.ZABRNO.Trim() + "' "
                            + " ORDER BY ZAAPNO DESC"
                            + "";

            return this.ExecuteQueryOne(strSql);
        }

        public List<ZAPPDto> GetList(ZAPPDto obj)
        {
            string strSql = "SELECT "
                            + " ZACONO "
                            + ", ZABRNO "
                            + ", ZAAPNO "
                            + ", ZAAPNA "
                            + ", ZAAURL "
                            + ", ZAIURL "
                            + ", ZAACLR "
                            + ", ZAAPSQ "
                            + ", ZAREMA "
                            + ", ZASYST "
                            + ", ZASTAT "
                            + ", ZARCST "
                            + ", ZACRDT "
                            + ", ZACRTM "
                            + ", ZACRUS "
                            + ", ZACHDT "
                            + ", ZACHTM "
                            + ", ZACHUS "
                            + " FROM ZAPP WHERE 1=1 ";

            if (obj.ZACONO != null && obj.ZACONO != String.Empty)
            {
                strSql += " AND ZACONO = '" + obj.ZACONO.Trim() + "' ";
            }

            if (obj.ZABRNO != null && obj.ZABRNO != String.Empty)
            {
                strSql += " AND ZABRNO = '" + obj.ZABRNO.Trim() + "' ";
            }

            List<ZAPPDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZAPPDto> GetListByAuthorization(ZAPPDto obj)
        {
            string strSql = string.Empty;

            if (!string.IsNullOrEmpty(obj.ZHUSNO))
            {
                strSql = "SELECT DISTINCT"
                            + " ZACONO "
                            + ", ZABRNO "
                            + ", ZAAPNO "
                            + ", ZAAPNA "
                            + ", ZAAURL "
                            + ", ZAIURL "
                            + ", ZAACLR "
                            + ", ZAAPSQ "
                            + ", ZAREMA "
                            + ", ZASYST "
                            + ", ZASTAT "
                            + ", ZARCST "
                            + ", ZACRDT "
                            + ", ZACRTM "
                            + ", ZACRUS "
                            + ", ZACHDT "
                            + ", ZACHTM "
                            + ", ZACHUS "
                            + " from ZAPP "
                            + " left join ZAUT on 1=1"
                            + "     and ZTCONO = ZACONO"
                            + "     and ZTBRNO = ZABRNO"
                            + "     and ZTAPNO = ZAAPNO"
                            + " left join ZUG2 on 1=1"
                            + "     and ZHCONO = ZTCONO"
                            + "     and ZHBRNO = ZTBRNO"
                            + "     and ZHUGNO = ZTUGNO"
                            + " left join ZUG1 on 1=1"
                            + "     and ZGCONO = ZHCONO"
                            + "     and ZGBRNO = ZHBRNO"
                            + "     and ZGUGNO = ZHUGNO"
                            + " WHERE 1=1 "
                            + " AND ZGRCST = 1"
                            + "";

                if (obj.ZHUSNO != null && obj.ZHUSNO != String.Empty)
                {
                    strSql += " AND ZHUSNO = '" + obj.ZHUSNO.Trim() + "' ";
                }
            }
            else
            {
                strSql = "SELECT DISTINCT"
                            + " ZACONO "
                            + ", ZABRNO "
                            + ", ZAAPNO "
                            + ", ZAAPNA "
                            + ", ZAAURL "
                            + ", ZAIURL "
                            + ", ZAACLR "
                            + ", ZAAPSQ "
                            + ", ZAREMA "
                            + ", ZASYST "
                            + ", ZASTAT "
                            + ", ZARCST "
                            + ", ZACRDT "
                            + ", ZACRTM "
                            + ", ZACRUS "
                            + ", ZACHDT "
                            + ", ZACHTM "
                            + ", ZACHUS "
                            + " FROM ZAPP "
                            + " WHERE 1=1 "
                            + "";
            }

            strSql += " ORDER BY ZAAPSQ ";

            List<ZAPPDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZAPPDto> GetListPaging(ZAPPDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                            + " ZACONO "
                            + ", ZABRNO "
                            + ", ZAAPNO "
                            + ", ZAAPNA "
                            + ", ZAAURL "
                            + ", ZAIURL "
                            + ", ZAACLR "
                            + ", ZAAPSQ "
                            + ", ZAREMA "
                            + ", ZASYST "
                            + ", ZASTAT "
                            + ", ZARCST "
                            + ", ZACRDT "
                            + ", ZACRTM "
                            + ", ZACRUS "
                            + ", ZACHDT "
                            + ", ZACHTM "
                            + ", ZACHUS "
                            + " FROM ZAPP WHERE 1=1 ";

            if (obj.ZACONO != null && obj.ZACONO != String.Empty)
            {
                strSql += " AND ZACONO = '" + obj.ZACONO.Trim() + "' ";
            }

            if (obj.ZABRNO != null && obj.ZABRNO != String.Empty)
            {
                strSql += " AND ZABRNO = '" + obj.ZABRNO.Trim() + "' ";
            }

            if (obj.ZAAPNO != null && obj.ZAAPNO != String.Empty)
            {
                strSql += " AND ZAAPNO LIKE '%" + obj.ZAAPNO.Trim() + "%' ";
            }

            if (obj.ZAAPNA != null && obj.ZAAPNA != String.Empty)
            {
                strSql += " AND ZAAPNA LIKE '%" + obj.ZAAPNA.Trim() + "%' ";
            }

            List<ZAPPDto> dto = this.ExecutePaging(strSql, "ZACONO, ZABRNO, ZAAPNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public DataTable GetDetailZR010A(ZAPPDto obj)
        {
            string strSql = "SELECT "
                            + "ZAAPNO "
                            + ",ZAAPNA "
                            + ",ZRVANA "
                            + "FROM ZAPP "
                + "LEFT JOIN ZVAR ON 1=1 "
                + "AND ZRCONO = '' "
                + "AND ZRBRNO = '' "
                + "AND ZRVATY = 'RCST' "
                + "AND ZRVAVL = ZARCST "
                + "WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(obj.ZAAPNO))
            {
                strSql += "AND ZAAPNO >= '" + obj.ZAAPNOFr.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZAAPNOTo))
            {
                strSql += "AND ZAAPNO <= '" + obj.ZAAPNOTo.Trim() + "' ";
            }

            strSql += "ORDER BY ZAAPNO ";

            return this.ExecuteDataTable(strSql);
        }


        #endregion
    }
}
