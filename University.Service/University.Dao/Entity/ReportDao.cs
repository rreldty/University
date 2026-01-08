using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using University.Dto.Base;
using University.Dao.Base;
using University.Dao.Zystem;
using University.Dto.Zystem;
using System.Web;
using University.Training;
using University.Dao.Training;
using University.Dto.Training;

namespace University.Dao.Entity
{
    public class ReportDao : BaseDao<Object>
    {
        #region Constructor

        public ReportDao()
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

        #region Get Data

        public DataSet GetDataSetEntity(string strEntity, HttpRequest Request, out string strPathRdlc)
        {
            DataSet dtsData = new DataSet("dtsReport");

            string strSql = string.Empty;
            strPathRdlc = string.Empty;

            switch (strEntity.ToUpper())
            {
                #region F
                case "FAKULTAS":
                    {
                        strPathRdlc = "Fakultas";

                        //Get Header
                        string strKode_Fakultas = Request["Kode_Fakultas"] ?? string.Empty;
                        string strNama_Fakultas = Request["Nama_Fakultas"] ?? string.Empty;
                        string strUser = Request["User"] ?? string.Empty;

                        string strQueryHeader = "SELECT "
                                        + " '" + strKode_Fakultas + " AS Kode_Fakultas"
                                        + ", '" + strNama_Fakultas + " AS Nama_Fakultas"
                                        + ", '" + strUser + "' as User "
                                        + " WHERE 1=1";
                       
                        DataTable dtHeader = this.ExecuteDataTable(strQueryHeader);
                        dtHeader.TableName = "dsHeader";
                        dtsData.Tables.Add(dtHeader);

                        //Get Detail
                        FakultasDto objInfo = new FakultasDto();
                        objInfo.kode_fakultas = strKode_Fakultas;
                        objInfo.nama_fakultas = strNama_Fakultas;
                        //objInfo.IsDownload = false;

                        FakultasDao dao = new FakultasDao();
                        DataTable dtDetail = dao.GetDetailFakultas(objInfo);
                        dtDetail.TableName = "dsDetail";
                        dtDetail.AcceptChanges();

                        dtsData.Tables.Add(dtDetail);

                        break;
                    }

                #endregion

                #region M
                case "PM030A":
                    {
                        strPathRdlc = "MataKuliah";

                        //Get Parameters
                        string strFakultas = Request["fakultas"] ?? string.Empty;
                        string strJurusan = Request["jurusan"] ?? string.Empty;

                        //Get Header - Get actual names from database
                        string strQueryHeader = "SELECT "
                                        + " CASE WHEN '" + strFakultas + "' = '' THEN 'ALL' ELSE ISNULL((SELECT TOP 1 nama_fakultas FROM Fakultas WHERE kode_fakultas = '" + strFakultas + "'), 'ALL') END AS Nama_FakultasFr"
                                        + ", CASE WHEN '" + strJurusan + "' = '' THEN 'ALL' ELSE ISNULL((SELECT TOP 1 nama_jurusan FROM Jurusan WHERE kode_jurusan = '" + strJurusan + "' AND kode_fakultas = '" + strFakultas + "'), 'ALL') END AS Nama_JurusanFr";

                        DataTable dtHeader = this.ExecuteDataTable(strQueryHeader);
                        dtHeader.TableName = "dsHeader";
                        dtsData.Tables.Add(dtHeader);

                        //Get Detail
                        MataKuliahDto objInfo = new MataKuliahDto();
                        objInfo.kode_fakultas = strFakultas;
                        objInfo.kode_jurusan = strJurusan;

                        MataKuliahDao dao = new MataKuliahDao();
                        DataTable dtDetail = dao.GetDetailMataKuliah(objInfo);
                        dtDetail.TableName = "dsDetail";
                        dtDetail.AcceptChanges();

                        dtsData.Tables.Add(dtDetail);

                        break;
                    }
                #endregion

                #region Z
                case "ZR010A":
                    {
                        strPathRdlc = "XM011A_ListOfApplication";

                        //Get Header
                        string strZAAPNOFr = Request["ZAAPNOFr"] ?? string.Empty;
                        string strZAAPNOTo = Request["ZAAPNOTo"] ?? string.Empty;
                        string strUSNA = Request["usna"] ?? string.Empty;

                        string strQueryHeader = "SELECT ZCCONA, ZBBRNA, ZBADR1, ZBADR2, ZBPHN1, ZBFAX1"
                                        + ", '" + strZAAPNOFr + " AS ZAAPNOFr"
                                        + ", '" + strZAAPNOTo + " AS ZAAPNOTo"
                                        + ", '" + strUSNA + "' as ZUUSNA"
                                        + " FROM ZCMP"
                                        + " LEFT JOIN ZBRC ON 1=1"
                                        + "     AND ZBCONO = ZCCONO"
                                        + " WHERE 1=1";
                                        //+ "     AND ZCCONO = '" + GlobalDto.CONO + "'"
                                        //+ "     AND ZBBRNO = '" + GlobalDto.BRNO + "'";

                        DataTable dtHeader = this.ExecuteDataTable(strQueryHeader);
                        dtHeader.TableName = "dsHeader";
                        dtsData.Tables.Add(dtHeader);

                        //Get Detail
                        ZAPPDto objInfo = new ZAPPDto();
                        objInfo.ZAAPNOFr = strZAAPNOFr;
                        objInfo.ZAAPNOTo = strZAAPNOTo;
                        //objInfo.IsDownload = false;

                        ZAPPDao dao = new ZAPPDao();
                        DataTable dtDetail = dao.GetDetailZR010A(objInfo);
                        dtDetail.TableName = "dsDetail";
                        dtDetail.AcceptChanges();

                        dtsData.Tables.Add(dtDetail);

                        break;
                    }


                #endregion

                default:
                    break;
            }

            return dtsData;
        }

        public DataTable GetDataTable(string strQuery)
        {
            DataTable dt = new DataTable("tblReport");
            DataTable dtQuery = this.ExecuteDataTable(strQuery);
            
            if (dtQuery != null)
            {
                dt.Merge(dtQuery);
            }

            return dt;
        }

        public DataSet GetDataSet(string strQuery)
        {
            DataSet dts = new DataSet("dtsReport");
            DataSet dtsQuery = this.ExecuteDataSet(strQuery);

            if (dtsQuery != null)
            {
                dts.Merge(dtsQuery);
            }

            return dts;
        }
        #endregion

    }
}