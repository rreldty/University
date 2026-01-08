class DDLDto{
  String Code = "";
  String Dscr = "";
  String Group = "";

  DDLDto({
    this.Code = "",
    this.Dscr = "",
    this.Group = "",
  });

  factory DDLDto.fromJson(Map<String, dynamic> json) {
    return DDLDto(
      Code: json["CODE"] ?? "",
      Dscr: json["DSCR"] ?? "",
    );
  }

  Map toMap() {
    var map = Map<String, dynamic>();
    map["CODE"] = Code;
    map["DSCR"] = Dscr;

    return map;
  }
}
