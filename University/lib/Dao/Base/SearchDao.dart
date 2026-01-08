import 'BaseDao.dart';
import '../../Dto/Base/EntityDto.dart';
import '../../Dto/Base/SearchDto.dart';

class SearchDao extends BaseDao {
  Future<List<SearchDto>> getList(String strEntity, String strFilter) async {
    try{
      EntityDto obj = EntityDto(
          Entity: strEntity,
          Filter: strFilter
      );

      Iterable list = await httpPost("api/Base/ListSearch/", obj);
      List<SearchDto> lst = list.map((model) => SearchDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }
}

