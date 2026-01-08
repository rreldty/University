import 'dart:convert';

import 'package:university/UserControls/IFrameExtender.dart';
import 'package:flutter/services.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import 'package:pointer_interceptor/pointer_interceptor.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:flutter/material.dart';
import '../../Common/AppConfig.dart';
import '../../Common/CommonMethod.dart';
import '../../Common/Encryption.dart';
import '../../Common/GlobalStyle.dart';
import '../../Common/PageBase.dart';
import '../../Dto/Base/GlobalDto.dart';
import '../../Dto/Base/MenuNodeDto.dart';
import '../../Dto/Base/ModalPopupResult.dart';
import '../../Dto/Zystem/ZAUTDto.dart';
import '../../Dto/Zystem/ZUSRDto.dart';
import '../../UserControls/AccordionMenu.dart';

import '../../Dao/Zystem/ZLUMDao.dart';
import '../../Dto/Zystem/ZLUMDto.dart';
import '../../UserControls/Lookup.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/ModalDialog.dart';
import '../../UserControls/NeoTreeMenu.dart';
import 'BlankPage.dart';
import 'ChangePassword.dart';
import 'Login.dart';
import 'MainApp.dart';
// import 'WebViewPage.dart';

class MainHome extends StatefulWidget {
  static const String route = "/";
  final StatefulNavigationShell navigationShell;

  const MainHome({
    super.key,
    required this.navigationShell,
  });

  @override
  createState() => MainHomeState();
}

class MainHomeState extends PageBase<MainHome> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  String TotalNotif = "";
  String strRouteMenu = "";
  bool visibilytMenu = false;

  List<MenuNodeDto> lstMenuNode = [];

  String strHeaderInfo1 = "";
  String strHeaderInfo2 = "";
  String strHeaderInfo3 = "";
  OverlayPortalController _overlayPortalController = OverlayPortalController();
  //endregion

  //region Init
  @override
  void initState() {
    debugPrint("MainHome: initState");

    strRouteMenu = CommonMethod.resolveUrl(BlankPage.route);

    if (GlobalDto.listAuthority.isNotEmpty) {
      Iterable list = json.decode(GlobalDto.listAuthority);
      List<ZAUTDto> lstZT = list.map((e) => ZAUTDto.fromJson(e)).toList();

      lstMenuNode = generateNodes(lstZT);
    }

    print(lstMenuNode);

    super.initState();

    initImages();
  }

  @override
  void appInit(PagePlatform pagePlatform) async {
    // strHeaderInfo1 = "${GlobalDto.USNA} | ${CommonMethod.DateToStringFormat(DateTime.now())}";
    strHeaderInfo1 = GlobalDto.USNA;
    strHeaderInfo2 = GlobalDto.CONA;
    strHeaderInfo3 = DateFormat.yMMMMd().format(
        DateTime.now()); //"${CommonMethod.DateToStringFormat(DateTime.now())}";

    SharedPreferences prefs = await SharedPreferences.getInstance();

    // if(prefs.containsKey("RedirectLanding")){
    //   String strRedirectLanding = prefs.getString("RedirectLanding") ?? "";
    //   if(strRedirectLanding.isNotEmpty){
    //     HtmlElement elem = document.getElementById("frmContent") as HtmlElement;
    //     strRouteMenu = CommonMethod.resolveUrl(strRedirectLanding);
    //   }
    //   prefs.remove("RedirectLanding");
    // }
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

  void btnLogout_Click() async {
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

  //
  // void btnLogout_Click() async {
  //   GlobalDto.CONO = "";
  //   GlobalDto.BRNO = "";
  //   GlobalDto.USNO = "";
  //   GlobalDto.USNA = "";
  //   GlobalDto.USTY = "";
  //
  //   SharedPreferences prefs = await SharedPreferences.getInstance();
  //   await prefs.setString("USNO", "");
  //   await prefs.setString("LOGIN_TIMESTAMP", "");
  //
  //   setState(() {
  //     strRouteMenu = CommonMethod.resolveUrl(BlankPage.route);
  //   });
  //
  //   context.go(Login.route);
  // }

  void btnLogo_Click() async {
    setState(() {
      strRouteMenu = CommonMethod.resolveUrl(BlankPage.route);
    });

    navigateGo(MainApp.route);
    // appInit();
  }

  //endregion

  //region Methods

  List<MenuNodeDto> generateNodes(List<ZAUTDto> lstZT) {
    List<MenuNodeDto> nodes = [];

    List<ZAUTDto> lstKey = lstZT.where((el) => el.ZMMEPA == "").toList();
    if (lstKey.isNotEmpty) {
      for (int n = 0; n < lstKey.length; n++) {
        ZAUTDto objKey = lstKey[n];

        MenuNodeDto node = MenuNodeDto(
            key: objKey.ZTMENO,
            label: objKey.ZMMENA,
            data: objKey,
            expanded: false);

        //search children
        List<MenuNodeDto> lstChildren =
            generateNodeChildren(lstZT, parentNode: node);
        node.children = lstChildren;

        nodes.add(node);
      }
    }

    return nodes;
  }

  List<MenuNodeDto> generateNodeChildren(List<ZAUTDto> lstZT,
      {MenuNodeDto? parentNode}) {
    String parentKey = "";

    if (parentNode != null) {
      parentKey = parentNode.key;
    }

    List<MenuNodeDto> lstParent = [];

    List<ZAUTDto> lstKey = lstZT.where((el) => el.ZMMEPA == parentKey).toList();
    if (lstKey.isNotEmpty) {
      for (int n = 0; n < lstKey.length; n++) {
        ZAUTDto objKey = lstKey[n];

        MenuNodeDto node = MenuNodeDto(
            key: objKey.ZTMENO,
            label: objKey.ZMMENA,
            data: objKey,
            expanded: false);

        //search children
        List<MenuNodeDto> lstChildren =
            generateNodeChildren(lstZT, parentNode: node);
        node.children = lstChildren;

        lstParent.add(node);
      }
    }

    return lstParent;
  }

  Stream<int> generateStream = (() async* {
    await Future<void>.delayed(const Duration(seconds: 2));
    yield 1;
  })();

  @override
  void pageBehaviour(pageMode) {
    // TODO: implement pageBehaviour
  }
  //endregion

  //region Layout
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        resizeToAvoidBottomInset: false,
        backgroundColor: Colors.white,
        appBar: PreferredSize(
            preferredSize:
                const Size.fromHeight(60.0), // here the desired height
            child: AppBar(
              leading: Container(),
              backgroundColor: Color(0xff262625), //GlobalStyle.primaryColor,
              flexibleSpace: Center(
                child: Row(
                  children: <Widget>[
                    Container(
                      margin: const EdgeInsets.only(left: 3),
                      child: TextButton(
                        style: TextButton.styleFrom(
                          padding: const EdgeInsets.all(0),
                          minimumSize: const Size(60,60),
                          alignment: Alignment.centerRight,
                        ),
                        onPressed: () {
                          setState(() {
                            visibilytMenu = !visibilytMenu;
                          });
                        },
                        child: Image.asset(
                          "assets/icons/icon_menu_taco.png",
                          width: 40,
                          height: 40,
                        ),
                      ),
                    ),
                    Container(
                      padding: const EdgeInsets.only(left: 1, right: 1),
                      child: TextButton(
                        style: TextButton.styleFrom(
                          padding: const EdgeInsets.all(0),
                          alignment: Alignment.centerLeft,
                        ),
                        onPressed: btnLogo_Click,
                        child: Image.asset(
                          "assets/images/tacoPortalLogo.png",
                        ),
                      ),
                    ),
                    Text(
                      strHeaderInfo2,
                      style: const TextStyle(fontSize: 14),
                    ),
                    const Spacer(),
                    Container(
                      padding: const EdgeInsets.only(right: 20),
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.end,
                        children: [
                          Text(
                            strHeaderInfo1,
                            style: TextStyle(
                                fontSize: 14,
                                color: GlobalStyle.headerColor,
                                fontFamily: GlobalStyle.fontFamily,
                                fontWeight: FontWeight.bold),
                          ),
                          Text(
                            strHeaderInfo3,
                            style: TextStyle(
                                fontSize: 12,
                                color: GlobalStyle.headerColor,
                                fontFamily: GlobalStyle.fontFamily,
                                fontWeight: FontWeight.bold),
                          ),
                        ],
                      ),
                    ),
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
                    const VerticalDivider(color: Colors.grey,),
                    //GlobalStyle.toolbarShowTitleDivider ? const VerticalDivider(color: Colors.grey,) : Container(),
                    Container(
                      margin: EdgeInsets.only(right: 10,left: 10),
                      // decoration: BoxDecoration(
                      //   border: Border(left: BorderSide(width: 1, color: Color(0xFFe5eaee))),
                      // ),
                      child: TextButton(
                        style: TextButton.styleFrom(
                          padding: EdgeInsets.zero,
                          minimumSize: Size(40, 40),
                          alignment: Alignment.center,
                        ),
                        onPressed: btnLogout_Click,
                        child: Image.asset(
                          "assets/icons/logout.png",
                        ),
                      ),
                    ),

                  ],
                ),
              ),
            )),
        body: Row(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisSize: MainAxisSize.max,
          children: [
            NeoTreeMenu(
              width: 280,
              height: MediaQuery.of(context).size.height-60,
              showMenu: visibilytMenu,
              menuNodes: lstMenuNode,
              showMinimizeIcon: false,
              showMenuBar: false,
              menuHeaderColor: GlobalStyle.primaryColor,
              menuItemColor: GlobalStyle.primaryColor,
              menuOnClik: (e) {
                setState(() {
                  visibilytMenu = false;
                  ZAUTDto objZT = e;

                  try {
                    //Save User Menu Log
                    ZLUMDto objZN = ZLUMDto();
                    objZN.ZNCONO = GlobalDto.CONO;
                    objZN.ZNBRNO = GlobalDto.BRNO;
                    objZN.ZNAPNO = GlobalDto.APNO;
                    objZN.ZNMENO = objZT.ZTMENO;
                    objZN.ZNUSNO = GlobalDto.USNO;
                    objZN.ZNREMA = "AGLiS Portal";

                    ZLUMDao daoZN = ZLUMDao();
                    daoZN.Save(objZN).then((value) {
                      if (value.isNotEmpty) {
                        debugPrint("MainHome-UserMenuLog Result: $value");
                      }
                    });
                  } catch (ex) {
                    debugPrint(ex.toString());
                  }

                  String strPURL = objZT.ZPPURL.replaceAll(".xaml", "");
                  strPURL = strPURL.replaceAll(".aspx", "");

                  // debugPrint("go to $strPURL");

                  if (strPURL.isEmpty) {
                    strPURL = "Controls/ErrorPage?m=Not Found";
                  }

                  if (objZT.ZMPARM.toLowerCase().contains("ret=bi")) {
                    // strPURL =
                    // "${WebViewPage.route}?prid=${objZT.ZTMENO}&mt=${objZT.ZMMENA}";
                  } else {
                    strPURL = "$strPURL${strPURL.contains("?") ? "&" : "?"}prid=${objZT.ZTMENO}&mt=${objZT.ZMMENA}";
                  }

                  String strParam = objZT.ZMPARM;

                  if (strParam.isNotEmpty) {
                    strPURL +=
                    "&${strParam.replaceAll("#USNO", GlobalDto.USNO)}";
                  }

                  strRouteMenu = Uri.encodeFull("/$strPURL");
                  // debugPrint("menu click $strRouteMenu");
                  navigateTo(strRouteMenu);
                });
              },
            ),
            Expanded(
                child: SizedBox(
                  width: double.infinity,
                  height: double.infinity,
                  child: widget.navigationShell
                )),
          ],
        ));
  }
//endregion
}
