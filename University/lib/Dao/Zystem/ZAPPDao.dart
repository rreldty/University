import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZAPPDto.dart';

class ZAPPDao extends BaseDao {
  Future<String> Save(ZAPPDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZAPP/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<ZAPPDto> GetData(String strCONO, String strBRNO, String strUSNO) async {
    try{
      dynamic dto = await httpGet("api/ZAPP/GetData/$strCONO/$strBRNO/$strUSNO");
      return  ZAPPDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<ZAPPDto?> oneData(ZAPPDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZAPP/OneData",obj);
      return dto != null ?  ZAPPDto?.fromJson(dto) : null;
    } catch(ex){
      rethrow;
    }
  }

}