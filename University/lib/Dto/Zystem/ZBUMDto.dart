
class ZBUMDto
{
  String ZVCONO = "";
  String ZVBRNO = "";
  String ZVUSNO = "";
  String ZVREMA = "";
  String ZVSYST = "";
  String ZVSTAT = "";
  double ZVRCST = 0;
  double ZVCRDT = 0;
  double ZVCRTM = 0;
  String ZVCRUS = "";
  double ZVCHDT = 0;
  double ZVCHTM = 0;
  String ZVCHUS = "";

  ZBUMDto({
    this.ZVCONO = "",
    this.ZVBRNO = "",
    this.ZVUSNO = "",
    this.ZVREMA = "",
    this.ZVSYST = "",
    this.ZVSTAT = "",
    this.ZVRCST = 0,
    this.ZVCRDT = 0,
    this.ZVCRTM = 0,
    this.ZVCRUS = "",
    this.ZVCHDT = 0,
    this.ZVCHTM = 0,
    this.ZVCHUS = "",
  });

  factory ZBUMDto.fromJson(Map<String, dynamic> json) {
    return ZBUMDto(
      ZVCONO: json["ZVCONO"] ?? "",
      ZVBRNO: json["ZVBRNO"] ?? "",
      ZVUSNO: json["ZVUSNO"] ?? "",
      ZVREMA: json["ZVREMA"] ?? "",
      ZVSYST: json["ZVSYST"] ?? "",
      ZVSTAT: json["ZVSTAT"] ?? "",
      ZVRCST: json["ZVRCST"] ?? 0,
      ZVCRDT: json["ZVCRDT"] ?? 0,
      ZVCRTM: json["ZVCRTM"] ?? 0,
      ZVCRUS: json["ZVCRUS"] ?? "",
      ZVCHDT: json["ZVCHDT"] ?? 0,
      ZVCHTM: json["ZVCHTM"] ?? 0,
      ZVCHUS: json["ZVCHUS"] ?? "",
    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZVCONO"] = ZVCONO;
    map["ZVBRNO"] = ZVBRNO;
    map["ZVUSNO"] = ZVUSNO;
    map["ZVREMA"] = ZVREMA;
    map["ZVSYST"] = ZVSYST;
    map["ZVSTAT"] = ZVSTAT;
    map["ZVRCST"] = ZVRCST;
    map["ZVCRDT"] = ZVCRDT;
    map["ZVCRTM"] = ZVCRTM;
    map["ZVCRUS"] = ZVCRUS;
    map["ZVCHDT"] = ZVCHDT;
    map["ZVCHTM"] = ZVCHTM;
    map["ZVCHUS"] = ZVCHUS;
    return map;
  }
}
