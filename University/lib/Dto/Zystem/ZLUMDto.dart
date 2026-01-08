
class ZLUMDto
{
  String ZNCONO = "";
  String ZNBRNO = "";
  String ZNAPNO = "";
  String ZNMENO = "";
  String ZNUSNO = "";
  double ZNLGDT = 0;
  double ZNLGTM = 0;
  String ZNREMA = "";
  String ZNSYST = "";
  String ZNSTAT = "";
  double ZNRCST = 0;
  double ZNCRDT = 0;
  double ZNCRTM = 0;
  String ZNCRUS = "";
  double ZNCHDT = 0;
  double ZNCHTM = 0;
  String ZNCHUS = "";

  ZLUMDto({
    this.ZNCONO = "",
    this.ZNBRNO = "",
    this.ZNAPNO = "",
    this.ZNMENO = "",
    this.ZNUSNO = "",
    this.ZNLGDT = 0,
    this.ZNLGTM = 0,
    this.ZNREMA = "",
    this.ZNSYST = "",
    this.ZNSTAT = "",
    this.ZNRCST = 0,
    this.ZNCRDT = 0,
    this.ZNCRTM = 0,
    this.ZNCRUS = "",
    this.ZNCHDT = 0,
    this.ZNCHTM = 0,
    this.ZNCHUS = "",
  });

  factory ZLUMDto.fromJson(Map<String, dynamic> json) {
    return ZLUMDto(
      ZNCONO: json["ZNCONO"] ?? "",
      ZNBRNO: json["ZNBRNO"] ?? "",
      ZNAPNO: json["ZNAPNO"] ?? "",
      ZNMENO: json["ZNMENO"] ?? "",
      ZNUSNO: json["ZNUSNO"] ?? "",
      ZNLGDT: json["ZNLGDT"] ?? 0,
      ZNLGTM: json["ZNLGTM"] ?? 0,
      ZNREMA: json["ZNREMA"] ?? "",
      ZNSYST: json["ZNSYST"] ?? "",
      ZNSTAT: json["ZNSTAT"] ?? "",
      ZNRCST: json["ZNRCST"] ?? 0,
      ZNCRDT: json["ZNCRDT"] ?? 0,
      ZNCRTM: json["ZNCRTM"] ?? 0,
      ZNCRUS: json["ZNCRUS"] ?? "",
      ZNCHDT: json["ZNCHDT"] ?? 0,
      ZNCHTM: json["ZNCHTM"] ?? 0,
      ZNCHUS: json["ZNCHUS"] ?? "",
    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZNCONO"] = ZNCONO;
    map["ZNBRNO"] = ZNBRNO;
    map["ZNAPNO"] = ZNAPNO;
    map["ZNMENO"] = ZNMENO;
    map["ZNUSNO"] = ZNUSNO;
    map["ZNLGDT"] = ZNLGDT;
    map["ZNLGTM"] = ZNLGTM;
    map["ZNREMA"] = ZNREMA;
    map["ZNSYST"] = ZNSYST;
    map["ZNSTAT"] = ZNSTAT;
    map["ZNRCST"] = ZNRCST;
    map["ZNCRDT"] = ZNCRDT;
    map["ZNCRTM"] = ZNCRTM;
    map["ZNCRUS"] = ZNCRUS;
    map["ZNCHDT"] = ZNCHDT;
    map["ZNCHTM"] = ZNCHTM;
    map["ZNCHUS"] = ZNCHUS;
    return map;
  }
}
