import '../../Dto/Zystem/ZVARDto.dart';
import 'BaseDao.dart';

class VariableDao extends BaseDao {
  Future<String> GetVariableValue(String strZRVANO) async {
    try{
      dynamic dto = await httpGet("api/Base/GetVariableValue/$strZRVANO");
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<ZVARDto> GetVariable(String strZRVANO) async {
    try{
      dynamic dto = await httpGet("api/Base/GetVariable/$strZRVANO");
      return ZVARDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }
}

