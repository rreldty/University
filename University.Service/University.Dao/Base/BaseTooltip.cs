using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using University.Dao.Base;
using University.Dto.Base;

namespace University.Dao.Base
{
    public class BaseTooltip : BaseDao<LookupDto>
    {
        #region Abstract Class Implementation

        public BaseTooltip()
        {
            this.MainDataSource = DataSource.University;
        }

        protected override Mapper<LookupDto> GetMapper()
        {
            Mapper<LookupDto> mapDto = new LookUpMappingDto();
            return mapDto;
        }

        #endregion

        #region Get Data

        protected DataTable GetDataTableBase(DataSource dtSource, string strSQL, string strSQLFilter, string strSQLGroup, string strDateColumn, string strFilter, string strSort)
        {
            string[] strFilterSplit = strFilter.Split(";".ToCharArray());
            string str = string.Empty;

            this.MainDataSource = dtSource;

            str = CreateQuery(strSQL, strSQLFilter, strSQLGroup, strFilterSplit[0].Trim(), strSort);

            DataTable dtt = this.ExecuteDataTable(str);

            DataTable dttLookUp = new DataTable("tblLookUp");

            if (dtt != null)
            {
                string[] strColumns = strDateColumn.Split(',');

                if (dtt.Columns.Count > 0)
                {
                    if (dtt.Columns[0].ColumnName == "REC_NUM")
                    {
                        dtt.Columns.Remove("REC_NUM");
                        dtt.AcceptChanges();
                    }

                    //Copy Column
                    for (int intCount = 0; intCount < dtt.Columns.Count; intCount++)
                    {
                        DataColumn dtc = dtt.Columns[intCount];

                        DataColumn dtcLookup = new DataColumn();
                        dtcLookup.ColumnName = dtc.ColumnName;
                        dtcLookup.AllowDBNull = dtc.AllowDBNull;
                        dtcLookup.ReadOnly = dtc.ReadOnly;
                        dtcLookup.MaxLength = dtc.MaxLength;

                        if (!IsDateColumn(strColumns, intCount))
                        {
                            dtcLookup.DataType = dtc.DataType;
                        }
                        else
                        {
                            dtcLookup.DataType = System.Type.GetType("System.String");
                        }

                        dttLookUp.Columns.Add(dtcLookup);
                    }
                    dttLookUp.AcceptChanges();
                }

                if (dtt.Rows.Count > 0)
                {
                    //Fill Rows
                    foreach (DataRow dtr in dtt.Rows)
                    {
                        DataRow dtrLookUp = dttLookUp.NewRow();

                        for (int intCount = 0; intCount < dttLookUp.Columns.Count; intCount++)
                        {
                            if (!IsDateColumn(strColumns, intCount))
                            {
                                dtrLookUp[intCount] = dtr[intCount];
                            }
                            else
                            {
                                dtrLookUp[intCount] = BaseMethod.NumericToDateString(Convert.ToDecimal(dtr[intCount]));
                            }
                        }

                        dttLookUp.Rows.Add(dtrLookUp);
                    }
                    dttLookUp.AcceptChanges();
                }
            }

            return dttLookUp;
        }

        #endregion

        #region Private Method

        private string CreateQuery(string strSQL, string strSQLFilter, string strSQLGroup, string strFilter, string strSort)
        {
            string strQuery = String.Empty;

            if (strSQL.Trim() != String.Empty)
            {
                strQuery = "SELECT " + strSQL;

                string strSortOut = String.Empty;

                if (strSQLFilter.Trim() != String.Empty)
                {
                    if (strFilter.Trim() != String.Empty)
                        strFilter += " AND " + strSQLFilter;
                    else if (!string.IsNullOrEmpty(strSQLFilter))
                        strFilter += strSQLFilter;
                }

                if (strFilter.Trim() != String.Empty)
                    strQuery += " WHERE 1=1 AND " + strFilter;

                if (strSQLGroup.Trim() != string.Empty)
                    strQuery += " GROUP BY " + strSQLGroup;

                if (strSort.Trim() != string.Empty)
                    strQuery += " ORDER BY " + strSort;
            }

            return strQuery;
        }

        private bool IsDateColumn(string[] strColumns, int intColToValid)
        {
            bool _IsDateColumn = false;

            if (strColumns.Length > 0)
            {
                for (int i = 0; i < strColumns.Length; i++)
                {
                    if (strColumns[i] != string.Empty)
                    {
                        int intColumn = Convert.ToInt32(strColumns[i]);
                        if (intColumn == intColToValid)
                        {
                            _IsDateColumn = true;
                            break;
                        }
                    }
                }
            }

            return _IsDateColumn;
        }

        #endregion
    }
}
