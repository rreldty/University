import '../../Dto/Base/EntityDto.dart';
import '../../Dto/Base/DDLDto.dart';

import 'BaseDao.dart';

class DDLDao extends BaseDao {
  Future<List<DDLDto>> getList(EntityDto obj) async {
    try{
      Iterable list = await httpPost("api/Base/ListComboBox", obj);
      List<DDLDto> lst = list.map((model) => DDLDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }
}

