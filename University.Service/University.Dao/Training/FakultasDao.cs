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
    public class FakultasDao : BaseDao<FakultasDto>
    {
        #region Constructor

        public FakultasDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<FakultasDto> GetMapper()
        {
            Mapper<FakultasDto> mapDto = new FakultasMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(FakultasDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_fakultas");
            lstField.Add("nama_fakultas");
            lstField.Add("record_status");

            return this.GenerateStringInsert("Fakultas", lstField, obj);
        }

        public string ScriptUpdate(FakultasDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nama_fakultas");
            lstField.Add("record_status");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("kode_fakultas");

            return this.GenerateStringUpdate("Fakultas", lstCondition, lstField, obj);
        }

        public string Save(FakultasDto obj)
        {
            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data

        public string Delete(FakultasDto obj)
        {
            List<string> lstCondition = new List<string>();
            lstCondition.Add("kode_fakultas");

            string strSql = this.GenerateStringDelete("Fakultas", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data
        
        public bool IsExists(FakultasDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM Fakultas "
                            + " WHERE 1=1 "
                            + " AND kode_fakultas   = '" + obj.kode_fakultas.Trim() + "'"
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

        public FakultasDto Get(FakultasDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_fakultas");
            lstField.Add("nama_fakultas");
            lstField.Add("record_status");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("kode_fakultas");

            string strSql = this.GenerateStringSelect("Fakultas", lstCondition, lstField, obj);
            FakultasDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<FakultasDto> GetList(FakultasDto obj)
        {
            string strSql = "SELECT "
                            + " kode_fakultas  "
                            + ", nama_fakultas  "
                            + ", record_status  "
                            + " FROM Fakultas WHERE 1=1 ";

            if (obj.kode_fakultas != null && obj.kode_fakultas != String.Empty)
            {
                strSql += " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            List<FakultasDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<FakultasDto> GetListPaging(FakultasDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                            + " kode_fakultas  "
                            + ", nama_fakultas  "
                            + ", record_status  "
                            + " FROM Fakultas WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.kode_fakultas))
            {
                strSql += " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            List<FakultasDto> dto = this.ExecutePaging(strSql, "kode_fakultas", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public DataTable GetDetailFakultas(FakultasDto obj)
        {
            string strSql = "SELECT "									
                            + "  kode_fakultas AS [Kode Fakultas] "
	                        + " , nama_fakultas AS [Nama Fakultas] "
	                        + " , b.nama_common AS [Record Status] "
                            + "  from fakultas a "
                            + "  left join common b on 1 = 1 "
                            + " and b.tipe_common = 'record_status' "
                            + " and b.nilai_common = a.record_status "
                            + " where 1 = 1 ";

            if (obj.kode_fakultas != null && obj.kode_fakultas != String.Empty)
            {
                strSql += " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            DataTable dto = this.ExecuteDataTable(strSql);
            return dto;
        }
        #endregion
    }
}
