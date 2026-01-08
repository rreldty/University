using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using University.Dto.Base;
using System.CodeDom.Compiler;

namespace University.Dao.Base
{
    public abstract class BaseDao<T>
    {
        private string m_providerName;
        private string m_providerType;
        private string m_connString;
        private DataSource m_dataSource;
        private DbProviderFactory m_factory;
        private DbTransaction m_factoryTrans;
        protected abstract Mapper<T> GetMapper();

        #region Enumerator

        public enum DataSource
        {
            University,
            SSAS,
            SYNU,
            SYND
        }

        #endregion

        #region Constructor

        public BaseDao()
        {

        }

        #endregion

        #region Common Method

        public DbTransaction BeginTransaction(DbConnection _con)
        {
            this.m_factoryTrans = _con.BeginTransaction();
            return m_factoryTrans;
        }

        public void CommitTransaction()
        {
            this.m_factoryTrans.Commit();
        }

        public void RollbackTransaction()
        {
            this.m_factoryTrans.Rollback();
        }

        #endregion

        #region AddInOutParamater
        //this function is for inserting database input parameter
        protected DbParameter AddInputParameter(string strParam, object objValue, DbType dbType)
        {
            DbParameter dbParameter = MainFactory.CreateParameter();
            dbParameter.ParameterName = strParam;
            dbParameter.Value = objValue;
            dbParameter.DbType = dbType;
            dbParameter.Direction = ParameterDirection.Input;
            return dbParameter;
            //this.MainCommand.Parameters.Add(dbParameter);
        }

        //this function is for getting value from database output parameter
        protected DbParameter AddOutputParameter(string strParam, object objValue, DbType dbType)
        {
            DbParameter dbParameter = MainFactory.CreateParameter();
            dbParameter.ParameterName = strParam;
            dbParameter.Value = objValue;
            dbParameter.DbType = dbType;
            dbParameter.Direction = ParameterDirection.Output;
            return dbParameter;
            //this.MainCommand.Parameters.Add(dbParameter);
        }

        //this function is for getting value from database output parameter
        protected DbParameter AddOutputParameter(string strParam, object objValue, DbType dbType, Int32 intSize)
        {
            DbParameter dbParameter = m_factory.CreateParameter();
            dbParameter.ParameterName = strParam;
            dbParameter.Value = objValue;
            dbParameter.DbType = dbType;
            dbParameter.Size = intSize;
            dbParameter.Direction = ParameterDirection.Output;
            return dbParameter;
            //this.MainCommand.Parameters.Add(dbParameter);
        }
        
        //this function is for insert/getting value from database parameter
        protected DbParameter AddInputOutputParameter(string strParam, object objValue, DbType dbType)
        {
            DbParameter dbParameter = MainFactory.CreateParameter();
            dbParameter.ParameterName = strParam;
            dbParameter.Value = objValue;
            dbParameter.DbType = dbType;
            dbParameter.Direction = ParameterDirection.InputOutput;
            return dbParameter;
            //this.MainCommand.Parameters.Add(dbParameter);
        }
        #endregion

        #region ExecuteDbNonQuery
        /// <summary>
        /// Execute sql query
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="lstDbParam"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQueryNonTransaction(string strQuery, List<DbParameter> lstDbParam)
        {
            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    //cmd.CommandText = QueryRegexp(strQuery);
                    cmd.CommandText = strQuery;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        cmd.ExecuteNonQuery();

                        return string.Empty;
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                        cmd.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Execute sql query
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQueryNonTransaction(string strQuery)
        {
            return ExecuteDbNonQueryNonTransaction(strQuery, null);
        }

        /// <summary>
        /// Execute sql query
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="lstDbParam"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQuery(string strQuery, List<DbParameter> lstDbParam)
        {
            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    //cmd.CommandText = QueryRegexp(strQuery);
                    cmd.CommandText = strQuery;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        cmd.Transaction = BeginTransaction(con);
                        cmd.ExecuteNonQuery();

                        CommitTransaction();

                        return string.Empty;
                    }
                    catch (Exception ex)
                    {
                        RollbackTransaction();

                        return ex.Message;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                        cmd.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Execute sql query
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQuery(string strQuery)
        {
            return ExecuteDbNonQuery(strQuery, null);
        }

        /// <summary>
        /// Execute list of sql query
        /// </summary>
        /// <param name="lstQuery"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQueryWithTransaction(List<string> lstQuery, DbCommand cmd)
        {
            string strCommand = string.Empty;

            foreach (string strQuery in lstQuery)
            {
                strCommand += " " + strQuery;
            }

            cmd.CommandText = strCommand;

            try
            {
                cmd.ExecuteNonQuery();

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Execute sql query
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQueryWithTransaction(string strQuery, DbCommand cmd)
        {
            cmd.CommandText = strQuery;

            try
            {
                cmd.ExecuteNonQuery();

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public DataSet ExecuteDataSetSP(string strQuery, List<DbParameter> lstDbParam, out string strResult)
        {
            strResult = string.Empty;

            using (DbConnection con = MainConnection)
            {
                DataSet dtsReturn = null;

                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = MainTimeout;
                    cmd.CommandText = strQuery;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DbDataAdapter da = this.MainAdapter;
                        da.SelectCommand = cmd;
                        dtsReturn = new DataSet();
                        da.Fill(dtsReturn);
                    }
                    catch (Exception ex)
                    {
                        strResult = ex.Message;
                        return null;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                }

                return dtsReturn;
            }
        }

        /// <summary>
        /// Execute stored procedure
        /// </summary>
        /// <param name="strSP"></param>
        /// <param name="lstDbParam"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQuerySP(string strSP, List<DbParameter> lstDbParam)
        {
            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = MainTimeout;
                    cmd.CommandText = strSP;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        cmd.Transaction = BeginTransaction(con);
                        cmd.ExecuteNonQuery();

                        CommitTransaction();

                        return string.Empty;
                    }
                    catch (Exception ex)
                    {
                        RollbackTransaction();

                        return ex.Message;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                        cmd.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Execute stored procedure
        /// </summary>
        /// <param name="strSP"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQuerySP(string strSP)
        {
            return ExecuteDbNonQuerySP(strSP, null);
        }


        /// <summary>
        /// Execute stored procedure
        /// </summary>
        /// <param name="strSP"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQueryNonTransactionSP(string strSP)
        {
            return ExecuteDbNonQueryNonTransaction(strSP, null);
        }


        /// <summary>
        /// Execute list of sql query
        /// </summary>
        /// <param name="lstQuery"></param>
        /// <returns></returns>
        public string ExecuteDbNonQueryTransaction(List<string> lstQuery)
        {
            string strResult = string.Empty;
            string strCommand = string.Empty;

            using (DbConnection conn = MainConnection)
            {
                try
                {
                    conn.ConnectionString = MainConnString;
                    conn.Open();
                    BeginTransaction(conn);

                    //strCommand += "BEGIN TRY ";
                    //strCommand += "BEGIN TRANSACTION ";

                    foreach (string strQuery in lstQuery)
                    {
                        strCommand += " " + strQuery;
                    }

                    //strCommand += " ";
                    //strCommand += "COMMIT TRANSACTION ";
                    //strCommand += "END TRY ";
                    //strCommand += "BEGIN CATCH ";
                    //strCommand += "ROLLBACK TRANSACTION ";
                    //strCommand += "END CATCH";

                    using (DbCommand cmd = MainCommand)
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = MainTimeout;
                        //cmd.CommandText = QueryRegexp(strCommand);
                        cmd.CommandText = strCommand;

                        try
                        {
                            cmd.Transaction = this.m_factoryTrans;
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            RollbackTransaction();
                            strResult = ex.Message;
                        }
                    }

                    if (string.IsNullOrEmpty(strResult))
                        CommitTransaction();
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                        RollbackTransaction();

                    strResult = ex.Message;
                }
            }
            return strResult;
        }



        /// <summary>
        /// Execute list of sql query
        /// </summary>
        /// <param name="lstQuery"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string ExecuteDbNonQueryTransaction(List<string> lstQuery, DbCommand cmd)
        {
            string strResult = string.Empty;
            string strCommand = string.Empty;

            foreach (string strQuery in lstQuery)
            {
                strCommand += " " + strQuery;
            }

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strCommand;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return strResult;
        }

        /// <summary>
        /// Execute sql query
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQueryTransaction(string strQuery, DbConnection conn, DbTransaction trans)
        {
            string strResult = string.Empty;

            using (DbCommand cmd = MainCommand)
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = MainTimeout;
                //cmd.CommandText = QueryRegexp(strQuery);
                cmd.CommandText = strQuery;

                try
                {
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    strResult = ex.Message;
                }
            }

            return strResult;
        }


        /// <summary>
        /// Execute list of stored procedure
        /// </summary>
        /// <param name="lstSP"></param>
        /// <param name="lstDbParam"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQuerySPTransaction(List<string> lstSP, List<DbParameter>[] lstDbParam)
        {
            string strResult = string.Empty;

            using (DbConnection conn = MainConnection)
            {
                try
                {
                    conn.ConnectionString = MainConnString;
                    conn.Open();
                    BeginTransaction(conn);

                    int i = 0;

                    for (i = 0; i < lstSP.Count - 1; i++)
                    {
                        using (DbCommand cmd = MainCommand)
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = MainTimeout;
                            cmd.CommandText = lstSP[i];

                            List<DbParameter> _lstDbParam = lstDbParam[i];

                            if (_lstDbParam != null && _lstDbParam.Count > 0)
                            {
                                foreach (DbParameter param in _lstDbParam)
                                {
                                    cmd.Parameters.Add(param);
                                }
                            }

                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                RollbackTransaction();
                                strResult = ex.Message;
                                break;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(strResult))
                        CommitTransaction();
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                        RollbackTransaction();

                    strResult = ex.Message;
                }
            }
            return strResult;
        }

        /// <summary>
        /// Execute stored procedure
        /// </summary>
        /// <param name="strSP"></param>
        /// <param name="lstDbParam"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQuerySPTransaction(string strSP, List<DbParameter> lstDbParam, DbConnection conn, DbTransaction trans)
        {
            string strResult = string.Empty;

            using (DbCommand cmd = MainCommand)
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = MainTimeout;
                cmd.CommandText = strSP;

                if (lstDbParam != null && lstDbParam.Count > 0)
                {
                    foreach (DbParameter param in lstDbParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                try
                {
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    strResult = ex.Message;
                }
            }

            return strResult;
        }

        /// <summary>
        /// Execute AS400 query
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQueryAS400(string strQuery)
        {
            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;

                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    //cmd.CommandText = QueryRegexp(strQuery);
                    cmd.CommandText = strQuery;

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        cmd.ExecuteNonQuery();

                        return string.Empty;
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }
        }

        /// <summary>
        /// Execute AS400 query
        /// </summary>
        /// <param name="lstQuery"></param>
        /// <returns></returns>
        protected string ExecuteDbNonQueryAS400(List<string> lstQuery)
        {
            string strResult = string.Empty;

            using (DbConnection conn = MainConnection)
            {
                try
                {
                    conn.ConnectionString = MainConnString;
                    conn.Open();

                    foreach (string strQuery in lstQuery)
                    {
                        using (DbCommand cmd = MainCommand)
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandTimeout = MainTimeout;
                            //cmd.CommandText = QueryRegexp(strQuery);
                            cmd.CommandText = strQuery;

                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                strResult = ex.Message;
                                break;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    strResult = ex.Message;
                }
            }
            return strResult;
        }
        #endregion

        #region ExecuteDbScalar
        //this function is for getting scalar value or one column query in database
        protected object ExecuteDbScalar(string strQuery, List<DbParameter> lstDbParam)
        {
            object objReturn = null;

            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;

                using (DbCommand cmd = this.MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandTimeout = MainTimeout;
                    cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = QueryRegexp(strQuery);
                    cmd.CommandText = strQuery;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        objReturn = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                        cmd.Dispose();
                    }
                }
            }

            return objReturn;
        }

        protected object ExecuteDbScalar(string strQuery)
        {
            return ExecuteDbScalar(strQuery, null);
        }

        //this function is for getting scalar value or one column query in database with stored procedure
        protected object ExecuteDbScalarSP(string strSP, List<DbParameter> lstDbParam)
        {
            object objReturn = null;

            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = this.MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = MainTimeout;
                    cmd.CommandText = strSP;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        objReturn = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                        cmd.Dispose();
                    }
                }
            }

            return objReturn;
        }

        //this function is for getting scalar value or one column query in database
        protected object ExecuteDbScalarWithTransaction(string strQuery, List<DbParameter> lstDbParam, DbCommand cmd)
        {
            object objReturn = null;
            cmd.CommandText = strQuery;

            if (lstDbParam != null && lstDbParam.Count > 0)
            {
                foreach (DbParameter param in lstDbParam)
                {
                    cmd.Parameters.Add(param);
                }
            }

            try
            {
                objReturn = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return objReturn;
        }

        protected object ExecuteDbScalarWithTransaction(string strQuery, DbCommand cmd)
        {
            return ExecuteDbScalarWithTransaction(strQuery, null, cmd);
        }

        protected object ExecuteDbScalarSP(string strSP)
        {
            return ExecuteDbScalarSP(strSP, null);
        }
        #endregion

        #region ExecuteDataSet
        public DataSet ExecuteDataSet(string strQuery, List<DbParameter> lstDbParam)
        {
            DataSet dtsReturn = null;

            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    //cmd.CommandText = QueryRegexp(strQuery);
                    cmd.CommandText = strQuery;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();


                        DbDataAdapter da = this.MainAdapter;
                        da.SelectCommand = cmd;
                        dtsReturn = new DataSet();
                        da.Fill(dtsReturn);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                }
            }

            return dtsReturn;
        }

        public DataSet ExecuteDataSet(string strQuery, out string strResult)
        {
            strResult = string.Empty;
            DataSet dtReturn = null;

            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    cmd.CommandText = strQuery;

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DbDataAdapter da = this.MainAdapter;
                        da.SelectCommand = cmd;
                        dtReturn = new DataSet();
                        da.Fill(dtReturn);
                    }
                    catch (Exception ex)
                    {
                        strResult = ex.Message;
                        return null;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                }

                return dtReturn;
            }
        }

        public DataSet ExecuteDataSet(string strQuery)
        {
            return ExecuteDataSet(strQuery, null);
        }
        #endregion

        #region ExecuteDataTable
        //this function is for getting list of data, return in datatable
        public DataTable ExecuteDataTable(string strQuery, List<DbParameter> lstDbParam)
        {
            using (DbConnection con = MainConnection)
            {
                DataTable dtReturn = null;

                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    //cmd.CommandText = QueryRegexp(strQuery);
                    cmd.CommandText = strQuery;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();


                        DbDataAdapter da = this.MainAdapter;
                        da.SelectCommand = cmd;
                        dtReturn = new DataTable();
                        da.Fill(dtReturn);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                }

                return dtReturn;
            }
        }
        public DataTable ExecuteDataTable(string strQuery, out string strResult)
        {
            strResult = string.Empty;
            DataTable dtReturn = null;

            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    //cmd.CommandText = QueryRegexp(strQuery);
                    cmd.CommandText = strQuery;

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DbDataAdapter da = this.MainAdapter;
                        da.SelectCommand = cmd;
                        dtReturn = new DataTable();
                        da.Fill(dtReturn);
                    }
                    catch (Exception ex)
                    {
                        strResult = ex.Message;
                        return null;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                }

                return dtReturn;
            }
        }

        public DataTable ExecuteDataTable(string strQuery)
        {
            return ExecuteDataTable(strQuery, null);
        }

        //this function is for getting list of data, return in datatable with stored procedure
        public DataTable ExecuteDataTableSP(string strSP, List<DbParameter> lstDbParam)
        {
            using (DbConnection con = MainConnection)
            {
                DataTable dtReturn = null;

                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = MainTimeout;
                    cmd.CommandText = strSP;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();


                        DbDataAdapter da = this.MainAdapter;
                        da.SelectCommand = cmd;
                        dtReturn = new DataTable();
                        da.Fill(dtReturn);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                }

                return dtReturn;
            }
        }

        public DataTable ExecuteDataTableSP(string strSP)
        {
            return ExecuteDataTableSP(strSP, null);
        }
        #endregion

        #region ExecuteQuery
        //this function is for getting list of data, return in list of class object
        protected List<T> ExecuteQuery(string strQuery, List<DbParameter> lstDbParam)
        {
            List<T> lstT = new List<T>();

            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    //cmd.CommandText = QueryRegexp(strQuery);
                    cmd.CommandText = strQuery;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DbDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (dbReader.HasRows)
                        {
                            Mapper<T> mapObj = GetMapper();
                            lstT = mapObj.MapAll(dbReader);
                        }

                        return lstT;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                }
            }
        }

        protected List<T> ExecuteQuery(string strQuery)
        {
            return ExecuteQuery(strQuery, null);
        }

        //this function is for getting list of data, return in list of class object with DB Command
        protected List<T> ExecuteQueryWithTransaction(string strQuery, List<DbParameter> lstDbParam, DbCommand cmd)
        {
            List<T> lstT = new List<T>();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;

            if (lstDbParam != null && lstDbParam.Count > 0)
            {
                foreach (DbParameter param in lstDbParam)
                {
                    cmd.Parameters.Add(param);
                }
            }

            DbDataReader dbReader = null;

            try
            {
                dbReader = cmd.ExecuteReader();

                if (dbReader.HasRows)
                {
                    Mapper<T> mapObj = GetMapper();
                    lstT = mapObj.MapAll(dbReader);
                }

                return lstT;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dbReader.Dispose();
            }
        }

        protected List<T> ExecuteQueryWithTransaction(string strQuery, DbCommand cmd)
        {
            return ExecuteQueryWithTransaction(strQuery, null, cmd);
        }

        //this function is for getting list of data, return in list of class object with stored procedure
        protected List<T> ExecuteQuerySP(string strSP, List<DbParameter> lstDbParam)
        {
            List<T> lstT = new List<T>();

            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = MainTimeout;
                    cmd.CommandText = strSP;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DbDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (dbReader.HasRows)
                        {
                            Mapper<T> mapObj = GetMapper();
                            lstT = mapObj.MapAll(dbReader);
                        }

                        return lstT;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                }
            }
        }

        protected List<T> ExecuteQuerySP(string strSP)
        {
            return ExecuteQuerySP(strSP, null);
        }
        #endregion

        #region ExecuteQueryOne
        //this function is for getting one record, return in class object
        protected T ExecuteQueryOneWithTransaction(string strQuery, List<DbParameter> lstDbParam, DbCommand cmd)
        {
            T objT = default(T);

            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = MainTimeout;
            //cmd.CommandText = QueryRegexp(strQuery);
            cmd.CommandText = strQuery;

            if (lstDbParam != null && lstDbParam.Count > 0)
            {
                foreach (DbParameter param in lstDbParam)
                {
                    cmd.Parameters.Add(param);
                }
            }

            DbDataReader dbReader = null;

            try
            {
                dbReader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dbReader.HasRows)
                {
                    Mapper<T> mapObj = GetMapper();
                    objT = mapObj.Map(dbReader);

                    if (objT is T)
                        return (T)objT;
                    else
                        return default(T);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dbReader.Dispose();
            }
        }

        protected T ExecuteQueryOneWithTransaction(string strQuery, DbCommand cmd)
        {
            return ExecuteQueryOneWithTransaction(strQuery, null, cmd);
        }

        //this function is for getting one record, return in class object
        protected T ExecuteQueryOne(string strQuery, List<DbParameter> lstDbParam)
        {
            using (DbConnection con = MainConnection)
            {
                T objT = default(T);

                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    //cmd.CommandText = QueryRegexp(strQuery);
                    cmd.CommandText = strQuery;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DbDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (dbReader.HasRows)
                        {
                            Mapper<T> mapObj = GetMapper();
                            objT = mapObj.Map(dbReader);

                            if (objT is T)
                                return (T)objT;
                            else
                                return default(T);
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                } //end of using DbCommand

                //return objT;
            }
        }

        protected T ExecuteQueryOne(string strQuery)
        {
            return ExecuteQueryOne(strQuery, null);
        }

        //this function is for getting one record, return in class object with stored procedure
        protected T ExecuteQueryOneSP(string strSP, List<DbParameter> lstDbParam)
        {
            using (DbConnection con = MainConnection)
            {
                T objT = default(T);

                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = MainTimeout;
                    cmd.CommandText = strSP;

                    if (lstDbParam != null && lstDbParam.Count > 0)
                    {
                        foreach (DbParameter param in lstDbParam)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DbDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        if (dbReader.HasRows)
                        {
                            Mapper<T> mapObj = GetMapper();
                            objT = mapObj.Map(dbReader);

                            if (objT is T)
                                return (T)objT;
                            else
                                return default(T);
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                } //end of using DbCommand

                //return objT;
            }
        }

        protected T ExecuteQueryOneSP(string strSP)
        {
            return ExecuteQueryOneSP(strSP, null);
        }
        #endregion

        #region ExecutePaging
        //this function is for getting partial list of data as for grid paging, return in list of class object
        protected List<T> ExecutePaging(string strQuery, string strOrderBy, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            return ExecutePaging(strQuery, strOrderBy, intPageNumber, intPageSize, out intTotalPage, out intTotalRecord, "");
        }

        //this function is for getting partial list of data as for grid paging, return in list of class object
        protected List<T> ExecutePaging(string strQuery, string strOrderBy, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord, string strFilterAS400)
        {
            strQuery = GenerateScriptPaging(strQuery, strOrderBy, intPageNumber, intPageSize, out intTotalPage, out intTotalRecord, strFilterAS400);
            
            List<T> lstT = null;
            IEnumerable<T> lst = null;

            if (!string.IsNullOrEmpty(strQuery)) 
            {
                lst = ExecuteQuery(strQuery);
            }
            
            if (lst != null)
            {
                if (MainProviderType == "AS400")
                {
                    string[] sortParts = strOrderBy.Trim().Split(',');
                    Dictionary<string, Extension.SortOrder> orderBy = new Dictionary<string, Extension.SortOrder>();

                    foreach (string sortBy in sortParts)
                    {
                        string[] sortField = sortBy.Trim().Split(' ');

                        if (sortField.Length == 2)
                            orderBy.Add(sortField[0], (sortField[1].Trim() == "ASC" ? Extension.SortOrder.ASC : Extension.SortOrder.DESC));
                        else
                            orderBy.Add(sortField[0], Extension.SortOrder.ASC);
                    }

                    lstT = Extension.Sort<T>(lst, orderBy).ToList();
                }
                else
                {
                    lstT = lst.ToList();
                }
            }
            else
            {
                lstT = new List<T>();
            }

            return lstT;
        }

        protected DataTable ExecuteDataTablePaging(string strQuery, string strOrderBy, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            return ExecuteDataTablePaging(strQuery, strOrderBy, intPageNumber, intPageSize, out intTotalPage, out intTotalRecord, "");
        }

        //this function is for getting partial list of data as for grid paging, return in datatable
        public DataTable ExecuteDataTablePaging(string strQuery, string strOrderBy, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord, string strFilterAS400)
        {
            strQuery = GenerateScriptPaging(strQuery, strOrderBy, intPageNumber, intPageSize, out intTotalPage, out intTotalRecord, strFilterAS400);
            
            DataTable dtReturn = new DataTable("tblPaging");
            DataTable dt = null;

            if (!string.IsNullOrEmpty(strQuery))
            {
                dt = ExecuteDataTable(strQuery);
            }

            if (dt != null)
            {
                if (MainProviderType == "AS400")
                {
                    dt.DefaultView.Sort = strOrderBy;

                    if (intPageNumber != 1)
                        dtReturn = dt.DefaultView.ToTable();
                    else
                        dtReturn = dt;
                }
                else
                    dtReturn = dt;
            }

            return dtReturn;
        }

        string GenerateScriptPaging(string strQuery, string strOrderBy, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord, string strFilterAS400)
        {
            intTotalPage = 1;
            intTotalRecord = 0;
            int intRemainRecord = 0;
            string strOrderBy1 = string.Empty;

            if (intPageSize > 0)
            {
                //get total records
                bool bolCount = false;

                if (MainProviderType == "AS400")
                {
                    if (strQuery.ToUpper().Contains("COUNT") || strQuery.ToUpper().Contains("SUM"))
                    {
                        bolCount = true;
                        DataTable dt = this.ExecuteDataTable(strQuery);

                        if (dt != null)
                            intTotalRecord = dt.Rows.Count;
                    }
                }

                if (!bolCount)
                {
                    string strCount = string.Empty;

                    if (strQuery.ToUpper().Contains("GROUP BY"))
                    {
                        strQuery = "SELECT * FROM (" + strQuery + ") TBLGRP";
                        strCount = "SELECT COUNT(0) FROM (" + strQuery + ") TBL";
                    }
                    else
                    {
                        strCount = "SELECT COUNT(0) FROM (" + strQuery + ") TBL";
                    }

                    intTotalRecord = Convert.ToInt32(ExecuteDbScalar(strCount));
                }

                if (intTotalRecord > intPageSize)
                    intTotalPage = (intTotalRecord / intPageSize);
                else
                    intTotalPage = 1;

                if (intTotalRecord > intPageSize)
                    intRemainRecord = (intTotalRecord % intPageSize);

                if (intRemainRecord > 0)
                    intTotalPage++;

                if (intPageNumber < 1)
                    intPageNumber = 1;

                if (intTotalRecord > 0)
                {
                    if (intPageNumber <= intTotalPage)
                    {
                        int intLBound = (intPageNumber - 1) * intPageSize;
                        int intUBound = 0;

                        if (intLBound + intPageSize >= intTotalRecord)
                        {
                            if (intRemainRecord != 0)
                                intUBound = intLBound + intRemainRecord;
                            else
                                intUBound = intTotalRecord;
                        }
                        else
                            intUBound = intLBound + intPageSize;

                        string strFirstColumn = string.Empty;
                        string strQueryColumn = string.Empty;

                        if (strQuery.Contains(") TBLGRP"))
                        {
                            strQueryColumn = strQuery.Replace("SELECT * FROM", "SELECT TOP 1 * FROM");
                            strQueryColumn = strQueryColumn.Replace("TBLGRP", "TBLCOL");
                        }
                        else 
                        {
                            strQueryColumn = "SELECT TOP 1 * FROM (" + strQuery + ") TBLCOL";
                        }

                        DataTable dtColumn = ExecuteDataTable(strQueryColumn);
                        if (dtColumn != null) 
                        {
                            strFirstColumn = "[" + dtColumn.Columns[0].ColumnName + "]";
                        }

                        switch (MainProviderName)
                        {
                            case "IBM.Data.Informix.IfxConnection":
                                {
                                    strQuery = strQuery.Substring(7);
                                    strQuery = " SKIP " + intUBound.ToString() + " FIRST " + (intUBound - intLBound).ToString() + strQuery;
                                    strQuery = "SELECT * FROM (SELECT " + strQuery + ") ";

                                    if (!string.IsNullOrEmpty(strOrderBy.Trim()))
                                        strQuery += " ORDER BY " + strOrderBy;

                                    break;
                                }
                            case "Oracle.DataAccess.Client":
                                {
                                    strQuery = strQuery.Substring(7);
                                    strQuery = " ROWNUM AS REC_NUM, " + strQuery;

                                    strQuery = "SELECT * FROM (SELECT " + strQuery + ") " +
                                                "WHERE (REC_NUM > " + intLBound.ToString() + " AND REC_NUM <= " + intUBound.ToString() + ") ";

                                    if (!string.IsNullOrEmpty(strOrderBy.Trim()))
                                        strQuery += " ORDER BY " + strOrderBy;

                                    break;
                                }
                            case "System.Data.OracleClient":
                                {
                                    strQuery = strQuery.Substring(7);
                                    strQuery = " ROWNUM AS REC_NUM, " + strQuery;

                                    strQuery = "SELECT * FROM (SELECT " + strQuery + ") " +
                                                "WHERE (REC_NUM > " + intLBound.ToString() + " AND REC_NUM <= " + intUBound.ToString() + ") ";

                                    if (!string.IsNullOrEmpty(strOrderBy.Trim()))
                                        strQuery += " ORDER BY " + strOrderBy;

                                    break;
                                }
                            case "System.Data.Odbc":
                                {
                                    switch (MainProviderType)
                                    {
                                        case "MSSQL":
                                            {
                                                strQuery = GenerateSqlServerPagingQuery(strQuery, strOrderBy, strFirstColumn, intLBound, intUBound, intPageSize);
                                                break;
                                            }
                                        case "Oracle":
                                            {
                                                strQuery = strQuery.Substring(7);
                                                strQuery = " ROWNUM AS REC_NUM, " + strQuery;

                                                strQuery = "SELECT * FROM (SELECT " + strQuery + ") " +
                                                            "WHERE (REC_NUM > " + intLBound.ToString() + " AND REC_NUM <= " + intUBound.ToString() + ") ";

                                                if (!string.IsNullOrEmpty(strOrderBy.Trim()))
                                                    strQuery += " ORDER BY " + strOrderBy;

                                                break;
                                            }
                                        case "Informix":
                                            {
                                                strQuery = strQuery.Substring(7);
                                                strQuery = " SKIP " + intUBound.ToString() + " FIRST " + (intUBound - intLBound).ToString() + strQuery;
                                                strQuery = "SELECT * FROM (SELECT " + strQuery + ") ";

                                                if (!string.IsNullOrEmpty(strOrderBy.Trim()))
                                                    strQuery += " ORDER BY " + strOrderBy;

                                                break;
                                            }
                                        case "AS400":
                                            {
                                                strQuery = strQuery.Substring(7);

                                                if (!string.IsNullOrEmpty(strFilterAS400))
                                                {
                                                    if (strQuery.Contains("WHERE"))
                                                        strQuery += " AND " + strFilterAS400;
                                                    else
                                                        strQuery += " WHERE " + strFilterAS400;
                                                }

                                                if (!string.IsNullOrEmpty(strOrderBy.Trim()))
                                                {
                                                    string[] strSort = strOrderBy.Trim().Split(',');

                                                    if (intPageNumber != intTotalPage)
                                                    {
                                                        if (strFilterAS400.Contains("<")) // Previous
                                                        {
                                                            if (strSort[0].Contains("ASC"))
                                                            {
                                                                foreach (string strSort1 in strSort)
                                                                {
                                                                    if (strOrderBy1 != string.Empty)
                                                                        strOrderBy1 += ",";

                                                                    if (strSort1.Trim().ToUpper().Contains("ASC"))
                                                                        strOrderBy1 += strSort1.ToUpper().Replace("ASC", "DESC");
                                                                    else if (strSort1.Trim().ToUpper().Contains("DESC"))
                                                                        strOrderBy1 += strSort1.ToUpper().Replace("DESC", "ASC");
                                                                    else
                                                                        strOrderBy1 += strSort1.Trim();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                strOrderBy1 = strOrderBy;
                                                            }

                                                            strQuery += " ORDER BY " + strOrderBy1;
                                                        }
                                                        else if (strFilterAS400.Contains(">")) // Next
                                                        {
                                                            if (strSort[0].Contains("DESC"))
                                                            {
                                                                foreach (string strSort1 in strSort)
                                                                {
                                                                    if (strOrderBy1 != string.Empty)
                                                                        strOrderBy1 += ",";

                                                                    if (strSort1.Trim().ToUpper().Contains("DESC"))
                                                                        strOrderBy1 += strSort1.ToUpper().Replace("DESC", "ASC");
                                                                    else if (strSort1.Trim().ToUpper().Contains("ASC"))
                                                                        strOrderBy1 += strSort1.ToUpper().Replace("ASC", "DESC");
                                                                    else
                                                                        strOrderBy1 += strSort1.Trim();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                strOrderBy1 = strOrderBy;
                                                            }

                                                            strQuery += " ORDER BY " + strOrderBy1;
                                                        }
                                                        else
                                                        {
                                                            strQuery += " ORDER BY " + strOrderBy;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (intPageSize >= intTotalRecord)
                                                            strOrderBy1 = strOrderBy;
                                                        else
                                                        {
                                                            foreach (string strSort1 in strSort)
                                                            {
                                                                if (strOrderBy1 != string.Empty)
                                                                    strOrderBy1 += ",";

                                                                if (strSort1.Trim().ToUpper().Contains("ASC"))
                                                                    strOrderBy1 += strSort1.ToUpper().Replace("ASC", "DESC");
                                                                else if (strSort1.Trim().ToUpper().Contains("DESC"))
                                                                    strOrderBy1 += strSort1.ToUpper().Replace("DESC", "ASC");
                                                                else
                                                                    strOrderBy1 += strSort1.Trim();
                                                            }
                                                        }

                                                        strQuery += " ORDER BY " + strOrderBy1;
                                                    }
                                                }

                                                strQuery += " FETCH FIRST " + (intUBound - intLBound).ToString().Trim() + " ROWS ONLY ";
                                                strQuery = "SELECT " + strQuery;

                                                break;
                                            }
                                    }

                                    break;
                                }
                            case "System.Data.OleDb":
                                {
                                    strQuery = GenerateSqlServerPagingQuery(strQuery, strOrderBy, strFirstColumn, intLBound, intUBound, intPageSize);
                                    break;
                                }
                            case "System.Data.SqlClient":
                                {
                                    strQuery = GenerateSqlServerPagingQuery(strQuery, strOrderBy, strFirstColumn, intLBound, intUBound, intPageSize);
                                    break;
                                }
                        }
                    }
                } //end of if intTotalRecord
            }

            return strQuery;
        }
        #endregion

        #region ExecuteDbSchema
        protected DataTable ExecuteDbSchema(string strQuery)
        {
            using (DbConnection con = MainConnection)
            {
                con.ConnectionString = MainConnString;
                using (DbCommand cmd = MainCommand)
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = MainTimeout;
                    cmd.CommandText = strQuery;
                    
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DbDataReader dbReader = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
                        DataTable tableSchema = dbReader.GetSchemaTable();
                        return tableSchema;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();

                        cmd.Dispose();
                    }
                } //end of using DbCommand

                //return objT;
            }
        }
        #endregion

        #region GenerateQueryString
        /// <summary>
        /// Generate standard query insert
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="lstField"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GenerateStringInsert(string strTableName, List<string> lstField, object obj)
        {
            //Declare
            string strSql = String.Empty;
            string strFld = String.Empty;
            string strVal = String.Empty;

            //Validation
            if (lstField == null)
            {
                throw new Exception("Field is not defined");
            }

            //Action
            for (int i = 0; i < lstField.Count; i++)
            {
                if (lstField[i] != null && lstField[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(lstField[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strFld != String.Empty)
                        {
                            strFld += ", ";
                        }

                        strFld += lstField[i];

                        if (strVal != String.Empty)
                        {
                            strVal += ", ";
                        }

                        if (prType == typeof(decimal) || prType == typeof(Nullable<decimal>))
                        {
                            if (lstField[i].Trim().EndsWith("CRDT"))
                            {
                                strVal += "CONVERT(VARCHAR(8), GETDATE(), 112)";
                            }
                            else if (lstField[i].Trim().EndsWith("CRTM"))
                            {
                                strVal += "REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','')";
                            }
                            else if (lstField[i].Trim().EndsWith("CHDT"))
                            {
                                strVal += "CONVERT(VARCHAR(8), GETDATE(), 112)";
                            }
                            else if (lstField[i].Trim().EndsWith("CHTM"))
                            {
                                strVal += "REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','')";
                            }
                            else
                            {
                                strVal += (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");
                            }
                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal += (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            strVal += "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }
                    }
                }
            }

            strSql = "INSERT INTO " + strTableName.Trim() + " ( " + strFld + " ) VALUES ( " + strVal + " )";

            return strSql;
        }

        /// <summary>
        /// Generate standard query insert
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strField"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete("GenerateStringInsert with parameter Array is deprecated, please use GenerateStringInsert with parameter List")]
        protected string GenerateStringInsert(string strTableName, string[] strField, object obj)
        {
            //Declare
            string strSql = String.Empty;
            string strFld = String.Empty;
            string strVal = String.Empty;

            //Validation
            if (strField == null)
            {
                throw new Exception("Field is not defined");
            }

            //Action
            for (int i = 0; i < strField.Length; i++)
            {
                if (strField[i] != null && strField[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(strField[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strFld != String.Empty)
                        {
                            strFld += ", ";
                        }

                        strFld += strField[i];

                        if (strVal != String.Empty)
                        {
                            strVal += ", ";
                        }

                        if (prType == typeof(decimal) || prType == typeof(Nullable<decimal>))
                        {
                            if (strField[i].Trim().EndsWith("CRDT"))
                            {
                                strVal += "CONVERT(VARCHAR(8), GETDATE(), 112)";
                            }
                            else if (strField[i].Trim().EndsWith("CRTM"))
                            {
                                strVal += "REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','')";
                            }
                            else if (strField[i].Trim().EndsWith("CHDT"))
                            {
                                strVal += "CONVERT(VARCHAR(8), GETDATE(), 112)";
                            }
                            else if (strField[i].Trim().EndsWith("CHTM"))
                            {
                                strVal += "REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','')";
                            }
                            else
                            {
                                strVal += (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");
                            }
                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal += (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            strVal += "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }
                    }
                }
            }

            strSql = "INSERT INTO " + strTableName.Trim() + " ( " + strFld + " ) VALUES ( " + strVal + " )";

            return strSql;
        }

        /// <summary>
        /// Generate standard query update
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="lstCondition"></param>
        /// <param name="lstField"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GenerateStringUpdate(string strTableName, List<string> lstCondition, List<string> lstField, object obj)
        {
            //Declare
            string strSql = String.Empty;
            string strFld = String.Empty;
            string strVal = String.Empty;
            string strCnd = String.Empty;

            //Validation
            if (lstField == null)
            {
                throw new Exception("Field is not defined");
            }

            if (lstCondition == null)
            {
                throw new Exception("Condition is not defined");
            }

            //Action
            for (int i = 0; i < lstField.Count; i++)
            {
                if (lstField[i] != null && lstField[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(lstField[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strFld != String.Empty)
                        {
                            strFld += ", ";
                        }

                        if (prType == typeof(decimal) || prType == typeof(Nullable<decimal>))
                        {
                            if (lstField[i].Trim().EndsWith("CRDT"))
                            {
                                strVal = "CONVERT(VARCHAR(8), GETDATE(), 112)";
                            }
                            else if (lstField[i].Trim().EndsWith("CRTM"))
                            {
                                strVal = "REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','')";
                            }
                            else if (lstField[i].Trim().EndsWith("CHDT"))
                            {
                                strVal = "CONVERT(VARCHAR(8), GETDATE(), 112)";
                            }
                            else if (lstField[i].Trim().EndsWith("CHTM"))
                            {
                                strVal = "REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','')";
                            }
                            else
                            {
                                strVal = (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");
                            }
                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            strVal = "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }

                        strFld += lstField[i] + " = " + strVal;
                    }
                }
            }

            for (int i = 0; i < lstCondition.Count; i++)
            {
                if (lstCondition[i] != null && lstCondition[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(lstCondition[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strCnd != String.Empty)
                        {
                            strCnd += " AND ";
                        }

                        if (prType == typeof(decimal))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");

                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            strVal = "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }

                        strCnd += lstCondition[i] + " = " + strVal;
                    }
                }
            }

            strSql = "UPDATE " + strTableName.Trim() + " SET " + strFld;
            if (strCnd != String.Empty)
            {
                strSql += " WHERE 1=1 AND " + strCnd;
            }

            return strSql;
        }

        /// <summary>
        /// Generate standard query update
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strCondition"></param>
        /// <param name="strField"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete("GenerateStringUpdate with parameter Array is deprecated, please use GenerateStringUpdate with parameter List")]
        protected string GenerateStringUpdate(string strTableName, string[] strCondition, string[] strField, object obj)
        {
            //Declare
            string strSql = String.Empty;
            string strFld = String.Empty;
            string strVal = String.Empty;
            string strCnd = String.Empty;

            //Validation
            if (strField == null)
            {
                throw new Exception("Field is not defined");
            }

            if (strCondition == null)
            {
                throw new Exception("Condition is not defined");
            }

            //Action
            for (int i = 0; i < strField.Length; i++)
            {
                if (strField[i] != null && strField[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(strField[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strFld != String.Empty)
                        {
                            strFld += ", ";
                        }

                        if (prType == typeof(decimal) || prType == typeof(Nullable<decimal>))
                        {
                            if (strField[i].Trim().EndsWith("CRDT"))
                            {
                                strVal = "CONVERT(VARCHAR(8), GETDATE(), 112)";
                            }
                            else if (strField[i].Trim().EndsWith("CRTM"))
                            {
                                strVal = "REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','')";
                            }
                            else if (strField[i].Trim().EndsWith("CHDT"))
                            {
                                strVal = "CONVERT(VARCHAR(8), GETDATE(), 112)";
                            }
                            else if (strField[i].Trim().EndsWith("CHTM"))
                            {
                                strVal = "REPLACE(CONVERT(VARCHAR(8), GETDATE(), 108),':','')";
                            }
                            else
                            {
                                strVal = (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");
                            }
                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            strVal = "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }

                        strFld += strField[i] + " = " + strVal;
                    }
                }
            }

            for (int i = 0; i < strCondition.Length; i++)
            {
                if (strCondition[i] != null && strCondition[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(strCondition[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strCnd != String.Empty)
                        {
                            strCnd += " AND ";
                        }

                        if (prType == typeof(decimal))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");

                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            strVal = "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }

                        strCnd += strCondition[i] + " = " + strVal;
                    }
                }
            }

            strSql = "UPDATE " + strTableName.Trim() + " SET " + strFld;
            if (strCnd != String.Empty)
            {
                strSql += " WHERE 1=1 AND " + strCnd;
            }

            return strSql;
        }

        /// <summary>
        /// Generate standard query delete
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="lstCondition"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GenerateStringDelete(string strTableName, List<string> lstCondition, object obj)
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
                if (lstCondition[i] != null && lstCondition[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(lstCondition[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strCnd != String.Empty)
                        {
                            strCnd += " AND ";
                        }

                        if (prType == typeof(decimal) || prType == typeof(Nullable<decimal>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");

                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            strVal = "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }

                        strCnd += lstCondition[i] + " = " + strVal;
                    }
                }
            }

            strSql = "DELETE FROM " + strTableName.Trim();
            if (strCnd != String.Empty)
            {
                strSql += " WHERE 1=1 AND " + strCnd;
            }

            return strSql;
        }

        /// <summary>
        /// Generate standard query delete
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strCondition"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete("GenerateStringSelect with parameter Array is deprecated, please use GenerateStringSelect with parameter List")]
        protected string GenerateStringDelete(string strTableName, string[] strCondition, object obj)
        {
            //Declare
            string strSql = String.Empty;
            string strVal = String.Empty;
            string strCnd = String.Empty;

            //Validation
            if (strCondition == null)
            {
                throw new Exception("Condition is not defined");
            }

            //Action
            for (int i = 0; i < strCondition.Length; i++)
            {
                if (strCondition[i] != null && strCondition[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(strCondition[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strCnd != String.Empty)
                        {
                            strCnd += " AND ";
                        }

                        if (prType == typeof(decimal) || prType == typeof(Nullable<decimal>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");

                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            strVal = "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }

                        strCnd += strCondition[i] + " = " + strVal;
                    }
                }
            }

            strSql = "DELETE FROM " + strTableName.Trim();
            if (strCnd != String.Empty)
            {
                strSql += " WHERE 1=1 AND " + strCnd;
            }

            return strSql;
        }

        /// <summary>
        /// Generate standard query select
        /// Don't use this function if you are expecting joining with another table
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="lstCondition"></param>
        /// <param name="lstField"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GenerateStringSelect(string strTableName, List<string> lstCondition, List<string> lstField, object obj)
        {
            //Declare
            string strSql = String.Empty;
            string strFld = String.Empty;
            string strVal = String.Empty;
            string strCnd = String.Empty;
            string strJn = String.Empty;
            string strOrb = String.Empty;

            //Validation
            if (lstField == null)
            {
                throw new Exception("Field is not defined");
            }

            if (lstCondition == null)
            {
                throw new Exception("Condition is not defined");
            }

            //Action
            for (int i = 0; i < lstField.Count; i++)
            {
                if (lstField[i] != null && lstField[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(lstField[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strFld != String.Empty)
                        {
                            strFld += ", ";
                        }

                        strFld += lstField[i];
                    }
                }
            }

            for (int i = 0; i < lstCondition.Count; i++)
            {
                if (lstCondition[i] != null && lstCondition[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(lstCondition[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strCnd != String.Empty)
                        {
                            strCnd += " AND ";
                        }

                        if (prType == typeof(decimal) || prType == typeof(Nullable<decimal>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");

                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            //if (prInfo.GetValue(obj, null) != null && prInfo.GetValue(obj, null) != string.Empty)
                            //{
                            //    strVal = "'" + prInfo.GetValue(obj, null).ToString().Replace("'", "''") + "'";
                            //}

                            strVal = "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }

                        strCnd += lstCondition[i] + " = " + strVal;
                    }
                }
            }

            strSql = "SELECT " + strFld + " FROM " + strTableName.Trim();
            if (strCnd != String.Empty)
            {
                strSql += " WHERE 1=1 AND " + strCnd;
            }

            return strSql;
        }

        /// <summary>
        /// Generate standard query select
        /// Don't use this function if you are expecting joining with another table
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strCondition"></param>
        /// <param name="strField"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete("GenerateStringSelect with Array is deprecated, please use GenerateStringSelect with List")]
        protected string GenerateStringSelect(string strTableName, string[] strCondition, string[] strField, object obj)
        {
            //Declare
            string strSql = String.Empty;
            string strFld = String.Empty;
            string strVal = String.Empty;
            string strCnd = String.Empty;
            string strJn = String.Empty;
            string strOrb = String.Empty;

            //Validation
            if (strField == null)
            {
                throw new Exception("Field is not defined");
            }

            if (strCondition == null)
            {
                throw new Exception("Condition is not defined");
            }

            //Action
            for (int i = 0; i < strField.Length; i++)
            {
                if (strField[i] != null && strField[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(strField[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strFld != String.Empty)
                        {
                            strFld += ", ";
                        }

                        strFld += strField[i];
                    }
                }
            }

            for (int i = 0; i < strCondition.Length; i++)
            {
                if (strCondition[i] != null && strCondition[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(strCondition[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strCnd != String.Empty)
                        {
                            strCnd += " AND ";
                        }

                        if (prType == typeof(decimal) || prType == typeof(Nullable<decimal>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");

                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            //if (prInfo.GetValue(obj, null) != null && prInfo.GetValue(obj, null) != string.Empty)
                            //{
                            //    strVal = "'" + prInfo.GetValue(obj, null).ToString().Replace("'", "''") + "'";
                            //}

                            strVal = "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }

                        strCnd += strCondition[i] + " = " + strVal;
                    }
                }
            }

            strSql = "SELECT " + strFld + " FROM " + strTableName.Trim();
            if (strCnd != String.Empty)
            {
                strSql += " WHERE 1=1 AND " + strCnd;
            }

            return strSql;
        }

        /// <summary>
        /// Generate standard query exists
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="lstCondition"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GenerateStringExists(string strTableName, List<string> lstCondition, object obj)
        {
            //Declare
            string strSql = String.Empty;
            string strVal = String.Empty;
            string strCnd = String.Empty;

            if (lstCondition == null)
            {
                throw new Exception("Condition is not defined");
            }

            //Action
            for (int i = 0; i < lstCondition.Count; i++)
            {
                if (lstCondition[i] != null && lstCondition[i] != String.Empty)
                {
                    Type t = obj.GetType();
                    System.Reflection.PropertyInfo prInfo = t.GetProperty(lstCondition[i]);

                    if (prInfo != null)
                    {
                        Type prType = prInfo.PropertyType;

                        if (strCnd != String.Empty)
                        {
                            strCnd += " AND ";
                        }

                        if (prType == typeof(decimal) || prType == typeof(Nullable<decimal>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace(",", ".") : "0");

                        }
                        else if (prType == typeof(DateTime) || prType == typeof(Nullable<DateTime>))
                        {
                            strVal = (prInfo.GetValue(obj, null) != null ? "'" + Convert.ToDateTime(prInfo.GetValue(obj, null)).ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) + "'" : "null");
                        }
                        else
                        {
                            //if (prInfo.GetValue(obj, null) != null && prInfo.GetValue(obj, null) != string.Empty)
                            //{
                            //    strVal = "'" + prInfo.GetValue(obj, null).ToString().Replace("'", "''") + "'";
                            //}

                            strVal = "'" + (prInfo.GetValue(obj, null) != null ? prInfo.GetValue(obj, null).ToString().Replace("'", "''") : String.Empty) + "'";
                        }

                        strCnd += lstCondition[i] + " = " + strVal;
                    }
                }
            }

            strSql = "SELECT CASE WHEN EXISTS("
            + "\n SELECT 1 FROM " + strTableName.Trim()
            + "\n WHERE 1=1 AND " + strCnd
            + "\n ) THEN 1 ELSE 0 END";

            return strSql;
        }
        #endregion

        #region ExecuteAssembly
        public string ExecuteAssemblyQuery(string strAssemblyFile, List<ParamDto> lstParam)
        {
            try
            {
                string strQuery = string.Empty;
                string strQueryBatch = string.Empty;
                string strResult = string.Empty;

                AssemblyExtender _assex = new AssemblyExtender();
                strQuery = _assex.ReadAssemblyString(strAssemblyFile);

                if (lstParam != null && !string.IsNullOrEmpty(strQuery.Trim()))
                {
                    foreach (ParamDto objParam in lstParam)
                    {
                        strQuery = strQuery.Replace(objParam.Param, objParam.Value);
                    }
                }

                //Search if there is GO in line
                foreach (string line in strQuery.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (line.ToUpperInvariant().Trim() == "GO")
                    {
                        strResult = this.ExecuteDbNonQuery(strQueryBatch);
                        strQueryBatch = string.Empty;
                    }
                    else
                    {
                        strQueryBatch += line + "\n";
                    }
                }

                //If there's no GO than execute query in one batch
                if (!string.IsNullOrEmpty(strQueryBatch))
                {
                    strResult = this.ExecuteDbNonQuery(strQueryBatch);
                }

                return strResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ExecuteAssemblyQueryNonTransaction(string strAssemblyFile, List<ParamDto> lstParam)
        {
            try
            {
                string strQuery = string.Empty;
                string strQueryBatch = string.Empty;
                string strResult = string.Empty;

                AssemblyExtender _assex = new AssemblyExtender();
                strQuery = _assex.ReadAssemblyString(strAssemblyFile);

                if (lstParam != null && !string.IsNullOrEmpty(strQuery.Trim()))
                {
                    foreach (ParamDto objParam in lstParam)
                    {
                        strQuery = strQuery.Replace(objParam.Param, objParam.Value);
                    }
                }

                //Search if there is GO in line
                foreach (string line in strQuery.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (line.ToUpperInvariant().Trim() == "GO")
                    {
                        strResult = this.ExecuteDbNonQueryNonTransaction(strQueryBatch);
                        strQueryBatch = string.Empty;
                    }
                    else
                    {
                        strQueryBatch += line + "\n";
                    }
                }

                //If there's no GO than execute query in one batch
                if (!string.IsNullOrEmpty(strQueryBatch))
                {
                    strResult = this.ExecuteDbNonQueryNonTransaction(strQueryBatch);
                }

                return strResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Methods
        string GenerateSqlServerPagingQuery(string strQuery, string strOrderBy, string strFirstColumn, int intLBound, int intUBound, int intPageSize)
        {
            if (!IsSqlServer2012Above())
            {
                if (strQuery.ToUpper().Contains("TOP 1"))
                {
                    strQuery = strQuery.Replace("TOP 1", String.Empty);
                    strQuery = strQuery.Substring(7);

                    if (!string.IsNullOrEmpty(strOrderBy))
                        strQuery = "SELECT TOP 1 ROW_NUMBER() OVER(ORDER BY " + strOrderBy.Trim() + ") AS REC_NUM, " + strQuery;
                    else
                        strQuery = "SELECT TOP 1 ROW_NUMBER() OVER(ORDER BY " + strFirstColumn + ") AS REC_NUM, " + strQuery;
                }
                else
                {
                    strQuery = strQuery.Substring(7);

                    if (!string.IsNullOrEmpty(strOrderBy))
                        strQuery = "SELECT ROW_NUMBER() OVER (ORDER BY " + strOrderBy.Trim() + ") AS REC_NUM, * FROM (SELECT " + strQuery + ") AS TDISC";
                    else
                        strQuery = "SELECT ROW_NUMBER() OVER (ORDER BY " + strFirstColumn + ") AS REC_NUM, * FROM (SELECT " + strQuery + ") AS TDISC";
                }

                strQuery = "SELECT * FROM (" + strQuery + ") AS TBL " +
                            "WHERE (REC_NUM > " + intLBound.ToString() + " AND REC_NUM <= " + intUBound.ToString() + ")";

                if (!string.IsNullOrEmpty(strOrderBy.Trim()))
                    strQuery += " ORDER BY " + strOrderBy;


            }
            else
            {
                strQuery = strQuery.Substring(7);
                strQuery = "SELECT * FROM (SELECT " + strQuery + ") QPAGING1 ";

                if (!string.IsNullOrEmpty(strOrderBy.Trim()))
                    strQuery += " ORDER BY " + strOrderBy;
                else
                    strQuery += " ORDER BY " + strFirstColumn;

                if (intLBound < 1)
                    intLBound = 1;

                if (intUBound < 1)
                    intUBound = 1;

                strQuery += " OFFSET " + (intLBound - 1).ToString() + " ROWS FETCH NEXT " + intPageSize.ToString() + " ROWS ONLY";
            }
            return strQuery;
        }

        bool IsSqlServer2012Above()
        {
            string strSql2012 = "11.0.2100.60";
            int intSql2012Ver = Convert.ToInt32(strSql2012.Split('.')[0]);

            string strSql = "SELECT SERVERPROPERTY('PRODUCTVERSION') as PRODUCT_VERSION";

            string strProdVersion = ExecuteDbScalar(strSql).ToString();


            if (!string.IsNullOrEmpty(strProdVersion))
            {
                int intProdVer = Convert.ToInt32(strProdVersion.Split('.')[0]);
                if (intProdVer >= intSql2012Ver)
                {
                    return true;
                }
            }
            return false;
        }

        string QueryRegexp(string strQuery)
        {
            //This Regular Expresion is use to change field Condition to UPPER
            string strResult = "";
            int idx = 0;

            //Search word "WHERE 1=1"
            if (Regex.Match(strQuery, "WHERE\\s1=1").Success)
            {
                idx = Regex.Match(strQuery, "WHERE\\s1=1").Index;
                strResult = strQuery.Substring(idx);
                strResult = strResult.ToUpper();
            }

            if (strResult != "")
            {
                if (MainProviderName == "System.Data.SqlClient" || MainProviderType == "MSSQL")
                {
                    if (Regex.Match(strResult, "\\'\\'\\)").Success)
                    {
                        strResult = strResult.Replace("'')", "'')");
                    }

                    if (Regex.Match(strResult, "\\'\\'\\s\\)").Success)
                    {
                        strResult = strResult.Replace("'' )", "'' )");
                    }
                }
                else
                {
                    if (Regex.Match(strResult, "\\'\\'\\)").Success)
                    {
                        strResult = strResult.Replace("'')", "' ')");
                    }

                    if (Regex.Match(strResult, "\\'\\'\\s\\)").Success)
                    {
                        strResult = strResult.Replace("'' )", "' ' )");
                    }
                }

                //Search with spaced line before =
                if (Regex.Match(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\s\\=\\s\\'").Success)
                {
                    if (MainProviderName == "System.Data.SqlClient" || MainProviderType == "MSSQL")
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\s\\=\\s\\'", "UPPER(ISNULL($&,''))");
                        strResult = strResult.Replace(" = ',''))", ",'')) = '");
                    }
                    else
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\s\\=\\s\\'", "UPPER($&)");
                        strResult = strResult.Replace(" = ')", ") = '");
                    }
                }

                //Search with spaced line before =
                if (Regex.Match(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\s\\=\\'").Success)
                {
                    if (MainProviderName == "System.Data.SqlClient" || MainProviderType == "MSSQL")
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\s\\=\\'", "UPPER(ISNULL($&,''))");
                        strResult = strResult.Replace(" =',''))", ",'')) ='");
                    }
                    else
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\s\\=\\'", "UPPER($&)");
                        strResult = strResult.Replace(" =')", ") ='");
                    }
                }

                //Search with no spaced line before =
                if (Regex.Match(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\=\\s\\'").Success)
                {
                    if (MainProviderName == "System.Data.SqlClient" || MainProviderType == "MSSQL")
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\=\\s\\'", "UPPER(ISNULL($&,''))");
                        strResult = strResult.Replace("= ',''))", ",''))= '");
                    }
                    else
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\=\\s\\'", "UPPER($&)");
                        strResult = strResult.Replace("= ')", ")= '");
                    }
                }

                //Search with no spaced line before =
                if (Regex.Match(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\=\\'").Success)
                {
                    if (MainProviderName == "System.Data.SqlClient" || MainProviderType == "MSSQL")
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\=\\'", "UPPER(ISNULL($&,''))");
                        strResult = strResult.Replace("=',''))", ",''))='");
                    }
                    else
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\=\\'", "UPPER($&)");
                        strResult = strResult.Replace("=')", ")='");
                    }
                }

                //Search with spaced line before LIKE
                if (Regex.Match(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\sLIKE").Success)
                {
                    if (MainProviderName == "System.Data.SqlClient" || MainProviderType == "MSSQL")
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\sLIKE", "UPPER(ISNULL($&,''))");
                        strResult = strResult.Replace(" LIKE,''))", ",'')) LIKE");
                    }
                    else
                    {
                        strResult = Regex.Replace(strResult, "(\\w*\\.\\w{6,8}|\\w{6,8})\\sLIKE", "UPPER($&)");
                        strResult = strResult.Replace(" LIKE)", ") LIKE");
                    }
                }

                strQuery = strQuery.Substring(0, idx) + strResult;
            }

            return strQuery;
        }
        #endregion

        #region Property

        public DbProviderFactory MainFactory
        {
            get
            {
                if (this.m_factory == null)
                    this.m_factory = DbProviderFactories.GetFactory(MainProviderName);

                return this.m_factory;
            }
        }

        public DataSource MainDataSource
        {
            get
            {
                return this.m_dataSource;
            }
            set
            {
                this.m_dataSource = value;
            }
        }

        public string MainConnString
        {
            get
            {
                switch (MainDataSource)
                {
                    case DataSource.University:
                        {
                            this.m_connString = Config.UniversityConnString;
                            break;
                        }
                }
                return m_connString;
            }
        }

        public string MainProviderName
        {
            get
            {
                switch (MainDataSource)
                {
                    case DataSource.University:
                        {
                            this.m_providerName = Config.UniversityProvider;
                            break;
                        }
                }
                return m_providerName;
            }
        }

        public string MainProviderType
        {
            get
            {
                switch (MainDataSource)
                {
                    case DataSource.University:
                        {
                            this.m_providerType = Config.UniversityProviderType;
                            break;
                        }
                }
                return m_providerType;
            }
        }

        public DbConnection MainConnection
        {
            get
            {
                this.m_factory = DbProviderFactories.GetFactory(MainProviderName);
                return this.m_factory.CreateConnection();
            }
        }

        public DbTransaction MainTransaction
        {
            get
            {
                return this.m_factoryTrans;
            }
            set
            {
                this.m_factoryTrans = value;
            }
        }

        public DbCommand MainCommand
        {
            get
            {
                return this.m_factory.CreateCommand();
            }
        }

        public DbDataAdapter MainAdapter
        {
            get
            {
                return this.m_factory.CreateDataAdapter();
            }
        }

        public int MainTimeout
        {
            get
            {
                return 0;
            }
        }

        #endregion
    }
}
