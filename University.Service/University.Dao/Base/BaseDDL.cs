using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dao.Base;
using University.Dto.Base;
using System.Data;

namespace University.Dao.Base
{
    public class BaseDDL : BaseDao<DDLDto>
    {
        #region Abstract Class Implementation

        public BaseDDL()
        {
            this.MainDataSource = DataSource.University;
        }

        protected override Mapper<DDLDto> GetMapper()
        {
            Mapper<DDLDto> mapDto = new DDLMappingDto();
            return mapDto;
        }

        #endregion

        #region Get Data
        protected List<DDLDto> GetListBase(DataSource dtSource, string strSQL, string strSQLFilter, string strOrderBy, string strOrderDirection, string strFilter, string strKeyCode, string strFilterMsc)
        {
            List<DDLDto> lst = new List<DDLDto>();

            string str = String.Empty;

            this.MainDataSource = dtSource;

            if (dtSource == DataSource.SSAS)
            {
                str = CreateQuerySSAS(strSQL, strSQLFilter, strOrderBy, strOrderDirection, strFilter, strKeyCode);

                AnalysisServiceDao _analysisServiceDao = new AnalysisServiceDao();
                DataTable dtDax = _analysisServiceDao.ExecuteDataTable(str);

                for (int n = 0; n < dtDax.Columns.Count; n++)
                {
                    dtDax.Columns[n].ColumnName = dtDax.Columns[n].ColumnName.Replace("[", "").Replace("]", "");
                }
                dtDax.AcceptChanges();

                ObjectFactory<DDLDto> _objFactory = new ObjectFactory<DDLDto>();
                lst = _objFactory.ConvertToList(dtDax);
            }
            else
            {
                str = CreateQuery(strSQL, strSQLFilter, strOrderBy, strOrderDirection, strFilter, strKeyCode);
                lst = this.ExecuteQuery(str);
            }


            return lst;
        }

        protected List<DDLDto> GetListBase(DataSource dtSource, string strSQL, string strSQLFilter, string strOrderBy, string strOrderDirection, string strFilter, string strKeyCode)
        {
            List<DDLDto> lst = new List<DDLDto>();

            string str = String.Empty;

            this.MainDataSource = dtSource;

            str = CreateQuery(strSQL, strSQLFilter, strOrderBy, strOrderDirection, strFilter, strKeyCode);

            lst = this.ExecuteQuery(str);

            return lst;
        }

        #endregion

        #region Private Methods
        private string CreateQuery(string strSQL, string strSQLFilter, string strOrderBy, string strOrderDirection, string strFilter, string strKeyCode)
        {
            string strQuery = string.Empty;

            if (strSQL.Trim() != String.Empty)
            {
                strQuery = "SELECT " + strSQL;

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

                if (strKeyCode.Trim() != String.Empty)
                {
                    strQuery = "SELECT CODE, DSCR FROM (" + strQuery + ") AS DDL WHERE 1=1 AND CODE = '" + strKeyCode.Trim() + "'";
                }
                else
                {
                    if (strOrderBy.ToUpper().Trim() == "C" || strOrderBy.ToUpper().Trim() == "D")
                    {
                        strQuery = "SELECT CODE, DSCR FROM (" + strQuery + ") DDL ";
                    }

                    switch (strOrderBy.ToUpper().Trim())
                    {
                        case "C":
                            strQuery += " ORDER BY CODE " + strOrderDirection;
                            break;

                        case "D":
                            strQuery += " ORDER BY DSCR " + strOrderDirection;
                            break;

                        default:
                            strQuery += " ORDER BY " + strOrderBy.ToUpper() + " " + strOrderDirection;
                            break;
                    }
                }
            }

            return strQuery;
        }

        private string CreateQuerySSAS(string strSQL, string strSQLFilter, string strOrderBy, string strOrderDirection, string strFilter, string strKeyCode)
        {
            string strQuery = string.Empty;
            string strQueryFilter = string.Empty;

            if (strSQL.Trim() != String.Empty)
            {
                strQuery = strSQL;
                strQueryFilter = strSQLFilter;

                if (!string.IsNullOrEmpty(strQueryFilter))
                {
                    strQueryFilter += " " + strFilter;
                }

                strQuery = strQuery.Replace("#FILTER", strQueryFilter);


                if (strKeyCode.Trim() != String.Empty)
                {
                    //NEXT FEATURE
                }
                else
                {
                    strQuery += " ORDER BY " + strOrderBy.ToUpper();
                }
            }

            return strQuery;
        }
        #endregion
    }
}
