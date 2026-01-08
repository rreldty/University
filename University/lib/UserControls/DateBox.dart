import 'package:intl/intl.dart';
import 'package:mask_text_input_formatter/mask_text_input_formatter.dart';
import '../../Common/AppConfig.dart';
import '../../Common/CommonMethod.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'EditText.dart';

class DateBox extends StatefulWidget {
  final DateController controller;
  final String hintText;
  final String mandatoryText;
  final bool isMandatory;
  final bool? isEnable;
  final bool? isVisible;
  final double width;
  final Function()? onLostFocus;
  final Function(DateTime selectedDate)? onSelected;

  const DateBox({
    super.key,
    required this.controller,
    this.hintText = "",
    this.mandatoryText = "",
    this.width = 0,
    this.isMandatory = false,
    this.isEnable,
    this.isVisible = true,
    this.onLostFocus,
    this.onSelected,
  });

  @override
  createState() => DateBoxState();
}

class DateBoxState extends State<DateBox> {
  final FocusNode focusNode = FocusNode();

  late DateTime dtmFirst;
  late DateTime dtmLast;

  @override
  void initState() {
    dtmFirst = DateTime(1945, 1, 1);
    dtmLast = DateTime(DateTime.now().year + 5, 12, 31);

    widget.controller.isEnable = widget.isEnable ?? true;
    widget.controller.isVisible = widget.isVisible ?? true;

    try {
      dtmFirst =
          CommonMethod.NumericToDate(widget.controller.minimumDateValue ?? 0);
    } catch (e) {
      //do nothing
    }

    try {
      dtmLast =
          CommonMethod.NumericToDate(widget.controller.maximumDateValue ?? 0);
    } catch (e) {
      //do nothing
    }

    focusNode.addListener(() {
      if (!focusNode.hasFocus) {
        //debugPrint("Lost Focus");
        if (widget.onLostFocus != null) {
          widget.onLostFocus!();
        }
      }
    });

    super.initState();
  }

  void btnCalendar_Click() async {
    DateTime dtmInitial = widget.controller.value > 0
        ? CommonMethod.NumericToDate(widget.controller.value)
        : DateTime.now();
    if (dtmInitial.isBefore(dtmFirst)) {
      dtmInitial = dtmFirst;
    }

    if (dtmInitial.isAfter(dtmLast)) {
      dtmInitial = dtmLast;
    }

    DateTime? selected = await showDatePicker(
      context: context,
      initialDate: dtmInitial,
      firstDate: dtmFirst,
      lastDate: dtmLast,
    );

    if (selected != null) {
      widget.controller._edtDate.text =
          CommonMethod.DateToStringFormat(selected);
      if (widget.onSelected != null) {
        widget.onSelected!(selected);
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    if (widget.controller.isVisible) {
      try {
        dtmFirst =
            CommonMethod.NumericToDate(widget.controller.minimumDateValue ?? 0);
      } catch (e) {
        //do nothing
      }

      try {
        dtmLast =
            CommonMethod.NumericToDate(widget.controller.maximumDateValue ?? 0);
      } catch (e) {
        //do nothing
      }

      return Container(
        width: 145,
        padding: const EdgeInsets.all(0),
        margin: const EdgeInsets.all(0),
        child: Stack(
          children: [
            Container(
              margin: const EdgeInsets.only(top: 5),
              width: 145,
              height: 33,
              decoration: BoxDecoration(
                color: widget.controller.isEnable
                    ? GlobalStyle.primaryColor
                    : GlobalStyle.disableColor,
                borderRadius: BorderRadius.circular(5.0),
              ),
            ),
            Container(
              margin: const EdgeInsets.only(top: 5),
              width: 120,
              child: TextFormField(
                  textInputAction: TextInputAction.go,
                  textAlign: TextAlign.start,
                  controller: widget.controller._edtDate,
                  obscureText: false,
                  keyboardType: TextInputType.text,
                  enabled: widget.controller.isEnable,
                  maxLength: 10,
                  inputFormatters: validateInputFormatter(),
                  validator: validateText,
                  //onFieldSubmitted: widget.whenFieldSubmitted != null ? widget.whenFieldSubmitted : null,
                  focusNode: focusNode,
                  style: TextStyle(
                      fontSize: GlobalStyle.fontSize, color: Colors.black),
                  decoration: InputDecoration(
                    counterText: "",
                    errorStyle: GlobalStyle.errorStyle,
                    prefixIconConstraints:
                        const BoxConstraints(maxHeight: 24, maxWidth: 24),
                    suffixIconConstraints:
                        const BoxConstraints(maxHeight: 24, maxWidth: 24),
                    hoverColor: Colors.transparent,
                    isDense: true,
                    contentPadding: const EdgeInsets.all(10),
                    filled: true,
                    hintStyle: TextStyle(fontSize: GlobalStyle.fontSize),
                    labelStyle: TextStyle(fontSize: GlobalStyle.fontSize),
                    helperStyle: TextStyle(fontSize: GlobalStyle.fontSize),
                    fillColor: widget.controller.isEnable
                        ? Colors.white
                        : GlobalStyle.disableColor,
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(5.0),
                      borderSide: const BorderSide(
                        color: Color(0xffbababa),
                        width: 1.0,
                      ),
                    ),
                    errorBorder: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(5.0),
                      borderSide: BorderSide(
                        color: GlobalStyle.errorColor,
                        width: 1.0,
                      ),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(5.0),
                      borderSide: BorderSide(
                        color: GlobalStyle.primaryColor,
                        width: 1.0,
                      ),
                    ),
                  )),
            ),
            Positioned(
              left: 120,
              child: Container(
                margin: const EdgeInsets.only(top: 5),
                padding:
                    const EdgeInsets.only(left: 0, top: 0, right: 2, bottom: 0),
                child: InkWell(
                  onTap: widget.controller.isEnable ? btnCalendar_Click : null,
                  child: Container(
                    width: 20,
                    height: 32,
                    margin: const EdgeInsets.only(bottom: 0, left: 2, right: 2),
                    child: const Icon(
                      Icons.calendar_today,
                      color: Colors.white,
                      size: 16,
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
      );
    } else {
      return Container();
    }
  }

  List<TextInputFormatter> validateInputFormatter() {
    String strPattern = AppConfig.patternStringDate.replaceAll("d", "#");
    strPattern = strPattern.replaceAll("M", "#");
    strPattern = strPattern.replaceAll("y", "#");

    var maskFormatter = MaskTextInputFormatter(
        mask: strPattern, filter: {"#": RegExp(r"[0-9]")});

    return [LengthLimitingTextInputFormatter(10), maskFormatter];
  }

  String? validateText(String? value) {
    String strValue = value ?? "";
    String strResult = "";

    if (widget.isMandatory && strValue.isEmpty) {
      if (widget.mandatoryText.isNotEmpty) {
        strResult = widget.mandatoryText;
      } else {
        strResult = "Please fill this field";
      }
    }

    if (strResult.isEmpty) {
      if (strValue.isNotEmpty) {
        strResult = validateDate(strValue);
      }
    }

    if (strResult.isEmpty) {
      if (strValue.isNotEmpty) {
        DateTime dt1 = DateFormat(AppConfig.patternStringDate).parse(strValue);
        if (dt1.isBefore(dtmFirst)) {
          strResult =
              "Date can't be smaller than ${CommonMethod.DateTimeToStringFormat(dtmFirst)}";
          debugPrint(strResult);
        }

        // double dblDate = CommonMethod.DateStringToNumeric(strValue);
        // double? dblDateMin = widget.controller.minimumDateValue;
        //
        // if(dblDateMin != null){
        //   if(dblDateMin > dblDate){
        //     strResult = "Date can't be smaller than ${CommonMethod.NumericToDateString(dblDateMin)}" ;
        //   }
        // }
      }
    }

    if (strResult.isEmpty) {
      if (strValue.isNotEmpty) {
        DateTime dt1 = DateFormat(AppConfig.patternStringDate).parse(strValue);
        if (dt1.isAfter(dtmLast)) {
          strResult =
              "Date can't be greater than ${CommonMethod.DateTimeToStringFormat(dtmLast)}";
          debugPrint(strResult);
        }

        // double dblDate = CommonMethod.DateStringToNumeric(strValue);
        // double? dblDateMax = widget.controller.maximumDateValue;
        //
        // if(dblDateMax != null){
        //   if(dblDateMax < dblDate){
        //     strResult = "Date can't be greater than ${CommonMethod.NumericToDateString(dblDateMax)}" ;
        //   }
        // }
      }
    }

    if (strResult.isNotEmpty) {
      return strResult;
    }

    return null;
  }

  String validateDate(String value) {
    Pattern pattern =
        r"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[012])-([0-9][0-9])([0-9][0-9])";
    switch (AppConfig.patternStringDate) {
      case "dd-MM-yyyy":
        {
          pattern =
              r"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[012])-([0-9][0-9])([0-9][0-9])";
          break;
        }
      case "dd MMM yyyy":
        {
          if (AppConfig.Language == "ID") {
            pattern =
                r"^(0[1-9]|[12][0-9]|3[01]) (Jan|Feb|Mar|Apr|Mei|Jun|Jul|Agu|Sep|Okt|Nop|Des) ([0-9][0-9])([0-9][0-9])";
          } else {
            pattern =
                r"^(0[1-9]|[12][0-9]|3[01]) (Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) ([0-9][0-9])([0-9][0-9])";
          }
          break;
        }
      default:
        break;
    }

    RegExp regex = RegExp(pattern.toString());
    if (!regex.hasMatch(value)) {
      return "Date is not valid";
    }

    return "";
  }
}

class DateController {
  final EditTextController _edtDate = EditTextController();
  bool isEnable = true;
  bool isVisible = true;
  double? minimumDateValue;
  double? maximumDateValue;

  double get value {
    if (_edtDate.text.isNotEmpty) {
      return CommonMethod.DateStringToNumeric(_edtDate.text);
    } else {
      return 0;
    }
  }

  set value(double val) {
    if (val > 0) {
      _edtDate.text = CommonMethod.NumericToDateString(val);
    } else {
      _edtDate.text = "";
    }
  }

  String get text {
    return _edtDate.text;
  }
}
