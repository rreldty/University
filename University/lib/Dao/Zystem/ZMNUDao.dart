import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZMNUDto.dart';

class ZMNUDao extends BaseDao {
  Future<String> Save(ZMNUDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZMNU/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<ZMNUDto?> oneData(ZMNUDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZMNU/OneData",obj);
      return dto != null ?  ZMNUDto?.fromJson(dto) : null;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZMNUDto>> list(ZMNUDto obj) async {
    try{
      Iterable list = await httpPost("api/ZMNU/List", obj);
      List<ZMNUDto> lst = list.map((model) => ZMNUDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZMNUDto>> listPaging(ZMNUDto obj) async {
    try{
      Iterable list = await httpPost("api/ZMNU/ListPaging", obj);
      List<ZMNUDto> lst = list.map((model) => ZMNUDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZMNUDto>> listPagingNotInUserAuthority(ZMNUDto obj) async {
    try{
      Iterable list = await httpPost("api/ZMNU/listPagingNotInUserAuthority", obj);
      List<ZMNUDto> lst = list.map((model) => ZMNUDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }
}