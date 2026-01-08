import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZCMPDto.dart';

class ZCMPDao extends BaseDao {

  Future<String> Save(ZCMPDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZCMP/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }
  
  Future<List<ZCMPDto>> listPagingCONO(ZCMPDto obj) async {
    try{
      Iterable list = await httpPost("api/ZCMP/ListPagingCONO", obj);
      List<ZCMPDto> lst = list.map((model) => ZCMPDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZCMPDto>> ListPagingNotInGlobalCustomerMaster(ZCMPDto obj) async {
    try {
      Iterable list = await httpPost("api/ZCMP/ListPagingNotInGlobalCustomerMaster", obj);
      List<ZCMPDto> lst = list.map((model) => ZCMPDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

}