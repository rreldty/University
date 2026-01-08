using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using MISStandarized.Cryptography;

using University.Dao.Base;
using University.Dto.Zystem;
using University.Dto.Base;

namespace University.Dao.Zystem
{
    public class ZUSRDao : BaseDao<ZUSRDto>
    {
        #region Constructor

        public ZUSRDao()
        {
            this.MainDataSource = DataSource.University;
        }

        #endregion

        #region Abstract Class Implementation

        protected override Mapper<ZUSRDto> GetMapper()
        {
            Mapper<ZUSRDto> mapDto = new ZUSRMappingDto();
            return mapDto;
        }

        #endregion

        #region Save Data

        public string ScriptInsert(ZUSRDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("ZUCONO");
            lstField.Add("ZUBRNO");
            lstField.Add("ZUUSNO");
            lstField.Add("ZUUSNA");
            lstField.Add("ZUNICK");
            lstField.Add("ZUEMAD");
            lstField.Add("ZUPSWD");
            lstField.Add("ZUHASH");
            lstField.Add("ZUUSTY");
            lstField.Add("ZUWHNO");
            lstField.Add("ZUPRNO");
            lstField.Add("ZUCUNO");
            lstField.Add("ZUDENO");
            lstField.Add("ZUOGNO");
            lstField.Add("ZUICTY");
            lstField.Add("ZUREMA");
            lstField.Add("ZUSYST");
            lstField.Add("ZUSTAT");
            lstField.Add("ZURCST");
            lstField.Add("ZUCRDT");
            lstField.Add("ZUCRTM");
            lstField.Add("ZUCRUS");
            lstField.Add("ZUCHDT");
            lstField.Add("ZUCHTM");
            lstField.Add("ZUCHUS");

            return this.GenerateStringInsert("ZUSR", lstField, obj);
        }

        public string ScriptUpdate(ZUSRDto obj)
        {
            string strSql = string.Empty;

            if (obj.isReset)
            {
                List<string> lstField = new List<string>();
                lstField.Add("ZUUSNA");
                lstField.Add("ZUNICK");
                lstField.Add("ZUEMAD");
                lstField.Add("ZUPSWD");
                lstField.Add("ZUHASH");
                lstField.Add("ZUUSTY");
                lstField.Add("ZUWHNO");
                lstField.Add("ZUPRNO");
                lstField.Add("ZUCUNO");
                lstField.Add("ZUDENO");
                lstField.Add("ZUOGNO");
                lstField.Add("ZUICTY");
                lstField.Add("ZUREMA");
                lstField.Add("ZUSYST");
                lstField.Add("ZUSTAT");
                lstField.Add("ZURCST");
                lstField.Add("ZUCHDT");
                lstField.Add("ZUCHTM");
                lstField.Add("ZUCHUS");


                List<string> lstCondition = new List<string>();
                lstCondition.Add("ZUUSNO");

                strSql = this.GenerateStringUpdate("ZUSR", lstCondition, lstField, obj);
            }
            else
            {
                string[] strField = new string[16];
                List<string> lstField = new List<string>(); 
                lstField.Add("ZUUSNA");
                lstField.Add("ZUNICK");
                lstField.Add("ZUEMAD");
                lstField.Add("ZUPSWD");
                lstField.Add("ZUHASH");
                lstField.Add("ZUUSTY");
                lstField.Add("ZUWHNO");
                lstField.Add("ZUPRNO");
                lstField.Add("ZUCUNO");
                lstField.Add("ZUDENO");
                lstField.Add("ZUOGNO");
                lstField.Add("ZUICTY");
                lstField.Add("ZUREMA");
                lstField.Add("ZUSYST");
                lstField.Add("ZUSTAT");
                lstField.Add("ZURCST");
                lstField.Add("ZUCHDT");
                lstField.Add("ZUCHTM");
                lstField.Add("ZUCHUS");


                List<string> lstCondition = new List<string>();
                lstCondition.Add("ZUUSNO");

                strSql = this.GenerateStringUpdate("ZUSR", lstCondition, lstField, obj);
            }

            return strSql;
        }

        public string ScriptUpdateZUSTAT(ZUSRDto obj)
        {
            string strSql = string.Empty;
            List<string> lstField = new List<string>();
            lstField.Add("ZUSTAT");

            lstField.Add("ZUCHDT");
            lstField.Add("ZUCHTM");
            lstField.Add("ZUCHUS");


            List<string> lstCondition = new List<string>();
            lstCondition.Add("ZUUSNO");

            strSql = this.GenerateStringUpdate("ZUSR", lstCondition, lstField, obj);

            return strSql;
        }

        public String SaveZUSTAT(ZUSRDto obj)
        {
            obj.ZUCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZUCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZUCHDT = obj.ZUCRDT;
            obj.ZUCHTM = obj.ZUCRTM;


            if (IsExists(obj))
            {
                string strSql = ScriptUpdateZUSTAT(obj);
                return ExecuteDbNonQuery(strSql);
            }
            else
            {
                return "";
            }

        }

        public string SaveOLD(ZUSRDto obj)
        {
            ZBUMDto dtoZBUM = obj.objZBUM;
            List<ZBUMDto> lstZBUM = obj.listZBUM;

            List<string> lstSql = new List<string>();

            obj.ZUSYST = BaseMethod.SystReady;
            obj.ZUSTAT = BaseMethod.StatDraft;
            obj.ZUCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZUCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZUCHDT = obj.ZUCRDT;
            obj.ZUCHTM = obj.ZUCRTM;

            Encryption _encrypt = new Encryption();
            _encrypt.RsaDynamicEncryption(obj.ZUPSWD, Encryption.DynamicEncrypt.Symmetric);
            obj.ZUPSWD = _encrypt.EncryptedText.ToString();
            obj.ZUHASH = _encrypt.EncryptedKey.ToString();

            ZUSRDao dao = new ZUSRDao();
            ZUSRDto dto = new ZUSRDto();

            if (IsExists(obj))
            {
                lstSql.Add(dao.ScriptUpdate(obj));
            }
            else
            {
                lstSql.Add(dao.ScriptInsert(obj));

                ZBUMDto objInfo = new ZBUMDto();
                objInfo.ZVCONO = obj.ZUCONO;
                objInfo.ZVBRNO = obj.ZUBRNO;
                objInfo.ZVUSNO = obj.ZUUSNO;

                ZBUMDto objTemp = new ZBUMDto();
                objTemp.ZVCONO = obj.ZUCONO;
                objTemp.ZVBRNO = obj.ZUBRNO;
                objTemp.ZVUSNO = obj.ZUUSNO;
                objTemp.ZVSYST = obj.ZUSYST;
                objTemp.ZVSTAT = obj.ZUSTAT;
                objTemp.ZVRCST = obj.ZURCST;
                objTemp.ZVCRDT = obj.ZUCRDT;
                objTemp.ZVCRTM = obj.ZUCRTM;
                objTemp.ZVCRUS = obj.ZUCRUS;
                objTemp.ZVCHDT = obj.ZUCHDT;
                objTemp.ZVCHTM = obj.ZUCHTM;
                objTemp.ZVCHUS = obj.ZUCHUS;

                ZBUMDao daoZBUM = new ZBUMDao();

                if (daoZBUM.Get(objInfo) == null)
                {
                    lstSql.Add(daoZBUM.ScriptInsert(objTemp));
                }
                else
                {
                    lstSql.Add(daoZBUM.ScriptUpdate(objTemp));
                }
            }


            if (dtoZBUM != null)
            {
                ZBUMDao daoZBUM = new ZBUMDao();

                dtoZBUM.ZVSYST = BaseMethod.SystReady;
                dtoZBUM.ZVSTAT = BaseMethod.StatDraft;
                dtoZBUM.ZVRCST = BaseMethod.RecordStatusActive;
                dtoZBUM.ZVCRDT = BaseMethod.DateToNumeric(DateTime.Now);
                dtoZBUM.ZVCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
                dtoZBUM.ZVCHDT = dtoZBUM.ZVCRDT;
                dtoZBUM.ZVCHTM = dtoZBUM.ZVCRTM;

                if (!daoZBUM.IsExists(dtoZBUM))
                    lstSql.Add(daoZBUM.ScriptInsert(dtoZBUM));
                else
                    lstSql.Add(daoZBUM.ScriptUpdate(dtoZBUM));
            }

            if (lstZBUM != null)
            {
                ZBUMDao daoZBUM = new ZBUMDao();

                foreach (ZBUMDto objZBUM in lstZBUM)
                {
                    objZBUM.ZVSYST = BaseMethod.SystReady;
                    objZBUM.ZVSTAT = BaseMethod.StatDraft;
                    objZBUM.ZVRCST = BaseMethod.RecordStatusActive;
                    objZBUM.ZVCRDT = BaseMethod.DateToNumeric(DateTime.Now);
                    objZBUM.ZVCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
                    objZBUM.ZVCHDT = objZBUM.ZVCRDT;
                    objZBUM.ZVCHTM = objZBUM.ZVCRTM;

                    if (!daoZBUM.IsExists(objZBUM))
                        lstSql.Add(daoZBUM.ScriptInsert(objZBUM));
                    else
                        lstSql.Add(daoZBUM.ScriptUpdate(objZBUM));
                }
            }

            return ExecuteDbNonQueryTransaction(lstSql);
        }

        public string Save(ZUSRDto obj)
        {
            String ZUSTAT = obj.ZUSTAT;

            List<string> lstSql = new List<string>();

            obj.ZUSYST = BaseMethod.SystReady;
            //obj.ZUSTAT = BaseMethod.StatDraft;
            obj.ZUCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZUCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZUCHDT = obj.ZUCRDT;
            obj.ZUCHTM = obj.ZUCRTM;

            Encryption _encrypt = new Encryption();
            _encrypt.RsaDynamicEncryption(obj.ZUPSWD, Encryption.DynamicEncrypt.Symmetric);
            obj.ZUPSWD = _encrypt.EncryptedText.ToString();
            obj.ZUHASH = _encrypt.EncryptedKey.ToString();

            ZUSRDao dao = new ZUSRDao();
            ZUSRDto dto = new ZUSRDto();

            if (IsExists(obj))
            {
                lstSql.Add(dao.ScriptUpdate(obj));
            }
            else
            {
                obj.ZUSTAT = !string.IsNullOrEmpty(ZUSTAT) ? ZUSTAT : obj.ZUSTAT;

                lstSql.Add(dao.ScriptInsert(obj));
            }


            if (!string.IsNullOrEmpty(obj.ZHUGNO))
            {
                ZUG2Dao daoZUG2 = new ZUG2Dao();
                ZUG2Dto dtoZUG2 = new ZUG2Dto();
                dtoZUG2.ZHCONO = string.Empty;// obj.ZUCONO;
                dtoZUG2.ZHBRNO = string.Empty;//obj.ZUBRNO;
                dtoZUG2.ZHUGNO = obj.ZHUGNO;
                dtoZUG2.ZHUSNO = obj.ZUUSNO;
                dtoZUG2.ZHSYST = obj.ZUSYST;
                dtoZUG2.ZHSTAT = obj.ZUSTAT;
                dtoZUG2.ZHRCST = obj.ZURCST;
                dtoZUG2.ZHCRDT = obj.ZUCRDT;
                dtoZUG2.ZHCRTM = obj.ZUCRTM;
                dtoZUG2.ZHCRUS = obj.ZUCRUS;
                dtoZUG2.ZHCHDT = obj.ZUCHDT;
                dtoZUG2.ZHCHTM = obj.ZUCHTM;
                dtoZUG2.ZHCHUS = obj.ZUCHUS;

                if (!daoZUG2.IsExistsUserGroup(dtoZUG2))
                    lstSql.Add(daoZUG2.ScriptInsert(dtoZUG2));
                else
                {
                    //lstSql.Add(daoZUG2.ScriptUpdateUserGroup(dtoZUG2));
                    lstSql.Add("UPDATE ZUG2 SET ZHUGNO = " + dtoZUG2.ZHUGNO
                                + ", ZHSYST = '" + dtoZUG2.ZHSYST + "'"
                                + ", ZHSTAT = '" + BaseMethod.StatDraft + "'"
                                + ", ZHRCST = " + dtoZUG2.ZHRCST
                                + ", ZHCHDT = " + dtoZUG2.ZHCHDT
                                + ", ZHCHTM = " + dtoZUG2.ZHCHTM
                                + ", ZHCHUS = " + dtoZUG2.ZHCHUS
                                + " WHERE 1=1"
                                + " AND ZHCONO = '" + dtoZUG2.ZHCONO + "'"
                                + " AND ZHBRNO = '" + dtoZUG2.ZHBRNO + "'"
                                + " AND ZHUSNO = '" + dtoZUG2.ZHUSNO + "'"
                                + " AND ZHRCST = 1 "
                                );
                }
            }

            return ExecuteDbNonQueryTransaction(lstSql);
        }

        public String ScriptSave(ZUSRDto obj)
        {
            obj.ZUSYST = BaseMethod.SystReady;
            obj.ZUSTAT = BaseMethod.StatDraft;
            obj.ZUCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZUCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZUCHDT = obj.ZUCRDT;
            obj.ZUCHTM = obj.ZUCRTM;

            Encryption _encrypt = new Encryption();
            _encrypt.RsaDynamicEncryption(obj.ZUPSWD, Encryption.DynamicEncrypt.Symmetric);
            obj.ZUPSWD = _encrypt.EncryptedText.ToString();
            obj.ZUHASH = _encrypt.EncryptedKey.ToString();

            if (IsExists(obj))
            {
                return ScriptUpdate(obj);
            }
            else
            {
                return ScriptInsert(obj);
            }

        }

        //public List<string> ScriptSave(ZUSRDto obj)
        //{
        //    List<string> lstSql = new List<string>();

        //    obj.ZUSYST = BaseMethod.SystReady;
        //    obj.ZUSTAT = BaseMethod.StatDraft;
        //    obj.ZUCRDT = BaseMethod.DateToNumeric(DateTime.Now);
        //    obj.ZUCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
        //    obj.ZUCHDT = obj.ZUCRDT;
        //    obj.ZUCHTM = obj.ZUCRTM;

        //    Encryption _encrypt = new Encryption();
        //    _encrypt.RsaDynamicEncryption(obj.ZUPSWD, Encryption.DynamicEncrypt.Symmetric);
        //    obj.ZUPSWD = _encrypt.EncryptedText.ToString();
        //    obj.ZUHASH = _encrypt.EncryptedKey.ToString();

        //    ZUSRDao dao = new ZUSRDao();
        //    ZUSRDto dto = new ZUSRDto();

        //    //dto = dao.GetByUserIdNick(obj);

        //    if (IsExists(obj))
        //    {
        //        if (dto != null)
        //        {
        //            if (dto.ZUUSNO != obj.ZUUSNO)
        //                lstSql.Add("User nickname already in use.");
        //            else
        //                lstSql.Add(dao.ScriptUpdate(obj));
        //        }
        //        else
        //            lstSql.Add(dao.ScriptUpdate(obj));
        //    }
        //    else
        //    {
        //        if (dto != null)
        //        {
        //            if (dto.ZUUSNO != obj.ZUUSNO)
        //                lstSql.Add("User nickname already in use.");
        //        }
        //        else
        //            lstSql.Add(dao.ScriptInsert(obj));
        //    }

        //    ZBUMDto objInfo = new ZBUMDto();
        //    objInfo.ZVCONO = obj.ZUCONO;
        //    objInfo.ZVBRNO = obj.ZUBRNO;
        //    objInfo.ZVUSNO = obj.ZUUSNO;

        //    ZBUMDto objTemp = new ZBUMDto();
        //    objTemp.ZVCONO = obj.ZUCONO;
        //    objTemp.ZVBRNO = obj.ZUBRNO;
        //    objTemp.ZVUSNO = obj.ZUUSNO;
        //    objTemp.ZVSYST = obj.ZUSYST;
        //    objTemp.ZVSTAT = obj.ZUSTAT;
        //    objTemp.ZVRCST = obj.ZURCST;
        //    objTemp.ZVCRDT = obj.ZUCRDT;
        //    objTemp.ZVCRTM = obj.ZUCRTM;
        //    objTemp.ZVCRUS = obj.ZUCRUS;
        //    objTemp.ZVCHDT = obj.ZUCHDT;
        //    objTemp.ZVCHTM = obj.ZUCHTM;
        //    objTemp.ZVCHUS = obj.ZUCHUS;

        //    ZBUMDao daoZBUM = new ZBUMDao();

        //    if (daoZBUM.Get(objInfo) == null)
        //    {
        //        lstSql.Add(daoZBUM.ScriptInsert(objTemp));
        //    }
        //    else
        //    {
        //        lstSql.Add(daoZBUM.ScriptUpdate(objTemp));
        //    }

        //    return lstSql;
        //}

        public List<string> ScriptSaveFromZBRC(ZUSRDto obj)
        {
            List<string> lstSql = new List<string>();

            obj.ZUSYST = BaseMethod.SystReady;
            obj.ZUSTAT = BaseMethod.StatDraft;
            obj.ZUCRDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZUCRTM = BaseMethod.TimeToNumeric(DateTime.Now);
            obj.ZUCHDT = obj.ZUCRDT;
            obj.ZUCHTM = obj.ZUCRTM;

            Encryption _encrypt = new Encryption();
            _encrypt.RsaDynamicEncryption(obj.ZUPSWD, Encryption.DynamicEncrypt.Symmetric);
            obj.ZUPSWD = _encrypt.EncryptedText.ToString();
            obj.ZUHASH = _encrypt.EncryptedKey.ToString();

            lstSql.Add(ScriptInsert(obj));

            ZBUMDto objInfo = new ZBUMDto();
            objInfo.ZVCONO = obj.ZUCONO;
            objInfo.ZVBRNO = obj.ZUBRNO;
            objInfo.ZVUSNO = obj.ZUUSNO;

            ZBUMDto objTemp = new ZBUMDto();
            objTemp.ZVCONO = obj.ZUCONO;
            objTemp.ZVBRNO = obj.ZUBRNO;
            objTemp.ZVUSNO = obj.ZUUSNO;
            objTemp.ZVSYST = obj.ZUSYST;
            objTemp.ZVSTAT = obj.ZUSTAT;
            objTemp.ZVRCST = obj.ZURCST;
            objTemp.ZVCRDT = obj.ZUCRDT;
            objTemp.ZVCRTM = obj.ZUCRTM;
            objTemp.ZVCRUS = obj.ZUCRUS;
            objTemp.ZVCHDT = obj.ZUCHDT;
            objTemp.ZVCHTM = obj.ZUCHTM;
            objTemp.ZVCHUS = obj.ZUCHUS;

            ZBUMDao daoZBUM = new ZBUMDao();

            if (daoZBUM.Get(objInfo) == null)
            {
                lstSql.Add(daoZBUM.ScriptInsert(objTemp));
            }
            else
            {
                lstSql.Add(daoZBUM.ScriptUpdate(objTemp));
            }

            return lstSql;
        }

        public string ChangePassword(ZUSRDto obj)
        {
            obj.ZUSYST = BaseMethod.SystReady;
            obj.ZUSTAT = BaseMethod.StatDraft;
            obj.ZUCHDT = BaseMethod.DateToNumeric(DateTime.Now);
            obj.ZUCHTM = BaseMethod.TimeToNumeric(DateTime.Now);

            ZUSRDto objInfo = GetByUserId(obj);

            string strResult = String.Empty;

            try
            {
                Encryption _encrypt = new Encryption();
                string strPass = _encrypt.RsaDynamicDecryption(objInfo.ZUPSWD, objInfo.ZUHASH, Encryption.DynamicEncrypt.Symmetric);

                if (obj.ZUPSWD != strPass)
                {
                    strResult = "Your current password is incorect!";
                }
                

                if (string.IsNullOrEmpty(strResult))
                {
                    _encrypt.RsaDynamicEncryption(obj.NewPassword, Encryption.DynamicEncrypt.Symmetric);
                    obj.ZUPSWD = _encrypt.EncryptedText.ToString();
                    obj.ZUHASH = _encrypt.EncryptedKey.ToString();


                    string[] strField = new string[7];
                    strField[0] = "ZUPSWD";
                    strField[1] = "ZUHASH";
                    strField[2] = "ZUSYST";
                    strField[3] = "ZUSTAT";
                    strField[4] = "ZUCHDT";
                    strField[5] = "ZUCHTM";
                    strField[6] = "ZUCHUS";

                    string[] strCondition = new string[1];
                    strCondition[0] = "ZUUSNO";

                    strResult = ExecuteDbNonQuery(this.GenerateStringUpdate("ZUSR", strCondition, strField, obj));
                    
                }
            }
            catch (Exception ex)
            {
                strResult = ex.ToString();
            }

            return strResult;
        }

        public string ScriptUpdateMBFL(ZUSRDto obj)
        {
            string strSql = string.Empty;

            List<string> lstField = new List<string>();
            lstField.Add("ZUMBFL");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("ZUUSNO");

            strSql = this.GenerateStringUpdate("ZUSR", lstCondition, lstField, obj);
            return strSql;
        }

        #endregion

        #region Submit Data
        public string SubmitData(ZUSRDto obj)
        {
            List<ZUSRDto> lst = obj.listZUSR;
            string strResult = string.Empty;
            List<string> lstSql = new List<string>();

            if (lst != null)
            {
                foreach (ZUSRDto dtoZUSR in lst)
                {
                    lstSql.Add(Save(dtoZUSR));
                }
            }

            if (lstSql.Count != 0)
                strResult = ExecuteDbNonQueryTransaction(lstSql);

            if (string.IsNullOrEmpty(strResult))
                return strResult;
            else
                return strResult;
        }
        #endregion

        #region Delete Data

        public string Delete(ZUSRDto obj)
        {
            string[] strCondition = new string[0];
            strCondition[1] = "ZUUSNO";

            string strSql = this.GenerateStringDelete("ZUSR", strCondition, obj);
            return this.ExecuteDbNonQuery(strSql);
        }

        #endregion

        #region Get Data

        public bool IsExists(ZUSRDto obj)
        {
            string strSql = "SELECT CASE WHEN EXISTS"
                            + " ("
                            + " SELECT * "
                            + " FROM ZUSR "
                            + " WHERE 1=1 "
                            + " AND ZUUSNO = '" + obj.ZUUSNO.Trim() + "'"
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

        public ZUSRDto CheckUserLogin(ZUSRDto objInfo)
        {
            ZUSRDto obj = null;
            bool IsExpire = false;
            bool IsSuperAdmin = false;
            Encryption _encrypt = new Encryption();
            ZLOGDao daoLog = new ZLOGDao();

            try
            {
                if ((objInfo.ZUUSNO.ToLower() == BaseMethod.SuperAdminId.ToLower()))
                {
                    string strPass = BaseMethod.SuperAdminPwd;
                    string strPassword = _encrypt.RsaDynamicDecryption(objInfo.ZUPSWD, objInfo.ZUHASH, Encryption.DynamicEncrypt.Symmetric);

                    if (strPassword != strPass)
                    {
                        throw new Exception("Please check your User Id or Password!");
                    }
                    else
                    {
                        obj = new ZUSRDto();

                        IsSuperAdmin = true;

                        obj.ZUUSNO = BaseMethod.SuperAdminId;
                        obj.ZUUSNA = "Super Administrator";
                        //obj.ZUPRNO = "";
                        obj.ZUNICK = "SUPER";
                        obj.ZURCST = 1;
                        obj.ZUUSTY = "ADMIN";
                        obj.ZUCONO = "TMW";
                        obj.ZUBRNO = "";

                        objInfo.ZUPSWD = string.Empty;
                        objInfo.ZUHASH = string.Empty;
                        obj.ZUPSWD = string.Empty;
                        obj.ZUHASH = string.Empty;

                    }
                }
                else
                {
                    obj = this.GetByUserId(objInfo);

                    if (obj != null)
                    {
                        if (obj.ZURCST != 0)
                        {
                            string strPass = _encrypt.RsaDynamicDecryption(obj.ZUPSWD, obj.ZUHASH, Encryption.DynamicEncrypt.Symmetric);
                            string strPassword = Encoding.UTF8.GetString(Convert.FromBase64String(objInfo.ZUPSWD));

                            if (strPassword != strPass)
                            {
                                throw new Exception("Please check your User Id or Password!");
                            }
                            else
                            {
                                if (obj.ZUUSNO.ToLower() == BaseMethod.SysadminId.ToLower())
                                {
                                    IsSuperAdmin = true;
                                }

                                objInfo.ZUPSWD = string.Empty;
                                objInfo.ZUHASH = string.Empty;
                                obj.ZUPSWD = strPass;// string.Empty;
                                obj.ZUHASH = string.Empty;

                                ZLOGDto objL = new ZLOGDto()
                                {
                                    ZLCONO = obj.ZUCONO,
                                    ZLBRNO = obj.ZUBRNO,
                                    ZLUSNO = obj.ZUUSNO,
                                    ZLLGDT = BaseMethod.DateToNumeric(DateTime.Now),
                                    ZLLGTM = BaseMethod.TimeToNumeric(DateTime.Now),
                                    ZLLGTY = "I",
                                    ZLLGIP = objInfo.ZLLGIP,
                                    ZLREMA = string.Empty,
                                    ZLSYST = "FN",
                                    ZLSTAT = string.Empty,
                                    ZLRCST = 1,
                                    ZLCRDT = BaseMethod.DateToNumeric(DateTime.Now),
                                    ZLCRTM = BaseMethod.TimeToNumeric(DateTime.Now),
                                    ZLCRUS = obj.ZUUSNO,
                                    ZLCHDT = BaseMethod.DateToNumeric(DateTime.Now),
                                    ZLCHTM = BaseMethod.TimeToNumeric(DateTime.Now),
                                    ZLCHUS = obj.ZUUSNO,
                                };

                                //Check if user Login
                                string strLogMessage = string.Empty;

                                ZLOGDto objLog = daoLog.GetUserLastStatus(objL);
                                if (objLog != null)
                                {
                                    //if (objLog.ZLLGIP.Equals(objInfo.ZLLGIP) && objLog.ZLLGTY.Equals("I") && objInfo.LoginDate > 0)
                                    {
                                        //Do Logout because it's crash before
                                        objL.ZLLGTY = "O";
                                        strLogMessage = daoLog.Save(objL);
                                    }
                                }
                                else {
                                    strLogMessage = daoLog.Save(objL);
                                }
                                //if (string.IsNullOrEmpty(strLogMessage))
                                //{
                                //    //Login
                                //    objL.ZLLGTY = "I";
                                //      strLogMessage = daoLog.Save(objL);


                                //    //Check User MBFL
                                //    if (obj.ZUMBFL == 0)
                                //    {
                                //        //Update MBFL
                                //        ZUSRDto objMBFL = new ZUSRDto()
                                //        {
                                //            ZUCONO = "",
                                //            ZUBRNO = "",
                                //            ZUUSNO = obj.ZUUSNO,
                                //            ZUMBFL = 1,
                                //        };

                                //        string strUserMBFL = string.Empty;
                                //        strUserMBFL = ExecuteDbNonQuery(this.ScriptUpdateMBFL(objMBFL));
                                //    }

                                //}

                            }
                        }
                        else
                        {
                            throw new Exception("User Id is inactive \nPlease contact your Administrator.");
                        }
                    }
                    else
                    {
                        throw new Exception("Please check your User Id or Password!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            obj.IsSuperAdmin = IsSuperAdmin;
            obj.IsExpire = IsExpire;

            return obj;
        }

        //int GetLicenseUser(ZUSRDto obj)
        //{
        //    ZLICDto objInfo = new ZLICDto();
        //    objInfo.ZICONO = obj.ZUCONO;
        //    objInfo.ZIBRNO = obj.ZUBRNO;
        //    objInfo.ZIPRNO = obj.ZUPRNO;
        //    objInfo.ZIAPNO = "AGLIS";

        //    ZLICDao dao = new ZLICDao();
        //    return dao.Get(objInfo);
        //}

        public ZUSRDto Get(ZUSRDto obj)
        {
            List<string> lstField = new List<string>();
            lstField.Add("ZUCONO");
            lstField.Add("ZUBRNO");
            lstField.Add("ZUUSNO");
            lstField.Add("ZUUSNA");
            lstField.Add("ZUNICK");
            lstField.Add("ZUEMAD");
            lstField.Add("ZUPSWD");
            lstField.Add("ZUHASH");
            lstField.Add("ZUUSTY");
            lstField.Add("ZUMOBN");
            lstField.Add("ZUFMKY");
            lstField.Add("ZUREMA");
            lstField.Add("ZUSYST");
            lstField.Add("ZUSTAT");
            lstField.Add("ZURCST");
            lstField.Add("ZUCRDT");
            lstField.Add("ZUCRTM");
            lstField.Add("ZUCRUS");
            lstField.Add("ZUCHDT");
            lstField.Add("ZUCHTM");
            lstField.Add("ZUCHUS");

            List<string> lstCondition = new List<string>();
            lstCondition.Add("ZUUSNO");

            string strSql = this.GenerateStringSelect("ZUSR", lstCondition, lstField, obj);
            ZUSRDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUSRDto GetUser(ZUSRDto obj)
        {
            string strSql = "SELECT * "
                            + " FROM ZUSR "
                            + " WHERE 1=1 "
                            + " AND LOWER(ZUUSNO) = LOWER('" + obj.ZUUSNO.Trim() + "')"
                            + "";

            ZUSRDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public List<ZUSRDto> GetHeadOfficeUser()
        {
            string strSql = "SELECT"
                    + " ZUUSNO"
                    + " FROM ZUSR"
                    + " JOIN ZCMP ON 1=1"
                    + "     AND ZCCONO = ZUCONO"
                    + " WHERE 1=1"
                    + "     AND ZCCOTY = '" + BaseMethod.GetVariableValue("COTY_HEADOFFICE") + "'";

            List<ZUSRDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public ZUSRDto GetFirst(ZUSRDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + "  ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    + ", ZUMOBN "
                    + ", ZUFMKY "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + " FROM ZUSR "
                    + " WHERE 1=1 "
                    + " ORDER BY ZUUSNO ASC"
                    + "";

            ZUSRDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUSRDto GetPrevious(ZUSRDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + "  ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    + ", ZUMOBN "
                    + ", ZUFMKY "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + " FROM ZUSR "
                    + " WHERE 1=1 "
                    + " AND ZUUSNO < '" + obj.ZUUSNO.Trim() + "' "
                    + " ORDER BY ZUUSNO DESC"
                    + "";

            ZUSRDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUSRDto GetNext(ZUSRDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + "  ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    + ", ZUMOBN "
                    + ", ZUFMKY "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + " FROM ZUSR "
                    + " WHERE 1=1 "
                    + " AND ZUUSNO > '" + obj.ZUUSNO.Trim() + "' "
                    + " ORDER BY ZUUSNO ASC"
                    + "";

            ZUSRDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUSRDto GetLast(ZUSRDto obj)
        {
            string strSql = "SELECT TOP 1"
                    + "  ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    + ", ZUMOBN "
                    + ", ZUFMKY "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + " FROM ZUSR "
                    + " WHERE 1=1 "
                    + " ORDER BY ZUUSNO DESC"
                    + "";

            ZUSRDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUSRDto GetByUserId(ZUSRDto obj)
        {
            string strSql = "SELECT"
                    + " ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    + ", ZUWHNO "
                    + ", ZUICTY "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + ", ZCCONA "
                    + ", ZCCOTY "
                    + ", ZCNOVT "
                    + ", ZUPRNO "
                    + ", ZBBRNA "
                    + ", ZHUGNO "
                    + " from ZUSR"
                    + " left join ZCMP on 1=1"
                    + "     and ZCCONO = ZUCONO"
                    + " left join ZBRC on 1=1"
                    + "     and ZBCONO = ZUCONO"
                    + "     and ZBBRNO = ZUBRNO"
                    + " left join ZUG2 on 1=1"
                    + "     and ZHUSNO = ZUUSNO"
                    + " WHERE 1=1";

            if (obj.ZUUSNO != null && obj.ZUUSNO != String.Empty)
            {
                strSql += " AND ZUUSNO = '" + obj.ZUUSNO.Trim() + "' ";
            }

            ZUSRDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUSRDto GetByUserIdNick(ZUSRDto obj)
        {
            string strSql = "SELECT"
                    + "  ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    + ", ZUMOBN "
                    + ", ZUFMKY "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + " FROM ZUSR"
                    + " WHERE 1=1";

            if (obj.ZUCONO != null)
            {
                strSql += " AND ZUCONO = '" + obj.ZUCONO.Trim() + "'";
            }

            if (obj.ZUBRNO != null)
            {
                strSql += " AND ZUBRNO = '" + obj.ZUBRNO.Trim() + "'";
            }

            if (obj.ZUNICK != null && obj.ZUNICK != String.Empty)
            {
                strSql += " AND ZUNICK = '" + obj.ZUNICK.Trim() + "'";
            }

            ZUSRDto dto = this.ExecuteQueryOne(strSql);
            return dto;
        }

        public ZUSRDto GetwithZUG(ZUSRDto obj)
        {
            string strSql = "SELECT "
                    + "  ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    //+ ", ZUMOBN "
                    //+ ", ZUFMKY "
                    //+ ", ZUVENO "
                    //+ ", ZUACNO "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + ", ZHUGNO "
                    + " FROM ZUSR "
                    + " JOIN ZUG2 ON 1=1 "
                    + "     AND ZHUSNO = ZUUSNO "
                    + "     AND ZHRCST = 1 "
                    + "WHERE 1=1 ";

            //if (obj.ZUCONO != null)
            if(!string.IsNullOrEmpty(obj.ZUCONO))
            {
                strSql += "AND ZUCONO = '" + obj.ZUCONO.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(obj.ZUBRNO))
            {
                strSql += "AND ZUBRNO = '" + obj.ZUBRNO.Trim() + "' ";
            }

            if (obj.ZUUSNO != null && obj.ZUUSNO != String.Empty)
            {
                strSql += "AND ZUUSNO = '" + obj.ZUUSNO.Trim() + "' ";
            }

            ZUSRDto dto = this.ExecuteQueryOne(strSql);
            //SFAEncryption _encrypt = new SFAEncryption();
            //dto.ZUPSWD = _encrypt.Decrypt(dto.ZUPSWD);
            return dto;
        }

        public List<ZUSRDto> GetUSERNotification(string PRNO, decimal CHLN)
        {
            string strSql = "SELECT DISTINCT ZUUSNO,ZUUSNA,ZUEMAD,ZUMOBN "
                            + " FROM PR02 "
                            + "  LEFT JOIN PR05 ON 1 = 1 "
                            + "  AND N5CONO = N2CONO "
                            + "  AND N5BRNO = N2BRNO "
                            + "  AND N5PRNO = N2PRNO "
                            + "  LEFT JOIN ZUCM ON 1 = 1 "
                            + "  AND ZOCONO = N2CONO "
                            + "  AND ZOBRNO = N2BRNO "
                            + "  AND ZOCHNL = N2CHNL "
                            + "  AND ZORCST = 1 "
                            + "  JOIN ZUSR ON 1 = 1 "
                            + "  AND ZUCONO = N2CONO "
                            + "  AND ZUBRNO = N2BRNO "
                            + "  AND (ZUUSNO = N5USNO OR ZUUSNO = ZOUSNO OR ZUUSNO = N2SBBY OR ZUUSTY = 'CVT') "
                            + "  AND ZURCST = 1 "
                            + "  WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(PRNO))
            {
                strSql += "AND N2PRNO = '" + PRNO.Trim() + "' ";
            }

            if (CHLN != 0)
            {
                strSql += "AND N2CHLN = '" + CHLN + "' ";
            }
            else
            {
                strSql += "AND N2DCST IN ('D', '" + BaseMethod.GetVariableValue("CPDC_WAITFORREVIEW") + "') ";
            }

            List<ZUSRDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }


        public List<ZUSRDto> GetList(ZUSRDto obj)
        {
            string strSql = "SELECT "
                    + "  ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    + ", ZUMOBN "
                    + ", ZUFMKY "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + "FROM ZUSR WHERE 1=1 ";

            if (obj.ZUCONO != null)
            {
                strSql += "AND ZUCONO = '" + obj.ZUCONO.Trim() + "' ";
            }

            if (obj.ZUBRNO != null)
            {
                strSql += "AND ZUBRNO = '" + obj.ZUBRNO.Trim() + "' ";
            }

            if (obj.ZUUSNO != null && obj.ZUUSNO != String.Empty)
            {
                strSql += "AND ZUUSNO = '" + obj.ZUUSNO.Trim() + "' ";
            }

            List<ZUSRDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZUSRDto> GetListUser(ZUSRDto obj)
        {
            string strSql = "SELECT "
                    + "  ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    + ", ZUMOBN "
                    + ", ZUFMKY "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + "FROM ZUSR WHERE 1=1 ";

            if (obj.ZUCONO != null)
            {
                strSql += "AND ZUCONO = '" + obj.ZUCONO.Trim() + "' ";
            }

            if (obj.ZUBRNO != null)
            {
                strSql += "AND ZUBRNO = '" + obj.ZUBRNO.Trim() + "' ";
            }

            if (obj.ZUUSNO != null && obj.ZUUSNO != String.Empty)
            {
                strSql += "AND (ZUUSNO LIKE '%" + obj.ZUUSNO.Trim() + "%' OR ZUUSNA LIKE '%" + obj.ZUUSNO.Trim() + "%')";
            }

            if (obj.ZURCST != 0)
            {
                strSql += "AND ZURCST = " + obj.ZURCST + "";
            }

            List<ZUSRDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }

        public List<ZUSRDto> GetListPaging(out int intTotalPage, out int intTotalRecord, ZUSRDto obj, int intPageNumber, int intPageSize)
        {
            string strSql = "SELECT "
                    + "  ZUCONO "
                    + ", ZUBRNO "
                    + ", ZUUSNO "
                    + ", ZUUSNA "
                    + ", ZUNICK "
                    + ", ZUEMAD "
                    + ", ZUPSWD "
                    + ", ZUHASH "
                    + ", ZUUSTY "
                    + ", ZUMOBN "
                    + ", ZUFMKY "
                    + ", ZUREMA "
                    + ", ZUSYST "
                    + ", ZUSTAT "
                    + ", ZURCST "
                    + ", ZUCRDT "
                    + ", ZUCRTM "
                    + ", ZUCRUS "
                    + ", ZUCHDT "
                    + ", ZUCHTM "
                    + ", ZUCHUS "
                    + "FROM ZUSR WHERE 1=1 ";

            if (obj.ZUCONO != null)
            {
                strSql += "AND ZUCONO = '" + obj.ZUCONO.Trim() + "' ";
            }

            if (obj.ZUBRNO != null)
            {
                strSql += "AND ZUBRNO = '" + obj.ZUBRNO.Trim() + "' ";
            }

            if (obj.ZUUSNO != null && obj.ZUUSNO != String.Empty)
            {
                strSql += "AND ZUUSNO = '" + obj.ZUUSNO.Trim() + "' ";
            }

            List<ZUSRDto> dto = this.ExecutePaging(strSql, "ZUCONO, ZUBRNO, ZUUSNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public List<ZUSRDto> GetListPagingNotInUserGroup(out int intTotalPage, out int intTotalRecord, ZUSRDto obj, int intPageNumber, int intPageSize)
        {
            string strSql = "SELECT "
                    + "ZUCONO "
                    + ",ZUBRNO "
                    + ",ZUUSNO "
                    + ",ZUUSNA "
                    + ",ZUNICK "
                    + ",ZURCST "
                    + ",ZUCHDT "
                    + ",ZUCHTM "
                    + ",ZUCHUS "
                    + ",ZRVANA "
                    + "FROM ZUSR "
                    + " LEFT JOIN ZVAR ON 1=1"
                    + "     AND ZRCONO = '' "
                    + "     AND ZRBRNO = '' "
                    + "     AND ZRVATY = 'RCST' "
                    + "     AND ZRVAVL = ZURCST "
                    + " WHERE 1=1 "
                    + " AND ZURCST = " + BaseMethod.RecordStatusActive + " "
                    + " AND ZUUSNO NOT IN "
                    + " ("
                    + " SELECT ZHUSNO "
                    + " FROM ZUG2 "
                    + " WHERE 1=1"
                    //+ " AND ZHCONO = '' "
                    //+ " AND ZHBRNO = '' "
                    //+ " AND ZHUGNO = '" + obj.ZHUGNO + "' "
                    + " AND ZHRCST = 1 "
                    + " )"
                    + "";

            if (!string.IsNullOrEmpty(obj.ZUUSNO))
            {
                strSql += " AND ZUUSNO LIKE '%" + obj.ZUUSNO.Trim() + "%' ";
            }

            if (!string.IsNullOrEmpty(obj.ZUUSNA))
            {
                strSql += " AND ZUUSNA LIKE '%" + obj.ZUUSNA.Trim() + "%' ";
            }

            if (!string.IsNullOrEmpty(obj.ZUNICK))
            {
                strSql += " AND ZUNICK LIKE '%" + obj.ZUNICK.Trim() + "%' ";
            }

            List<ZUSRDto> dto = this.ExecutePaging(strSql, "ZUCONO, ZUBRNO, ZUUSNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);

            return dto;
        }

        public List<ZUSRDto> GetListSearch(out int intTotalPage, out int intTotalRecord, ZUSRDto obj, int intPageNumber, int intPageSize)
        {
            string strSql = "SELECT "
                    + " ZUUSNO "
                    + ", ZUUSNA "
                    + ", ISNULL(ZSACNO, '') AS ZSACNO "
                    + ", ISNULL(G1ACNA, '') AS G1ACNA"
                    + ", ISNULL(ZRVANA, '') AS ZRVANA "
                    + " FROM ZUSR "
                    + " LEFT JOIN ZAUM ON 1=1 "
                    + " AND ZSCONO = '' "
                    + " AND ZSBRNO = '' "
                    + " AND ZSUSNO = ZUUSNO "
                    + " AND ZSRCST = 1 "
                    + " LEFT JOIN GAC1 ON 1=1 "
                    + " AND G1CONO = '' "
                    + " AND G1BRNO = '' "
                    + " AND G1ACNO = ZSACNO "
                    + " LEFT JOIN ZVAR ON 1=1 "
                    + " AND ZRCONO = '' "
                    + " AND ZRBRNO = '' "
                    + " AND ZRVATY = 'RCST' "
                    + " AND ZRVAVL = ZURCST "
                    + " WHERE 1=1 "
                    + " AND ZUCONO = '' "
                    + " AND ZUBRNO = '' "
                    + "";

            if (obj.ZUUSNO != null && obj.ZUUSNO != String.Empty)
            {
                strSql += "AND ZUUSNO LIKE '%" + obj.ZUUSNO.Trim() + "%' ";
            }

            if (obj.ZUUSNA != null && obj.ZUUSNA != String.Empty)
            {
                strSql += "AND ZUUSNA LIKE '%" + obj.ZUUSNA.Trim() + "%' ";
            }

            if (obj.ZSACNO != null && obj.ZSACNO != String.Empty)
            {
                strSql += "AND ZSACNO = '" + obj.ZSACNO + "' ";
            }

            if (obj.ZURCST != null && obj.ZURCST <= 1)
            {
                strSql += "AND ZURCST = " + obj.ZURCST + " ";
            }
            List<ZUSRDto> dto = ExecutePaging(strSql, "ZUUSNO", intPageNumber, intPageSize, out intTotalPage, out intTotalRecord);
            return dto;
        }

        public List<ZUSRDto> GetListPIC(ZUSRDto obj)
        {
            string strSql = "SELECT DISTINCT  ZUUSNO , ZUUSNA "
                        + " FROM ( "
                        + " SELECT A.ZUUSNO, A.ZUUSNA , A.ZUUSTY,B.CSSPNO "
                        + ", CASE WHEN ZUBM.N2PRNO <> '' THEN ZUBM.N2PRNO WHEN ZUCM.N2PRNO <> '' THEN ZUCM.N2PRNO ELSE '' END N2PRNO,CASE WHEN ZUBM.N2CHLN <> 0 THEN ZUBM.N2CHLN WHEN ZUCM.N2CHLN <> 0 THEN ZUCM.N2CHLN ELSE 0 END N2CHLN "
                        + " FROM ZUSR A "
                        + " LEFT JOIN (SELECT ZUCONO, ZUBRNO, ZUUSNO, ZUUSNA, ZUUSTY, ZXIC02, N2PRNO, N2CHLN FROM ZUSR JOIN ZUBM ON 1=1	AND ZXCONO = ZUCONO	AND ZXBRNO = ZUBRNO	AND ZXUSNO = ZUUSNO	JOIN PR02 ON 1=1 AND N2CONO = ZXCONO AND N2BRNO = ZXBRNO AND N2IC02 = ZXIC02 WHERE 1=1 AND ZURCST = 1 AND ZXRCST = 1) ZUBM ON 1=1 AND ZUBM.ZUCONO = A.ZUCONO AND ZUBM.ZUBRNO = A.ZUBRNO AND ZUBM.ZUUSNO = A.ZUUSNO AND ZUBM.ZUUSTY = A.ZUUSTY "
                        + " LEFT JOIN (SELECT ZUCONO, ZUBRNO, ZUUSNO, ZUUSNA, ZUUSTY, ZOCHNL, N2PRNO, N2CHLN FROM ZUSR JOIN ZUCM ON 1=1	AND ZOCONO = ZUCONO	AND ZOBRNO = ZUBRNO	AND ZOUSNO = ZUUSNO	JOIN PR02 ON 1=1 AND N2CONO = ZOCONO AND N2BRNO = ZOBRNO AND N2CHNL = ZOCHNL WHERE 1=1 AND ZURCST = 1 AND ZORCST = 1) ZUCM ON 1=1 AND ZUCM.ZUCONO = A.ZUCONO AND ZUCM.ZUBRNO = A.ZUBRNO	AND ZUCM.ZUUSNO = A.ZUUSNO AND ZUCM.ZUUSTY = A.ZUUSTY JOIN SUPR B ON 1=1 AND B.CSCONO = A.ZUCONO AND B.CSBRNO = A.ZUBRNO AND B.CSSPIC = A.ZUUSTY WHERE 1=1 AND ZURCST = 1 "
                        + " ) Q "
                        + " WHERE 1=1"
                        + "";

            if (!string.IsNullOrEmpty(obj.ZUCONO))
                strSql += " AND ZUCONO = '" + obj.ZUCONO.Trim() + "' ";
            if (!string.IsNullOrEmpty(obj.ZUBRNO))
                strSql += " AND ZUBRNO = '" + obj.ZUBRNO.Trim() + "' ";
            if (!string.IsNullOrEmpty(obj.ZCCONA))
                strSql += "AND CSSPNO = '" + obj.ZCCONA.Trim() + "' ";
            if (!string.IsNullOrEmpty(obj.ZUUSTY))
                strSql += "AND ZUUSTY = '" + obj.ZUUSTY.Trim() + "' ";
            if (!string.IsNullOrEmpty(obj.ZBBRNA))
                strSql += "AND (N2PRNO = '" + obj.ZBBRNA.Trim() + "' OR N2PRNO = '') ";

            strSql += " AND (N2CHLN = 1 OR N2CHLN = 0) ";

            List<ZUSRDto> dto = ExecuteQuery(strSql);
            return dto;
        }

        public List<ZUSRDto> GetListDataPIC(ZUSRDto obj)
        {
            string strSql = string.Empty;
            if (!string.IsNullOrEmpty(obj.ZRVANA))
            {
                if (obj.ZRVANA != "ACC")
                {
                    strSql = "SELECT DISTINCT  ZUUSNO "
                        + ", ZUUSNA "
                        + " FROM ( "
                        + " SELECT A.ZUUSNO, A.ZUUSNA , A.ZUUSTY,B.CSSPNO, ZUACNO, ZUVENO "
                        + ", CASE WHEN ZUBM.N2PRNO <> '' THEN ZUBM.N2PRNO WHEN ZUCM.N2PRNO <> '' THEN ZUCM.N2PRNO ELSE '' END N2PRNO,CASE WHEN ZUBM.N2CHLN <> 0 THEN ZUBM.N2CHLN WHEN ZUCM.N2CHLN <> 0 THEN ZUCM.N2CHLN ELSE 0 END N2CHLN "
                        + " FROM ZUSR A "
                        + " LEFT JOIN (SELECT ZUCONO, ZUBRNO, ZUUSNO, ZUUSNA, ZUUSTY, ZXIC02, N2PRNO, N2CHLN FROM ZUSR JOIN ZUBM ON 1=1	AND ZXCONO = ZUCONO	AND ZXBRNO = ZUBRNO	AND ZXUSNO = ZUUSNO	JOIN PR02 ON 1=1 AND N2CONO = ZXCONO AND N2BRNO = ZXBRNO AND N2IC02 = ZXIC02 WHERE 1=1 AND ZURCST = 1 AND ZXRCST = 1) ZUBM ON 1=1 AND ZUBM.ZUCONO = A.ZUCONO AND ZUBM.ZUBRNO = A.ZUBRNO AND ZUBM.ZUUSNO = A.ZUUSNO AND ZUBM.ZUUSTY = A.ZUUSTY "
                        + " LEFT JOIN (SELECT ZUCONO, ZUBRNO, ZUUSNO, ZUUSNA, ZUUSTY, ZOCHNL, N2PRNO, N2CHLN FROM ZUSR JOIN ZUCM ON 1=1	AND ZOCONO = ZUCONO	AND ZOBRNO = ZUBRNO	AND ZOUSNO = ZUUSNO	JOIN PR02 ON 1=1 AND N2CONO = ZOCONO AND N2BRNO = ZOBRNO AND N2CHNL = ZOCHNL WHERE 1=1 AND ZURCST = 1 AND ZORCST = 1) ZUCM ON 1=1 AND ZUCM.ZUCONO = A.ZUCONO AND ZUCM.ZUBRNO = A.ZUBRNO	AND ZUCM.ZUUSNO = A.ZUUSNO AND ZUCM.ZUUSTY = A.ZUUSTY JOIN SUPR B ON 1=1 AND B.CSCONO = A.ZUCONO AND B.CSBRNO = A.ZUBRNO AND B.CSSPIC = A.ZUUSTY WHERE 1=1 AND ZURCST = 1 "
                        + " ) Q "
                        + " WHERE 1=1"
                        + "";

                    strSql += " AND (N2CHLN = 1 OR N2CHLN = 0) ";
                }
                else
                {
                    strSql = "SELECT DISTINCT  ZUUSNO "
                        + ", ZUUSNA "
                        + " , CKACNA "
                        + " FROM ( "
                        + " SELECT A.ZUUSNO, A.ZUUSNA , A.ZUUSTY,B.CSSPNO, ZUACNO, ZUVENO "
                        + " , CASE WHEN ZUBM.N2PRNO <> '' THEN ZUBM.N2PRNO WHEN ZUCM.N2PRNO <> '' THEN ZUCM.N2PRNO ELSE '' END N2PRNO,CASE WHEN ZUBM.N2CHLN <> 0 THEN ZUBM.N2CHLN WHEN ZUCM.N2CHLN <> 0 THEN ZUCM.N2CHLN ELSE 0 END N2CHLN "
                        + " FROM ZUSR A	 "
                        + " LEFT JOIN (SELECT ZUCONO, ZUBRNO, ZUUSNO, ZUUSNA, ZUUSTY, ZXIC02, N2PRNO, N2CHLN FROM ZUSR JOIN ZUBM ON 1=1	AND ZXCONO = ZUCONO	AND ZXBRNO = ZUBRNO	AND ZXUSNO = ZUUSNO	JOIN PR02 ON 1=1 AND N2CONO = ZXCONO AND N2BRNO = ZXBRNO AND N2IC02 = ZXIC02 WHERE 1=1 AND ZURCST = 1 AND ZXRCST = 1) ZUBM ON 1=1 AND ZUBM.ZUCONO = A.ZUCONO AND ZUBM.ZUBRNO = A.ZUBRNO AND ZUBM.ZUUSNO = A.ZUUSNO AND ZUBM.ZUUSTY = A.ZUUSTY "
                        + " LEFT JOIN (SELECT ZUCONO, ZUBRNO, ZUUSNO, ZUUSNA, ZUUSTY, ZOCHNL, N2PRNO, N2CHLN FROM ZUSR JOIN ZUCM ON 1=1	AND ZOCONO = ZUCONO	AND ZOBRNO = ZUBRNO	AND ZOUSNO = ZUUSNO	JOIN PR02 ON 1=1 AND N2CONO = ZOCONO AND N2BRNO = ZOBRNO AND N2CHNL = ZOCHNL WHERE 1=1 AND ZURCST = 1 AND ZORCST = 1) ZUCM ON 1=1 AND ZUCM.ZUCONO = A.ZUCONO AND ZUCM.ZUBRNO = A.ZUBRNO	AND ZUCM.ZUUSNO = A.ZUUSNO AND ZUCM.ZUUSTY = A.ZUUSTY JOIN SUPR B ON 1=1 AND B.CSCONO = A.ZUCONO AND B.CSBRNO = A.ZUBRNO AND B.CSSPIC = A.ZUUSTY WHERE 1=1 AND ZURCST = 1 "
                        + " ) Q "
                        + " JOIN SACM ON 1=1 "
                        + " AND CKCONO = '' "
                        + " AND CKBRNO = '' "
                        + " AND CKACNO = ZUACNO "
                        + " WHERE 1=1"
                        + "";

                    strSql += "AND ZUUSTY = 'ACC' AND (N2CHLN = 1 OR N2CHLN = 0) ";
                }
            }

            if (!string.IsNullOrEmpty(obj.ZUCONO))
                strSql += " AND ZUCONO = '" + obj.ZUCONO.Trim() + "' ";
            if (!string.IsNullOrEmpty(obj.ZUBRNO))
                strSql += " AND ZUBRNO = '" + obj.ZUBRNO.Trim() + "' ";
            if (!string.IsNullOrEmpty(obj.ZCCONA))
                strSql += "AND CSSPNO = '" + obj.ZCCONA.Trim() + "' ";
            if (!string.IsNullOrEmpty(obj.ZUUSTY))
                strSql += "AND ZUUSTY = '" + obj.ZUUSTY.Trim() + "' ";
            if (!string.IsNullOrEmpty(obj.ZBBRNA))
                strSql += "AND (N2PRNO = '" + obj.ZBBRNA.Trim() + "' OR N2PRNO = '') ";


            List<ZUSRDto> dto = ExecuteQuery(strSql);
            return dto;
        }

        #endregion
    }
}