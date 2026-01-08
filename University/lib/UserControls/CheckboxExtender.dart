import 'package:flutter/material.dart';
import 'package:flutter/painting.dart';
import '../../Common/GlobalStyle.dart';

class CheckboxExtender extends StatefulWidget {
  final CheckboxExtenderController? controller;
  final String? label;
  final bool isChecked;
  final Function(bool isChecked)? onChanged;
  final bool isEnable;
  final bool isVisible;

  CheckboxExtender({
    super.key,
    this.controller,
    this.label,
    this.onChanged,
    this.isChecked = false,
    this.isEnable = true,
    this.isVisible = true,
  });

  @override
  createState() => CheckboxExtenderState();
}

class CheckboxExtenderState extends State<CheckboxExtender> {
  bool _isChecked = false;
  bool _isEnable = true;
  bool _isVisible = true;

  @override
  void initState() {
    _isChecked = widget.isChecked;
    _isEnable = widget.isEnable;
    _isVisible = widget.isVisible;

    if(widget.controller != null) {
      widget.controller!.isChecked = widget.isChecked;
      widget.controller!.isEnable = widget.isEnable;
      widget.controller!.isVisible = widget.isVisible;
    }

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    //debugPrint("_isChecked:" + _isChecked.toString());
    //debugPrint("widget.isChecked:" + widget.isChecked.toString());

    if(widget.controller != null) {
      _isChecked = widget.controller!.isChecked;
      _isEnable = widget.controller!.isEnable;
      _isVisible = widget.controller!.isVisible;
    }

    if(_isVisible){
      return Container(
        padding: const EdgeInsets.all(0),
        margin: const EdgeInsets.only(top: 5, bottom: 0, left: 0, right: 0),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.start,
          children: <Widget>[
            SizedBox(
              width: 17,
              height: 17,
              child: Container(
                decoration: BoxDecoration(
                    color: _isEnable ? Colors.white : GlobalStyle.disableColor
                ),
                child: Theme(
                  data: ThemeData(
                    primarySwatch: GlobalStyle.primaryMaterialColor,
                    unselectedWidgetColor: GlobalStyle.primaryMaterialColor,
                    disabledColor: GlobalStyle.disableMaterialColor,
                  ),
                  child: Checkbox(
                    value: _isChecked,
                    activeColor: _isEnable ? GlobalStyle.primaryColor : GlobalStyle.disableColor,
                    // side: MaterialStateBorderSide.resolveWith((states) => BorderSide(width: 1, color: GlobalStyle.primaryColor)),
                    onChanged: (value) {
                      if(_isEnable) {
                        setState(() {
                          _isChecked = value!;
                          if(widget.controller != null) {
                            widget.controller!.isChecked = value;
                          }
                        });

                        if (widget.onChanged != null) {
                          widget.onChanged!(value!);
                        }
                      }
                    },
                  ),
                ),
              ),
            ),
            widget.label != null ? Container(
              margin: const EdgeInsets.only(left: 10),
              child: Text(
                  widget.label ?? "",
                  style: TextStyle(fontSize: GlobalStyle.fontSize)
              ),
            ) : Container(),
          ],
        ),
      );
    }else{
      return Container();
    }

  }
}

class CheckboxExtenderController{
  bool isChecked = false;
  bool isEnable = true;
  bool isVisible = true;
}