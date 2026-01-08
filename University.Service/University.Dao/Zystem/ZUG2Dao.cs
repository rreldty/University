using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dao.Base;
using University.Dto.Zystem;
using University.Dto.Base;
using System.Linq;
using System.Data;

namespace University.Dao.Zystem
{
    public class ZUG2Dao : BaseDao<ZUG2Dto>
    {
        #region "Constructor"

        public ZUG2Dao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region "Abstract Class Implementation"

        protected override Mapper<ZUG2Dto> GetMapper()
        {
            Mapper<ZUG2Dto> mapDto = new ZUG2MappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(ZUG2Dto obj)
        {
            string[] strField = new string[13];
            strField[0] = "ZHCONO";
            strField[1] = "ZHBRNO";
            strField[2] = "ZHUGNO";
            strField[3] = "ZHUSNO";
            strField[4] = "ZHSYST";
            strField[5] = "ZHSTAT";
            strField[6] = "ZHRCST";
            strField[7] = "ZHCRDT";
            strField[8] = "ZHCRTM";
            strField[9] = "ZHCRUS";
            strField[10] = "ZHCHDT";
            strField[11] = "ZHCHTM";
            strField[12] = "ZHCHUS";

            return this.GenerateStringInsert("ZUG2", strField, obj);
        }

        public string ScriptUpdate(ZUG2Dto obj)
        {
            string[] strField = new string[6];
            strField[0] = "ZHSYST";
            strField[1] = "ZHSTAT";
            strField[2] = "ZHRCST";
            strField[3] = "ZHCHDT";
            strField[4] = "ZHCHTM";
            strField[5] = "ZHCHUS";

            string[] strCondition = new string[4];
            strCondition[0] = "ZHCONO";
            strCondition[1] = "ZHBRNO";
            strCondition[2] = "ZHUGNO";
            strCondition[3] = "ZHUSNO";

            return this.GenerateStringUpdate("ZUG2", strCondition, strField, obj);
        }


        public string ScriptUpdateUserGroup(ZUG2Dto obj)
        {
            string[] strField = new string[7];
            strField[0] = "ZHUGNO";
            strField[1] = "ZHSYST";
            strField[2] = "ZHSTAT";
            strField[3] = "ZHRCST";
            strField[4] = "ZHCHDT";
            strField[5] = "ZHCHTM";
            strField[6] = "ZHCHUS";

            string[] strCondition = new string[4];
            strCondition[0] = "ZHCONO";
            strCondition[1] = "ZHBRNO";
            strCondition[2] = "ZHUSNO";
            strCondition[3] = "ZHRCST";

            return this.GenerateStringUpdate("ZUG2", strCondition, strField, obj);
        }

        public string Save(ZUG2Dto obj)
        {
            obj.ZHSYST = BaseMethod.SystReady;
            obj.ZHSTAT = BaseMethod.StatDraft;
            obj.ZHCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZHCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZHCHDT = obj.ZHCRDT;
            obj.ZHCHTM = obj.ZHCRTM;

            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        public List<string> ScriptSave(ZUG2Dto obj)
        {
            List<string> lstSql = new List<string>();

            obj.ZHSYST = BaseMethod.SystReady;
            obj.ZHSTAT = BaseMethod.StatDraft;
            obj.ZHCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZHCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZHCHDT = obj.ZHCRDT;
            obj.ZHCHTM = obj.ZHCRTM;

            if (!IsExists(obj))
                lstSql.Add(ScriptInsert(obj));
            else
                lstSql.Add(ScriptUpdate(obj));

            return lstSql;
        }

        #endregion

        #region Delete Data

        public string Delete(ZUG2Dto obj)
        {
            string[] strCondition = new string[4];
            strCondition[0] = "ZHCONO";
            strCondition[1] = "ZHBRNO";
            strCondition[2] = "ZHUGNO";
            strCondition[3] = "ZHUSNO";

            string strSql = this.GenerateStringDelete("ZUG2", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        public string DeleteLineUpdateHeader(ZUG1Dto objZUG1, List<ZUG2Dto> lstobj)
        {
            List<string> lstSql = new List<string>();

            // Inisialisasi lstobj sebagai list kosong jika null
            if (lstobj == null)
            {
                lstobj = new List<ZUG2Dto>();
            }

            // Pastikan ada data yang akan dihapus
            if (lstobj.Count > 0)
            {
                foreach (ZUG2Dto obj in lstobj)
                {
                    string[] strCondition = new string[4];
                    strCondition[0] = "ZHCONO";
                    strCondition[1] = "ZHBRNO";
                    strCondition[2] = "ZHUGNO";
                    strCondition[3] = "ZHUSNO";

                    // Hasilkan query DELETE dan tambahkan ke list
                    string strSql = this.GenerateStringDelete("ZUG2", strCondition, obj);

                    // Log query untuk debugging
                    Console.WriteLine("Generated DELETE Query: " + strSql);

                    lstSql.Add(strSql);
                }
            }
            else
            {
                // Log untuk debugging jika lstobj kosong
                Console.WriteLine("No records to delete in lstobj");
            }

            // Query update untuk ZUG1
            lstSql.Add("UPDATE ZUG1 SET"
                        + " ZGSYST = '" + BaseMethod.SystReady + "'"
                        + ", ZGCHDT = " + BaseMethod.DateToNumeric(DateTime.Now)
                        + ", ZGCHTM = " + BaseMethod.TimeToNumeric(DateTime.Now)
                        + ", ZGCHUS = '" + objZUG1.ZGCHUS + "'"
                        + " WHERE ZGCONO = '" + objZUG1.ZGCONO.Trim() + "'"
                        + "     AND ZGBRNO = '" + objZUG1.ZGBRNO.Trim() + "'"
                        + "     AND ZGUGNO = '" + objZUG1.ZGUGNO.Trim() + "'");

            // Log query update untuk debugging
            Console.WriteLine("Generated UPDATE Query: " + lstSql.Last());

            // Eksekusi query menggunakan transaksi
            return ExecuteDbNonQueryTransaction(lstSql);
        }



        #endregion

        #region Select Data

        public bool IsExists(ZUG2Dto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZUG2 "
                            + " WHERE 1=1 "
                            + " AND ZHCONO = '" + obj.ZHCONO.Trim() + "'"
                            + " AND ZHBRNO = '" + obj.ZHBRNO.Trim() + "'"
                            + " AND ZHUGNO = '" + obj.ZHUGNO.Trim() + "'"
                            + " AND ZHUSNO = '" + obj.ZHUSNO.Trim() + "'"
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
        public bool IsExistsUserGroup(ZUG2Dto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZUG2 "
                            + " WHERE 1=1 "
                            + " AND ZHCONO = '" + obj.ZHCONO.Trim() + "'"
                            + " AND ZHBRNO = '" + obj.ZHBRNO.Trim() + "'"
                            + " AND ZHUSNO = '" + obj.ZHUSNO.Trim() + "'"
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


        public ZUG2Dto Get(ZUG2Dto obj)
        {
            string[] strField = new string[13];
            strField[0] = "ZHCONO";
            strField[1] = "ZHBRNO";
            strField[2] = "ZHUGNO";
            strField[3] = "ZHUSNO";
            strField[4] = "ZHSYST";
            strField[5] = "ZHSTAT";
            strField[6] = "ZHRCST";
            strField[7] = "ZHCRDT";
            strField[8] = "ZHCRTM";
            strField[9] = "ZHCRUS";
            strField[10] = "ZHCHDT";
            strField[11] = "ZHCHTM";
            strField[12] = "ZHCHUS";

            string[] strCondition = new string[4];
            strCondition[0] = "ZHCONO";
            strCondition[1] = "ZHBRNO";
            strCondition[2] = "ZHUGNO";
            strCondition[3] = "ZHUSNO";

            string strSql = this.GenerateStringSelect("ZUG2", strCondition, strField, obj);
            ZUG2Dto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<ZUG2Dto> GetList(ZUG2Dto obj)
        {
            string strSql = "SELECT "
                    + " ZHCONO "
                    + ", ZHBRNO "
                    + ", ZHUGNO "
                    + ", ZHUSNO "
                    + ", ZHSYST "
                    + ", ZHSTAT "
                    + ", ZHRCST "
                    + ", ZHCRDT "
                    + ", ZHCRTM "
                    + ", ZHCRUS "
                    + ", ZHCHDT "
                    + ", ZHCHTM "
                    + ", ZHCHUS "
                    + ", ZGUGNA "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZRVANA "
                    + " FROM ZUG2 "
                    + " LEFT JOIN ZUG1 ON 1=1"
                    + "     AND ZGCONO = ZHCONO"
                    + "     AND ZGBRNO = ZHBRNO"
                    + "     AND ZGUGNO = ZHUGNO"
                    + " LEFT JOIN ZUSR ON 1=1"
                    + "     AND ZUUSNO = ZHUSNO "
                    + " LEFT JOIN ZVAR ON 1=1 "
                    + "     AND ZRVATY = 'RCST' "
                    + "     AND ZRVAVL = ZURCST "
                    + " WHERE 1=1 ";

            if (obj.ZHCONO != null && obj.ZHCONO != String.Empty)
            {
                strSql += "AND ZHCONO = '" + obj.ZHCONO.Trim() + "' ";
            }

            if (obj.ZHBRNO != null && obj.ZHBRNO != String.Empty)
            {
                strSql += "AND ZHBRNO = '" + obj.ZHBRNO.Trim() + "' ";
            }

            if (obj.ZHUGNO != null && obj.ZHUGNO != String.Empty)
            {
                strSql += "AND ZHUGNO = '" + obj.ZHUGNO.Trim() + "' ";
            }

            if (obj.ZHUSNO != null && obj.ZHUSNO != String.Empty)
            {
                strSql += "AND ZHUSNO = '" + obj.ZHUSNO.Trim() + "' ";
            }

            List<ZUG2Dto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZUG2Dto> GetListPaging(out int intTotalPage, out int intTotalRecord, ZUG2Dto obj, int intPageNumber, int intPageSize)
        {
            string strSql = "SELECT "
                    + " ZHCONO "
                    + ", ZHBRNO "
                    + ", ZHUGNO "
                    + ", ZHUSNO "
                    + ", ZHSYST "
                    + ", ZHSTAT "
                    + ", ZHRCST "
                    + ", ZHCRDT "
                    + ", ZHCRTM "
                    + ", ZHCRUS "
                    + ", ZHCHDT "
                    + ", ZHCHTM "
                    + ", ZHCHUS "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZRVANA "
                    + " FROM ZUG2 "
                    + " LEFT JOIN ZUSR ON 1=1"
                    + "     AND ZUUSNO = ZHUSNO "
                    + " LEFT JOIN ZVAR ON 1=1 "
                    + "     AND ZRVATY = 'RCST' "
                    + "     AND ZRVAVL = ZURCST "
                    + " WHERE 1=1 ";

            if (obj.ZHCONO != null && obj.ZHCONO != String.Empty)
            {
                strSql += "AND ZHCONO = '" + obj.ZHCONO.Trim() + "' ";
            }

            if (obj.ZHBRNO != null && obj.ZHBRNO != String.Empty)
            {
                strSql += "AND ZHBRNO = '" + obj.ZHBRNO.Trim() + "' ";
            }

            if (obj.ZHUGNO != null && obj.ZHUGNO != String.Empty)
            {
                strSql += "AND ZHUGNO = '" + obj.ZHUGNO.Trim() + "' ";
            }

            if (obj.ZHUSNO != null && obj.ZHUSNO != String.Empty)
            {
                strSql += "AND ZHUSNO = '" + obj.ZHUSNO.Trim() + "' ";
            }

            List<ZUG2Dto> dto = this.ExecutePaging(strSql, "ZHCONO, ZHBRNO, ZHUGNO, ZHUSNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public DataTable GetTablePaging(out int intTotalPage, out int intTotalRecord, ZUG2Dto obj, int intPageNumber, int intPageSize)
        {
            string strSql = "SELECT "
                    + " ZHCONO "
                    + ", ZHBRNO "
                    + ", ZHUGNO "
                    + ", ZHUSNO "
                    + ", ZHSYST "
                    + ", ZHSTAT "
                    + ", ZHRCST "
                    + ", ZHCRDT "
                    + ", ZHCRTM "
                    + ", ZHCRUS "
                    + ", ZHCHDT "
                    + ", ZHCHTM "
                    + ", ZHCHUS "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZRVANA "
                    + ", 1 as [2024] "
                    + " FROM ZUG2 "
                    + " LEFT JOIN ZUSR ON 1=1"
                    + "     AND ZUUSNO = ZHUSNO "
                    + " LEFT JOIN ZVAR ON 1=1 "
                    + "     AND ZRVATY = 'RCST' "
                    + "     AND ZRVAVL = ZURCST "
                    + " WHERE 1=1 ";

            if (obj.ZHCONO != null && obj.ZHCONO != String.Empty)
            {
                strSql += "AND ZHCONO = '" + obj.ZHCONO.Trim() + "' ";
            }

            if (obj.ZHBRNO != null && obj.ZHBRNO != String.Empty)
            {
                strSql += "AND ZHBRNO = '" + obj.ZHBRNO.Trim() + "' ";
            }

            if (obj.ZHUGNO != null && obj.ZHUGNO != String.Empty)
            {
                strSql += "AND ZHUGNO = '" + obj.ZHUGNO.Trim() + "' ";
            }

            if (obj.ZHUSNO != null && obj.ZHUSNO != String.Empty)
            {
                strSql += "AND ZHUSNO = '" + obj.ZHUSNO.Trim() + "' ";
            }

            DataTable dto = this.ExecuteDataTablePaging(strSql, "ZHCONO, ZHBRNO, ZHUGNO, ZHUSNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }
        #endregion

    }
}
