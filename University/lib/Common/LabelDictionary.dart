import 'dart:convert';

import '../../Common/CommonMethod.dart';
import '../../Dto/Base/GlobalDto.dart';
import '../../Dto/Zystem/ZDICDto.dart';

class LabelDictionary{
  static getLabelDictionary(String strLabel,{String dictionaryType = "S"})
  {
    if(strLabel.length >= 4 && strLabel.length <= 6){
      String strNewLabel = CommonMethod.stringRight(strLabel, 4);

      List<ZDICDto> lstDict = [];
      if(GlobalDto.listDictionary.isNotEmpty){
        Iterable list = json.decode(GlobalDto.listDictionary);
        lstDict = list.map((e) => ZDICDto.fromJson(e)).toList();
      }

      //print("label-start: $strLabel; $dictionaryType; " + strLabel.length.toString() + "; " + strNewLabel);
      if(lstDict.isNotEmpty)
      {
        List<ZDICDto> lstDicFiltered = lstDict.where((el) => el.ZDFIEL == strNewLabel).where((el) => el.ZDDITY == dictionaryType).toList();
        if(lstDicFiltered.isNotEmpty)
        {
          strLabel = lstDicFiltered[0].ZDLABL;
        }
      }
    }
    //print("label-end: $strLabel");
    return strLabel;
  }
}