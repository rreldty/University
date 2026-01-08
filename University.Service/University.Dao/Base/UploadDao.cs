using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using University.Dao.Base;
//using University.Dto.General;
using University.Dto.Base;

namespace University.Dao.Base
{
    public class UploadDao : BaseDao<Object>
    {
        #region Constructor

        public UploadDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<Object> GetMapper()
        {
            return null;
        }

        #endregion

        #region Execute Non Query

        public string ExecuteNonQuery(string strSql)
        {
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Public Methods

        public string UploadCheckTableAndField(string strTable, List<string> lstField)
        {
            string strResult = string.Empty;

            if (!IsTableExists(strTable))
                strResult = "Table " + strTable + " is not found";

            if (string.IsNullOrEmpty(strResult))
            {
                string strResultCheckFields = IsFieldsExists(strTable, lstField);

                if (string.IsNullOrEmpty(strResultCheckFields))
                {
                    if (!string.IsNullOrEmpty(strResult))
                        strResult += "\n";

                    strResult += strResultCheckFields;
                }
            }

            return strResult;
        }

        public string UploadRecord(string strTableName, List<string> lstField, List<string> lstFieldType, List<string> lstKey, List<string> lstValue)
        {
            string strResult = string.Empty;
            string strSql = string.Empty;
            string strCONO = string.Empty;
            string strBRNO = string.Empty;
            string strCRUS = string.Empty;
            List<string> lstCondition = new List<string>();
            List<string> lstConditionFieldType = new List<string>();
            List<string> lstConditionValue = new List<string>();

            //Create List Condition By Key
            for (int i = 0; i < lstField.Count; i++)
            {
                if (lstKey[i].ToUpper() == "P")
                    lstCondition.Add(lstField[i]);

                if (lstKey[i].ToUpper() == "P")
                    lstConditionFieldType.Add(lstFieldType[i]);

                if (lstKey[i].ToUpper() == "P")
                    lstConditionValue.Add(lstValue[i]);

                if (lstField[i].ToUpper().Contains("CONO"))
                    strCONO = lstValue[i].Trim();

                if (lstField[i].ToUpper().Contains("BRNO"))
                    strBRNO = lstValue[i].Trim();

                if (lstField[i].ToUpper().Contains("CRUS"))
                    strCRUS = lstValue[i].Trim();
            }

            if (!IsExists(strTableName, lstCondition, lstConditionFieldType, lstConditionValue))
            {
                strSql = GenerateStringInsert(strTableName, lstField, lstFieldType, lstValue, strCONO, strBRNO, strCRUS);
                strResult = ExecuteDbNonQuery(strSql);
            }
            else
            {
                strSql = GenerateStringUpdate(strTableName, lstCondition, lstConditionFieldType, lstConditionValue, lstField, lstFieldType, lstValue);
                strResult = ExecuteDbNonQuery(strSql);
            }

            return strResult;
        }

        #endregion

        #region Methods
        public bool IsPERDExists(string strPERD)
        {
            string strSql = "SELECT CASE WHEN EXISTS(SELECT GZWRDT FROM GBM3 WHERE GZWRDT=" + strPERD + ") THEN 1 ELSE 0 END";

            try
            {
                Object _obj = this.ExecuteDbScalar(strSql);

                if (Convert.ToInt32(_obj) == 1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsTableExists(string strTable)
        {
            string strSql = "SELECT CASE WHEN EXISTS(SELECT name FROM sys.tables WHERE name='" + strTable + "') THEN 1 ELSE 0 END";

            try
            {
                Object _obj = this.ExecuteDbScalar(strSql);

                if (Convert.ToInt32(_obj) == 1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        string IsFieldsExists(string strTable, List<string> lstField)
        {
            string strSql = string.Empty;
            string strResult = string.Empty;

            foreach (string strField in lstField)
            {
                if (!string.IsNullOrEmpty(strField))
                {
                    strSql = "SELECT CASE WHEN EXISTS "
                            + " (SELECT t.name AS table_name "
                            + " , c.name AS column_name "
                            + " FROM sys.tables AS t "
                            + " INNER JOIN sys.columns c ON t.OBJECT_ID = c.OBJECT_ID "
                            + " WHERE 1=1 "
                            + " AND t.name='" + strTable + "' "
                            + " AND c.name='" + strField + "') THEN 1 "
                            + " ELSE 0 END ";

                    try
                    {
                        Object _obj = this.ExecuteDbScalar(strSql);

                        if (Convert.ToInt32(_obj) == 0)
                        {
                            if (!string.IsNullOrEmpty(strResult))
                                strResult += "\n";

                            strResult += "Field " + strField + " is not exists in table " + strTable;
                        }
                    }
                    catch
                    {
                        if (!string.IsNullOrEmpty(strResult))
                            strResult += "\n";

                        strResult += "Field " + strField + " is not exists in table " + strTable;
                    }
                }
            }

            return strResult;
        }

        bool IsExists(string strTableName, List<string> lstCondition, List<string> lstConditionFieldType, List<string> lstConditionValue)
        {
            string strSql = "SELECT CASE WHEN EXISTS(SELECT * FROM " + strTableName + GenerateStringFilter(lstCondition, lstConditionFieldType, lstConditionValue) + ") THEN 1 ELSE 0 END";

            try
            {
                Object _obj = this.ExecuteDbScalar(strSql);

                if (Convert.ToInt32(_obj) == 1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Generate Script

        public string GenerateScriptUpload(string strTableName, List<string> lstField, List<string> lstFieldType, List<string> lstKey, List<string> lstValue)
        {
            string strSql = string.Empty;
            string strFilter = string.Empty;
            string strValue = string.Empty;
            string strCONO = string.Empty;
            string strBRNO = string.Empty;
            string strCRUS = string.Empty;
            List<string> lstFilter = new List<string>();
            List<string> lstFilterFieldType = new List<string>();
            List<string> lstFilterValue = new List<string>();

            //Create List Condition By Key
            for (int i = 0; i < lstField.Count; i++)
            {
                if (lstKey[i].ToUpper() == "P")
                    lstFilter.Add(lstField[i]);

                if (lstKey[i].ToUpper() == "P")
                    lstFilterFieldType.Add(lstFieldType[i]);

                if (lstKey[i].ToUpper() == "P")
                    lstFilterValue.Add(lstValue[i]);

                if (lstField[i].ToUpper().Contains("CONO"))
                    strCONO = lstValue[i].Trim();

                if (lstField[i].ToUpper().Contains("BRNO"))
                    strBRNO = lstValue[i].Trim();

                if (lstField[i].ToUpper().Contains("CRUS"))
                    strCRUS = lstValue[i].Trim();
            }

            for (int i = 0; i < lstFilter.Count; i++)
            {
                if (!string.IsNullOrEmpty(lstFilter[i]) && !string.IsNullOrEmpty(lstFilterFieldType[i]))
                {
                    if (strFilter != string.Empty)
                    {
                        strFilter += " AND ";
                    }

                    if (lstFilterFieldType[i].ToUpper().Contains("NUMERIC"))
                    {
                        strValue = (!string.IsNullOrEmpty(lstFilterValue[i]) ? lstFilterValue[i].Replace(",", ".") : "0");
                    }
                    else
                    {
                        strValue = "'" + (!string.IsNullOrEmpty(lstFilterValue[i]) ? lstFilterValue[i].Replace("'", "''").Trim() : String.Empty) + "'";
                    }

                    strFilter += lstFilter[i] + " = " + strValue.Trim();
                }
            }

            strSql = " IF NOT EXISTS"
                + " ("
                + " SELECT *"
                + " FROM"
                + " " + strTableName
                + " WHERE 1=1"
                + " AND " + strFilter
                + " )"
                + " BEGIN"
                + " " + GenerateStringInsert(strTableName, lstField, lstFieldType, lstValue, strCONO, strBRNO, strCRUS)
                + " END"
                + " ELSE"
                + " BEGIN"
                + " " + GenerateStringUpdate(strTableName, lstFilter, lstFilterFieldType, lstFilterValue, lstField, lstFieldType, lstValue)
                + " END"
                + " ";

            return strSql;
        }

        public string GenerateStringInsert(string strTableName, List<string> lstField, List<string> lstFieldType, List<string> lstValue, string strCONO, string strBRNO, string strCRUS)
        {
            //Declare
            string strSql = String.Empty;
            string strField = String.Empty;
            string strValue = String.Empty;
            string strResult = string.Empty;

            //Validation
            if (lstField == null)
            {
                throw new Exception("Field is not defined");
            }

            //Action
            for (int i = 0; i < lstField.Count; i++)
            {
                if (!string.IsNullOrEmpty(lstField[i]) && !string.IsNullOrEmpty(lstFieldType[i]))
                {
                    if (strField != String.Empty)
                    {
                        strField += ", ";
                    }

                    strField += lstField[i];

                    if (strValue != String.Empty)
                    {
                        strValue += ", ";
                    }

                    if (lstFieldType[i].ToUpper().Contains("NUMERIC"))
                    {
                        strValue += (!string.IsNullOrEmpty(lstValue[i]) ? lstValue[i].Replace(",", ".") : "0");
                    }
                    else
                    {
                        strValue += "'" + (!string.IsNullOrEmpty(lstValue[i]) ? lstValue[i].Replace("'", "''").Trim() : String.Empty) + "'";
                    }
                }
            }

            strSql = "INSERT INTO " + strTableName.Trim() + " ( " + strField + " ) VALUES ( " + strValue + " )";

            return strSql;
        }

        string GenerateStringUpdate(string strTableName, List<string> lstFilter, List<string> lstFilterFieldType, List<string> lstFilterValue, List<string> lstField, List<string> lstFieldType, List<string> lstValue)
        {
            //Declare
            string strSql = string.Empty;
            string strField = string.Empty;
            string strValue = string.Empty;
            string strFilter = string.Empty;

            //Validation
            if (lstField == null)
            {
                throw new Exception("Field is not defined");
            }

            if (lstFilter == null)
            {
                throw new Exception("Condition is not defined");
            }

            //Action
            for (int i = 0; i < lstField.Count; i++)
            {
                if (!string.IsNullOrEmpty(lstField[i]) && !string.IsNullOrEmpty(lstFieldType[i]))
                {
                    if (!lstField[i].Contains("CRDT") && !lstField[i].Contains("CRTM") && !lstField[i].Contains("CRUS"))
                    {
                        if (strField != string.Empty)
                        {
                            strField += ", ";
                        }

                        if (lstFieldType[i].ToUpper().Contains("NUMERIC"))
                        {
                            strValue = (!string.IsNullOrEmpty(lstValue[i]) ? lstValue[i].Replace(",", ".") : "0");
                        }
                        else
                        {
                            strValue = "'" + (!string.IsNullOrEmpty(lstValue[i]) ? lstValue[i].Replace("'", "''").Trim() : String.Empty) + "'";
                        }

                        strField += lstField[i] + " = " + strValue.Trim();
                    }
                }
            }

            for (int i = 0; i < lstFilter.Count; i++)
            {
                if (!string.IsNullOrEmpty(lstFilter[i]) && !string.IsNullOrEmpty(lstFilterFieldType[i]))
                {
                    if (strFilter != string.Empty)
                    {
                        strFilter += " AND ";
                    }

                    if (lstFilterFieldType[i].ToUpper().Contains("NUMERIC"))
                    {
                        strValue = (!string.IsNullOrEmpty(lstFilterValue[i]) ? lstFilterValue[i].Replace(",", ".") : "0");
                    }
                    else
                    {
                        strValue = "'" + (!string.IsNullOrEmpty(lstFilterValue[i]) ? lstFilterValue[i].Replace("'", "''").Trim() : String.Empty) + "'";
                    }

                    strFilter += lstFilter[i] + " = " + strValue.Trim();
                }
            }

            strSql = "UPDATE " + strTableName.Trim() + " SET " + strField;

            if (strFilter != string.Empty)
            {
                strSql += " WHERE 1=1 AND " + strFilter;
            }

            return strSql;
        }

        protected string GenerateStringFilter(List<string> lstCondition, List<string> lstConditionFieldType, List<string> lstConditionValue)
        {
            //Declare
            string strSql = String.Empty;
            string strVal = String.Empty;
            string strCnd = String.Empty;

            //Validation
            if (lstCondition == null)
            {
                throw new Exception("Condition is not defined");
            }

            //Action
            for (int i = 0; i < lstCondition.Count; i++)
            {
                if (!string.IsNullOrEmpty(lstCondition[i]) && !string.IsNullOrEmpty(lstConditionFieldType[i]))
                {
                    if (strCnd != String.Empty)
                    {
                        strCnd += " AND ";
                    }

                    if (lstConditionFieldType[i].ToUpper().Contains("NUMERIC"))
                    {
                        strVal = (!string.IsNullOrEmpty(lstConditionValue[i]) ? lstConditionValue[i].Replace(",", ".") : "0");
                    }
                    else
                    {
                        strVal = "'" + (!string.IsNullOrEmpty(lstConditionValue[i]) ? lstConditionValue[i].Replace("'", "''") : String.Empty) + "'";
                    }

                    strCnd += lstCondition[i] + " = " + strVal;
                }
            }

            if (strCnd != String.Empty)
            {
                strSql += " WHERE 1=1 AND " + strCnd;
            }

            return strSql;
        }

        #endregion

    }
}
