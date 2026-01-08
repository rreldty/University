using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dao.Base;
using University.Dto.Zystem;
using University.Dto.Base;
using System.Linq;

namespace University.Dao.Zystem
{
    public class ZMNUDao : BaseDao<ZMNUDto>
    {
        #region "Constructor"

        public ZMNUDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region "Abstract Class Implementation"

        protected override Mapper<ZMNUDto> GetMapper()
        {
            Mapper<ZMNUDto> mapDto = new ZMNUMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(ZMNUDto obj)
        {
            string[] strField = new string[21];
            strField[0] = "ZMCONO";
            strField[1] = "ZMBRNO";
            strField[2] = "ZMAPNO";
            strField[3] = "ZMMENO";
            strField[4] = "ZMMENA";
            strField[5] = "ZMMETY";
            strField[6] = "ZMMEPA";
            strField[7] = "ZMMESQ";
            strField[8] = "ZMPGNO";
            strField[9] = "ZMPARM";
            strField[10] = "ZMIURL";
            strField[11] = "ZMREMA";
            strField[12] = "ZMSYST";
            strField[13] = "ZMSTAT";
            strField[14] = "ZMRCST";
            strField[15] = "ZMCRDT";
            strField[16] = "ZMCRTM";
            strField[17] = "ZMCRUS";
            strField[18] = "ZMCHDT";
            strField[19] = "ZMCHTM";
            strField[20] = "ZMCHUS";

            return this.GenerateStringInsert("ZMNU", strField, obj);
        }

        public string ScriptUpdate(ZMNUDto obj)
        {
            string[] strField = new string[14];
            strField[0] = "ZMMENA";
            strField[1] = "ZMMETY";
            strField[2] = "ZMMEPA";
            strField[3] = "ZMMESQ";
            strField[4] = "ZMPGNO";
            strField[5] = "ZMPARM";
            strField[6] = "ZMIURL";
            strField[7] = "ZMREMA";
            strField[8] = "ZMSYST";
            strField[9] = "ZMSTAT";
            strField[10] = "ZMRCST";
            strField[11] = "ZMCHDT";
            strField[12] = "ZMCHTM";
            strField[13] = "ZMCHUS";

            string[] strCondition = new string[4];
            strCondition[0] = "ZMCONO";
            strCondition[1] = "ZMBRNO";
            strCondition[2] = "ZMAPNO";
            strCondition[3] = "ZMMENO";

            return this.GenerateStringUpdate("ZMNU", strCondition, strField, obj);
        }

        public string Save(ZMNUDto obj)
        {
            obj.ZMSYST = BaseMethod.SystReady;
            obj.ZMSTAT = BaseMethod.StatDraft;
            obj.ZMCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZMCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZMCHDT = obj.ZMCRDT;
            obj.ZMCHTM = obj.ZMCRTM;

            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data

        public string Delete(ZMNUDto obj)
        {
            string[] strCondition = new string[4];
            strCondition[0] = "ZMCONO";
            strCondition[1] = "ZMBRNO";
            strCondition[2] = "ZMAPNO";
            strCondition[3] = "ZMMENO";

            string strSql = this.GenerateStringDelete("ZMNU", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

		public string DeleteLineUpdateHeader(ZAUTDto objZAUT, List<ZMNUDto> lstobj)
		{
			List<string> lstSql = new List<string>();

			// Inisialisasi lstobj sebagai list kosong jika null
			if (lstobj == null)
			{
				lstobj = new List<ZMNUDto>();
			}

			// Pastikan ada data yang akan dihapus
			if (lstobj.Count > 0)
			{
				foreach (ZMNUDto obj in lstobj)
				{
					string[] strCondition = new string[4];
					strCondition[0] = "ZMCONO";
					strCondition[1] = "ZMBRNO";
					strCondition[2] = "ZMAPNO";
					strCondition[3] = "ZMMENO";

					// Hasilkan query DELETE dan tambahkan ke list
					string strSql = this.GenerateStringDelete("ZMNU", strCondition, obj);

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

			// Query update untuk ZAUT
			lstSql.Add("UPDATE ZAUT SET"
						+ " ZTSYST = '" + BaseMethod.SystReady + "'"
						+ ", ZTCHDT = " + BaseMethod.DateToNumeric(DateTime.Now)
						+ ", ZTCHTM = " + BaseMethod.TimeToNumeric(DateTime.Now)
						+ ", ZTCHUS = '" + objZAUT.ZTCHUS + "'"
						+ " WHERE ZTCONO = '" + objZAUT.ZTCONO.Trim() + "'"
						+ "     AND ZTBRNO = '" + objZAUT.ZTBRNO.Trim() + "'"
						+ "     AND ZTUGNO = '" + objZAUT.ZTUGNO.Trim() + "'");

			// Log query update untuk debugging
			Console.WriteLine("Generated UPDATE Query: " + lstSql.Last());

			// Eksekusi query menggunakan transaksi
			return ExecuteDbNonQueryTransaction(lstSql);
		}

		#endregion

		#region Select Data

		public bool IsExists(ZMNUDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZMNU "
                            + " WHERE 1=1 "
                            + " AND ZMCONO = '" + obj.ZMCONO.Trim() + "'"
                            + " AND ZMBRNO = '" + obj.ZMBRNO.Trim() + "'"
                            + " AND ZMAPNO = '" + obj.ZMAPNO.Trim() + "'"
                            + " AND ZMMENO = '" + obj.ZMMENO.Trim() + "'"
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

        public ZMNUDto Get(ZMNUDto obj)
        {
            string[] strField = new string[21];
            strField[0] = "ZMCONO";
            strField[1] = "ZMBRNO";
            strField[2] = "ZMAPNO";
            strField[3] = "ZMMENO";
            strField[4] = "ZMMENA";
            strField[5] = "ZMMETY";
            strField[6] = "ZMMEPA";
            strField[7] = "ZMMESQ";
            strField[8] = "ZMPGNO";
            strField[9] = "ZMPARM";
            strField[10] = "ZMIURL";
            strField[11] = "ZMREMA";
            strField[12] = "ZMSYST";
            strField[13] = "ZMSTAT";
            strField[14] = "ZMRCST";
            strField[15] = "ZMCRDT";
            strField[16] = "ZMCRTM";
            strField[17] = "ZMCRUS";
            strField[18] = "ZMCHDT";
            strField[19] = "ZMCHTM";
            strField[20] = "ZMCHUS";

            string[] strCondition = new string[4];
            strCondition[0] = "ZMCONO";
            strCondition[1] = "ZMBRNO";
            strCondition[2] = "ZMAPNO";
            strCondition[3] = "ZMMENO";

            string strSql = this.GenerateStringSelect("ZMNU", strCondition, strField, obj);
            ZMNUDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZMNUDto GetFirst(ZMNUDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + " ZMCONO "
                    + ", ZMBRNO "
                    + ", ZMAPNO "
                    + ", ZMMENO "
                    + ", ZMMENA "
                    + ", ZMMETY "
                    + ", ZMMEPA "
                    + ", ZMMESQ "
                    + ", ZMPGNO "
                    + ", ZMPARM "
                    + ", ZMIURL "
                    + ", ZMREMA "
                    + ", ZMSYST "
                    + ", ZMSTAT "
                    + ", ZMRCST "
                    + ", ZMCRDT "
                    + ", ZMCRTM "
                    + ", ZMCRUS "
                    + ", ZMCHDT "
                    + ", ZMCHTM "
                    + ", ZMCHUS "
                    + "FROM ZMNU "
                    + "WHERE 1=1 "
                    + "AND ZMCONO = '" + obj.ZMCONO.Trim() + "' "
                    + "AND ZMBRNO = '" + obj.ZMBRNO.Trim() + "' "
                    + "ORDER BY ZMCONO, ZMBRNO, ZMAPNO + ZMMENO"
                    + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZMNUDto GetPrevious(ZMNUDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + " ZMCONO "
                    + ", ZMBRNO "
                    + ", ZMAPNO "
                    + ", ZMMENO "
                    + ", ZMMENA "
                    + ", ZMMETY "
                    + ", ZMMEPA "
                    + ", ZMMESQ "
                    + ", ZMPGNO "
                    + ", ZMPARM "
                    + ", ZMIURL "
                    + ", ZMREMA "
                    + ", ZMSYST "
                    + ", ZMSTAT "
                    + ", ZMRCST "
                    + ", ZMCRDT "
                    + ", ZMCRTM "
                    + ", ZMCRUS "
                    + ", ZMCHDT "
                    + ", ZMCHTM "
                    + ", ZMCHUS "
                    + "FROM ZMNU "
                    + "WHERE 1=1 "
                    + "AND ZMCONO = '" + obj.ZMCONO.Trim() + "' "
                    + "AND ZMBRNO = '" + obj.ZMBRNO.Trim() + "' "
                    + "AND ZMAPNO + ZMMENO < '" + obj.ZMAPNO.Trim() + obj.ZMMENO.Trim() + "' "
                    + "ORDER BY ZMCONO, ZMBRNO, ZMAPNO + ZMMENO DESC"
                    + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZMNUDto GetNext(ZMNUDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + " ZMCONO "
                    + ", ZMBRNO "
                    + ", ZMAPNO "
                    + ", ZMMENO "
                    + ", ZMMENA "
                    + ", ZMMETY "
                    + ", ZMMEPA "
                    + ", ZMMESQ "
                    + ", ZMPGNO "
                    + ", ZMPARM "
                    + ", ZMIURL "
                    + ", ZMREMA "
                    + ", ZMSYST "
                    + ", ZMSTAT "
                    + ", ZMRCST "
                    + ", ZMCRDT "
                    + ", ZMCRTM "
                    + ", ZMCRUS "
                    + ", ZMCHDT "
                    + ", ZMCHTM "
                    + ", ZMCHUS "
                    + "FROM ZMNU "
                    + "WHERE 1=1 "
                    + "AND ZMCONO = '" + obj.ZMCONO.Trim() + "' "
                    + "AND ZMBRNO = '" + obj.ZMBRNO.Trim() + "' "
                    + "AND ZMAPNO + ZMMENO > '" + obj.ZMAPNO.Trim() + obj.ZMMENO.Trim() + "' "
                    + "ORDER BY ZMCONO, ZMBRNO, ZMAPNO, ZMMENO"
                    + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZMNUDto GetLast(ZMNUDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + " ZMCONO "
                    + ", ZMBRNO "
                    + ", ZMAPNO "
                    + ", ZMMENO "
                    + ", ZMMENA "
                    + ", ZMMETY "
                    + ", ZMMEPA "
                    + ", ZMMESQ "
                    + ", ZMPGNO "
                    + ", ZMPARM "
                    + ", ZMIURL "
                    + ", ZMREMA "
                    + ", ZMSYST "
                    + ", ZMSTAT "
                    + ", ZMRCST "
                    + ", ZMCRDT "
                    + ", ZMCRTM "
                    + ", ZMCRUS "
                    + ", ZMCHDT "
                    + ", ZMCHTM "
                    + ", ZMCHUS "
                    + "FROM ZMNU "
                    + "WHERE 1=1 "
                    + "AND ZMCONO = '" + obj.ZMCONO.Trim() + "' "
                    + "AND ZMBRNO = '" + obj.ZMBRNO.Trim() + "' "
                    + "ORDER BY ZMCONO, ZMBRNO, ZMAPNO + ZMMENO DESC"
                    + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZMNUDto GetProgram(ZMNUDto obj)
        {
            string strSql = "SELECT "
                    + " ZMCONO "
                    + ", ZMBRNO "
                    + ", ZMAPNO "
                    + ", ZMMENO "
                    + ", ZMMENA "
                    + ", ZMMETY "
                    + ", ZMMEPA "
                    + ", ZMMESQ "
                    + ", ZMPGNO "
                    + ", ZMPARM "
                    + ", ZMIURL "
                    + ", ZMREMA "
                    + ", ZMSYST "
                    + ", ZMSTAT "
                    + ", ZMRCST "
                    + ", ZMCRDT "
                    + ", ZMCRTM "
                    + ", ZMCRUS "
                    + ", ZMCHDT "
                    + ", ZMCHTM "
                    + ", ZMCHUS "
                    + ", ZPPURL "
                    + "FROM ZMNU "
                    + "LEFT JOIN ZPGM ON 1=1 "
                    + "AND ZPCONO=ZMCONO "
                    + "AND ZPBRNO=ZMBRNO "
                    + "AND ZPAPNO=ZMAPNO "
                    + "AND ZPPGNO=ZMPGNO "
                    + "WHERE 1=1 ";

            if (obj.ZMCONO != null && obj.ZMCONO != String.Empty)
            {
                strSql += " AND ZMCONO = '" + obj.ZMCONO.Trim() + "' ";
            }

            if (obj.ZMBRNO != null && obj.ZMBRNO != String.Empty)
            {
                strSql += " AND ZMBRNO = '" + obj.ZMBRNO.Trim() + "' ";
            }

            if (obj.ZMAPNO != null && obj.ZMAPNO != String.Empty)
            {
                strSql += "AND ZMAPNO = '" + obj.ZMAPNO.Trim() + "' ";
            }

            if (obj.ZMMENO != null && obj.ZMMENO != String.Empty)
            {
                strSql += "AND ZMMENO = '" + obj.ZMMENO.Trim() + "' ";
            }

            ZMNUDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<ZMNUDto> GetList(ZMNUDto obj)
        {
            string strSql = "SELECT "
                    + " ZMCONO "
                    + ", ZMBRNO "
                    + ", ZMAPNO "
                    + ", ZMMENO "
                    + ", ZMMENA "
                    + ", ZMMETY "
                    + ", ZMMEPA "
                    + ", ZMMESQ "
                    + ", ZMPGNO "
                    + ", ZMPARM "
                    + ", ZMIURL "
                    + ", ZMREMA "
                    + ", ZMSYST "
                    + ", ZMSTAT "
                    + ", ZMRCST "
                    + ", ZMCRDT "
                    + ", ZMCRTM "
                    + ", ZMCRUS "
                    + ", ZMCHDT "
                    + ", ZMCHTM "
                    + ", ZMCHUS "
                    + ", ZRVANA "
                    + "FROM ZMNU "
                    + "LEFT JOIN ZVAR ON ZRVATY='METY'AND ZRVAVL=ZMMETY "
                    + "WHERE 1=1 ";

            if (obj.ZMCONO != null && obj.ZMCONO != String.Empty)
            {
                strSql += " AND ZMCONO = '" + obj.ZMCONO.Trim() + "' ";
            }

            if (obj.ZMBRNO != null && obj.ZMBRNO != String.Empty)
            {
                strSql += " AND ZMBRNO = '" + obj.ZMBRNO.Trim() + "' ";
            }

            if (obj.ZMAPNO != null && obj.ZMAPNO != String.Empty)
            {
                strSql += "AND ZMAPNO = '" + obj.ZMAPNO.Trim() + "' ";
            }

            if (obj.ZMMENO != null && obj.ZMMENO != String.Empty)
            {
                strSql += "AND ZMMENO LIKE '%" + obj.ZMMENO.Trim() + "%' ";
            }

            if (obj.ZMMENA != null && obj.ZMMENA != String.Empty)
            {
                strSql += "AND ZMMENA LIKE '%" + obj.ZMMENA.Trim() + "%' ";
            }

            List<ZMNUDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZMNUDto> GetListPaging(ZMNUDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                    + " ZMCONO "
                    + ", ZMBRNO "
                    + ", ZMAPNO "
                    + ", ZMMENO "
                    + ", ZMMENA "
                    + ", ZMMETY "
                    + ", ZMMEPA "
                    + ", ZMMESQ "
                    + ", ZMPGNO "
                    + ", ZMPARM "
                    + ", ZMIURL "
                    + ", ZMREMA "
                    + ", ZMSYST "
                    + ", ZMSTAT "
                    + ", ZMRCST "
                    + ", ZMCRDT "
                    + ", ZMCRTM "
                    + ", ZMCRUS "
                    + ", ZMCHDT "
                    + ", ZMCHTM "
                    + ", ZMCHUS "
                    + ", ZRVANA "
                    + "FROM ZMNU "
                    + "LEFT JOIN ZVAR ON ZRVATY='METY'AND ZRVAVL=ZMMETY "
                    + "WHERE 1=1 ";

            if (obj.ZMCONO != null && obj.ZMCONO != String.Empty)
            {
                strSql += " AND ZMCONO = '" + obj.ZMCONO.Trim() + "' ";
            }

            if (obj.ZMBRNO != null && obj.ZMBRNO != String.Empty)
            {
                strSql += " AND ZMBRNO = '" + obj.ZMBRNO.Trim() + "' ";
            }

            if (obj.ZMAPNO != null && obj.ZMAPNO != String.Empty)
            {
                strSql += "AND ZMAPNO = '" + obj.ZMAPNO.Trim() + "' ";
            }

            if (obj.ZMMENO != null && obj.ZMMENO != String.Empty)
            {
                strSql += "AND ZMMENO LIKE '%" + obj.ZMMENO.Trim() + "%' ";
            }

            if (obj.ZMMENA != null && obj.ZMMENA != String.Empty)
            {
                strSql += "AND ZMMENA LIKE '%" + obj.ZMMENA.Trim() + "%' ";
            }

            List<ZMNUDto> dto = this.ExecutePaging(strSql, "ZMCONO, ZMBRNO, ZMAPNO, ZMMENO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public List<ZMNUDto> GetlistPagingNotInUserAuthority(ZMNUDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                    + " ZMCONO "
                    + ", ZMBRNO "
                    + ", ZMAPNO "
                    + ", ZMMENO "
                    + ", ZMMENA "
                    + ", A.ZRVANA AS ZMMETY "
                    + ", ZMMEPA "
                    + ", ZMMESQ "
                    + ", ZMPGNO "
                    + ", ZMIURL "
                    + ", ZMREMA "
                    + ", B.ZRVANA AS RCST "
                    + ", ZMCRDT "
                    + ", ZMCRTM "
                    + ", ZMCRUS "
                    + ", ZMCHDT "
                    + ", ZMCHTM "
                    + ", ZMCHUS "
                    + " FROM ZMNU "
                    + " LEFT JOIN ZAUT on 1=1"
                    + "     AND ZTCONO = ZMCONO"
                    + "     AND ZTBRNO = ZMBRNO"
                    + "     AND ZTAPNO = ZMAPNO"
                    + "     AND ZTMENO = ZMMENO"
                    + "     AND ZTUGNO = '" + obj.ZTUGNO.Trim() + "'"
                    + " LEFT JOIN ZVAR A ON 1=1"
                    + "     AND A.ZRCONO = ZMCONO"
                    + "     AND A.ZRBRNO = ZMBRNO"
                    + "     AND A.ZRVATY = 'METY'"
                    + "     AND A.ZRVAVL = ZMMETY"
                    + " LEFT JOIN ZVAR B ON 1=1"
                    + "     AND B.ZRCONO = ZMCONO"
                    + "     AND B.ZRBRNO = ZMBRNO"
                    + "     AND B.ZRVATY = 'RCST'"
                    + "     AND B.ZRVAVL = ZMRCST"
                    + " WHERE 1=1 "
                    + " AND ZMRCST = 1"
                    + " AND ISNULL(ZTMENO, '') = ''"
                    + "";

            if (obj.ZMCONO != null && obj.ZMCONO != String.Empty)
            {
                strSql += "AND ZMCONO = '" + obj.ZMCONO.Trim() + "' ";
            }

            if (obj.ZMBRNO != null && obj.ZMBRNO != String.Empty)
            {
                strSql += "AND ZMBRNO = '" + obj.ZMBRNO.Trim() + "' ";
            }

            if (obj.ZMAPNO != null && obj.ZMAPNO != String.Empty)
            {
                strSql += "AND ZMAPNO = '" + obj.ZMAPNO.Trim() + "' ";
            }

            if (obj.ZMMENO != null && obj.ZMMENO != String.Empty)
            {
                strSql += "AND ZMMENO = '" + obj.ZMMENO.Trim() + "' ";
            }

            if (obj.ZMMENA != null && obj.ZMMENA != String.Empty)
            {
                strSql += "AND ZMMENA = '" + obj.ZMMENA.Trim() + "' ";
            }

            List<ZMNUDto> dto = this.ExecutePaging(strSql, "ZMCONO, ZMBRNO, ZMAPNO, ZMMENO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        #endregion

    }
}
