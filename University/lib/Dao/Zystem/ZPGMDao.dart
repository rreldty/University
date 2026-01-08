import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZPGMDto.dart';

class ZPGMDao extends BaseDao {
  Future<String> Save(ZPGMDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZPGM/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<ZPGMDto?> oneData(ZPGMDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZPGM/OneData", obj);
      return dto != null ?  ZPGMDto?.fromJson(dto) : null;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZPGMDto>> list(ZPGMDto obj) async {
    try{
      Iterable list = await httpPost("api/ZPGM/List", obj);
      List<ZPGMDto> lst = list.map((model) => ZPGMDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZPGMDto>> listPaging(ZPGMDto obj) async {
    try{
      Iterable list = await httpPost("api/ZPGM/ListPaging", obj);
      List<ZPGMDto> lst = list.map((model) => ZPGMDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<ZPGMDto> GetData(String strCONO, String strBRNO, String strUSNO) async {
    try{
      dynamic dto = await httpGet("api/ZPGM/GetData/$strCONO/$strBRNO/$strUSNO");
      return  ZPGMDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

}