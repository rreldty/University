import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZLUMDto.dart';

class ZLUMDao extends BaseDao {
  Future<String> Save(ZLUMDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZLUM/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }
}