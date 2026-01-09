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
    public class KRSHeaderDao : BaseDao<KRSHeaderDto>
    {
        #region Constructor

        public KRSHeaderDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<KRSHeaderDto> GetMapper()
        {
            Mapper<KRSHeaderDto> mapDto = new KRSHeaderMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(KRSHeaderDto     obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("total_sks");

            return this.GenerateStringInsert("KRSHeader", lstField, obj);
        }

        public string ScriptUpdate(KRSHeaderDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("total_sks");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");

            return this.GenerateStringUpdate("KRSHeader", lstCondition, lstField, obj);
        }

        public string Save(KRSHeaderDto obj)
        {
            string strResult = string.Empty;
            List<string> lstSql = new List<string>();

            KRSDetailDao dao = new KRSDetailDao();
            KRSDetailDto objLine = obj.objLine;

            if (!IsExists(obj))
            {
                lstSql.Add(ScriptInsert(obj));
            }
            else
            {
                lstSql.Add(ScriptUpdate(obj));
            }

            // Otomatis generate line_no jika 0
            if (objLine != null)
            {
                if (objLine.line_no == 0)
                {
                    objLine.line_no = dao.GetNextLineNo(objLine.nim, objLine.semester);
                }
                // Gunakan Save agar insert/update otomatis
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

        public string Calculate(KRSHeaderDto obj)
        {
            string strResult = string.Empty;

            try
            {
                KRSDetailDao detailDao = new KRSDetailDao();
                KRSDetailDto filter = new KRSDetailDto
                {
                    nim = obj.nim,
                    semester = obj.semester
                };
                List<KRSDetailDto> lstDetail = detailDao.GetList(filter);

                decimal totalSks = 0;
                foreach (KRSDetailDto detail in lstDetail)
                {
                    totalSks += detail.sks;
                }

                obj.total_sks = totalSks;
                string strSql = "UPDATE KRSHeader SET total_sks = " + totalSks
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

        #endregion

        #region Delete Data

        public string Delete(KRSHeaderDto obj)
        {
            KRSDetailDao detailDao = new KRSDetailDao();
            detailDao.DeleteByNimSemester(obj.nim, obj.semester);

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");

            string strSql = this.GenerateStringDelete("KRSHeader", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        public bool IsExists(KRSHeaderDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM KRSHeader "
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

        public KRSHeaderDto Get(KRSHeaderDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("total_sks");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");

            string strSql = this.GenerateStringSelect("KRSHeader", lstCondition, lstField, obj);
            KRSHeaderDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<KRSHeaderDto> GetList(KRSHeaderDto obj)
        {
            string strSql = "SELECT "
                            + " nim  "
                            + ", semester  "
                            + ", kode_fakultas  "
                            + ", kode_jurusan  "
                            + ", total_sks  "
                            + " FROM KRSHeader WHERE 1=1 ";

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

            List<KRSHeaderDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<KRSHeaderDto> GetListPaging(KRSHeaderDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                            + " nim  "
                            + ", semester  "
                            + ", kode_fakultas  "
                            + ", kode_jurusan  "
                            + ", total_sks  "
                            + " FROM KRSHeader WHERE 1=1 ";

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

            List<KRSHeaderDto> dto = this.ExecutePaging(strSql, "nim", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public List<KRSHeaderDto> GetReportKRS(KRSHeaderDto obj)
        {
            string strSql = @"
                SELECT 
                    a.nim AS [nim]
                    , a.semester AS [semester]
                    , a.kode_fakultas AS [kode_fakultas]
                    , d.nama_fakultas AS [nama_fakultas]
                    , a.kode_jurusan AS [kode_jurusan]
                    , e.nama_jurusan AS [nama_jurusan]
                    , a.total_sks AS [total_sks]
                FROM KRSHeader a
                LEFT JOIN Fakultas d ON 1=1
                    AND d.kode_fakultas = a.kode_fakultas
                LEFT JOIN Jurusan e ON 1=1
                    AND e.kode_fakultas = a.kode_fakultas
                    AND e.kode_jurusan = a.kode_jurusan
                WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.nim))
            {
                strSql += " AND a.nim = '" + obj.nim.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.semester))
            {
                strSql += " AND a.semester = '" + obj.semester.Trim() + "' ";
            }

            List<KRSHeaderDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        #endregion
    }
}