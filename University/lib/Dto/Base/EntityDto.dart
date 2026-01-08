
import 'ParamDto.dart';

class EntityDto{
  String Entity;
  String Filter;
  String Sort;
  String KeyCode;
  String FilterMultiselect;
  int PageNum;
  int PageSize;
  List<String>? SearchBys;
  List<String>? Operators;
  List<String>? SearchKeys;
  List<ParamDto>? Parameters;
  String FileName;
  String FileType;

  EntityDto({
    this.Entity = "",
    this.Filter = "",
    this.Sort = "",
    this.KeyCode = "",
    this.FilterMultiselect = "",
    this.PageNum = 0,
    this.PageSize = 20,
    this.SearchBys,
    this.Operators,
    this.SearchKeys,
    this.Parameters,
    this.FileName = "",
    this.FileType = ""
  });

  factory EntityDto.fromJson(Map<String, dynamic> json) {
    return EntityDto(
      Entity: json["Entity"] ?? "",
      Filter: json["Filter"] ?? "",
      Sort: json["Sort"] ?? "",
      KeyCode: json["KeyCode"] ?? "",
      FilterMultiselect: json["FilterMultiselect"] ?? "",
      PageNum: json["PageNum"] ?? 0,
      PageSize: json["PageSize"] ?? 20,
      SearchBys: json["SearchBys"] != null ? json["SearchBys"] as List<String> : null,
      Operators: json["Operators"] != null ? json["Operators"] as List<String> : null,
      SearchKeys: json["SearchKeys"] != null ? json["SearchKeys"] as List<String> : null,
      Parameters: json["Parameters"] != null ? (json["Parameters"] as List).map((e) => ParamDto.fromJson(e)).toList() : null,
      FileName: json["FileName"] ?? "",
      FileType: json["FileType"] ?? ""
    );
  }

  Map toMap() {
    Map<String, dynamic> map = {};
    map["Entity"] = Entity;
    map["Filter"] = Filter;
    map["Sort"] = Sort;
    map["KeyCode"] = KeyCode;
    map["FilterMultiselect"] = FilterMultiselect;
    map["PageNum"] = PageNum;
    map["PageSize"] = PageSize;
    map["SearchBys"] = SearchBys;
    map["Operators"] = Operators;
    map["SearchKeys"] = SearchKeys;
    map["Parameters"] = Parameters?.map((e) => e.toMap()).toList();
    map["FileName"] = FileName;
    map["FileType"] = FileType;

    return map;
  }
}
