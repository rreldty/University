using System;
using System.Data;
using System.Collections.Generic;

using University.Dao.Base;
using University.Dto.Training;
using University.Dto.Base;

namespace University.Dao.Training
{
    public class NilaiDetailDao : BaseDao<NilaiDetailDto>
    {
        #region Constructor

        public NilaiDetailDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<NilaiDetailDto> GetMapper()
        {
            return new NilaiDetailMappingDto();
        }

        #endregion

        #region Save Data

        public string ScriptInsert(NilaiDetailDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("kode_matakuliah");
            lstField.Add("sks");
            lstField.Add("skor");

            return this.GenerateStringInsert("NilaiDetail", lstField, obj);
        }

        public string ScriptUpdate(NilaiDetailDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("sks");
            lstField.Add("skor");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");
            lstCondition.Add("kode_matakuliah");

            return this.GenerateStringUpdate("NilaiDetail", lstCondition, lstField, obj);
        }

        public string Save(NilaiDetailDto obj)
        {
            string strResult = string.Empty;

            if (!IsExists(obj))
            {
                strResult = this.ExecuteDbNonQuery(ScriptInsert(obj));
            }
            else
            {
                strResult = this.ExecuteDbNonQuery(ScriptUpdate(obj));
            }

            return strResult;
        }

        #endregion

        #region Delete Data

        public string ScriptDelete(NilaiDetailDto obj)
        {
            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");
            lstCondition.Add("kode_matakuliah");

            return this.GenerateStringDelete("NilaiDetail", lstCondition, obj);
        }

        public string Delete(NilaiDetailDto obj)
        {
            string strSql = ScriptDelete(obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        public string DeleteByNimSemester(string nim, string semester)
        {
            string strSql = "DELETE FROM NilaiDetail WHERE nim = '" + nim.Trim() + "' AND semester = '" + semester.Trim() + "'";
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        public bool IsExists(NilaiDetailDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS ( SELECT * FROM NilaiDetail WHERE nim = '" + obj.nim.Trim() + "' AND semester = '" + obj.semester.Trim() + "' AND kode_matakuliah = '" + obj.kode_matakuliah.Trim() + "' ) THEN 1 ELSE 0 END";
            Object _obj = this.ExecuteDbScalar(strSql);
            if (_obj == DBNull.Value) return false;
            return Convert.ToInt32(_obj) != 0;
        }

        public NilaiDetailDto Get(NilaiDetailDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("kode_matakuliah");
            lstField.Add("sks");
            lstField.Add("skor");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");
            lstCondition.Add("kode_matakuliah");

            string strSql = this.GenerateStringSelect("NilaiDetail", lstCondition, lstField, obj);
            NilaiDetailDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<NilaiDetailDto> GetList(NilaiDetailDto obj)
        {
            string strSql = "SELECT "
                            + " nim  "
                            + ", semester  "
                            + ", kode_matakuliah  "
                            + ", sks  "
                            + ", skor  "
                            + " FROM NilaiDetail WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.nim))
            {
                strSql += " AND nim = '" + obj.nim.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.semester))
            {
                strSql += " AND semester = '" + obj.semester.Trim() + "' ";
            }

            strSql += " ORDER BY kode_matakuliah ";

            List<NilaiDetailDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<NilaiDetailDto> GetListPaging(NilaiDetailDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            if (intPageNumber < 1) intPageNumber = 1;
            if (intPageSize < 1) intPageSize = 20;

            string strSql = @"
                SELECT
                    a.nim
                    , a.semester
                    , a.kode_matakuliah
                    , ISNULL(m.nama_matakuliah, '') AS nama_matakuliah
                    , a.sks
                    , a.skor
                FROM NilaiDetail a
                LEFT JOIN MataKuliah m ON a.kode_matakuliah = m.kode_matakuliah
                WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.nim))
            {
                strSql += " AND a.nim = '" + obj.nim.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.semester))
            {
                strSql += " AND a.semester = '" + obj.semester.Trim() + "' ";
            }

            List<NilaiDetailDto> dto = this.ExecutePaging(strSql, "kode_matakuliah ASC", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        #endregion
    }
}
