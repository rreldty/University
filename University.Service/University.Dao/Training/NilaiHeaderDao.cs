using System;
using System.Data;
using System.Collections.Generic;

using University.Dao.Base;
using University.Dto.Training;
using University.Dto.Base;

namespace University.Dao.Training
{
    public class NilaiHeaderDao : BaseDao<NilaiHeaderDto>
    {
        #region Constructor

        public NilaiHeaderDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<NilaiHeaderDto> GetMapper()
        {
            return new NilaiHeaderMappingDto();
        }

        #endregion

        #region Save Data

        public string ScriptInsert(NilaiHeaderDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("nilai");

            return this.GenerateStringInsert("NilaiHeader", lstField, obj);
        }

        public string ScriptUpdate(NilaiHeaderDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("nilai");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");

            return this.GenerateStringUpdate("NilaiHeader", lstCondition, lstField, obj);
        }

        public string Save(NilaiHeaderDto obj)
        {
            string strResult = string.Empty;
            List<string> lstSql = new List<string>();

            NilaiDetailDao dao = new NilaiDetailDao();
            NilaiDetailDto objLine = obj.objLine;

            if (!IsExists(obj))
            {
                lstSql.Add(ScriptInsert(obj));
            }
            else
            {
                lstSql.Add(ScriptUpdate(obj));
            }

            // Insert atau update detail
            if (objLine != null)
            {
                if (!dao.IsExists(objLine))
                    lstSql.Add(dao.ScriptInsert(objLine));
                else
                    lstSql.Add(dao.ScriptUpdate(objLine));
            }

            strResult = ExecuteDbNonQueryTransaction(lstSql);

            if (strResult == string.Empty)
            {
                Calculate(obj);
            }

            return strResult;
        }

        public string Calculate(NilaiHeaderDto obj)
        {
            string strResult = string.Empty;

            try
            {
                NilaiDetailDao detailDao = new NilaiDetailDao();
                NilaiDetailDto filter = new NilaiDetailDto
                {
                    nim = obj.nim,
                    semester = obj.semester
                };
                List<NilaiDetailDto> lstDetail = detailDao.GetList(filter);

                double totalSkor = 0;
                int jumlahMataKuliah = lstDetail.Count;

                foreach (NilaiDetailDto detail in lstDetail)
                {
                    totalSkor += detail.skor;
                }

                // Nilai Akhir = Total Skor / jumlah MataKuliah
                double nilaiAkhir = jumlahMataKuliah > 0 ? totalSkor / jumlahMataKuliah : 0;
                obj.nilai = nilaiAkhir;

                string strSql = "UPDATE NilaiHeader SET nilai = " + nilaiAkhir.ToString().Replace(",", ".")
                              + " WHERE nim = '" + obj.nim.Trim() + "'"
                              + " AND semester = '" + obj.semester.Trim() + "'";

                strResult = this.ExecuteDbNonQuery(strSql);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return strResult;
        }

        /// <summary>
        /// Update - Deletes selected details and recalculates nilai
        /// </summary>
        public string Update(NilaiHeaderDto obj)
        {
            string strResult = string.Empty;
            List<string> lstSql = new List<string>();

            NilaiDetailDao dao = new NilaiDetailDao();

            // Loop through Details list and delete each selected item
            if (obj.Details != null && obj.Details.Count > 0)
            {
                foreach (NilaiDetailDto detail in obj.Details)
                {
                    lstSql.Add(dao.ScriptDelete(detail));
                }
            }

            strResult = ExecuteDbNonQueryTransaction(lstSql);

            if (strResult == string.Empty)
            {
                Calculate(obj);
            }

            return strResult;
        }

        #endregion

        #region Delete Data

        public string Delete(NilaiHeaderDto obj)
        {
            NilaiDetailDao detailDao = new NilaiDetailDao();
            detailDao.DeleteByNimSemester(obj.nim, obj.semester);

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");

            string strSql = this.GenerateStringDelete("NilaiHeader", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        public bool IsExists(NilaiHeaderDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM NilaiHeader "
                            + " WHERE 1=1 "
                            + " AND nim = '" + obj.nim.Trim() + "'"
                            + " AND semester = '" + obj.semester.Trim() + "'"
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

        public NilaiHeaderDto Get(NilaiHeaderDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("nilai");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");

            string strSql = this.GenerateStringSelect("NilaiHeader", lstCondition, lstField, obj);
            NilaiHeaderDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        /// <summary>
        /// GetDataMahasiswa - get header data with student info
        /// </summary>
        public NilaiHeaderDto GetDataMahasiswa(string nim, string semester)
        {
            string strSql = @"
                SELECT
                    b.nim as [nim]
                    , @semester as [semester]
                    , b.kode_fakultas as [kode_fakultas]
                    , b.kode_jurusan as [kode_jurusan]
                    , ISNULL(a.nilai, 0) as [nilai]
                FROM mahasiswa b
                LEFT JOIN nilaiheader a on 1 = 1
                    and a.nim = b.nim
                    and a.semester = @semester
                WHERE 1 = 1
                    and b.nim = @nim";

            strSql = strSql.Replace("@nim", "'" + nim.Trim() + "'");
            strSql = strSql.Replace("@semester", "'" + semester.Trim() + "'");

            NilaiHeaderDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<NilaiHeaderDto> GetList(NilaiHeaderDto obj)
        {
            string strSql = "SELECT "
                            + " nim  "
                            + ", semester  "
                            + ", kode_fakultas  "
                            + ", kode_jurusan  "
                            + ", nilai  "
                            + " FROM NilaiHeader WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.nim))
            {
                strSql += " AND nim = '" + obj.nim.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.semester))
            {
                strSql += " AND semester = '" + obj.semester.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_fakultas))
            {
                strSql += " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_jurusan))
            {
                strSql += " AND kode_jurusan = '" + obj.kode_jurusan.Trim() + "' ";
            }

            List<NilaiHeaderDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<NilaiHeaderDto> GetListPaging(NilaiHeaderDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                            + " nim  "
                            + ", semester  "
                            + ", kode_fakultas  "
                            + ", kode_jurusan  "
                            + ", nilai  "
                            + " FROM NilaiHeader WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.nim))
            {
                strSql += " AND nim = '" + obj.nim.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.semester))
            {
                strSql += " AND semester = '" + obj.semester.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_fakultas))
            {
                strSql += " AND kode_fakultas = '" + obj.kode_fakultas.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.kode_jurusan))
            {
                strSql += " AND kode_jurusan = '" + obj.kode_jurusan.Trim() + "' ";
            }

            List<NilaiHeaderDto> dto = this.ExecutePaging(strSql, "nim", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        #endregion
    }
}
