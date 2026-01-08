import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZDICDto.dart';

class ZDICDao extends BaseDao {
  Future<List<ZDICDto>> ListDictionary(ZDICDto obj) async {
    try{
      Iterable list = await httpPost("api/ZDIC/ListDictionary", obj);
      List<ZDICDto> lst = list.map((model) => ZDICDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }


}