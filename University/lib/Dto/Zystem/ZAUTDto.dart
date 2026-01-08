
import 'ZMNUDto.dart';

class ZAUTDto
{
  //Properties
  String ZTCONO = "";
  String ZTBRNO = "";
  String ZTUGNO = "";
  String ZTAPNO = "";
  String ZTMENO = "";
  String ZTRIGH = "";
  String ZTSYST = "";
  String ZTSTAT = "";
  double ZTRCST = 0;
  double ZTCRDT = 0;
  double ZTCRTM = 0;
  String ZTCRUS = "";
  double ZTCHDT = 0;
  double ZTCHTM = 0;
  String ZTCHUS = "";

  //Custom Properties
  String ZHUSNO = "";
  String ZPPURL = "";
  String ZMMENA = "";
  String ZMMEPA = "";
  String ZMPARM = "";
  String ZMIURL = "";

  String ZAAPNA = "";
  String ZAAURL = "";
  String ZAIURL = "";
  String ZAACLR = "";
  double ZAAPSQ = 0;
  String ZAREMA = "";

  List<ZMNUDto>? lstZMNU;
  List<ZAUTDto>? lstZAUT;

  int PageNumber = 0;
  int PageSize = 0;
  int TotalPage = 0;
  int TotalRecord = 0;
  bool IsSelected = false;
  String SqlFilter = "";
  String SqlSort = "";

  ZAUTDto({
    this.ZTCONO = "",
    this.ZTBRNO = "",
    this.ZTUGNO = "",
    this.ZTAPNO = "",
    this.ZTMENO = "",
    this.ZTRIGH = "",
    this.ZTSYST = "",
    this.ZTSTAT = "",
    this.ZTRCST = 0,
    this.ZTCRDT = 0,
    this.ZTCRTM = 0,
    this.ZTCRUS = "",
    this.ZTCHDT = 0,
    this.ZTCHTM = 0,
    this.ZTCHUS = "",
    this.ZHUSNO = "",
    this.ZPPURL = "",
    this.ZMMENA = "",
    this.ZMMEPA = "",
    this.ZMPARM = "",
    this.ZMIURL = "",
    this.ZAAPNA = "",
    this.ZAAURL = "",
    this.ZAIURL = "",
    this.ZAACLR = "",
    this.ZAAPSQ = 0,
    this.ZAREMA = "",
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
    this.IsSelected = false,
    this.SqlFilter = "",
    this.SqlSort = "",
    this.lstZMNU,
    this.lstZAUT,
  });

  factory ZAUTDto.fromJson(Map<String, dynamic> json) {
    return ZAUTDto(
      ZTCONO: json["ZTCONO"] ?? "",
      ZTBRNO: json["ZTBRNO"] ?? "",
      ZTUGNO: json["ZTUGNO"] ?? "",
      ZTAPNO: json["ZTAPNO"] ?? "",
      ZTMENO: json["ZTMENO"] ?? "",
      ZTRIGH: json["ZTRIGH"] ?? "",
      ZTSYST: json["ZTSYST"] ?? "",
      ZTSTAT: json["ZTSTAT"] ?? "",
      ZTRCST: json["ZTRCST"] ?? 0,
      ZTCRDT: json["ZTCRDT"] ?? 0,
      ZTCRTM: json["ZTCRTM"] ?? 0,
      ZTCRUS: json["ZTCRUS"] ?? "",
      ZTCHDT: json["ZTCHDT"] ?? 0,
      ZTCHTM: json["ZTCHTM"] ?? 0,
      ZTCHUS: json["ZTCHUS"] ?? "",
      ZHUSNO: json["ZHUSNO"] ?? "",
      ZPPURL: json["ZPPURL"] ?? "",
      ZMMENA: json["ZMMENA"] ?? "",
      ZMMEPA: json["ZMMEPA"] ?? "",
      ZMPARM: json["ZMPARM"] ?? "",
      ZMIURL: json["ZMIURL"] ?? "",
      ZAAPNA: json["ZAAPNA"] ?? "",
      ZAAURL: json["ZAAURL"] ?? "",
      ZAIURL: json["ZAIURL"] ?? "",
      ZAACLR: json["ZAACLR"] ?? "",
      ZAAPSQ: json["ZAAPSQ"] ?? 0,
      ZAREMA: json["ZAREMA"] ?? "",
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
      IsSelected: json["IsSelected"] ?? false,
      SqlFilter: json["SqlFilter"] ?? "",
      SqlSort: json["SqlSort"] ?? "",
      lstZMNU:json["lstZMNU"] != null ? (json["lstZMNU"] as List).map((e) => ZMNUDto.fromJson(e)).toList() : null,
      lstZAUT:json["lstZAUT"] != null ? (json["lstZAUT"] as List).map((e) => ZAUTDto.fromJson(e)).toList() : null,
    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZTCONO"] = ZTCONO;
    map["ZTBRNO"] = ZTBRNO;
    map["ZTUGNO"] = ZTUGNO;
    map["ZTAPNO"] = ZTAPNO;
    map["ZTMENO"] = ZTMENO;
    map["ZTRIGH"] = ZTRIGH;
    map["ZTSYST"] = ZTSYST;
    map["ZTSTAT"] = ZTSTAT;
    map["ZTRCST"] = ZTRCST;
    map["ZTCRDT"] = ZTCRDT;
    map["ZTCRTM"] = ZTCRTM;
    map["ZTCRUS"] = ZTCRUS;
    map["ZTCHDT"] = ZTCHDT;
    map["ZTCHTM"] = ZTCHTM;
    map["ZTCHUS"] = ZTCHUS;
    map["ZHUSNO"] = ZHUSNO;
    map["ZPPURL"] = ZPPURL;
    map["ZMMENA"] = ZMMENA;
    map["ZMMEPA"] = ZMMEPA;
    map["ZMPARM"] = ZMPARM;
    map["ZMIURL"] = ZMIURL;
    map["ZAAPNA"] = ZAAPNA;
    map["ZAAURL"] = ZAAURL;
    map["ZAIURL"] = ZAIURL;
    map["ZAACLR"] = ZAACLR;
    map["ZAAPSQ"] = ZAAPSQ;
    map["ZAREMA"] = ZAREMA;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    map["IsSelected"] = IsSelected;
    map["SqlFilter"] = SqlFilter;
    map["SqlSort"] = SqlSort;
    map["lstZMNU"] = lstZMNU != null ? lstZMNU!.map((e) => e.toMap()).toList() : null;
    map["lstZAUT"] = lstZAUT != null ? lstZAUT!.map((e) => e.toMap()).toList() : null;
    return map;
  }
}
