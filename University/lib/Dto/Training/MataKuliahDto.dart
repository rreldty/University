class MataKuliahDto {
  String kode_fakultas = "";
  String kode_jurusan = "";
  String kode_matakuliah = "";
  String nama_matakuliah = "";
  double sks = 0;
  double record_status = 0;

  bool isSelected = false;
  double PageNumber = 0;
  double PageSize = 0;
  double TotalPage = 0;
  double TotalRecord = 0;

  MataKuliahDto({
    this.kode_fakultas = "",
    this.kode_jurusan = "",
    this.kode_matakuliah = "",
    this.nama_matakuliah = "",
    this.sks = 0,
    this.record_status = 0,
    this.isSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
  });

  factory MataKuliahDto.fromJson(Map<String, dynamic> json) {
    return MataKuliahDto(
      kode_fakultas: json["kode_fakultas"] ?? "",
      kode_jurusan: json["kode_jurusan"] ?? "",
      kode_matakuliah: json["kode_matakuliah"] ?? "",
      nama_matakuliah: json["nama_matakuliah"] ?? "",
      sks: (json["sks"] ?? 0).toDouble(),
      record_status: (json["record_status"] ?? 0).toDouble(),
      isSelected: json["isSelected"] ?? false,
      PageNumber: (json["PageNumber"] ?? 0).toDouble(),
      PageSize: (json["PageSize"] ?? 0).toDouble(),
      TotalPage: (json["TotalPage"] ?? 0).toDouble(),
      TotalRecord: (json["TotalRecord"] ?? 0).toDouble(),
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["kode_fakultas"] = kode_fakultas;
    map["kode_jurusan"] = kode_jurusan;
    map["kode_matakuliah"] = kode_matakuliah;
    map["nama_matakuliah"] = nama_matakuliah;
    map["sks"] = sks;
    map["record_status"] = record_status;
    map["isSelected"] = isSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    return map;
  }
}
