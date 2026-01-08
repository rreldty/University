import '../../Dto/Base/EntityDto.dart';
import '../../Dto/Base/LookupDto.dart';
import 'BaseDao.dart';

class LookupDao extends BaseDao {
  Future<LookupDto> listLookup(EntityDto obj) async {
    try{
      dynamic dto = await httpPost("api/Base/ListLookup", obj);
      return LookupDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<LookupDto> listLookupOne(EntityDto obj) async {
    try{
      dynamic dto = await httpPost("api/Base/ListLookupOne", obj);
      return LookupDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }
}

