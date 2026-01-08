using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using University.Dto.Base;
using System.IO;
using System.Linq;

namespace University.Dao.Base
{
    public class QueryDao : BaseDao<LookupDto>
    {
        #region "Abstract Class Implementation"

        public QueryDao()
        {
            this.MainDataSource = DataSource.University;
        }

        protected override Mapper<LookupDto> GetMapper()
        {
            Mapper<LookupDto> mapDto = new LookUpMappingDto();
            return mapDto;
        }

        #endregion

        #region "Public Method"
        public DataTable ExecuteSPDataTable(out List<ParamDto> lstParamOut, List<ParamDto> lstParam, string strSP)
        {
            lstParamOut = new List<ParamDto>();

            List<DbParameter> lstDBParams = new List<DbParameter>();
            if (lstParam != null && !string.IsNullOrEmpty(strSP.Trim()))
            {
                foreach (ParamDto objParam in lstParam)
                {
                    DbType typeDb = (DbType)Enum.Parse(typeof(DbType), "String");

                    if (!string.IsNullOrEmpty(objParam.Type))
                    {
                        typeDb = (DbType)Enum.Parse(typeof(DbType), objParam.Type);
                    }

                    if (objParam.Direction.ToLower() == "out")
                    {
                        lstDBParams.Add(AddOutputParameter(objParam.Param, objParam.Value, typeDb));
                    }
                    else
                    {
                        lstDBParams.Add(AddInputParameter(objParam.Param, objParam.Value, typeDb));
                    }
                }
            }

            DataTable dtQuery = this.ExecuteDataTableSP(strSP, lstDBParams);

            lstParamOut = lstParam.Where(w => w.Direction.ToLower() == "out").ToList();
            if (lstParamOut != null && lstParamOut.Count > 0)
            {
                foreach (ParamDto objParam in lstParamOut)
                {
                    DbParameter dbParam = lstDBParams.Find(s => s.ParameterName == objParam.Param);
                    objParam.Value = dbParam.Value.ToString();
                }
            }

            return dtQuery;
        }

        public DataTable ExecuteSPDataTablePaging(out int intTotalPage, out int intTotalRecord, List<ParamDto> lstParam, EntityDto obj)
        {
            intTotalPage = 0;
            intTotalRecord = 0;

            if (!lstParam.Exists(s => s.Param.Contains("intTotalPage")))
            {
                ParamDto objParam = new ParamDto();
                objParam.Param = "@intTotalPage";
                objParam.Value = "0";
                objParam.Type = "Int32";
                objParam.Direction = "out";

                lstParam.Add(objParam);
            }

            if (!lstParam.Exists(s => s.Param.Contains("intTotalRecord")))
            {
                ParamDto objParam = new ParamDto();
                objParam.Param = "@intTotalRecord";
                objParam.Value = "0";
                objParam.Type = "Int32";
                objParam.Direction = "out";

                lstParam.Add(objParam);
            }

            if (!lstParam.Exists(s => s.Param.Contains("intPageNumber")))
            {
                ParamDto objParam = new ParamDto();
                objParam.Param = "@intPageNumber";
                objParam.Value = obj.PageNum.ToString();
                objParam.Type = "Int32";
                objParam.Direction = "in";

                lstParam.Add(objParam);
            }

            if (!lstParam.Exists(s => s.Param.Contains("intPageSize")))
            {
                ParamDto objParam = new ParamDto();
                objParam.Param = "@intPageSize";
                objParam.Value = obj.PageSize.ToString();
                objParam.Type = "Int32";
                objParam.Direction = "in";

                lstParam.Add(objParam);
            }

            List<ParamDto> lstParamOut = new List<ParamDto>();
            DataTable dtQuery = ExecuteSPDataTable(out lstParamOut, lstParam, obj.Entity);

            if (dtQuery != null)
            {
                ParamDto objP1 = lstParamOut.Find(s => s.Param == "@intTotalPage");

                if (objP1 != null)
                {
                    intTotalPage = Convert.ToInt32(objP1.Value);
                }

                ParamDto objP2 = lstParamOut.Find(s => s.Param == "@intTotalRecord");

                if (objP2 != null)
                {
                    intTotalRecord = Convert.ToInt32(objP2.Value);
                }

                if (dtQuery != null && dtQuery.Columns.Contains("REC_NUM"))
                {
                    dtQuery.Columns.Remove("REC_NUM");
                    dtQuery.AcceptChanges();
                }

            }

            return dtQuery;
        }

        public DataTable ExecuteSPDataTableDownload(List<ParamDto> lstParam, string strSP)
        {
            if (!lstParam.Exists(s => s.Param.Contains("intTotalPage")))
            {
                ParamDto objParam = new ParamDto();
                objParam.Param = "@intTotalPage";
                objParam.Value = "0";
                objParam.Type = "Int32";
                objParam.Direction = "out";

                lstParam.Add(objParam);
            }

            if (!lstParam.Exists(s => s.Param.Contains("intTotalRecord")))
            {
                ParamDto objParam = new ParamDto();
                objParam.Param = "@intTotalRecord";
                objParam.Value = "0";
                objParam.Type = "Int32";
                objParam.Direction = "out";

                lstParam.Add(objParam);
            }

            if (!lstParam.Exists(s => s.Param.Contains("Download")))
            {
                ParamDto objParam = new ParamDto();
                objParam.Param = "@Download";
                objParam.Value = "1";
                objParam.Type = "Decimal";
                objParam.Direction = "in";

                lstParam.Add(objParam);
            }


            List<ParamDto> lstParamOut = new List<ParamDto>();
            DataTable dtQuery = ExecuteSPDataTable(out lstParamOut, lstParam, strSP);

            return dtQuery;
        }

        public DataTable ExecutePathDataTable(List<ParamDto> lstParam, string strPath)
        {
            string strResult = string.Empty;

            string strQuery = string.Empty;

            using (StreamReader sr = new StreamReader(strPath))
            {
                strQuery = sr.ReadToEnd();
                sr.Close();
            }

            if (lstParam != null && !string.IsNullOrEmpty(strQuery.Trim()))
            {
                foreach (ParamDto objParam in lstParam)
                {
                    strQuery = strQuery.Replace(objParam.Param, objParam.Value);
                }
            }

            return this.ExecuteDataTable(strQuery);
        }
        public object ExecuteScalar(string strScript)
        {
            return this.ExecuteDbScalar(strScript);
        }

        public new string ExecuteQuery(string strQuery)
        {
            return ExecuteDbNonQuery(strQuery);
        }

        public string ExecuteDbNonQuery(int intDataSource, string strQuery)
        {
            switch (intDataSource)
            {
                case 0:
                    this.MainDataSource = DataSource.University;
                    break;
                case 1:
                    this.MainDataSource = DataSource.University;
                    break;
                default:
                    {
                        this.MainDataSource = DataSource.University;
                        break;
                    }
            }

            string strResult = this.ExecuteDbNonQuery(strQuery);

            return strResult;
        }

        public DataSet GetDataSet(out string strResult, string strQuery)
        {
            this.MainDataSource = DataSource.University;
            string strConnect = "CONNECT";

            if (strQuery.Contains(strConnect))
            {
                string strDb = "AGLIS";
                if (strQuery.Contains(strConnect + " " + strDb))
                {
                    strConnect += " " + strDb;
                    this.MainDataSource = DataSource.University;
                }
                else
                {
                    strDb = "SYNU";
                    if (strQuery.Contains(strConnect + " " + strDb))
                    {
                        strConnect += " " + strDb;
                        this.MainDataSource = DataSource.SYNU;
                    }
                    else
                    {
                        strDb = "SYND";
                        if (strQuery.Contains(strConnect + " " + strDb))
                        {
                            strConnect += " " + strDb;
                            this.MainDataSource = DataSource.SYND;
                        }
                    }
                }

                strQuery = strQuery.Replace(strConnect, "");
            }

            DataSet ds = new DataSet("dtsQuery");
            DataSet dsQuery = this.ExecuteDataSet(strQuery, out strResult);

            if (dsQuery != null)
            {
                ds.Merge(dsQuery);
                ds.AcceptChanges();
            }

            return ds;
        }

        public DataTable GetDataTable(out string strResult, int intDataSource, string strQuery)
        {
            switch (intDataSource)
            {
                case 0:
                    this.MainDataSource = DataSource.University;
                    break;
                default:
                    {
                        this.MainDataSource = DataSource.University;
                        break;
                    }
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

        public DataTable GetDataTableSP(int intDataSource, string strSP, List<DbParameter> lstDbParam)
        {
            switch (intDataSource)
            {
                case 0:
                    this.MainDataSource = DataSource.University;
                    break;
                case 1:
                    this.MainDataSource = DataSource.University;
                    break;
                default:
                    {
                        this.MainDataSource = DataSource.University;
                        break;
                    }
            }

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
