import 'dart:convert';
import 'dart:io';

import 'package:auto_size_text/auto_size_text.dart';
import 'package:flutter/services.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';
import '../../Dao/Base/LookupDao.dart';
import '../../Dto/Base/EntityDto.dart';
import '../../Dto/Base/LookupDto.dart';
import 'EditText.dart';
import 'LookupPage.dart';

class Lookup extends StatefulWidget {
  final String entity;
  final String? filter;
  final String title;
  final bool isMandatory;
  final bool? isEnable;
  final bool isVisible;
  final Function(Map<String, dynamic> item)? onLostFocus;
  final LookupController controller;
  final int maxLength;
  final int minLength;
  final String mandatoryText;
  final bool showDescription;
  /// Hides column with the given column index.
  final List<int>? hideColumns;

  Lookup({
    this.entity = "",
    this.filter,
    this.title = "",
    this.isMandatory = false,
    this.isEnable,
    this.isVisible = true,
    this.onLostFocus,
    required this.controller,
    this.maxLength = -1,
    this.minLength = -1,
    this.mandatoryText = "",
    this.showDescription = true,
    this.hideColumns,
  });

  @override
  createState() => LookupState();
}

class LookupState extends State<Lookup> {
  //region Variables
  final FocusNode _focusNode = FocusNode();
  bool isValueValid = true;
  String strLookupDescription = "";
  Map<String, dynamic> itemLookup = {};
  //endregion

  @override
  void initState() {
    _focusNode.addListener(() {
      if(!_focusNode.hasFocus){
        // debugPrint("Lookup lost focus");
        if(widget.onLostFocus != null) {
          edtLookup_LostFocus();
        }
      }else{
        // debugPrint("Lookup has focus");
        //debugPrint("val: " + widget.fieldController.text);
      }
    });

    if(widget.filter != null) {
      widget.controller.filter = widget.filter ?? "";
    }

    widget.controller.isEnable = widget.isEnable ?? true;
    widget.controller.isVisible = widget.isVisible ?? true;

    widget.controller._isRefreshDscr = true;

    super.initState();
  }

  void btnLookup_Click() async {
    String strResult = await LookupPage().showLookup(
      context: context,
      entity: widget.entity,
      title: widget.title,
      filter: widget.controller.filter,
      hideColumns: widget.hideColumns,
      showPaging: GlobalStyle.lookupShowPaging,
    ) ?? "";
    if (strResult.isNotEmpty) {
      itemLookup = json.decode(strResult);
      setState(() {
        widget.controller.text = itemLookup.values.elementAt(0);
        strLookupDescription = itemLookup.values.elementAt(1);
        isValueValid = true;
      });
      if (widget.onLostFocus != null) {
        widget.onLostFocus!(itemLookup);
      }
    }
  }

  void edtLookup_LostFocus() {
    // debugPrint("edtLookup_LostFocus");
    setState(() {
      widget.controller._isRefreshDscr = true;
    });
    if(widget.onLostFocus != null){
      widget.onLostFocus!(itemLookup);
    }
  }

  @override
  Widget build(BuildContext context) {
    if(widget.isVisible){
      return Container(
        width: 350,
        padding: const EdgeInsets.all(0),
        margin: const EdgeInsets.all(0),
        child: Stack(
          children: [
            Container(
              margin: const EdgeInsets.only(top: 5),
              width: 145,
              height: 32,
              decoration: BoxDecoration(
                color: widget.controller.isEnable ? GlobalStyle.primaryColor : GlobalStyle.disableColor,
                borderRadius: BorderRadius.circular(5.0),
              ),
            ),
            Container(
              margin: const EdgeInsets.only(top: 5),
              width: 120,
              child: TextFormField(
                  textInputAction: TextInputAction.go,
                  textAlign: TextAlign.start,
                  controller: widget.controller._textController,
                  obscureText: false,
                  keyboardType: TextInputType.text,
                  enabled: widget.controller.isEnable,
                  maxLength: widget.maxLength,
                  inputFormatters: validateInputFormatter(),
                  onChanged: (val) {
                    // debugPrint("onChanged");
                    validateValue();
                  },
                  onFieldSubmitted: (val) => debugPrint("onFieldSubmitted"),
                  onEditingComplete: () => debugPrint("onEditingComplete"),
                  onSaved: (val) => debugPrint("onSaved"),
                  validator:  validateText,
                  //onFieldSubmitted: widget.whenFieldSubmitted != null ? widget.whenFieldSubmitted : null,
                  focusNode: _focusNode,
                  style: TextStyle(fontSize: GlobalStyle.fontSize, color: Colors.black),
                  decoration: InputDecoration(
                    counterText: "",
                    errorStyle: GlobalStyle.errorStyle,
                    prefixIconConstraints: const BoxConstraints(maxHeight: 24, maxWidth: 24),
                    suffixIconConstraints: const BoxConstraints(maxHeight: 24, maxWidth: 24),
                    hoverColor: Colors.transparent,
                    isDense: true,
                    contentPadding: const EdgeInsets.all(10),
                    filled: true,
                    hintStyle: TextStyle(fontSize: GlobalStyle.fontSize),
                    labelStyle: TextStyle(fontSize: GlobalStyle.fontSize),
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
                  )
              ),
            ),
            Positioned(
              left: 120,
              child: Container(
                margin: const EdgeInsets.only(top: 5),
                padding: const EdgeInsets.only(left: 0, top: 0, right: 2, bottom: 0),
                child: InkWell(
                  onTap: widget.controller.isEnable ? btnLookup_Click : null,
                  child: Container(
                    width: 20,
                    height: 32,
                    margin: const EdgeInsets.only(bottom: 0, left: 2, right: 2),
                    child: const Text(
                      "...",
                      style: TextStyle(
                        fontSize: 16,
                        color: Colors.white,
                        letterSpacing: 2.0,
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ),
                ),
              ),
            ),
            Visibility(
              visible: widget.showDescription,
              child: Positioned(
                left: 150,
                child: Container(
                  margin: const EdgeInsets.only(top: 5),
                  padding: const EdgeInsets.only(left: 10, top: 5, right: 10, bottom: 5),
                  width: 200,
                  height: 32,
                  decoration: BoxDecoration(
                    color: GlobalStyle.disableColor,
                    border: Border.all(color: Colors.grey, width: 1),
                    borderRadius: BorderRadius.circular(5),
                  ),
                  child: builderDescription(),
                ),
              ),
            ),

          ],
        ),
      );
    }else{
      return Container();
    }

  }

  Widget builderDescription(){
    return FutureBuilder(
      future: getDescription(),
      builder: (context, snapshot) {
        if(snapshot.connectionState == ConnectionState.done && snapshot.hasData){
          return Text(
            snapshot.data.toString(),
              style: TextStyle(fontSize: GlobalStyle.fontSize,color:  Colors.black ,)
          );
        }
        return Text(
          strLookupDescription,
            style: TextStyle(fontSize: GlobalStyle.fontSize,color:  Colors.black ,)
        );
      },
    );
  }

  Future<String> getDescription() async {
    // debugPrint("[getDescription-isRefreshDscr] ${widget.controller._isRefreshDscr.toString()}");

    if(widget.controller._isRefreshDscr){
      // debugPrint("[getDescription-Init] ${widget.entity} | ${widget.controller.text} | $strLookupDescription" );

      isValueValid = false;
      strLookupDescription = "";
      itemLookup = {};

      if(widget.entity.isNotEmpty && widget.controller.text.isNotEmpty){
        // debugPrint("[getDescription-Get] ${widget.entity} | ${widget.controller.text} | $strLookupDescription" );

        EntityDto objEntity = EntityDto(
          Entity: widget.entity,
          Filter: "",
          KeyCode: widget.controller.text,
        );

        LookupDao dao = LookupDao();
        LookupDto objLookup = await dao.listLookupOne(objEntity);

        if (objLookup.Rows!.isNotEmpty) {
          itemLookup = objLookup.Rows![0];
          strLookupDescription = itemLookup.values.elementAt(1);
          // debugPrint("[getDescription-Finish] ${widget.entity} | ${widget.controller.text} | $strLookupDescription" );

          isValueValid = true;
        }
      }

      widget.controller._isRefreshDscr = false;
    }

    return Future.value(strLookupDescription);
  }
  List<TextInputFormatter> validateInputFormatter() {
    return [LengthLimitingTextInputFormatter(widget.maxLength)];
  }

  String? validateText(String? value) {
    String strResult = "";
    String strValue = value ?? "";
    // debugPrint("validateText");
    if(widget.isMandatory && strValue.isEmpty){
      if(widget.mandatoryText.isNotEmpty){
        strResult = widget.mandatoryText;
      }else{
        strResult = "Please fill this field";
      }
    }

    if(strResult.isEmpty){
      if(strValue.isNotEmpty){
        //if(widget.controller.isEnable && widget.isMandatory && !isValueValid){
        if(widget.controller.isEnable && !isValueValid){
          strResult = "Field is not valid!";
        }
      }
    }

    if(strResult.isEmpty){
      if(widget.minLength > 0){
        if(strValue.length < widget.minLength) {
          strResult = "Minimum length is ${widget.minLength}";
        }
      }
    }

    if(strResult.isNotEmpty) {
      return strResult;
    }

    return null;
  }

  void validateValue() async {
    EntityDto obj = EntityDto(
      Entity: widget.entity,
      Filter: widget.controller.filter,
      KeyCode: widget.controller.text,
    );

    LookupDao dao = LookupDao();
    dao.listLookupOne(obj).then((obj){
      if (obj.Rows!.isNotEmpty) {
        setState(() {
          isValueValid = true;
        });

      }else{
        setState(() {
          isValueValid = false;
        });
      }
    });
  }
}

class LookupController{
  String filter = "";
  bool isEnable = true;
  bool isVisible = true;
  bool _isRefreshDscr = false;

  final EditTextController _textController = EditTextController();
  String get text{
    return _textController.text;
  }

  set text(String value) {
    _textController.text = value;
    _isRefreshDscr = true;
  }
}