import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZAUMDto.dart';

class ZAUMDao extends BaseDao {

  Future<String> Save(ZAUMDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZAUM/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<ZAUMDto> oneData(ZAUMDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZAUM/OneData",obj);
      return  ZAUMDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<ZAUMDto> getOne(ZAUMDto obj) async {
    try{
      print("get dao");
      dynamic dto = await httpPost("api/ZAUM/GetOne",obj);
      return  ZAUMDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZAUMDto>> listPaging(ZAUMDto obj) async {
    try{
      Iterable list = await httpPost("api/ZAUM/ListPaging", obj);
      List<ZAUMDto> lst = list.map((model) => ZAUMDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZAUMDto>> listUser(ZAUMDto obj) async {
    try{
      Iterable list = await httpPost("api/ZAUM/ListPagingUser", obj);
      List<ZAUMDto> lst = list.map((model) => ZAUMDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<String> addLine(ZAUMDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZAUM/AddLine", obj);
      return dto.toString();
    } catch(ex){
      throw ex;
    }
  }

  Future<String> Delete(ZAUMDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZAUM/Delete", obj);
      return dto.toString();
    } catch(ex){
      throw ex;
    }
  }

}