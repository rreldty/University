import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZUG1Dto.dart';

class ZUG1Dao extends BaseDao {
  Future<String> Save(ZUG1Dto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUG1/Save", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }
  Future<String> SaveWithLine(ZUG1Dto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUG1/SaveWithLine", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }


  Future<String> SaveDelete(ZUG1Dto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUG1/SaveDelete", obj);
      return dto.toString();
    } catch (ex) {
      throw ex;
    }
  }

  Future<ZUG1Dto?> oneData(ZUG1Dto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUG1/OneData", obj);
      return dto != null ? ZUG1Dto.fromJson(dto) : null;
    } catch (ex) {
      rethrow;
    }
  }

}

