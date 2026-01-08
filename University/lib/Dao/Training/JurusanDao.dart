import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Training/JurusanDto.dart';

class JurusanDao extends BaseDao {
  Future<String> Save(JurusanDto obj) async {
    try {
      dynamic dto = await httpPost("api/Jurusan/Save", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<JurusanDto> oneData(JurusanDto obj) async {
    try {
      dynamic dto = await httpPost("api/Jurusan/OneData", obj);
      return JurusanDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<JurusanDto>> ListPaging(JurusanDto obj) async {
    try {
      Iterable list = await httpPost("api/Jurusan/ListPaging", obj);
      List<JurusanDto> lst = list.map((model) => JurusanDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }
}
