using System;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

using University.Dto.Base;
using University.Dao.Base;

namespace University.Dao.Base
{
    public class BaseDW : BaseDao<DWDto>
    {
        #region Constructor
        public BaseDW()
        {
            this.MainDataSource = DataSource.University;
        }
        #endregion

        #region Abstract Class Implementation
        protected override Mapper<DWDto> GetMapper()
        {
            Mapper<DWDto> mapDto = new DWMappingDto();
            return mapDto;
        }
        #endregion

        #region Base Methods
        protected DataTable GetDataBase(out int intTotalPage, out int intTotalRecord, OutputType _OutType, List<DWDto> lstParam, int intPageNumber, int intPageSize, string strOrderBy, QuerySource objQSrc, string strReportQuery, DWColumns _columnsDW, int[] RemoveColumn)
        {
            DataTable dtt = GetDataTableBase(out intTotalPage, out intTotalRecord, _OutType, lstParam, intPageNumber, intPageSize, strOrderBy, objQSrc, strReportQuery);

            DataTable dttBI = new DataTable("tblBI");

            if (dtt != null && dtt.Columns.Count > 0)
            {
                //Create Columns
                if (dtt.Columns.Count > 0)
                {
                    //Remove Column
                    if (RemoveColumn != null && RemoveColumn.Length > 0)
                    {
                        foreach (int iCol in RemoveColumn)
                        {
                            dtt.Columns.RemoveAt(iCol);
                        }
                        dtt.AcceptChanges();
                    }

                    if (dtt.Columns.Contains("REC_NUM"))
                    {
                        dtt.Columns.Remove("REC_NUM");
                        dtt.AcceptChanges();
                    }

                    //Copy Column
                    for (int n = 0; n < dtt.Columns.Count; n++)
                    {
                        DataColumn dtc = dtt.Columns[n];

                        DataColumn dtcBI = new DataColumn();
                        dtcBI.ColumnName = dtc.ColumnName;
                        dtcBI.AllowDBNull = dtc.AllowDBNull;
                        dtcBI.ReadOnly = dtc.ReadOnly;
                        dtcBI.MaxLength = dtc.MaxLength;

                        if (_columnsDW != null)
                        {
                            if (_columnsDW.DateColumn != null && _columnsDW.DateColumn.Contains(n))
                            {
                                dtcBI.DataType = System.Type.GetType("System.String");
                            }
                            else if (_columnsDW.TimeColumn != null && _columnsDW.TimeColumn.Contains(n))
                            {
                                dtcBI.DataType = System.Type.GetType("System.String");
                            }
                            else if (_columnsDW.DateTimeColumn != null && _columnsDW.DateTimeColumn.Contains(n))
                            {
                                dtcBI.DataType = System.Type.GetType("System.String");
                            }
                            else
                            {
                                dtcBI.DataType = dtc.DataType;
                            }
                        }
                        else if (dtc.ColumnName.Contains("%"))
                        {
                            dtcBI.DataType = System.Type.GetType("System.String");
                        }
                        else
                        {
                            dtcBI.DataType = dtc.DataType;
                        }

                        dttBI.Columns.Add(dtcBI);
                    }
                    dttBI.AcceptChanges();
                }

                //Fill Rows
                if (dtt.Rows.Count > 0)
                {
                    foreach (DataRow dtr in dtt.Rows)
                    {
                        DataRow dtrBI = dttBI.NewRow();

                        for (int n = 0; n < dttBI.Columns.Count; n++)
                        {
                            DataColumn dtc = dttBI.Columns[n];

                            if (_columnsDW != null)
                            {
                                if (_columnsDW.DateColumn != null && _columnsDW.DateColumn.Contains(n))
                                {
                                    dtrBI[n] = BaseMethod.NumericToDateString((dtr[n] != DBNull.Value ? Convert.ToDecimal(dtr[n]) : 0));
                                }
                                else if (_columnsDW.TimeColumn != null && _columnsDW.TimeColumn.Contains(n))
                                {
                                    dtrBI[n] = BaseMethod.NumericToTimeString((dtr[n] != DBNull.Value ? Convert.ToDecimal(dtr[n]) : 0));
                                }
                                else if (_columnsDW.DateTimeColumn != null && _columnsDW.DateTimeColumn.Contains(n))
                                {
                                    dtrBI[n] = BaseMethod.NumericToDateTimeString((dtr[n] != DBNull.Value ? Convert.ToDecimal(dtr[n]) : 0));
                                }
                                else
                                {
                                    dtrBI[n] = (dtr[n] != DBNull.Value ? dtr[n].ToString() : string.Empty);
                                }
                            }
                            else if (dtc.ColumnName.Contains("N:2"))
                            {
                                dtrBI[n] = (dtr[n] != DBNull.Value ? String.Format("{0:#,##0.00;-#,##0.00}", dtr[n]) : String.Format("{0:#,##0.00;-#,##0.00}", 0));
                            }
                            else if (dtc.ColumnName.Contains("%"))
                            {
                                dtrBI[n] = (dtr[n] != DBNull.Value ? String.Format("{0:#,##0.00%;-#,##0.00%}", dtr[n]) : String.Format("{0:#,##0.00%;-#,##0.00%}", 0));
                            }
                            else
                            {
                                if (dtc.DataType == Type.GetType("System.String"))
                                {
                                    dtrBI[n] = (dtr[n] != DBNull.Value ? dtr[n] : string.Empty);
                                }
                                else
                                {
                                    dtrBI[n] = (dtr[n] != DBNull.Value ? dtr[n] : 0);
                                }
                            }
                        }

                        dttBI.Rows.Add(dtrBI);
                    }
                    dttBI.AcceptChanges();
                }
                //else
                //{
                //    DataRow dtrBI = dttBI.NewRow();

                //    for (int n = 0; n < dttBI.Columns.Count; n++)
                //    {
                //        if (dttBI.Columns[n].DataType == System.Type.GetType("System.String"))
                //        {
                //            dtrBI[n] = string.Empty;
                //        }
                //        else
                //        {
                //            dtrBI[n] = 0;
                //        }
                //    }

                //    dttBI.Rows.Add(dtrBI);
                //    dttBI.AcceptChanges();
                //}
            }
            else
            {
                dttBI = null;
            }

            return dttBI;
        }

        DataTable GetDataTableBase(out int intTotalPage, out int intTotalRecord, OutputType _OutType, List<DWDto> lstParam, int intPageNumber, int intPageSize, string strOrderBy, QuerySource objQSrc, string strReportQuery)
        {
            intTotalRecord = 0;
            intTotalPage = 0;
            DataTable dttBI = new DataTable("TableBI");

            switch (objQSrc)
            {
                case QuerySource.Embedded:
                    {
                        string strQuery = string.Empty;
                        Assembly assem = this.GetType().Assembly;

                        using (Stream _stream = assem.GetManifestResourceStream("University.Portal.Dao.SQLScript." + strReportQuery + ".sql"))
                        {
                            using (StreamReader sr = new StreamReader(_stream))
                            {
                                strQuery = sr.ReadToEnd();
                            }
                        }

                        foreach (DWDto objParam in lstParam)
                        {
                            if (objParam.ParamType == DbType.String)
                            {
                                strQuery = strQuery.Replace(objParam.Param, (!string.IsNullOrEmpty(objParam.Value) && !objParam.Value.Equals("undefined") ? objParam.Value.Replace("'", "''") : ""));
                            }
                            else
                            {
                                strQuery = strQuery.Replace(objParam.Param, (!string.IsNullOrEmpty(objParam.Value) && !objParam.Value.Equals("undefined") ? objParam.Value : "0"));
                            }
                        }

                        dttBI.Merge(this.ExecuteDataTable(strQuery));


                        break;
                    }
                case QuerySource.StoredProcedure:
                    {
                        List<DbParameter> lstDbParam = new List<DbParameter>();

                        foreach (DWDto objParam in lstParam)
                        {
                            if (objParam.ParamType == DbType.String)
                            {
                                lstDbParam.Add(AddInputParameter(objParam.Param.Replace("#", "@"), (!string.IsNullOrEmpty(objParam.Value) && !objParam.Value.Equals("undefined") ? objParam.Value : ""), objParam.ParamType));
                            }
                            else
                            {
                                lstDbParam.Add(AddInputParameter(objParam.Param.Replace("#", "@"), (!string.IsNullOrEmpty(objParam.Value) && !objParam.Value.Equals("undefined") ? objParam.Value : "0"), objParam.ParamType));
                            }
                        }

                        switch (_OutType)
                        {
                            case OutputType.Grid:
                                {
                                    lstDbParam.Add(AddInputParameter("@GetColumn", 0, System.Data.DbType.Int32));
                                    lstDbParam.Add(AddInputParameter("@UseChart", 0, System.Data.DbType.Int32));
                                    lstDbParam.Add(AddInputParameter("@UsePaging", 1, System.Data.DbType.Int32));
                                    break;
                                }
                            case OutputType.ColumnHeader:
                                {
                                    lstDbParam.Add(AddInputParameter("@GetColumn", 1, System.Data.DbType.Int32));
                                    lstDbParam.Add(AddInputParameter("@UseChart", 0, System.Data.DbType.Int32));
                                    lstDbParam.Add(AddInputParameter("@UsePaging", 1, System.Data.DbType.Int32));
                                    break;
                                }
                            case OutputType.Chart:
                                {
                                    lstDbParam.Add(AddInputParameter("@GetColumn", 0, System.Data.DbType.Int32));
                                    lstDbParam.Add(AddInputParameter("@UseChart", 1, System.Data.DbType.Int32));
                                    lstDbParam.Add(AddInputParameter("@UsePaging", 0, System.Data.DbType.Int32));
                                    break;
                                }
                            case OutputType.Excel:
                                {
                                    lstDbParam.Add(AddInputParameter("@GetColumn", 0, System.Data.DbType.Int32));
                                    lstDbParam.Add(AddInputParameter("@UseChart", 0, System.Data.DbType.Int32));
                                    lstDbParam.Add(AddInputParameter("@UsePaging", 0, System.Data.DbType.Int32));
                                    break;
                                }
                            default:
                                break;
                        }

                        lstDbParam.Add(AddInputParameter("@intPageNumber", intPageNumber, System.Data.DbType.Int32));
                        lstDbParam.Add(AddInputParameter("@intPageSize", intPageSize, System.Data.DbType.Int32));
                        lstDbParam.Add(AddOutputParameter("@intTotalPage", intTotalPage, System.Data.DbType.Int32));
                        lstDbParam.Add(AddOutputParameter("@intTotalRecord", intTotalRecord, System.Data.DbType.Int32));

                        //ADD NEW CONDITION FOR SORTING KEYFIGURE
                        if (!string.IsNullOrEmpty(strOrderBy.Trim()))
                        {
                            if (strOrderBy.Contains("asc"))
                            {
                                string a = strOrderBy.Trim().Replace("asc", "");
                                strOrderBy = a.Replace(a, "[" + a + "] ASC");
                            }
                            else if (strOrderBy.Contains("desc"))
                            {
                                string a = strOrderBy.Trim().Replace("desc", "");
                                strOrderBy = a.Replace(a, "[" + a + "] DESC");
                            }
                        }
                        //END

                        lstDbParam.Add(AddInputParameter("@strOrderBy", strOrderBy, System.Data.DbType.String));

                        dttBI.Merge(this.ExecuteDataTableSP(strReportQuery, lstDbParam));

                        intTotalPage = Convert.ToInt32(lstDbParam.Find(s => s.ParameterName == "@intTotalPage").Value);
                        intTotalRecord = Convert.ToInt32(lstDbParam.Find(s => s.ParameterName == "@intTotalRecord").Value);

                        break;
                    }
                default:
                    break;
            }

            return dttBI;
        }
        #endregion
    }
}
