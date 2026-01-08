using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

using University.Dao.Base;
using University.Dto.Base;
using University.Training;

namespace University.Dao.Training
{
    public class VariabelDao : BaseDao<VariabelDto>
    {
        #region Constructor
        public VariabelDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation
        protected override Mapper<VariabelDto> GetMapper()
        {
            Mapper<VariabelDto> mapDto = new VariabelMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data
        public string ScriptInsert(VariabelDto obj)
        {
            string[] strField = new string[6];
            strField[0] = "Kode_Variabel";
            strField[1] = "Tipe_Variabel";
            strField[2] = "Nama_Variabel";
            strField[3] = "Nilai_Variabel";
            strField[4] = "Note";
            strField[5] = "Record_Status";

            return this.GenerateStringInsert("Variabel", strField, obj);
        }

        public string ScriptUpdate(VariabelDto obj)
        {
            string[] strField = new string[6];
            strField[0] = "Kode_Variabel";
            strField[1] = "Tipe_Variabel";
            strField[2] = "Nama_Variabel";
            strField[3] = "Nilai_Variabel";
            strField[4] = "Note";
            strField[5] = "Record_Status";

            string[] strCondition = new string[1];
            strCondition[0] = "Kode_Variabel";

            return this.GenerateStringUpdate("Variabel", strCondition, strField, obj);
        }

        public string Save(VariabelDto obj)
        {

            if (!IsExists(obj))
                return ExecuteDbNonQuery(ScriptInsert(obj));
            else
                return ExecuteDbNonQuery(ScriptUpdate(obj));
        }

        #endregion

        #region Delete Data
        public string Delete(VariabelDto obj)
        {
            string[] strCondition = new string[1];
            strCondition[0] = "Kode_Variabel";

            string strSql = this.GenerateStringDelete("Variabel", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data
        public bool IsExists(VariabelDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM Variabel "
                            + " WHERE 1=1 "
                            + " AND Kode_Variabel = '" + obj.Kode_Variabel.Trim() + "'"
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

        public VariabelDto Get(VariabelDto obj)
        {
            string[] strField = new string[6];
            strField[0] = "Kode_Variabel";
            strField[1] = "Tipe_Variabel";
            strField[2] = "Nama_Variabel";
            strField[3] = "Nilai_Variabel";
            strField[4] = "Note";
            strField[5] = "Record_Status";

            string[] strCondition = new string[1];
            strCondition[0] = "Kode_Variabel";

            string strSql = this.GenerateStringSelect("Variabel", strCondition, strField, obj);
            VariabelDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public VariabelDto GetFirst(VariabelDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " Kode_Variabel "
                    + ", Tipe_Variabel "
                    + ", Nama_Variabel "
                    + ", Nilai_Variabel "
                    + ", Note "
                    + ", Record_Status "
                    + " FROM Variabel "
                    + " WHERE 1=1 "
                    + " ORDER BY Kode_Variabel ASC "
                    + "";

            VariabelDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public VariabelDto GetPrevious(VariabelDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " Kode_Variabel "
                    + ", Tipe_Variabel "
                    + ", Nama_Variabel "
                    + ", Nilai_Variabel "
                    + ", Note "
                    + ", Record_Status "
                    + " FROM Variabel "
                    + " WHERE 1=1 "
                    + " AND Kode_Variabel < '" + obj.Kode_Variabel.Trim() + "' "
                    + " ORDER BY Kode_Variabel DESC "
                    + "";

            VariabelDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public VariabelDto GetNext(VariabelDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " Kode_Variabel "
                    + ", Tipe_Variabel "
                    + ", Nama_Variabel "
                    + ", Nilai_Variabel "
                    + ", Note "
                    + ", Record_Status "
                    + " FROM Variabel "
                    + " WHERE 1=1 "
                    + " AND Kode_Variabel > '" + obj.Kode_Variabel.Trim() + "' "
                    + " ORDER BY Kode_Variabel ASC "
                    + "";

            VariabelDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public VariabelDto GetLast(VariabelDto obj)
        {
            string strSql = "SELECT TOP 1 "
                    + " Kode_Variabel "
                    + ", Tipe_Variabel "
                    + ", Nama_Variabel "
                    + ", Nilai_Variabel "
                    + ", Note "
                    + ", Record_Status "
                    + " FROM Variabel "
                    + " WHERE 1=1 "
                    + " ORDER BY Kode_Variabel DESC "
                    + "";

            VariabelDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<VariabelDto> GetList(VariabelDto obj)
        {
            string strSql = "SELECT "
                    + " Kode_Variabel "
                    + ", Tipe_Variabel "
                    + ", Nama_Variabel "
                    + ", Nilai_Variabel "
                    + ", Note "
                    + ", Record_Status "
                    + " FROM Variabel "
                    + "WHERE 1=1 "
                    + "";


            if (obj.Kode_Variabel != null && obj.Kode_Variabel != String.Empty)
            {
                strSql += " AND Kode_Variabel = '" + obj.Kode_Variabel.Trim() + "' ";
            }

            List<VariabelDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<VariabelDto> GetListPaging(out int intTotalPage, out int intTotalRecord, VariabelDto obj, int intPageNumber, int intPageSize)
        {
            string strSql = "SELECT "
                    + " Kode_Variabel "
                    + ", Tipe_Variabel "
                    + ", Nama_Variabel "
                    + ", Nilai_Variabel "
                    + ", Note "
                    + ", Record_Status "
                    + "FROM Variabel WHERE 1=1 ";

            if (obj.Kode_Variabel != null && obj.Kode_Variabel != String.Empty)
            {
                strSql += " AND Kode_Variabel = '" + obj.Kode_Variabel.Trim() + "' ";
            }

            List<VariabelDto> dto = this.ExecutePaging(strSql, "Kode_Variabel", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        #endregion
    }
}