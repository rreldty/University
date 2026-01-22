import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Training/NilaiDetailDto.dart';

class NilaiDetailDao extends BaseDao {
  Future<List<NilaiDetailDto>> getList(NilaiDetailDto obj) async {
    try {
      Iterable list = await httpPost("api/NilaiDetail/GetList", obj);
      List<NilaiDetailDto> lst =
          list.map((model) => NilaiDetailDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<NilaiDetailDto>> listPaging(NilaiDetailDto obj) async {
    return getList(obj);
  }
}
