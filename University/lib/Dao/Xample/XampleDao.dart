import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Xample/XampleDto.dart';

class XampleDao extends BaseDao {
  Future<String> Save(XampleDto obj) async {
    try{
      dynamic dto = await httpPost("api/SSN2/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<XampleDto> oneData(XampleDto obj) async {
    try{
      dynamic dto = await httpPost("api/SSN2/OneData",obj);
      return  XampleDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<String> SaveList(XampleDto obj) async {
    try{
      dynamic dto = await httpPost("api/SSN2/SaveList", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<List<XampleDto>> ListPaging(XampleDto obj) async {
    try{
      Iterable list = await httpPost("api/SSN2/ListPaging", obj);
      List<XampleDto> lst = list.map((model) => XampleDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }
}