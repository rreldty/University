import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Training/KrsDetailDto.dart';

class KrsDetailDao extends BaseDao {
  Future<String> Save(KrsDetailDto obj) async {
    try{
      dynamic dto = await httpPost("api/KrsDetail/Save", obj);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<String> SaveHapus(List<KrsDetailDto> lst) async {
    try{
      dynamic dto = await httpPost("api/KrsDetail/SaveHapus", lst);
      return dto.toString();
    } catch(ex){
      rethrow;
    }
  }

  Future<KrsDetailDto> oneData(KrsDetailDto obj) async {
    try{
      dynamic dto = await httpPost("api/KrsDetail/OneData",obj);
      return  KrsDetailDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<List<KrsDetailDto>> ListPaging(KrsDetailDto obj) async {
    try{
      Iterable list = await httpPost("api/KrsDetail/ListPaging", obj);
      List<KrsDetailDto> lst = list.map((model) => KrsDetailDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<KrsDetailDto>> ListMatakuliah(KrsDetailDto obj) async {
    try{
      Iterable list = await httpPost("api/KrsDetail/ListMatakuliah", obj);
      List<KrsDetailDto> lst = list.map((model) => KrsDetailDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }
}