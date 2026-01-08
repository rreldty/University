import 'dart:math';
import 'dart:io';

import 'package:flutter/foundation.dart';
import "package:universal_html/html.dart" as html;
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:go_router/go_router.dart';
import 'package:package_info_plus/package_info_plus.dart';
import '../../Dao/Base/VariableDao.dart';
import '../../Dto/Base/ModalPopupResult.dart';
import '../../UserControls/MessageBox.dart';
import 'package:intl/intl.dart';
import 'package:url_launcher/url_launcher.dart';

import '../Dto/Zystem/ZVARDto.dart';
import 'AppConfig.dart';

class CommonMethod{
  static Future<String> GetVariableValue(String strZRVANO) {
    VariableDao dao = VariableDao();
    return dao.GetVariableValue(strZRVANO);
  }

  static Future<ZVARDto> GetVariable(String strZRVANO) async {
    VariableDao dao = VariableDao();
    return dao.GetVariable(strZRVANO);
  }

  //region DateTime
  static double DateToNumeric(DateTime dt){
    return double.tryParse(DateFormat(AppConfig.patternNumericDate, "en_US").format(dt)) ?? 0;
  }

  static double TimeToNumeric(DateTime dt){
    return double.tryParse(DateFormat(AppConfig.patternNumericTime, "en_US").format(dt)) ?? 0;
  }

  static double DateTimeToNumeric(DateTime dt){
    return double.tryParse(DateFormat(AppConfig.patternNumericDateTime, "en_US").format(dt)) ?? 0;
  }

  static String DateToStringFormat(DateTime dt){
    return DateFormat(AppConfig.patternStringDate, "en_US").format(dt);
  }

  static String DateTimeToStringFormat(DateTime dt){
    return DateFormat(AppConfig.patternStringDateTime, "en_US").format(dt);
  }

  static String DateTimeSecondToStringFormat(DateTime dt){
    return DateFormat(AppConfig.patternStringDateTimeSecond, "en_US").format(dt);
  }
  
  static double TimeStringToNumeric(String str){
    List<String> lstTime = str.split(":").toList();
    double dblTime = 0;
    for(int n = 0; n < lstTime.length; n++){
      dblTime = (dblTime * 100) + (double.tryParse(lstTime[n]) ?? 0);
    }
    return  dblTime;
  }

  static double DateStringToNumeric(String str) {
    try{
      var parsedDate = DateFormat(AppConfig.patternStringDate).parse(str);
      return DateToNumeric(parsedDate);
    }catch(ex){
      debugPrint(ex.toString());
      return 0;
    }
  }

  static DateTime NumericToDate(double dbl) {
    String strDate = dbl.toString();
    int intYear = int.tryParse(strDate.substring(0, 4)) ?? 0;
    int intMonth = int.tryParse(strDate.substring(4, 6)) ?? 0;
    int intDate = int.tryParse(strDate.substring(6, 8)) ?? 0;

    return DateTime(intYear,intMonth,intDate);
  }

  static String NumericToDateString(double dbl) {
    try{
      if(dbl > 0) {
        DateTime dt = NumericToDate(dbl);
        return DateToStringFormat(dt);
      }
    }catch(e){}
    return "";
  }

  static String NumericToTimeString(double dbl, {bool showSecond = false}) {
    try{
      if(dbl > 0) {
        String strTime = NumberFormat("##0", "en_US").format(dbl);
        strTime = strTime.length > 4 ? (1000000 + (int.tryParse(strTime) ?? 0)).toString() : (10000 + (int.tryParse(strTime) ?? 0)).toString();
        strTime = strTime.substring(1);
        if(!showSecond){
          return "${strTime.substring(0, 2)}:${strTime.substring(2, 4)}";
        }
        else{
          return "${strTime.substring(0, 2)}:${strTime.substring(2, 4)}:${strTime.substring(4, 6)}";
        }
      }
    }catch(e){}
    return "";
  }

  static String NumericToDateTimeString(double dbl) {
    try{
      if(dbl > 0) {
        DateTime dt = NumericToDate(dbl);
        return DateTimeToStringFormat(dt);
      }
    }catch(e){}
    return "";
  }

  static String NumericToStringFormat(NumericType numericType, double flt){
    return NumberFormat(AppConfig.numericFormat[numericType.name], "en_US").format(flt);
  }

  static String NumericToAmountFormat(double flt){
    return NumberFormat(AppConfig.numericFormat[NumericType.Amount], "en_US").format(flt);
  }

  static String NumericToPriceFormat(double flt){
    return NumberFormat(AppConfig.numericFormat[NumericType.Price], "en_US").format(flt);
  }

  static String NumericToQuantityFormat(double flt){
    return NumberFormat(AppConfig.numericFormat[NumericType.Quantity], "en_US").format(flt);
  }

  static String NumericToRateFormat(double flt){
    return NumberFormat(AppConfig.numericFormat[NumericType.Rate], "en_US").format(flt);
  }

  static String NumericToPercentFormat(double flt){
    return NumberFormat(AppConfig.numericFormat[NumericType.Percent], "en_US").format(flt);
  }

  static String NumericToUnitFormat(double flt){
    return NumberFormat(AppConfig.numericFormat[NumericType.Unit], "en_US").format(flt);
  }
  //endregion

  //region DeviceInfo
  static Future<String> getVersionName() async{
    PackageInfo packageInfo = await PackageInfo.fromPlatform();
    return packageInfo.version;
  }

  static Future<String> getVersionCode() async{
    PackageInfo packageInfo = await PackageInfo.fromPlatform();
    return packageInfo.buildNumber;
  }
  //endregion

  //region url Launcher
  static launchURL(String strUrl, {bool isNewTab = true}) async {
    try{
      strUrl = strUrl.replaceFirst("~", AppConfig.ApiBaseURL);
      if (await canLaunchUrl(Uri.parse(strUrl))) {
        await launchUrl(Uri.parse(strUrl),webOnlyWindowName: isNewTab ? '_blank' : '_self');
      }else{
        throw Exception("Url not found");
      }
    }catch(ex){
      rethrow;
    }
  }

  static downloadFile(String strUrl,String strName){
    strUrl = strUrl.replaceFirst("~", AppConfig.ApiBaseURL);
    html.AnchorElement anchorElement =  html.AnchorElement(href: strUrl);
    anchorElement.download = strName;
    anchorElement.click();
  }
  //endregion

  //region MapHelper
  static Map<String, dynamic> getUrlParameter(String strRoute){
    //debugPrint("getUrlParameter");
    Map<String, dynamic> mapArgs = <String, dynamic>{};
    if(strRoute.contains("?")){
      List<String> q1 = strRoute.split("?");

      mapArgs["routeName"] = q1[0];
      List<String> q2 = q1[1].split("&");

      for(int n = 0; n < q2.length; n++){
        List<String> q3 = q2[n].split("=");
        if(q3.length > 1) {
          mapArgs[q3[0]] = q3[1];
        }
      }
    }
    return mapArgs;
  }

  static Map<String, dynamic> getParameter(String strParm){
    //debugPrint("getUrlParameter");
    Map<String, dynamic> mapArgs = <String, dynamic>{};
    if(strParm.contains("&")){
      List<String> q2 = strParm.split("&");

      for(int n = 0; n < q2.length; n++){
        List<String> q3 = q2[n].split("=");
        if(q3.length > 1) {
          mapArgs[q3[0]] = q3[1];
        }
      }
    }
    return mapArgs;
  }

  static String getMapValueByIndex(Map<dynamic, dynamic> map, int idx){
    return map.values.elementAt(idx);
  }
  //endregion

//region StringHelper
  static String stringRight(String strText, int n) {
    return strText.padLeft(n).substring(max(strText.length - n, 0)).trim();
  }
//endregion

//region Color
  static Color darken(Color color, [double amount = .1]) {
    assert(amount >= 0 && amount <= 1);

    final hsl = HSLColor.fromColor(color);
    final hslDark = hsl.withLightness((hsl.lightness - amount).clamp(0.0, 1.0));

    return hslDark.toColor();
  }

  static Color lighten(Color color, [double amount = .1]) {
    assert(amount >= 0 && amount <= 1);

    final hsl = HSLColor.fromColor(color);
    final hslLight = hsl.withLightness((hsl.lightness + amount).clamp(0.0, 1.0));

    return hslLight.toColor();
  }

//endregion

  static bool isSameDate(DateTime dtm1, DateTime dtm2) {
    return dtm1.year == dtm2.year && dtm1.month == dtm2.month
        && dtm1.day == dtm2.day;
  }

  static String resolveUrl(String strUrl) {
    strUrl = strUrl.replaceFirst("//", "/");
    if(!strUrl.startsWith("/")){
      strUrl = "/$strUrl";
    }
    return "${Uri.base.origin + Uri.base.path}#$strUrl";
  }

  static Future<Widget?> getAssetImage(String path) async {
    try {
      await rootBundle.load(path);
      return Image.asset(path);
    } catch (_) {
      return null; // Return this widget
    }
  }

  static bool isWebOrDesktop(){
    if(!kIsWeb) {
      if (Platform.isAndroid) {
        return false;
      }

      if (Platform.isIOS) {
        return false;
      }
    }
    return true;
  }
}