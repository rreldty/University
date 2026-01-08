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
    public class JurusanDao : BaseDao<JurusanDto>
    {
        #region Constructor

        public JurusanDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<JurusanDto> GetMapper()
        {
            Mapper<JurusanDto> mapDto = new JurusanMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(JurusanDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("nama_jurusan");
            lstField.Add("record_status");

            return this.GenerateStringInsert("Jurusan", lstField, obj);
        }

        public string ScriptUpdate(JurusanDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nama_jurusan");
            lstField.Add("record_status");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("kode_fakultas");
            lstCondition.Add("kode_jurusan");

            return this.GenerateStringUpdate("Jurusan", lstCondition, lstField, obj);
        }

        public string Save(JurusanDto obj)
        {
            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data

        public string Delete(JurusanDto obj)
        {
            List<string> lstCondition = new List<string>();
            lstCondition.Add("kode_fakultas");
            lstCondition.Add("kode_jurusan");

            string strSql = this.GenerateStringDelete("Jurusan", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        public bool IsExists(JurusanDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM Jurusan "
                            + " WHERE 1=1 "
                            + " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "'"
                            + " AND kode_jurusan = '" + obj.kode_jurusan.Trim() + "'"
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

        public JurusanDto Get(JurusanDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("nama_jurusan");
            lstField.Add("record_status");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("kode_fakultas");
            lstCondition.Add("kode_jurusan");

            string strSql = this.GenerateStringSelect("Jurusan", lstCondition, lstField, obj);
            JurusanDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<JurusanDto> GetList(JurusanDto obj)
        {
            string strSql = "SELECT "
                            + " kode_fakultas  "
                            + ", kode_jurusan  "
                            + ", nama_jurusan  "
                            + ", record_status  "
                            + " FROM Jurusan WHERE 1=1 ";

            if (obj.kode_fakultas != null && obj.kode_fakultas != String.Empty)
            {
                strSql += " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            if (obj.kode_jurusan != null && obj.kode_jurusan != String.Empty)
            {
                strSql += " AND kode_jurusan = '" + obj.kode_jurusan.Trim() + "' ";
            }

            List<JurusanDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<JurusanDto> GetListPaging(JurusanDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                            + " kode_fakultas  "
                            + ", kode_jurusan  "
                            + ", nama_jurusan  "
                            + ", record_status  "
                            + " FROM Jurusan WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.kode_fakultas))
            {
                strSql += " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_jurusan))
            {
                strSql += " AND kode_jurusan = '" + obj.kode_jurusan.Trim() + "' ";
            }

            List<JurusanDto> dto = this.ExecutePaging(strSql, "kode_jurusan", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public DataTable GetDetailJurusan(JurusanDto obj)
        {
            string strSql = "SELECT "
                            + "  A.kode_fakultas AS [Kode Fakultas] "
                            + " , A.kode_jurusan AS [Kode Jurusan] "
                            + " , A.nama_jurusan AS [Nama Jurusan] "
                            + " , B.nama_common AS [Record Status] "
                            + "  FROM Jurusan A "
                            + "  LEFT JOIN common B ON 1 = 1 "
                            + " AND B.tipe_common = 'record_status' "
                            + " AND B.nilai_common = A.record_status "
                            + " WHERE 1 = 1 ";

            if (obj.kode_fakultas != null && obj.kode_fakultas != String.Empty)
            {
                strSql += " AND A.kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            if (obj.kode_jurusan != null && obj.kode_jurusan != String.Empty)
            {
                strSql += " AND A.kode_jurusan = '" + obj.kode_jurusan.Trim() + "' ";
            }

            DataTable dto = this.ExecuteDataTable(strSql);
            return dto;
        }
        #endregion
    }
}
