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
    public class BaseLookUp : BaseDao<LookupDto>
    {
        #region Abstract Class Implementation

        public BaseLookUp()
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

        protected DataTable GetDataTableBase(DataSource dtSource, string strSQL, string strSQLFilter, string strSQLGroup, string strSQLOrderBy, EntityDto obj, out int intTotalPage, out int intTotalRecord)
        {
            intTotalPage = 1;
            intTotalRecord = 0;
            string[] strFilterSplit = obj.Filter.Split(";".ToCharArray());
            string strFilterAS400 = string.Empty;

            this.MainDataSource = dtSource;

            string strSort = obj.Sort;
            string strSqlOutput = CreateQuery(strSQL, strSQLFilter, strSQLGroup, strSQLOrderBy, strFilterSplit[0].ToString().Trim(), obj.Sort, obj.SearchBys, obj.Operators, obj.SearchKeys);

            if (strSQLOrderBy.Trim() != string.Empty)
            {
                if (!string.IsNullOrEmpty(strSort))
                    strSort += ", ";

                strSort += strSQLOrderBy;
            }

            if (strFilterSplit.Length > 1)
                strFilterAS400 = CreateFilterAS400(strSQL, strFilterSplit[1].ToString().Trim());

            if (!string.IsNullOrEmpty(strSort.Trim()))
                strSqlOutput += " ORDER BY " + strSort;

            DataTable dtt = this.ExecuteDataTablePaging(strSqlOutput, strSort, obj.PageNum, obj.PageSize, out intTotalPage, out intTotalRecord, strFilterAS400);

            if (dtt != null && dtt.Columns.Contains("REC_NUM"))
            {
                dtt.Columns.Remove("REC_NUM");
                dtt.AcceptChanges();
            }

            return dtt;
        }

        protected DataTable GetDataTableBase(DataSource dtSource, string strSQL, string strSQLFilter, string strSQLGroup, string strSQLOrderBy, string strDateColumn, string strFilter, string strSort, List<string> lstSearchBy, List<string> lstOperator, List<string> lstSearchKey, int intPageNum, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            intTotalPage = 1;
            intTotalRecord = 0;
            string[] strFilterSplit = strFilter.Split(";".ToCharArray());
            string str = string.Empty, strFilterAS400 = string.Empty;

            this.MainDataSource = dtSource;

            str = CreateQuery(strSQL, strSQLFilter, strSQLGroup, strSQLOrderBy, strFilterSplit[0].ToString().Trim(), strSort, lstSearchBy, lstOperator, lstSearchKey);

            if (strSQLOrderBy.Trim() != string.Empty)
            {
                if (!string.IsNullOrEmpty(strSort))
                    strSort += ", ";

                strSort += strSQLOrderBy;
            }

            if (strFilterSplit.Length > 1)
                strFilterAS400 = CreateFilterAS400(strSQL, strFilterSplit[1].ToString().Trim());

            DataTable dtt = this.ExecuteDataTablePaging(str, strSort, intPageNum, intPageSize, out intTotalPage, out intTotalRecord, strFilterAS400);

            DataTable dttLookUp = new DataTable("tblLookUp");

            if (dtt != null && dtt.Columns.Count > 0)
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
                else
                {
                    DataRow dtrLookUp = dttLookUp.NewRow();

                    for (int intCount = 0; intCount < dttLookUp.Columns.Count; intCount++)
                    {
                        if (!IsDateColumn(strColumns, intCount))
                        {
                            if (dttLookUp.Columns[intCount].DataType == System.Type.GetType("System.String"))
                            {
                                dtrLookUp[intCount] = string.Empty;
                            }
                            else
                            {
                                dtrLookUp[intCount] = 0;
                            }
                        }
                        else
                        {
                            dtrLookUp[intCount] = string.Empty;
                        }
                    }

                    dttLookUp.Rows.Add(dtrLookUp);
                    dttLookUp.AcceptChanges();
                }
            }
            else
            {
                dttLookUp = null;
            }

            return dttLookUp;
        }

        protected DataTable GetSingleDataBase(DataSource dtSource, string strSQL, string strSQLFilter, string strFilter, string strKeyCode, bool isDscr)
        {
            this.MainDataSource = dtSource;

            string[] strFilterSplit = strFilter.Split(";".ToCharArray());

            string strSqlOutput = CreateQueryOne(strSQL, strSQLFilter, strFilterSplit[0].Trim(), strKeyCode, isDscr);

            DataTable dtt = this.ExecuteDataTable(strSqlOutput);

            if (dtt != null && dtt.Columns.Contains("REC_NUM"))
            {
                dtt.Columns.Remove("REC_NUM");
                dtt.AcceptChanges();
            }

            return dtt;
        }

        protected string GetDescriptionBase(DataSource dtSource, string strSQL, string strSQLFilter, string strFilter, string strKeyCode, int idxDscrColumn)
        {
            string[] strFilterSplit = strFilter.Split(";".ToCharArray());
            string str = string.Empty, strDscr = string.Empty;

            this.MainDataSource = dtSource;

            str = CreateQueryOne(strSQL, strSQLFilter, strFilterSplit[0].Trim(), strKeyCode, true);

            DataTable dtt = this.ExecuteDataTable(str);

            if (dtt != null)
            {
                if (dtt.Rows.Count == 1)
                {
                    strDscr = dtt.Rows[0][idxDscrColumn].ToString();
                }
            }

            return strDscr;
        }

        protected bool IsValidBase(DataSource dtSource, string strSQL, string strSQLFilter, string strFilter, string strKeyCode)
        {
            string[] strFilterSplit = strFilter.Split(";".ToCharArray());
            string str = string.Empty;
            bool _IsValid = false;

            this.MainDataSource = dtSource;

            str = CreateQueryOne(strSQL, strSQLFilter, strFilterSplit[0].Trim(), strKeyCode, false);

            DataTable dtt = this.ExecuteDataTable(str);

            if (dtt.Rows.Count == 0)
            {
                _IsValid = false;
            }
            else
            {
                _IsValid = true;
            }

            return _IsValid;
        }

        #endregion

        #region Private Method

        string CreateQueryOne(string strSQL, string strSQLFilter, string strFilter, string strKeyCode, bool _isDscr)
        {
            string strQuery = string.Empty, strField = string.Empty;

            strSQL = strSQL.ToUpper();

            //Get First Column
            string strPattern1 = @"\w*\w{6} (as|AS) \[";
            string strPattern2 = @"\w*\D*RCST = \w{1} AND";   // Record Status 1 digit
            string strPattern3 = @"AND \w*\D*RCST = \w{1}";   // Record Status 1 digit
            string strPattern4 = @"\w*\D*RCST = \w{1}";       // Record Status 1 digit
            string strPattern5 = @"\w*\w{3}";
            string strPattern6 = @"\w*\D*RCST = \w{2} AND";   // Record Status 2 digit
            string strPattern7 = @"AND \w*\D*RCST = \w{2}";   // Record Status 2 digit
            string strPattern8 = @"\w*\D*RCST = \w{2}";       // Record Status 2 digit

            //string[] strFields;

            string strSqlTemp1 = strSQL.Replace("DISTINCT", "").Trim();

            MatchCollection matchColl = Regex.Matches(strSqlTemp1, strPattern5);
            Console.WriteLine(matchColl[0]);
            strField = matchColl[0].Value.Trim();

            //if (Regex.Match(strSQL, strPattern1).Success)
            //{
            //    strFields = Regex.Match(strSQL, strPattern1).Value.ToUpper().Trim().Replace("DISTINCT", "").Replace(",", "").Split(new string[] { "AS" }, StringSplitOptions.None);
            //    strField = strFields[0].Trim();
            //}
            //else
            //{
            //    if (Regex.Match(strSQL, strPattern5).Success)
            //    {
            //        strFields = Regex.Match(strSQL, strPattern5).Value.Replace("DISTINCT", "").Trim().Split(new string[] { "," }, StringSplitOptions.None);
            //        strField = strFields[0].Trim();
            //    }
            //}

            //Temporary Solution && !strSQL.ToUpper().Contains("SCCO")
            if (_isDscr && !strSQL.ToUpper().Contains("SCCO"))
            {
                //strPattern6
                if (Regex.Match(strSQLFilter, strPattern6).Success)
                {
                    strSQLFilter = Regex.Replace(strSQLFilter, strPattern6, "");
                }

                if (Regex.Match(strFilter, strPattern6).Success)
                {
                    strFilter = Regex.Replace(strFilter, strPattern6, "");
                }

                //strPattern7
                if (Regex.Match(strSQLFilter, strPattern7).Success)
                {
                    strSQLFilter = Regex.Replace(strSQLFilter, strPattern7, "");
                }

                if (Regex.Match(strFilter, strPattern7).Success)
                {
                    strFilter = Regex.Replace(strFilter, strPattern7, "");
                }

                //strPattern8
                if (Regex.Match(strSQLFilter, strPattern8).Success)
                {
                    strSQLFilter = Regex.Replace(strSQLFilter, strPattern8, "");
                }

                if (Regex.Match(strFilter, strPattern8).Success)
                {
                    strFilter = Regex.Replace(strFilter, strPattern8, "");
                }

                //strPattern2
                if (Regex.Match(strSQLFilter, strPattern2).Success)
                {
                    strSQLFilter = Regex.Replace(strSQLFilter, strPattern2, "");
                }

                if (Regex.Match(strFilter, strPattern2).Success)
                {
                    strFilter = Regex.Replace(strFilter, strPattern2, "");
                }

                //strPattern3
                if (Regex.Match(strSQLFilter, strPattern3).Success)
                {
                    strSQLFilter = Regex.Replace(strSQLFilter, strPattern3, "");
                }

                if (Regex.Match(strFilter, strPattern3).Success)
                {
                    strFilter = Regex.Replace(strFilter, strPattern3, "");
                }

                //strPattern4
                if (Regex.Match(strSQLFilter, strPattern4).Success)
                {
                    strSQLFilter = Regex.Replace(strSQLFilter, strPattern4, "");
                }

                if (Regex.Match(strFilter, strPattern4).Success)
                {
                    strFilter = Regex.Replace(strFilter, strPattern4, "");
                }
            }

            strSQLFilter = strSQLFilter.Trim();
            strFilter = strFilter.Trim();

            if (_isDscr)
            {
                if (string.IsNullOrEmpty(strSQLFilter))
                {
                    strSQLFilter += strField + " = '" + strKeyCode + "' ";
                }
                else
                {
                    strSQLFilter += " AND " + strField + " = '" + strKeyCode + "' ";
                }
            }

            if (strSQL.Trim() != String.Empty)
            {
                strQuery = "SELECT TOP 1 " + strSQL.Trim();
                strQuery = strQuery.Replace("TOP 1 DISTINCT", "DISTINCT TOP 1");

                if (strSQLFilter.Trim() != String.Empty)
                {
                    strQuery += " WHERE 1=1 AND " + strSQLFilter;

                    if (strFilter.Trim() != String.Empty)
                    {
                        strQuery += " AND " + strFilter;
                    }
                }
                else
                {
                    if (strFilter.Trim() != String.Empty)
                    {
                        strQuery += " WHERE 1=1 AND " + strFilter;
                    }
                }
            }

            return strQuery;
        }

        private string CreateQuery(string strSQL, string strSQLFilter, string strSQLGroup, string strSQLOrderBy, string strFilter, string strSort, List<string> lstSearchBy, List<string> lstOperator, List<string> lstSearchKey)
        {
            string strQuery = String.Empty;

            if (strSQL.Trim() != String.Empty)
            {
                strQuery = "SELECT " + strSQL;

                string strSortOut = String.Empty;
                string strSearch = CreateSearch(strSQL, lstSearchBy, lstOperator, lstSearchKey);

                if (strSQLFilter.Trim() != String.Empty)
                {
                    if (strFilter.Trim() != String.Empty)
                        strFilter += " AND " + strSQLFilter;
                    else if (!string.IsNullOrEmpty(strSQLFilter))
                        strFilter += strSQLFilter;
                }

                if (strSearch.Trim() != String.Empty)
                {
                    if (strFilter.Trim() != String.Empty)
                        strFilter += " AND " + strSearch;
                    else
                        strFilter += strSearch;
                }

                if (strFilter.Trim() != String.Empty)
                    strQuery += " WHERE 1=1 AND " + strFilter;

                if (strSQLGroup.Trim() != string.Empty)
                    strQuery += " GROUP BY " + strSQLGroup;
            }

            return strQuery;
        }

        private string CreateSearch(string strSQL, List<string> lstSearchBy, List<string> lstOperator, List<string> lstSearchKey)
        {
            string str = String.Empty;

            strSQL = strSQL.ToUpper();

            if (lstSearchBy != null && lstSearchBy.Count > 0)
            {
                for (int i = 0; i < lstSearchBy.Count; i++)
                {
                    string strPattern1 = "\\w*\\.\\w{6} (as|AS) " + lstSearchBy[i].ToUpper().Trim();
                    string strPattern2 = "\\w{6} (as|AS) " + lstSearchBy[i].ToUpper().Trim();
                    string strPattern3 = "\\w*\\.\\w{6} (as|AS) \\[" + lstSearchBy[i].ToUpper().Trim() + "\\]";
                    string strPattern4 = "\\w{6} (as|AS) \\[" + lstSearchBy[i].ToUpper().Trim() + "\\]";

                    if (Regex.Match(strSQL, strPattern1).Success)
                    {
                        string strMatch = Regex.Match(strSQL, strPattern1).Value.Trim().ToUpper();
                        int intIdxAs = strMatch.IndexOf("AS");

                        string strField = strMatch.Substring(0, intIdxAs).Trim();
                        lstSearchBy[i] = strField;
                    }
                    else if (Regex.Match(strSQL, strPattern3).Success)
                    {
                        string strMatch = Regex.Match(strSQL, strPattern3).Value.Trim().ToUpper();
                        int intIdxAs = strMatch.IndexOf("AS");

                        string strField = strMatch.Substring(0, intIdxAs).Trim();
                        lstSearchBy[i] = strField;
                    }
                    else if (Regex.Match(strSQL, strPattern2).Success)
                    {
                        string strField = Regex.Match(strSQL, strPattern2).Value.Trim().Substring(0, 6);
                        lstSearchBy[i] = strField;
                    }
                    else if (Regex.Match(strSQL, strPattern4).Success)
                    {
                        string strField = Regex.Match(strSQL, strPattern4).Value.Trim().Substring(0, 6);
                        lstSearchBy[i] = strField;
                    }
                }

                if (lstSearchKey.Count > 0 && lstSearchKey != null)
                {
                    for (int i = 0; i < lstSearchKey.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(lstSearchKey[i]))
                        {
                            if (lstOperator[i] == "%")
                            {
                                str += " UPPER(" + lstSearchBy[i] + ") LIKE '%" + lstSearchKey[i] + "%' AND ";
                            }
                            else
                            {
                                str += " UPPER(" + lstSearchBy[i] + ") = '" + lstSearchKey[i] + "' AND ";
                            }
                        }
                    }

                    if (str.Length > 0 && str != String.Empty)
                    {
                        if (str.Substring((str.Length - 4), 3) == "AND")
                        {
                            str = str.Substring(0, (str.Length - 4)) + " ";
                        }
                    }
                }
            }

            return str;
        }

        private string CreateFilterAS400(string strSQL, string strFilterBy)
        {
            string str = String.Empty;
            string[] strTemp = strSQL.Substring(0, strSQL.IndexOf("FROM")).Split(',');

            if (!string.IsNullOrEmpty(strFilterBy))
            {
                string[] strFilter = strFilterBy.Replace("AND", "").Split(' ');
                bool bolFilter = true;

                if (strFilter.Length > 0)
                {
                    foreach (string strFilterField in strFilter)
                    {
                        if (strFilterField.Trim().Length > 0)
                        {
                            foreach (string strTempField in strTemp)
                            {
                                if (strTempField.Contains("AS"))
                                {
                                    if (strFilterField.ToUpper().Contains(strTempField.Substring(strTempField.IndexOf("AS") + 3).ToUpper()))
                                    {
                                        if (str.Length > 0)
                                            str += " ";

                                        str += strTempField.Substring(0, strTempField.IndexOf("AS"));
                                        bolFilter = true;
                                    }
                                }
                            }

                            if (!bolFilter)
                            {
                                if (str.Length > 0)
                                    str += " ";

                                str += strFilterField;
                            }

                            bolFilter = false;
                        }
                    }
                }
            }

            return str;
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
