import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZUSRDto.dart';

class ZUSRDao extends BaseDao {
  Future<String> Save(ZUSRDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUSR/Save", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<ZUSRDto?> oneData(ZUSRDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUSR/OneData", obj);
      return dto != null ?  ZUSRDto?.fromJson(dto) : null;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZUSRDto>> list(ZUSRDto obj) async {
    try {
      Iterable list = await httpPost("api/ZUSR/List", obj);
      List<ZUSRDto> lst = list.map((model) => ZUSRDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZUSRDto>> listPaging(ZUSRDto obj) async {
    try {
      Iterable list = await httpPost("api/ZUSR/ListPaging", obj);
      List<ZUSRDto> lst = list.map((model) => ZUSRDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZUSRDto>> listPagingNotInUserGroup(ZUSRDto obj) async {
    try {
      Iterable list = await httpPost("api/ZUSR/ListPagingNotInUserGroup", obj);
      List<ZUSRDto> lst = list.map((model) => ZUSRDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<ZUSRDto> Login(ZUSRDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUSR/Login", obj);
      return ZUSRDto.fromJson(dto);
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> ChangePassword(ZUSRDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUSR/ChangePassword", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZUSRDto>> searchList(ZUSRDto obj) async {
    try {
      Iterable list = await httpPost("api/ZUSR/SearchList", obj);
      List<ZUSRDto> lst = list.map((model) => ZUSRDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> SubmitData(ZUSRDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUSR/SubmitData", obj);
      return dto.toString();
    } catch (ex) {
      throw ex;
    }
  }

  Future<List<ZUSRDto>> ListData(ZUSRDto obj) async {
    try {
      Iterable list = await httpPost("api/ZUSR/ListData", obj);
      List<ZUSRDto> lst = list.map((model) => ZUSRDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZUSRDto>> ListDataPIC(ZUSRDto obj) async {
    try {
      Iterable list = await httpPost("api/ZUSR/ListDataPIC", obj);
      List<ZUSRDto> lst = list.map((model) => ZUSRDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> SaveZUSTAT(ZUSRDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZUSR/SaveZUSTAT", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }
}
