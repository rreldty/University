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
    public class MataKuliahDao : BaseDao<MataKuliahDto>
    {
        #region Constructor

        public MataKuliahDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<MataKuliahDto> GetMapper()
        {
            Mapper<MataKuliahDto> mapDto = new MataKuliahMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(MataKuliahDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("kode_matakuliah");
            lstField.Add("nama_matakuliah");
            lstField.Add("sks");
            lstField.Add("record_status");

            return this.GenerateStringInsert("MataKuliah", lstField, obj);
        }

        public string ScriptUpdate(MataKuliahDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nama_matakuliah");
            lstField.Add("sks");
            lstField.Add("record_status");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("kode_fakultas");
            lstCondition.Add("kode_jurusan");
            lstCondition.Add("kode_matakuliah");

            return this.GenerateStringUpdate("MataKuliah", lstCondition, lstField, obj);
        }

        public string Save(MataKuliahDto obj)
        {
            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data

        public string Delete(MataKuliahDto obj)
        {
            List<string> lstCondition = new List<string>();
            lstCondition.Add("kode_fakultas");
            lstCondition.Add("kode_jurusan");
            lstCondition.Add("kode_matakuliah");

            string strSql = this.GenerateStringDelete("MataKuliah", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        public bool IsExists(MataKuliahDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM MataKuliah "
                            + " WHERE 1=1 "
                            + " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "'"
                            + " AND kode_jurusan = '" + obj.kode_jurusan.Trim() + "'"
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

        public MataKuliahDto Get(MataKuliahDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("kode_matakuliah");
            lstField.Add("nama_matakuliah");
            lstField.Add("sks");
            lstField.Add("record_status");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("kode_fakultas");
            lstCondition.Add("kode_jurusan");
            lstCondition.Add("kode_matakuliah");

            string strSql = this.GenerateStringSelect("MataKuliah", lstCondition, lstField, obj);
            MataKuliahDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<MataKuliahDto> GetList(MataKuliahDto obj)
        {
            string strSql = "SELECT "
                            + " kode_fakultas  "
                            + ", kode_jurusan  "
                            + ", kode_matakuliah  "
                            + ", nama_matakuliah  "
                            + ", sks  "
                            + ", record_status  "
                            + " FROM MataKuliah WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.kode_fakultas))
            {
                strSql += " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_jurusan))
            {
                strSql += " AND kode_jurusan = '" + obj.kode_jurusan.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_matakuliah))
            {
                strSql += " AND kode_matakuliah = '" + obj.kode_matakuliah.Trim() + "' ";
            }

            List<MataKuliahDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<MataKuliahDto> GetListPaging(MataKuliahDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                            + " kode_fakultas  "
                            + ", kode_jurusan  "
                            + ", kode_matakuliah  "
                            + ", nama_matakuliah  "
                            + ", sks  "
                            + ", record_status  "
                            + " FROM MataKuliah WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.kode_fakultas))
            {
                strSql += " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_jurusan))
            {
                strSql += " AND kode_jurusan = '" + obj.kode_jurusan.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_matakuliah))
            {
                strSql += " AND kode_matakuliah = '" + obj.kode_matakuliah.Trim() + "' ";
            }

            List<MataKuliahDto> dto = this.ExecutePaging(strSql, "kode_matakuliah", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public DataTable GetDetailMataKuliah(MataKuliahDto obj)
        {
            string strSql = "SELECT "
                            + "  A.kode_matakuliah AS [Kode_Mata_Kuliah] "
                            + " , A.nama_matakuliah AS [Nama_Mata_Kuliah] "
                            + " , A.sks AS [SKS] "
                            + " , C.nama_jurusan AS [Nama_Jurusan] "
                            + " , B.nama_common AS [Record_Status] "
                            + "  FROM MataKuliah A "
                            + "  LEFT JOIN common B ON 1 = 1 "
                            + " AND B.tipe_common = 'Record_Status' "
                            + " AND B.nilai_common = A.record_status "
                            + "  LEFT JOIN Jurusan C ON 1 = 1 "
                            + " AND C.kode_fakultas = A.kode_fakultas "
                            + " AND C.kode_jurusan = A.kode_jurusan "
                            + " WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(obj.kode_fakultas))
            {
                strSql += " AND A.kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_jurusan))
            {
                strSql += " AND A.kode_jurusan = '" + obj.kode_jurusan.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_matakuliah))
            {
                strSql += " AND A.kode_matakuliah = '" + obj.kode_matakuliah.Trim() + "' ";
            }

            DataTable dto = this.ExecuteDataTable(strSql);
            return dto;
        }
        #endregion
    }
}
