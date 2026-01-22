import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Training/KrsHeaderDto.dart';

class KrsHeaderDao extends BaseDao {
  Future<String> Save(KrsHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/KrsHeader/Save", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<KrsHeaderDto> OneData(KrsHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/KrsHeader/OneData", obj);
      return KrsHeaderDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }

  Future<KrsHeaderDto> OneDataMahasiswa(KrsHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/KrsHeader/OneDataMahasiswa", obj);
      return KrsHeaderDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> SaveList(KrsHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/KrsHeader/SaveList", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<KrsHeaderDto>> ListPaging(KrsHeaderDto obj) async {
    try {
      Iterable list = await httpPost("api/KrsHeader/ListPaging", obj);
      List<KrsHeaderDto> lst =
          list.map((model) => KrsHeaderDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }
}
