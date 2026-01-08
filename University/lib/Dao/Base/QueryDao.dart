import '../../Common/AppConfig.dart';
import '../../Dto/Base/LookupDto.dart';
import 'BaseDao.dart';
import '../../Dto/Base/EntityDto.dart';

class QueryDao extends BaseDao {
  Future<LookupDto> search(EntityDto obj) async {
    try{
      dynamic dto = await httpPost("api/Base/QuerySearch", obj);
      return LookupDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }
  Future<String> download(EntityDto obj) async {
    try{
      dynamic dto = await httpPost("api/Base/QueryDownload", obj);
      return dto.toString().replaceAll("~", AppConfig.ApiBaseURL);
    } catch(ex){
      rethrow;
    }
  }

  Future<List<dynamic>> SPSearch(EntityDto obj) async {
    try{
      dynamic lstDto = await httpPost("api/Base/QuerySPSearch", obj);
      return lstDto;
    } catch(ex){
      rethrow;
    }
  }

  Future<String> SPDownload(EntityDto obj) async {
    try{
      dynamic dto = await httpPost("api/Base/QuerySPDownload", obj);
      return dto.toString();//.replaceAll("~", AppConfig.ApiBaseURL);
    } catch(ex){
      rethrow;
    }
  }

}

