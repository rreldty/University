import 'dart:convert';

import 'package:university/Views/Xample/XM010A_Application.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:session_storage/session_storage.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../Common/AppConfig.dart';
import '../../Common/CommonMethod.dart';
import '../../Common/Encryption.dart';
import '../../Common/PageBase.dart';
import '../../Dao/Base/TokenHelper.dart';
import '../../Dao/Zystem/ZAUTDao.dart';
import '../../Dao/Zystem/ZDICDao.dart';
import '../../Dao/Zystem/ZUSRDao.dart';
import '../../Dto/Base/GlobalDto.dart';
import '../../Dto/Zystem/ZAUTDto.dart';
import '../../Dto/Zystem/ZDICDto.dart';
import '../../Dto/Zystem/ZUSRDto.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/ModalProgressExtender.dart';

import '../Training/Fakultas.dart';
import 'MainApp.dart';
import 'MainHome.dart';

class Login extends StatefulWidget {
  static const String route = "/Controls/Login";

  @override
  createState() => LoginState();
}

class LoginState extends PageBase<Login> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  bool isPasswordHidden = true;
  final edtUSNO = EditTextController();
  final edtPSWD = EditTextController();
  String tvwVersion = "";

  //endregion

  //region Init
  @override
  void appInit(PagePlatform pagePlatform) async {
    tvwVersion = await CommonMethod.getVersionName();

    // String strUrlParam = "";
    // for(int n = 0; n < args.keys.length;n++){
    //   if(strUrlParam.isNotEmpty){
    //     strUrlParam += "&";
    //   }
    //   strUrlParam += "${args.keys.elementAt(n)}=${args[args.keys.elementAt(n)]}";
    // }
    // debugPrint("Login [appInit]: $strUrlParam");
  }

  //endregion

  //region Events  (otak logikanya)
  Future btnLogin_Click() async {
    if (form1.currentState!.validate()) {
      String strResult = "";

      navigateGo(Fakultas.route);
      // navigateTo(MataKuliah.route);

      // showModalProgress.value = true;
      //
      // if (strResult.isEmpty) {
      //   try {
      //     ZUSRDao dao = ZUSRDao();
      //     ZUSRDto obj = await dao.Login(collectionInfo());
      //
      //     GlobalDto.CONO = obj.ZUCONO;
      //     // GlobalDto.CONA = obj.ZCCONA;
      //     GlobalDto.BRNO = obj.ZUBRNO;
      //     // GlobalDto.BRNA = obj.ZBBRNA;
      //     GlobalDto.USNO = obj.ZUUSNO;
      //     GlobalDto.USNA = obj.ZUUSNA;
      //     GlobalDto.USTY = obj.ZUUSTY;
      //
      //     SharedPreferences prefs = await SharedPreferences.getInstance();
      //
      //     if (args.containsKey("rl")) {
      //       if (args["rl"] != null && args["rl"]!.isNotEmpty) {
      //         await prefs.setString("RedirectLanding", args["rl"] ?? "");
      //       }
      //     }
      //
      //     await prefs.setString("CONO", GlobalDto.CONO);
      //     //await prefs.setString("CONA", GlobalDto.CONA);
      //     await prefs.setString("BRNO", GlobalDto.BRNO);
      //     //await prefs.setString("BRNA", GlobalDto.BRNA);
      //     await prefs.setString("USNO", GlobalDto.USNO);
      //     await prefs.setString("USNA", GlobalDto.USNA);
      //     await prefs.setString("USTY", GlobalDto.USTY);
      //
      //     SessionStorage session = SessionStorage();
      //     session["LOGIN_STAMP"] =
      //         DateTime.now().millisecondsSinceEpoch.toString();
      //
      //     TokenHelper.setUserApi(edtUSNO.text, edtPSWD.text);
      //   } catch (e) {
      //     strResult = e.toString();
      //   }
      // }
      //
      // if (strResult.isEmpty) {
      //   List<ZAUTDto> lstAppAuth = [];
      //
      //   try {
      //     //Get List Application
      //     ZAUTDao daoZT = ZAUTDao();
      //     lstAppAuth = await daoZT.listApplication(ZAUTDto(
      //       ZHUSNO: GlobalDto.USNO,
      //     ));
      //
      //     SharedPreferences prefs = await SharedPreferences.getInstance();
      //     String strLstAppAuthority = lstAppAuth.isNotEmpty
      //         ? json.encode(lstAppAuth.map((e) => e.toMap()).toList())
      //         : "";
      //
      //     await prefs.setString("lstAppAuthority", strLstAppAuthority);
      //     GlobalDto.listAppAuthority = strLstAppAuthority;
      //   } catch (e) {
      //     strResult = e.toString();
      //   }
      //
      //   if (lstAppAuth.length == 1) {
      //     try {
      //       //Get List Menu
      //       GlobalDto.APNO = lstAppAuth[0].ZTAPNO;
      //
      //       ZAUTDao daoZT = ZAUTDao();
      //       List<ZAUTDto> lstZT = await daoZT.ListMenu(ZAUTDto(
      //         ZHUSNO: GlobalDto.USNO,
      //         ZTAPNO: GlobalDto.APNO,
      //       ));
      //
      //       String strLstAuthority = lstZT.isNotEmpty
      //           ? json.encode(lstZT.map((e) => e.toMap()).toList())
      //           : "";
      //       GlobalDto.listAuthority = strLstAuthority;
      //
      //       SharedPreferences prefs = await SharedPreferences.getInstance();
      //       await prefs.setString("APNO", GlobalDto.APNO);
      //       await prefs.setString("lstAuthority", GlobalDto.listAuthority);
      //
      //       if (args.containsKey("apno")) {
      //         if (args["apno"] != null && args["apno"]!.isNotEmpty) {
      //           GlobalDto.APNO = args["apno"] ?? "";
      //
      //           SharedPreferences prefs = await SharedPreferences.getInstance();
      //           await prefs.setString("APNO", GlobalDto.APNO);
      //         }
      //       }
      //     } catch (e) {
      //       strResult = e.toString();
      //     }
      //   }
      // }
      //
      // if (strResult.isEmpty) {
      //   try {
      //     //Get List Dictionary
      //     ZDICDao daoZD = ZDICDao();
      //     List<ZDICDto> lstZD = await daoZD.ListDictionary(ZDICDto(
      //       ZDCONO: "",
      //       ZDBRNO: "",
      //       ZDLGNO: AppConfig.Language,
      //     ));
      //
      //     SharedPreferences prefs = await SharedPreferences.getInstance();
      //     String strLstDictionary = lstZD.isNotEmpty
      //         ? json.encode(lstZD.map((e) => e.toMap()).toList())
      //         : "";
      //     await prefs.setString("lstDictionary", strLstDictionary);
      //     GlobalDto.listDictionary = strLstDictionary;
      //   } catch (e) {
      //     strResult = "[Dictionary Error] ${e.toString()}";
      //   }
      // }
      //
      // await Encryption.generateRandomKey();
      //
      // showModalProgress.value = false;

      // if (strResult.isEmpty) {
      //   // if(GlobalDto.APNO.isNotEmpty){
      //   //   navigateGo(MainHome.route);
      //   // }
      //   // else{
      //   //   navigateGo(MainApp.route);
      //   // }
      // } else {
      //   await MessageBox.show(
      //       context: context,
      //       message: strResult,
      //       title: "Login",
      //       dialogButton: DialogButton.OK);
      // }
    }
  }

  ZUSRDto collectionInfo() {
    var objInfo = ZUSRDto();
    objInfo.ZUUSNO = edtUSNO.text;
    objInfo.ZUPSWD = Encryption.simpleEncrypt(edtPSWD.text);
    return objInfo;
  }

  //endregion

//region Layout
  @override //yang diubah
  Widget build(BuildContext context) {
    return Scaffold(
      body: ModalProgressExtender(
        inAsyncCall: showModalProgress,
        child: Form(
          key: form1,
          child: Column(
              children: [
            Stack(
              children: [
                // Background Image
                Container(
                  width: MediaQuery.of(context).size.width,
                  height: MediaQuery.of(context).size.height,
                  child: Image.asset(
                    "assets/images/tacobg.png",
                    fit: BoxFit.fill,
                  ),
                ),

                // Main Container
                Align(
                  alignment: Alignment.topRight,
                  child: Container(
                    width: 419,
                    height: 517,
                    margin: EdgeInsets.only(right: 85, top: 0),
                    child: Column(
                      children: [
                        Stack(
                          children: [
                            // Background Color
                            Container(
                              width: double.infinity,
                              height: 209,
                              color: Color(0xFF262625),
                            ),
                            // Logo Image
                            Positioned(
                              bottom: 15,
                              child: Padding(
                                padding: EdgeInsets.only(top: 105),
                                child: Image.asset(
                                  width: 200,
                                  height: 90,
                                  "assets/images/tacoPortalLogo.png",
                                  fit: BoxFit.contain,
                                  // height: 90,
                                ),
                              ),
                            ),
                            // Text on Top of Logo
                          ],
                        ),

                        // Login Form
                        Expanded(
                          child: Container(
                            padding: EdgeInsets.symmetric(
                                horizontal: 20, vertical: 20),
                            height: 308,
                            width: 419,
                            color: Color.fromRGBO(217, 217, 217, 0.8),
                            child:
                            Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: <Widget>[
                                Expanded(
                                  flex: 2,
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                        horizontal: 20, vertical: 2),
                                    width: 343, // Menyamakan lebar dengan EditText
                                    alignment: Alignment.centerLeft, // Menempatkan teks di kiri, sejajar dengan EditText
                                    child: Text(
                                      'Please login',
                                      style: TextStyle(
                                        fontSize: 15,
                                        fontWeight: FontWeight.bold,
                                        color: Color(0xFF262625),
                                        fontFamily: 'Inter',
                                      ),
                                    ),
                                  ),
                                ),
                                Expanded(
                                  flex: 5,
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                        horizontal: 20, vertical: 10),
                                    child: EditText(
                                      controller: edtUSNO,
                                      prefixIcon: const Icon(Icons.person),
                                      textMode: TextInputType.text,
                                      hintText: "User ID",
                                      contentPadding: const EdgeInsets.fromLTRB(
                                          10, 20, 15, 15),
                                      isMandatory: true,
                                      textInputAction: TextInputAction.next,
                                      height: 48,
                                      width: 343,
                                    ),
                                  ),
                                ),

                                Expanded(
                                  flex: 5,
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(
                                        horizontal: 20, vertical: 1),
                                    child: EditText(
                                      controller: edtPSWD,
                                      prefixIcon: const Icon(Icons.lock),
                                      textMode: TextInputType.text,
                                      contentPadding: const EdgeInsets.fromLTRB(
                                          10, 20, 15, 15),
                                      hintText: "Password",

                                      isMandatory: true,
                                      isPassword: true,
                                      textInputAction: TextInputAction.done,
                                      height: 48,
                                      width: 344,
                                      onLostFocus: () {
                                        btnLogin_Click();
                                      },
                                    ),
                                  ),
                                ),
                                Expanded(
                                  flex: 3,
                                  child: Container(
                                    alignment: Alignment.topRight,
                                    margin: const EdgeInsets.only( right: 15),
                                    child: SizedBox(
                                      height: 40, // Tinggi tombol
                                      width: 343, // Lebar tombol
                                      child: ElevatedButton(
                                        onPressed: btnLogin_Click,
                                        style: ButtonStyle(
                                          backgroundColor: MaterialStateProperty.resolveWith<Color>((states) {
                                            if (states.contains(MaterialState.pressed)) {
                                              return Colors.white;
                                            }
                                            return Color(0xff824C31);
                                          }),
                                          foregroundColor: MaterialStateProperty.resolveWith<Color>((states) {
                                            if (states.contains(MaterialState.pressed)) {
                                              return Color(0xff824C31); // Warna teks saat tombol ditekan
                                            }
                                            return Colors.white; // Warna teks saat tombol tidak ditekan
                                          }),
                                          shape: MaterialStateProperty.all<RoundedRectangleBorder>(
                                            RoundedRectangleBorder(
                                              borderRadius: BorderRadius.circular(5.0),
                                            ),
                                          ),
                                        ),
                                        child: Text(
                                          'LOGIN',
                                          style: TextStyle(
                                            fontSize: 18,
                                            fontWeight: FontWeight.bold,
                                          ),
                                        ),
                                      ),
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              ],
            )
          ]),
        ),
      ),
    );
  }

  @override
  void pageBehaviour(PageMode pageMode) {
    // TODO: implement pageBehaviour
  }

//endregion
}
