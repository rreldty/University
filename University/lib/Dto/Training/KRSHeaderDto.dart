import 'package:university/Dto/Training/KRSDetailDto.dart';

class KRSHeaderDto {
  String nim = "";
  String semester = "";
  String kode_fakultas = "";
  String kode_jurusan = "";
  double total_sks = 0;
  double record_status = 0;
  String nama_fakultas = "";
  String nama_jurusan = "";

  bool isSelected = false;
  double PageNumber = 0;
  double PageSize = 0;
  double TotalPage = 0;
  double TotalRecord = 0;

  List<KRSDetailDto> Details = [];
  KRSDetailDto? objLine;

  KRSHeaderDto({
    this.nim = "",
    this.semester = "",
    this.kode_fakultas = "",
    this.kode_jurusan = "",
    this.total_sks = 0,
    this.record_status = 0,
    this.nama_fakultas = "",
    this.nama_jurusan = "",
    this.isSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
    List<KRSDetailDto>? Details,
    this.objLine,
  }) : Details = Details ?? [];

  factory KRSHeaderDto.fromJson(Map<String, dynamic> json) {
    List<KRSDetailDto> detailList = [];
    if (json["Details"] != null) {
      detailList = (json["Details"] as List)
          .map((e) => KRSDetailDto.fromJson(e))
          .toList();
    }

    return KRSHeaderDto(
      nim: json["nim"] ?? "",
      semester: json["semester"] ?? "",
      kode_fakultas: json["kode_fakultas"] ?? "",
      kode_jurusan: json["kode_jurusan"] ?? "",
      total_sks: (json["total_sks"] ?? 0).toDouble(),
      record_status: (json["record_status"] ?? 0).toDouble(),
      nama_fakultas: json["nama_fakultas"] ?? "",
      nama_jurusan: json["nama_jurusan"] ?? "",
      isSelected: json["isSelected"] ?? false,
      PageNumber: (json["PageNumber"] ?? 0).toDouble(),
      PageSize: (json["PageSize"] ?? 0).toDouble(),
      TotalPage: (json["TotalPage"] ?? 0).toDouble(),
      TotalRecord: (json["TotalRecord"] ?? 0).toDouble(),
      Details: detailList,
      objLine: json["objLine"] != null
          ? KRSDetailDto.fromJson(json["objLine"])
          : null,
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["nim"] = nim;
    map["semester"] = semester;
    map["kode_fakultas"] = kode_fakultas;
    map["kode_jurusan"] = kode_jurusan;
    map["total_sks"] = total_sks;
    map["record_status"] = record_status;
    map["nama_fakultas"] = nama_fakultas;
    map["nama_jurusan"] = nama_jurusan;
    map["isSelected"] = isSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    map["Details"] = Details.map((e) => e.toMap()).toList();
    map["objLine"] = objLine?.toMap();
    return map;
  }
}
