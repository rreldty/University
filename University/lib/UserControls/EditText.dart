import 'package:flutter/foundation.dart';
import 'package:intl/intl.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import '../Common/AppConfig.dart';
import '../Common/CommonMethod.dart';

class EditText extends StatefulWidget {
  final EditTextController controller;
  final String? hintText;
  final String? labelText;
  final String? helperText;
  final String? mandatoryText;
  final bool isPassword;
  final bool isEnforcePasswordComplexity;
  final bool isMandatory;
  final bool? isEnable;
  final bool? isVisible;
  final TextInputType? textMode;
  final Icon? prefixIcon;
  final int maxLength;
  final int minLength;
  final double? maxValue;
  final double? minValue;
  final Function()? onLostFocus;
  final TextInputAction? textInputAction;
  final double width;
  final double height;
  final NumericType numericType;
  final EdgeInsetsGeometry? contentPadding;
  final Iterable<String>? autofillHints;

  const EditText({
    super.key,
    required this.controller,
    this.hintText,
    this.labelText,
    this.helperText,
    this.mandatoryText,
    this.textMode = TextInputType.text,
    this.isPassword = false,
    this.isEnforcePasswordComplexity = false,
    this.isMandatory = false,
    this.isEnable = true,
    this.isVisible = true,
    this.prefixIcon,
    this.maxLength = -1,
    this.minLength = -1,
    //this.whenFieldSubmitted,
    this.onLostFocus,
    this.textInputAction = TextInputAction.done,
    this.width = 350,
    this.height = 100,
    this.numericType = NumericType.Amount,
    this.maxValue,
    this.minValue,
    this.contentPadding = const EdgeInsets.fromLTRB(10, 10, 10, 10),
    this.autofillHints,
  });

  @override
  createState() => EditTextState();
}

class EditTextState extends State<EditText> {
  late FocusNode focusNode;

  @override
  void initState() {
    widget.controller.isEnable = widget.isEnable ?? true;
    widget.controller.isVisible = widget.isVisible ?? true;

    widget.controller._numericType = widget.numericType;
    widget.controller._textMode = widget.textMode;

    focusNode = FocusNode(
      // onKey: (node, event) {
      //   print(event.logicalKey);
      //   if (event.isKeyPressed(LogicalKeyboardKey.enter) || event.isKeyPressed(LogicalKeyboardKey.tab)) {
      //     // print("Run lost focus");
      //     if(widget.onLostFocus != null) {
      //       widget.onLostFocus!();
      //     }
      //     // prevent passing the event into the TextField
      //     // return KeyEventResult.handled;
      //   }
      //   // pass the event to the TextField
      //   return KeyEventResult.ignored;
      // },
    );

    focusNode.addListener(() {
      if(!focusNode.hasFocus){
        //debugPrint("Lost Focus");
        if(widget.textMode == TextInputType.number && widget.controller.text.isNotEmpty){
          double dblValue = double.parse(widget.controller.text.replaceAll(AppConfig.thousandSeparator, ""));
          widget.controller.text = NumberFormat(AppConfig.numericFormat[widget.numericType.name], "en_US").format(dblValue);
        }

        if(widget.onLostFocus != null) {
          widget.onLostFocus!();
        }
      }
    });

    super.initState();
  }

  @override
  void dispose() {
    super.dispose();
    focusNode.dispose();
  }

  @override
  Widget build(BuildContext context) {
    double widthSize = double.infinity;

    if(CommonMethod.isWebOrDesktop()){
      widthSize = widget.width;
    }

    if(widget.controller.isVisible){
      // debugPrint("EditText widthSize $widthSize");
      Widget wEditText = TextFormField(
        controller: widget.controller,
        autofillHints: widget.autofillHints,
        textAlign: widget.textMode == TextInputType.number ? TextAlign.end : TextAlign.start,
        textAlignVertical: widget.textMode == TextInputType.multiline ? TextAlignVertical.top : TextAlignVertical.center,
        obscureText: (widget.isPassword) ? widget.controller.isObscureText : false,
        keyboardType: textInputType(),
        textInputAction: textInputAction(),
        enabled: widget.controller.isEnable,
        inputFormatters: validateInputFormatter(),
        validator:  validateText,
        focusNode: focusNode,
        minLines: widget.textMode == TextInputType.multiline ? null : 1,
        maxLines: widget.textMode == TextInputType.multiline ? null : 1,
        expands: widget.textMode == TextInputType.multiline ? true : false,
        style: TextStyle(fontSize: GlobalStyle.fontSize, color: Colors.black),
        decoration: InputDecoration(
            counterText: "",
            errorStyle: GlobalStyle.errorStyle,
            prefixIcon: _prefixIcon(),
            prefixIconConstraints: const BoxConstraints(maxHeight: 24, maxWidth: 24),
            suffixIcon: _suffixIcon(),
            suffixIconConstraints: const BoxConstraints(maxHeight: 24, maxWidth: 24),
            hoverColor: Colors.transparent,
            isDense: true,
            contentPadding: widget.contentPadding,
            constraints: widget.textMode != TextInputType.multiline ? BoxConstraints(maxWidth: widthSize) : BoxConstraints(maxHeight: widget.height, maxWidth: widthSize),
            filled: true,
            hintText: hintText(),
            hintStyle: TextStyle(fontSize: GlobalStyle.fontSize),
            labelText: widget.labelText,
            labelStyle: TextStyle(fontSize: GlobalStyle.fontSize),
            helperText: widget.helperText,
            helperStyle: TextStyle(fontSize: GlobalStyle.fontSize),
            fillColor: widget.controller.isEnable ? Colors.white : GlobalStyle.disableColor,
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
          ),
      );

      return Container(
        margin: const EdgeInsets.only(top: 5),
        padding: const EdgeInsets.all(0),
        child: LimitedBox(
          maxWidth: widthSize,
          maxHeight: widget.height,
          child: wEditText,
        ),
      );

    }else{
      return Container();
    }

  }

  TextInputType? textInputType(){
    if(widget.textMode == TextInputType.number) {
      return const TextInputType.numberWithOptions(decimal: true);
    }

    return widget.textMode;
  }

  TextInputAction? textInputAction(){
    if(widget.textMode == TextInputType.multiline) {
      return TextInputAction.newline;
    }

    return widget.textInputAction;
  }

  String? hintText(){
    if(widget.textMode == TextInputType.number) {
      String strNumericFormat = AppConfig.numericFormat[widget.numericType.name] ?? "";
      int intDecSepFormat = strNumericFormat.indexOf(RegExp(r"\" + AppConfig.decimalSeparator + r""));
      if(intDecSepFormat > -1) {
        return "0${strNumericFormat.substring(intDecSepFormat)}";
      }
      return "0";
    }

    return widget.hintText;
  }

  Widget? _prefixIcon(){
    if(widget.prefixIcon != null) {
      return Container(
        padding: const EdgeInsets.only(left: 5, right: 5),
        child: widget.prefixIcon,
      );
    }
    return null;
  }

  Widget? _suffixIcon(){
    if(widget.isPassword) {
      return InkWell(
        onTap: () {
          setState(() {
            widget.controller.isObscureText = !widget.controller.isObscureText;
          });
        },
        child: Container(
          padding: const EdgeInsets.only(right: 8),
          child: Icon(
            widget.controller.isObscureText ? Icons.visibility : Icons.visibility_off,
            semanticLabel: widget.controller.isObscureText ? 'show password' : 'hide password',
            size: 16,
          ),
        ),
      );
    }
    return null;
  }

  List<TextInputFormatter> validateInputFormatter() {
    List<TextInputFormatter> lstFormatter = [];

    if(widget.textMode != TextInputType.number && widget.maxLength > 0) {
      lstFormatter.add(LengthLimitingTextInputFormatter(widget.maxLength));
    }

    if(widget.textMode == TextInputType.number) {
      lstFormatter.add(FilteringTextInputFormatter.allow(RegExp(r"[0-9]+[" + AppConfig.decimalSeparator + r"]?[0-9]*")));
      lstFormatter.add(TextInputFormatter.withFunction((oldValue, newValue) {
        if(newValue.text.isNotEmpty) {
          // debugPrint("oldValue: " + oldValue.text);
          // debugPrint("newValue: " + newValue.text);
          // debugPrint("oldValueExtendPos: " + oldValue.selection.extentOffset.toString());
          // debugPrint("newValueExtendPos: " + newValue.selection.extentOffset.toString());

          String newText = newValue.text;
          String newTextDec = "";

          String strNumericFormat = AppConfig.numericFormat[widget.numericType.name] ?? "";
          int intDecSepFormat = strNumericFormat.indexOf(RegExp(r"\" + AppConfig.decimalSeparator + r""));
          int intDecLen = -1;
          if(intDecSepFormat > -1) {
            intDecLen = strNumericFormat.substring(intDecSepFormat + 1).length;
            //newTextDec = strNumericFormat.substring(intDecSepFormat);
          }

          int intDecSepNew = newText.indexOf(RegExp(r"\" + AppConfig.decimalSeparator + r""));
          if(intDecSepNew > -1){
            newText = newText.substring(0, intDecSepNew);
            newTextDec = newValue.text.replaceAll(newText, "");
          }

          double value = double.parse(newText);
          newText = NumberFormat("#${AppConfig.thousandSeparator}###", "en_US").format(value);

          if(newTextDec.isNotEmpty) {
            int newDecLen = newTextDec.replaceAll(".", "").length;
            intDecLen = newDecLen > intDecLen ? intDecLen : newDecLen;
            newText += newTextDec.substring(0, intDecLen + 1);
          }

          return newValue.copyWith(
            text: newText,
            selection: TextSelection.collapsed(offset: oldValue.selection.extentOffset + (newText.length - oldValue.text.length))
          );
        }

        return newValue;
      }));

    }

    return lstFormatter;
  }

  String? validateText(String? value){
    String strResult = "";
    String strValue = value ?? "";
    String strMandatory = widget.mandatoryText ?? "";

    if(widget.controller.isEnable)
    {
      if(widget.isMandatory && strValue.isEmpty){
        if(strMandatory.isNotEmpty){
          strResult = strMandatory;
        }else{
          strResult = "Please fill this field";
        }
      }

      if(strResult.isEmpty) {
        if(widget.textMode == TextInputType.number){
          if (widget.minValue != null) {
            double dblValue = double.tryParse(strValue.replaceAll(AppConfig.thousandSeparator, "")) ?? 0;
            double dblMinValue = widget.minValue ?? 0;
            if (widget.isMandatory && widget.minValue == null) {
              dblMinValue = 1;
            }
            if (dblValue < dblMinValue) {
              strResult = "Min. value is ${NumberFormat(AppConfig.numericFormat[widget.numericType.name], "en_US").format(dblMinValue)}";
            }
          }
        }
      }

      if(strResult.isEmpty) {
        if(widget.textMode == TextInputType.number) {
          if (widget.maxValue != null) {
            double dblValue = double.tryParse(strValue.replaceAll(AppConfig.thousandSeparator, "")) ?? 0;
            double dblMaxValue = widget.maxValue ?? 0;
            if (dblValue > dblMaxValue) {
              strResult = "Max. value is ${NumberFormat(AppConfig.numericFormat[widget.numericType.name], "en_US").format(dblMaxValue)}";
            }
          }
        }
      }

      if(strResult.isEmpty){
        if(widget.textMode == TextInputType.emailAddress){
          strResult = validateEmail(strValue);
        }
      }

      if(strResult.isEmpty){
        if(widget.minLength > 0){
          if(strValue.length < widget.minLength) {
            strResult = "Minimum length is ${widget.minLength}";
          }
        }
      }

      if(strResult.isEmpty){
        if(widget.isPassword && widget.isEnforcePasswordComplexity && widget.minLength > 0){
          strResult = validatePassword(strValue);
        }
      }
    }

    if(strResult.isNotEmpty) {
      return strResult;
    }

    return null;
  }

  String validateEmail(String value) {
    Pattern pattern =
        r'^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$';
    RegExp regex = RegExp(pattern.toString());
    if (!regex.hasMatch(value)) {
      return "Email is not valid";
    }
    return "";
  }

  String validatePassword(String value) {
    Pattern pattern = r'^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])(?!.* ).{12,}$';
    RegExp regex = RegExp(pattern.toString().replaceAll("{8,}", "{${widget.minLength},}"));
    if (!regex.hasMatch(value)) {
      return "Password must contain one lowercase letter, one uppercase letter, one special character, one number and it must be {${widget.minLength} characters long";
    }
    return "";
  }
}

class EditTextController extends TextEditingController{
  bool isEnable = true;
  bool isVisible = true;
  bool isObscureText = true;
  NumericType? _numericType;
  TextInputType? _textMode;

  double get numericValue{
    double? dblValue;
    if(text.isNotEmpty){
      dblValue = double.tryParse(text.replaceAll(AppConfig.thousandSeparator, ""));
    }
    return dblValue ?? 0;
  }
  set numericValue(double value){
    if(_textMode == TextInputType.number){
      text =
          NumberFormat(AppConfig.numericFormat[_numericType!.name], "en_US")
              .format(value);
    }else{
      text = value.toString();
    }
  }
}
