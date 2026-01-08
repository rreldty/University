using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

using University.Dao.Base;
using University.Dto.Base;

namespace University.Dao.Base
{
    public class GeneralDao : BaseDao<GeneralDto>
    {
        #region "Abstract Class Implementation"

        public GeneralDao()
        {
            this.MainDataSource = DataSource.University;
        }

        protected override Mapper<GeneralDto> GetMapper()
        {
            Mapper<GeneralDto> mapDto = new GeneralMappingDto();
            return mapDto;
        }

        #endregion

        #region "Public Method"

        public string ExecuteScriptNonTransaction(string strScript)
        {
            return this.ExecuteDbNonQueryNonTransaction(strScript);
        }

        public string ExecuteScript(string strScript)
        {
            return this.ExecuteDbNonQuery(strScript);
        }

        public List<GeneralDto> GetListPricing(string strCONO, string strBRNO, decimal decDTFR, decimal decDTTO)
        {
            string strSql = "SELECT "
                    //+ "'INSERT INTO GPR8 '"
                    //+ " + '('"
                    //+ " + 'DHCONO, DHBRNO, DHSACD, DHSQLN, DHCTTC, DHDTFR, DHDTTO, DHAR01, DHAR02, DHAR03, DHAR04, DHAR05, DHAR06, DHAR07, DHAR08 '"
                    //+ " + ', DHAR09, DHDIST, DHIC01, DHIC02, DHIC03, DHIC04, DHIC05, DHIC06, DHIC07, DHIC08, DHIC09, DHITG1, DHITG2, DHITG3, DHITG4 '"
                    //+ " + ', DHITG5, DHITG6, DHITG7, DHITG8, DHITG9, DHITNO, DHCGR1, DHCGR2, DHCGR3, DHCGR4, DHCGR5, DHCGR6, DHCGR7, DHCGR8, DHCGR9 '"
                    //+ " + ', DHCUNO, DHREST, DHSCTY, DHSCQT, DHSCUN, DHAMNT, DHUNIT, DHPPER, DHUPER, DHLMMN, DHLMMX, DHPLCV, DHCMVL, DHCAVL, DHMCVL '"
                    //+ " + ', DHCYNO, DHDLIN, DHREMA, DHSYST, DHSTAT, DHRCST, DHCRDT, DHCRTM, DHCRUS, DHCHDT, DHCHTM, DHCHUS'"
                    //+ " + ') '"
                    //+ " + 'VALUES '"
                    //+ " + '( '"
                    //+ " + '''' + CAST(DHCONO AS VARCHAR) + ''',''' + CAST(DHBRNO AS VARCHAR) + ''',''' + CAST(DHSACD AS VARCHAR) + ''',''' + CAST(DHSQLN AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHCTTC AS VARCHAR) + ''',''' + CAST(DHDTFR AS VARCHAR) + ''',''' + CAST(DHDTTO AS VARCHAR) + ''',''' + CAST(DHAR01 AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHAR02 AS VARCHAR) + ''',''' + CAST(DHAR03 AS VARCHAR) + ''',''' + CAST(DHAR04 AS VARCHAR) + ''',''' + CAST(DHAR05 AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHAR06 AS VARCHAR) + ''',''' + CAST(DHAR07 AS VARCHAR) + ''',''' + CAST(DHAR08 AS VARCHAR) + ''',''' + CAST(DHAR09 AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHDIST AS VARCHAR) + ''',''' + CAST(DHIC01 AS VARCHAR) + ''',''' + CAST(DHIC02 AS VARCHAR) + ''',''' + CAST(DHIC03 AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHIC04 AS VARCHAR) + ''',''' + CAST(DHIC05 AS VARCHAR) + ''',''' + CAST(DHIC06 AS VARCHAR) + ''',''' + CAST(DHIC07 AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHIC08 AS VARCHAR) + ''',''' + CAST(DHIC09 AS VARCHAR) + ''',''' + CAST(DHITG1 AS VARCHAR) + ''',''' + CAST(DHITG2 AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHITG3 AS VARCHAR) + ''',''' + CAST(DHITG4 AS VARCHAR) + ''',''' + CAST(DHITG5 AS VARCHAR) + ''',''' + CAST(DHITG6 AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHITG7 AS VARCHAR) + ''',''' + CAST(DHITG8 AS VARCHAR) + ''',''' + CAST(DHITG9 AS VARCHAR) + ''',''' + CAST(DHITNO AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHCGR1 AS VARCHAR) + ''',''' + CAST(DHCGR2 AS VARCHAR) + ''',''' + CAST(DHCGR3 AS VARCHAR) + ''',''' + CAST(DHCGR4 AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHCGR5 AS VARCHAR) + ''',''' + CAST(DHCGR6 AS VARCHAR) + ''',''' + CAST(DHCGR7 AS VARCHAR) + ''',''' + CAST(DHCGR8 AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHCGR9 AS VARCHAR) + ''',''' + CAST(DHCUNO AS VARCHAR) + ''',''' + CAST(DHREST AS VARCHAR) + ''',''' + CAST(DHSCTY AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHSCQT AS VARCHAR) + ''',''' + CAST(DHSCUN AS VARCHAR) + ''',''' + CAST(DHAMNT AS VARCHAR) + ''',''' + CAST(DHUNIT AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHPPER AS VARCHAR) + ''',''' + CAST(DHUPER AS VARCHAR) + ''',''' + CAST(DHLMMN AS VARCHAR) + ''',''' + CAST(DHLMMX AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHPLCV AS VARCHAR) + ''',''' + CAST(DHCMVL AS VARCHAR) + ''',''' + CAST(DHCAVL AS VARCHAR) + ''',''' + CAST(DHMCVL AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHCYNO AS VARCHAR) + ''',''' + CAST(DHDLIN AS VARCHAR) + ''',''' + CAST(DHREMA AS VARCHAR) + ''',''FN'''"
                    //+ " + ',''' + CAST(DHSTAT AS VARCHAR) + ''',''' + CAST(DHRCST AS VARCHAR) + ''',''' + CAST(DHCRDT AS VARCHAR) + ''',''' + CAST(DHCRTM AS VARCHAR)"
                    //+ " + ''',''' + CAST(DHCRUS AS VARCHAR) + ''',''' + CAST(DHCHDT AS VARCHAR) + ''',''' + CAST(DHCHTM AS VARCHAR) + ''',''' + CAST(DHCHUS AS VARCHAR)"
                    //+ " + ''''"
                    //+ " + ') '"
                    + "'''' + CAST(DHCONO AS VARCHAR) + ''',''' + CAST(DHBRNO AS VARCHAR) + ''',''' + CAST(DHSACD AS VARCHAR) + ''',''' + CAST(DHSQLN AS VARCHAR)"
                    + " + ''',''' + CAST(DHCTTC AS VARCHAR) + ''',''' + CAST(DHDTFR AS VARCHAR) + ''',''' + CAST(DHDTTO AS VARCHAR) + ''',''' + CAST(DHAR01 AS VARCHAR)"
                    + " + ''',''' + CAST(DHAR02 AS VARCHAR) + ''',''' + CAST(DHAR03 AS VARCHAR) + ''',''' + CAST(DHAR04 AS VARCHAR) + ''',''' + CAST(DHAR05 AS VARCHAR)"
                    + " + ''',''' + CAST(DHAR06 AS VARCHAR) + ''',''' + CAST(DHAR07 AS VARCHAR) + ''',''' + CAST(DHAR08 AS VARCHAR) + ''',''' + CAST(DHAR09 AS VARCHAR)"
                    + " + ''',''' + CAST(DHDIST AS VARCHAR) + ''',''' + CAST(DHIC01 AS VARCHAR) + ''',''' + CAST(DHIC02 AS VARCHAR) + ''',''' + CAST(DHIC03 AS VARCHAR)"
                    + " + ''',''' + CAST(DHIC04 AS VARCHAR) + ''',''' + CAST(DHIC05 AS VARCHAR) + ''',''' + CAST(DHIC06 AS VARCHAR) + ''',''' + CAST(DHIC07 AS VARCHAR)"
                    + " + ''',''' + CAST(DHIC08 AS VARCHAR) + ''',''' + CAST(DHIC09 AS VARCHAR) + ''',''' + CAST(DHITG1 AS VARCHAR) + ''',''' + CAST(DHITG2 AS VARCHAR)"
                    + " + ''',''' + CAST(DHITG3 AS VARCHAR) + ''',''' + CAST(DHITG4 AS VARCHAR) + ''',''' + CAST(DHITG5 AS VARCHAR) + ''',''' + CAST(DHITG6 AS VARCHAR)"
                    + " + ''',''' + CAST(DHITG7 AS VARCHAR) + ''',''' + CAST(DHITG8 AS VARCHAR) + ''',''' + CAST(DHITG9 AS VARCHAR) + ''',''' + CAST(DHITNO AS VARCHAR)"
                    + " + ''',''' + CAST(DHCGR1 AS VARCHAR) + ''',''' + CAST(DHCGR2 AS VARCHAR) + ''',''' + CAST(DHCGR3 AS VARCHAR) + ''',''' + CAST(DHCGR4 AS VARCHAR)"
                    + " + ''',''' + CAST(DHCGR5 AS VARCHAR) + ''',''' + CAST(DHCGR6 AS VARCHAR) + ''',''' + CAST(DHCGR7 AS VARCHAR) + ''',''' + CAST(DHCGR8 AS VARCHAR)"
                    + " + ''',''' + CAST(DHCGR9 AS VARCHAR) + ''',''' + CAST(DHCUNO AS VARCHAR) + ''',''' + CAST(DHREST AS VARCHAR) + ''',''' + CAST(DHSCTY AS VARCHAR)"
                    + " + ''',''' + CAST(DHSCQT AS VARCHAR) + ''',''' + CAST(DHSCUN AS VARCHAR) + ''',''' + CAST(DHAMNT AS VARCHAR) + ''',''' + CAST(DHUNIT AS VARCHAR)"
                    + " + ''',''' + CAST(DHPPER AS VARCHAR) + ''',''' + CAST(DHUPER AS VARCHAR) + ''',''' + CAST(DHLMMN AS VARCHAR) + ''',''' + CAST(DHLMMX AS VARCHAR)"
                    + " + ''',''' + CAST(DHPLCV AS VARCHAR) + ''',''' + CAST(DHCMVL AS VARCHAR) + ''',''' + CAST(DHCAVL AS VARCHAR) + ''',''' + CAST(DHMCVL AS VARCHAR)"
                    + " + ''',''' + CAST(DHCYNO AS VARCHAR) + ''',''' + CAST(DHDLIN AS VARCHAR) + ''',''' + CAST(DHREMA AS VARCHAR) + ''',''FN'''"
                    + " + ',''' + CAST(DHSTAT AS VARCHAR) + ''',''' + CAST(DHRCST AS VARCHAR) + ''',''' + CAST(DHCRDT AS VARCHAR) + ''',''' + CAST(DHCRTM AS VARCHAR)"
                    + " + ''',''' + CAST(DHCRUS AS VARCHAR) + ''',''' + CAST(DHCHDT AS VARCHAR) + ''',''' + CAST(DHCHTM AS VARCHAR) + ''',''' + CAST(DHCHUS AS VARCHAR)"
                    + " + ''''"
                    + " AS DATA "
                    + "FROM GPR8 "
                    + "JOIN GPR2 ON 1=1 "
                    + "AND DBCONO = DHCONO "
                    + "AND DBBRNO = DHBRNO "
                    + "AND DBCTTC = DHCTTC "
                    + "JOIN GPR1 ON 1=1 "
                    + "AND DACONO = DBCONO "
                    + "AND DABRNO = DBBRNO "
                    + "AND DAPTCD = DBPTCD "
                    + "WHERE 1=1 "
                    + "AND DHSYST <> '' "
                    + "AND DAPTMT = '+' "
                    + "";

            if (strCONO != null)
            {
                strSql += "AND (DHCONO = '" + strCONO.Trim() + "' OR DHCONO = '') ";
            }

            if (strBRNO != null)
            {
                strSql += "AND (DHBRNO = '" + strBRNO.Trim() + "' OR DHBRNO = '') ";
            }

            if (decDTFR != 0)
            {
                strSql += "AND DHDTFR = " + decDTFR.ToString().Trim() + " ";
            }

            if (decDTTO != 0)
            {
                strSql += "AND DHDTTO = " + decDTTO.ToString().Trim() + " ";
            }

            List<GeneralDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<GeneralDto> GetListPagingPricing(string strCONO, string strBRNO, decimal decDTFR, decimal decDTTO, int intPageNumber, int intPageSize)
        {
            string strSql = "SELECT "
                    + "  DHCONO"
                    + ", DHBRNO"
                    + ", DHSACD"
                    + ", DHSQLN"
                    + ", '"
                    + " ''' + CAST(DHCONO AS VARCHAR) + ''',''' + CAST(DHBRNO AS VARCHAR) + '''"
                    + ",''' + CAST(DHSACD AS VARCHAR) + ''',''' + CAST(DHSQLN AS VARCHAR) + ''',''' + CAST(DHCTTC AS VARCHAR) + '''"
                    //+ ",''' + CAST(DHDTFR AS VARCHAR) + ''',''' + CAST(DHDTTO AS VARCHAR) + '''"
                    + ",''' + CAST(DHAR01 AS VARCHAR) + ''',''' + CAST(DHAR02 AS VARCHAR) + ''',''' + CAST(DHAR03 AS VARCHAR) + '''"
                    + ",''' + CAST(DHAR04 AS VARCHAR) + ''',''' + CAST(DHAR05 AS VARCHAR) + ''',''' + CAST(DHAR06 AS VARCHAR) + '''"
                    //+ ",''' + CAST(DHAR07 AS VARCHAR) + ''',''' + CAST(DHAR08 AS VARCHAR) + ''',''' + CAST(DHAR09 AS VARCHAR) + '''"
                    //+ ",''' + CAST(DHDIST AS VARCHAR) + ''',''' + CAST(DHIC01 AS VARCHAR) + ''',''' + CAST(DHIC02 AS VARCHAR) + '''"
                    //+ ",''' + CAST(DHIC03 AS VARCHAR) + ''',''' + CAST(DHIC04 AS VARCHAR) + ''',''' + CAST(DHIC05 AS VARCHAR) + '''"
                    //+ ",''' + CAST(DHIC06 AS VARCHAR) + ''',''' + CAST(DHIC07 AS VARCHAR) + ''',''' + CAST(DHIC08 AS VARCHAR) + '''"
                    //+ ",''' + CAST(DHIC09 AS VARCHAR) + '''"
                    + ",''' + CAST(DHITG1 AS VARCHAR) + ''',''' + CAST(DHITG2 AS VARCHAR) + ''',''' + CAST(DHITG3 AS VARCHAR) + '''"
                    + ",''' + CAST(DHITG4 AS VARCHAR) + ''',''' + CAST(DHITG5 AS VARCHAR) + ''',''' + CAST(DHITG6 AS VARCHAR) + '''"
                    + ",''' + CAST(DHITG7 AS VARCHAR) + ''',''' + CAST(DHITG8 AS VARCHAR) + ''',''' + CAST(DHITG9 AS VARCHAR) + '''"
                    + ",''' + CAST(DHITNO AS VARCHAR) + ''',''' + CAST(DHCGR1 AS VARCHAR) + ''',''' + CAST(DHCGR2 AS VARCHAR) + '''"
                    + ",''' + CAST(DHCGR3 AS VARCHAR) + ''',''' + CAST(DHCGR4 AS VARCHAR) + ''',''' + CAST(DHCGR5 AS VARCHAR) + '''"
                    //+ ",''' + CAST(DHCGR6 AS VARCHAR) + ''',''' + CAST(DHCGR7 AS VARCHAR) + ''',''' + CAST(DHCGR8 AS VARCHAR) + '''"
                    //+ ",''' + CAST(DHCGR9 AS VARCHAR) + '''"
                    + ",''' + CAST(DHCUNO AS VARCHAR) + ''',''' + CAST(DHREST AS VARCHAR) + ''',''' + CAST(DHSCTY AS VARCHAR) + '''"
                    + ",''' + CAST(DHSCQT AS VARCHAR) + ''',''' + CAST(DHSCUN AS VARCHAR) + ''',''' + CAST(DHAMNT AS VARCHAR) + '''"
                    + ",''' + CAST(DHUNIT AS VARCHAR) + ''',''' + CAST(DHPPER AS VARCHAR) + ''',''' + CAST(DHUPER AS VARCHAR) + '''"
                    + ",''' + CAST(DHLMMN AS VARCHAR) + ''',''' + CAST(DHLMMX AS VARCHAR) + ''',''' + CAST(DHPLCV AS VARCHAR) + '''"
                    + ",''' + CAST(DHCMVL AS VARCHAR) + ''',''' + CAST(DHCAVL AS VARCHAR) + ''',''' + CAST(DHMCVL AS VARCHAR) + '''"
                    + ",''' + CAST(DHCYNO AS VARCHAR) + ''',''' + CAST(DHDLIN AS VARCHAR) + ''',''' + CAST(DHREMA AS VARCHAR) + '''"
                    //+ ",''FN''' + ',''' + CAST(DHSTAT AS VARCHAR) + ''',''' + CAST(DHRCST AS VARCHAR) + '''
                    //+ ",''' + CAST(DHCRDT AS VARCHAR) + ''',''' + CAST(DHCRTM AS VARCHAR) + ''',''' + CAST(DHCRUS AS VARCHAR) + '''"
                    //+ ",''' + CAST(DHCHDT AS VARCHAR) + ''',''' + CAST(DHCHTM AS VARCHAR) + ''',''' + CAST(DHCHUS AS VARCHAR)"
                    //+ " + '''"
                    + "'"
                    + " AS DATA "
                    + "FROM GPR8 "
                    + "JOIN GPR2 ON 1=1 "
                    + "AND DBCONO = DHCONO "
                    + "AND DBBRNO = DHBRNO "
                    + "AND DBCTTC = DHCTTC "
                    + "JOIN GPR1 ON 1=1 "
                    + "AND DACONO = DBCONO "
                    + "AND DABRNO = DBBRNO "
                    + "AND DAPTCD = DBPTCD "
                    + "WHERE 1=1 "
                    + "AND DHSYST <> '' "
                    + "AND DAPTMT = '+' "
                    + "";

            if (strCONO != null)
            {
                strSql += "AND (DHCONO = '" + strCONO.Trim() + "' OR DHCONO = '') ";
            }

            if (strBRNO != null)
            {
                strSql += "AND (DHBRNO = '" + strBRNO.Trim() + "' OR DHBRNO = '') ";
            }

            if (decDTFR != 0)
            {
                strSql += "AND DHDTFR = " + decDTFR.ToString().Trim() + " ";
            }

            if (decDTTO != 0)
            {
                strSql += "AND DHDTTO = " + decDTTO.ToString().Trim() + " ";
            }

            int intTotalPage;
            int intTotalRecord;

            List<GeneralDto> dto = this.ExecutePaging(strSql, "DHCONO, DHBRNO, DHSACD, DHSQLN", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public List<GeneralDto> GetListDownload (string strCONO, string strBRNO, string strEMNO, string strDTFR, string strDTTO)
        {
            string strScript = string.Empty;
            Assembly assem = this.GetType().Assembly;

            using (Stream stream = assem.GetManifestResourceStream("University.Dao.SQLScripts.GetDownload.sql"))
            {
                using (var reader = new StreamReader(stream))
                {
                    strScript = reader.ReadToEnd();
                }
            }

            strScript = strScript.Replace("#CONO", strCONO);
            strScript = strScript.Replace("#BRNO", strBRNO);
            strScript = strScript.Replace("#EMNO", strEMNO);
            strScript = strScript.Replace("#PRNO", "TNS");
            strScript = strScript.Replace("#DTFR", strDTFR);
            strScript = strScript.Replace("#DTTO", strDTTO);

            List<GeneralDto> dto = ExecuteQuery(strScript);
            return dto;
        }

        public List<GeneralDto> GetListPricingDownload(string strCONO, string strBRNO, string strEMNO, string strDTFR, string strDTTO)
        {
            string strScript = string.Empty;
            Assembly assem = this.GetType().Assembly;

            using (Stream stream = assem.GetManifestResourceStream("University.Dao.SQLScripts.GetPricing.sql"))
            {
                using (var reader = new StreamReader(stream))
                {
                    strScript = reader.ReadToEnd();
                }
            }

            strScript = strScript.Replace("#CONO", strCONO);
            strScript = strScript.Replace("#BRNO", strBRNO);
            strScript = strScript.Replace("#EMNO", strEMNO);
            strScript = strScript.Replace("#PRNO", "TNS");
            strScript = strScript.Replace("#DTFR", strDTFR);
            strScript = strScript.Replace("#DTTO", strDTTO);

            List<GeneralDto> dto = ExecuteQuery(strScript);
            return dto;
        }

        #endregion

    }
}
