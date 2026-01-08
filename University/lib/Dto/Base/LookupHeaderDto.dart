class LookupHeaderDto{
  String HeaderName = "";
  String HeaderType = "";
  String HeaderFormat = "";
  String HeaderWidth = "";

  LookupHeaderDto({
    this.HeaderName = "",
    this.HeaderType = "",
    this.HeaderFormat = "",
    this.HeaderWidth = ""
  });

  factory LookupHeaderDto.fromJson(Map<String, dynamic> json) {
    return LookupHeaderDto(
      HeaderName: json["HeaderName"] ?? "",
      HeaderType: json["HeaderType"] ?? "",
      HeaderFormat: json["HeaderFormat"] ?? "",
      HeaderWidth: json["HeaderWidth"] ?? "",
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["HeaderName"] = HeaderName;
    map["HeaderType"] = HeaderType;
    map["HeaderFormat"] = HeaderFormat;
    map["HeaderWidth"] = HeaderWidth;

    return map;
  }
}
