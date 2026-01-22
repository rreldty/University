using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dao.Base;
using University.Dto.Base;
using System.Data;
using University.Dto.Training;

namespace University.Dao.Training
{
    public class KrsDetailDao : BaseDao<KrsDetailDto>
    {
        #region Constructor

        public KrsDetailDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<KrsDetailDto> GetMapper()
        {
            Mapper<KrsDetailDto> mapDto = new KrsDetailMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(KrsDetailDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("line_no");
            lstField.Add("kode_matakuliah");
            lstField.Add("sks");

            return this.GenerateStringInsert("KrsDetail", lstField, obj);
        }

        public string ScriptUpdate(KrsDetailDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_matakuliah");
            lstField.Add("sks");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");
            lstCondition.Add("line_no");

            return this.GenerateStringUpdate("KrsDetail", lstCondition, lstField, obj);
        }

        public string Save(KrsDetailDto obj)
        {
            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data
        public string DeleteList(List<KrsDetailDto> lst)
        {
            string strResult = string.Empty;
            List<string> lstSql = new List<string>();

            foreach (KrsDetailDto obj in lst)
            {

                List<string> lstCondition = new List<string>();
                lstCondition.Add("nim");
                lstCondition.Add("semester");
                lstCondition.Add("line_no");

                lstSql.Add(GenerateStringDelete("KrsDetail", lstCondition, obj));
            }

            strResult = ExecuteDbNonQueryTransaction(lstSql);

            if (strResult == string.Empty)
            {
                KrsHeaderDao daokrsheader = new KrsHeaderDao();
                strResult = daokrsheader.Calculate(new KrsHeaderDto()
                {
                    nim = lst[0].nim,
                    semester = lst[0].semester,
                });
            }
            return strResult;
        }

        public string Delete(KrsDetailDto obj)
        {
            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");
            lstCondition.Add("line_no");

            string strSql = this.GenerateStringDelete("KrsDetail", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        public bool IsExists(KrsDetailDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM KrsDetail "
                            + " WHERE 1=1 "
                            + " AND nim   = '" + obj.nim.Trim() + "'"
                            + " AND semester   = " + obj.semester + ""
                            + " AND line_no   = " + obj.line_no + ""
                            + " )"
                            + " THEN 1 ELSE 0 END"
                            + "";

            Object _obj = this.ExecuteDbScalar(strSql);

            if (_obj == DBNull.Value)
            {
                return false;
            }
            else
            {
                if (Convert.ToInt32(_obj) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public decimal getLineMax(KrsDetailDto obj)
        {
            string strSql = " SELECT MAX(line_no) "
                            + " FROM KrsDetail "
                            + " WHERE 1=1 "
                            + " AND nim   = '" + obj.nim.Trim() + "'"
                            + " AND semester   = " + obj.semester + ""
                            + "";

            Object _obj = this.ExecuteDbScalar(strSql);

            if (_obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                if (Convert.ToInt32(_obj) == 0)
                {
                    return 0;
                }
            }

            return Convert.ToDecimal(_obj);
        }

        public KrsDetailDto Get(KrsDetailDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("line_no");
            lstField.Add("kode_matakuliah");
            lstField.Add("sks");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");
            lstCondition.Add("line_no");

            string strSql = this.GenerateStringSelect("KrsDetail", lstCondition, lstField, obj);
            KrsDetailDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<KrsDetailDto> GetList(KrsDetailDto obj)
        {
            string strSql = "SELECT "
                        + "    nim  "
                        + ",    semester  "
                        + ",    line_no  "
                        + ",    kode_matakuliah  "
                        + ",    sks  "
                        + " FROM KrsDetail "
                        + " WHERE 1=1 ";

            if (obj.nim != null && obj.nim != String.Empty)
            {
                strSql += " AND nim   = '" + obj.nim.Trim() + "'";
            }

            if (obj.semester > 0)
            {
                strSql += " AND semester = " + obj.semester + "";
            }



            List<KrsDetailDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<KrsDetailDto> GetListPaging(KrsDetailDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                        + "   nim  "
                        + ",    semester  "
                        + ",    line_no  "
                        + ",    kode_matakuliah  "
                        + ",    sks  "
                        + " FROM KrsDetail "
                        + " WHERE 1=1 ";

            if (obj.nim != null && obj.nim != String.Empty)
            {
                strSql += " AND nim   = '" + obj.nim.Trim() + "'";
            }

            if (obj.semester > 0)
            {
                strSql += " AND semester = " + obj.semester + "";
            }

            if (obj.line_no > 0)
            {
                strSql += " AND line_no = " + obj.line_no + "";
            }

            List<KrsDetailDto> dto = this.ExecutePaging(strSql, "nim, semester , line_no", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public List<KrsDetailDto> GetListMataKuliah(KrsDetailDto obj)
        {
            string strSql = "SELECT "
                         + "   A.NIM AS [nim],  "
                         + "   A.Semester AS [semester],  "
                         + "   A.Line_No AS line_no,  "
                         + "   A.kode_matakuliah AS [kode_matakuliah],  "
                         + "   B.nama_matakuliah AS [nama_matakuliah],  "
                         + "   A.SKS AS [sks]  "
                         + " FROM   KRSDetail A  "
                         + "   JOIN matakuliah B on 1 = 1  "
                         + "   AND B.Kode_Jurusan = '" + obj.kode_jurusan + "'  "
                         + "   AND A.kode_matakuliah = B.kode_matakuliah "
                         + " WHERE 1 = 1  ";

            if (obj.nim != null && obj.nim != String.Empty)
            {
                strSql += " AND nim   = '" + obj.nim.Trim() + "'";
            }

            if (obj.semester > 0)
            {
                strSql += " AND semester = " + obj.semester + "";
            }

            List<KrsDetailDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }


        public DataTable GetDetailKrsDetail(KrsDetailDto obj)
        {
            string strSql = "SELECT "
                            + "    nim  "
                        + ",    semester  "
                        + ",    kode_fakultas  "
                        + ",    kode_jurusan  "
                        + ",    total_sks  "
                        + " FROM KrsDetail "
                        + " WHERE 1=1 ";

            if (obj.nim != null && obj.nim != String.Empty)
            {
                strSql += " AND nim   = '" + obj.nim.Trim() + "'";
            }

            if (obj.semester > 0)
            {
                strSql += " AND semester = " + obj.semester + "";
            }

            DataTable dto = this.ExecuteDataTable(strSql);
            return dto;
        }
        #endregion
    }
}