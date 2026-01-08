class ParamDto{
  String Param = "";
  String Value = "";
  String Type = "";
  String Direction = "";

  ParamDto({
    this.Param = "",
    this.Value = "",
    this.Type = "",
    this.Direction = "",
  });

  factory ParamDto.fromJson(Map<String, dynamic> json) {
    return ParamDto(
      Param: json["Param"] ?? "",
      Value: json["Value"] ?? "",
      Type: json["Type"] ?? "",
      Direction: json["Direction"] ?? "",
    );
  }

  Map toMap() {
    var map = Map<String, dynamic>();
    map["Param"] = Param;
    map["Value"] = Value;
    map["Type"] = Type;
    map["Direction"] = Direction;
    return map;
  }
}
