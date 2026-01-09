import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Training/KRSDetailDto.dart';

class KRSDetailDao extends BaseDao {
  Future<String> Save(KRSDetailDto obj) async {
    try {
      dynamic dto = await httpPost("api/KRSDetail/Save", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> Delete(KRSDetailDto obj) async {
    try {
      dynamic dto = await httpPost("api/KRSDetail/Delete", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<KRSDetailDto> oneData(KRSDetailDto obj) async {
    try {
      dynamic dto = await httpPost("api/KRSDetail/OneData", obj);
      return KRSDetailDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<KRSDetailDto>> getList(KRSDetailDto obj) async {
    try {
      Iterable list = await httpPost("api/KRSDetail/List", obj);
      List<KRSDetailDto> lst =
          list.map((model) => KRSDetailDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<KRSDetailDto>> listMataKuliah(KRSDetailDto obj) async {
    try {
      Iterable list = await httpPost("api/KRSDetail/ListMataKuliah", obj);
      List<KRSDetailDto> lst =
          list.map((model) => KRSDetailDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<KRSDetailDto>> listPaging(KRSDetailDto obj) async {
    try {
      Iterable list = await httpPost("api/KRSDetail/ListPaging", obj);
      List<KRSDetailDto> lst =
          list.map((model) => KRSDetailDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }
}
