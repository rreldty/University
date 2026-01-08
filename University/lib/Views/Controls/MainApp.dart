import 'dart:convert';

import 'package:adaptive_scrollbar/adaptive_scrollbar.dart';
import 'package:university/UserControls/CustomToolbar.dart';
import 'package:university/UserControls/PageContent.dart';
import 'package:flutter_widget_from_html/flutter_widget_from_html.dart';

import '../../Dto/Base/ModalPopupResult.dart';
import '../../Dto/Zystem/ZUSRDto.dart';
import '../../UserControls/ComboBox.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/ModalDialog.dart';
import '../../UserControls/ToolbarBox.dart';
import '../../Views/Controls/MainHome.dart';

import '../../Common/AppRoute.dart';
import '../../Dao/Zystem/ZAUTDao.dart';
import 'package:flutter/services.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import 'package:pointer_interceptor/pointer_interceptor.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:flutter/material.dart';
import '../../Common/AppConfig.dart';
import '../../Common/CommonMethod.dart';
import '../../Common/GlobalStyle.dart';
import '../../Common/PageBase.dart';
import '../../Dto/Base/GlobalDto.dart';
import '../../Dto/Base/MenuNodeDto.dart';
import '../../Dto/Zystem/ZAUTDto.dart';
import '../../UserControls/AccordionMenu.dart';

import '../../UserControls/MessageBox.dart';
import '../../UserControls/ModalProgressExtender.dart';
import '../../UserControls/NeoTreeMenu.dart';
import '../Controls/ChangePassword.dart';
import 'BlankPage.dart';
import 'Login.dart';

// @JS('updateAppCode')
// external set updateAppCode(void Function(String value) f);

class MainApp extends StatefulWidget {
  static const String route = "/MainApp";

  const MainApp({super.key});

  @override
  createState() => MainAppState();
}

class MainAppState extends PageBase<MainApp> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier(false);
  final ScrollController _scrollControllerHorizontal = ScrollController();

  final cbxBranch = ComboBoxController();
  final edtCurrentPassword = EditTextController();
  final edtNewPassword = EditTextController();
  final edtNewPasswordConfirm = EditTextController();

  String strUserName = "-";
  String strWarehouseDate = "-";
  String strLastSyncDate = "-";

  List<ZAUTDto> lstAppAuth = [];

  //endregion

  //region Init
  @override
  void initState() {
    debugPrint("MainApp: initState");

    // updateAppCode = allowInterop(updateAppCodeHandler);

    super.initState();
  }

  @override
  void appInit(PagePlatform pagePlatform) async {
    await initImages();

    if (GlobalDto.listAppAuthority.isNotEmpty) {
      Iterable list = json.decode(GlobalDto.listAppAuthority);
      setState(() {
        lstAppAuth = list.map((e) => ZAUTDto.fromJson(e)).toList();
      });
    }

    setState(() {
      strUserName = GlobalDto.USNA;
    });
  }

  Future initImages() async {
    final Map<String, dynamic> assets =
        jsonDecode(await rootBundle.loadString('AssetManifest.json'));

    List<String> lstImagePath = [];
    lstImagePath = assets.keys
        .where((String key) => key.startsWith("assets/icons/"))
        .toList();

    try {
      for (int n = 0; n < lstImagePath.length; n++) {
        String strImagePath = lstImagePath[n];
        await precacheImage(AssetImage(strImagePath), context);
      }

      debugPrint('Image loaded and cached successfully!');
    } catch (e) {
      debugPrint('Failed to load and cache the image: $e');
    }
  }

  //endregion

  //region Events

  void btnSetting_Click() async {
    ModalPopupResult? popupResult = await ModalDialog.show(
        context: context,
        title: "Change Password",
        modalWidth: 550,
        modalHeight: 270,
        child: ChangePassword(
        ));

    if (popupResult!.dialogResult == DialogResult.OK) {}
  }

  void btnLogoutClick() async {
    GlobalDto.APNO = "";
    GlobalDto.CONO = "";
    GlobalDto.CONA = "";
    GlobalDto.BRNO = "";
    GlobalDto.BRNA = "";
    GlobalDto.USNO = "";
    GlobalDto.USNA = "";
    GlobalDto.USTY = "";
    GlobalDto.Version = "";

    GlobalDto.listAppAuthority = "";
    GlobalDto.listAuthority = "";
    GlobalDto.listDictionary = "";

    SharedPreferences prefs = await SharedPreferences.getInstance();
    // await prefs.setString("USNO", "");
    // await prefs.setString("LOGIN_TIMESTAMP", "");
    prefs.clear();

    navigateGo(Login.route);
  }

  void btnLogoClick() async {
    navigateGo(MainApp.route);
  }

  void btnSelectApp(ZAUTDto obj) async {
    String strResult = "";

    showModalProgress = ValueNotifier(true);

    try {
      //Get List Menu

      debugPrint(obj.ZAAURL);

      GlobalDto.APNO = obj.ZTAPNO;

      ZAUTDao daoZT = ZAUTDao();
      List<ZAUTDto> lstZT = await daoZT.ListMenu(ZAUTDto(
        ZHUSNO: GlobalDto.USNO,
        ZTAPNO: GlobalDto.APNO,
      ));

      String strLstAuthority = lstZT.isNotEmpty
          ? json.encode(lstZT.map((e) => e.toMap()).toList())
          : "";
      GlobalDto.listAuthority = strLstAuthority;

      debugPrint("[SelectApp] ${GlobalDto.APNO}");

      SharedPreferences prefs = await SharedPreferences.getInstance();
      await prefs.setString("APNO", GlobalDto.APNO);
      await prefs.setString("lstAuthority", GlobalDto.listAuthority);
    } catch (ex) {
      strResult = ex.toString();
    }

    showModalProgress = ValueNotifier(false);

    if (strResult.isEmpty) {
      if(obj.ZAAURL.contains("http")){
        CommonMethod.launchURL(obj.ZAAURL, isNewTab: true);
      }
      else{
        navigateGo(MainHome.route);
      }
    } else {
      await MessageBox.show(
          context: context,
          message: strResult,
          title: "Applications",
          dialogButton: DialogButton.OK);
    }
  }

  //endregion

  //region Methods

  @override
  void pageBehaviour(pageMode) {
    // TODO: implement pageBehaviour
  }

  //endregion

  //region Layout
  @override
  Widget build(BuildContext context) {
    return PageContent(
      formKey: form1,
      showModalProgress: showModalProgress,
      paddingLeft: 0,
      paddingTop: 0,
      toolbar: CustomToolbar(
        backgroundColor: Color(0xff262625),
        padding: EdgeInsets.fromLTRB(10, 0, 10, 0),
        height: 300,
        children: [
          Expanded(
            child: Column(
              mainAxisSize: MainAxisSize.max,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  mainAxisSize: MainAxisSize.max,
                  children: [
                    Container(
                      height: 50,
                      padding: const EdgeInsets.only(right: 2),
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.end,
                        children: [
                          Container(
                            margin: EdgeInsets.only(right: 3),
                            decoration: BoxDecoration(
                              border: Border(left: BorderSide(width: 1, color: Colors.transparent)),
                            ),
                            child: TextButton(
                              style: TextButton.styleFrom(
                                padding: EdgeInsets.zero,
                                minimumSize: Size(40, 40),
                                alignment: Alignment.center,
                              ),
                              onPressed: btnSetting_Click,
                              child: Image.asset(
                                "assets/icons/setting.png",
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                    Container(
                      width: 50,
                      margin: const EdgeInsets.only(right: 3),
                      child: TextButton(
                        style: TextButton.styleFrom(
                          padding: const EdgeInsets.all(0),
                          minimumSize: const Size(60, 60),
                          alignment: Alignment.center,
                        ),
                        onPressed: btnLogoutClick,
                        child: Image.asset(
                          "assets/icons/logout.png",
                        ),
                      ),
                    ),
                  ],
                ),
                Row(
                  // mainAxisAlignment: MainAxisAlignment.start,
                  // mainAxisSize: MainAxisSize.max,
                  children: [
                    Image.asset(
                      height: 88,
                      width: 191,
                      "assets/images/tacoPortalLogo.png",
                      fit: BoxFit.contain,
                    ),
                  ],
                ),
                Container(
                  padding: EdgeInsets.only(top: 15, bottom: 10),
                  child: Text(
                    strUserName,
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                Container(
                  padding: EdgeInsets.only(bottom: 10),
                  child: Text(
                    'Welcome to TACO AGLiS Portal',
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 18,
                    ),
                  ),
                ),
                Text(
                  'An application to view analytical report, query and analyze your data with your own favor from your browser, approving workflow and application to order TACO products online.',
                  style: TextStyle(
                    color: Colors.white,
                    fontSize: 14,
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
      builder: (context, constraints) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.start,
          children: <Widget>[
            Row(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisSize: MainAxisSize.max,
                children: builderApplications(3),
            ),
          ],
        );
      },
      builderPhone: (context, constraints) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.start,
          children: builderApplications(1),
        );
      },
    );
  }

  List<Widget> builderApplications(double dblMaxColumn) {
    // debugPrint(lstAppAuth.length.toString());
    List<Widget> lstApp = [];
    List<Widget> lstAppCol1 = [];
    List<Widget> lstAppCol2 = [];
    List<Widget> lstAppCol3 = [];

    for (int i = 0; i < lstAppAuth.length; i++) {
      if (i % dblMaxColumn == 0) {
        lstAppCol1.add(buildAppButton(lstAppAuth[i]));
      }
      if (i % dblMaxColumn == 1) {
        lstAppCol2.add(buildAppButton(lstAppAuth[i]));
      }
      if (i % dblMaxColumn == 2) {
        lstAppCol3.add(buildAppButton(lstAppAuth[i]));
      }
    }

    if(lstAppCol1.isNotEmpty) {
      lstApp.add(Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          children: lstAppCol1,
        ),
      ));
    }

    if(lstAppCol2.isNotEmpty) {
      lstApp.add(Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          children: lstAppCol2,
        ),
      ));
    }

    if(lstAppCol3.isNotEmpty) {
      lstApp.add(Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(children: lstAppCol3),
      ));
    }

    return lstApp;
  }

  Widget buildAppButton(ZAUTDto obj) {
    return Padding(
      padding: (lstAppAuth.length % 4 == 0)
          ? const EdgeInsets.all(8.0)
          : const EdgeInsets.fromLTRB(8.0, 58.0, 8.0, 8.0),
      child: InkWell(
        onTap: () {
          debugPrint("click app ${obj.ZTAPNO}");
          btnSelectApp(obj);
        },
        borderRadius: BorderRadius.circular(10),
        child: Container(
          height: 100.0,
          margin: const EdgeInsets.only(right: 5, left: 30),
          //alignment: Alignment.center,
          decoration: BoxDecoration(
            borderRadius:
            BorderRadius.circular(10), // Menambahkan border radius
          ),
          child: ClipRRect(
            borderRadius: BorderRadius.circular(10),
            // Radius yang sama untuk memotong gambar
            child: Image.asset(
              obj.ZAIURL,
            ),
          ),
        ),
      ),
    );
  }

//endregion
}
