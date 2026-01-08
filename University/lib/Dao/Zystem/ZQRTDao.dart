import 'dart:typed_data';

import '../../Dao/Base/BaseDao.dart';
import '../../Dto/Zystem/ZQRTDto.dart';

class ZQRTDao extends BaseDao {

  Future<ZQRTDto> Save(ZQRTDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZQRT/Save", obj);
      return ZQRTDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<ZQRTDto?> oneData(ZQRTDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZQRT/OneData",obj);
      return dto != null ?  ZQRTDto?.fromJson(dto) : null;
    } catch(ex){
      rethrow;
    }
  }

  Future<ZQRTDto> getOne(ZQRTDto obj) async {
    try{
      print("get dao");
      dynamic dto = await httpPost("api/ZQRT/GetOne",obj);
      return  ZQRTDto.fromJson(dto);
    } catch(ex){
      rethrow;
    }
  }

  Future<List<dynamic>?> ListQueryTools(ZQRTDto obj) async {
    try{
      List<dynamic>? list = await httpPost("api/ZQRT/ListQueryTools",obj);
      print(list);
      return list;
    } catch(ex){
      rethrow;
    }
  }

  Future<String> DownloadQueryTools(ZQRTDto obj) async {
    try {
      dynamic dto = await httpPost("api/ZQRT/DownloadQueryTools", obj);
      return dto.toString();
    } catch (ex) {
      throw ex;
    }
  }

  Future<Uint8List> Download(ZQRTDto obj) async {
    try {
      Uint8List list = await httpDownloadFile("api/ZQRT/Download", obj);
      return list;
    } catch (ex) {
      rethrow;
    }
  }

  Future<List<ZQRTDto>> listPaging(ZQRTDto obj) async {
    try{
      Iterable list = await httpPost("api/ZQRT/ListPaging", obj);
      List<ZQRTDto> lst = list.map((model) => ZQRTDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZQRTDto>> listUser(ZQRTDto obj) async {
    try{
      Iterable list = await httpPost("api/ZQRT/ListPagingUser", obj);
      List<ZQRTDto> lst = list.map((model) => ZQRTDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<List<ZQRTDto>> ListMenu(ZQRTDto obj) async {
    try{
      Iterable list = await httpPost("api/ZQRT/ListMenu", obj);
      List<ZQRTDto> lst = list.map((model) => ZQRTDto.fromJson(model)).toList();
      return lst;
    } catch(ex){
      rethrow;
    }
  }

  Future<String> addLine(ZQRTDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZQRT/AddLine", obj);
      return dto.toString();
    } catch(ex){
      throw ex;
    }
  }

  Future<String> Delete(ZQRTDto obj) async {
    try{
      dynamic dto = await httpPost("api/ZQRT/Delete", obj);
      return dto.toString();
    } catch(ex){
      throw ex;
    }
  }
  

}