import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';
import '../../Common/AppConfig.dart';
import '../../Common/Encryption.dart';
import 'ResponseException.dart';
import 'TokenHelper.dart';

abstract class BaseDao{
//region HTTP
  Future<dynamic> httpGet(String strUrlApi,
      {bool useTokenApi = true, String authorizationToken = ""}) async {
    try {
      String urlApi = getApiUrl(strUrlApi);
      Map<String, String> paramHeader = {};

      if (useTokenApi) {
        String tokenAPI = await getTokenApi();
        paramHeader["Authorization"] = "Bearer $tokenAPI";
      }

      final response = await http.get(Uri.parse(urlApi), headers: paramHeader);
      if (isResponseSuccessful(response)) {
        if(response.body.isNotEmpty) {
          if(isJson(response.body)){
            return json.decode(response.body);
          }
          return response.body;
        }else{
          return "";
        }
      } else {
        return _failedResponse(response);
      }
    } on SocketException {
      throw FetchDataException("Failed to open connection to server");
    }
  }

  Future<dynamic> httpPost(String strUrlApi, dynamic obj,
      {bool useTokenApi = true, String authorizationToken = ""}) async {
    try {
      String urlApi = getApiUrl(strUrlApi);

      Map<String, String> paramHeader = {};
      paramHeader["Content-Type"] = "application/json";
      //paramHeader["Access-Control-Allow-Origin"] = "*";

      // if (useTokenApi) {
      //   String tokenAPI = await getTokenApi();
      //   paramHeader["Authorization"] = "Bearer $tokenAPI";
      // }

      dynamic objBody;

      if(obj != null) {
        if (obj is List) {
          objBody = json.encode(obj.map((e) => e.toMap()).toList());
        } else {
          objBody = json.encode(obj.toMap());
          // debugPrint(objBody);
        }
      }

      // debugPrint(paramHeader["Authorization"]);
      // debugPrint(json.encode(obj.toMap()));
      final response = await http.post(Uri.parse(urlApi),
          headers: paramHeader, body: objBody);
      if (isResponseSuccessful(response)) {
        //debugPrint(response.headers["content-type"]);
        if(response.body.isNotEmpty) {
          //debugPrint(response.body);
          if(isJson(response.body)){
            return json.decode(response.body);
          }
          return response.body;
        }else{
          return "";
        }
      } else {
        return _failedResponse(response);
      }
    } on SocketException {
      throw FetchDataException("Failed to open connection to server");
    }
  }

  Future<dynamic> httpUploadFile(String strUrlApi, String strFileName, Uint8List fileBytes,
      {bool useTokenApi = true, String authorizationToken = "", Map<String, String>? dataParam}) async {
    try {
      String urlApi = getApiUrl(strUrlApi);

      var request = http.MultipartRequest("POST", Uri.parse(urlApi));
      request.files.add(
          http.MultipartFile.fromBytes(
              "file", fileBytes,
              filename: strFileName)
      );

      if(dataParam != null){
        request.fields.addAll(dataParam);
      }

      if (useTokenApi) {
        String tokenAPI = await getTokenApi();
        request.headers["Authorization"] = "Bearer $tokenAPI";
      }

      http.Response response = await http.Response.fromStream(await request.send());
      // debugPrint("Result: ${response.statusCode}");
      if (isResponseSuccessful(response)) {
        //debugPrint(response.headers["content-type"]);
        if(response.body.isNotEmpty) {
          //debugPrint(response.body);
          if(isJson(response.body)){
            return json.decode(response.body);
          }
          return response.body;
        }else{
          return "";
        }
      } else {
        return _failedResponse(response);
      }
    } on SocketException {
      throw FetchDataException("Failed to open connection to server");
    }
  }

  Future<dynamic> httpDownloadFile(String strUrlApi, dynamic obj,
      {bool useTokenApi = true, String authorizationToken = ""}) async {
    try {
      String urlApi = getApiUrl(strUrlApi);

      var paramHeader = <String, String>{};

      dynamic objBody;

      if(obj != null) {
        if (obj is List) {
          objBody = json.encode(obj.map((e) => e.toMap()).toList());
        } else {
          objBody = json.encode(obj.toMap());
        }
      }

      paramHeader["Content-Type"] = "application/json";
      if (useTokenApi) {
        String tokenAPI = (authorizationToken.isNotEmpty  && authorizationToken.isNotEmpty)
            ? authorizationToken
            : await getTokenApi();
        paramHeader["Authorization"] = "Bearer $tokenAPI";
      }

      final response = await http.post(Uri.parse(urlApi),
          headers: paramHeader, body: objBody);
      Uint8List fileBytes = Uint8List.fromList(response.bodyBytes);
      return fileBytes;
    } on SocketException {
      throw FetchDataException("Failed to open connection to server");
    }
  }

  Future<dynamic> httpPut(String strUrlApi, dynamic obj,
      {bool useTokenApi = true, String authorizationToken = ""}) async {
    try {
      String urlApi = getApiUrl(strUrlApi);

      Map<String, String> paramHeader = {};
      paramHeader["Content-Type"] = "application/json";
      //paramHeader["Access-Control-Allow-Origin"] = "*";

      if (useTokenApi) {
        String tokenAPI = await getTokenApi();
        paramHeader["Authorization"] = "Bearer $tokenAPI";
      }

      dynamic objBody;

      if(obj != null) {
        if (obj is List) {
          objBody = json.encode(obj.map((e) => e.toMap()).toList());
        } else {
          objBody = json.encode(obj.toMap());
        }
      }

      final response = await http.put(Uri.parse(urlApi),
          headers: paramHeader, body: objBody);
      if (isResponseSuccessful(response)) {
        if(response.body.isNotEmpty) {
          if(isJson(response.body)){
            return json.decode(response.body);
          }
          return response.body;
        }else{
          return "";
        }
      } else {
        return _failedResponse(response);
      }
    } on SocketException {
      throw FetchDataException("Failed to open connection to server");
    }
  }

    //endregion

  //region Methods
  bool isResponseSuccessful(http.Response response)
  {
    if(response.statusCode >= 200 && response.statusCode <= 202){
        return true;
    }

    return false;
  }

  Future<String> getTokenApi() async {
    String strToken = "";
    String strTokenExpired = "";

    debugPrint("getTokenApi");
    SharedPreferences prefs = await SharedPreferences.getInstance();
    strToken = prefs.getString("TokenAPI") ?? "";
    strTokenExpired = prefs.getString("TokenExpired") ?? "";

    // debugPrint(strToken);
    // debugPrint(strTokenExpired);

    if (strToken.isNotEmpty) {
      DateTime dtmTokenExpired = DateTime.now();

      if (strTokenExpired.isNotEmpty) {
        dtmTokenExpired = DateTime.parse(strTokenExpired);
      }

      if (dtmTokenExpired.isBefore(DateTime.now())) {
        debugPrint("Token Expired");
        strToken = "";
        strTokenExpired = "";
      } else {
        debugPrint("Token Valid");
      }
    }

    if(strToken.isEmpty){
      try {
        String urlApi = getApiUrl("oauth/token");
        var paramBody = await TokenHelper.getUserApi();

        var basicAuth = Encryption.simpleEncrypt("${TokenHelper.ApiKey}:${TokenHelper.ApiSecret}");
        Map<String, String> paramHeader = {};
        paramHeader["Content-Type"] = "application/x-www-form-urlencoded";
        paramHeader["Authorization"] = "Basic $basicAuth";

        final response = await http.post(Uri.parse(urlApi),
            headers: paramHeader,
            body: paramBody);
        if (isResponseSuccessful(response)) {
          debugPrint("token response");
          if(response.body.isNotEmpty) {
            Map<String, dynamic> dto = json.decode(response.body);
            strToken = dto["access_token"] ?? "";
            var dtmExpireIn = DateTime.now().add(Duration(seconds: dto["expires_in"] ?? 0));
            await prefs.setString("TokenAPI", strToken);
            await prefs.setString("TokenExpired", dtmExpireIn.toString());
          } else {
            throw UnauthorizedException(response.body.toString());
          }
        } else {
          return _failedResponse(response);
        }
      } on SocketException {
        throw FetchDataException("Failed to open connection to server");
      }
    }

    return strToken;
  }

  dynamic _failedResponse(http.Response response) {
    switch (response.statusCode) {
      case 400:
        throw BadRequestException(response.body.toString());
      case 401:
        throw UnauthorizedException("Unauthorized");
      case 403:
        throw UnauthorizedException("Unauthorized");
      case 500:
        throw InternalServerException(json.decode(response.body));
      default:
        throw FetchDataException(
            "Error ${response.statusCode} - ${response.body}");
    }
  }

  static String getApiUrl(String urlApi){
    String baseUrl = AppConfig.ApiBaseURL;
    String targetUrl = urlApi.replaceAll(RegExp(r"\/\B"), "/ ");
    String strFullUrl = Uri.encodeFull("$baseUrl/$targetUrl").trim();
    // debugPrint(strFullUrl);
    return strFullUrl;
  }

  bool isJson(String strResponse) {
    if(strResponse.isNotEmpty) {
      try {
        json.decode(strResponse);
      } catch (e) {
        return false;
      }
    }
    return true;
  }

// dynamic _successResponse(http.Response response) async {
//   switch (response.headers["content-type"]) {
//     case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
//       {
//         if (response.contentLength == 0){
//           return;
//         }
//         String strDisposition = response.headers["content-disposition"];
//         String strFileName = "";
//         if(strDisposition.contains("filename=")){
//           strFileName = strDisposition.split("filename=")[1];
//         }
//         Directory tempDir = await getTemporaryDirectory();
//         String tempPath = tempDir.path;
//         File file = File("$tempPath/$strFileName");
//         await file.writeAsBytes(response.bodyBytes);
//
//         return file;
//       }
//     default:
//       {
//
//       }
//   }
// }
//endregion
}