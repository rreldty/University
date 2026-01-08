class XampleDto{
  String SSCONO = "";
  String SSBRNO = "";
  String SSSNDN = "";
  double SSSNLN = 0;
  String SSIVDN = "";
  double SSIVLN = 0;
  String SSWHNO = "";
  String SSLONO = "";
  String SSITNO = "";
  String SSSLUM = "";
  String SSFTQT = "";
  double SSSNQT = 0;
  double SSACQT = 0;
  double SSITPR = 0;
  double SSGSAM = 0;
  double SSDCAM = 0;
  double SSNTAM = 0;
  double SSSLVP = 0;
  double SSSLVT = 0;
  double SSSNAM = 0;
  String SSRSNO = "";
  String SSREMA = "";
  String SSDCST = "";
  String SSSYST = "";
  String SSSTAT = "";
  double SSRCST = 0;
  double SSCRDT = 0;
  double SSCRTM = 0;
  String SSCRUS = "";
  double SSCHDT = 0;
  double SSCHTM = 0;
  String SSCHUS = "";
  XampleDto? objSSN2;
  List<XampleDto>? listSSN2;
  double PageNumber = 0;
  double PageSize = 0;
  double TotalPage = 0;
  double TotalRecord = 0;
  String HMITNA = "";
  double SELISIH = 0;
  String PHOTO1 = "";

  XampleDto({
    this.SSCONO = "",
    this.SSBRNO = "",
    this.SSSNDN = "",
    this.SSSNLN = 0,
    this.SSIVDN = "",
    this.SSIVLN = 0,
    this.SSWHNO = "",
    this.SSLONO = "",
    this.SSITNO = "",
    this.SSSLUM = "",
    this.SSFTQT = "",
    this.SSSNQT = 0,
    this.SSACQT = 0,
    this.SSITPR = 0,
    this.SSGSAM = 0,
    this.SSDCAM = 0,
    this.SSNTAM = 0,
    this.SSSLVP = 0,
    this.SSSLVT = 0,
    this.SSSNAM = 0,
    this.SSRSNO = "",
    this.SSREMA = "",
    this.SSDCST = "",
    this.SSSYST = "",
    this.SSSTAT = "",
    this.SSRCST = 0,
    this.SSCRDT = 0,
    this.SSCRTM = 0,
    this.SSCRUS = "",
    this.SSCHDT = 0,
    this.SSCHTM = 0,
    this.SSCHUS = "",
    this.objSSN2,
    this.listSSN2,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
    this.HMITNA = "",
    this.SELISIH = 0,
    this.PHOTO1 = "",
  });

  factory XampleDto.fromJson(Map<String, dynamic> json) {
    return XampleDto(
      SSCONO: json["SSCONO"] ?? "",
      SSBRNO: json["SSBRNO"] ?? "",
      SSSNDN: json["SSSNDN"] ?? "",
      SSSNLN: json["SSSNLN"] ?? 0,
      SSIVDN: json["SSIVDN"] ?? "",
      SSIVLN: json["SSIVLN"] ?? 0,
      SSWHNO: json["SSWHNO"] ?? "",
      SSLONO: json["SSLONO"] ?? "",
      SSITNO: json["SSITNO"] ?? "",
      SSSLUM: json["SSSLUM"] ?? "",
      SSFTQT: json["SSFTQT"] ?? "",
      SSSNQT: json["SSSNQT"] ?? 0,
      SSACQT: json["SSACQT"] ?? 0,
      SSITPR: json["SSITPR"] ?? 0,
      SSGSAM: json["SSGSAM"] ?? 0,
      SSDCAM: json["SSDCAM"] ?? 0,
      SSNTAM: json["SSNTAM"] ?? 0,
      SSSLVP: json["SSSLVP"] ?? 0,
      SSSLVT: json["SSSLVT"] ?? 0,
      SSSNAM: json["SSSNAM"] ?? 0,
      SSRSNO: json["SSRSNO"] ?? "",
      SSREMA: json["SSREMA"] ?? "",
      SSDCST: json["SSDCST"] ?? "",
      SSSYST: json["SSSYST"] ?? "",
      SSSTAT: json["SSSTAT"] ?? "",
      SSRCST: json["SSRCST"] ?? 0,
      SSCRDT: json["SSCRDT"] ?? 0,
      SSCRTM: json["SSCRTM"] ?? 0,
      SSCRUS: json["SSCRUS"] ?? "",
      SSCHDT: json["SSCHDT"] ?? 0,
      SSCHTM: json["SSCHTM"] ?? 0,
      SSCHUS: json["SSCHUS"] ?? "",
      objSSN2: json["objSSN2"] != null ? XampleDto.fromJson(json["objSSN2"]) : null,
      listSSN2:json["listSSN2"] != null ? (json["listSSN2"] as List).map((e) => XampleDto.fromJson(e)).toList() : null,
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
      HMITNA: json["HMITNA"] ?? "",
      SELISIH: json["SELISIH"] ?? 0,
      PHOTO1: json["PHOTO1"] ?? "",
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["SSCONO"] = SSCONO;
    map["SSBRNO"] = SSBRNO;
    map["SSSNDN"] = SSSNDN;
    map["SSSNLN"] = SSSNLN;
    map["SSIVDN"] = SSIVDN;
    map["SSIVLN"] = SSIVLN;
    map["SSWHNO"] = SSWHNO;
    map["SSLONO"] = SSLONO;
    map["SSITNO"] = SSITNO;
    map["SSSLUM"] = SSSLUM;
    map["SSFTQT"] = SSFTQT;
    map["SSSNQT"] = SSSNQT;
    map["SSACQT"] = SSACQT;
    map["SSITPR"] = SSITPR;
    map["SSGSAM"] = SSGSAM;
    map["SSDCAM"] = SSDCAM;
    map["SSNTAM"] = SSNTAM;
    map["SSSLVP"] = SSSLVP;
    map["SSSLVT"] = SSSLVT;
    map["SSSNAM"] = SSSNAM;
    map["SSRSNO"] = SSRSNO;
    map["SSREMA"] = SSREMA;
    map["SSDCST"] = SSDCST;
    map["SSSYST"] = SSSYST;
    map["SSSTAT"] = SSSTAT;
    map["SSRCST"] = SSRCST;
    map["SSCRDT"] = SSCRDT;
    map["SSCRTM"] = SSCRTM;
    map["SSCRUS"] = SSCRUS;
    map["SSCHDT"] = SSCHDT;
    map["SSCHTM"] = SSCHTM;
    map["SSCHUS"] = SSCHUS;
    map["objSSN2"] = objSSN2 != null ? objSSN2!.toMap() : null;
    map["listSSN2"] = listSSN2 != null ? listSSN2!.map((e) => e.toMap()).toList() : null;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    map["HMITNA"] = HMITNA;
    map["SELISIH"] = SELISIH;
    map["PHOTO1"] = PHOTO1;
    return map;
  }
}