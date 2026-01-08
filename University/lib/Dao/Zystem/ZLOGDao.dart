
import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZLOGDto.dart';

class ZLOGDao extends BaseDao {
  Future<ZLOGDto> Save(ZLOGDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZLOG/Save", obj);
      return ZLOGDto.fromJson(dto);
      //return dto;
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> SubmitHeader(ZLOGDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZLOG/SubmitHeader", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> SaveHeader(ZLOGDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZLOG/SaveHeader", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> Delete(ZLOGDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZLOG/Delete", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }

  Future<ZLOGDto?> oneData(ZLOGDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZLOG/OneData", obj);
      return dto != null ?  ZLOGDto?.fromJson(dto) : null;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZLOGDto>> listPaging(ZLOGDto obj) async {
    try {
      Iterable list = await httpPost("api/ZLOG/listpaging", obj);
      List<ZLOGDto> lst = list.map((model) => ZLOGDto.fromJson(model)).toList();
      //return list.isNotEmpty ? list.map((model) => ZLOGDto.fromJson(model)).toList() : null;
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZLOGDto>> listSearch(ZLOGDto obj) async {
    try {
      Iterable list = await httpPost("api/ZLOG/ListSearch", obj);
      List<ZLOGDto> lst = list.map((model) => ZLOGDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZLOGDto>> listSearchReview(ZLOGDto obj) async {
    try {
      Iterable list = await httpPost("api/ZLOG/ListSearchReview", obj);
      List<ZLOGDto> lst = list.map((model) => ZLOGDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZLOGDto>> listTask(ZLOGDto obj) async {
    try {
      Iterable list = await httpPost("api/ZLOG/ListTask", obj);
      List<ZLOGDto> lst = list.map((model) => ZLOGDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZLOGDto>> listOnGoingProject(ZLOGDto obj) async {
    try {
      Iterable list = await httpPost("api/ZLOG/ListOnGoingProject", obj);
      List<ZLOGDto> lst = list.map((model) => ZLOGDto.fromJson(model)).toList();
      return lst;
    } catch (ex) {
      rethrow;
    }
  }

  Future<String> UpdateDoc(ZLOGDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZLOG/SubmitHeader", obj);
      return dto.toString();
    } catch (ex) {
      rethrow;
    }
  }
}
