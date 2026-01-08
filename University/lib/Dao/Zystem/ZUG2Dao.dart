import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZUG2Dto.dart';

class ZUG2Dao extends BaseDao {
  Future<String> Save(ZUG2Dto obj) async {
    try{
      dynamic dto = await httpPost("api/ZUG2/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<String> SaveHeader(ZUG2Dto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUG2/SaveHeader", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<ZUG2Dto?> oneData(ZUG2Dto obj) async {
    try{
      dynamic dto = await httpPost("api/ZUG2/OneData",obj);
      return dto != null ? ZUG2Dto.fromJson(dto) : null;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZUG2Dto>?> listPaging(ZUG2Dto obj) async {
    try{
      List list = await httpPost("api/ZUG2/ListPaging", obj);
      return list.isNotEmpty ? list.map((model) => ZUG2Dto.fromJson(model)).toList() : null;
    } catch(ex){
      rethrow;
    }
  }

  Future<List?> tablePaging(ZUG2Dto obj) async {
    try{
      List list = await httpPost("api/ZUG2/TablePaging", obj);
      return list;
    } catch(ex){
      rethrow;
    }
  }

  Future<String> SaveDelete(ZUG2Dto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUG2/SaveDelete", obj);
      return dto.toString();
    } catch (ex) {
      throw ex;
    }
  }
}