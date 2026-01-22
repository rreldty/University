import 'package:university/Dto/Training/NilaiDetailDto.dart';

class NilaiHeaderDto {
  String nim = "";
  String semester = "";
  String kode_fakultas = "";
  String kode_jurusan = "";
  double nilai = 0;
  double record_status = 0;
  String nama_fakultas = "";
  String nama_jurusan = "";
  String nama_mahasiswa = "";

  bool isSelected = false;
  int PageNumber = 0;
  int PageSize = 0;
  int TotalPage = 0;
  int TotalRecord = 0;

  List<NilaiDetailDto> Details = [];
  NilaiDetailDto? objLine;

  NilaiHeaderDto({
    this.nim = "",
    this.semester = "",
    this.kode_fakultas = "",
    this.kode_jurusan = "",
    this.nilai = 0,
    this.record_status = 0,
    this.nama_fakultas = "",
    this.nama_jurusan = "",
    this.nama_mahasiswa = "",
    this.isSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
    List<NilaiDetailDto>? Details,
    this.objLine,
  }) : Details = Details ?? [];

  factory NilaiHeaderDto.fromJson(Map<String, dynamic> json) {
    List<NilaiDetailDto> detailList = [];
    if (json["Details"] != null) {
      detailList = (json["Details"] as List)
          .map((e) => NilaiDetailDto.fromJson(e))
          .toList();
    }

    return NilaiHeaderDto(
      nim: json["nim"] ?? "",
      semester: json["semester"] ?? "",
      kode_fakultas: json["kode_fakultas"] ?? "",
      kode_jurusan: json["kode_jurusan"] ?? "",
      nilai: (json["nilai"] ?? 0).toDouble(),
      record_status: (json["record_status"] ?? 0).toDouble(),
      nama_fakultas: json["nama_fakultas"] ?? "",
      nama_jurusan: json["nama_jurusan"] ?? "",
      nama_mahasiswa: json["nama_mahasiswa"] ?? "",
      isSelected: json["isSelected"] ?? false,
      PageNumber: (json["PageNumber"] ?? 0).toInt(),
      PageSize: (json["PageSize"] ?? 0).toInt(),
      TotalPage: (json["TotalPage"] ?? 0).toInt(),
      TotalRecord: (json["TotalRecord"] ?? 0).toInt(),
      Details: detailList,
      objLine: json["objLine"] != null
          ? NilaiDetailDto.fromJson(json["objLine"])
          : null,
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["nim"] = nim;
    map["semester"] = semester;
    map["kode_fakultas"] = kode_fakultas;
    map["kode_jurusan"] = kode_jurusan;
    map["nilai"] = nilai;
    map["record_status"] = record_status;
    map["nama_fakultas"] = nama_fakultas;
    map["nama_jurusan"] = nama_jurusan;
    map["nama_mahasiswa"] = nama_mahasiswa;
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
