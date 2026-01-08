import 'dart:typed_data';

import 'BaseDao.dart';
import '../../Dto/Base/EntityDto.dart';

class GeneralUploadDao extends BaseDao {
  Future<List<dynamic>?> uploadFile(String strFilename, Uint8List fileBytes, EntityDto? objEntity) async {
    try{
      Map<String, String>? param;

      if(objEntity != null) {
        param = {};
        param["Entity"] = objEntity.Entity;
        param["Filter"] = objEntity.Filter;
        param["FileName"] = objEntity.FileName;
      }

      List<dynamic>? list = await httpUploadFile("api/Base/GeneralUpload", strFilename, fileBytes, dataParam: param);
      return list;
    } catch(ex){
      rethrow;
    }
  }

}

