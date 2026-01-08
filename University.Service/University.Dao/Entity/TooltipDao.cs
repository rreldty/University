using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using University.Dao.Base;

namespace University.Dao.Entity
{
    public class TooltipDao : BaseTooltip
    {
        #region Create SQL
        private void CreateSQL(string strEntity, out DataSource dtSource, out string strSQL, out string strSQLFilter, out string strSQLGroup, out string strDateColumn, out string strSort)
        {
            strSQLGroup = "";
            strDateColumn = "";
            strSort = "";
            dtSource = DataSource.University;

            switch (strEntity)
            {
                case "IVCL-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IVC1.VEIVCL AS Class_Level_5"
                            + " ,IVC1.VECLNA AS Class_Name_Level_5"
                            + " ,IVC2.VEIVCL AS Class_Level_4"
                            + " ,IVC2.VECLNA AS Class_Name_Level_4"
                            + " ,IVC3.VEIVCL AS Class_Level_3"
                            + " ,IVC3.VECLNA AS Class_Name_Level_3"
                            + " ,IVC4.VEIVCL AS Class_Level_2"
                            + " ,IVC4.VECLNA AS Class_Name_Level_2"
                            + " ,IVC5.VEIVCL AS Class_Level_1"
                            + " ,IVC5.VECLNA AS Class_Name_Level_1"
                            + " FROM PIVC AS IVC1"
                            + " LEFT JOIN PIVC  AS IVC2 ON 1=1"
                            + " AND IVC1.VECONO = IVC2.VECONO"
                            + " AND IVC1.VEBRNO = IVC2.VEBRNO"
                            + " AND IVC1.VECLPA = IVC2.VEIVCL"
                            + " LEFT JOIN PIVC  AS IVC3 ON 1=1"
                            + " AND IVC2.VECONO = IVC3.VECONO"
                            + " AND IVC2.VEBRNO = IVC3.VEBRNO"
                            + " AND IVC2.VECLPA = IVC3.VEIVCL"
                            + " LEFT JOIN PIVC  AS IVC4 ON 1=1"
                            + " AND IVC3.VECONO = IVC4.VECONO"
                            + " AND IVC3.VEBRNO = IVC4.VEBRNO"
                            + " AND IVC3.VECLPA = IVC4.VEIVCL"
                            + " LEFT JOIN PIVC  AS IVC5 ON 1=1"
                            + " AND IVC4.VECONO = IVC5.VECONO"
                            + " AND IVC4.VEBRNO = IVC5.VEBRNO"
                            + " AND IVC4.VECLPA = IVC5.VEIVCL"
                            + "";

                        strSQLFilter = "IVC1.VECLLV=5";
                        strSQLGroup = "";
                        strSort = "";
                        strDateColumn = "";

                        break;
                    }

                case "PPO2-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "PPREMA AS Remark"
                            + " FROM PPO2"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSort = "";
                        strDateColumn = "";

                        break;
                    }

                case "PPR2-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "PJREMA AS Remark"
                            + " FROM PPR2"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSort = "";
                        strDateColumn = "";

                        break;
                    }

                default:
                    {
                        dtSource = DataSource.University;
                        strSQL = "";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        strDateColumn = "";
                        break;
                    }

            }
        }
        #endregion

        #region Get Data

        public DataTable GetDataTable(string strEntity, string strFilter)
        {
            string strSQL = string.Empty, strSQLFilter = string.Empty, strSQLGroup = string.Empty, strDateColumn = string.Empty, strSort = string.Empty;
            DataSource dtSource;

            CreateSQL(strEntity, out dtSource, out strSQL, out strSQLFilter, out strSQLGroup, out strDateColumn, out strSort);

            return this.GetDataTableBase(dtSource, strSQL, strSQLFilter, strSQLGroup, strDateColumn, strFilter, strSort);
        }

        #endregion
    }
}
