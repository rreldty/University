import 'dart:convert';
import "package:universal_html/html.dart" as html;
import 'package:university/Common/Encryption.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/services.dart';
import 'package:go_router/go_router.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../Dao/Zystem/ZLUMDao.dart';
import '../Dto/Base/GlobalDto.dart';
import '../Dto/Base/GlobalEnum.dart';
import '../Dto/Zystem/ZLUMDto.dart';
import 'CommonMethod.dart';

enum PageMode { New, Add, Edit, View, Copy }
enum PagePlatform { landscape, portrait }

abstract class PageBase<T extends StatefulWidget> extends State<T>{
  late Map<String, dynamic> args;
  late Map<String, dynamic> argsEncrypted;

  @override
  void initState() {
    debugPrint("[PageBase] initState");
    super.initState();

    Future.delayed(Duration.zero, () async {
      args = {};
      argsEncrypted = {};

      SharedPreferences prefs = await SharedPreferences.getInstance();
      await prefs.setBool("isAppInitRun", true);

      if(ModalRoute.of(context)!.settings.arguments != null) {
        argsEncrypted = ModalRoute
            .of(context)!
            .settings
            .arguments as Map<String, dynamic>;

        debugPrint("[PageBase-EncryptedArgs] ${argsEncrypted.toString()}");

        if(argsEncrypted.isNotEmpty){
          String strParam = "";

          if(argsEncrypted.containsKey("xe")){
            strParam = await Encryption.symmetricDecrypt(argsEncrypted["xe"]);

            if(strParam.contains("&")){
              List<String> q2 = strParam.split("&");

              for(int n = 0; n < q2.length; n++){
                List<String> q3 = q2[n].split("=");
                if(q3.length > 1) {
                  args[q3[0]] = q3[1];
                }
              }
            }
          }
          else{
            args.addAll(argsEncrypted);
          }
        }
      }

      await initImages();
      await baseInit();

      double screenWidth = MediaQuery.sizeOf(context).width;
      double screenHeight = MediaQuery.sizeOf(context).height;

      PagePlatform pagePlatform = PagePlatform.landscape;

      if(screenWidth < 600){
        pagePlatform = PagePlatform.portrait;
      }

      appInit(pagePlatform);

      await Future.delayed(const Duration(milliseconds: 5));
      await prefs.setBool("isAppInitRun", false);
      // debugPrint("AppInit Finish");
    });

    // WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
    //
    //
    //
    //
    //   await Future.delayed(const Duration(milliseconds: 10));
    //
    //
    // });
  }

  @override
  void didChangeDependencies() {
    debugPrint("[PageBase] didChangeDependencies");

    super.didChangeDependencies();

    // WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
    //   await Future.delayed(const Duration(milliseconds: 30));
    //   args = {};
    //
    //   // String strRoute = Uri.decodeFull(Uri.base.toString());
    //   // args = CommonMethod.getUrlParameter(strRoute);
    //
    //   if(ModalRoute.of(context)!.settings.arguments != null) {
    //     args = ModalRoute
    //         .of(context)!
    //         .settings
    //         .arguments as Map<String, String>;
    //   }
    //
    //   await initImages();
    //   baseInit();
    //   //await Future.delayed(Duration(seconds: 1));
    //   appInit();
    // });
  }

  Future baseInit() async {
    try{
      if(args.containsKey("prid")){
        String strMENO = args["prid"] ?? "";

        //Save User Menu Log
        ZLUMDto objZN = ZLUMDto();
        objZN.ZNCONO = GlobalDto.CONO;
        objZN.ZNBRNO = GlobalDto.BRNO;
        objZN.ZNAPNO = GlobalDto.APNO;
        objZN.ZNMENO = strMENO;
        objZN.ZNUSNO = GlobalDto.USNO;

        ZLUMDao daoZN = ZLUMDao();
        daoZN.Save(objZN).then((value) {
          if(value.isNotEmpty) {
            debugPrint("[PageBase] [UserMenuLog] Result: $value");
          }
        });


      }
    }catch(ex){
      debugPrint("[PageBase] [UserMenuLog] Result: ${ex.toString()}");
    }
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

  void appInit(PagePlatform pagePlatform);

  void pageBehaviour(PageMode pageMode);

  void initBuild(BuildContext context){
    if(kIsWeb){
      html.document.addEventListener('keydown', (event) {
        if (event.type == 'tab') {
          event.preventDefault();
        }
      });
    }

    // GlobalStyle.screenWidth = MediaQuery.of(context).size.width;
    // GlobalStyle.screenHeight = MediaQuery.of(context).size.height;
  }

  /// Go back to the previous route using history route
  /// Only work if page called with NavigateTo
  navigateToBack(){
    context.pop();
  }

  /// Use to navigate to other page and can be use with NavigateToBack
  void navigateTo(String strRoute, {Map<String, String>? args}) async {
    String strArgs = "";
    if(args != null){
      args.forEach((key, value) {
        if (strArgs.isNotEmpty) {
          strArgs += "&";
        }
        strArgs += "$key=$value";
      });
    }

    if(strArgs.isNotEmpty){
      if(!strRoute.contains("?")){
        strRoute += "?";
      }
      strRoute += strArgs;
    }

    if(strRoute.isNotEmpty && strRoute.contains("?")){
      List<String> lstRoute = strRoute.split("?").toList();
      if(lstRoute.isNotEmpty){
        String strParam = await Encryption.symmetricEncrypt(lstRoute[1]);
        String strRouteEncrypted = "${lstRoute[0]}?xe=$strParam";
        //debugPrint("[RouteEncrypted] $strRouteEncrypted");
        strRoute = strRouteEncrypted;
      }
    }

    context.push(strRoute);
  }

  /// ALWAYS USE NavigateTo
  /// ONLY USE FOR Parent Route ex: Login, MainApp, ,MainHome
  void navigateGo(String strRoute, {Map<String, String>? args}) async {
    String strArgs = "";
    if(args != null){
      args.forEach((key, value) {
        if (strArgs.isNotEmpty) {
          strArgs += "&";
        }
        strArgs += "$key=$value";
      });
    }

    if(strArgs.isNotEmpty){

      if(!strRoute.contains("?")){
        strRoute += "?";
      }
      strRoute += strArgs;
    }

    if(strRoute.isNotEmpty && strRoute.contains("?")){
      List<String> lstRoute = strRoute.split("?").toList();
      if(lstRoute.isNotEmpty){
        String strParam = await Encryption.symmetricEncrypt(lstRoute[1]);
        String strRouteEncrypted = "${lstRoute[0]}?xe=$strParam";
        //debugPrint("[RouteEncrypted] $strRouteEncrypted");
        strRoute = strRouteEncrypted;
      }
    }

    while (context.canPop()) {
      context.pop();
    }

    Router.neglect(context, () => context.pushReplacement(strRoute));
  }

  void navigateToPBIEmbed(String menuTitle, String pbiWorkspaceId, String pbiReportId, {String? pbiFilterTable, String? pbiFilter}){
    String strRoute = "/UserControls/PowerBIViewer?mt=$menuTitle&ret=BI&wid=$pbiWorkspaceId&rid=$pbiReportId";
    if(pbiFilterTable != null && pbiFilterTable.isNotEmpty && pbiFilter != null && pbiFilter.isNotEmpty){
      strRoute += "&tbno=$pbiFilterTable&filter=$pbiFilter";
    }

    String strArgs = "";
    args.forEach((key, value) {
      if (strArgs.isNotEmpty) {
        strArgs += "&";
      }
      strArgs += "$key=$value";
    });
      if(strArgs.isNotEmpty){
      if(!strRoute.contains("?")){
        strRoute += "?";
      }
      strRoute += strArgs;
    }

    context.push(strRoute);
  }


}