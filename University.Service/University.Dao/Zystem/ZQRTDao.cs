#region Summary
//''''''''''''''''''''''''''''S U M M A R Y '''''''''''''''''''''''''''''
//'File Name     : ZQRTDao.cs
//'Author        : Vinno
//'Creation Date : 6/21/2016
//'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
#endregion

#region Reference
using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data;

using University.Dto.Base;
using University.Dao.Base;
using University.Dto.Zystem;
using University.Dao.Connector;
#endregion

namespace University.Dao.Zystem
{
    public class ZQRTDao : BaseDao<ZQRTDto>
    {
        #region Constructor
        public ZQRTDao()
		{
			this.MainDataSource = DataSource.University;
		}
        #endregion

        #region Abstract Class Implementation
        protected override Mapper<ZQRTDto> GetMapper()
        {
            Mapper<ZQRTDto> mapDto = new ZQRTDtoMap();
            return mapDto;
        }
        #endregion

        #region Save Data
        public string Save(ZQRTDto obj, out string strDocNo)
        {
            obj.ZQSYST = BaseMethod.SystReady;
            obj.ZQSTAT = BaseMethod.StatDraft;
            obj.ZQCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZQCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZQCHDT = obj.ZQCRDT;
            obj.ZQCHTM = obj.ZQCRTM;

            strDocNo = string.Empty;

            if (string.IsNullOrEmpty(obj.ZQQRNO))
            {
                //obj.ZQQRNO = Document.GenerateSysNo(obj.ZQCONO, obj.ZQBRNO, "QRY", obj.ZQCHUS);
                strDocNo = obj.ZQQRNO;

                return Insert(obj);
            }
            else
            {
                return Update(obj);
            }
        }
        #endregion

        #region Insert Data
        public string Insert(ZQRTDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("ZQCONO");
            lstField.Add("ZQBRNO");
            lstField.Add("ZQQRNO");
            lstField.Add("ZQQRNA");
            lstField.Add("ZQQURY");
            lstField.Add("ZQREMA");
            lstField.Add("ZQSYST");
            lstField.Add("ZQSTAT");
            lstField.Add("ZQRCST");
            lstField.Add("ZQCRDT");
            lstField.Add("ZQCRTM");
            lstField.Add("ZQCRUS");
            lstField.Add("ZQCHDT");
            lstField.Add("ZQCHTM");
            lstField.Add("ZQCHUS");

            string strSql = this.GenerateStringInsert("ZQRT", lstField, obj);
            return this.ExecuteDbNonQuery(strSql);
        }
        #endregion

        #region Update Data
        public string Update(ZQRTDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("ZQQRNA");
            lstField.Add("ZQQURY");
            lstField.Add("ZQREMA");
            lstField.Add("ZQSYST");
            lstField.Add("ZQSTAT");
            lstField.Add("ZQRCST");
            lstField.Add("ZQCHDT");
            lstField.Add("ZQCHTM");
            lstField.Add("ZQCHUS");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("ZQCONO");
            lstCondition.Add("ZQBRNO");
            lstCondition.Add("ZQQRNO");

            string strSql = this.GenerateStringUpdate("ZQRT", lstCondition, lstField, obj);
            return this.ExecuteDbNonQuery(strSql);
        }
        #endregion

        #region Delete Data
        public string Delete(ZQRTDto obj)
        {
            List<string> lstCondition = new List<string>();
            lstCondition.Add("ZQCONO");
            lstCondition.Add("ZQBRNO");
            lstCondition.Add("ZQQRNO");

            string strSql = this.GenerateStringDelete("ZQRT", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }
        #endregion

        #region Select Data
        public bool IsExists(ZQRTDto obj)
        {
            string strSql = "SELECT COUNT(*) FROM ZQRT WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.ZQCONO))
            {
                strSql += " AND ZQCONO = '" + obj.ZQCONO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZQBRNO))
            {
                strSql += " AND ZQBRNO = '" + obj.ZQBRNO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZQQRNO))
            {
                strSql += " AND ZQQRNO = '" + obj.ZQQRNO.Trim() + "' ";
            }

            Object _obj = this.ExecuteDbScalar(strSql);
            if (Convert.ToInt32(_obj) == 0)
            {
                return false;
            }
            return true;
        }

        public ZQRTDto Get(ZQRTDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("ZQCONO");
            lstField.Add("ZQBRNO");
            lstField.Add("ZQQRNO");
            lstField.Add("ZQQRNA");
            lstField.Add("ZQQURY");
            lstField.Add("ZQREMA");
            lstField.Add("ZQSYST");
            lstField.Add("ZQSTAT");
            lstField.Add("ZQRCST");
            lstField.Add("ZQCRDT");
            lstField.Add("ZQCRTM");
            lstField.Add("ZQCRUS");
            lstField.Add("ZQCHDT");
            lstField.Add("ZQCHTM");
            lstField.Add("ZQCHUS");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("ZQCONO");
            lstCondition.Add("ZQBRNO");
            lstCondition.Add("ZQQRNO");

            string strSql = this.GenerateStringSelect("ZQRT", lstCondition, lstField, obj);
            ZQRTDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZQRTDto GetFirst(ZQRTDto obj)
        {
            string strSql = "SELECT TOP 1"
                            + " ZQCONO "
                            + ", ZQBRNO "
                            + ", ZQQRNO "
                            + ", ZQQRNA "
                            + ", ZQQURY "
                            + ", ZQREMA "
                            + ", ZQSYST "
                            + ", ZQSTAT "
                            + ", ZQRCST "
                            + ", ZQCRDT "
                            + ", ZQCRTM "
                            + ", ZQCRUS "
                            + ", ZQCHDT "
                            + ", ZQCHTM "
                            + ", ZQCHUS "
                            + " FROM ZQRT "
                            + " WHERE 1=1"
                            + " AND ZQCONO = '" + obj.ZQCONO.Trim() + "' "
                            + " AND ZQBRNO = '" + obj.ZQBRNO.Trim() + "' "
                            + " ORDER BY ZQQRNO ASC"
                            + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZQRTDto GetPrevious(ZQRTDto obj)
        {
            string strSql = "SELECT TOP 1"
                            + " ZQCONO "
                            + ", ZQBRNO "
                            + ", ZQQRNO "
                            + ", ZQQRNA "
                            + ", ZQQURY "
                            + ", ZQREMA "
                            + ", ZQSYST "
                            + ", ZQSTAT "
                            + ", ZQRCST "
                            + ", ZQCRDT "
                            + ", ZQCRTM "
                            + ", ZQCRUS "
                            + ", ZQCHDT "
                            + ", ZQCHTM "
                            + ", ZQCHUS "
                            + " FROM ZQRT "
                            + " WHERE 1=1"
                            + " AND ZQCONO = '" + obj.ZQCONO.Trim() + "' "
                            + " AND ZQBRNO = '" + obj.ZQBRNO.Trim() + "' "
                            + " AND ZQQRNO < '" + obj.ZQQRNO.Trim() + "' "
                            + " ORDER BY ZQQRNO DESC"
                            + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZQRTDto GetNext(ZQRTDto obj)
        {
            string strSql = "SELECT TOP 1"
                            + " ZQCONO "
                            + ", ZQBRNO "
                            + ", ZQQRNO "
                            + ", ZQQRNA "
                            + ", ZQQURY "
                            + ", ZQREMA "
                            + ", ZQSYST "
                            + ", ZQSTAT "
                            + ", ZQRCST "
                            + ", ZQCRDT "
                            + ", ZQCRTM "
                            + ", ZQCRUS "
                            + ", ZQCHDT "
                            + ", ZQCHTM "
                            + ", ZQCHUS "
                            + " FROM ZQRT "
                            + " WHERE 1=1"
                            + " AND ZQCONO = '" + obj.ZQCONO.Trim() + "' "
                            + " AND ZQBRNO = '" + obj.ZQBRNO.Trim() + "' "
                            + " AND ZQQRNO > '" + obj.ZQQRNO.Trim() + "' "
                            + " ORDER BY ZQQRNO ASC"
                            + "";

            return this.ExecuteQueryOne(strSql);
        }

        public ZQRTDto GetLast(ZQRTDto obj)
        {
            string strSql = "SELECT TOP 1"
                            + " ZQCONO "
                            + ", ZQBRNO "
                            + ", ZQQRNO "
                            + ", ZQQRNA "
                            + ", ZQQURY "
                            + ", ZQREMA "
                            + ", ZQSYST "
                            + ", ZQSTAT "
                            + ", ZQRCST "
                            + ", ZQCRDT "
                            + ", ZQCRTM "
                            + ", ZQCRUS "
                            + ", ZQCHDT "
                            + ", ZQCHTM "
                            + ", ZQCHUS "
                            + " FROM ZQRT "
                            + " WHERE 1=1"
                            + " AND ZQCONO = '" + obj.ZQCONO.Trim() + "' "
                            + " AND ZQBRNO = '" + obj.ZQBRNO.Trim() + "' "
                            + " ORDER BY ZQQRNO DESC"
                            + "";

            return this.ExecuteQueryOne(strSql);
        }

        public List<ZQRTDto> GetList(ZQRTDto obj)
        {
            string strSql = "SELECT "
                    + " ZQCONO "
                    + " ,ZQBRNO "
                    + " ,ZQQRNO "
                    + " ,ZQQRNA "
                    + " ,ZQQURY "
                    + " ,ZQREMA "
                    + " ,ZQSYST "
                    + " ,ZQSTAT "
                    + " ,ZQRCST "
                    + " ,ZQCRDT "
                    + " ,ZQCRTM "
                    + " ,ZQCRUS "
                    + " ,ZQCHDT "
                    + " ,ZQCHTM "
                    + " ,ZQCHUS "
                    + " FROM ZQRT "
                    + " WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.ZQCONO))
            {
                strSql += " AND ZQCONO = '" + obj.ZQCONO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZQBRNO))
            {
                strSql += " AND ZQBRNO = '" + obj.ZQBRNO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZQQRNO))
            {
                strSql += " AND ZQQRNO = '" + obj.ZQQRNO.Trim() + "' ";
            }

            List<ZQRTDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZQRTDto> GetListPaging(out int intTotalPage, out int intTotalRecord, ZQRTDto obj, int intPageNumber, int intPageSize)
        {
            string strSql = "SELECT "
                    + " ZQCONO "
                    + " ,ZQBRNO "
                    + " ,ZQQRNO "
                    + " ,ZQQRNA "
                    + " ,ZQQURY "
                    + " ,ZQREMA "
                    + " ,ZQSYST "
                    + " ,ZQSTAT "
                    + " ,ZQRCST "
                    + " ,ZQCRDT "
                    + " ,ZQCRTM "
                    + " ,ZQCRUS "
                    + " ,ZQCHDT "
                    + " ,ZQCHTM "
                    + " ,ZQCHUS "
                    + " FROM ZQRT "
                    + " WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.ZQCONO))
            {
                strSql += " AND ZQCONO = '" + obj.ZQCONO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZQBRNO))
            {
                strSql += " AND ZQBRNO = '" + obj.ZQBRNO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZQQRNO))
            {
                strSql += " AND ZQQRNO = '" + obj.ZQQRNO.Trim() + "' ";
            }

            List<ZQRTDto> dto = this.ExecutePaging(strSql, "ZQCONO, ZQBRNO, ZQQRNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }
        public DataTable GetListQueryTools(ZQRTDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = obj.ZQQURY;

            DataTable dtt = this.ExecuteDataTablePaging(strSql, "", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);//, strFilterAS400);

            if (dtt != null && dtt.Columns.Contains("REC_NUM"))
            {
                dtt.Columns.Remove("REC_NUM");
                dtt.AcceptChanges();
            }

            return dtt;
        }


        public DataTable GetDataTable(out string strResult, string strQuery)
        {
            string strConnect = "CONNECT";

            if (strQuery.Contains(strConnect)) 
            {
                string strDb = "University";
                if (strQuery.Contains(strConnect + " " + strDb))
                {
                    strConnect += " " + strDb;
                    this.MainDataSource = DataSource.University;
                }

                strQuery = strQuery.Replace(strConnect, "");
            }

            DataTable dt = new DataTable("tblQuery");
            DataTable dtQuery = this.ExecuteDataTable(strQuery, out strResult);

            if (dtQuery != null)
            {
                dt.Merge(dtQuery);
                dt.AcceptChanges();
            }

            return dt;
        }

        public DataTable GetDataTableSP(string strSP, List<DbParameter> lstDbParam)
        {
            DataTable dt = new DataTable("tblQuery");
            DataTable dtQuery = this.ExecuteDataTableSP(strSP, lstDbParam);

            if (dtQuery != null)
            {
                dt.Merge(dtQuery);
                dt.AcceptChanges();
            }

            return dt;
        }

        #endregion


    }
}
