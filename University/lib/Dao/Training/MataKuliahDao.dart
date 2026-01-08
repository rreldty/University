import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Training/MataKuliahDto.dart';

class MataKuliahDao extends BaseDao {
  Future<String> Save(MataKuliahDto obj) async {
    try {
      dynamic dto = await httpPost("api/MataKuliah/Save", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<MataKuliahDto> oneData(MataKuliahDto obj) async {
    try {
      dynamic dto = await httpPost("api/MataKuliah/OneData", obj);
      return MataKuliahDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<MataKuliahDto>> ListPaging(MataKuliahDto obj) async {
    try {
      Iterable list = await httpPost("api/MataKuliah/ListPaging", obj);
      List<MataKuliahDto> lst = list.map((model) => MataKuliahDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }
}
