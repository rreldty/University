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
    public class ZAUTDao : BaseDao<ZAUTDto>
    {

        #region Constructor

        public ZAUTDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<ZAUTDto> GetMapper()
        {
            Mapper<ZAUTDto> mapDto = new ZAUTMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(ZAUTDto obj)
        {
            string[] strField = new string[15];
            strField[0] = "ZTCONO";
            strField[1] = "ZTBRNO";
            strField[2] = "ZTUGNO";
            strField[3] = "ZTAPNO";
            strField[4] = "ZTMENO";
            strField[5] = "ZTRIGH";
            strField[6] = "ZTSYST";
            strField[7] = "ZTSTAT";
            strField[8] = "ZTRCST";
            strField[9] = "ZTCRDT";
            strField[10] = "ZTCRTM";
            strField[11] = "ZTCRUS";
            strField[12] = "ZTCHDT";
            strField[13] = "ZTCHTM";
            strField[14] = "ZTCHUS";

            return this.GenerateStringInsert("ZAUT", strField, obj);
        }

        public string ScriptUpdate(ZAUTDto obj)
        {
            string[] strField = new string[7];
            strField[0] = "ZTRIGH";
            strField[1] = "ZTSYST";
            strField[2] = "ZTSTAT";
            strField[3] = "ZTRCST";
            strField[4] = "ZTCHDT";
            strField[5] = "ZTCHTM";
            strField[6] = "ZTCHUS";

            string[] strCondition = new string[5];
            strCondition[0] = "ZTCONO";
            strCondition[1] = "ZTBRNO";
            strCondition[2] = "ZTUGNO";
            strCondition[3] = "ZTAPNO";
            strCondition[4] = "ZTMENO";

            return this.GenerateStringUpdate("ZAUT", strCondition, strField, obj);
        }

        public string Save(ZAUTDto obj)
        {
            obj.ZTSYST = BaseMethod.SystReady;
            obj.ZTSTAT = BaseMethod.StatDraft;
            obj.ZTCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZTCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZTCHDT = obj.ZTCRDT;
            obj.ZTCHTM = obj.ZTCRTM;

            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

		public string SaveWithLine(ZAUTDto objLine)
		{
			List<ZMNUDto> lstZMNU = objLine.lstZMNU;

			string strResult = string.Empty;
			List<string> lstSql = new List<string>();

			if (lstZMNU != null)
			{
				ZMNUDao daoZMNU = new ZMNUDao();

				foreach (ZMNUDto objZMNU in lstZMNU)
				{
					if (objZMNU.IsSelected == true)
					{
						ZAUTDto dto = new ZAUTDto();
						dto.ZTCONO = String.Empty;
						dto.ZTBRNO = String.Empty;
						dto.ZTUGNO = objLine.ZTUGNO;
						dto.ZTAPNO = objLine.ZTAPNO;
						dto.ZTMENO = objZMNU.ZMMENO;
						dto.ZTSYST = BaseMethod.SystReady;
						dto.ZTSTAT = BaseMethod.StatDraft;
                        dto.ZTRCST = BaseMethod.RecordStatusActive;
						dto.ZTCRDT = BaseMethod.DateToNumeric(DateTime.Now);
						dto.ZTCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
						dto.ZTCHUS = objLine.ZTCHUS;
						dto.ZTCHDT = objLine.ZTCRDT;
						dto.ZTCHTM = objLine.ZTCRTM;
						dto.ZTCHUS = objLine.ZTCHUS;

						if (!IsExists(dto))
							lstSql.Add(ScriptInsert(dto));
						else
							lstSql.Add(ScriptUpdate(dto));
					}
				}
			}


			return ExecuteDbNonQueryTransaction(lstSql);
		}

		#endregion

		#region Delete Data

		public string Delete(ZAUTDto obj)
        {
            string[] strCondition = new string[5];
            strCondition[0] = "ZTCONO";
            strCondition[1] = "ZTBRNO";
            strCondition[2] = "ZTUGNO";
            strCondition[3] = "ZTAPNO";
            strCondition[4] = "ZTMENO";

            string strSql = this.GenerateStringDelete("ZAUT", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

		public string DeleteLineUpdateHeader(ZAUTDto objZAUT, List<ZAUTDto> lstobj)
		{
			List<string> lstSql = new List<string>();

			// Pastikan ada data yang akan dihapus
			if (lstobj.Count > 0)
			{
				foreach (ZAUTDto obj in lstobj)
				{
					string[] strCondition = new string[4];
					strCondition[0] = "ZTCONO";
					strCondition[1] = "ZTBRNO";
					strCondition[2] = "ZTUGNO";
					strCondition[3] = "ZTMENO";

					// Hasilkan query DELETE dan tambahkan ke list
					string strSql = this.GenerateStringDelete("ZAUT", strCondition, obj);

					lstSql.Add(strSql);
				}
			}

			// Eksekusi query menggunakan transaksi
			return ExecuteDbNonQueryTransaction(lstSql);
		}
		#endregion

		#region Get Data

		public bool IsExists(ZAUTDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZAUT "
                            + " WHERE 1=1 "
                            + " AND ZTCONO = '" + obj.ZTCONO.Trim() + "'"
                            + " AND ZTBRNO = '" + obj.ZTBRNO.Trim() + "'"
                            + " AND ZTUGNO = '" + obj.ZTUGNO.Trim() + "'"
                            + " AND ZTAPNO = '" + obj.ZTAPNO.Trim() + "'"
                            + " AND ZTMENO = '" + obj.ZTMENO.Trim() + "'"
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

        public ZAUTDto Get(ZAUTDto obj)
        {
            string strSql = "SELECT "
                    + " ZTCONO "
                    + ", ZTBRNO "
                    + ", ZTUGNO "
                    + ", ZTAPNO "
                    + ", ZTMENO "
                    + ", ZTRIGH "
                    + ", ZTSYST "
                    + ", ZTSTAT "
                    + ", ZTCRDT "
                    + ", ZTCRTM "
                    + ", ZTCRUS "
                    + ", ZTCHDT "
                    + ", ZTCHTM "
                    + ", ZTCHUS "
                    + " from ZAUT "
                    + " where 1=1 ";

            if (obj.ZTCONO != null && obj.ZTCONO != String.Empty)
            {
                strSql += " AND ZTCONO = '" + obj.ZTCONO.Trim() + "' ";
            }

            if (obj.ZTBRNO != null && obj.ZTBRNO != String.Empty)
            {
                strSql += " AND ZTBRNO = '" + obj.ZTBRNO.Trim() + "' ";
            }

            if (obj.ZTUGNO != null && obj.ZTUGNO != String.Empty)
            {
                strSql += " AND ZTUGNO = '" + obj.ZTUGNO.Trim() + "' ";
            }

            if (obj.ZTAPNO != null && obj.ZTAPNO != String.Empty)
            {
                strSql += " AND ZTAPNO = '" + obj.ZTAPNO.Trim() + "' ";
            }

            if (obj.ZTMENO != null && obj.ZTMENO != String.Empty)
            {
                strSql += " AND ZTMENO = '" + obj.ZTMENO.Trim() + "' ";
            }

            ZAUTDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZAUTDto GetFirst(ZAUTDto obj)
        {
            string strSql = "SELECT TOP 1 DISTINCT"
                    + " ZTCONO "
                    + ", ZTBRNO "
                    + ", ZTUGNO "
                    + ", ZTAPNO "
                    + ", ZTUGNO+ZTAPNO "
                    + " from ZAUT "
                    + " where 1=1 "
                    + " and ZTCONO = '" + obj.ZTCONO.Trim() + "'"
                    + " and ZTBRNO = '" + obj.ZTBRNO.Trim() + "'"
                    + " order by ZTCONO, ZTBRNO, ZTUGNO + ZTAPNO ASC"
                    + "";

            ZAUTDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZAUTDto GetPrevious(ZAUTDto obj)
        {
            string strSql = "SELECT TOP 1 DISTINCT"
                    + " ZTCONO "
                    + ", ZTBRNO "
                    + ", ZTUGNO "
                    + ", ZTAPNO "
                    + ", ZTUGNO+ZTAPNO "
                    + " from ZAUT "
                    + " where 1=1 "
                    + " and ZTCONO = '" + obj.ZTCONO.Trim() + "'"
                    + " and ZTBRNO = '" + obj.ZTBRNO.Trim() + "'"
                    + " and ZTUGNO + ZTAPNO < '" + obj.ZTUGNO.Trim() + obj.ZTAPNO.Trim() + "'"
                    + " order by ZTCONO, ZTBRNO, ZTUGNO + ZTAPNO DESC"
                    + "";

            ZAUTDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZAUTDto GetNext(ZAUTDto obj)
        {
            string strSql = "SELECT TOP 1 DISTINCT"
                    + " ZTCONO "
                    + ", ZTBRNO "
                    + ", ZTUGNO "
                    + ", ZTAPNO "
                    + ", ZTUGNO+ZTAPNO "
                    + " from ZAUT "
                    + " where 1=1 "
                    + " and ZTCONO = '" + obj.ZTCONO.Trim() + "'"
                    + " and ZTBRNO = '" + obj.ZTBRNO.Trim() + "'"
                    + " and ZTUGNO + ZTAPNO > '" + obj.ZTUGNO.Trim() + obj.ZTAPNO.Trim() + "'"
                    + " order by ZTCONO, ZTBRNO, ZTUGNO + ZTAPNO ASC"
                    + "";

            ZAUTDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZAUTDto GetLast(ZAUTDto obj)
        {
            string strSql = "SELECT TOP 1 DISTINCT"
                    + " ZTCONO "
                    + ", ZTBRNO "
                    + ", ZTUGNO "
                    + ", ZTAPNO "
                    + ", ZTUGNO+ZTAPNO "
                    + " from ZAUT "
                    + " where 1=1 "
                    + " and ZTCONO = '" + obj.ZTCONO.Trim() + "'"
                    + " and ZTBRNO = '" + obj.ZTBRNO.Trim() + "'"
                    + " order by ZTCONO, ZTBRNO, ZTUGNO+ZTAPNO DESC "
                    + "";

            ZAUTDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<ZAUTDto> GetListPaging(ZAUTDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                    + " ZTCONO "
                    + ", ZTBRNO "
                    + ", ZTUGNO "
                    + ", ZTAPNO "
                    + ", ZTMENO "
                    + ", ZTRIGH "
                    + ", B.ZRVANA AS ZTRCNA"
                    + ", ZTSYST "
                    + ", ZTSTAT "
                    + ", ZTCRDT "
                    + ", ZTCRTM "
                    + ", ZTCRUS "
                    + ", ZTCHDT "
                    + ", ZTCHTM "
                    + ", ZTCHUS "
                    + ", ZMMENA "
                    + ", ZMPGNO "
                    + ", A.ZRVANA AS ZMMETY "
                    + ", ZMPARM "
                    + ", ZMREMA "
                    + " from ZAUT "
                    + " left join ZMNU on 1=1"
                    + "     and ZMCONO = ZTCONO"
                    + "     and ZMBRNO = ZTBRNO"
                    + "     and ZMAPNO = ZTAPNO"
                    + "     and ZMMENO = ZTMENO"
                    + " left join ZVAR A on 1=1"
                    + "     and A.ZRCONO = ''"
                    + "     and A.ZRBRNO = ''"
                    + "     and A.ZRVATY = 'METY'"
                    + "     and A.ZRVAVL = ZMMETY"
                    + " left join ZVAR B on 1=1"
                    + "     and B.ZRCONO = ''"
                    + "     and B.ZRBRNO = ''"
                    + "     and B.ZRVATY = 'RCST'"
                    + "     and B.ZRVAVL = ZTRCST"
                    + " where 1=1 ";

            if (obj.ZTCONO != null && obj.ZTCONO != String.Empty)
            {
                strSql += " AND ZTCONO LIKE '%" + obj.ZTCONO.Trim() + "%' ";
            }

            if (obj.ZTBRNO != null && obj.ZTBRNO != String.Empty)
            {
                strSql += " AND ZTBRNO LIKE '%" + obj.ZTBRNO.Trim() + "%' ";
            }

            if (obj.ZTUGNO != null && obj.ZTUGNO != String.Empty)
            {
                strSql += " AND ZTUGNO = '" + obj.ZTUGNO.Trim() + "' ";
            }

            if (obj.ZTAPNO != null && obj.ZTAPNO != String.Empty)
            {
                strSql += " AND ZTAPNO = '" + obj.ZTAPNO.Trim() + "' ";
            }

            if (obj.ZTMENO != null && obj.ZTMENO != String.Empty)
            {
                strSql += " AND ZTMENO = '" + obj.ZTMENO.Trim() + "' ";
            }

            List<ZAUTDto> dto = this.ExecutePaging(strSql, "ZTCONO, ZTBRNO, ZTUGNO, ZTAPNO, ZTMENO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public List<ZAUTDto> GetListPagingByUserGroupApp(ZAUTDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                    + " ZTCONO "
                    + ", ZTBRNO "
                    + ", ZTUGNO "
                    + ", ZTAPNO "
                    + ", ZTMENO "
                    + ", ZTRIGH "
                    + ", ZTSYST "
                    + ", ZTSTAT "
                    + ", ZTRCST "
                    + ", ZTCRDT "
                    + ", ZTCRTM "
                    + ", ZTCRUS "
                    + ", ZTCHDT "
                    + ", ZTCHTM "
                    + ", ZTCHUS "
                    + ", ZMMENA "
                    + ", ZMPGNO "
                    + ", ZMMETY"
                    + ", ZMPARM"
                    + ", ZMREMA"
                    + ", ZRVANA "
                    + " from ZAUT "
                    + " left join ZMNU on 1=1"
                    + "     and ZMCONO = ZTCONO"
                    + "     and ZMBRNO = ZTBRNO"
                    + "     and ZMAPNO = ZTAPNO"
                    + "     and ZMMENO = ZTMENO"
                    + " left join ZVAR on 1=1"
                    + "     and ZRCONO = ''"
                    + "     and ZRBRNO = ''"
                    + "     and ZRVATY = 'METY'"
                    + "     and ZRVAVL = ZMMETY"
                    + " where 1=1 ";

            if (obj.ZTCONO != null && obj.ZTCONO != String.Empty)
            {
                strSql += " AND ZTCONO = '" + obj.ZTCONO.Trim() + "' ";
            }

            if (obj.ZTBRNO != null && obj.ZTBRNO != String.Empty)
            {
                strSql += " AND ZTBRNO = '" + obj.ZTBRNO.Trim() + "' ";
            }

            if (obj.ZTUGNO != null && obj.ZTUGNO != String.Empty)
            {
                strSql += "AND ZTUGNO = '" + obj.ZTUGNO.Trim() + "' ";
            }

            if (obj.ZTAPNO != null && obj.ZTAPNO != String.Empty)
            {
                strSql += "AND ZTAPNO = '" + obj.ZTAPNO.Trim() + "' ";
            }

            if (obj.ZTMENO != null && obj.ZTMENO != String.Empty)
            {
                strSql += "AND ZTMENO = '" + obj.ZTMENO.Trim() + "' ";
            }

            List<ZAUTDto> dto = this.ExecutePaging(strSql, "ZTCONO, ZTBRNO, ZTUGNO, ZTAPNO, ZTMENO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public List<ZAUTDto> GetListByUserIdParentId(ZAUTDto obj)
        {
            List<ZAUTDto> lst = null;

            string str = string.Empty;

            if (!string.IsNullOrEmpty(obj.ZHUSNO))
            {
                str = "select "
                        + "  ZTAPNO"
                        + ", ZTMENO"
                        + ", ZTRIGH"
                        + ", ZMMENA"
                        + ", ZMIURL"
                        + ", ZMMEPA"
                        + ", ZMMETY"
                        + ", ZMPARM"
                        + ", ZMREMA"
                        + ", ZAAPNA"
                        + ", ZAAURL"
                        + ", ZPPURL"
                        + ", ZPREMA"
                        + " from ZAUT"
                        + " left join ZMNU on 1=1"
                        + "     and ZMCONO = ZTCONO"
                        + "     and ZMBRNO = ZTBRNO"
                        + "     and ZMAPNO = ZTAPNO"
                        + "     and ZMMENO = ZTMENO"
                        + " left join ZAPP on 1=1"
                        + "     and ZACONO = ZTCONO"
                        + "     and ZABRNO = ZTBRNO"
                        + "     and ZAAPNO = ZTAPNO"
                        + " left join ZPGM on 1=1"
                        + "     and ZPCONO = ZMCONO"
                        + "     and ZPBRNO = ZMBRNO"
                        + "     and ZPAPNO = ZMAPNO"
                        + "     and ZPPGNO = ZMPGNO"
                        + " left join ZAUT on 1=1"
                        + "     and ZGCONO = ZTCONO"
                        + "     and ZGBRNO = ZTBRNO"
                        + "     and ZGUGNO = ZTUGNO"
                        + " left join ZMNU on 1=1"
                        + "     and ZHCONO = ZTCONO"
                        + "     and ZHBRNO = ZTBRNO"
                        + "     and ZHUGNO = ZTUGNO"
                        + " where 1=1"
                        + " and ZGRCST = 1"
                        + " and ZHUSNO = '" + obj.ZHUSNO.Trim() + "'"
                        + " and ZMMEPA = '" + obj.ZMMEPA.Trim() + "'"
                        + " and ZTAPNO = '" + obj.ZTAPNO.Trim() + "'"
                        + " order by ZMMESQ asc ";
            }
            else
            {
                str = "select "
                    + "  ZMAPNO AS ZTAPNO"
                    + ", ZMMENO AS ZTMENO"
                    + ", 'W' AS ZTRIGH"
                    + ", ZMMENA"
                    + ", ZMIURL"
                    + ", ZMMEPA"
                    + ", ZMMETY"
                    + ", ZMPARM"
                    + ", ZMREMA"
                    + ", ZAAPNA"
                    + ", ZAAURL"
                    + ", ZPPURL"
                    + ", ZPREMA"
                    + " from ZMNU "
                    + " left join ZAPP on 1=1"
                    + "     and ZACONO = ZMCONO"
                    + "     and ZABRNO = ZMBRNO"
                    + "     and ZAAPNO = ZMAPNO"
                    + " left join ZPGM on 1=1"
                    + "     and ZPCONO = ZMCONO"
                    + "     and ZPBRNO = ZMBRNO"
                    + "     and ZPAPNO = ZMAPNO"
                    + "     and ZPPGNO = ZMPGNO"
                    + " where 1=1"
                    + " and ZMMEPA = '" + obj.ZMMEPA.Trim() + "'"
                    + " and ZMAPNO = '" + obj.ZTAPNO.Trim() + "'"
                    + " order by ZMMESQ asc "
                    + "";
            }

            lst = this.ExecuteQuery(str);

            return lst;
        }

        public List<ZAUTDto> GetListByUserIdApp(ZAUTDto obj)
        {
            List<ZAUTDto> lst = null;

            string str = string.Empty;

            if (!string.IsNullOrEmpty(obj.ZHUSNO))
            {
                str = "select "
                        + "  ZTAPNO"
                        + ", ZTMENO"
                        + ", ZTRIGH"
                        + ", ZMMENA"
                        + ", ZMMEPA"
                        + ", ZMIURL"
                        + ", ZMMETY"
                        + ", ZMPARM"
                        + ", ZMREMA"
                        + ", ZAAPNA"
                        + ", ZAAURL"
                        + ", ZPPURL"
                        + ", ZPREMA"
                        + " from ZAUT"
                        + " left join ZAPP on 1=1"
                        + "     and ZACONO = ZTCONO"
                        + "     and ZABRNO = ZTBRNO"
                        + "     and ZAAPNO = ZTAPNO"
                        + " left join ZMNU on 1=1"
                        + "     and ZMCONO = ZTCONO"
                        + "     and ZMBRNO = ZTBRNO"
                        + "     and ZMAPNO = ZTAPNO"
                        + "     and ZMMENO = ZTMENO"
                        + " left join ZPGM on 1=1"
                        + "     and ZPCONO = ZMCONO"
                        + "     and ZPBRNO = ZMBRNO"
                        + "     and ZPAPNO = ZMAPNO"
                        + "     and ZPPGNO = ZMPGNO"
                        + " left join ZUG1 on 1=1"
                        + "     and ZGCONO = ZTCONO"
                        + "     and ZGBRNO = ZTBRNO"
                        + "     and ZGUGNO = ZTUGNO"
                        + " left join ZUG2 on 1=1"
                        + "     and ZHCONO = ZGCONO"
                        + "     and ZHBRNO = ZGBRNO"
                        + "     and ZHUGNO = ZGUGNO"
                        + " where 1=1"
                        + " and ZGRCST = 1"
                        + " and ZMRCST = 1"
                        + " and ZHUSNO = '" + obj.ZHUSNO.Trim() + "'"
                        + " and ZTAPNO like '%" + obj.ZTAPNO.Trim() + "%'"
                        + " order by ZMMESQ asc ";
            }
            else
            {
                str = "select "
                    + "  ZMAPNO AS ZTAPNO"
                    + ", ZMMENO AS ZTMENO"
                    + ", 'W' AS ZTRIGH"
                    + ", ZMMENA"
                    + ", ZMMEPA"
                    + ", ZMIURL"
                    + ", ZMMETY"
                    + ", ZMPARM"
                    + ", ZMREMA"
                    + ", ZAAPNA"
                    + ", ZAAURL"
                    + ", ZPPURL"
                    + ", ZPREMA"
                    + " from ZMNU "
                    + " left join ZAPP on 1=1"
                    + "     and ZACONO = ZMCONO"
                    + "     and ZABRNO = ZMBRNO"
                    + "     and ZMAPNO = ZAAPNO"
                    + " left join ZPGM on 1=1"
                    + "     and ZPCONO = ZMCONO"
                    + "     and ZPBRNO = ZMBRNO"
                    + "     and ZPAPNO = ZMAPNO"
                    + "     and ZPPGNO = ZMPGNO"
                    + " where 1=1"
                    + " and ZMRCST = 1"
                    + " and ZMAPNO LIKE '%" + obj.ZTAPNO.Trim() + "%'"
                    + " order by ZMMESQ asc "
                    + "";
            }

            lst = this.ExecuteQuery(str);

            return lst;
        }

        public List<ZAUTDto> GetListByUserIdMenu(ZAUTDto obj)
        {
            string str = "select "
                    + "  ZTAPNO"
                    + ", ZTMENO"
                    + ", ZTRIGH"
                    + ", ZMMENA"
                    + ", ZMIURL"
                    + ", ZMMETY"
                    + ", ZMPARM"
                    + ", ZMREMA"
                    + ", ZAAPNA"
                    + ", ZAAURL"
                    + ", ZPPURL"
                    + ", ZPREMA"
                    + " from ZAUT"
                    + " left join ZAPP on 1=1"
                    + "     and ZACONO = ZTCONO"
                    + "     and ZABRNO = ZTBRNO"
                    + "     and ZAAPNO = ZTAPNO"
                    + " left join ZMNU on 1=1"
                    + "     and ZMCONO = ZTCONO"
                    + "     and ZMBRNO = ZTBRNO"
                    + "     and ZMAPNO = ZTAPNO"
                    + "     and ZMMENO = ZTMENO"
                    + " left join ZPGM on 1=1"
                    + "     and ZPCONO = ZMCONO"
                    + "     and ZPBRNO = ZMBRNO"
                    + "     and ZPAPNO = ZMAPNO"
                    + "     and ZPPGNO = ZMPGNO"
                    + " left join ZAUT on 1=1"
                    + "     and ZGCONO = ZTCONO"
                    + "     and ZGBRNO = ZTBRNO"
                    + "     and ZGUGNO = ZTUGNO"
                    + " left join ZMNU on 1=1"
                    + "     and ZHCONO = ZGCONO"
                    + "     and ZHBRNO = ZGBRNO"
                    + "     and ZHUGNO = ZGUGNO"
                    + " where 1=1"
                    + " AND ZGRCST = 1"
                    + " AND ZHUSNO = '" + obj.ZHUSNO.Trim() + "'"
                    + " AND ZTAPNO = '" + obj.ZTAPNO.Trim() + "'"
                    + " AND ZMMETY = '" + BaseMethod.GetVariableValue("METY_DETAIL") + "'"
                    + " ORDER BY ZMMESQ ASC ";

            return this.ExecuteQuery(str);
        }

        public List<ZAUTDto> GetListByUserGroupApp(ZAUTDto obj)
        {
            List<ZAUTDto> lst = null;

            string str = string.Empty;

            if (!string.IsNullOrEmpty(obj.ZTUGNO))
            {
                str = "select DISTINCT "
                            + "  ZTCONO"
                            + ", ZTBRNO"
                            + ", ZTUGNO"
                            + ", ZTAPNO"
                            + ", ZAAPNA"
                            + ", ZGUGNA"
                            + ", ZTRCST"
                            + " from ZAUT"
                            + " left join ZAPP on 1=1"
                            + "     and ZACONO = ZTCONO"
                            + "     and ZABRNO = ZTBRNO"
                            + "     and ZAAPNO = ZTAPNO"
                            + " left join ZMNU on 1=1"
                            + "     and ZMCONO = ZTCONO"
                            + "     and ZMBRNO = ZTBRNO"
                            + "     and ZMAPNO = ZTAPNO"
                            + "     and ZMMENO = ZTMENO"
                            + " left join ZPGM on 1=1"
                            + "     and ZPCONO = ZMCONO"
                            + "     and ZPBRNO = ZMBRNO"
                            + "     and ZPAPNO = ZMAPNO"
                            + "     and ZPPGNO = ZMPGNO"
                            + " left join ZAUT on 1=1"
                            + "     and ZGCONO = ZTCONO"
                            + "     and ZGBRNO = ZTBRNO"
                            + "     and ZGUGNO = ZTUGNO"
                            + " where 1=1"
                            + "";

                if (!string.IsNullOrEmpty(obj.ZTUGNO))
                {
                    str += " and ZTUGNO = '" + obj.ZTUGNO.Trim() + "' ";
                }

                if (!string.IsNullOrEmpty(obj.ZTAPNO))
                {
                    str += " and ZTAPNO = '" + obj.ZTAPNO.Trim() + "' ";
                }
            }
            else
            {
                str = "select DISTINCT "
                        + "  ZMCONO AS ZTCONO"
                        + ", ZMBRNO AS ZTBRNO"
                        + ", 'ADMIN' AS ZTUGNO"
                        + ", ZMAPNO AS ZTAPNO"
                        + ", ZAAPNA"
                        + ", 'ADMIN' AS ZGUGNA"
                        + ", 1 AS ZTRCST"
                        + " from ZMNU "
                        + " left join ZAPP on 1=1"
                        + "     and ZACONO = ZMCONO"
                        + "     and ZABRNO = ZMBRNO"
                        + "     and ZMAPNO = ZAAPNO"
                        + " left join ZPGM on 1=1"
                        + "     and ZPCONO = ZMCONO"
                        + "     and ZPBRNO = ZMBRNO"
                        + "     and ZPAPNO = ZMAPNO"
                        + "     and ZPPGNO = ZMPGNO"
                        + " where 1=1"
                        + "";

                if (!string.IsNullOrEmpty(obj.ZTAPNO))
                {
                    str += " and ZTAPNO = '" + obj.ZTAPNO.Trim() + "' ";
                }
            }

            lst = this.ExecuteQuery(str);

            return lst;
        }

        public List<ZAUTDto> GetMenu(ZAUTDto obj)
        {
            List<ZAUTDto> lst = null;
            string str = string.Empty;
            
                str = "SELECT"
                    + " ZTCONO"
                    + ", ZTBRNO"
                    + ", ZTUGNO"
                    + ", ZTAPNO"
                    + ", ZTMENO"
                    + ", ZTRIGH"
                    + ", ZTRCST"
                    + ", ZTCRDT"
                    + ", ZTCRTM"
                    + ", ZTCRUS"
                    + ", ZTCHDT"
                    + ", ZTCHTM"
                    + ", ZTCHUS"
                    + ", ZMMENO"
                    + ", ZMMENA"
                    + ", ZMIURL"
                    + ", ZMMETY"
                    + ", ZMMEPA"
                    + ", ZMPARM"
                    + ", ZMREMA"
                    + ", ZPPURL"
                    + " from ZAUT"
                    + " left join ZMNU on 1=1"
                    + "     and ZMCONO = ZTCONO"
                    + "     and ZMBRNO = ZTBRNO"
                    + "     and ZMAPNO = ZTAPNO"
                    + "     and ZMMENO = ZTMENO"
                    + " left join ZPGM on 1=1"
                    + "     and ZPCONO = ZMCONO"
                    + "     and ZPBRNO = ZMBRNO"
                    + "     and ZPAPNO = ZMAPNO"
                    + "     and ZPPGNO = ZMPGNO"
                    + " left join ZAUT on 1=1"
                    + "     and ZGCONO = ZTCONO"
                    + "     and ZGBRNO = ZTBRNO"
                    + "     and ZGUGNO = ZTUGNO"
                    + " left join ZMNU on 1=1"
                    + "     and ZHCONO = ZGCONO"
                    + "     and ZHBRNO = ZGBRNO"
                    + "     and ZHUGNO = ZGUGNO"
                    + " where 1=1 "
                    + " and ZTRCST = 1 "
                    + " and ZGRCST = 1 "
                    + " and ZHUSNO = '" + obj.ZHUSNO.Trim() + "'";

                if (obj.ZTAPNO != null && obj.ZTAPNO != String.Empty)
                {
                    str += " and ZTAPNO = '" + obj.ZTAPNO.Trim() + "'";
                }

                if (obj.ZMMETY != null && obj.ZMMETY != String.Empty)
                {
                    str += " and ZMMETY = '" + obj.ZMMETY.Trim() + "'";
                }

                if (obj.ZTMENO != null && obj.ZTMENO != String.Empty)
                {
                    str += " and ZTMENO = '" + obj.ZTMENO.Trim() + "' ";
                }
            
            lst = this.ExecuteQuery(str);

            return lst;
        }


            public List<ZAUTDto> GetListMenuGroupByUserId(ZAUTDto obj)
        {
            List<ZAUTDto> lst = null;
            string str = string.Empty;

            if (!string.IsNullOrEmpty(obj.ZHUSNO))
            {
                str = "SELECT"
                    + " ZTCONO"
                    + ", ZTBRNO"
                    + ", ZTUGNO"
                    + ", ZTAPNO"
                    + ", ZTMENO"
                    + ", ZTRIGH"
                    + ", ZTRCST"
                    + ", ZTCRDT"
                    + ", ZTCRTM"
                    + ", ZTCRUS"
                    + ", ZTCHDT"
                    + ", ZTCHTM"
                    + ", ZTCHUS"
                    + ", ZMMENO"
                    + ", ZMMENA"
                    + ", ZMIURL"
                    + ", ZMMETY"
                    + ", ZMMEPA"
                    + ", ZMPARM"
                    + ", ZMREMA"
                    + ", ZPPURL"
                    + " from ZAUT"
                    + " left join ZMNU on 1=1"
                    + "     and ZMCONO = ZTCONO"
                    + "     and ZMBRNO = ZTBRNO"
                    + "     and ZMAPNO = ZTAPNO"
                    + "     and ZMMENO = ZTMENO"
                    + " left join ZPGM on 1=1"
                    + "     and ZPCONO = ZMCONO"
                    + "     and ZPBRNO = ZMBRNO"
                    + "     and ZPAPNO = ZMAPNO"
                    + "     and ZPPGNO = ZMPGNO"
                    + " left join ZAUT on 1=1"
                    + "     and ZGCONO = ZTCONO"
                    + "     and ZGBRNO = ZTBRNO"
                    + "     and ZGUGNO = ZTUGNO"
                    + " left join ZMNU on 1=1"
                    + "     and ZHCONO = ZGCONO"
                    + "     and ZHBRNO = ZGBRNO"
                    + "     and ZHUGNO = ZGUGNO"
                    + " where 1=1 "
                    + " and ZTRCST = 1 "
                    + " and ZGRCST = 1 "
                    + " and ZHUSNO = '" + obj.ZHUSNO.Trim() + "'";

                    if (obj.ZTAPNO != null && obj.ZTAPNO != String.Empty)
                    {
                        str += " and ZTAPNO = '" + obj.ZTAPNO.Trim() + "'";
                    }

                    if (obj.ZMMETY != null && obj.ZMMETY != String.Empty)
                    {
                        str += " and ZMMETY = '" + obj.ZMMETY.Trim() + "'";
                    }

                    if (obj.ZMMEPA != null && obj.ZMMEPA != String.Empty)
                    {
                        str += " and ZMMEPA = '" + obj.ZMMEPA.Trim() + "' ";
                    }
            }
            else
            {
                str = "select "
                    + "  ZMCONO AS ZTCONO"
                    + ", ZMBRNO AS ZTBRNO"
                    + ", '' AS ZTUGNO"
                    + ", ZMAPNO AS ZTAPNO"
                    + ", ZMMENO AS ZTMENO"
                    + ", 'W' AS ZTRIGH"
                    + ", ZMRCST AS ZTRCST"
                    + ", ZMCRDT AS ZTCRDT"
                    + ", ZMCRTM AS ZTCRTM"
                    + ", ZMCRUS AS ZTCRUS"
                    + ", ZMCHDT AS ZTCHDT"
                    + ", ZMCHTM AS ZTCHTM"
                    + ", ZMCHUS AS ZTCHUS"
                    + ", ZMMENO"
                    + ", ZMMENA"
                    + ", ZMIURL"
                    + ", ZMMETY"
                    + ", ZMMEPA"
                    + ", ZMPARM"
                    + ", ZMREMA"
                    + ", ZPPURL"
                    + " from ZMNU"
                    + " left join ZPGM on 1=1"
                    + "     and ZPCONO = ZMCONO"
                    + "     and ZPBRNO = ZMBRNO"
                    + "     and ZPAPNO = ZMAPNO"
                    + "     and ZPPGNO = ZMPGNO"
                    + " where 1=1"
                    + " and ZTRCST = 1 "
                    + " and ZGRCST = 1 ";

                    if (obj.ZTAPNO != null && obj.ZTAPNO != String.Empty)
                    {
                        str += " and ZMAPNO = '" + obj.ZTAPNO.Trim() + "'";
                    }

                    if (obj.ZMMETY != null && obj.ZMMETY != String.Empty)
                    {
                        str += " and ZMMETY = '" + obj.ZMMETY.Trim() + "'";
                    }

                    if (obj.ZMMEPA != null && obj.ZMMEPA != String.Empty)
                    {
                        str += " and ZMMEPA = '" + obj.ZMMEPA.Trim() + "' ";
                    }
            }

            str += " order by ZMMESQ asc ";

            lst = this.ExecuteQuery(str);

            return lst;
        }

        public List<ZAUTDto> GetListMenuByUserId(ZAUTDto obj)
        {
            List<ZAUTDto> lst = null;
            string str = string.Empty;

            if (!string.IsNullOrEmpty(obj.ZHUSNO))
            {
                str = "select"
                        + "  ZTCONO"
                        + ", ZTBRNO"
                        + ", ZTUGNO"
                        + ", ZTAPNO"
                        + ", ZTMENO"
                        + ", ZTRIGH"
                        + ", ZTRCST"
                        + ", ZTCRDT"
                        + ", ZTCRTM"
                        + ", ZTCRUS"
                        + ", ZTCHDT"
                        + ", ZTCHTM"
                        + ", ZTCHUS"
                        + ", ZMMENA"
                        + ", ZMIURL"
                        + ", ZMMETY"
                        + ", ZMPARM"
                        + ", ZMREMA"
                        + ", ZMMEPA"
                        + ", Z2USNO"
                        + ", ZPPURL"
                        + " from ZAUT"
                        + " left join ZMNU on 1=1"
                        + "     and ZMCONO = ZTCONO"
                        + "     and ZMBRNO = ZTBRNO"
                        + "     and ZMAPNO = ZTAPNO"
                        + "     and ZMMENO = ZTMENO"
                        + " left join ZPGM on 1=1"
                        + "     and ZPCONO = ZMCONO"
                        + "     and ZPBRNO = ZMBRNO"
                        + "     and ZPAPNO = ZMAPNO"
                        + "     and ZPPGNO = ZMPGNO"
                        + " left join ZMNU on 1=1"
                        + "     and ZHCONO = ZTCONO"
                        + "     and ZHBRNO = ZTBRNO"
                        + "     and ZHUGNO = ZTUGNO"
                        + " where 1=1"
                        + " and ZHUSNO = '" + obj.ZHUSNO.Trim() + "'"
                        + "";
            }
            else
            {
                str = "select "
                        + "  ZMCONO AS ZTCONO"
                        + ", ZMBRNO AS ZTBRNO"
                        + ", '' AS ZTUGNO"
                        + ", ZMAPNO AS ZTAPNO"
                        + ", ZMMENO AS ZTMENO"
                        + ", 'W' AS ZTRIGH"
                        + ", ZMRCST AS ZTRCST"
                        + ", ZMCRDT AS ZTCRDT"
                        + ", ZMCRTM AS ZTCRTM"
                        + ", ZMCRUS AS ZTCRUS"
                        + ", ZMCHDT AS ZTCHDT"
                        + ", ZMCHTM AS ZTCHTM"
                        + ", ZMCHUS AS ZTCHUS"
                        + ", ZMMENA"
                        + ", ZMIURL"
                        + ", ZMMETY"
                        + ", ZMPARM"
                        + ", ZMREMA"
                        + ", ZMMEPA"
                        + ", ZPPURL"
                        + " from ZMNU"
                        + " left join ZPGM on 1=1"
                        + "     and ZPCONO = ZMCONO"
                        + "     and ZPBRNO = ZMBRNO"
                        + "     and ZPAPNO = ZMAPNO"
                        + "     and ZPPGNO = ZMPGNO"
                        + " where 1=1"
                        + "";
            }

            str += " order by ZMMESQ asc ";

            lst = this.ExecuteQuery(str);

            return lst;
        }
        public List<ZAUTDto> GetListApplication(ZAUTDto obj)
        {
            string strSql = "SELECT DISTINCT "
                    + " ZTAPNO "
                    + ", ZAAPNA "
                    + ", ZAAURL "
                    + ", ZAIURL "
                    + ", ZAACLR "
                    + ", ZAAPSQ "
                    + ", ZAREMA "
                    + " FROM ZAUT "
                    + " JOIN ZUG2 ON 1=1"
                    + "     AND ZHCONO = ZTCONO"
                    + "     AND ZHBRNO = ZTBRNO"
                    + "     AND ZHUGNO = ZTUGNO"
                    + " JOIN ZAPP ON 1=1"
                    + "     AND ZACONO = ZTCONO"
                    + "     AND ZABRNO = ZTBRNO"
                    + "     AND ZAAPNO = ZTAPNO"
                    + " WHERE 1=1 "
                    + " AND ZTRCST = 1 "
                    + " AND ZARCST = 1 "
                    + " AND ZASTAT IN ('15','20') "
                    + "";

            if (obj.ZHUSNO != null && obj.ZHUSNO != String.Empty)
            {
                strSql += " AND ZHUSNO = '" + obj.ZHUSNO.Trim() + "' ";
            }

            List<ZAUTDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }
        #endregion
    }
}
