import 'package:university/Dao/Base/BaseDao.dart';
import 'package:university/Dto/Zystem/ZAUTDto.dart';

class ZAUTDao extends BaseDao {

  Future<String> Save(ZAUTDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZAUT/Save", obj);
      return dto.toString();
    } catch(ex){
      throw ex;
    }
  }

  Future<List<ZAUTDto>> listPaging(ZAUTDto obj) async {
    try{
      Iterable list = await httpPost("api/ZAUT/ListPaging", obj);
      List<ZAUTDto> lst = list.map((model) => ZAUTDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      throw ex;
    }
  }

  Future<List<ZAUTDto>> listApplication(ZAUTDto obj) async {
    try{
      Iterable list = await httpPost("api/ZAUT/ListApplication", obj);
      List<ZAUTDto> lst = list.map((model) => ZAUTDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      throw ex;
    }
  }

  Future<List<ZAUTDto>> ListMenu(ZAUTDto obj) async {
    try{
      Iterable list = await httpPost("api/ZAUT/ListMenu", obj);
      List<ZAUTDto> lst = list.map((model) => ZAUTDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      throw ex;
    }
  }
  Future<ZAUTDto?> oneData(ZAUTDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZAUT/OneData", obj);
      return dto != null ? ZAUTDto.fromJson(dto) : null;
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> SaveWithLine(ZAUTDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZAUT/SaveWithLine", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> SaveDelete(ZAUTDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZAUT/SaveDelete", obj);
      return dto.toString();
    } catch (ex) {
      throw ex;
    }
  }

}