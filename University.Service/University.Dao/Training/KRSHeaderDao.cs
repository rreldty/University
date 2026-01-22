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
    public class KrsHeaderDao : BaseDao<KrsHeaderDto>
    {
        #region Constructor

        public KrsHeaderDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<KrsHeaderDto> GetMapper()
        {
            Mapper<KrsHeaderDto> mapDto = new KrsHeaderMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(KrsHeaderDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("nim");
            lstField.Add("semester");
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("total_sks");

            return this.GenerateStringInsert("krsheader", lstField, obj);
        }

        public string ScriptUpdate(KrsHeaderDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("kode_fakultas");
            lstField.Add("kode_jurusan");
            lstField.Add("total_sks");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");

            return this.GenerateStringUpdate("krsheader", lstCondition, lstField, obj);
        }

        public string Save(KrsHeaderDto obj)
        {
            KrsDetailDao dao = new KrsDetailDao();
            List<string> lstSql = new List<string>();
            string strResult = string.Empty;

            //insert Header
            if (!IsExists(obj))
                lstSql.Add(ScriptInsert(obj));
            else
                lstSql.Add(ScriptUpdate(obj));

            //insert Detail 
            if (obj.objKrsDetail != null)
            {
                if (!dao.IsExists(obj.objKrsDetail))
                {
                    if (obj.objKrsDetail.line_no == 0)
                    {
                        obj.objKrsDetail.line_no = dao.getLineMax(obj.objKrsDetail) + 1;
                    }

                    lstSql.Add(dao.ScriptInsert(obj.objKrsDetail));
                }
                else
                {
                    lstSql.Add(dao.ScriptUpdate(obj.objKrsDetail));
                }

            }

            strResult = this.ExecuteDbNonQueryTransaction(lstSql);

            //proses calculate
            strResult = Calculate(obj);

            return strResult;
        }
        public string Calculate(KrsHeaderDto obj)
        {
            string strResult = string.Empty;

            KrsDetailDao dao = new KrsDetailDao();

            if (string.IsNullOrEmpty(strResult))
            {
                decimal decTotal = 0;
                obj.total_sks = 0;

                List<KrsDetailDto> lst = dao.GetList(new KrsDetailDto()
                {
                    nim = obj.nim,
                    semester = obj.semester,
                });
                if (lst.Count > 0)
                {
                    foreach (KrsDetailDto dto in lst)
                    {
                        decTotal += dto.sks;
                    }
                }
                obj.total_sks = decTotal;

                strResult = ExecuteDbNonQuery(ScriptUpdate(obj));
            }
            return strResult;
        }

        #endregion

        #region Delete Data

        public string Delete(KrsHeaderDto obj)
        {
            List<string> lstCondition = new List<string>();
            lstCondition.Add("nim");
            lstCondition.Add("semester");

            string strSql = this.GenerateStringDelete("krsheader", lstCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Select Data

        public bool IsExists(KrsHeaderDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM krsheader "
                            + " WHERE 1=1 "
                            + " AND nim   = '" + obj.nim.Trim() + "'"
                            + " AND semester   = " + obj.semester + ""
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

        public KrsHeaderDto Get(KrsHeaderDto obj)
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

            string strSql = this.GenerateStringSelect("krsheader", lstCondition, lstField, obj);
            KrsHeaderDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public KrsHeaderDto GetDataMahasiswa(KrsHeaderDto obj)
        {
            String strSql = "SELECT   B.NIM, "
                             + "   A.Semester,  "
                             + "   B.Kode_Fakultas AS kode_fakultas,  "
                             + "   B.Kode_Jurusan as kode_jurusan,  "
                             + "   A.Total_SKS AS total_sks "
                             + " FROM  Mahasiswa B  "
                             + "   LEFT JOIN krsheader A on 1 = 1  "
                             + "   AND A.NIM = B.NIM  "
                             + "   AND A.Semester = " + obj.semester + ""
                             + " WHERE 1 = 1  ";

            if (obj.nim != null && obj.nim != String.Empty)
            {
                strSql += " AND B.nim   = '" + obj.nim.Trim() + "'";
            }

            KrsHeaderDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<KrsHeaderDto> GetList(KrsHeaderDto obj)
        {
            string strSql = "SELECT "
                        + "    nim  "
                        + ",    semester  "
                        + ",    kode_fakultas  "
                        + ",    kode_jurusan  "
                        + ",    total_sks  "
                        + " FROM krsheader "
                        + " WHERE 1=1 ";

            if (obj.nim != null && obj.nim != String.Empty)
            {
                strSql += " AND nim   = '" + obj.nim.Trim() + "'";
            }

            if (obj.semester > 0)
            {
                strSql += " AND semester = " + obj.semester + "";
            }

            List<KrsHeaderDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<KrsHeaderDto> GetListPaging(KrsHeaderDto obj, int intPageNumber, int intPageSize, out int intTotalPage, out int intTotalRecord)
        {
            string strSql = "SELECT "
                        + "    nim  "
                        + ",    semester  "
                        + ",    kode_fakultas  "
                        + ",    kode_jurusan  "
                        + ",    total_sks  "
                        + " FROM krsheader "
                        + " WHERE 1=1 ";

            if (obj.nim != null && obj.nim != String.Empty)
            {
                strSql += " AND nim   = '" + obj.nim.Trim() + "'";
            }

            if (obj.semester > 0)
            {
                strSql += " AND semester = " + obj.semester + "";
            }

            List<KrsHeaderDto> dto = this.ExecutePaging(strSql, "kode_KrsHeader", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public DataTable GetDetailKrsHeader(KrsHeaderDto obj)
        {
            string strSql = "SELECT "
                            + "    nim  "
                        + ",    semester  "
                        + ",    kode_fakultas  "
                        + ",    kode_jurusan  "
                        + ",    total_sks  "
                        + " FROM krsheader "
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