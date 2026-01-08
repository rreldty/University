import 'dart:convert';

import 'dart:math';

import 'package:encrypt/encrypt.dart' as encrypt;
import 'package:flutter/foundation.dart';
import 'package:shared_preferences/shared_preferences.dart';

class Encryption {
  //region Simple
  static String simpleDecrypt(String strEncrypted){
    String strPlain = "";

    try {
      var byData = base64.decode(strEncrypted);
      strPlain = utf8.decode(byData);
    }
    catch (exception){
      strPlain = "";
    }

    return strPlain;
  }

  static String simpleEncrypt(String strPlain){
    String strEncrypted = "";

    try {
      var byData = utf8.encode(strPlain);
      strEncrypted = base64.encode(byData);
    }
    catch (exception){
      strEncrypted = "";
    }

    return strEncrypted;
  }
  //endregion
  
  //region Symmetric
  static Future<String> symmetricDecrypt(String strEncrypted) async {
    String strPlain = "";

    try {
      SharedPreferences pref = await SharedPreferences.getInstance();
      String strKey = pref.getString("RandomKey") ?? "";

      strEncrypted = utf8.fuse(base64Url).decode(strEncrypted);
      List<String> lstEncrypted = strEncrypted.split("|");

      final key = encrypt.Key.fromBase64(strKey);
      final iv = encrypt.IV.fromBase64(lstEncrypted[0]);

      strPlain = encrypt.Encrypter(encrypt.AES(key)).decrypt64(lstEncrypted[1], iv: iv);
    }
    catch (ex){
      debugPrint("[symmetricDecrypt-Exception] $ex");
      strPlain = "";
    }

    return strPlain;
  }

  static Future<String> symmetricEncrypt(String strPlain) async {
    String strEncrypted = "";

    try {
      SharedPreferences pref = await SharedPreferences.getInstance();
      String strKey = pref.getString("RandomKey") ?? "";

      encrypt.Key key = encrypt.Key.fromBase64(strKey);
      encrypt.IV iv = encrypt.IV.fromLength(16);

      encrypt.Encrypted encrypted = encrypt.Encrypter(encrypt.AES(key)).encrypt(strPlain, iv: iv);
      strEncrypted = "${base64.encode(iv.bytes)}|${base64.encode(encrypted.bytes)}";
      strEncrypted = utf8.fuse(base64Url).encode(strEncrypted);
      // debugPrint("[symmetricEncrypt-EncryptedLength] ${strEncrypted.length}");
      // debugPrint("[symmetricEncrypt-Encrypted] $strEncrypted");
    }
    catch (exception){
      strEncrypted = "";
    }

    return strEncrypted;
  }

  static Future generateRandomKey() async {
    String strKey = _getRandomString(32);

    SharedPreferences pref = await SharedPreferences.getInstance();
    pref.setString("RandomKey", utf8.fuse(base64).encode(strKey));
  }

  static String _getRandomString(int length){
    const chars = 'AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890';
    Random rnd = Random();

    return String.fromCharCodes(Iterable.generate(
        length, (_) => chars.codeUnitAt(rnd.nextInt(chars.length))));
  }
  //endregion
}