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
import 'SearchBoxPage.dart';

class SearchBoxFormField extends FormField<String?>{
  SearchBoxFormField({
    super.key,
    super.autovalidateMode,
    required String entity,
    String? filter,
    String title = "",
    bool isMandatory = false,
    bool? isEnable,
    bool isVisible = true,
    Function(Map<String, dynamic> item)? onLostFocus,
    required SearchBoxController controller,
    int maxLength = -1,
    int minLength = -1,
    String mandatoryText = "",
    bool showDescription = true,

    /// Hides column with the given column index.
    List<int>? hideColumns,
  }):super(
    initialValue: controller.text,
    builder: (state){
      void onLostFocusHandler(Map<String, dynamic> item) {
        debugPrint("[onLostFocusHandler] $item");
        state.didChange(item[item.keys.first]);
        if (onLostFocus != null) {
          onLostFocus(item);
        }
      }

      void onTapHandler(Map<String, dynamic> item) {
        debugPrint("[onTapHandler] $item");
        //state.didChange(item[item.keys.first]);
        if (onLostFocus != null) {
          onLostFocus(item);
        }
      }

      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          SearchBox(
            controller: controller,
            entity: entity,
            filter: filter,
            title: title,
            isEnable: isEnable,
            isVisible: isVisible,
            onLostFocus: onLostFocusHandler,
            onTap: onTapHandler,
            maxLength: maxLength,
            minLength: minLength,
            showDescription: showDescription,
            hideColumns: hideColumns,
            colorBorder: state.hasError ? Colors.red : null,
          ),
          if (state.hasError) ...[
            Container(
              padding: const EdgeInsets.only(top: 8, left: 8),
              child: Text(
                state.errorText!,
                style: GlobalStyle.errorStyle,
              ),
            ),
          ],
        ],
      );
    },
    validator: (value) {
      String strResult = "";
      String strValue = value ?? "";
      //baru
      strValue = controller.text;

      // debugPrint("validateText");
      if(isMandatory && strValue.isEmpty){
        if(mandatoryText.isNotEmpty){
          strResult = mandatoryText;
        }else{
          strResult = "Please fill this field";
        }
      }

      // if(strResult.isEmpty){
      //   if(strValue.isNotEmpty){
      //     //if(widget.controller.isEnable && widget.isMandatory && !isValueValid){
      //     if(controller.isEnable && !isValueValid){
      //       strResult = "Field is not valid!";
      //     }
      //   }
      // }

      if(strResult.isEmpty){
        if(minLength > 0){
          if(strValue.length < minLength) {
            strResult = "Minimum length is $minLength";
          }
        }
      }

      if(strResult.isNotEmpty) {
        return strResult;
      }

      return null;
    },
  );

}

class SearchBox extends StatefulWidget {
  final String entity;
  final String? filter;
  final String title;
  // final bool isMandatory;
  final bool? isEnable;
  final bool isVisible;
  final Function(Map<String, dynamic> item)? onLostFocus;
  final Function(Map<String, dynamic> item)? onTap;
  final SearchBoxController controller;
  final int maxLength;
  final int minLength;

  // final String mandatoryText;
  final bool showDescription;

  /// Hides column with the given column index.
  final List<int>? hideColumns;
  final Color? colorBorder;

  SearchBox({
    required this.controller,
    this.entity = "",
    this.filter,
    this.title = "",
    // this.isMandatory = false,
    this.isEnable,
    this.isVisible = true,
    this.onLostFocus,
    this.onTap,
    this.maxLength = -1,
    this.minLength = -1,
    // this.mandatoryText = "",
    this.showDescription = true,
    this.hideColumns,
    this.colorBorder,
  });

  @override
  createState() => SearchBoxState();
}

class SearchBoxState extends State<SearchBox> {
  //region Variables
  final FocusNode _focusNode = FocusNode();
  bool isValueValid = true;
  String strSearchBoxDescription = "";
  Map<String, dynamic> itemSearchBox = {};
  //endregion

  @override
  void initState() {
    _focusNode.addListener(() {
      if(!_focusNode.hasFocus){
        // debugPrint("SearchBox lost focus");
        if(widget.onLostFocus != null) {
          edtSearchBox_LostFocus();
        }
      }else{
        // debugPrint("SearchBox has focus");
        //debugPrint("val: " + widget.fieldController.text);
      }
    });

    if(widget.filter != null) {
      widget.controller.filter = widget.filter ?? "";
    }

    if(widget.isEnable != null) {
      widget.controller.isEnable = widget.isEnable  ?? true;
    }

    widget.controller._isRefreshDscr = true;

    super.initState();
  }

  void btnSearchBox_Click() async {
    String strResult = await SearchBoxPage().showSearchBox(
      context: context,
      entity: widget.entity,
      title: widget.title,
      filter: widget.controller.filter,
      hideColumns: widget.hideColumns,
    ) ?? "";
    if (strResult.isNotEmpty) {
      debugPrint("[btnSearchBox_Click] $strResult");

      itemSearchBox = json.decode(strResult);

      widget.controller.text = itemSearchBox.values.elementAt(0);
      //strSearchBoxDescription = itemSearchBox.values.elementAt(1);
      isValueValid = true;

      if(widget.onTap != null){
        widget.onTap!(itemSearchBox);
      }
    }
  }

  void edtSearchBox_LostFocus() {
    debugPrint("edtSearchBox_LostFocus");
    setState(() {
      widget.controller._isRefreshDscr = true;
    });
    if(widget.onLostFocus != null){
      widget.onLostFocus!(itemSearchBox);
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
            InkWell(
              onTap: widget.controller.isEnable ? btnSearchBox_Click : null,
              child: Container(
                margin: const EdgeInsets.only(top: 5),
                padding: const EdgeInsets.only(left: 10, top: 5, right: 10, bottom: 5),
                width: 350,
                height: 32,
                decoration: BoxDecoration(
                  // color: Colors.white,
                  color: widget.controller.isEnable ? Colors.white : GlobalStyle.disableColor,
                  border: Border.all(color: widget.colorBorder ?? Colors.grey, width: 1),
                  borderRadius: BorderRadius.circular(5),
                ),
                child: builderDescription(),
              ),
            ),
            // Container(
            //   margin: const EdgeInsets.only(top: 5),
            //   width: 120,
            //   child: TextFormField(
            //       textInputAction: TextInputAction.go,
            //       textAlign: TextAlign.start,
            //       controller: widget.controller._textController,
            //       obscureText: false,
            //       keyboardType: TextInputType.text,
            //       enabled: widget.controller.isEnable,
            //       maxLength: widget.maxLength,
            //       inputFormatters: validateInputFormatter(),
            //       onChanged: (val) {
            //         // debugPrint("onChanged");
            //         validateValue();
            //       },
            //       onFieldSubmitted: (val) => debugPrint("onFieldSubmitted"),
            //       onEditingComplete: () => debugPrint("onEditingComplete"),
            //       onSaved: (val) => debugPrint("onSaved"),
            //       validator:  validateText,
            //       //onFieldSubmitted: widget.whenFieldSubmitted != null ? widget.whenFieldSubmitted : null,
            //       focusNode: _focusNode,
            //       style: TextStyle(fontSize: GlobalStyle.fontSize, color: Colors.black),
            //       decoration: InputDecoration(
            //         counterText: "",
            //         errorStyle: const TextStyle(fontWeight: FontWeight.bold),
            //         prefixIconConstraints: const BoxConstraints(maxHeight: 24, maxWidth: 24),
            //         suffixIconConstraints: const BoxConstraints(maxHeight: 24, maxWidth: 24),
            //         hoverColor: Colors.transparent,
            //         isDense: true,
            //         contentPadding: const EdgeInsets.all(10),
            //         filled: true,
            //         hintStyle: TextStyle(fontSize: GlobalStyle.fontSize),
            //         labelStyle: TextStyle(fontSize: GlobalStyle.fontSize),
            //         helperStyle: TextStyle(fontSize: GlobalStyle.fontSize),
            //         fillColor: widget.controller.isEnable ? Colors.white : GlobalStyle.disableColor,
            //         border: OutlineInputBorder(
            //           borderRadius: BorderRadius.circular(5.0),
            //           borderSide: const BorderSide(
            //             color: Color(0xffbababa),
            //             width: 1.0,
            //           ),
            //         ),
            //         errorBorder: OutlineInputBorder(
            //           borderRadius: BorderRadius.circular(5.0),
            //           borderSide: BorderSide(
            //             color: GlobalStyle.errorColor,
            //             width: 1.0,
            //           ),
            //         ),
            //         focusedBorder: OutlineInputBorder(
            //           borderRadius: BorderRadius.circular(5.0),
            //           borderSide: BorderSide(
            //             color: GlobalStyle.primaryColor,
            //             width: 1.0,
            //           ),
            //         ),
            //       )
            //   ),
            // ),
            Positioned(
              left: 320,
              child: Container(
                margin: const EdgeInsets.only(top: 5),
                padding: const EdgeInsets.only(left: 0, top: 0, right: 2, bottom: 0),
                child: InkWell(
                  onTap: widget.controller.isEnable ? btnSearchBox_Click : null,
                  child: Container(
                    width: 20,
                    height: 32,
                    margin: const EdgeInsets.only(bottom: 0, left: 2, right: 2),
                    child: Icon(Icons.search, color: GlobalStyle.primaryColor,),
                  ),
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
        if(snapshot.hasData){
          return Text(
              snapshot.data.toString(),
              style: TextStyle(fontSize: GlobalStyle.fontSize,color:  Colors.black ,)
          );
        }
        return Text(
            "",
            style: TextStyle(fontSize: GlobalStyle.fontSize,color:  Colors.black ,)
        );
      },
    );
  }

  Future<String> getDescription() async {
    // debugPrint("[getDescription-isRefreshDscr] ${widget.controller._isRefreshDscr.toString()}");

    //String strSearchBoxDescription = "";

    if(widget.controller._isRefreshDscr){
      // debugPrint("[getDescription-Start] ${widget.entity} | ${widget.controller.text} | $strSearchBoxDescription |" );

      isValueValid = false;
      itemSearchBox = {};
      //baru
      strSearchBoxDescription = "";

      if(widget.entity.isNotEmpty && widget.controller.text.isNotEmpty){
        EntityDto objEntity = EntityDto(
          Entity: widget.entity,
          Filter: "",
          KeyCode: widget.controller.text,
        );

        LookupDao dao = LookupDao();
        LookupDto objSearchBox = await dao.listLookupOne(objEntity);

        if (objSearchBox.Rows!.isNotEmpty) {
          itemSearchBox = objSearchBox.Rows![0];
          strSearchBoxDescription = itemSearchBox.values.elementAt(1);
          // debugPrint("[getDescription-Finish] ${widget.entity} | ${widget.controller.text} | $strSearchBoxDescription |" );

          isValueValid = true;
        }
      }

      widget.controller._isRefreshDscr = false;
    }

    // debugPrint("[getDescription-isRefreshDscr] ${widget.controller._isRefreshDscr.toString()}");

    return Future.value(strSearchBoxDescription);
  }

  List<TextInputFormatter> validateInputFormatter() {
    return [LengthLimitingTextInputFormatter(widget.maxLength)];
  }

  // String? validateText(String? value) {
  //   String strResult = "";
  //   String strValue = value ?? "";
  //   // debugPrint("validateText");
  //   if(widget.isMandatory && strValue.isEmpty){
  //     if(widget.mandatoryText.isNotEmpty){
  //       strResult = widget.mandatoryText;
  //     }else{
  //       strResult = "Please fill this field";
  //     }
  //   }
  //
  //   if(strResult.isEmpty){
  //     if(strValue.isNotEmpty){
  //       //if(widget.controller.isEnable && widget.isMandatory && !isValueValid){
  //       if(widget.controller.isEnable && !isValueValid){
  //         strResult = "Field is not valid!";
  //       }
  //     }
  //   }
  //
  //   if(strResult.isEmpty){
  //     if(widget.minLength > 0){
  //       if(strValue.length < widget.minLength) {
  //         strResult = "Minimum length is ${widget.minLength}";
  //       }
  //     }
  //   }
  //
  //   if(strResult.isNotEmpty) {
  //     return strResult;
  //   }
  //
  //   return null;
  // }

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

class SearchBoxController{
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
    // debugPrint("on set Text will set _isRefreshDscr = true");
    _isRefreshDscr = true;
  }
}