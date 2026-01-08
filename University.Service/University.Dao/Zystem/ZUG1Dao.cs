using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dao.Base;
using University.Dto.Zystem;
using University.Dto.Base;
using System.Runtime.InteropServices;

namespace University.Dao.Zystem
{
    public class ZUG1Dao : BaseDao<ZUG1Dto>
    {
        #region "Constructor"

        public ZUG1Dao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region "Abstract Class Implementation"

        protected override Mapper<ZUG1Dto> GetMapper()
        {
            Mapper<ZUG1Dto> mapDto = new ZUG1MappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(ZUG1Dto obj)
        {
            string[] strField = new string[14];
            strField[0] = "ZGCONO";
            strField[1] = "ZGBRNO";
            strField[2] = "ZGUGNO";
            strField[3] = "ZGUGNA";
            strField[4] = "ZGREMA";
            strField[5] = "ZGSYST";
            strField[6] = "ZGSTAT";
            strField[7] = "ZGRCST";
            strField[8] = "ZGCRDT";
            strField[9] = "ZGCRTM";
            strField[10] = "ZGCRUS";
            strField[11] = "ZGCHDT";
            strField[12] = "ZGCHTM";
            strField[13] = "ZGCHUS";

            return this.GenerateStringInsert("ZUG1", strField, obj);
        }

        public string ScriptUpdate(ZUG1Dto obj)
        {
            string[] strField = new string[8];
            strField[0] = "ZGUGNA";
            strField[1] = "ZGREMA";
            strField[2] = "ZGSYST";
            strField[3] = "ZGSTAT";
            strField[4] = "ZGRCST";
            strField[5] = "ZGCHDT";
            strField[6] = "ZGCHTM";
            strField[7] = "ZGCHUS";

            string[] strCondition = new string[3];
            strCondition[0] = "ZGCONO";
            strCondition[1] = "ZGBRNO";
            strCondition[2] = "ZGUGNO";

            return this.GenerateStringUpdate("ZUG1", strCondition, strField, obj);
        }

        public string Save(ZUG1Dto obj)
        {
            obj.ZGSYST = BaseMethod.SystReady;
            obj.ZGSTAT = BaseMethod.StatDraft;
            obj.ZGCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZGCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZGCHDT = obj.ZGCRDT;
            obj.ZGCHTM = obj.ZGCRTM;

            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        public string SaveWithLine(ZUG1Dto obj, ZUG2Dto objLine)
        {
            string strResult = string.Empty;
            List<string> lstSql = new List<string>();

            obj.ZGSYST = BaseMethod.SystReady;
            obj.ZGSTAT = BaseMethod.StatDraft;
            obj.ZGCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZGCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZGCHDT = obj.ZGCRDT;
            obj.ZGCHTM = obj.ZGCRTM;

            if (!IsExists(obj))
                lstSql.Add(ScriptInsert(obj));
            else
                lstSql.Add(ScriptUpdate(obj));

            if (objLine != null)
            {
                objLine.ZHSYST = BaseMethod.SystReady;
                objLine.ZHSTAT = BaseMethod.StatDraft;
                objLine.ZHCRDT = BaseMethod.DateToNumeric(DateTime.Now);
                objLine.ZHCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
                objLine.ZHCHDT = objLine.ZHCRDT;
                objLine.ZHCHTM = objLine.ZHCRTM;

                ZUG2Dao daoZUG2 = new ZUG2Dao();
                if (!daoZUG2.IsExists(objLine))
                    lstSql.Add(daoZUG2.ScriptInsert(objLine));
                else
                    lstSql.Add(daoZUG2.ScriptUpdate(objLine));
            }

            return ExecuteDbNonQueryTransaction(lstSql);
        }

        public string SaveWithLine(ZUG1Dto objLine)
        {
            List<ZUSRDto> lstZUSR = objLine.lstZUSR;

            string strResult = string.Empty;
            List<string> lstSql = new List<string>();

            lstSql.Add("UPDATE ZUG1 SET"
                        + " ZGSYST = '" + BaseMethod.SystReady + "'"
                        + ", ZGCHDT = CONVERT(VARCHAR(8), GETDATE(), 112) "
                        + ", ZGCHTM = REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','') "
                        + ", ZGCHUS = '" + objLine.ZGCHUS + "'"
                        + " WHERE 1=1"
                        + "     AND ZGCONO = '" + objLine.ZGCONO.Trim() + "'"
                        + "     AND ZGBRNO = '" + objLine.ZGBRNO.Trim() + "'"
                        + "     AND ZGUGNO = '" + objLine.ZGUGNO.Trim() + "'"
                        + "");

            if (lstZUSR != null)
            {
                ZUG2Dao daoZUG2 = new ZUG2Dao();

                foreach (ZUSRDto objZUSR in lstZUSR)
                {
                    if (objZUSR.IsSelected == true)
                    {
                        ZUG2Dto dto = new ZUG2Dto();
                        dto.ZHCONO = objLine.ZGCONO;
                        dto.ZHBRNO = objLine.ZGBRNO;
                        dto.ZHUGNO = objLine.ZGUGNO;
                        dto.ZHUSNO = objZUSR.ZUUSNO;
                        dto.ZHSYST = BaseMethod.SystReady;
                        dto.ZHSTAT = BaseMethod.StatDraft;
                        dto.ZHRCST = objLine.ZGRCST;
                        dto.ZHCRDT = BaseMethod.DateToNumeric(DateTime.Now);
                        dto.ZHCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
                        dto.ZHCHUS = objLine.ZGCHUS;
                        dto.ZHCHDT = objLine.ZGCRDT;
                        dto.ZHCHTM = objLine.ZGCRTM;
                        dto.ZHCHUS = objLine.ZGCHUS;

                        if (!daoZUG2.IsExists(dto))
                            lstSql.Add(daoZUG2.ScriptInsert(dto));
                        else
                            lstSql.Add(daoZUG2.ScriptUpdate(dto));
                    }
                }
            }


            return ExecuteDbNonQueryTransaction(lstSql);
        }
        #endregion

        #region Delete Data

        public string Delete(ZUG1Dto obj)
        {
            string[] strCondition = new string[3];
            strCondition[0] = "ZGCONO";
            strCondition[1] = "ZGBRNO";
            strCondition[2] = "ZGUGNO";

            string strSql = this.GenerateStringDelete("ZUG1", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        public bool IsExists(ZUG1Dto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZUG1 "
                            + " WHERE 1=1 "
                            + " AND ZGCONO = '" + obj.ZGCONO.Trim() + "'"
                            + " AND ZGBRNO = '" + obj.ZGBRNO.Trim() + "'"
                            + " AND ZGUGNO = '" + obj.ZGUGNO.Trim() + "'"
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

        public ZUG1Dto Get(ZUG1Dto obj)
        {
            string[] strField = new string[14];
            strField[0] = "ZGCONO";
            strField[1] = "ZGBRNO";
            strField[2] = "ZGUGNO";
            strField[3] = "ZGUGNA";
            strField[4] = "ZGREMA";
            strField[5] = "ZGSYST";
            strField[6] = "ZGSTAT";
            strField[7] = "ZGRCST";
            strField[8] = "ZGCRDT";
            strField[9] = "ZGCRTM";
            strField[10] = "ZGCRUS";
            strField[11] = "ZGCHDT";
            strField[12] = "ZGCHTM";
            strField[13] = "ZGCHUS";

            string[] strCondition = new string[3];
            strCondition[0] = "ZGCONO";
            strCondition[1] = "ZGBRNO";
            strCondition[2] = "ZGUGNO";

            string strSql = this.GenerateStringSelect("ZUG1", strCondition, strField, obj);
            ZUG1Dto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUG1Dto GetFirst(ZUG1Dto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZGCONO "
                    + ", ZGBRNO "
                    + ", ZGUGNO "
                    + ", ZGUGNA "
                    + ", ZGREMA "
                    + ", ZGSYST "
                    + ", ZGSTAT "
                    + ", ZGRCST "
                    + ", ZGCRDT "
                    + ", ZGCRTM "
                    + ", ZGCRUS "
                    + ", ZGCHDT "
                    + ", ZGCHTM "
                    + ", ZGCHUS "
                    + " FROM ZUG1 "
                    + " WHERE 1=1 "
                    + " AND ZGCONO = '" + obj.ZGCONO.Trim() + "' "
                    + " AND ZGBRNO = '" + obj.ZGBRNO.Trim() + "' "
                    + " ORDER BY ZGUGNO ASC"
                    + "";

            ZUG1Dto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUG1Dto GetPrevious(ZUG1Dto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZGCONO "
                    + ", ZGBRNO "
                    + ", ZGUGNO "
                    + ", ZGUGNA "
                    + ", ZGREMA "
                    + ", ZGSYST "
                    + ", ZGSTAT "
                    + ", ZGRCST "
                    + ", ZGCRDT "
                    + ", ZGCRTM "
                    + ", ZGCRUS "
                    + ", ZGCHDT "
                    + ", ZGCHTM "
                    + ", ZGCHUS "
                    + " FROM ZUG1 "
                    + " WHERE 1=1 "
                    + " AND ZGCONO = '" + obj.ZGCONO.Trim() + "' "
                    + " AND ZGBRNO = '" + obj.ZGBRNO.Trim() + "' "
                    + " AND ZGUGNO < '" + obj.ZGUGNO.Trim() + "' "
                    + " ORDER BY ZGUGNO DESC"
                    + "";

            ZUG1Dto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUG1Dto GetNext(ZUG1Dto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZGCONO "
                    + ", ZGBRNO "
                    + ", ZGUGNO "
                    + ", ZGUGNA "
                    + ", ZGREMA "
                    + ", ZGSYST "
                    + ", ZGSTAT "
                    + ", ZGRCST "
                    + ", ZGCRDT "
                    + ", ZGCRTM "
                    + ", ZGCRUS "
                    + ", ZGCHDT "
                    + ", ZGCHTM "
                    + ", ZGCHUS "
                    + " FROM ZUG1 "
                    + " WHERE 1=1 "
                    + " AND ZGCONO = '" + obj.ZGCONO.Trim() + "' "
                    + " AND ZGBRNO = '" + obj.ZGBRNO.Trim() + "' "
                    + " AND ZGUGNO > '" + obj.ZGUGNO.Trim() + "' "
                    + " ORDER BY ZGUGNO ASC"
                    + "";

            ZUG1Dto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUG1Dto GetLast(ZUG1Dto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZGCONO "
                    + ", ZGBRNO "
                    + ", ZGUGNO "
                    + ", ZGUGNA "
                    + ", ZGREMA "
                    + ", ZGSYST "
                    + ", ZGSTAT "
                    + ", ZGRCST "
                    + ", ZGCRDT "
                    + ", ZGCRTM "
                    + ", ZGCRUS "
                    + ", ZGCHDT "
                    + ", ZGCHTM "
                    + ", ZGCHUS "
                    + " FROM ZUG1 "
                    + " WHERE 1=1 "
                    + " AND ZGCONO = '" + obj.ZGCONO.Trim() + "' "
                    + " AND ZGBRNO = '" + obj.ZGBRNO.Trim() + "' "
                    + " ORDER BY ZGUGNO DESC"
                    + "";

            ZUG1Dto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<ZUG1Dto> GetListPaging(ZUG1Dto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                    + " ZGCONO "
                    + ", ZGBRNO "
                    + ", ZGUGNO "
                    + ", ZGUGNA "
                    + ", ZGREMA "
                    + ", ZGSYST "
                    + ", ZGSTAT "
                    + ", ZGRCST "
                    + ", ZGCRDT "
                    + ", ZGCRTM "
                    + ", ZGCRUS "
                    + ", ZGCHDT "
                    + ", ZGCHTM "
                    + ", ZGCHUS "
                    + "FROM ZUG1 WHERE 1=1 ";

            if (obj.ZGCONO != null && obj.ZGCONO != String.Empty)
            {
                strSql += "AND ZGCONO = '" + obj.ZGCONO.Trim() + "' ";
            }

            if (obj.ZGBRNO != null && obj.ZGBRNO != String.Empty)
            {
                strSql += "AND ZGBRNO = '" + obj.ZGBRNO.Trim() + "' ";
            }

            if (obj.ZGUGNO != null && obj.ZGUGNO != String.Empty)
            {
                strSql += "AND ZGUGNO = '" + obj.ZGUGNO.Trim() + "' ";
            }

            List<ZUG1Dto> dto = this.ExecutePaging(strSql, "ZGCONO, ZGBRNO, ZGUGNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }
        #endregion

    }
}
