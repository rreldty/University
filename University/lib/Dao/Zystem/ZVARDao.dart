import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZVARDto.dart';

class ZVARDao extends BaseDao {
  Future<String> Save(ZVARDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZVAR/Save", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<ZVARDto> GetData(String strCONO, String strBRNO,
      String strUSNO) async {
    try {
      dynamic dto = await httpGet(
          "api/ZVAR/GetData/$strCONO/$strBRNO/$strUSNO");
      return ZVARDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }


  Future<ZVARDto> oneData(ZVARDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZVAR/OneData", obj);
      return ZVARDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }

   Future<ZVARDto> UrlByVANO(ZVARDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZVAR/UrlByVANO", obj);
      return ZVARDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }
}