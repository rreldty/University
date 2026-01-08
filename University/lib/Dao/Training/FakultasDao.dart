import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Training/FakultasDto.dart';

class FakultasDao extends BaseDao {
  Future<String> Save(FakultasDto obj) async {
    try{
      dynamic dto = await httpPost("api/Fakultas/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<FakultasDto> oneData(FakultasDto obj) async {
    try{
      dynamic dto = await httpPost("api/Fakultas/OneData",obj);
      return  FakultasDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<List<FakultasDto>> ListPaging(FakultasDto obj) async {
    try{
      Iterable list = await httpPost("api/Fakultas/ListPaging", obj);
      List<FakultasDto> lst = list.map((model) => FakultasDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }
}