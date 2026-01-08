using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AnalysisServices.AdomdClient;

using University.Dto.Base;
using System.IO;

namespace University.Dao.Base
{
    public class AnalysisServiceDao
    {
        public DataTable ExecutePathDataTable(List<ParamDto> lstParam, string strPath)
        {
            string strResult = string.Empty;
            DataTable dtReturn = null;

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

            using (AdomdConnection con = new AdomdConnection())
            {
                con.ConnectionString = Config.SSASConnString;
                using (AdomdCommand cmd = new AdomdCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = strQuery;

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        AdomdDataAdapter da = new AdomdDataAdapter();
                        da.SelectCommand = cmd;
                        dtReturn = new DataTable();
                        da.Fill(dtReturn);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
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

        public DataTable ExecuteAssemblyDataTable(List<ParamDto> lstParam, string strAssemblyFile)
        {
            string strResult = string.Empty;
            DataTable dtReturn = null;

            string strQuery = string.Empty;

            AssemblyExtender _assex = new AssemblyExtender();
            strQuery = _assex.ReadAssemblyString(strAssemblyFile);

            if (lstParam != null && !string.IsNullOrEmpty(strQuery.Trim()))
            {
                foreach (ParamDto objParam in lstParam)
                {
                    strQuery = strQuery.Replace(objParam.Param, objParam.Value);
                }
            }

            using (AdomdConnection con = new AdomdConnection())
            {
                con.ConnectionString = Config.SSASConnString;
                using (AdomdCommand cmd = new AdomdCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = strQuery;

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        AdomdDataAdapter da = new AdomdDataAdapter();
                        da.SelectCommand = cmd;
                        dtReturn = new DataTable();
                        da.Fill(dtReturn);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
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
            string strResult = string.Empty;
            DataTable dtReturn = null;

            using (AdomdConnection con = new AdomdConnection())
            {
                con.ConnectionString = Config.SSASConnString;
                using (AdomdCommand cmd = new AdomdCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = strQuery;

                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        AdomdDataAdapter da = new AdomdDataAdapter();
                        da.SelectCommand = cmd;
                        dtReturn = new DataTable();
                        da.Fill(dtReturn);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
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
    }
}
