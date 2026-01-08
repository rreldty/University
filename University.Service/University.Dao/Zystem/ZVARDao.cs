using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dto.Base;
using University.Dao.Base;
using University.Dto.Zystem;

namespace University.Dao.Zystem
{
    public class ZVARDao : BaseDao<ZVARDto>
    {
        #region Constructor
        public ZVARDao()
        {
            this.MainDataSource = DataSource.University;
        }
        #endregion

        #region Abstract Class Implementation
        protected override Mapper<ZVARDto> GetMapper()
        {
            Mapper<ZVARDto> mapDto = new ZVARDtoMap();
            return mapDto;
        }
        #endregion

        #region Save Data

        public string ScriptInsert(ZVARDto obj)
        {
            string[] strField = new string[17];
            strField[0] = "ZRCONO";
            strField[1] = "ZRBRNO";
            strField[2] = "ZRVANO";
            strField[3] = "ZRVANA";
            strField[4] = "ZRVATY";
            strField[5] = "ZRVAVL";
            strField[6] = "ZRVASQ";
            strField[7] = "ZRREMA";
            strField[8] = "ZRSYST";
            strField[9] = "ZRSTAT";
            strField[10] = "ZRRCST";
            strField[11] = "ZRCRDT";
            strField[12] = "ZRCRTM";
            strField[13] = "ZRCRUS";
            strField[14] = "ZRCHDT";
            strField[15] = "ZRCHTM";
            strField[16] = "ZRCHUS";

            return this.GenerateStringInsert("ZVAR", strField, obj);
        }

        public string ScriptUpdate(ZVARDto obj)
        {
            string[] strField = new string[11];
            strField[0] = "ZRVANA";
            strField[1] = "ZRVATY";
            strField[2] = "ZRVAVL";
            strField[3] = "ZRVASQ";
            strField[4] = "ZRREMA";
            strField[5] = "ZRSYST";
            strField[6] = "ZRSTAT";
            strField[7] = "ZRRCST";
            strField[8] = "ZRCHDT";
            strField[9] = "ZRCHTM";
            strField[10] = "ZRCHUS";

            string[] strCondition = new string[3];
            strCondition[0] = "ZRCONO";
            strCondition[1] = "ZRBRNO";
            strCondition[2] = "ZRVANO";

            return this.GenerateStringUpdate("ZVAR", strCondition, strField, obj);
        }

        public string Save(ZVARDto obj)
        {
            obj.ZRSYST = BaseMethod.SystReady;
            obj.ZRSTAT = BaseMethod.StatDraft;
            obj.ZRCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZRCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZRCHDT = obj.ZRCRDT;
            obj.ZRCHTM = obj.ZRCRTM;

            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data
        public string Delete(ZVARDto obj)
        {
            string[] strCondition = new string[3];
            strCondition[0] = "ZRCONO";
            strCondition[1] = "ZRBRNO";
            strCondition[2] = "ZRVANO";

            string strSql = this.GenerateStringDelete("ZVAR", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }
        #endregion

        #region Select Data

        public bool IsExists(ZVARDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZVAR "
                            + " WHERE 1=1 "
                            + " AND ZRCONO = '" + obj.ZRCONO.Trim() + "'"
                            + " AND ZRBRNO = '" + obj.ZRBRNO.Trim() + "'"
                            + " AND ZRVANO = '" + obj.ZRVANO.Trim() + "'"
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

        public bool IsExistsByTypeValue(ZVARDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZVAR "
                            + " WHERE 1=1 "
                            + " AND ZRCONO = '" + obj.ZRCONO.Trim() + "'"
                            + " AND ZRBRNO = '" + obj.ZRBRNO.Trim() + "'"
                            + " AND ZRVATY = '" + obj.ZRVATY.Trim() + "'"
                            + " AND ZRVAVL = '" + obj.ZRVAVL.Trim() + "'"
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

        public ZVARDto Get(ZVARDto obj)
        {
            string[] strField = new string[17];
            strField[0] = "ZRCONO";
            strField[1] = "ZRBRNO";
            strField[2] = "ZRVANO";
            strField[3] = "ZRVANA";
            strField[4] = "ZRVATY";
            strField[5] = "ZRVAVL";
            strField[6] = "ZRVASQ";
            strField[7] = "ZRREMA";
            strField[8] = "ZRSYST";
            strField[9] = "ZRSTAT";
            strField[10] = "ZRRCST";
            strField[11] = "ZRCRDT";
            strField[12] = "ZRCRTM";
            strField[13] = "ZRCRUS";
            strField[14] = "ZRCHDT";
            strField[15] = "ZRCHTM";
            strField[16] = "ZRCHUS";

            string[] strCondition = new string[3];
            strCondition[0] = "ZRCONO";
            strCondition[1] = "ZRBRNO";
            strCondition[2] = "ZRVANO";

            string strSql = this.GenerateStringSelect("ZVAR", strCondition, strField, obj);
            ZVARDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZVARDto GetByTypeValue(ZVARDto obj)
        {
            string[] strField = new string[17];
            strField[0] = "ZRCONO";
            strField[1] = "ZRBRNO";
            strField[2] = "ZRVANO";
            strField[3] = "ZRVANA";
            strField[4] = "ZRVATY";
            strField[5] = "ZRVAVL";
            strField[6] = "ZRVASQ";
            strField[7] = "ZRREMA";
            strField[8] = "ZRSYST";
            strField[9] = "ZRSTAT";
            strField[10] = "ZRRCST";
            strField[11] = "ZRCRDT";
            strField[12] = "ZRCRTM";
            strField[13] = "ZRCRUS";
            strField[14] = "ZRCHDT";
            strField[15] = "ZRCHTM";
            strField[16] = "ZRCHUS";

            string[] strCondition = new string[4];
            strCondition[0] = "ZRCONO";
            strCondition[1] = "ZRBRNO";
            strCondition[2] = "ZRVATY";
            strCondition[3] = "ZRVAVL";

            string strSql = this.GenerateStringSelect("ZVAR", strCondition, strField, obj);
            ZVARDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZVARDto GetFirst(ZVARDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZRCONO "
                    + ", ZRBRNO "
                    + ", ZRVANO "
                    + ", ZRVANA "
                    + ", ZRVATY "
                    + ", ZRVAVL "
                    + ", ZRVASQ "
                    + ", ZRREMA "
                    + ", ZRSYST "
                    + ", ZRSTAT "
                    + ", ZRRCST "
                    + ", ZRCRDT "
                    + ", ZRCRTM "
                    + ", ZRCRUS "
                    + ", ZRCHDT "
                    + ", ZRCHTM "
                    + ", ZRCHUS "
                    + " FROM ZVAR "
                    + " WHERE 1=1 "
                    + " AND ZRCONO = '" + obj.ZRCONO.Trim() + "' "
                    + " AND ZRBRNO = '" + obj.ZRBRNO.Trim() + "' "
                    + " ORDER BY ZRVANO ASC"
                    + "";

            ZVARDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZVARDto GetPrevious(ZVARDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZRCONO "
                    + ", ZRBRNO "
                    + ", ZRVANO "
                    + ", ZRVANA "
                    + ", ZRVATY "
                    + ", ZRVAVL "
                    + ", ZRVASQ "
                    + ", ZRREMA "
                    + ", ZRSYST "
                    + ", ZRSTAT "
                    + ", ZRRCST "
                    + ", ZRCRDT "
                    + ", ZRCRTM "
                    + ", ZRCRUS "
                    + ", ZRCHDT "
                    + ", ZRCHTM "
                    + ", ZRCHUS "
                    + " FROM ZVAR "
                    + " WHERE 1=1 "
                    + " AND ZRCONO = '" + obj.ZRCONO.Trim() + "' "
                    + " AND ZRBRNO = '" + obj.ZRBRNO.Trim() + "' "
                    + " AND ZRVANO < '" + obj.ZRVANO.Trim() + "' "
                    + " ORDER BY ZRVANO DESC"
                    + "";

            ZVARDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZVARDto GetNext(ZVARDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZRCONO "
                    + ", ZRBRNO "
                    + ", ZRVANO "
                    + ", ZRVANA "
                    + ", ZRVATY "
                    + ", ZRVAVL "
                    + ", ZRVASQ "
                    + ", ZRREMA "
                    + ", ZRSYST "
                    + ", ZRSTAT "
                    + ", ZRRCST "
                    + ", ZRCRDT "
                    + ", ZRCRTM "
                    + ", ZRCRUS "
                    + ", ZRCHDT "
                    + ", ZRCHTM "
                    + ", ZRCHUS "
                    + " FROM ZVAR "
                    + " WHERE 1=1 "
                    + " AND ZRCONO = '" + obj.ZRCONO.Trim() + "' "
                    + " AND ZRBRNO = '" + obj.ZRBRNO.Trim() + "' "
                    + " AND ZRVANO > '" + obj.ZRVANO.Trim() + "' "
                    + " ORDER BY ZRVANO ASC"
                    + "";

            ZVARDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZVARDto GetLast(ZVARDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZRCONO "
                    + ", ZRBRNO "
                    + ", ZRVANO "
                    + ", ZRVANA "
                    + ", ZRVATY "
                    + ", ZRVAVL "
                    + ", ZRVASQ "
                    + ", ZRREMA "
                    + ", ZRSYST "
                    + ", ZRSTAT "
                    + ", ZRRCST "
                    + ", ZRCRDT "
                    + ", ZRCRTM "
                    + ", ZRCRUS "
                    + ", ZRCHDT "
                    + ", ZRCHTM "
                    + ", ZRCHUS "
                    + " FROM ZVAR "
                    + " WHERE 1=1 "
                    + " AND ZRCONO = '" + obj.ZRCONO.Trim() + "' "
                    + " AND ZRBRNO = '" + obj.ZRBRNO.Trim() + "' "
                    + " ORDER BY ZRVANO DESC"
                    + "";

            ZVARDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<ZVARDto> GetList(ZVARDto obj)
        {
            string strSql = "SELECT "
                    + " ZRCONO "
                    + ", ZRBRNO "
                    + ", ZRVANO "
                    + ", ZRVANA "
                    + ", ZRVATY "
                    + ", ZRVAVL "
                    + ", ZRVASQ "
                    + ", ZRREMA "
                    + ", ZRSYST "
                    + ", ZRSTAT "
                    + ", ZRRCST "
                    + ", ZRCRDT "
                    + ", ZRCRTM "
                    + ", ZRCRUS "
                    + ", ZRCHDT "
                    + ", ZRCHTM "
                    + ", ZRCHUS "
                    + " FROM ZVAR "
                    + " WHERE 1=1 ";

            if (obj.ZRCONO != null && obj.ZRCONO != String.Empty)
            {
                strSql += " AND ZRCONO = '" + obj.ZRCONO.Trim() + "' ";
            }

            if (obj.ZRBRNO != null && obj.ZRBRNO != String.Empty)
            {
                strSql += " AND ZRBRNO = '" + obj.ZRBRNO.Trim() + "' ";
            }

            if (obj.ZRVANO != null && obj.ZRVANO != String.Empty)
            {
                strSql += " AND ZRVANO = '" + obj.ZRVANO.Trim() + "' ";
            }

            if (obj.ZRVATY != null && obj.ZRVATY != String.Empty)
            {
                strSql += " AND ZRVATY = '" + obj.ZRVATY.Trim() + "' ";
            }

            List<ZVARDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public ZVARDto UrlByVANO(ZVARDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " ZRVAVL "
                    + " FROM ZVAR "
                    + " WHERE 1=1 "
                    + " AND ZRCONO = '" + obj.ZRCONO.Trim() + "' "
                    + " AND ZRBRNO = '" + obj.ZRBRNO.Trim() + "' "
                    + " AND ZRVATY = 'SRVURL' ";

            if (obj.ZRVANO != null)
            {
                strSql += " AND ZRVANO = '" + obj.ZRVANO.Trim() + "' ";
            }

            ZVARDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<ZVARDto> GetListByVANO(string strCONO, string strBRNO, List<string> lstVANO)
        {
            string strVANO = string.Empty;

            if (lstVANO != null)
            {
                foreach (string str in lstVANO)
                {
                    if (strVANO.Length != 0)
                        strVANO += ",";

                    strVANO += "'" + str + "'";
                }
            }

            string strSql = "SELECT "
                    + " ZRCONO "
                    + ", ZRBRNO "
                    + ", ZRVANO "
                    + ", ZRVANA "
                    + ", ZRVATY "
                    + ", ZRVAVL "
                    + ", ZRVASQ "
                    + ", ZRREMA "
                    + ", ZRSYST "
                    + ", ZRSTAT "
                    + ", ZRRCST "
                    + ", ZRCRDT "
                    + ", ZRCRTM "
                    + ", ZRCRUS "
                    + ", ZRCHDT "
                    + ", ZRCHTM "
                    + ", ZRCHUS "
                    + " FROM ZVAR "
                    + " WHERE 1=1 ";

            if (strCONO != null && strCONO != String.Empty)
            {
                strSql += " AND (ZRCONO = '" + strCONO.Trim() + "' OR ZRCONO = '') ";
            }

            if (strBRNO != null && strBRNO != String.Empty)
            {
                strSql += " AND (ZRBRNO = '" + strBRNO.Trim() + "' OR ZRBRNO = '') ";
            }

            if (strVANO != null && strVANO != String.Empty)
            {
                strSql += " AND ZRVANO IN (" + strVANO.Trim() + ") ";
            }

            List<ZVARDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZVARDto> GetListPaging(ZVARDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                    + " ZRCONO "
                    + ", ZRBRNO "
                    + ", ZRVANO "
                    + ", ZRVANA "
                    + ", ZRVATY "
                    + ", ZRVAVL "
                    + ", ZRVASQ "
                    + ", ZRREMA "
                    + ", ZRSYST "
                    + ", ZRSTAT "
                    + ", ZRRCST "
                    + ", ZRCRDT "
                    + ", ZRCRTM "
                    + ", ZRCRUS "
                    + ", ZRCHDT "
                    + ", ZRCHTM "
                    + ", ZRCHUS "
                    + " FROM ZVAR "
                    + " WHERE 1=1 ";

            if (obj.ZRCONO != null && obj.ZRCONO != String.Empty)
            {
                strSql += " AND ZRCONO = '" + obj.ZRCONO.Trim() + "' ";
            }

            if (obj.ZRBRNO != null && obj.ZRBRNO != String.Empty)
            {
                strSql += " AND ZRBRNO = '" + obj.ZRBRNO.Trim() + "' ";
            }

            if (obj.ZRVANO != null && obj.ZRVANO != String.Empty)
            {
                strSql += " AND ZRVANO = '" + obj.ZRVANO.Trim() + "' ";
            }

            List<ZVARDto> dto = this.ExecutePaging(strSql, "ZRCONO, ZRBRNO, ZRVANO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }
        
        #endregion
    }
}
