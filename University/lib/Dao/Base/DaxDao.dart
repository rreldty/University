import '../../Common/AppConfig.dart';
import '../../Dto/Base/EntityDto.dart';
import '../../Dto/Base/LookupDto.dart';
import 'BaseDao.dart';

class DaxDao extends BaseDao {
  Future<LookupDto> search(EntityDto obj) async {
    try{
      dynamic dto = await httpPost("api/Base/DaxSearch", obj);
      return LookupDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }
  Future<String> download(EntityDto obj) async {
    try{
      dynamic dto = await httpPost("api/Base/DaxDownload", obj);
      return dto.toString().replaceAll("~", AppConfig.ApiBaseURL);
    } catch(ex){
      rethrow;
    }
  }
}

