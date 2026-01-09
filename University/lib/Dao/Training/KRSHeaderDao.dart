import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Training/KRSHeaderDto.dart';

class KRSHeaderDao extends BaseDao {
  Future<String> Save(KRSHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/KRSHeader/Save", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> Update(KRSHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/KRSHeader/Update", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> Delete(KRSHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/KRSHeader/Delete", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<KRSHeaderDto> oneData(KRSHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/KRSHeader/OneData", obj);
      return KRSHeaderDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<KRSHeaderDto>> getList(KRSHeaderDto obj) async {
    try {
      Iterable list = await httpPost("api/KRSHeader/List", obj);
      List<KRSHeaderDto> lst =
          list.map((model) => KRSHeaderDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<KRSHeaderDto>> listPaging(KRSHeaderDto obj) async {
    try {
      Iterable list = await httpPost("api/KRSHeader/ListPaging", obj);
      List<KRSHeaderDto> lst =
          list.map((model) => KRSHeaderDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<KRSHeaderDto>> reportKRS(KRSHeaderDto obj) async {
    try {
      Iterable list = await httpPost("api/KRSHeader/ReportKRS", obj);
      List<KRSHeaderDto> lst =
          list.map((model) => KRSHeaderDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }
}
