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
    public class KRSDetailDao : BaseDao<KRSDetailDto>
    {
        public int GetNextLineNo(string nim, string semester)
        {
            string sqlMax = $"SELECT ISNULL(MAX(line_no),0) FROM KRSDetail WHERE nim = '{nim}' AND semester = '{semester}'";
            object maxLine = this.ExecuteDbScalar(sqlMax);
            int nextLine = 1;
            if (maxLine != null && maxLine != DBNull.Value)
                nextLine = Convert.ToInt32(maxLine) + 1;
            return nextLine;
        }
        #region Constructor

        public KRSDetailDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<KRSDetailDto> GetMapper()
        {
            Mapper<KRSDetailDto> mapDto = new KRSDetailMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(KRSDetailDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("kode_matakuliah");
            lstField.Add("sks");
            lstField.Add("line_no");

            return this.GenerateStringInsert("KRSDetail", lstField, obj);
        }

        public string ScriptUpdate(KRSDetailDto obj)
        {
    		List<string> lstField = new List<string>();
    		lstField.Add("sks");
    		lstField.Add("line_no");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");
            lstCondition.Add("kode_matakuliah");

            return this.GenerateStringUpdate("KRSDetail", lstCondition, lstField, obj);
        }

        public string Save(KRSDetailDto obj)
        {
            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data

        public string Delete(KRSDetailDto obj)
        {
            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");
            lstCondition.Add("kode_matakuliah");

            string strSql = this.GenerateStringDelete("KRSDetail", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        public string DeleteByNimSemester(string nim, string semester)
        {
            string strSql = "DELETE FROM KRSDetail WHERE nim = '" + nim.Trim() + "' AND semester = '" + semester.Trim() + "'";
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        public bool IsExists(KRSDetailDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM KRSDetail "
                            + " WHERE 1=1 "
                            + " AND nim = '" + obj.nim.Trim() + "'"
                            + " AND semester = '" + obj.semester.Trim() + "'"
                            + " AND kode_matakuliah = '" + obj.kode_matakuliah.Trim() + "'"
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

        public KRSDetailDto Get(KRSDetailDto obj)
        {
            string strSql = @"SELECT 
                A.nim,
                A.semester,
                A.kode_matakuliah,
                B.nama_matakuliah,
                A.sks,
                A.line_no
            FROM KRSDetail A
            LEFT JOIN MataKuliah B ON A.kode_matakuliah = B.kode_matakuliah
            WHERE A.nim = '" + obj.nim.Trim() + "' AND A.semester = '" + obj.semester.Trim() + "' AND A.kode_matakuliah = '" + obj.kode_matakuliah.Trim() + "'";
            KRSDetailDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<KRSDetailDto> GetList(KRSDetailDto obj)
        {
            string strSql = @"SELECT 
                A.nim,
                A.semester,
                A.kode_matakuliah,
                B.nama_matakuliah,
                A.sks,
                A.line_no
            FROM KRSDetail A
            LEFT JOIN MataKuliah B ON A.kode_matakuliah = B.kode_matakuliah
            WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.nim))
            {
                strSql += " AND A.nim = '" + obj.nim.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.semester))
            {
                strSql += " AND A.semester = '" + obj.semester.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_matakuliah))
            {
                strSql += " AND A.kode_matakuliah = '" + obj.kode_matakuliah.Trim() + "' ";
            }

            strSql += " ORDER BY A.line_no ";

            List<KRSDetailDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<KRSDetailDto> GetListMataKuliah(KRSDetailDto obj)
        {
            string strSql = @"
                SELECT 
                    A.nim AS [nim]
                    , A.semester AS [semester]
                    , A.kode_matakuliah AS [kode_matakuliah]
                    , C.nama_matakuliah AS [nama_matakuliah]
                    , A.sks AS [sks]
                FROM KRSDetail A
                JOIN KRSHeader B ON 1=1
                    AND B.nim = A.nim
                    AND B.semester = A.semester
                JOIN MataKuliah C ON 1=1
                    AND C.kode_fakultas = B.kode_fakultas
                    AND C.kode_jurusan = B.kode_jurusan
                    AND C.kode_matakuliah = A.kode_matakuliah
                WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.nim))
            {
                strSql += " AND A.nim = '" + obj.nim.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.semester))
            {
                strSql += " AND A.semester = '" + obj.semester.Trim() + "' ";
            }

            List<KRSDetailDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<KRSDetailDto> GetListPaging(KRSDetailDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = @"SELECT 
                A.nim,
                A.semester,
                A.kode_matakuliah,
                B.nama_matakuliah,
                A.sks,
                A.line_no
            FROM KRSDetail A
            LEFT JOIN MataKuliah B ON A.kode_matakuliah = B.kode_matakuliah
            WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.nim))
            {
                strSql += " AND A.nim = '" + obj.nim.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.semester))
            {
                strSql += " AND A.semester = '" + obj.semester.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_matakuliah))
            {
                strSql += " AND A.kode_matakuliah = '" + obj.kode_matakuliah.Trim() + "' ";
            }

            List<KRSDetailDto> dto = this.ExecutePaging(strSql, "A.line_no", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        #endregion
    }
}