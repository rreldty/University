import 'LookupHeaderDto.dart';

class LookupDto{
  String Entity = "";
  List<LookupHeaderDto>? Headers;
  List<dynamic>? Rows;
  int TotalPage = 0;
  int TotalRecord = 0;
  String WindowSize = "";

  LookupDto({
    this.Entity = "",
    this.Headers,
    this.Rows,
    this.TotalPage = 0,
    this.TotalRecord = 0,
    this.WindowSize = "",
  });

  factory LookupDto.fromJson(Map<String, dynamic> json) {
    return LookupDto(
      Entity: json["Entity"] ?? "",
      Headers: json["Headers"] != null ? (json["Headers"] as List).map((e) => LookupHeaderDto.fromJson(e)).toList() : null,
      Rows: json["Rows"] != null ? json["Rows"] as List : null,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
      WindowSize: json["WindowSize"] ?? "",
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["Entity"] = Entity;
    map["Headers"] = Headers != null ? Headers!.map((e) => e.toMap()).toList() : null;
    map["Rows"] = Rows;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    map["WindowSize"] = WindowSize;

    return map;
  }
}
