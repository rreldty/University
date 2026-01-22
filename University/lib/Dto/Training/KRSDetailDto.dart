class KrsDetailDto{
  String nim = "";
  double semester = 0;
  double line_no = 0;
  String kode_matakuliah = "";
  double sks = 0;
  String nama_matakuliah = "";
  String kode_jurusan = "";

  bool IsSelected = false;
  double PageNumber = 0;
  double PageSize = 0;
  double TotalPage = 0;
  double TotalRecord = 0;

  KrsDetailDto({
    this.nim = "",
    this.semester = 0,
    this.line_no = 0,
    this.kode_matakuliah = "",
    this.sks = 0,
    this.nama_matakuliah = "",
    this.kode_jurusan = "",

    this.IsSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
  });

  factory KrsDetailDto.fromJson(Map<String, dynamic> json) {
    return KrsDetailDto(
      nim: json["nim"] ?? "",
      semester: json["semester"] ?? 0,
      line_no: json["line_no"] ?? 0,
      kode_matakuliah: json["kode_matakuliah"] ?? "",
      sks: json["sks"] ?? 0,
      nama_matakuliah: json["nama_matakuliah"] ?? "",
      kode_jurusan: json["kode_jurusan"] ?? "",

      IsSelected: json["IsSelected"] ?? false,
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["nim"] = nim ?? "";
    map["semester"] = semester ?? 0;
    map["line_no"] = line_no ?? 0;
    map["kode_matakuliah"] = kode_matakuliah ?? "";
    map["sks"] = sks ?? 0;
    map["nama_matakuliah"] = nama_matakuliah ?? "";
    map["kode_jurusan"] = kode_jurusan ?? "";

    map["IsSelected"] = IsSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    return map;
  }
}