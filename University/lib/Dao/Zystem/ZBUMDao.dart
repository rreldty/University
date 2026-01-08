import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZBUMDto.dart';

class ZBUMDao extends BaseDao {
  Future<String> Save(ZBUMDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZBUM/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<ZBUMDto> oneData(ZBUMDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZBUM/OneData", obj);
      return  ZBUMDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZBUMDto>> list(ZBUMDto obj) async {
    try{
      Iterable list = await httpPost("api/ZBUM/List", obj);
      List<ZBUMDto> lst = list.map((model) => ZBUMDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZBUMDto>> listPaging(ZBUMDto obj) async {
    try{
      Iterable list = await httpPost("api/ZBUM/ListPaging", obj);
      List<ZBUMDto> lst = list.map((model) => ZBUMDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

}