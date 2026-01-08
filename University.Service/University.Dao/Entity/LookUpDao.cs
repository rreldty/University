using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using University.Dao.Base;
using University.Dto.Base;

namespace University.Dao.Entity
{
    public class LookUpDao : BaseLookUp
    {
        #region Create SQL

        private void CreateSQL(string strEntity, out DataSource dtSource, out string strSQL, out string strSQLFilter, out string strSQLGroup, out string strSQLOrderBy, out string strWindowSize, out string strDecimalColumn, out string strColumnWidth)
        {
            strSQLGroup = "";
            strSQLOrderBy = "";

            dtSource = DataSource.University;
            strWindowSize = "800;480"; //by default Widht 800, Height 480

            //[Column Index]-[Column Format] if more than 1 user ; as separator
            strDecimalColumn = ""; //ex: "3-Date;4-Time;5-Amount;6-Unit"
            strColumnWidth = ""; //ex: "1-100;2-200;3-*" or "1-100;2-200;3-500" >> can be set to fixed width or auto (*) 

            switch (strEntity)
            {
                case "DEVTEST":
                    {
                        dtSource = DataSource.University;
                        strSQL = "ZAAPNO"
                            + ", ZAAPNA"
                            + ", ZRVANA AS ZARCST"
                            + ", ZACRDT"
                            + ", ZACRTM"
                            + ", ZACHDT"
                            + ", ZACHTM"
                            + " FROM ZAPP"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = ZARCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        strDecimalColumn = "3-Date;4-Time;5-Amount;6-Unit";
                        break;
                    }

                #region A

                case "APNO-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "ZAAPNO"
                            + " , ZAAPNA"
                            + " , ZRVANA AS ZARCST"
                            + " FROM ZAPP"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = ZARCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        strDecimalColumn = "";
                        break;
                    }

                case "AJDN-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "IAAJDN"
                            + ", IAAJDT"
                            + ", HWWHNA"
                            + ", HLLONA"
                            + ", ZRVANA AS IARCST"
                            + " FROM IAJ1"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = IARCST"
                            + " LEFT JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IACONO"
                            + "     AND HWBRNO = IABRNO"
                            + "     AND HWWHNO = IAWHNO"
                            + " LEFT JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IACONO"
                            + "     AND HLBRNO = IABRNO"
                            + "     AND HLWHNO = IAWHNO"
                            + "     AND HLLONO = IALONO";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        strDecimalColumn = "1-Date";
                        break;
                    }

                case "ARNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CBKYNO"
                            + ", CBKYNA as CBARNA"
                            + " FROM GCT2"
                            + "";

                        strSQLFilter = " CBTBNO = 'ARNO' ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "AYDN-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "F1AYDN"
                            + ", F1AYDT"
                            + ", F1AYAM"
                            + ", F1REMA"
                            + " FROM FAPY"
                            + "";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        strDecimalColumn = "1-Date;2-Amount";
                        break;
                    }

                #endregion

                #region B
                case "BIDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT"
                            + " FQBIDN"
                            + " , FQBIDT"
                            + " , GIEMNA"
                            + " , FQBITO"
                            + " FROM FBI1"
                            + " LEFT JOIN GEMP ON 1=1"
                            + "     AND GICONO = FQCONO"
                            + "     AND GIEMNO = FQCOLT"
                            + " LEFT JOIN FBI2 ON 1=1"
                            + "     AND FRCONO = FQCONO AND FRBRNO = FQBRNO AND FRBIDN = FQBIDN"
                            + " LEFT JOIN SAR1 ON 1=1"
                            + "     AND SACONO = FQCONO AND SABRNO = FQBRNO AND SAIVDN = FRIVDN";

                        strSQLFilter = "SADCST = '" + BaseMethod.DocStatusOpen + "' ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date;3-Amount";
                        strColumnWidth = "1-100";
                        break;
                    }

                case "BRNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZBCONO"
                            + ", ZBBRNO"
                            + ", ZBBRNA"
                            + " FROM ZBRC";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "BRNO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZBCONO"
                            + ", ZBBRNO"
                            + ", ZBBRNA"
                            + " FROM ZBRC";

                        strSQLFilter = "ZBRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "BRNO-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZBCONO"
                            + ", ZBBRNO"
                            + ", ZBBRNA"
                            + " FROM ZBRC"
                            + " LEFT JOIN ZCMP ON 1=1"
                            + "     AND ZCCONO = ZBCONO";

                        strSQLFilter = "ZBRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "BUNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GBBUNO"
                            + ", GBBUNA"
                            + ", ZRVANA AS GBRCST"
                            + " FROM GBIZ"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GBRCST";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "BUNO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GBBUNO"
                            + ", GBBUNA"
                            + ", ZRVANA AS GBRCST"
                            + " FROM GBIZ"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GBRCST";

                        strSQLFilter = "GBRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }
                #endregion

                #region C

                case "CCNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GFCCNO"
                            + ", GFCCNA"
                            + ", ZRVANA AS GFRCST"
                            + " FROM GCCA"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GFRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CNDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SHCNDN, SHCNDT, SHCUNO, CMCUNA, SHCNAM, SHOUAM"
                            + " FROM SARC"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SHCONO"
                            + "     AND (CMBRNO = SHBRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SHCUNO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = SHDCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 4-Amount; 5-Amount";
                        break;
                    }

                case "CNDN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SHCNDN, SHCNDT, SHCUNO, CMCUNA, SHCNAM, SHOUAM"
                            + " FROM SARC"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SHCONO"
                            + "     AND CMBRNO = SHBRNO"
                            + "     AND CMCUNO = SHCUNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 4-Amount; 5-Amount";
                        break;
                    }

                //Outstanding Credit Note
                case "CNDN-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SHCNDN, SHCNDT, SHCUNO, CMCUNA, SHCNAM, SHOUAM"
                            + " FROM SARC"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SHCONO"
                            + "     AND (CMBRNO = SHBRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SHCUNO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = SHDCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 4-Amount; 5-Amount";
                        break;
                    }

                case "COAC-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "FAACNO"
                            + ", FAACNA"
                            + " FROM FACT"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'ACTY_ASSET'"
                            + "     AND ZRVAVL = FAACTY";

                        strSQLFilter = "FAHDTY = 'D' AND FARCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                case "COAC-02":
                    {
                        dtSource = DataSource.University;
                        strSQL = "FAACNO"
                            + ", FAACNA"
                            + " FROM FACT"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'ACTY_LIABILITY'"
                            + "     AND ZRVAVL = FAACTY";

                        strSQLFilter = "FAHDTY = 'D' AND FARCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                case "COAC-03":
                    {
                        dtSource = DataSource.University;
                        strSQL = "FAACNO"
                            + ", FAACNA"
                            + " FROM FACT"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'ACTY_EQUITY'"
                            + "     AND ZRVAVL = FAACTY";

                        strSQLFilter = "FAHDTY = 'D' AND FARCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                case "COAC-04":
                    {
                        dtSource = DataSource.University;
                        strSQL = "FAACNO"
                            + ", FAACNA"
                            + " FROM FACT"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'ACTY_REVENUE'"
                            + "     AND ZRVAVL = FAACTY";

                        strSQLFilter = "FAHDTY = 'D' AND FARCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                case "COAC-05":
                    {
                        dtSource = DataSource.University;
                        strSQL = "FAACNO"
                            + ", FAACNA"
                            + " FROM FACT"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'ACTY_COS'"
                            + "     AND ZRVAVL = FAACTY";

                        strSQLFilter = "FAHDTY = 'D' AND FARCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                case "COAC-06":
                    {
                        dtSource = DataSource.University;
                        strSQL = "FAACNO"
                            + ", FAACNA"
                            + " FROM FACT"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'ACTY_EXPENSE'"
                            + "     AND ZRVAVL = FAACTY";

                        strSQLFilter = "FAHDTY = 'D' AND FARCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                case "COAC-07":
                    {
                        dtSource = DataSource.University;
                        strSQL = "FAACNO"
                            + ", FAACNA"
                            + " FROM FACT"
                            + "";

                        strSQLFilter = "FAHDTY = 'D' AND FARCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                case "CONO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZCCONO"
                            + ", ZCCONA"
                            + " FROM ZCMP";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CONO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZCCONO"
                            + ", ZCCONA"
                            + " FROM ZCMP";

                        strSQLFilter = "ZCRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CONO-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZCCONO"
                            + ", ZCCONA"
                            + " FROM ZCMP"
                            + " JOIN ZVAR on 1=1"
                            + " AND ZRCONO = ''"
                            + " AND ZRBRNO = ''"
                            + " AND ZRVANO = 'COTY_DISTRIBUTOR'"
                            + " AND ZRVAVL = ZCCOTY"
                            + "";

                        strSQLFilter = "ZCRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CTNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CBKYNO"
                            + ", CBKYNA as CBCTNA"
                            + " FROM GCT2"
                            + "";

                        strSQLFilter = " CBTBNO = 'CTNO' ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUCA-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUGR-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " FROM SCGR"
                            + "";

                        strSQLFilter = "CDLVNO = 1 AND CDRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUGR-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " FROM SCGR"
                            + "";

                        strSQLFilter = "CDLVNO = 2 AND CDRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUGR-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " FROM SCGR"
                            + "";

                        strSQLFilter = "CDLVNO = 3 AND CDRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUGR-04":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " FROM SCGR"
                            + "";

                        strSQLFilter = "CDLVNO = 4 AND CDRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUGR-05":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " FROM SCGR"
                            + "";

                        strSQLFilter = "CDLVNO = 5 AND CDRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUGR-06":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " FROM SCGR"
                            + "";

                        strSQLFilter = "CDLVNO = 6 AND CDRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUGR-07":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " FROM SCGR"
                            + "";

                        strSQLFilter = "CDLVNO = 7 AND CDRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUGR-08":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " FROM SCGR"
                            + "";

                        strSQLFilter = "CDLVNO = 8 AND CDRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUGR-09":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " FROM SCGR"
                            + "";

                        strSQLFilter = "CDLVNO = 9 AND CDRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CUNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CMCUNO"
                            + ", CMCUNA"
                            + ", CMCUOL"
                            + ", CMADR1"
                            + ", CMADR2"
                            + ", CMADR3"
                            + ", ZRVANA AS CMRCST"
                            + " FROM SCMA"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = CMRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strWindowSize = "854;480";
                        strColumnWidth = "0-110;1-150;2-130;3-240;4-150;5-100;6-80";

                        break;
                    }

                case "CUNO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CMCUNO"
                            + " , CMCUNA"
                            + " , CMADR1"
                            + " , CMADR2"
                            + " , CMADR3"
                            + " , ZRVANA AS CMRCST"
                            + " FROM SCMA"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = CMRCST"
                            + "";

                        strSQLFilter = "CMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strWindowSize = "854;480";
                        strColumnWidth = "0-110;1-160;2-240;3-160;4-120;5-80";

                        break;
                    }

                case "CUNO-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CMCUNO"
                            + " , CMCUNA"
                            + " , CMADR1"
                            + " , CMADR2"
                            + " , CMADR3"
                            + " FROM SCMA"
                            + "";

                        strSQLFilter = "CMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strWindowSize = "854;480";
                        strColumnWidth = "0-110;1-160;2-240;3-160;4-120";

                        break;
                    }

                case "CYNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GGCYNO"
                            + ", GGCYNA"
                            + ", ZRVANA AS GGRCST"
                            + " FROM GCUR"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GGRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "CYNO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GGCYNO"
                            + ", GGCYNA"
                            + " FROM GCUR"
                            + "";

                        strSQLFilter = "GGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                #endregion

                #region D

                case "DNDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "PHDNDN, PHDNDT, PHDNAM, PHOUAM, PHVENO, VMVENA"
                            + " FROM PAPD"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = PHCONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = PHVENO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = PHDCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 2-Amount; 3-Amount";

                        break;
                    }

                case "DNDN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "PHDNDN, PHDNDT, PHDNAM, PHOUAM, PHVENO, VMVENA"
                            + " FROM PAPD"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = PHCONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = PHVENO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 2-Amount; 3-Amount";

                        break;
                    }

                case "DODN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SDDODN"
                            + ", SDDODT"
                            + ", SDCUNO"
                            + ", CMCUNA"
                            + ", SDDOAM"
                            + " FROM SDO1"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SDCONO"
                            + "     AND (CMBRNO = SDBRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SDCUNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 4-Amount";
                        strColumnWidth = "0-148;1-88;2-106;3-200;4-120;";

                        break;
                    }

                case "DODN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT SDDODN"
                            + ", SDDODT"
                            + ", SDCUNO"
                            + ", CMCUNA"
                            + " FROM SDO1"
                            + " JOIN SDO2 ON 1=1"
                            + "     AND SECONO = SDCONO"
                            + "     AND SEBRNO = SDBRNO"
                            + "     AND SEDODN = SDDODN"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SDCONO"
                            + "     AND CMBRNO = SDBRNO"
                            + "     AND CMCUNO = SDCUNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "DODN-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SDDODN"
                            + ", SDDODT"
                            + ", SDCUNO"
                            + ", CMCUNA"
                            + ", SDDOAM"
                            + " FROM SDO1"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SDCONO"
                            + "     AND (CMBRNO = SDBRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SDCUNO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO IN ('DCST_OPEN', 'DCST_FINISH')"
                            + "     AND ZRVAVL = SDDCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 4-Amount";
                        strColumnWidth = "0-148;1-88;2-106;3-200;4-120;";

                        break;
                    }

                #endregion

                #region E

                case "EMNO-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "GIEMNO"
                            + ", GIEMNA"
                            + ", ZRVANA AS GIRCST"
                            + " FROM GEMP"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = GICONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = GIBRNO OR ZRBRNO = '')"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GIRCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "EMNO-02":
                    {
                        dtSource = DataSource.University;
                        strSQL = "GIEMNO"
                            + ", GIEMNA"
                            + " FROM GEMP"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = GICONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = GIBRNO OR ZRBRNO = '')"
                            + "     AND ZRVANO = 'EMTY_SALESMAN'"
                            + "     AND ZRVAVL = GIEMTY";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "EMNO-03":
                    {
                        dtSource = DataSource.University;
                        strSQL = "GIEMNO"
                            + ", GIEMNA"
                            + " FROM GEMP"
                            + "";
                        strSQLFilter = "GIRCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                #endregion

                #region F

                case "FACT-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FAACNO, FAACNA"
                            + " FROM FACT"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "FACT-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FAACNO, FAACNA"
                            + " FROM FACT"
                            + "";

                        strSQLFilter = "FARCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "FACT-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FAACNO, FAACNA"
                            + " FROM FACT"
                            + "";

                        strSQLFilter = "FAHDTY = 'D' AND FARCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "FBAM-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FMBKNO"
                            + ", FMBKNA"
                            + ", ZRVANA AS FMRCST"
                            + " FROM FBAM"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = FMRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "FBAM-02":
                    {
                        dtSource = DataSource.University;
                        strSQL = "FMBKNO"
                            + ", FMBKNA"
                            + " FROM FBAM"
                            + "";

                        strSQLFilter = "FMRCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                case "FCOS-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FCCSNO"
                            + ", FCCSNA"
                            + ", ZRVANA AS FCRCST"
                            + " FROM FCOS"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = FCRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "FCOS-02":
                    {
                        dtSource = DataSource.University;
                        strSQL = "FCCSNO"
                            + ", FCCSNA"
                            + " FROM FCOS"
                            + "";

                        strSQLFilter = "FCRCST = 1";
                        strSQLGroup = "";
                        break;
                    }

                case "FWNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "OFFWNO"
                            + ", OFFWNA"
                            + ", OFADR1 AS Address"
                            + ", CBKYNA AS [City Name]"
                            + ", OFPHN1 AS [Phone Number]"
                            + ", ZRVANA AS Status"
                            + " FROM PFWD"
                            + " LEFT JOIN GCT2 ON 1=1"
                            + "     AND (CBCONO = OFCONO OR CBCONO = '')"
                            + "     AND (CBBRNO = OFBRNO OR CBBRNO = '')"
                            + "     AND CBTBNO = 'CTNO'"
                            + "     AND CBKYNO = OFCTNO"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = OFRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "FWNO-02":
                    {
                        dtSource = DataSource.University;
                        strSQL = "OFFWNO"
                            + ", OFFWNA"
                            + " FROM PFWD"
                            + "";

                        strSQLFilter = "OFRCST = 1";
                        strSQLGroup = "";
                        break;
                    }


                #endregion

                #region G

                case "GADR-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GEADNO"
                            + " , GEADNA"
                            + " , GEADR1"
                            + " , GEADR2"
                            + " , GEADR3"
                            + " FROM GADR"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GADR-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GEADNO"
                            + " , GEADNA"
                            + " , GEADR1"
                            + " , GEADR2"
                            + " , GEADR3"
                            + " FROM GADR"
                            + "";

                        strSQLFilter = "GERCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GCCA-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GFCCNO"
                            + " , GFCCNA"
                            + " FROM GCCA"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GCCA-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GFCCNO"
                            + ", GFCCNA"
                            + ", ZRVANA AS GFRCST"
                            + " FROM GCCA"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GFRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GCT1-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CATBNO"
                            + " , CATBNA"
                            + " , ZRVANA AS CARCST"
                            + " FROM GCT1"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = CARCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GCT2-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CBKYNO"
                            + " , CBKYNA"
                            + " , CATBNO"
                            + " , CATBNA"
                            + " , ZRVANA AS CBRCST"
                            + " FROM GCT2"
                            + " LEFT JOIN GCT1 ON 1=1"
                            + "     AND CACONO = CBCONO"
                            + "     AND CABRNO = ''"
                            + "     AND CATBNO = CBTBNO"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = CBRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GDEP-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GDDENO"
                            + ", GDDENA"
                            + ", ZRVANA AS GDRCST"
                            + " FROM GDEP"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GDRCST";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GDM1-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "D1DFNO"
                            + ", D1DFNA"
                            + ", ZRVANA AS D1RCST"
                            + " FROM GDM1"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = D1CONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = D1BRNO OR ZRBRNO = '')"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = D1RCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "GEGR-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "GQEMGR"
                            + ", GQGRNA"
                            + ", GQLVNO"
                            + ", ZRVANA AS GQRCST"
                            + " FROM GEGR"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = GQCONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = GQBRNO OR ZRBRNO = '')"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GQRCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "GEMM-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GMVENO"
                            + " , VMVENA"
                            + " , GMEMFR "
                            + " , GMEMTO "
                            + " , GIEMNA "
                            + " FROM GEMM"
                            + " LEFT JOIN PVMA on 1=1"
                            + " AND VMCONO = ''"
                            + " AND VMBRNO = ''"
                            + " AND VMVENO = GMVENO"
                            + " LEFT JOIN GEMP ON 1=1"
                            + " AND GIEMNO = GMEMTO"
                            + "";

                        strSQLFilter = "GMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GEMP-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "GIEMNO"
                            + ", GIEMNA"
                            + ", A.ZRVANA as GIEMTY"
                            + ", B.ZRVANA as GISLTY"
                            + ", C.ZRVANA AS GIRCST"
                            + " FROM GEMP"
                            + " LEFT JOIN ZVAR A ON 1=1"
                            + "     AND (A.ZRCONO = GICONO OR A.ZRCONO = '')"
                            + "     AND (A.ZRBRNO = GIBRNO OR A.ZRBRNO = '')"
                            + "     AND A.ZRVATY = 'EMTY'"
                            + "     AND A.ZRVAVL = GIEMTY"
                            + " LEFT JOIN ZVAR B ON 1=1"
                            + "     AND (B.ZRCONO = GICONO OR B.ZRCONO = '')"
                            + "     AND (B.ZRBRNO = GIBRNO OR B.ZRBRNO = '')"
                            + "     AND B.ZRVATY = 'SLTY'"
                            + "     AND B.ZRVAVL = GISLTY"
                            + " LEFT JOIN ZVAR C ON 1=1"
                            + "     AND (C.ZRCONO = GICONO OR C.ZRCONO = '')"
                            + "     AND (C.ZRBRNO = GIBRNO OR C.ZRBRNO = '')"
                            + "     AND C.ZRVATY = 'RCST'"
                            + "     AND C.ZRVAVL = GIRCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "GEMP-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GIEMNO"
                            + ", GIEMNA"
                            + " FROM GEMP"
                            + "";

                        strSQLFilter = "GIRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "GEPM-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GJPERD"
                            + " , GJEMNO"
                            + " , EMEMNA"
                            + " , GJITNO"
                            + " , HMITNA"
                            + " , ZRVANA as GJRCST"
                            + " FROM GEPM"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = GJCONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = GJBRNO OR ZRBRNO = '')"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GJRCST"
                            + " LEFT JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = '' OR HMCONO = GJCONO)"
                            + "     AND (HMBRNO = '' OR HMBRNO = GJBRNO)"
                            + "     AND HMITNO = GJITNO"
                            + " LEFT JOIN GEMP ON 1=1"
                            + "     AND (EMCONO = '' OR EMCONO = GJCONO)"
                            + "     AND (EMBRNO = '' OR EMBRNO = GJBRNO)"
                            + "     AND EMEMNO = GJEMNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GERS-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "GKPERD"
                            + ", GKEMNO"
                            + ", GKITGR"
                            + ", GKCONO"
                            + ", GKBRNO"
                            + ", ZRVANA AS GKRCST"
                            + " FROM GERS"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = GKCONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = GKBRNO OR ZRBRNO = '')"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GKRCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "";
                        break;
                    }

                case "GMP1-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "M1MPNO"
                            + " , M1MPNA"
                            + " , M1MPTY"
                            + " FROM GMP1"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GMP2-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "M2MPNO"
                            + " , M1MPNA"
                            + " , M2MPFR"
                            + " , M2MPTO"
                            + " FROM GMP2"
                            + " LEFT JOIN GMP1 on 1=1"
                            + " AND M1CONO = M2CONO"
                            + " AND M1BRNO = M2BRNO"
                            + " AND M1MPNO = M2MPNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GRDN-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "PDGRDN"
                            + ", PDGRDT"
                            + ", PDVENO"
                            + ", VMVENA"
                            + ", PDPODN"
                            + " FROM PRC1"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = PDCONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = PDVENO";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";
                        break;
                    }

                case "GRDN-02":
                    {
                        dtSource = DataSource.University;
                        strSQL = "DISTINCT PDGRDN"
                            + ", PDGRDT"
                            + ", PDVENO"
                            + ", VMVENA"
                            + " FROM PRC1"
                            + " JOIN PRC2 ON 1=1"
                            + "     AND PECONO = PDCONO"
                            + "     AND PEBRNO = PDBRNO"
                            + "     AND PEGRDN = PDGRDN"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = PDCONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = PDVENO";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";
                        strColumnWidth = "0-120;1-130;2-100;3-*";
                        break;
                    }

                case "GRES-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GRRSTB"
                            + " , GRRSNO"
                            + " , GRRSNA"
                            + " , ZRVANA AS GRRCST"
                            + " FROM GRES"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + " AND ZRVATY  = 'RCST'"
                            + " AND ZRVAVL  = GRRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GRSF-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "G2RSTY"
                            + ", G2SFRS"
                            + ", G2RSNA"
                            + ", ZRVANA AS G2RCST"
                            + " FROM GRSF"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = G2RCST";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GOSF-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "G3OTNO"
                            + ", G3OTNA"
                            + ", G3OTCA"
                            + ", G3SINV"
                            + ", ZRVANA AS G2RCST"
                            + " FROM GOSF"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = G3RCST";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GTOD-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GWTDNO"
                            + " , GWTDNA"
                            + " , GWTDDY"
                            + " FROM GTOD"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GTOP-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GTTPNO"
                            + " , GTTPNA"
                            + " , GTTPDY"
                            + " FROM GTOP"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "GTOP-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GTTPNO"
                            + " , GTTPNA"
                            + " , GTTPDY"
                            + " FROM GTOP"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }
                #endregion

                #region H
                #endregion

                #region I
                //Incoming Bank Outstanding Type Check
                case "IBDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FKIBDN, FKCENO, FKCEDT, FKDUDT, FKCEAM - FKUSAM AS FKOUAM"
                            + " FROM FIBK"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'IBTY_CHECK'"
                            + "     AND ZRVAVL = FKIBTY"
                            + " JOIN SCMA ON 1=1 "
                            + "     AND CMCONO = FKCONO"
                            + "     AND CMBRNO = FKBRNO"
                            + "     AND CMCUNO = FKCUNO"
                            + "";

                        strSQLFilter = "FKVOID = 0 AND FKCLDT <> 0 AND FKCEAM - FKUSAM > 0";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Date; 3-Date; 4-Amount";

                        break;
                    }

                //Incoming Bank Outstanding Type Transfer
                case "IBDN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FKIBDN, FKCENO, FKCEDT, FKDUDT, FKCEAM - FKUSAM AS FKOUAM"
                            + " FROM FIBK"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'IBTY_TRANSFER'"
                            + "     AND ZRVAVL = FKIBTY"
                            + " JOIN SCMA ON 1=1 "
                            + "     AND CMCONO = FKCONO"
                            + "     AND CMBRNO = FKBRNO"
                            + "     AND CMCUNO = FKCUNO"
                            + "";

                        strSQLFilter = "FKVOID = 0 AND FKCLDT <> 0 AND FKCEAM - FKUSAM > 0";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Date; 3-Date; 4-Amount";

                        break;
                    }

                //Incoming Bank No.
                case "IBDN-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FKIBDN, FKCENO, FKCEDT, FKDUDT, FKCEAM - FKUSAM AS FKOUAM"
                            + " FROM FIBK"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'IBTY'"
                            + "     AND ZRVAVL = FKIBTY"
                            + " JOIN SCMA ON 1=1 "
                            + "     AND CMCONO = FKCONO"
                            + "     AND CMBRNO = FKBRNO"
                            + "     AND CMCUNO = FKCUNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Date; 3-Date; 4-Amount";

                        break;
                    }

                case "IBPL-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = " DISTINCT"
                            + " HPPENO"
                            + " , HPPENA"
                            + " , CBKYNA AS HPPETY"
                            + " FROM IBPL"
                            + " LEFT JOIN IPAL ON 1=1 "
                            + "     AND HPCONO = LDCONO"
                            + "     AND HPBRNO = LDBRNO"
                            + "     AND HPPENO = LDPENO"
                            + " LEFT JOIN GCT2 ON 1=1 "
                            + "     AND (CBCONO = LDCONO OR CBCONO = '')"
                            + "     AND (CBBRNO = LDBRNO OR CBBRNO = '')"
                            + "     AND CBTBNO = 'PETY'"
                            + "     AND CBKYNO = HPPETY"
                            + "";

                        strSQLFilter = "HPRCST = 1 AND LDPERD >= " + BaseMethod.GetVariableValue("PERD_INCOS") + " AND LDAAQT > 0";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IBPL-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT"
                            + "   HMITNO"
                            + " , HMITNA"
                            + " , HMITUM"
                            + " FROM IBPL"
                            + " LEFT JOIN IIMA ON 1=1 "
                            + "     AND (HMCONO = LDCONO OR HMCONO = '')"
                            + "     AND (HMBRNO = LDBRNO OR HMBRNO = '')"
                            + "     AND HMITNO = LDITNO"
                            + "";

                        strSQLFilter = "HMRCST = 1 AND LDPERD >= " + BaseMethod.GetVariableValue("PERD_INCOS") + " AND LDAAQT > 0";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IBM1-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HEITNO"
                            + " , HMITNA"
                            + " , ZRVANA AS HERCST"
                            + " FROM IBM1"
                            + " LEFT JOIN IIMA ON 1=1 "
                            + "     AND (HMCONO = HECONO OR HMCONO = '')"
                            + "     AND (HMBRNO = HEBRNO OR HMBRNO = '')"
                            + "     AND HMITNO = HEITNO"
                            + " LEFT JOIN ZVAR ON 1=1 "
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HERCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IBM1-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HFMTNO"
                            + ", A.HMITNA AS MTITNA"
                            + ", HFITNO"
                            + ", B.HMITNA AS HMITNA"
                            + ", ZRVANA"
                            + " FROM IBM2"
                            + " LEFT JOIN IBM1 ON 1=1"
                            + " AND HECONO = HFCONO"
                            + " AND HEBRNO = HFBRNO"
                            + " AND HEITNO = HFITNO"
                            + " LEFT JOIN IIMA A ON 1=1"
                            + " AND(A.HMCONO = HFCONO OR A.HMCONO = '')"
                            + " AND(A.HMBRNO = HFBRNO OR A.HMBRNO = '')"
                            + " AND A.HMITNO = HFMTNO"
                            + " LEFT JOIN IIMA B ON 1=1"
                            + " AND(B.HMCONO = HFCONO OR B.HMCONO = '')"
                            + " AND(B.HMBRNO = HFBRNO OR B.HMBRNO = '')"
                            + " AND B.HMITNO = HFITNO"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + " AND ZRCONO = ''"
                            + " AND ZRBRNO = ''"
                            + " AND ZRVATY = 'RCST'"
                            + " AND ZRVAVL = HERCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC01-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 1 ";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC02-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " LEFT JOIN IHIR ON 1=1 "
                            + "     AND (HACONO = HHCONO OR HACONO = '')"
                            + "     AND (HABRNO = HHBRNO OR HABRNO = '')"
                            + "     AND HAITCA = HHIC02"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 2";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC03-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " LEFT JOIN IHIR ON 1=1 "
                            + "     AND (HACONO = HHCONO OR HACONO = '')"
                            + "     AND (HABRNO = HHBRNO OR HABRNO = '')"
                            + "     AND HAITCA = HHIC03"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 3 ";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC04-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " LEFT JOIN IHIR ON 1=1 "
                            + "     AND (HACONO = HHCONO OR HACONO = '')"
                            + "     AND (HABRNO = HHBRNO OR HABRNO = '')"
                            + "     AND HAITCA = HHIC04"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 4 ";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC05-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " LEFT JOIN IHIR ON 1=1 "
                            + "     AND (HACONO = HHCONO OR HACONO = '')"
                            + "     AND (HABRNO = HHBRNO OR HABRNO = '')"
                            + "     AND HAITCA = HHIC05"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 5 ";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC06-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " LEFT JOIN IHIR ON 1=1 "
                            + "     AND (HACONO = HHCONO OR HACONO = '')"
                            + "     AND (HABRNO = HHBRNO OR HABRNO = '')"
                            + "     AND HAITCA = HHIC06"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 6 ";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC07-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " LEFT JOIN IHIR ON 1=1 "
                            + "     AND (HACONO = HHCONO OR HACONO = '')"
                            + "     AND (HABRNO = HHBRNO OR HABRNO = '')"
                            + "     AND HAITCA = HHIC07"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 7 ";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC08-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " LEFT JOIN IHIR ON 1=1 "
                            + "     AND (HACONO = HHCONO OR HACONO = '')"
                            + "     AND (HABRNO = HHBRNO OR HABRNO = '')"
                            + "     AND HAITCA = HHIC08"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 8 ";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC09-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " LEFT JOIN IHIR ON 1=1 "
                            + "     AND (HACONO = HHCONO OR HACONO = '')"
                            + "     AND (HABRNO = HHBRNO OR HABRNO = '')"
                            + "     AND HAITCA = HHIC09"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 9 ";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC10-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + "";

                        strSQLFilter = "HARCST = 1 AND HALVNO = 10 ";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }


                case "IC01-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT "
                            + "   HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " JOIN IHIR ON 1=1 "
                            + "         AND HHCONO = '' "
                            + "         AND HHBRNO = '' "
                            + "         AND HHPRNO = HAPRNO "
                            + "         AND HHIC01 = HAITCA "
                            + "		    AND HALVNO = 1 "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC02-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT "
                            + "   HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " JOIN IHIR ON 1=1 "
                            + "         AND HHCONO = '' "
                            + "         AND HHBRNO = '' "
                            + "         AND HHPRNO = HAPRNO "
                            + "         AND HHIC02 = HAITCA "
                            + "		    AND HALVNO = 2 "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC03-02":
                    {
                        dtSource = DataSource.University;


                        strSQL = "DISTINCT "
                            + "HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " JOIN IHIR ON 1=1 "
                            + "         AND HHCONO = '' "
                            + "         AND HHBRNO = '' "
                            + "         AND HHPRNO = HAPRNO "
                            + "         AND HHIC03 = HAITCA "
                            + "		    AND HALVNO = 3 "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC04-02":
                    {
                        dtSource = DataSource.University;


                        strSQL = "DISTINCT "
                            + "HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " JOIN IHIR ON 1=1 "
                            + "         AND HHCONO = '' "
                            + "         AND HHBRNO = '' "
                            + "         AND HHPRNO = HAPRNO "
                            + "         AND HHIC04 = HAITCA "
                            + "		    AND HALVNO = 4 "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC05-02":
                    {
                        dtSource = DataSource.University;


                        strSQL = "DISTINCT "
                            + "HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " JOIN IHIR ON 1=1 "
                            + "         AND HHCONO = '' "
                            + "         AND HHBRNO = '' "
                            + "         AND HHPRNO = HAPRNO "
                            + "         AND HHIC05 = HAITCA "
                            + "		    AND HALVNO = 5 "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC06-02":
                    {
                        dtSource = DataSource.University;


                        strSQL = "DISTINCT "
                            + "HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " JOIN IHIR ON 1=1 "
                            + "         AND HHCONO = '' "
                            + "         AND HHBRNO = '' "
                            + "         AND HHPRNO = HAPRNO "
                            + "         AND HHIC06 = HAITCA "
                            + "		    AND HALVNO = 6 "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC07-02":
                    {
                        dtSource = DataSource.University;


                        strSQL = "DISTINCT "
                            + "HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " JOIN IHIR ON 1=1 "
                            + "         AND HHCONO = '' "
                            + "         AND HHBRNO = '' "
                            + "         AND HHPRNO = HAPRNO "
                            + "         AND HHIC07 = HAITCA "
                            + "		    AND HALVNO = 7 "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC08-02":
                    {
                        dtSource = DataSource.University;


                        strSQL = "DISTINCT "
                            + "HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " JOIN IHIR ON 1=1 "
                            + "         AND HHCONO = '' "
                            + "         AND HHBRNO = '' "
                            + "         AND HHPRNO = HAPRNO "
                            + "         AND HHIC08 = HAITCA "
                            + "		    AND HALVNO = 8 "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IC09-02":
                    {
                        dtSource = DataSource.University;


                        strSQL = "DISTINCT "
                            + "HAITCA"
                            + " , HACANA"
                            + " FROM ICAT"
                            + " JOIN IHIR ON 1=1 "
                            + "         AND HHCONO = '' "
                            + "         AND HHBRNO = '' "
                            + "         AND HHPRNO = HAPRNO "
                            + "         AND HHIC09 = HAITCA "
                            + "		    AND HALVNO = 9 "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ICAT-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HALVNO"
                            + ", HAITCA"
                            + ", HACANA"
                            + ", ZRVANA AS HARCST"
                            + " FROM ICAT"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HARCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strColumnWidth = "0-80; 1-100; 2-500; 3-100";

                        break;
                    }

                case "ICLS-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HCITCL"
                            + " , HCCLNA"
                            + " FROM ICLS"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ICTF-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "I1CINO"
                            + " , I1CINA"
                            + " , I1RCST"
                            + " FROM ICTF"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ICTF-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "I1CINO"
                            + " , I1CINA"
                            + " FROM ICTF"
                            + "";

                        strSQLFilter = "I1RCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IGLO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HDLCIT"
                            + ", HDLCIN"
                            + ", HMITNA"
                            + ", ZRVANA AS HDRCST"
                            + ", HDCONO"
                            + " FROM IGLO"
                            + " LEFT JOIN IIMA ON 1=1 "
                            + "     AND (HMCONO = HDCONO OR HMCONO = '')"
                            + "     AND (HMBRNO = HDBRNO OR HMBRNO = '')"
                            + "     AND HMITNO = HDITNO"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = HDCONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = HDBRNO OR ZRBRNO = '')"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HDRCST"
                            + "";

                        strSQLFilter = "HMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IGRP-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HGITGR"
                            + ", HGGRNA"
                            + ", HGLVNO"
                            + ", ZRVANA AS HGRCST"
                            + " FROM IGRP"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HGRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IHIR-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HHIHNO"
                            + ", HHIHNA"
                            + ", ZRVANA AS HHRCST"
                            + " FROM IHIR"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HHRCST";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IHIR-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HHIHNO"
                            + " , HHIHNA"
                            + " FROM IHIR"
                            + "";

                        strSQLFilter = "HHRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IIMC-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "HOITNO"
                            + ", HMITNA"
                            + ", HWWHNA"
                            + ", HLLONA"
                            + ", ZRVANA AS HORCST"
                            + " FROM IIMC"
                            + " LEFT JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = HMCONO OR HOCONO = '')"
                            + "     AND (HMBRNO = HMBRNO OR HOBRNO = '')"
                            + "     AND HMITNO = HOITNO"
                            + " LEFT JOIN IWHS ON 1=1"
                            + "     AND HWCONO = HOCONO"
                            + "     AND HWBRNO = HOBRNO"
                            + "     AND HWWHNO = HOWHNO"
                            + " LEFT JOIN ILOC ON 1=1"
                            + "     AND HLCONO = HOCONO"
                            + "     AND HLBRNO = HOBRNO"
                            + "     AND HLWHNO = HOWHNO"
                            + "     AND HLLONO = HOLONO"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HORCST";

                        strSQLFilter = "HORCST = 1 ";
                        strSQLGroup = "";
                        strDecimalColumn = "";

                        break;
                    }

                case "ILOC-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HLLONO"
                            + ", HLLONA"
                            + ", HLWHNO"
                            + ", HWWHNA"
                            + ", ZRVANA AS HLRCST"
                            + " FROM ILOC"
                            + " LEFT JOIN IWHS ON 1=1"
                            + "     AND HWCONO = HLCONO"
                            + "     AND HWBRNO = HLBRNO"
                            + "     AND HWWHNO = HLWHNO"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HLRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ILOC-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HLLONO"
                            + " , HLLONA"
                            + " FROM ILOC"
                            + "";

                        strSQLFilter = "HLRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IPAL-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HPPENO"
                            + " , HPPENA"
                            + " FROM IPAL"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IPAL-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HPPENO"
                            + " , HPPENA"
                            + " FROM IPAL "
                            + "";

                        strSQLFilter = "HPRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IPDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FIIPDN"
                            + " , FIIPDT"
                            + " , FICUNO"
                            + " , CMCUNA"
                            + " FROM FIP1 "
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = FICONO"
                            + "     AND CMBRNO = FIBRNO"
                            + "     AND CMCUNO = FICUNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "IRDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "PAIRDN, PAIRDT, PAVENO, VMVENA"
                            + " FROM PAP1"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = PACONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = PAVENO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = PADCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "IRDN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "PAIRDN, PAIRDT, PARFDN, PAIVAM, PAOUAM, PAVENO, VMVENA"
                            + " FROM PAP1"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = PACONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = PAVENO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 3-Amount; 4-Amount";

                        break;
                    }

                //Outgoing Payment Page C
                case "IRDN-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "PAIRDN, PAIRDT, PARFDN AS PDVDON , PAIVAM, PAOUAM, PAVENO, VMVENA"
                            + " FROM PAP1"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = PACONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = PAVENO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = PADCST"
                            + "";

                        strSQLFilter = "PAIRDN NOT IN ("
                                    + "     Select Distinct FPIRDN"
                                    + "     from FOP2"
                                    + "     where 1=1"
                                    + "         and FPCONO = PACONO"
                                    + "         and FPBRNO = PABRNO"
                                    + "         and FPIRDN = PAIRDN"
                                    + "         and FPDCST = 'D'"
                                    + "         and PAOUAM - FPPYAM = 0"
                                    + "     )";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 3-Amount; 4-Amount";
                        strColumnWidth = "1-145";

                        break;
                    }

                case "IRES-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HRRSNO"
                            + " , HRRSNA"
                            + " , HRTRTY"
                            + " FROM IRES"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ITNO-00":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , ZRVANA AS HMRCST"
                            + " FROM IIMA"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = HMCONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = HMBRNO OR ZRBRNO = '')"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HMRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ITNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , ZRVANA AS HMRCST"
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = HMCONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = HMBRNO OR ZRBRNO = '')"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HMRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ITNO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMIUC3"
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + " LEFT JOIN IIMC ON 1=1"
                            + "     AND HOBRNO = ''"
                            + "     AND HOITNO = HMITNO"
                            + "";

                        strSQLFilter = "HMRCST = 1 AND HORCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMITUM"
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "";

                        strSQLFilter = "HMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ITNO-04":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IPITNO"
                            + " , HMITNA"
                            + " , IPITQT - IPTIQT AS IPTOCQ"
                            + " , IPITUM"
                            + " , IPTOLN"
                            + " FROM ITO2"
                            + " LEFT JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = IPCONO OR HMCONO = '') "
                            + "     AND (HMBRNO = IPBRNO OR HMBRNO = '') "
                            + "     AND HMITNO = IPITNO "
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-05":
                    {
                        dtSource = DataSource.University;
                        strSQL = "HMITNO"
                               + ", HMITNA"
                               + ", HMSUM1"
                               + ", HMSUM2"
                               + ", HMSUM3"
                               + " FROM IIMA"
                               + " JOIN IHIR ON 1=1"
                               + "     AND HHCONO = ''"
                               + "     AND HHBRNO = ''"
                               + "     AND HHIHNO = HMIHNO"
                               + "";

                        strSQLFilter = "HMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "ITNO-06":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMIUC3"
                            + " FROM GECP"
                            + " JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = GNCONO or HMCONO = '')"
                            + "     AND (HMBRNO = GNBRNO or HMBRNO = '')"
                            + "     AND HMITNO = GNITNO"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + " LEFT JOIN IIMC ON 1=1"
                            + "     AND HOBRNO = ''"
                            + "     AND HOITNO = HMITNO"
                            + "";

                        strSQLFilter = "HMRCST = 1 AND ISNULL(HOSTST, 'I') <> 'N' AND GNRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-07":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMIUC3"
                            + " FROM SAR2"
                            + " JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = SSCONO or HMCONO = '')"
                            + "     AND (HMBRNO = SSBRNO or HMBRNO = '')"
                            + "     AND HMITNO = SSITNO"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "";

                        strSQLFilter = "SSDCST = '" + BaseMethod.DocStatusOpen + "' AND SSSLQT - SSSNQT > 0";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-08":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMIUC3"
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "";

                        strSQLFilter = "HMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-09":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMIUC3"
                            + " FROM SDO2"
                            + " JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = SECONO OR HMCONO = '')"
                            + "     AND (HMBRNO = SEBRNO OR HMBRNO = '')"
                            + "     AND HMITNO = SEITNO"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-10":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMIUC3"
                            + " FROM SDO3"
                            + " JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = SFCONO OR HMCONO = '')"
                            + "     AND (HMBRNO = SFBRNO OR HMBRNO = '')"
                            + "     AND HMITNO = SFITNO"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "";

                        strSQLFilter = "SFDCST = '" + BaseMethod.DocStatusOpen + "' AND SFPMQT - SFDRQT > 0";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-11":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMIUC3"
                            + " FROM SSO2"
                            + " JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = SPCONO OR HMCONO = '')"
                            + "     AND (HMBRNO = SPBRNO OR HMBRNO = '')"
                            + "     AND HMITNO = SPITNO"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-12":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMIUC3"
                            + " FROM SSO3"
                            + " JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = SQCONO OR HMCONO = '')"
                            + "     AND (HMBRNO = SQBRNO OR HMBRNO = '')"
                            + "     AND HMITNO = SQITNO"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "";

                        strSQLFilter = "SQDCST = '" + BaseMethod.DocStatusOpen + "' AND SQPMQT - SQDOQT - SQPMCQ > 0";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-13":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMVENO"
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "";

                        strSQLFilter = "HMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "";

                        break;
                    }

                case "ITNO-14":
                    {
                        dtSource = DataSource.University;
                        strSQL = "DISTINCT "
                                + " HMITNO"
                                + " , HMITNA "
                                + "  FROM IIMA "
                                + " JOIN PVMI ON 1=1 "
                                + "     AND VICONO = ''"
                                + "     AND VIBRNO = ''"
                                + "     AND VIITNO = HMITNO "
                               + "";

                        strSQLFilter = "HMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "ITNO-15":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , HMITUM"
                            + " , HMIUM2"
                            + " , HMIUM3"
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + " LEFT JOIN IIMC ON 1=1"
                            + "     AND HOBRNO = ''"
                            + "     AND HOITNO = HMITNO"
                            + "";

                        strSQLFilter = "HMRCST = 1 AND ISNULL(HOSTST, 'I') <> 'N'";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-16":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , S9PMPN"
                            + " , S9PMLN"
                            + " FROM SCPP"
                            + " JOIN SPM1 ON 1=1"
                            + "     AND S1CONO = ''"
                            + "     AND S1BRNO = ''"
                            + "     AND S1PMNO = CJPMNO"
                            + " JOIN SPM4 ON 1=1"
                            + "     AND S4CONO = S1CONO"
                            + "     AND S4BRNO = S1BRNO"
                            + "     AND S4PMNO = CJPMNO"
                            + "     AND S4PMLN = CJP4LN"
                            + " JOIN SPM9 ON 1=1"
                            + "     AND S9CONO = S4CONO"
                            + "     AND S9BRNO = S4BRNO"
                            + "     AND S9PMNO = S4PMNO"
                            + "     AND S9PMLN = S4PMLN"
                            + " LEFT JOIN IIMA ON 1=1"
                            + "     AND HMCONO = ''"
                            + "     AND HMBRNO = ''"
                            + "     AND HMITNO = S9ITNO"
                            + " LEFT JOIN IIMC ON 1=1"
                            + "     AND HOBRNO = ''"
                            + "     AND HOITNO = HMITNO"
                            + "";

                        strSQLFilter = "ISNULL(HOSTST, 'I') <> 'N' AND HMRCST = 1 AND S1RCST = 1 AND S4RCST = 1 AND S9RCST = 1 AND CJOUPN > S9PMPN";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-17":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , LDAAQT"
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "  JOIN IIMC ON 1=1"
                            + "     AND HOBRNO = ''"
                            + "     AND HOITNO = HMITNO"
                            + " LEFT JOIN IBPL ON 1=1"
                            + "     AND LDCONO = HOCONO"
                            + "     AND LDITNO = HMITNO"
                            + "";

                        strSQLFilter = "HMRCST = 1 AND HORCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-18":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , LDAAQT"
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + " LEFT JOIN IIMC ON 1=1"
                            + "     AND HOBRNO = ''"
                            + "     AND HOITNO = HMITNO"
                            + " LEFT JOIN IBPL ON 1=1"
                            + "     AND LDITNO = HMITNO"
                            + "";

                        strSQLFilter = "HMRCST = 1 AND HORCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-19": //ITNO PURC. RECEIPT & PO
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , ISNULL((SELECT SUM(LDAAQT) FROM IWHS JOIN IBPL ON LDCONO = HWCONO AND LDBRNO = HWBRNO AND LDWHNO = HWWHNO AND LDPERD = HWPERD WHERE LDCONO = HOCONO AND LDITNO = HOITNO), 0) AS LDAAQT "
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "  JOIN IIMC ON 1=1"
                            + "     AND HOBRNO = ''"
                            + "     AND HOITNO = HMITNO"

                            + "";

                        strSQLFilter = "HMRCST = 1 AND HORCST = 1 AND HMPUFL = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-20": //ITNO SO All Item
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , ISNULL((SELECT SUM(LDAAQT) FROM IWHS JOIN IBPL ON LDCONO = HWCONO AND LDBRNO = HWBRNO AND LDWHNO = HWWHNO AND LDPERD = HWPERD WHERE LDCONO = HOCONO AND LDITNO = HOITNO), 0) AS LDAAQT "
                            + " FROM IIMA"
                            + " JOIN IHIR ON 1=1"
                            + "     AND HHCONO = ''"
                            + "     AND HHBRNO = ''"
                            + "     AND HHIHNO = HMIHNO"
                            + "  JOIN IIMC ON 1=1"
                            + "     AND HOBRNO = ''"
                            + "     AND HOITNO = HMITNO"

                            + "";

                        strSQLFilter = "HMRCST = 1 AND HORCST = 1 AND HMSEFL = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Unit";

                        break;
                    }

                case "ITNO-21":
                    {
                        dtSource = DataSource.University;

                        strSQL = " HMITNO, HMITNA, CUITPR"
                            + " FROM"
                            + " ("
                            + " SELECT ZBCONO, ZBBRNO, HMITNO, HMITNA, HWPERD, CUITPR, ROW_NUMBER() OVER(PARTITION BY ZBCONO, ZBBRNO, HMITNO ORDER BY CUDTFR DESC) ROWNO"
                            + " FROM IIMA"
                            + " FULL JOIN ZBRC ON 1=1"
                            + " JOIN IIMC ON 1=1"
                            + " AND HOCONO = ZBCONO"
                            + " AND HOBRNO = ''"
                            + " AND HOITNO = HMITNO"
                            + " AND HORCST = 1"
                            + " LEFT JOIN IWHS ON 1=1"
                            + " AND HWCONO = ZBCONO"
                            + " AND HWBRNO = ZBBRNO"
                            + " AND HWWHNO = 'MW'"
                            + " LEFT JOIN SSPR ON 1 = 1"
                            + " AND(CUCONO = ZBCONO OR CUCONO = '')"
                            + " AND(CUBRNO = ZBBRNO OR CUBRNO = '')"
                            + " AND CUSLPG = ZBSLPG"
                            + " AND CUDTFR <= HWPERD"
                            + " AND CUDTTO >= HWPERD"
                            + " AND CUITNO = HMITNO"
                            + " AND CUSLUM = HMSLUM"
                            + " AND CURCST = 1"
                            + " WHERE 1 = 1"
                            + " AND HMRCST = 1"
                            + " AND HMSEFL = 1"
                            + " ) A"
                            + "";

                        strSQLFilter = "ROWNO = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "2-Amount";

                        break;
                    }

                case "ITO1-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT "
                            + "   IOTODN"
                            + " , IOTODT"
                            + " , IOWHFR"
                            + " , WF.HWWHNA IOWFNA"
                            + " , IOWHTO"
                            + " , WT.HWWHNA IOWTNA"
                            + " FROM ITO1"
                            + " LEFT JOIN IWHS AS WF ON 1=1"
                            + "     AND (WF.HWCONO = IOCONO OR WF.HWCONO = '')"
                            + "     AND (WF.HWBRNO = IOBRNO OR WF.HWBRNO = '')"
                            + "     AND WF.HWWHNO = IOWHFR"
                            + " LEFT JOIN IWHS AS WT ON 1=1"
                            + "     AND (WT.HWCONO = IOCONO OR WT.HWCONO = '')"
                            + "     AND (WT.HWBRNO = IOBRNO OR WT.HWBRNO = '')"
                            + "     AND WT.HWWHNO = IOWHTO "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "ITR1-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = " DISTINCT"
                            + " IRTQDN"
                            + ", IRTQDT"
                            + ", IRWHFR"
                            + ", WF.HWWHNA IRWFNA"
                            + ", IRWHTO"
                            + ", WT.HWWHNA IRWTNA"
                            + " FROM ITR1"
                            + " LEFT JOIN ITR2 ON 1=1"
                            + "     AND ISCONO = IRCONO"
                            + "     AND ISBRNO = IRBRNO"
                            + "     AND ISTQDN = IRTQDN"
                            + " LEFT JOIN IWHS AS WF ON 1=1"
                            + "     AND WF.HWCONO = IRCONO"
                            + "     AND WF.HWBRNO = IRBRNO"
                            + "     AND WF.HWWHNO = IRWHFR"
                            + " LEFT JOIN IWHS AS WT ON 1=1"
                            + "     AND WT.HWCONO = IRCONO"
                            + "     AND WT.HWBRNO = IRBRNO"
                            + "     AND WT.HWWHNO = IRWHTO "
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "ITR2-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HMITNO"
                            + " , HMITNA"
                            + " , ISITQT - ISTOQT AS ISOUQT"
                            + " , ISITUM"
                            + " , ISTQLN"
                            + " FROM ITR2"
                            + " JOIN ITR1 ON 1=1"
                            + "     AND IRCONO = ISCONO"
                            + "     AND IRBRNO = ISBRNO"
                            + "     AND IRTQDN = ISTQDN"
                            + " LEFT JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = ISCONO or HMCONO = '')"
                            + "     AND (HMBRNO = ISBRNO or HMBRNO = '')"
                            + "     AND HMITNO = ISITNO"
                            + "";

                        strSQLFilter = "ISITQT - ISTOQT > 0";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ITUM-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HUUMNO"
                            + ", HUUMNA"
                            + ", ZRVANA AS HURCST"
                            + " FROM IUOM"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HURCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "ITYP-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HTITTY"
                            + ", HTTYNA"
                            + ", ZRVANA AS HTRCST"
                            + " FROM ITYP"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HTRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IVDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SAIVDN"
                            + ", SAIVDT"
                            + ", SACUNO"
                            + ", CMCUNA"
                            + ", SAIVAM"
                            + ", SAOUAM"
                            + " FROM SAR1"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SACONO"
                            //+ "     AND (CMBRNO = SABRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SACUNO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = SADCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 4-Amount; 5-Amount";
                        strColumnWidth = "0-148;1-88;2-106;3-200;4-120;5-120";

                        break;
                    }

                case "IVDN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SAIVDN, SAIVDT, SACUNO, CMCUNA, SAIVAM, SAOUAM"
                            + " FROM SAR1"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SACONO"
                            //+ "     AND CMBRNO = SABRNO"
                            + "     AND CMCUNO = SACUNO"
                            + " LEFT JOIN SCGR CGR1 on 1=1"
                            + "     AND (CGR1.CDCONO = SACONO OR CGR1.CDCONO = '')"
                            + "     AND (CGR1.CDBRNO = SABRNO OR CGR1.CDBRNO = '')"
                            + "     AND CGR1.CDCUGR = CMCGR1"
                            + "     AND CGR1.CDLVNO = 1"
                            + " LEFT JOIN SCGR CGR2 on 1=1"
                            + "     AND (CGR2.CDCONO = SACONO OR CGR2.CDCONO = '')"
                            + "     AND (CGR2.CDBRNO = SABRNO OR CGR2.CDBRNO = '')"
                            + "     AND CGR2.CDCUGR = CMCGR2"
                            + "     AND CGR2.CDLVNO = 2"
                            + " LEFT JOIN SCGR CGR3 on 1=1"
                            + "     AND (CGR3.CDCONO = SACONO OR CGR3.CDCONO = '')"
                            + "     AND (CGR3.CDBRNO = SABRNO OR CGR3.CDBRNO = '')"
                            + "     AND CGR3.CDCUGR = CMCGR3"
                            + "     AND CGR3.CDLVNO = 3"
                            + " LEFT JOIN SCGR CGR4 on 1=1"
                            + "     AND (CGR4.CDCONO = SACONO OR CGR4.CDCONO = '')"
                            + "     AND (CGR4.CDBRNO = SABRNO OR CGR4.CDBRNO = '')"
                            + "     AND CGR4.CDCUGR = CMCGR4"
                            + "     AND CGR4.CDLVNO = 4"
                            + " LEFT JOIN SCGR CGR5 on 1=1"
                            + "     AND (CGR5.CDCONO = SACONO OR CGR5.CDCONO = '')"
                            + "     AND (CGR5.CDBRNO = SABRNO OR CGR5.CDBRNO = '')"
                            + "     AND CGR5.CDCUGR = CMCGR5"
                            + "     AND CGR5.CDLVNO = 5"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = SADCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 4-Amount; 5-Amount";
                        strColumnWidth = "0-148;1-88;2-106;3-200;4-120;5-120";

                        break;
                    }

                case "IVDN-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT "
                            + "  SAIVDN"
                            + ", SAIVDT"
                            + " FROM SAR1"
                            + " JOIN SAR2 ON 1=1"
                            + "     and SBCONO = SACONO"
                            + "     and SBBRNO = SABRNO"
                            + "     and SBIVDN = SAIVDN"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SACONO"
                            //+ "     AND (CMBRNO = SABRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SACUNO"
                            + "";

                        strSQLFilter = "SAJRDN <> ''";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "IVDN-04":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SAIVDN"
                            + ", SAIVDT"
                            + ", SADUDT"
                            + ", CMCUNA"
                            + ", GIEMNA"
                            + " FROM SAR1"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SACONO"
                            //+ "     AND CMBRNO = SABRNO"
                            + "     AND CMCUNO = SACUNO"
                            + " JOIN GEMP ON 1=1"
                            + "     AND GICONO = SACONO"
                            + "     AND GIBRNO = SABRNO"
                            + "     AND GIEMNO = SAEMNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date;2-Date";

                        break;
                    }

                case "IVDN-05":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SAIVDN"
                            + ", SAIVDT"
                            + ", SACUNO"
                            + ", CMCUNA"
                            + ", SAEMNO"
                            + ", GIEMNA"
                            + " FROM SAR1"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SACONO"
                            //+ "     AND CMBRNO = SABRNO"
                            + "     AND CMCUNO = SACUNO"
                            + " JOIN GEMP ON 1=1"
                            + "     AND GICONO = SACONO"
                            + "     AND GIBRNO = SABRNO"
                            + "     AND GIEMNO = SAEMNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                //Invoice in Incoming Payment by Billing List
                case "IVDN-06":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SAIVDN"
                            + ", SAIVDT"
                            + ", SACUNO"
                            + ", CMCUNA"
                            + ", SAEMNO"
                            + ", GIEMNA"
                            + " FROM SAR1"
                            + " LEFT JOIN FBI2 on 1=1"
                            + "     AND FRCONO = SACONO"
                            + "     AND FRBRNO = SABRNO"
                            + "     AND FRIVDN = SAIVDN"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SACONO"
                            //+ "     AND CMBRNO = SABRNO"
                            + "     AND CMCUNO = SACUNO"
                            + " JOIN GEMP ON 1=1"
                            + "     AND GICONO = SACONO"
                            + "     AND GIBRNO = SABRNO"
                            + "     AND GIEMNO = SAEMNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";
                        strColumnWidth = "1-100";

                        break;
                    }

                //tambahan ref.no di incoming payment
                case "IVDN-07":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SAIVDN"
                            + ", SAIVDT"
                            + ", SACUNO"
                            + ", CMCUNA"
                            + ", SARFDN"
                            + ", SAIVAM"
                            + ", SAOUAM"
                            + " FROM SAR1"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SACONO"
                            //+ "     AND (CMBRNO = SABRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SACUNO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = SADCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 4-Amount; 5-Amount;6-Amount";
                        strColumnWidth = "0-148;1-88;2-106;3-200;4-150;5-120;6-120";

                        break;
                    }


                case "IWGR-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HVWHGR"
                            + ", HVGRNA"
                            + ", ZRVANA AS HVRCST"
                            + " FROM IWGR"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HVRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IWGR-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HVWHGR"
                            + " , HVGRNA"
                            + " FROM IWGR"
                            + "";

                        strSQLFilter = "HVRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IWHS-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HWWHNO"
                            + ", HWWHNA"
                            + ", ZRVANA AS HWRCST"
                            + " FROM IWHS"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = HWRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "IWHS-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "HWWHNO"
                            + " , HWWHNA"
                            + " FROM IWHS"
                            + "";

                        strSQLFilter = "HWRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                #endregion

                #region K
                #endregion

                #region L
                #endregion

                #region M

                case "MENO-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "ZMMENO"
                            + " , ZMMENA"
                            + " , ZMAPNO"
                            + " , ZRVANA AS ZMRCST"
                            + " FROM ZMNU"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = ZMRCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "MRKT-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CBKYNO"
                            + ", CBKYNA as CBMRKT"
                            + " FROM GCT2"
                            + "";

                        strSQLFilter = " CBTBNO = 'MRKT' ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "MRNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CBKYNO"
                            + " , CBKYNA"
                            + " , CBRCST"
                            + " FROM GCT2"
                            + "";

                        strSQLFilter = "CBTBNO = 'MRK' AND CBRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "MSHW-02":
                    {
                        dtSource = DataSource.University;
                        strSQL = "NIM "
                        + ",Nama "
                        + ",kode_Fakultas "
                        + ",kode_Jurusan "
                        + "FROM mahasiswa "
                        + "";

                        strSQLFilter = "Record_Status = 1";
                        strSQLGroup = "";
                        strSQLOrderBy = "";
                        break;
                    }
                    
                #endregion

                #region N

                #endregion

                #region O

                case "OCDN-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "F2OCDN"
                            + ", F2OCDT"
                            + ", F2VRAM"
                            + " FROM FOC1"
                            + "";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        strDecimalColumn = "1-Date;2-Amount";
                        break;
                    }

                case "OPDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "FOOPDN"
                            + ", FOOPDT"
                            + ", FOVENO"
                            + ", VMVENA"
                            + " FROM FOP1"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = FOCONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = FOVENO";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                #endregion

                #region P

                case "PGNO-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "ZPPGNO"
                            + " , ZPPGNA"
                            + " , ZPAPNO"
                            + " , ZRVANA AS ZPRCST"
                            + " FROM ZPGM"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = ZPRCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "PMNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "S1PMNO, S1PMNA, ZRVANA as S1PMTY, S1DTFR, S1DTTO"
                            + " FROM SPM1"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'PMTY'"
                            + "     AND ZRVAVL = S1PMTY"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "3-Date;4-Date";

                        break;
                    }

                case "PMNO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT S1PMNO, S4SCHM, CJOUPN "
                            + " FROM SCPP"
                            + " JOIN SPM1 ON 1=1"
                            + "     AND S1CONO = ''"
                            + "     AND S1BRNO = ''"
                            + "     AND S1PMNO = CJPMNO"
                            + " JOIN SPM4 ON 1=1"
                            + "     AND S4CONO = ''"
                            + "     AND S4BRNO = ''"
                            + "     AND S4PMNO = CJPMNO"
                            + "     AND S4PMLN = CJP4LN"
                            + "";

                        strSQLFilter = "S1RCST = 1 AND S4RCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "3-Date;4-Date";

                        break;
                    }

                case "PMNO-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "S1PMNO, S1PMNA, S1DTFR, S1DTTO"
                            + " FROM SPM1"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVANO = 'PMTY_POINT'"
                            + "     AND ZRVAVL = S1PMTY"
                            + "";

                        strSQLFilter = "S1RCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "3-Date;4-Date";

                        break;
                    }

                case "PMNO-04":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CJPMNO, S1PMNA, CJPMPN, CJOUPN, CJCUNO"
                            + " FROM SCPP"
                            + " LEFT JOIN SPM1 ON 1=1"
                            + "     AND S1CONO = ''"
                            + "     AND S1BRNO = ''"
                            + "     AND S1PMNO = CJPMNO"
                            + "";

                        strSQLFilter = "CJRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "";
                        strColumnWidth = "0-140";

                        break;
                    }

                case "PNDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "PKPNDN"
                            + " , PKPNDT"
                            + " , PKVENO"
                            + " , VMVENA"
                            + " FROM PPN1"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = PKCONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = PKVENO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                //PODN-01 - Open
                case "PODN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "POPODN"
                            + " , POPODT"
                            + " , POVENO"
                            + " , VMVENA"
                            + " FROM PPO1"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = POCONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = POVENO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "PODN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "POPODN"
                            + " , POPODT"
                            + " , POVENO"
                            + " , VMVENA"
                            + " FROM PPO1"
                            + " JOIN PVMA ON 1=1"
                            + "     AND VMCONO = POCONO"
                            + "     AND VMBRNO = ''"
                            + "     AND VMVENO = POVENO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "PRNO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GAPRNO"
                            + ", GAPRNA"
                            + ", ZRVANA AS GFRCST"
                            + " FROM GPCL"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GARCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                #endregion

                #region Q
                #endregion

                #region R

                case "RENO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = " CBKYNO"
                            + ", CBKYNA as CBRENA"
                            + " FROM GCT2"
                            + "";

                        strSQLFilter = "CBTBNO = 'RENO'";
                        strSQLGroup = "";
                        strSQLOrderBy = "";
                        //strDateColumn = "";

                        break;
                    }

                case "RENO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CBKYNO "
                            + ", CBKYNA "
                            + " FROM GCT2"
                            + "";

                        strSQLFilter = "CBTBNO = 'RENO' AND CBRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "RSNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GRRSNO"
                            + " , GRRSNA"
                            + " , GRRSTB"
                            + " , ZRVANA"
                            + " FROM GRES"
                            + " JOIN ZVAR ON GRCONO = ZRCONO AND ZRBRNO = '' AND ZRVATY = 'RSTB' AND GRRSTB = ZRVAVL"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }
                #endregion

                #region S

                case "SCCA-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 1 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 2 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 3 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-04":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 4 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-05":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 5 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-06":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 6 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-07":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 7 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-08":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 8 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-09":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 9 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-10":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " FROM SCCA"
                            + "";

                        strSQLFilter = "CCRCST = 1 AND CCLVNO = 10 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCA-11":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CCCUCA"
                            + " , CCCANA"
                            + " , CCLVNO"
                            + " , ZRVANA AS CCRCST"
                            + " FROM SCCA"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = CCRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCCO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT"
                            + "   CMCUNO"
                            + " , CMCUNA"
                            + " , CMADR1"
                            + " , CMADR2"
                            + " , CMADR3"
                            + " , CMCUOL"
                            + " from SCMA"
                            + " join SCCO on 1=1"
                            + "     and CSCONO = CMCONO"
                            + "     and CSBRNO = CMBRNO"
                            + "     and CSCUNO = CMCUNO"
                            + "";

                        strSQLFilter = "CMRCST = 1 and CSRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strColumnWidth = "0-110;1-150;2-200;3-150;4-100;5-130";
                        break;
                    }

                case "SCGM-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CKVENO"
                            + " , VMVENA"
                            + " , CKCGF1"
                            + " , CKCGF2"
                            + " , CKCGF3"
                            + " , CKCGF4"
                            + " , CKCGF5"
                            + " FROM SCGM"
                            + " LEFT JOIN PVMA on 1=1"
                            + " AND VMVENO = CKVENO"
                            + "";

                        strSQLFilter = " CKRCST = 1 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strColumnWidth = "0-100";
                        break;
                    }

                case "SCGR-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CDCUGR"
                            + " , CDGRNA"
                            + " , CDLVNO"
                            + " , ZRVANA AS CDRCST"
                            + " FROM SCGR"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = CDRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SCMM-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CRVENO"
                            + " , VMVENA"
                            + " , CRCUFR"
                            + " , CRCUTO"
                            + " , CMCUNA"
                            + " FROM SCMM"
                            + " LEFT JOIN PVMA on 1=1"
                            + " AND VMCONO = ''"
                            + " AND VMBRNO = ''"
                            + " AND VMVENO = CRVENO"
                            + " LEFT JOIN SCMA on 1=1"
                            + " AND CMCUNO = CRCUTO"
                            + "";

                        strSQLFilter = " CRRCST = 1 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SDAH-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CHARHC"
                            + ", CHARHN"
                            + ", ZRVANA AS CHRCST"
                            + " FROM SDAH"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = CHRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strColumnWidth = "";

                        break;
                    }

                case "SDAH-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CHARHC"
                            + ", CHARHN"
                            + " FROM SDAH"
                            + "";

                        strSQLFilter = "CHRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strColumnWidth = "";

                        break;
                    }

                case "SDAR-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGLVNO"
                            + ", CGARNO"
                            + ", CGARNA"
                            + ", ZRVANA AS CGRCST"
                            + " FROM SDAR"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = CGRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strColumnWidth = "0-80; 1-100; 2-500; 3-100";

                        break;
                    }

                case "AR01-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGARNO"
                            + ", CGARNA"
                            + " FROM SDAR"
                            + "";

                        strSQLFilter = "CGLVNO = 1 AND CGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "AR02-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGARNO"
                            + ", CGARNA"
                            + " FROM SDAR"
                            + "";

                        strSQLFilter = "CGLVNO = 2 AND CGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "AR03-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGARNO"
                            + ", CGARNA"
                            + " FROM SDAR"
                            + "";

                        strSQLFilter = "CGLVNO = 3 AND CGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "AR04-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGARNO"
                            + ", CGARNA"
                            + " FROM SDAR"
                            + "";

                        strSQLFilter = "CGLVNO = 4 AND CGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "AR05-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGARNO"
                            + ", CGARNA"
                            + " FROM SDAR"
                            + "";

                        strSQLFilter = "CGLVNO = 5 AND CGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "AR06-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGARNO"
                            + ", CGARNA"
                            + " FROM SDAR"
                            + "";

                        strSQLFilter = "CGLVNO = 6 AND CGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "AR07-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGARNO"
                            + ", CGARNA"
                            + " FROM SDAR"
                            + "";

                        strSQLFilter = "CGLVNO = 7 AND CGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "AR08-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGARNO"
                            + ", CGARNA"
                            + " FROM SDAR"
                            + "";

                        strSQLFilter = "CGLVNO = 8 AND CGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "AR09-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CGARNO"
                            + ", CGARNA"
                            + " FROM SDAR"
                            + "";

                        strSQLFilter = "CGLVNO = 9 AND CGRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "SICA-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "RGSINO"
                            + " , RGSINA"
                            + " , ZRVANA AS RGSLTY"
                            //+ " , ZRVANA AS RGRCST"
                            //+ " , CMCUNA"
                            + " FROM SICA"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'SLTY'"
                            + "     AND ZRVAVL = RGSLTY"
                            + "";

                        strSQLFilter = " RGRCST = 1 ";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }



                case "SLTM-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CBKYNO"
                            + " , CBKYNA as CBSTNA"
                            + " FROM GCT2"
                            + "";

                        strSQLFilter = "CBTBNO = 'SLTM' AND CBRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "SLOF-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SLSOFC"
                            + " , SLSOFN"
                            + " , SLRENO"
                            + " , CBKYNA as SLRENA"
                            + " , ZRVANA as SLRCST"
                            + " FROM SLOF"
                            + " LEFT JOIN GCT2 ON 1=1 "
                            + "     AND (CBCONO = SLCONO OR CBCONO = '')"
                            + "     AND (CBBRNO = SLBRNO OR CBBRNO = '')"
                            + "     AND CBTBNO = 'RENO'"
                            + "     AND CBKYNO = SLRENO"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = SLRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "SLOF-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SLSOFC"
                            + " , SLSOFN"
                            + " , SLRENO"
                            + " , CBKYNA as CBRENA"
                            + " FROM SLOF"
                            + " JOIN GCT2 on 1=1"
                            + "     AND CBCONO = ''"
                            + "     AND CBBRNO = ''"
                            + "     AND CBTBNO = 'RENO'"
                            + "     AND CBKYNO = SLRENO"
                            + "";

                        strSQLFilter = "SLRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "SNDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SRSNDN"
                            + ", SRSNDT"
                            + ", SRCUNO"
                            + ", CMCUNA"
                            + ", SREMNO"
                            + ", GIEMNA"
                            + " FROM SSN1"
                            + " JOIN SCMA ON CMCONO =  SRCONO AND CMBRNO = SRBRNO AND CMCUNO = SRCUNO"
                            + " JOIN GEMP ON 1=1"
                            + "     AND GICONO = SRCONO"
                            + "     AND GIBRNO = SRBRNO"
                            + "     AND GIEMNO = SREMNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "SODN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SOSODN"
                            + ", SOSODT"
                            + ", SOCUNO"
                            + ", CMCUNA "
                            + ", SOEMNO"
                            + ", GIEMNA"
                            + " FROM SSO1"
                            + " LEFT JOIN SCMA ON 1=1"
                            + "     AND (CMCONO = SOCONO OR CMCONO = '')"
                            + "     AND (CMBRNO = SOBRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SOCUNO"
                            + " LEFT JOIN GEMP ON 1=1"
                            + "     AND GICONO = SOCONO"
                            + "     AND GIBRNO = SOBRNO"
                            + "     AND GIEMNO = SOEMNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "SODN-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT SOSODN"
                            + ", SOSODT"
                            + ", SOCUNO"
                            + ", CMCUNA "
                            + ", SOSAD1"
                            //+ ", SOSAD2"
                            //+ ", SOSAD3"
                            + " FROM SSO1"
                            + " JOIN SSO2 ON 1=1"
                            + "     AND SPCONO = SOCONO"
                            + "     AND SPBRNO = SOBRNO"
                            + "     AND SPSODN = SOSODN"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SOCONO"
                            + "     AND CMBRNO = SOBRNO"
                            + "     AND CMCUNO = SOCUNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";
                        strColumnWidth = "0-148;1-110;2-106;3-200;4-220;";

                        break;
                    }

                case "SRDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SISRDN"
                            + ", SISRDT"
                            + ", SIEMNO"
                            + ", GIEMNA"
                            + ", SICUNO"
                            + ", CMCUNA"
                            + " FROM SSR1"
                            + " LEFT JOIN GEMP ON 1=1"
                            + "     AND GICONO = SICONO"
                            + "     AND GIBRNO = SIBRNO"
                            + "     AND GIEMNO = SIEMNO"
                            + " LEFT JOIN SCMA ON 1=1"
                            + "     AND (CMCONO = SICONO OR CMCONO = '')"
                            + "     AND (CMBRNO = SIBRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SICUNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "SSAR-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SWARNO"
                            + " , SWARNA"
                            + " , SWLVNO"
                            + " , ZRVANA AS SWRCST"
                            + " FROM SSAR"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = SWRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "SSAR-02":
                    {
                        dtSource = DataSource.University;
                        strSQL = "SWARNO, "
                                + " SWARNA "
                                + " FROM SSAR"
                                + "";
                        strSQLFilter = "SWRCST = 1 AND SWLVNO = 1";
                        strSQLGroup = "";
                        strDecimalColumn = "";
                        break;
                    }

                case "SSAR-03":
                    {
                        dtSource = DataSource.University;
                        strSQL = "SWARNO,"
                                + " SWARNA"
                                + " FROM SSAR"
                                + "";
                        strSQLFilter = "SWRCST = 1 AND SWLVNO = 2";
                        strSQLGroup = "";
                        strDecimalColumn = "";
                        break;
                    }

                case "SSAR-04":
                    {
                        dtSource = DataSource.University;
                        strSQL = "SWARNO,"
                                + " SWARNA"
                                + " FROM SSAR"
                                + "";
                        strSQLFilter = "SWRCST = 1 AND SWLVNO = 3";
                        strSQLGroup = "";
                        strDecimalColumn = "";
                        break;
                    }

                case "SSAR-05":
                    {
                        dtSource = DataSource.University;
                        strSQL = "SWARNO,"
                                + " SWARNA"
                                + " FROM SSAR"
                                + "";
                        strSQLFilter = "SWRCST = 1 AND SWLVNO = 4";
                        strSQLGroup = "";
                        strDecimalColumn = "";
                        break;
                    }

                case "SSAR-06":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SWARNO"
                            + " , SWARNA"
                            + " , SWLVNO"
                            + " FROM SSAR"
                            + "";

                        strSQLFilter = "SWRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "SSNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GSSSTY"
                            + " , GSSSNO"
                            + " , GSSSNA"
                            + " , GSRCST"
                            + " FROM GSTA"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "SSTG-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "CQPERD"
                            + ", CQCONO"
                            + ", CQBRNO"
                            + ", CQITNO"
                            + ", HMITNA"
                            + ", ZRVANA AS CQRCST"
                            + " FROM SSTG"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = GICONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = GIBRNO OR ZRBRNO = '')"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = CQRCST"
                            + " LEFT JOIN IIMA ON 1=1"
                            + "     AND (HMCONO = '' OR HMCONO = CQCONO)"
                            + "     AND (HMBRNO = '' OR HMBRNO = CQBRNO)"
                            + "     AND HMITNO = CQITNO";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "";

                        break;
                    }

                case "STTY-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "STTRTY"
                            + ", STTYNA"
                            + " FROM STTY"
                            + "";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "";
                        break;
                    }

                case "STDN-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "ILSTDN"
                            + ", ILENDT"
                            + ", ILAJDT"
                            + ", HWWHNA"
                            + " FROM IST1"
                            + " LEFT JOIN IWHS ON 1=1"
                            + "     AND HWCONO = ILCONO"
                            + "     AND HWBRNO = ILBRNO"
                            + "     AND HWWHNO = ILWHNO"
                            + "";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date;2-Date";
                        break;
                    }
                #endregion

                #region T

                case "TIDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IITIDN"
                            + " , IITIDT"
                            + " , IIWHFR"
                            + " , HWWHNA "
                            + " , IILOFR"
                            + " , HLLONA"
                            + " FROM ITI1"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IICONO"
                            + "     AND HWBRNO = IIBRNO"
                            + "     AND HWWHNO = IIWHFR"
                            + " JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IICONO"
                            + "     AND HLBRNO = IIBRNO"
                            + "     AND HLWHNO = IIWHFR"
                            + "     AND HLLONO = IILOFR"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TIDN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IITIDN, IITIDT, IIBRFR, ZBBRNA, HWWHNA, HLLONA"
                            + " FROM ITI1"
                            + " JOIN ZBRC ON 1=1"
                            + "     AND ZBCONO = IICONO"
                            + "     AND ZBBRNO = IIBRFR"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IICONO"
                            + "     AND HWBRNO = IIBRNO"
                            + "     AND HWWHNO = IIWHFR"
                            + " JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IICONO"
                            + "     AND HLBRNO = IIBRNO"
                            + "     AND HLWHNO = IIWHFR"
                            + "     AND HLLONO = IILOFR"
                            + "";

                        strSQLFilter = "IIBRFR <> IIBRTO";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TODN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IOTODN"
                            + " , IOTODT"
                            + " , IOWHFR"
                            + " , HWWHNA "
                            + " , IOLOFR"
                            + " , HLLONA"
                            + " FROM ITO1"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IOCONO"
                            + "     AND HWBRNO = IOBRNO"
                            + "     AND HWWHNO = IOWHFR"
                            + " JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IOCONO"
                            + "     AND HLBRNO = IOBRNO"
                            + "     AND HLWHNO = IOWHFR"
                            + "     AND HLLONO = IOLOFR"
                            //+ " JOIN ZVAR ON 1=1"
                            //+ "     AND (ZRCONO = IOCONO OR ZRCONO = '')"
                            //+ "     AND (ZRBRNO = IOBRNO OR ZRBRNO = '')"
                            //+ "     AND ZRVANO = 'DCST_OPEN'"
                            //+ "     AND ZRVAVL = IODCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TODN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IOTODN"
                            + " , IOTODT"
                            + " , IOBRFR"
                            + " , IOWHFR"
                            + " , HWWHNA"
                            + " , IOLOFR"
                            + " , HLLONA"
                            + " FROM ITO1"
                            + " LEFT JOIN ZBRC ON 1=1"
                            + "     AND ZBCONO = IOCONO"
                            + "     AND ZBBRNO = IOBRFR"
                            + " LEFT JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IOCONO"
                            + "     AND HWBRNO = IOBRFR"
                            + "     AND HWWHNO = IOWHFR"
                            + " LEFT JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IOCONO"
                            + "     AND HLBRNO = IOBRFR"
                            + "     AND HLWHNO = IOWHFR"
                            + "     AND HLLONO = IOLOFR"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'DCST'"
                            + "     AND ZRVAVL = IODCST"
                            + "";

                        strSQLFilter = "IOBRFR <> IOBRTO";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TODN-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IOTODN"
                            + " , IOTODT"
                            + " , IOWHFR"
                            + " , HWWHNA "
                            + " , IOLOFR"
                            + " , HLLONA"
                            + " FROM ITO1"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IOCONO"
                            + "     AND HWBRNO = IOBRNO"
                            + "     AND HWWHNO = IOWHFR"
                            + " JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IOCONO"
                            + "     AND HLBRNO = IOBRNO"
                            + "     AND HLWHNO = IOWHFR"
                            + "     AND HLLONO = IOLOFR"
                            + "";

                        strSQLFilter = "IOBRFR = IOBRTO";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TODN-04":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IOTODN"
                            + " , IOTODT"
                            + " , IOWHFR"
                            + " , HWWHNA"
                            + " , IOLOFR"
                            + " , HLLONA"
                            //+ " , IOBRTO"
                            + " , ZBBRNA AS IOBRTO"
                            + " FROM ITO1"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IOCONO"
                            + "     AND HWBRNO = IOBRNO"
                            + "     AND HWWHNO = IOWHFR"
                            + " JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IOCONO"
                            + "     AND HLBRNO = IOBRNO"
                            + "     AND HLWHNO = IOWHFR"
                            + "     AND HLLONO = IOLOFR"
                            + " JOIN ZBRC ON 1=1"
                            + "     AND ZBCONO = IOCONO"
                            + "     AND ZBBRNO = IOBRTO"
                            + "";

                        strSQLFilter = "IOBRFR <> IOBRTO";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TPNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GTTPNO"
                            + " , GTTPNA"
                            + " , GTTPDY"
                            + " , ZRVANA AS GTRCST"
                            + " FROM GTOP"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GTRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "TPNO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GTTPNO"
                            + " , GTTPNA"
                            + " FROM GTOP"
                            + "";

                        strSQLFilter = "GTRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "TQDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IRTQDN, IRTQDT, IRWHTO, HWWHNA, IRLOTO, HLLONA"
                            + " FROM ITR1"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IRCONO"
                            + "     AND HWBRNO = IRBRNO"
                            + "     AND HWWHNO = IRWHTO"
                            + " LEFT JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IRCONO"
                            + "     AND HLBRNO = IRBRNO"
                            + "     AND HLWHNO = IRWHTO"
                            + "     AND HLLONO = IRLOTO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = IRCONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = IRBRNO OR ZRBRNO = '')"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = IRDCST"
                            + "";

                        strSQLFilter = "IRBRFR = IRBRTO";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TQDN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IRTQDN, IRTQDT, IRWHTO, HWWHNA, IRLOTO, HLLONA"
                            + " FROM ITR1"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IRCONO"
                            + "     AND HWBRNO = IRBRNO"
                            + "     AND HWWHNO = IRWHTO"
                            + " LEFT JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IRCONO"
                            + "     AND HLBRNO = IRBRNO"
                            + "     AND HLWHNO = IRWHTO"
                            + "     AND HLLONO = IRLOTO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND (ZRCONO = IRCONO OR ZRCONO = '')"
                            + "     AND (ZRBRNO = IRBRNO OR ZRBRNO = '')"
                            + "     AND ZRVANO = 'DCST_OPEN'"
                            + "     AND ZRVAVL = IRDCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TQDN-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IRTQDN, IRTQDT, IRBRTO, ZBBRNA, IRWHTO, HWWHNA"
                            + " FROM ITR1"
                            + " LEFT JOIN ZBRC ON 1=1"
                            + "     AND ZBCONO = IRCONO"
                            + "     AND ZBBRNO = IRBRTO"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IRCONO"
                            + "     AND HWBRNO = IRBRTO"
                            + "     AND HWWHNO = IRWHTO"
                            + " JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'DCST'"
                            + "     AND ZRVAVL = IRDCST"
                            + "";

                        strSQLFilter = "IRBRFR <> IRBRTO";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TQDN-04":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IRTQDN, IRTQDT, IRWHTO, HWWHNA, IRLOTO, HLLONA"
                            + " FROM ITR1"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IRCONO"
                            + "     AND HWBRNO = IRBRNO"
                            + "     AND HWWHNO = IRWHTO"
                            + " JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IRCONO"
                            + "     AND HLBRNO = IRBRNO"
                            + "     AND HLWHNO = IRWHTO"
                            + "     AND HLLONO = IRLOTO"
                            + "";

                        strSQLFilter = "IRBRFR = IRBRTO";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TQDN-05":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IRTQDN, IRTQDT, IRBRTO, ZBBRNA, IRWHTO, HWWHNA, IRLOTO"
                            + " FROM ITR1"
                            + " LEFT JOIN ZBRC ON 1=1"
                            + "     AND ZBCONO = IRCONO"
                            + "     AND ZBBRNO = IRBRTO"
                            + " JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IRCONO"
                            + "     AND HWBRNO = IRBRTO"
                            + "     AND HWWHNO = IRWHTO"
                            + " JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IRCONO"
                            + "     AND HLBRNO = IRBRNO"
                            + "     AND HLWHNO = IRWHTO"
                            + "     AND HLLONO = IRLOTO"
                            + "";

                        strSQLFilter = "IRBRFR <> IRBRTO";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }

                case "TRTY-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZRVAVL AS TRTY"
                            + " , ZRVANA AS TRTN"
                            + " , ZRVANO AS VANO"
                            + " FROM ZVAR"
                            + "";

                        strSQLFilter = "ZRVATY = 'TRTY'";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "TSNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "GETSNO"
                            + " , GETSNA"
                            + " , GENPOL"
                            + " , ZRVANA AS GLRCST"
                            + " FROM GTPR"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = GERCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "TTAX-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "TMTXNO"
                            + ", TMTXNA"
                            + ", ZRVANA AS TMRCST"
                            + " FROM TTAX"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = TMRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "TTAX-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "TMTXNO"
                            + ", TMTXNA"
                            + ", ZRVANA AS TMRCST"
                            + " FROM TTAX"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = TMRCST"
                            + "";

                        strSQLFilter = "TMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "TXNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "TMTXNO"
                            + " ,TMTXNA"
                            + " FROM TTAX"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }
                #endregion

                #region U
                case "UGNO-01":
                    {
                        dtSource = DataSource.University;
                        strSQL = "ZGUGNO, ZGUGNA, ZRVANA AS ZGRCST"
                            + " FROM ZUG1"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = ZGRCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "USDN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "IXUSDN"
                            + ", IXUSDT"
                            + ", HWWHNA"
                            + ", HLLONA"
                            + " , ZRVANA AS IXRCST"
                            + " FROM IUS1"
                            + " LEFT JOIN IWHS ON 1=1"
                            + "     AND HWCONO = IXCONO"
                            + "     AND HWBRNO = IXBRNO"
                            + "     AND HWWHNO = IXWHNO"
                            + " LEFT JOIN ILOC ON 1=1"
                            + "     AND HLCONO = IXCONO"
                            + "     AND HLBRNO = IXBRNO"
                            + "     AND HLWHNO = IXWHNO"
                            + "     AND HLLONO = IXLONO"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = IXRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date";

                        break;
                    }
                case "USNO":
                    {
                        dtSource = DataSource.University;
                        strSQL = "ZUUSNO, ZUUSNA, ZRVANA AS ZURCST, ZUCONO"
                            + " FROM ZUSR"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = ZURCST";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        break;
                    }

                case "USNO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZUUSNO, ZUUSNA, ZUCONO, ZUBRNO, ZBBRNA, ZRVANA AS ZURCST"
                            + " FROM ZUSR"
                            + " LEFT JOIN ZCMP ON ZCCONO = ZUCONO"
                            + " LEFT JOIN ZBRC ON ZBCONO = ZUCONO AND ZBBRNO = ZUBRNO"
                            + " LEFT JOIN ZVAR ON ZRCONO = '' AND ZRBRNO = '' AND ZRVATY = 'RCST' AND ZRVAVL = ZURCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strWindowSize = "854;480";

                        break;
                    }

                case "USNO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZUUSNO"
                            + " , ZUUSNA"
                            + " FROM ZUSR"
                            + "";

                        strSQLFilter = "ZURCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }
                #endregion

                #region V
                case "VENO-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "VMVENO"
                            + ", VMVENA"
                            + ", ZRVANA AS VMRCST"
                            + " FROM PVMA"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = VMRCST"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "VENO-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT VMVENO"
                            + " , VMVENA"
                            + " FROM PVMA"
                            + "";

                        strSQLFilter = "VMRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "VENO-03":
                    {
                        dtSource = DataSource.University;

                        strSQL = "VDVENO"
                            + ", VMVENA"
                            + ", VDDTFR"
                            + ", VDDTTO"
                            + ", ZRVANA AS VDRCST"
                            + " FROM PVMA"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRCONO = ''"
                            + "     AND ZRBRNO = ''"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = VMRCST"
                            + " JOIN PVDC ON 1=1"
                            + "     AND VDVENO = VMVENO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "VEIT-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "DISTINCT VIVENO"
                            + " , VMVENA"
                            + " , VIVEIT "
                            + " , VIITNO "
                            + " , HMITNA "
                            + " FROM PVMI"
                            + " LEFT JOIN PVMA on 1=1"
                            + " AND VMBRNO = ''"
                            + " AND VMVENO = VIVENO "
                            + " LEFT JOIN IIMA on 1=1 "
                            + " AND HMCONO = ''"
                            + " AND HMBRNO = ''"
                            + " AND HMITNO = VIITNO "
                            + "";

                        strSQLFilter = "VIRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                #endregion

                #region W

                case "WODN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "SHWODN, SHWODT, SHCUNO, CMCUNA, SHWOAM"
                            + " FROM SARC"
                            + " JOIN SCMA ON 1=1"
                            + "     AND CMCONO = SHCONO"
                            + "     AND (CMBRNO = SHBRNO OR CMBRNO = '')"
                            + "     AND CMCUNO = SHCUNO"
                            + "";

                        strSQLFilter = "SHWODN <> '' ";
                        strSQLGroup = "";
                        strSQLOrderBy = "SHWODN ";
                        //strDateColumn = "";
                        strDecimalColumn = "1-Date; 4-Amount";
                        break;
                    }

                #endregion

                #region X
                #endregion

                #region Y
                #endregion

                #region Z
                case "ZBRC-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZBCONO"
                            + ", ZBBRNO"
                            + ", ZBBRNA"
                            + " FROM ZBRC"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        strColumnWidth = "0-150;1-100;2-300";
                        break;
                    }

                case "ZBUM-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZVCONO, ZCCONA, ZVBRNO, ZBBRNA, ZVUSNO, ZUUSNA"
                            + " FROM ZBUM"
                            + " LEFT JOIN ZCMP on 1=1"
                            + "     AND ZCCONO = ZVCONO"
                            + " LEFT JOIN ZBRC on 1=1"
                            + "     AND ZBCONO = ZVCONO"
                            + "     AND ZBBRNO = ZVBRNO"
                            + " LEFT JOIN ZUSR on 1=1"
                            + "     AND ZUCONO = ZVCONO"
                            + "     AND ZUBRNO = ZVBRNO"
                            + "     AND ZUUSNO = ZVUSNO"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "ZBUM-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZVCONO"
                            + ", ZVBRNO"
                            + ", ZVUSNO"
                            + " FROM ZBUM"
                            + "";

                        strSQLFilter = "ZVRCST = 1";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "ZCMP-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZCCONO"
                            + ", ZCCONA"
                            + ", ZCCOTY"
                            + ", ZCCOOL"
                            + " FROM ZCMP"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";
                        break;
                    }

                case "ZVAR-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "ZRVANO"
                            + " , ZRVANA"
                            + " , ZRVATY"
                            + " , ZRVAVL"
                            + " , CASE "
                            + " WHEN ZRRCST = 1 THEN 'Active'"
                            + " WHEN ZRRCST = 0 THEN 'Inactive'"
                            + " END AS ZRRCST"
                            + " FROM ZVAR"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }
                #endregion


                #region Training

                case "JRSN-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "A.Kode_Fakultas AS [Kode Fakultas]"
                            + " , A.Kode_Jurusan AS [Kode Jurusan]"
                            + " , A.Nama_Jurusan AS [Nama Jurusan]"
                            + " , B.nama_common AS [Record Status]"
                            + " FROM Jurusan A"
                            + " LEFT JOIN common B ON 1=1"
                            + "     AND B.tipe_common = 'Record_Status'"
                            + "     AND B.nilai_common = A.Record_Status"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        //strDateColumn = "";

                        break;
                    }

                case "JRSN-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "Kode_Jurusan"
                            + " , Nama_Jurusan"
                            + " , ZRVANA AS Record_Status"
                            + " FROM Jurusan"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = Record_Status"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSQLOrderBy = "Kode_Jurusan";
                        //strDateColumn = "";

                        break;
                    }

                case "FKLS-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "A.Kode_Fakultas AS [Kode Fakultas]"
                            + " , A.Nama_Fakultas AS [Nama Fakultas]"
                            + " , B.nama_common AS [Record Status]"
                            + " FROM Fakultas A"
                            + " LEFT JOIN common B ON 1=1"
                            + "     AND B.tipe_common = 'Record_Status'"
                            + "     AND B.nilai_common = A.Record_Status"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSQLOrderBy = "";
                        //strDateColumn = "";

                        break;
                    }

                case "FKLS-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "Kode_Fakultas"
                            + " , Nama_Fakultas"
                            + " , ZRVANA AS Record_Status"
                            + " FROM Fakultas"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = Record_Status"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSQLOrderBy = "Kode_Fakultas";
                        //strDateColumn = "";

                        break;
                    }

                case "MTKL-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "A.kode_fakultas AS [Kode Fakultas]"
                            + " , A.kode_jurusan AS [Kode Jurusan]"
                            + " , A.kode_matakuliah AS [Kode Matakuliah]"
                            + " , A.nama_matakuliah AS [Nama Matakuliah]"
                            + " , B.nama_common AS [Record Status]"
                            + " FROM MataKuliah A"
                            + " LEFT JOIN common B ON 1=1"
                            + "     AND B.tipe_common = 'Record_Status'"
                            + "     AND B.nilai_common = A.record_status"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSQLOrderBy = "";
                        //strDateColumn = "";

                        break;
                    }

                case "MTKL-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "Kode_MataKuliah"
                            + " , Nama_MataKuliah"
                            + " , SKS"
                            + " FROM MataKuliah"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSQLOrderBy = "";
                        //strDateColumn = "";

                        break;
                    }

                case "MSHW-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "NIM AS [NIM]"
                            + " , Nama AS [Nama]"
                            + " , Telepon AS [Telepon]"
                            + " , ZRVANA AS [Record Status]"
                            + " FROM Mahasiswa"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = Record_Status"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSQLOrderBy = "";
                        //strDateColumn = "";

                        break;
                    }

                

                case "VRBL-01":
                    {
                        dtSource = DataSource.University;

                        strSQL = "Kode_Variabel AS [Kode Variabel]"
                            + " , Nama_Variabel AS [Nama Variabel]"
                            + " , Nilai_Variabel AS [Nilai Variabel]"
                            + " , Tipe_Variabel AS [Tipe Variabel]"
                            + " , ZRVANA AS [Record Status]"
                            + " FROM Variabel"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = Record_Status"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSQLOrderBy = "";
                        //strDateColumn = "";

                        break;
                    }

                case "VRBL-02":
                    {
                        dtSource = DataSource.University;

                        strSQL = "Kode_Variabel"
                            + " , Nama_Variabel"
                            + " , Nilai_Variabel"
                            + " , Tipe_Variabel"
                            + " , ZRVANA AS Record_Status"
                            + " FROM Mahasiswa"
                            + " LEFT JOIN ZVAR ON 1=1"
                            + "     AND ZRVATY = 'RCST'"
                            + "     AND ZRVAVL = Record_Status"
                            + "";

                        strSQLFilter = "";
                        strSQLGroup = "";
                        strSQLOrderBy = "Kode_Variabel";
                        //strDateColumn = "";

                        break;
                    }

                #endregion


                default:
                    {
                        dtSource = DataSource.University;
                        strSQL = "";
                        strSQLFilter = "";
                        strSQLGroup = "";
                        ////strDateColumn = "";
                        strDecimalColumn = ""; //[Column Index]-[Column Format] if more than 1 user ; as separator
                        break;
                    }
            }
        }

        #endregion

        #region Get Data
        public void GetWindowInfo(string strEntity, out string strWindowSize)
        {
            string strSQL = string.Empty;
            string strSQLFilter = string.Empty;
            string strSQLGroup = string.Empty;
            string strSQLOrderBy = string.Empty;
            string strDecimalColumn = string.Empty;
            string strColumnWidth = string.Empty;
            DataSource dtSource;

            CreateSQL(strEntity, out dtSource, out strSQL, out strSQLFilter, out strSQLGroup, out strSQLOrderBy, out strWindowSize, out strDecimalColumn, out strColumnWidth);
        }

        public DataTable GetDataTable(EntityDto obj, out int intTotalPage, out int intTotalRecord, out string strWindowSize, out string strDecimalColumn, out string strColumnWidth)
        {
            string strSQL = string.Empty;
            string strSQLFilter = string.Empty;
            string strSQLGroup = string.Empty;
            string strSQLOrderBy = string.Empty;
            strDecimalColumn = string.Empty;
            strColumnWidth = string.Empty;
            DataSource dtSource;

            CreateSQL(obj.Entity, out dtSource, out strSQL, out strSQLFilter, out strSQLGroup, out strSQLOrderBy, out strWindowSize, out strDecimalColumn, out strColumnWidth);

            return this.GetDataTableBase(dtSource, strSQL, strSQLFilter, strSQLGroup, strSQLOrderBy, obj, out intTotalPage, out intTotalRecord);
        }

        public DataTable GetSingleData(string strEntity, string strFilter, string strKeyCode, out string strWindowSize, out string strDecimalColumn, out string strColumnWidth, bool isDscr)
        {

            string strSQL = string.Empty, strSQLFilter = string.Empty, strSQLGroup = string.Empty, strSQLOrderBy = string.Empty;
            DataSource dtSource;

            CreateSQL(strEntity, out dtSource, out strSQL, out strSQLFilter, out strSQLGroup, out strSQLOrderBy, out strWindowSize, out strDecimalColumn, out strColumnWidth);

            return this.GetSingleDataBase(dtSource, strSQL, strSQLFilter, strFilter, strKeyCode, isDscr);
        }

        public string GetDescription(string strEntity, string strFilter, string strKeyCode, int idxDscrColumn)
        {
            string strSQL = string.Empty, strSQLFilter = string.Empty, strSQLGroup = string.Empty, strSQLOrderBy = string.Empty, strWindowSize = string.Empty, strDecimalColumn = string.Empty, strColumnWidth = string.Empty;
            DataSource dtSource;

            CreateSQL(strEntity, out dtSource, out strSQL, out strSQLFilter, out strSQLGroup, out strSQLOrderBy, out strWindowSize, out strDecimalColumn, out strColumnWidth);

            return this.GetDescriptionBase(dtSource, strSQL, strSQLFilter, strFilter, strKeyCode, idxDscrColumn);
        }

        public bool IsValid(string strEntity, string strFilter, string strKeyCode)
        {
            string strSQL = string.Empty, strSQLFilter = string.Empty, strSQLGroup = string.Empty, strSQLOrderBy = string.Empty, strWindowSize = string.Empty, strDecimalColumn = string.Empty, strColumnWidth = string.Empty;
            DataSource dtSource;

            CreateSQL(strEntity, out dtSource, out strSQL, out strSQLFilter, out strSQLGroup, out strSQLOrderBy, out strWindowSize, out strDecimalColumn, out strColumnWidth);

            return this.IsValidBase(dtSource, strSQL, strSQLFilter, strFilter, strKeyCode);
        }

        #endregion

    }
}
