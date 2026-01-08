using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using University.Dto.Base;
using University.Dao.Base;

namespace University.Dao.Entity
{
    public class DDLDao : BaseDDL
    {
        #region Create DDL Query

        private void CreateDDLQuery(string strEntity, out DataSource dtSource, out string strSQL, out string strSQLFilter, out string strOrderBy, out string strOrderDirection)
        {
            switch (strEntity)
            {
                #region A

                case "APNO-01":
                    dtSource = DataSource.University;
                    strSQL = "ZAAPNO AS CODE, ZAAPNA AS DSCR FROM ZAPP ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ARNO-01":
                    dtSource = DataSource.University;
                    strSQL = "SWARNO AS CODE,"
                            + " SWARNA AS DSCR"
                            + " FROM SSAR"
                            + "";

                    strSQLFilter = "SWRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ARNO-02":
                    dtSource = DataSource.University;
                    strSQL = "SWARNO AS CODE,"
                            + " SWARNA AS DSCR"
                            + " FROM SSAR"
                            + "";

                    strSQLFilter = "SWRCST = 1 AND SWLVNO = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ARNO-03":
                    dtSource = DataSource.University;
                    strSQL = "SWARNO AS CODE,"
                            + " SWARNA AS DSCR"
                            + " FROM SSAR"
                            + "";

                    strSQLFilter = "SWRCST = 1 AND SWLVNO = 2";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ARNO-04":
                    dtSource = DataSource.University;
                    strSQL = "SWARNO AS CODE,"
                            + " SWARNA AS DSCR"
                            + " FROM SSAR"
                            + "";

                    strSQLFilter = "SWRCST = 1 AND SWLVNO = 3";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ARNO-05":
                    dtSource = DataSource.University;
                    strSQL = "SWARNO AS CODE,"
                            + " SWARNA AS DSCR"
                            + " FROM SSAR"
                            + "";

                    strSQLFilter = "SWRCST = 1 AND SWLVNO = 4";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ARNO-06":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";

                    strSQLFilter = "CBRCST = 1 AND CBTBNO='ARNO'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "AR01-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CGARNO AS CODE,"
                            + " CGARNA AS DSCR"
                            + " FROM SDAR"
                            + " JOIN SDAH ON 1=1"
                            + " AND CHCONO = CGCONO"
                            + " AND CHBRNO = CGBRNO"
                            + " AND CHAR01 = CGARNO"
                            + "";

                    strSQLFilter = "CGRCST = 1 AND CGLVNO = 1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "AR01-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CGARNO AS CODE, "
                             + " CGARNA AS DSCR "
                             + " FROM SDAR "
                             + " JOIN SDAH ON 1=1 "
                             + " AND CHCONO = CGCONO "
                             + " AND CHBRNO = CGBRNO "
                             + " AND CHAR01 = CGARNO "
                             + " AND CHAR03 = ''"
                             + "";

                    strSQLFilter = "CGRCST = 1 AND CGLVNO = 1 AND CHAR03 = '' ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;


                case "AR01-03":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CGARNO AS CODE,"
                            + " CGARNA AS DSCR"
                            + " FROM SDAR"
                            + " JOIN SDAH ON 1=1"
                            + " AND CHCONO = CGCONO"
                            + " AND CHBRNO = CGBRNO"
                            + " AND CHAR01 = CGARNO"
                            + " JOIN ZCMP ON 1=1 "
                            + " AND ZCRHAR = CHARHC "
                            + " JOIN ZBUM ON 1 = 1 "
                            + " AND ZVCONO = ZCCONO "
                            + "";

                    strSQLFilter = "CGCONO = 'TNS' AND CGRCST = 1 AND CGLVNO = 1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                //case "AR01-03":
                //    dtSource = DataSource.University;
                //    strSQL = " DISTINCT CGARNO AS CODE,"
                //             + "    CGARNA AS DSCR "
                //             + "    FROM SDAR "
                //             + "    JOIN SDAH ON 1 = 1 "
                //             + "    AND CHCONO = CGCONO "
                //             + "    AND CHBRNO = CGBRNO "
                //             + "    AND CHAR01 = CGARNO "
                //             + "    JOIN ZBUM ON 1 = 1 "
                //             + "    JOIN SCMA ON 1 = 1 "
                //             + "    AND CMCONO = 'TNS' "
                //             + "    AND CMBRNO = 'JKT' "
                //             + "    AND CMCUNO = ZVCONO "
                //             + "    AND CMARHC = CHARHC ";
                //             //+ "    AND ZVUSNO = 'SYSADMIN'

                //    strSQLFilter = "CGCONO = 'TNS' AND CGRCST = 1 AND CGLVNO = 1 ";
                //    strOrderBy = "D";
                //    strOrderDirection = "ASC";
                //    break;

                case "AR02-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CGARNO AS CODE,"
                            + " CGARNA AS DSCR"
                            + " FROM SDAR"
                            + " JOIN SDAH ON 1=1"
                            + " AND CHCONO = 'TNS'"
                            + " AND CHBRNO = CGBRNO"
                            + " AND CHAR02 = CGARNO"
                            //+ " JOIN ZCMP ON 1=1 "
                            //+ " AND ZCRHAR = CHARHC "
                            + "";

                    strSQLFilter = "CGCONO = 'TNS' AND CGRCST = 1 AND CGLVNO = 2";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "AR02-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CGARNO AS CODE,"
                            + " CGARNA AS DSCR"
                            + " FROM SDAR"
                            + " JOIN SDAH ON 1=1"
                            + " AND CHCONO = CGCONO"
                            + " AND CHBRNO = CGBRNO"
                            + " AND CHAR02 = CGARNO"
                            + " AND CHAR03 = ''"
                            + "";

                    strSQLFilter = "CGRCST = 1 AND CGLVNO = 2 AND CHAR03 = ''";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "AR02-03":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CGARNO AS CODE,"
                            + " CGARNA AS DSCR"
                            + " FROM SDAR"
                            + " JOIN SDAH ON 1=1"
                            + " AND CHCONO = CGCONO"
                            + " AND CHBRNO = CGBRNO"
                            + " AND CHAR02 = CGARNO"
                            + " JOIN ZCMP ON 1=1 "
                            + " AND ZCRHAR = CHARHC "
                            + " JOIN ZBUM ON 1 = 1 "
                            + " AND ZVCONO = ZCCONO "
                            + "";

                    strSQLFilter = "CGCONO = 'TNS' AND  CGRCST = 1 AND CGLVNO = 2";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                //case "AR02-03":
                //    dtSource = DataSource.University;
                //    strSQL = " DISTINCT CGARNO AS CODE,"
                //             + "    CGARNA AS DSCR "
                //             + "    FROM SDAR "
                //             + "    JOIN SDAH ON 1 = 1 "
                //             + "    AND CHCONO = CGCONO "
                //             + "    AND CHBRNO = CGBRNO "
                //             + "    AND CHAR02 = CGARNO "
                //             + "    JOIN ZBUM ON 1 = 1 "
                //             + "    JOIN SCMA ON 1 = 1 "
                //             + "    AND CMCONO = 'TNS' "
                //             + "    AND CMBRNO = 'JKT' "
                //             + "    AND CMCUNO = ZVCONO "
                //             + "    AND CMARHC = CHARHC ";
                //    //+ "    AND ZVUSNO = 'SYSADMIN'

                //    strSQLFilter = "CGCONO = 'TNS' AND CGRCST = 1 AND CGLVNO = 2 ";
                //    strOrderBy = "D";
                //    strOrderDirection = "ASC";
                //    break;

                case "AR03-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CGARNO AS CODE,"
                            + " CGARNA AS DSCR"
                            + " FROM SDAR"
                            + " JOIN SDAH ON 1=1"
                            + " AND CHCONO = 'TNS'"
                            + " AND CHBRNO = CGBRNO"
                            + " AND CHAR03 = CGARNO"
                            + "";

                    strSQLFilter = "CGCONO = 'TNS' AND CGRCST = 1 AND CGLVNO = 3";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "AR03-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CGARNO AS CODE,"
                            + " CGARNA AS DSCR"
                            + " FROM SDAR"
                            + " JOIN SDAH ON 1=1"
                            + " AND CHCONO = CGCONO"
                            + " AND CHBRNO = CGBRNO"
                            + " AND CHAR03 = CGARNO"
                            + " JOIN ZCMP ON 1=1 "
                            + " AND ZCRHAR = CHARHC "
                            + " JOIN ZBUM ON 1 = 1 "
                            + " AND ZVCONO = ZCCONO "
                            + "";

                    strSQLFilter = "CGCONO = 'TNS' AND CGRCST = 1 AND CGLVNO = 3";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                //case "AR03-02":
                //    dtSource = DataSource.University;
                //    strSQL = " DISTINCT CGARNO AS CODE,"
                //             + "    CGARNA AS DSCR "
                //             + "    FROM SDAR "
                //             + "    JOIN SDAH ON 1 = 1 "
                //             + "    AND CHCONO = CGCONO "
                //             + "    AND CHBRNO = CGBRNO "
                //             + "    AND CHAR03 = CGARNO "
                //             + "    JOIN ZBUM ON 1 = 1 "
                //             + "    JOIN SCMA ON 1 = 1 "
                //             + "    AND CMCONO = 'TNS' "
                //             + "    AND CMBRNO = 'JKT' "
                //             + "    AND CMCUNO = ZVCONO "
                //             + "    AND CMARHC = CHARHC ";
                //    //+ "    AND ZVUSNO = 'SYSADMIN'

                //    strSQLFilter = "CGCONO = 'TNS' AND CGRCST = 1 AND CGLVNO = 3 ";
                //    strOrderBy = "D";
                //    strOrderDirection = "ASC";
                //    break;

                case "AVST-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'AVST'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region B

                case "BDNO-01":
                    dtSource = DataSource.University;
                    strSQL = "GUBDNO AS CODE,"
                            + " GUBDNA AS DSCR"
                            + " FROM GBUD"
                            + "";

                    strSQLFilter = "GURCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "BRNO-01":
                    dtSource = DataSource.University;
                    strSQL = "ZBBRNO AS CODE, ZBBRNA AS DSCR FROM ZBRC ";
                    strSQLFilter = "ZBRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "BRNO-02":
                    dtSource = DataSource.University;
                    strSQL = "ZBBRNO as CODE, ZBBRNA as DSCR from ZBRC";
                    strSQLFilter = "ZBRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "BRNO-03":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT ZBBRNO as CODE, "
                            + " ZBBRNA as DSCR "
                            + " from ZBRC"
                            + " join ZBUM on 1=1"
                            + "     and ZVCONO = ZBCONO"
                            + "     and ZVBRNO = ZBBRNO"
                            + "";
                    strSQLFilter = "ZBRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "BRNO-04":
                    dtSource = DataSource.University;
                    strSQL = "ZBBRNO AS CODE, ZBBRNA AS DSCR FROM ZBRC ";
                    strSQLFilter = "ZBRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "BRNO-05":
                    dtSource = DataSource.University;
                    strSQL = "ZBBRNO AS CODE,  ZBBRNA AS DSCR FROM ZBRC "
                            + " LEFT JOIN SDAH on 1 = 1"
                              + " AND CHCONO = ''"
                              + " AND CHBRNO = ''"
                              + " AND CHARHC = ZBARHC"
                              + " LEFT JOIN ZCMP ON 1=1 "
                              + " AND ZCCONO = ZBCONO "
                              + " join ZBUM on 1=1"
                              + "   and ZVCONO = ZCCONO"
                              + " ";
                    strSQLFilter = "ZBRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "BRTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE"
                            + ", ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'BRTY' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "BUNO-01":
                    dtSource = DataSource.University;
                    strSQL = "ZZBUNO AS CODE,"
                            + " ZZBUNA AS DSCR"
                            + " FROM ZBIZ"
                            + "";

                    strSQLFilter = " ZZRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "BYNO-01":
                    dtSource = DataSource.University;
                    strSQL = "VDBYNO AS CODE,"
                            + " VDBYNA AS DSCR"
                            + " FROM PBUY"
                            + "";

                    strSQLFilter = "VDRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "BYNO-02":
                    dtSource = DataSource.University;
                    strSQL = "VDBYNO AS CODE,"
                            + " VDBYNA AS DSCR"
                            + " FROM PBUY"
                            + "";

                    strSQLFilter = "VDRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region C
                case "CUNO-01":
                    dtSource = DataSource.University;
                    strSQL = "CMCUNO AS CODE,"
                            + " CMCUNA AS DSCR"
                            + " FROM SCMA"
                            + "";

                    strSQLFilter = "CMRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                //case "CUNO-02":
                //    dtSource = DataSource.University;
                //    strSQL = "CMCUNO AS CODE,"
                //            + " CMCUNA AS DSCR"
                //            + " FROM SCMA"
                //            + " LEFT JOIN SCHI on 1=1"
                //            + " AND CICONO = ''"
                //            + " AND CIBRNO = ''"
                //            + " AND CICHNO = CMCHNO"
                //            + "";

                //    strSQLFilter = "CMRCST = 1";
                //    strOrderBy = "D";
                //    strOrderDirection = "ASC";
                //    break;

                case "CUNO-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT SACUNO AS CODE,"
                            + " CMCUNA AS DSCR"
                            + " FROM SAR1"
                            + " JOIN SCMA on 1=1"
                            + " AND SACUNO = CMCUNO"
                            + "";

                    strSQLFilter = "SARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;


                case "CUPT-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE"
                            + ", ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'CUPT' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                    
                case "CCNO-01":
                    dtSource = DataSource.University;
                    strSQL = "GFCCNO AS CODE,"
                            + " GFCCNA AS DSCR"
                            + " FROM GCCA"
                            + "";

                    strSQLFilter = "GFRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                //case "CC01-01":
                //    dtSource = DataSource.University;
                //    strSQL = "DISTINCT CCCANO AS CODE,"
                //            + " CCCANA AS DSCR"
                //            + " FROM SCCA"
                //            + " JOIN SCHI ON 1=1"
                //            + " AND CICC01 = CCCANO"
                //            + "";

                //    strSQLFilter = "CCRCST = 1 AND CCLVNO = 1";
                //    strOrderBy = "D";
                //    strOrderDirection = "ASC";
                //    break;

                case "CC01-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CMCGR1 AS CODE,"
                            + " CDGRNA AS DSCR"
                            + " FROM SCMA"
                            + " JOIN SCGR ON 1=1"
                            + " AND CMCGR1 = CDCUGR"
                            + "";

                    strSQLFilter = "CMRCST = 1 AND CDLVNO = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CC02-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CMCGR2 AS CODE,"
                            + " CDGRNA AS DSCR"
                            + " FROM SCMA"
                            + " JOIN SCGR ON 1=1"
                            + " AND CMCGR2 = CDCUGR"
                            + "";

                    strSQLFilter = "CMRCST = 1 AND CDLVNO = 2";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                case "CC03-03":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CMCGR3 AS CODE,"
                            + " CDGRNA AS DSCR"
                            + " FROM SCMA"
                            + " JOIN SCGR ON 1=1"
                            + " AND CMCGR3 = CDCUGR"
                            + "";

                    strSQLFilter = "CMRCST = 1 AND CDLVNO = 3";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                case "CC02-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CCCANO AS CODE,"
                            + " CCCANA AS DSCR"
                            + " FROM SCCA"
                            + " JOIN SCHI ON 1=1"
                            + " AND CICC02 = CCCANO"
                            + "";

                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 2";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CC03-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CCCANO AS CODE,"
                            + " CCCANA AS DSCR"
                            + " FROM SCCA"
                            + " JOIN SCHI ON 1=1"
                            + " AND CICC03 = CCCANO"
                            + "";

                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 3";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CC03-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CCCANO AS CODE,"
                            + " CCCANA AS DSCR"
                            + " FROM SCCA"
                            + " JOIN SCHI ON 1=1"
                            + " AND CICC03 = CCCANO"
                            + "";

                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 3 AND CICC01 = '3' AND CICC02 = '16'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CC04-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CCCANO AS CODE,"
                            + " CCCANA AS DSCR"
                            + " FROM SCCA"
                            + " JOIN SCHI ON 1=1"
                            + " AND CICC04 = CCCANO"
                            + "";

                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 4";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CC04-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CCCANO AS CODE,"
                            + " CCCANA AS DSCR"
                            + " FROM SCCA"
                            + " JOIN SCHI ON 1=1"
                            + " AND CICC04 = CCCANO"
                            + "";

                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 4 AND CICC01 = '3' AND CICC02 = '16'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CC05-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CCCANO AS CODE,"
                            + " CCCANA AS DSCR"
                            + " FROM SCCA"
                            + " JOIN SCHI ON 1=1"
                            + " AND CICC05 = CCCANO"
                            + "";

                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 5";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CC05-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CCCANO AS CODE,"
                            + " CCCANA AS DSCR"
                            + " FROM SCCA"
                            + " JOIN SCHI ON 1=1"
                            + " AND CICC05 = CCCANO"
                            + "";

                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 5 AND CICC01 = '3' AND CICC02 = '16'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CC06-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CCCANO AS CODE,"
                            + " CCCANA AS DSCR"
                            + " FROM SCCA"
                            + " JOIN SCHI ON 1=1"
                            + " AND CICC06 = CCCANO"
                            + "";

                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 6";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CC06-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CCCANO AS CODE,"
                            + " CCCANA AS DSCR"
                            + " FROM SCCA"
                            + " JOIN SCHI ON 1=1"
                            + " AND CICC06 = CCCANO"
                            + "";

                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 6 AND CICC01 = '3' AND CICC02 = '16'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CGR1-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT CMCGR1 AS CODE,"
                            + " CDGRNA AS DSCR"
                            + " FROM SCMA"
                            + " JOIN SCGR ON 1=1"
                            + " AND CMCGR1 = CDCUGR"
                            + "";

                    strSQLFilter = "CMRCST = 1 AND CDLVNO = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CNNO-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";
                    
                    strSQLFilter = " CBTBNO = 'CNNO' AND CBRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "COGR-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT ZDCOGR AS CODE,"
                            + " ZDCOGN AS DSCR"
                            + " FROM ZCMG"

                            + "";

                    strSQLFilter = "ZDRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CONO-00":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT ZCCONO AS CODE,"
                            + " ZCBINA AS DSCR"
                            + " FROM ZCMP"

                            + "";

                    strSQLFilter = "ZCRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CONO-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT ZCCONO AS CODE,"
                            + " ZCBINA AS DSCR"
                            + " FROM ZCMP"
                            + " LEFT JOIN SDAH on 1=1"
                            + " AND CHCONO = ''"
                            + " AND CHBRNO = ''"
                            + " AND CHARHC = ZCARHC"
                            + "";

                    strSQLFilter = "ZCRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CONO-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT ZCCONO AS CODE,"
                            + " ZCBINA AS DSCR"
                            + " from ZCMP"
                            + " join ZBUM on 1=1"
                            + " and ZVCONO = ZCCONO"
                            + "";

                    strSQLFilter = "ZVRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CONO-03":
                    dtSource = DataSource.University;
                    strSQL = "ZCCONO AS CODE,"
                            + " ZCBINA AS DSCR"
                            + " FROM ZCMP"
                            + " LEFT JOIN SLOF ON 1=1 AND SLSOFC = ZCSOFC";

                    strSQLFilter = "ZCCOTY='D' AND ZCRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CONO-04":
                    dtSource = DataSource.University;
                    strSQL = "ZCCONO AS CODE,"
                            + " ZCBINA AS DSCR"
                            + " FROM ZCMP"
                            + " LEFT JOIN SDAH on 1=1"
                            + " AND CHCONO = ''"
                            + " AND CHBRNO = ''"
                            + " AND CHARHC = ZCARHC"
                            + "";

                    strSQLFilter = "ZCRCST=1 AND ZCCOTY not in ('P', 'H')";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CONO-05":
                    dtSource = DataSource.University;
                    //strSQL = "ZCCONO AS CODE "
                    //        + ", ZCCONA AS DSCR "
                    //        + "from "
                    //        + "( "
                    //        + "select distinct A1SLNO AS ZCCONO, A1SLNA AS ZCCONA, ZCARHC, ZCRCST "
                    //        + "from DSCM "
                    //        + "join ZCMP on 1=1 "
                    //        + "and ZCCONO = A1CUNO "
                    //        + ""
                    //        + "union all "
                    //        + ""
                    //        + "select distinct ZCCONO, ZCCONA, ZCARHC, ZCRCST "
                    //        + "from ZCMP "
                    //        + "where 1=1 "
                    //        + "and ZCCONO not in "
                    //        + "(select A1CUNO from DSCM) "
                    //        + ") AS Que "
                    //        + "LEFT JOIN SDAH on 1=1 "
                    //        + "AND CHCONO = '' "
                    //        + "AND CHBRNO = '' "
                    //        + "AND CHARHC = ZCARHC "
                    //        + "";
                    strSQL = "DISTINCT A1SLNO AS CODE "
                            + ", ZCBINA AS DSCR "
                            + "FROM ZCMP "
                            + "JOIN DSCM on 1=1 "
                            + "AND A1CONO = '' "
                            + "AND A1BRNO = '' "
                            + "AND A1CUNO = ZCCONO "
                            + "LEFT JOIN SDAH on 1=1 "
                            + "AND CHCONO = '' "
                            + "AND CHBRNO = '' "
                            + "AND CHARHC = ZCARHC "
                            + "";

                    strSQLFilter = "ZCRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CONO-06":
                    dtSource = DataSource.University;
                    
                    strSQL = "DISTINCT ZCCONO AS CODE "
                            + ", ZCBINA AS DSCR "
                            + "FROM ZCMP "
                            + "JOIN DSCM on 1=1 "
                            + "AND A1CONO = '' "
                            + "AND A1BRNO = '' "
                            + "AND A1CUNO = ZCCONO "
                            + "LEFT JOIN SDAH on 1=1 "
                            + "AND CHCONO = '' "
                            + "AND CHBRNO = '' "
                            + "AND CHARHC = ZCARHC "
                            + "";

                    strSQLFilter = "ZCRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CONO-07":
                    dtSource = DataSource.University;
                    strSQL = " DISTINCT ZCCONO AS CODE,"
                            + " ZCBINA AS DSCR"
                            + " FROM ZCMP"
                            + " LEFT JOIN SDAH on 1=1"
                            + " AND CHCONO = ''"
                            + " AND CHBRNO = ''"
                            + " AND CHARHC = ZCARHC"
                            //+ " LEFT JOIN SDAH on 1=1"
                            //+ " AND CHCONO = ''"
                            //+ " AND CHBRNO = ''"
                            //+ " AND CHARHC = ZCARHC"
                            + " LEFT JOIN ZBUM ON 1=1"
                            + " AND ZVCONO = ZCCONO"
                            + "";

                    strSQLFilter = "ZVRCST=1 AND ZCCOTY not in ('P', 'H')";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CONO-08":
                    dtSource = DataSource.University;
                    strSQL = " DISTINCT ZCCONO AS CODE,"
                            + " ZCBINA AS DSCR"
                            + " FROM ZCMP"
                            + " LEFT JOIN SDAH on 1=1"
                            + " AND CHCONO = 'TNS'"
                            + " AND CHBRNO = 'JKT'"
                            + " AND CHARHC = ZCRHAR"
                            + " LEFT JOIN ZBUM ON 1=1"
                            + " AND ZVCONO = ZCCONO"
                            + "";

                    strSQLFilter = "ZVRCST=1 AND ZCCOTY not in ('P', 'H')";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                    
                    //stock Approval
                case "CONO-09":
                    dtSource = DataSource.University;
                    strSQL = " DISTINCT ZCCONO AS CODE,"
                            + " ZCCONO + ' - ' + ZCBINA AS DSCR"
                            + " FROM ZCMP"
                            + " LEFT JOIN SDAH on 1=1"
                            + " AND CHCONO = 'TNS'"
                            + " AND CHBRNO = 'JKT'"
                            + " AND CHARHC = ZCRHAR"
                            + " LEFT JOIN ZBUM ON 1=1"
                            + " AND ZVCONO = ZCCONO"
                            + "";

                    strSQLFilter = "ZVRCST=1 AND ZCCOTY not in ('P', 'H')";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;


                case "COTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE"
                            + ", ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'COTY' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CTNO-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";
                    
                    strSQLFilter = " CBTBNO = 'CTNO' AND CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CUCA-01":
                    dtSource = DataSource.University;
                    strSQL = "CCCUCA AS CODE"
                            + ", CCCANA AS DSCR"
                            + " FROM SCCA"
                            + "";

                    strSQLFilter = "CCRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CUST-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE"
                            + ", ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'CUST' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "CYNO-01":
                    dtSource = DataSource.University;
                    strSQL = "GGCYNO AS CODE,"
                            + " GGCYNA AS DSCR"
                            + " FROM GCUR"
                            + "";
                    
                    strSQLFilter = " GGRCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region D
                case "DAYS-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'DAYS' and ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "DCST-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";
                    
                    strSQLFilter = "ZRVATY = 'DCST' and ZRRCST = 1";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "DCST-02":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";
                    
                    strSQLFilter = "ZRVANO IN ('DCST_OPEN','DCST_FINISH') AND ZRRCST = 1";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "DCST-03":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVANO IN ('DCST_DRAFT','DCST_APPROVE','DCST_VOID', 'DCST_CLOSE')";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "DCTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'DCTY'";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "DENO-01":
                    dtSource = DataSource.University;
                    strSQL = "ZYDENO AS CODE,"
                            + " ZYDENO AS DSCR"
                            + " FROM ZDUM"
                            + "";

                    strSQLFilter = "ZYRCST = 1";
                    strOrderBy = "ZYDENO";
                    strOrderDirection = "ASC";
                    break;

                case "DENO-02":
                    dtSource = DataSource.University;
                    strSQL = "ZYDENO AS CODE,"
                            + " GDDENA AS DSCR"
                            + " FROM ZDUM JOIN GDEP ON ZYCONO = GDCONO AND ZYBRNO=GDBRNO AND ZYDENO = GDDENO"
                            + "";

                    strSQLFilter = "ZYRCST = 1";
                    strOrderBy = "ZYDENO";
                    strOrderDirection = "ASC";
                    break;

                case "DFNO-01":
                    dtSource = DataSource.University;
                    strSQL = "DEDFNO AS CODE, DEDFNA AS DSCR FROM MDF1 ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "DFRS-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'DFRS' AND ZRRCST = 1 ";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "DFTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'DFTY'";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "DFTY-02":
                    dtSource = DataSource.University;
                    strSQL = " ZRVAVL AS CODE"
                            + " , ZRVANA AS DSCR"
                            + " FROM"
                            + " (SELECT DISTINCT "
                            + "   MMCONO"
                            + " , MMBRNO"
                            + " , MMLPNO"
                            + " , ZRVAVL"
                            + " , ZRVANA"
                            + " , ZRVASQ"
                            + " FROM ZVAR"
                            + " JOIN MPS2 ON 1=1"
                            + " AND MMDFTY = ZRVAVL"
                            + " WHERE ZRVATY = 'DFTY'"
                            + " ) A"
                            + "";

                    strSQLFilter = "";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "DMPS-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE"
                        + " , CBKYNA AS DSCR "
                        + " FROM GCT2 ";
                    strSQLFilter = "CBTBNO ='DMPS' ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "DMSS-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE"
                        + " , CBKYNA AS DSCR "
                        + " FROM GCT2 ";
                    strSQLFilter = "CBTBNO ='DMSS' ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "DMSP-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE"
                        + " , CBKYNA AS DSCR "
                        + " FROM GCT2 ";
                    strSQLFilter = "CBTBNO ='DMSP' ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "DMST-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE"
                        + " , CBKYNA AS DSCR "
                        + " FROM GCT2 ";
                    strSQLFilter = "CBTBNO ='DMST' ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "DMWS-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE"
                        + " , CBKYNA AS DSCR "
                        + " FROM GCT2 ";
                    strSQLFilter = "CBTBNO ='DMWS' ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "DMGC-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE"
                        + " , CBKYNA AS DSCR "
                        + " FROM GCT2 ";
                    strSQLFilter = "CBTBNO ='DMGC' ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "DPNO-01":
                    dtSource = DataSource.University;
                    strSQL = "DPDPNO AS CODE, DPDPNA AS DSCR FROM MDPO ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "DSNO-01":
                    dtSource = DataSource.University;
                    strSQL = "DSDSNO AS CODE, DSDSNA AS DSCR FROM MDSU ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region E
                case "EMNO-01":
                    dtSource = DataSource.University;
                    strSQL = "GIEMNO AS CODE,"
                            + " GIEMNA AS DSCR"
                            + " FROM GEMP"
                            + "";

                    strSQLFilter = "GIRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMNO-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT SOEMNO AS CODE,"
                            + " GIEMNA AS DSCR"
                            + " FROM SSO1"
                            + " LEFT JOIN GEMP ON 1=1 "
                            + " AND SOEMNO = GIEMNO"
                            + "";

                    strSQLFilter = "GIRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMG1-01":
                    dtSource = DataSource.University;
                    strSQL = "GQEMGR AS CODE,"
                            + " GQGRNA AS DSCR"
                            + " FROM GEGR"
                            + "";

                    strSQLFilter = " GQLVNO = 1 AND GQRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMG2-01":
                    dtSource = DataSource.University;
                    strSQL = "GQEMGR AS CODE,"
                            + " GQGRNA AS DSCR"
                            + " FROM GEGR"
                            + "";

                    strSQLFilter = " GQLVNO = 2 AND GQRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMG3-01":
                    dtSource = DataSource.University;
                    strSQL = "GQEMGR AS CODE,"
                            + " GQGRNA AS DSCR"
                            + " FROM GEGR"
                            + "";

                    strSQLFilter = " GQLVNO = 3 AND GQRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMG4-01":
                    dtSource = DataSource.University;
                    strSQL = "GQEMGR AS CODE,"
                            + " GQGRNA AS DSCR"
                            + " FROM GEGR"
                            + "";

                    strSQLFilter = " GQLVNO = 4 AND GQRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMG5-01":
                    dtSource = DataSource.University;
                    strSQL = "GQEMGR AS CODE,"
                            + " GQGRNA AS DSCR"
                            + " FROM GEGR"
                            + "";

                    strSQLFilter = " GQLVNO = 5 AND GQRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMG6-01":
                    dtSource = DataSource.University;
                    strSQL = "GQEMGR AS CODE,"
                            + " GQGRNA AS DSCR"
                            + " FROM GEGR"
                            + "";

                    strSQLFilter = " GQLVNO = 6 AND GQRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMG7-01":
                    dtSource = DataSource.University;
                    strSQL = "GQEMGR AS CODE,"
                            + " GQGRNA AS DSCR"
                            + " FROM GEGR"
                            + "";

                    strSQLFilter = " GQLVNO = 7 AND GQRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMG8-01":
                    dtSource = DataSource.University;
                    strSQL = "GQEMGR AS CODE,"
                            + " GQGRNA AS DSCR"
                            + " FROM GEGR"
                            + "";

                    strSQLFilter = " GQLVNO = 8 AND GQRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMG9-01":
                    dtSource = DataSource.University;
                    strSQL = "GQEMGR AS CODE,"
                            + " GQGRNA AS DSCR"
                            + " FROM GEGR"
                            + "";

                    strSQLFilter = " GQLVNO = 9 AND GQRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EMTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";
                    
                    strSQLFilter = " ZRVATY = 'EMTY' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EVGR-01":
                    dtSource = DataSource.University;
                    strSQL = "RVEVGR AS CODE"
                            + " , RVEVGN AS DSCR"
                            + " FROM SEV1"
                            + "";

                    strSQLFilter = "RVRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "EVCA-01":
                    dtSource = DataSource.University;
                    strSQL = "RWEVCA AS CODE"
                            + " , RWEVCN AS DSCR"
                            + " FROM SEV2"
                            + "";

                    strSQLFilter = "RWRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region F

                case "FCNO-01":
                    dtSource = DataSource.University;
                    strSQL = "HEFCNO AS CODE,"
                            + " HEFCNA AS DSCR"
                            + " FROM IFS1"
                            + "";

                    strSQLFilter = "HERCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "FKLT-01":
                    dtSource = DataSource.University;
                    strSQL = "Kode_Fakultas as Code"
                            + " , Nama_Fakultas as DSCR "
                            + " FROM Fakultas"
                            + "";

                    strSQLFilter = "Record_Status = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                #endregion

                #region G
                case "GCT1-01":
                    dtSource = DataSource.University;
                    strSQL = "CATBNO AS CODE, CATBNA AS DSCR FROM GCT1 ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "GEND-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO as CODE, CBKYNA as DSCR from GCT2";
                    strSQLFilter = "CBTBNO = 'GEND' and CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                    
                case "GRLV-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'GRLV'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "GSPN-01":
                    dtSource = DataSource.University;
                    strSQL = "G9SPNO AS CODE, G9SPNO + ' - ' + G9SPNA AS DSCR FROM GSPN ";
                    strSQLFilter = "G9RCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "GTOD-01":
                    dtSource = DataSource.University;
                    strSQL = "GWTDNO AS CODE,"
                            + " GWTDNA AS DSCR"
                            + " FROM GTOD"
                            + "";

                    strSQLFilter = "GWRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region H
                #endregion

                #region I
                
                case "IC01-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HAITCA AS CODE, HACANA AS DSCR "
                            + " FROM ICAT"
                            + " JOIN IHIR on 1=1"
                            + " AND HHCONO = HACONO"
                            + " AND HHBRNO = HABRNO"
                            + " AND HHIC01 = HAITCA"
                            + "";

                    strSQLFilter = "HALVNO = 1 AND HARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IC02-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HAITCA AS CODE, HACANA AS DSCR "
                            + " FROM ICAT"
                            + " JOIN IHIR on 1=1"
                            + " AND HHCONO = HACONO"
                            + " AND HHBRNO = HABRNO"
                            + " AND HHIC02 = HAITCA"
                            + "";

                    strSQLFilter = "HALVNO = 2 AND HARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IC03-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HAITCA AS CODE, HACANA AS DSCR "
                            + " FROM ICAT"
                            + " JOIN IHIR on 1=1"
                            + " AND HHCONO = HACONO"
                            + " AND HHBRNO = HABRNO"
                            + " AND HHIC03 = HAITCA"
                            + "";

                    strSQLFilter = "HALVNO = 3 AND HARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IC04-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HAITCA AS CODE, HACANA AS DSCR "
                            + " FROM ICAT"
                            + " JOIN IHIR on 1=1"
                            + " AND HHCONO = HACONO"
                            + " AND HHBRNO = HABRNO"
                            + " AND HHIC04 = HAITCA"
                            + "";

                    strSQLFilter = "HALVNO = 4 AND HARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IC05-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HAITCA AS CODE, HACANA AS DSCR "
                            + " FROM ICAT"
                            + " JOIN IHIR on 1=1"
                            + " AND HHCONO = HACONO"
                            + " AND HHBRNO = HABRNO"
                            + " AND HHIC05 = HAITCA"
                            + "";

                    strSQLFilter = "HALVNO = 5 AND HARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IC06-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HAITCA AS CODE, HACANA AS DSCR "
                            + " FROM ICAT"
                            + " JOIN IHIR on 1=1"
                            + " AND HHCONO = HACONO"
                            + " AND HHBRNO = HABRNO"
                            + " AND HHIC06 = HAITCA"
                            + "";

                    strSQLFilter = "HALVNO = 6 AND HARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IC07-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HAITCA AS CODE, HACANA AS DSCR "
                            + " FROM ICAT"
                            + " JOIN IHIR on 1=1"
                            + " AND HHCONO = HACONO"
                            + " AND HHBRNO = HABRNO"
                            + " AND HHIC07 = HAITCA"
                            + "";

                    strSQLFilter = "HALVNO = 7 AND HARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IC08-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HAITCA AS CODE, HACANA AS DSCR "
                            + " FROM ICAT"
                            + " JOIN IHIR on 1=1"
                            + " AND HHCONO = HACONO"
                            + " AND HHBRNO = HABRNO"
                            + " AND HHIC08 = HAITCA"
                            + "";

                    strSQLFilter = "HALVNO = 8 AND HARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC01 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=1 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC01 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC02 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=2 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC02 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-03":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC03 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=3 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC03 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-04":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC04 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=4 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC04 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-05":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC05 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=5 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC05 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-06":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC06 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=6 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC06 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-07":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC07 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=7 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC07 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-08":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC08 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=8 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC08 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-09":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC09 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=9 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC09 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ICAT-10":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC10 AS CODE, HACANA AS DSCR FROM IHIR "
                            + "JOIN ICAT ON 1=1 "
                            + "AND HACONO=HHCONO "
                            + "AND HABRNO=HHBRNO "
                            + "AND HALVNO=10 "
                            + "AND HARCST=1 "
                            + "AND HAITCA=HHIC10 "
                            + "";

                    strSQLFilter = "HHRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IGRP-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";

                    strSQLFilter = "HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ILOC-01":
                    dtSource = DataSource.University;
                    strSQL = "HLLONO AS CODE,"
                            + " HLLONA AS DSCR"
                            + " FROM ILOC"
                            + "";
                    
                    strSQLFilter = "HLRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ILOC-02":
                    dtSource = DataSource.University;
                    strSQL = "HLLONO AS CODE,"
                            + " HLLONA AS DSCR"
                            + " FROM ILOC"
                            //+ " JOIN ZVAR on 1=1"
                            //+ "     AND ZRCONO = ''"
                            //+ "     AND ZRBRNO = ''"
                            //+ "     AND ZRVANO = 'LOTY_GOOD'"
                            + "";

                    strSQLFilter = "HLRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ILOC-03":
                    dtSource = DataSource.University;
                    strSQL = "HLLONO AS CODE,"
                            + " HLLONA AS DSCR"
                            + " FROM ILOC"
                            //+ " JOIN ZVAR ON 1=1"
                            //+ "     AND ZRCONO = ''"
                            //+ "     AND ZRBRNO = ''"
                            //+ "     AND ZRVANO = 'LOTY_BAD'"
                            + "";

                    strSQLFilter = "HLRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IRES-01":
                    dtSource = DataSource.University;
                    strSQL = "HRRSNO AS CODE,"
                            + " HRRSNA AS DSCR"
                            + " FROM IRES"
                            + "";
                    
                    strSQLFilter = "HRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IRES-02":
                    dtSource = DataSource.University;
                    strSQL = "HRRSNO AS CODE,"
                            + " HRRSNA AS DSCR"
                            + " FROM IRES"
                            + " JOIN ZVAR A on 1=1"
                            + "     AND A.ZRCONO = ''"
                            + "     AND A.ZRBRNO = ''"
                            + "     AND A.ZRVANO = 'TRTY_AJ'"
                            + "     AND A.ZRVAVL = HRTRTY"
                            + " JOIN ZVAR B on 1=1"
                            + "     AND B.ZRCONO = ''"
                            + "     AND B.ZRBRNO = ''"
                            + "     AND B.ZRVANO = 'RSTY_NORMAL'"
                            + "     AND B.ZRVAVL = HRRSTY"
                            + "";
                    
                    strSQLFilter = "HRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITCL-01":
                    dtSource = DataSource.University;
                    strSQL = "HCITCL AS CODE,"
                            + " HCCLNA AS DSCR"
                            + " FROM ICLS"
                            + "";

                    strSQLFilter = "HCRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITNO-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HMITNO AS CODE, HMITNA AS DSCR"
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + " AND HHCONO = HMCONO"
                            + " AND HHBRNO = HMBRNO"
                            + " AND HHIHNO = HMIHNO"
                            + "";

                    strSQLFilter = "";
                    strOrderBy = "C";
                    strOrderDirection = "ASC";
                    break;

                case "ITNO-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HMITNO AS CODE, CASE WHEN HMITN2 <> '' THEN HMITN2 ELSE HMITNA END AS DSCR"
                            + " FROM IIMA"
                            + "";

                    strSQLFilter = "";
                    strOrderBy = "C";
                    strOrderDirection = "ASC";
                    break;

                case "ITG1-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";
                    
                    strSQLFilter = "HGLVNO = 1 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG1-02":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "JOIN IIMA ON HMCONO = '' AND HMBRNO = '' AND HMITG1 = HGITGR "
                            + "";

                    strSQLFilter = "HGLVNO = 1 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG2-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";

                    strSQLFilter = "HGLVNO = 2 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG2-02":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "JOIN IIMA ON HMCONO = '' AND HMBRNO = '' AND HMITG2 = HGITGR "
                            + "";

                    strSQLFilter = "HGLVNO = 2 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG3-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";

                    strSQLFilter = "HGLVNO = 3 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG3-02":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "JOIN IIMA ON HMCONO = '' AND HMBRNO = '' AND HMITG3 = HGITGR "
                            + "";

                    strSQLFilter = "HGLVNO = 3 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG4-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";

                    strSQLFilter = "HGLVNO = 4 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG5-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";

                    strSQLFilter = "HGLVNO = 5 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG6-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";

                    strSQLFilter = "HGLVNO = 6 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG7-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";

                    strSQLFilter = "HGLVNO = 7 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG8-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";

                    strSQLFilter = "HGLVNO = 8 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITG9-01":
                    dtSource = DataSource.University;
                    strSQL = "HGITGR AS CODE,"
                            + " HGGRNA AS DSCR"
                            + " FROM IGRP"
                            + "";
                    
                    strSQLFilter = "HGLVNO = 9 AND HGRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITTY-01":
                    dtSource = DataSource.University;
                    strSQL = "HTITTY AS CODE,"
                            + " HTTYNA AS DSCR"
                            + " FROM ITYP"
                            + "";

                    strSQLFilter = "HTRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ITUM-01":
                    dtSource = DataSource.University;
                    strSQL = "HUUMNO AS CODE,"
                            + " HUUMNA AS DSCR"
                            + " FROM IUOM"
                            + "";

                    strSQLFilter = "HURCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IWGR-01":
                    dtSource = DataSource.University;
                    strSQL = "HVWHGR AS CODE,"
                            + " HVGRNA AS DSCR"
                            + " FROM IWGR"
                            + "";

                    strSQLFilter = "HVRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IWHS-01":
                    dtSource = DataSource.University;
                    strSQL = "HWWHNO AS CODE,"
                            + " HWWHNA AS DSCR"
                            + " FROM IWHS"
                            + "";
                    
                    strSQLFilter = "HWRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IWHS-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HWWHNO AS CODE,"
                            + " HWWHNA AS DSCR"
                            + " FROM IWHS"
                            //+ " JOIN IWAU ON 1=1"
                            //+ "     AND WWCONO = HWCONO"
                            //+ "     AND WWBRNO = HWBRNO"
                            //+ "     AND WWWHNO = HWWHNO"
                            //+ " JOIN IWUS ON 1=1"
                            //+ "     AND WVCONO = WWCONO"
                            //+ "     AND WVBRNO = WWBRNO"
                            //+ "     AND WVUGNO = WWUGNO"
                            + "";

                    strSQLFilter = "HWRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC01 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=1"
                            + " AND HAITCA=HHIC01"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-02":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC02 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=2"
                            + " AND HAITCA=HHIC02"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-03":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC03 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=3"
                            + " AND HAITCA=HHIC03"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-04":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC04 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=4"
                            + " AND HAITCA=HHIC04"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-05":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC05 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=5"
                            + " AND HAITCA=HHIC05"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-06":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC06 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=6"
                            + " AND HAITCA=HHIC06"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-07":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC07 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=7"
                            + " AND HAITCA=HHIC07"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-08":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC08 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=8"
                            + " AND HAITCA=HHIC08"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-09":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC09 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=9"
                            + " AND HAITCA=HHIC09"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "IHIR-10":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HHIC10 AS CODE "
                            + " , HACANA AS DSCR"
                            + " FROM IHIR"
                            + " JOIN ICAT ON 1=1"
                            + " AND HACONO=''"
                            + " AND HABRNO=''"
                            + " AND HALVNO=10"
                            + " AND HAITCA=HHIC10"
                            + "";

                    strSQLFilter = "HHCONO = '' AND HHBRNO = '' AND HHRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region J
                case "JBTY-01":
                    dtSource = DataSource.University;
                    strSQL = "YJJBTY AS CODE,"
                            + " YJJBTZ AS DSCR"
                            + " FROM SYCJ"
                            + "";

                    strSQLFilter = "YJRCST = 1";
                    strOrderBy = "C";
                    strOrderDirection = "ASC";
                    break;

                case "JRSN-01":
                    dtSource = DataSource.University;
                    strSQL = "Kode_Jurusan AS CODE"
                            + " , Nama_Jurusan AS DSCR "
                            + " FROM Jurusan"
                            + "";

                    strSQLFilter = "Record_Status = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                #endregion

                #region K
                #endregion

                #region L
                case "LOTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'LOTY' and ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "LPGR-01":
                    dtSource = DataSource.University;
                    strSQL = "MJLPGR AS CODE, MJLPGN AS DSCR FROM MLPG ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "LPNO-01":
                    dtSource = DataSource.University;
                    strSQL = "MDLPNO AS CODE, MDLPNA AS DSCR FROM MLPR ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "LPTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE, ZRVANA AS DSCR FROM ZVAR ";
                    strSQLFilter = "ZRVATY = 'LPTY'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "LPRS-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE, ZRVANA AS DSCR FROM ZVAR ";
                    strSQLFilter = "ZRVATY = 'LPRS'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "LPST-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE, ZRVANA AS DSCR FROM ZVAR ";
                    strSQLFilter = "ZRVATY = 'LPST'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "LVNO-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'GRLV'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region M
                case "MDF1-01":
                    dtSource = DataSource.University;
                    strSQL = "DEDFNO AS CODE"
                            + " , DEDFNA AS DSCR"
                            + " FROM MDF1"
                            + " LEFT JOIN MDGR ON 1=1"
                            + "     AND DECONO = DGCONO"
                            + "     AND (DEBRNO = DGBRNO OR DEBRNO = '')"
                            + "     AND DEDPGR = DGDPGR"
                            + "";

                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "MEMP-01":
                    dtSource = DataSource.University;
                    strSQL = "EMEMNO AS CODE,"
                            + " EMEMNA AS DSCR"
                            + " FROM MEMP"
                            + "";

                    strSQLFilter = "EMRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "MEPA-01":
                    dtSource = DataSource.University;
                    strSQL = "ZMMENO AS CODE, ZMMENA AS DSCR FROM ZMNU ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "METY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE, ZRVANA AS DSCR FROM ZVAR ";
                    strSQLFilter = "ZRVATY = 'METY' ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "MITY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'MITY'";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "MLPR-01":
                    dtSource = DataSource.University;
                    strSQL = "MDLPNO AS CODE, MDLPNA AS DSCR FROM MLPR ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;



                case "MONT-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'MONT'";
                    strOrderBy = " CAST(ZRVASQ AS INT) ";
                    strOrderDirection = "ASC";
                    break;



                case "MOTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'MOTY'";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "MRKT-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";

                    strSQLFilter = "CBTBNO = 'MRKT' AND CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "MRST-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";

                    strSQLFilter = "CBTBNO = 'MRST' AND CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region N
                case "NDNO-01":
                    dtSource = DataSource.University;
                    strSQL = "YNNDNO AS CODE,"
                            + " YNNDNA AS DSCR"
                            + " FROM SYCN"
                            + " JOIN ZBRC ON 1=1 AND ZBCONO=YNCUNO AND ZBBRNO=YNCUBR AND ZBBRTY='D'"
                            + "";

                    strSQLFilter = "ZBRCST=1 AND YNRCST = 1";
                    strOrderBy = "C";
                    strOrderDirection = "ASC";
                    break;

                case "NDNO-02":
                    dtSource = DataSource.University;
                    strSQL = "YNNDNO AS CODE,"
                            + " YNNDNA AS DSCR"
                            + " FROM SYCN"
                            + "";

                    strSQLFilter = "";
                    strOrderBy = "C";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region O
                case "OGNO-01":
                    dtSource = DataSource.University;
                    strSQL = "GOOGNO AS CODE,"
                            + " GOOGNA AS DSCR"
                            + " FROM GOG1"
                            + "";

                    strSQLFilter = "GORCST = 1";
                    strOrderBy = "C";
                    strOrderDirection = "ASC";
                    break;

                case "OGNO-02":
                    dtSource = DataSource.University;
                    strSQL = "GOOGNO AS CODE,"
                            + " GOOGNA AS DSCR"
                            + " FROM [@GOOG]"
                            + "";

                    strSQLFilter = "";
                    strOrderBy = "C";
                    strOrderDirection = "ASC";
                    break;

                case "ORST-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'ORST'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "ORTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'ORTY'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "OTGR-01":
                    dtSource = DataSource.University;
                    strSQL = "CATBNO AS CODE,"
                            + " CATBNA AS DSCR"
                            + " FROM GCT1"
                            + "";

                    strSQLFilter = "CATBTY = 'OTGR' AND CARCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "OTNO-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";

                    strSQLFilter = "CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region P
                case "PETY-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";

                    strSQLFilter = " CBTBNO = 'PETY' AND CBRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "PGNO-01":
                    dtSource = DataSource.University;
                    strSQL = "ZPPGNO AS CODE, ZPPGNA AS DSCR FROM ZPGM ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "PLST-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVANA AS DSCR,"
                            + " ZRVAVL AS CODE"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'PLST'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "PRTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'PRTY'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "PYST-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'PYST'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "PRGR-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";

                    strSQLFilter = " CBTBNO = 'SLPG' AND CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "PYTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'PYTY' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "PCSQ-01":
                    dtSource = DataSource.University;
                    strSQL = "YPPCSQ AS CODE,"
                            + " YPPCNA AS DSCR"
                            + " FROM SYCP"
                            + "";

                    strSQLFilter = "YPRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region Q
                #endregion

                #region R
                case "RENO-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";
                    
                    strSQLFilter = "CBTBNO = 'RENO' and CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "RELI-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";
                    
                    strSQLFilter = "CBTBNO = 'RELI' and CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "RSNO-01":
                    dtSource = DataSource.University;
                    strSQL = "GRRSNO AS CODE,"
                            + " GRRSNA AS DSCR"
                            + " FROM GRES"
                            + "";

                    strSQLFilter = /*"GRRSTB = 'PR' AND*/ "GRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "RSNO-02":
                    dtSource = DataSource.University;
                    strSQL = "GRRSNO AS CODE,"
                            + " GRRSNA AS DSCR"
                            + " FROM GRES"
                            + "";

                    strSQLFilter = "GRRSTB = 'AV' AND GRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "RSNO-03":
                    dtSource = DataSource.University;
                    strSQL = "GRRSNO AS CODE,"
                            + " GRRSNA AS DSCR"
                            + " FROM GRES"
                            + "";

                    strSQLFilter = "GRRSTB = 'PO' AND GRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "RSTB-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'RSTB'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "RSTB-02":
                    dtSource = DataSource.University;
                    strSQL = "GRRSNO AS CODE, "
                            + " GRRSNA AS DSCR "
                            + " FROM GRES"
                            + "";

                    strSQLFilter = "GRRSTB = '" + BaseMethod.GetVariableValue("RSTB_LP") + "'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "RSTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'RSTY' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "RTTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = " ZRVATY = 'RTTY' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region S

                case "SBNO-01":
                    dtSource = DataSource.University;
                    strSQL = "DISTINCT HJSBNO AS CODE, HMITNA AS DSCR "
                            + "FROM ISBM "
                            + "JOIN IIMA ON HMCONO = '' AND HMBRNO = '' AND HMITNO = HJSBNO "
                            + "JOIN IHIR ON HHIHNO = HMIHNO";

                    strSQLFilter = "";
                    strOrderBy = "C";
                    strOrderDirection = "ASC";
                    break;
                                    
                case "SLOF-01":
                    dtSource = DataSource.University;
                    strSQL = "SLSOFC AS CODE,"
                            + " SLSOFN AS DSCR"
                            + " FROM SLOF"
                            + "";

                    strSQLFilter = "SLRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-01":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-02":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 2 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-03":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 3 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-04":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 4 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-05":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 5 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-06":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 6 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-07":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 7 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-08":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 8 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-09":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 9 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCCA-10":
                    dtSource = DataSource.University;
                    strSQL = "CCCANO AS CODE, CCCANA AS DSCR FROM SCCA ";
                    strSQLFilter = "CCRCST = 1 AND CCLVNO = 10 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCGR-01":
                    dtSource = DataSource.University;
                    strSQL = "CDCUGR AS CODE, CDGRNA AS DSCR FROM SCGR ";
                    strSQLFilter = "CDRCST = 1 AND CDLVNO = 1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCGR-02":
                    dtSource = DataSource.University;
                    strSQL = "CDCUGR AS CODE, CDGRNA AS DSCR FROM SCGR ";
                    strSQLFilter = "CDRCST = 1 AND CDLVNO = 2 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCGR-03":
                    dtSource = DataSource.University;
                    strSQL = "CDCUGR AS CODE, CDGRNA AS DSCR FROM SCGR ";
                    strSQLFilter = "CDRCST = 1 AND CDLVNO = 3 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCGR-04":
                    dtSource = DataSource.University;
                    strSQL = "CDCUGR AS CODE, CDGRNA AS DSCR FROM SCGR ";
                    strSQLFilter = "CDRCST = 1 AND CDLVNO = 4 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCGR-05":
                    dtSource = DataSource.University;
                    strSQL = "CDCUGR AS CODE, CDGRNA AS DSCR FROM SCGR ";
                    strSQLFilter = "CDRCST = 1 AND CDLVNO = 5 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCGR-06":
                    dtSource = DataSource.University;
                    strSQL = "CDCUGR AS CODE, CDGRNA AS DSCR FROM SCGR ";
                    strSQLFilter = "CDRCST = 1 AND CDLVNO = 6 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCGR-07":
                    dtSource = DataSource.University;
                    strSQL = "CDCUGR AS CODE, CDGRNA AS DSCR FROM SCGR ";
                    strSQLFilter = "CDRCST = 1 AND CDLVNO = 7 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCGR-08":
                    dtSource = DataSource.University;
                    strSQL = "CDCUGR AS CODE, CDGRNA AS DSCR FROM SCGR ";
                    strSQLFilter = "CDRCST = 1 AND CDLVNO = 8 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SCGR-09":
                    dtSource = DataSource.University;
                    strSQL = "CDCUGR AS CODE, CDGRNA AS DSCR FROM SCGR ";
                    strSQLFilter = "CDRCST = 1 AND CDLVNO = 9 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SDNO-01":
                    dtSource = DataSource.University;
                    strSQL = "DFSDNO AS CODE, DFSDNA AS DSCR FROM MDF2 ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SHIF-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE, ZRVANA AS DSCR FROM ZVAR ";
                    strSQLFilter = "ZRVATY = 'SHIF' AND ZRRCST = 1";
                    strOrderBy = "ZRVASQ";
                    strOrderDirection = "ASC";
                    break;

                case "SFIT-01":
                    dtSource = DataSource.University;
                    strSQL = "SVITNO AS CODE, SVITNO +' - '+ HMITNA AS DSCR FROM SFIT JOIN IIMA ON 1=1 AND HMCONO = '' AND HMBRNO = '' AND HMITNO = SVITNO ";
                    strSQLFilter = "SVRCST = 1";
                    strOrderBy = "C";
                    strOrderDirection = "ASC";
                    break;

                case "SLTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";
                    
                    strSQLFilter = " ZRVATY = 'SLTY' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SLPG-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE, CBKYNA AS DSCR FROM GCT2 ";
                    strSQLFilter = "CBTBNO = 'SLPG' AND CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SMTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE, ZRVANA AS DSCR FROM ZVAR ";
                    
                    strSQLFilter = "ZRVATY = 'SMTY' AND ZRRCST = 1 AND ZRVANO <> 'SMTY_Daily'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SMTY-02":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE, ZRVANA AS DSCR FROM ZVAR ";
                    
                    strSQLFilter = "ZRVATY = 'SMTY' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;


                case "SSAR-02":
                    dtSource = DataSource.University;
                    strSQL = "SWARNO AS CODE, SWARNA AS DSCR FROM SSAR ";
                    strSQLFilter = "SWRCST = 1 AND SWLVNO = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SSAR-03":
                    dtSource = DataSource.University;
                    strSQL = "SWARNO AS CODE, SWARNA AS DSCR FROM SSAR ";
                    strSQLFilter = "SWRCST = 1 AND SWLVNO = 2";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SSAR-04":
                    dtSource = DataSource.University;
                    strSQL = "SWARNO AS CODE, SWARNA AS DSCR FROM SSAR ";
                    strSQLFilter = "SWRCST = 1 AND SWLVNO = 3";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SSAR-05":
                    dtSource = DataSource.University;
                    strSQL = "SWARNO AS CODE, SWARNA AS DSCR FROM SSAR ";
                    strSQLFilter = "SWRCST = 1 AND SWLVNO = 4";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SYNC-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = " ZRVATY = 'SYNC' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "SYCJ-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "YJJBTY AS CODE "
                                + ", YJJBTZ AS DSCR "
                                + " FROM SYCJ "
                                + " ";

                        strSQLFilter = "YJRCST = 1";
                        strOrderBy = "D";
                        strOrderDirection = "ASC";
                        break;
                    }

                case "SYCP-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "YPPCSQ AS CODE "
                                + ", YPPCNA AS DSCR "
                                + " FROM SYCP "
                                + " ";

                        strSQLFilter = "YPRCST = 1";
                        strOrderBy = "D";
                        strOrderDirection = "ASC";
                        break;
                    }

                case "SYM1-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "YMMPNO AS CODE "
                                + ", YMMPNA AS DSCR "
                                + " FROM SYM1 "
                                + " ";

                        strSQLFilter = "YMRCST = 1";
                        strOrderBy = "D";
                        strOrderDirection = "ASC";
                        break;
                    }

                #endregion

                #region T
                case "TOTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = " ZRVATY = 'TOTY' ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "TPNO-01":
                    dtSource = DataSource.University;
                    strSQL = "GTTPNO AS CODE"
                            + " , GTTPNA AS DSCR"
                            + " FROM GTOP"
                            + "";

                    strSQLFilter = "GTRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "TRTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'TRTY' AND ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "TXTY-01":
                    dtSource = DataSource.University;
                    strSQL = "TATXTY AS CODE,"
                            + " TATYNA AS DSCR"
                            + " FROM TAP1"
                            + "";

                    strSQLFilter = "TARCST=1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region U

                case "UGNO-01":
                    dtSource = DataSource.University;
                    strSQL = "ZGUGNO AS CODE, ZGUGNA AS DSCR FROM ZUG1 ";
                    strSQLFilter = "";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "UMNO-01":
                    dtSource = DataSource.University;
                    strSQL = "HUUMNO as CODE, HUUMNA as DSCR from IUOM";
                    strSQLFilter = "HURCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "UNIT-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'UNIT'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "URTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";

                    strSQLFilter = "ZRVATY = 'URTY'";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                #endregion

                #region V
                case "VCNO-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";
                    
                    strSQLFilter = " CBTBNO = 'VCNO' AND CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "VEGR-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";
                    
                    strSQLFilter = " CBTBNO = 'VEGR' AND CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "VETY-01":
                    dtSource = DataSource.University;
                    strSQL = "CBKYNO AS CODE,"
                            + " CBKYNA AS DSCR"
                            + " FROM GCT2"
                            + "";
                    
                    strSQLFilter = " CBTBNO = 'VETY' AND CBRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region W

                case "WEEK-01":
                    dtSource = DataSource.University;
                    strSQL = " DISTINCT GZWEEK AS CODE, "
                              + " GZWEEK as DSCR"
                              + " FROM  GBM3 "
                              + "";

                    strSQLFilter = " GZRCST = 1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "WEEK-02":
                    dtSource = DataSource.University;
                    strSQL = " DISTINCT GZWEEK AS CODE, "
                              + " GZWEEK as DSCR"
                              + " FROM  GBM3 "
                              + "";

                    strSQLFilter = " GZRCST = 1 ";
                    strOrderBy = "D";
                    strOrderDirection = "DESC";
                    break;

                case "WHNO-01":
                    dtSource = DataSource.University;
                    strSQL = "HWWHNO AS CODE,"
                            + " HWWHNA AS DSCR"
                            + " FROM IWHS"
                            + "";

                    strSQLFilter = " HWRCST=1 ";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;

                case "WHTY-01":
                    dtSource = DataSource.University;
                    strSQL = "ZRVAVL AS CODE,"
                            + " ZRVANA AS DSCR"
                            + " FROM ZVAR"
                            + "";
                    
                    strSQLFilter = "ZRVATY = 'WHTY' and ZRRCST = 1";
                    strOrderBy = "D";
                    strOrderDirection = "ASC";
                    break;
                #endregion

                #region X
                #endregion

                #region Y
                #endregion

                #region Z

                #endregion

                default:
                    dtSource = DataSource.University;
                    strSQL = "";
                    strSQLFilter = "";
                    strOrderBy = "D"; //Value: C (Code) / D (Description) / Field Name
                    strOrderDirection = "ASC"; //Value: ASC (Ascending) / DESC (Descending)
                    break;
            }
        }

        #endregion

        #region Get Data

        public List<DDLDto> GetList(string strEntity, string strFilter, string strKeyCode, string strFilterMsc)
        {
            string strSQL = string.Empty, strSQLFilter = string.Empty, strOrderBy = string.Empty, strOrderDirection = string.Empty;
            DataSource dtSource;

            CreateDDLQuery(strEntity, out dtSource, out strSQL, out strSQLFilter, out strOrderBy, out strOrderDirection);

            if (strEntity == "SSPR-01")
            {
                strSQL = ";WITH cte_SSPR AS"
                         + " ("
                         + " SELECT "
                         + strSQL
                         + " WHERE 1=1"
                         + " AND " + strFilter
                         + " )"
                         + " SELECT"
                         + " CAST(CUSLP1 AS VARCHAR) + '|' + CAST(CUSLP2 AS VARCHAR) + '|' + CAST(CUSLP3 AS VARCHAR) + '|' + CAST(ROW_NUMBER() OVER (ORDER BY CUDTFR DESC) - 1 AS VARCHAR) AS CODE,"
                         + " FORMAT(CUSLP3, '" + BaseMethod.PriceFormat + "') AS DSCR"
                         + " FROM cte_SSPR"
                         + " WHERE 1=1"
                         + " AND RWNO = 1"
                         + " ORDER BY " + strOrderBy + " " + strOrderDirection
                         + "";

                strFilter = string.Empty;
                strOrderBy = string.Empty;
                strOrderDirection = string.Empty;
            }

            List<DDLDto> lst = GetListBase(dtSource, strSQL, strSQLFilter, strOrderBy, strOrderDirection, strFilter, strKeyCode, strFilterMsc);

            //if (strEntity == "PPPR-01" || strEntity == "SSPR-01")
            //{
            //    foreach (DDLDto obj in lst)
            //        obj.DSCR = Convert.ToDecimal(obj.DSCR).ToString(BaseMethod.PriceFormat);
            //}

            return lst;
        }

        #endregion
    }
}
