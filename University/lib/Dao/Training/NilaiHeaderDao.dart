import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Training/NilaiHeaderDto.dart';

class NilaiHeaderDao extends BaseDao {
  Future<String> Save(NilaiHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/NilaiHeader/Save", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> Update(NilaiHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/NilaiHeader/Update", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> Delete(NilaiHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/NilaiHeader/Delete", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<NilaiHeaderDto> oneData(NilaiHeaderDto obj) async {
    try {
      dynamic dto = await httpPost("api/NilaiHeader/OneData", obj);
      return NilaiHeaderDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<NilaiHeaderDto>> getList(NilaiHeaderDto obj) async {
    try {
      Iterable list = await httpPost("api/NilaiHeader/List", obj);
      List<NilaiHeaderDto> lst =
          list.map((model) => NilaiHeaderDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<NilaiHeaderDto>> listPaging(NilaiHeaderDto obj) async {
    try {
      Iterable list = await httpPost("api/NilaiHeader/ListPaging", obj);
      List<NilaiHeaderDto> lst =
          list.map((model) => NilaiHeaderDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<NilaiHeaderDto>> reportNilai(NilaiHeaderDto obj) async {
    try {
      Iterable list = await httpPost("api/NilaiHeader/ReportNilai", obj);
      List<NilaiHeaderDto> lst =
          list.map((model) => NilaiHeaderDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }
}
