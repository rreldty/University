import 'package:shared_preferences/shared_preferences.dart';
import '../../Common/Encryption.dart';

class TokenHelper{
  static void setUserApi(String strUserId, String strPassword) async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString("Token_User", Encryption.simpleEncrypt("$strUserId|$strPassword"));
  }

  static Future<Map<String, String>> getUserApi() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    List<String> lstUserApi = Encryption.simpleDecrypt(prefs.getString("Token_User") ?? "").split("|");

    Map<String, String> map = Map<String, String>();
    map["username"] = "aglis"; //lstUserApi[0];
    map["password"] = "aglis"; //lstUserApi[1];
    map["grant_type"] = "password";
    return map;
  }

  static String ApiKey  = "81E7C798-695E-4F95-BD0A-E1E951F33ABF";

  static String ApiSecret  = "FCC672AA-4E7F-4B70-901E-45F5C7DDB3EE";
}