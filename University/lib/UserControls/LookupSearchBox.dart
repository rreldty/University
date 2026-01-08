import 'package:flutter/foundation.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';

import 'EditText.dart';

class LookupSearchBox extends StatefulWidget {
  final EditTextController? controller;
  final Function()? onLostFocus;

  const LookupSearchBox({
    super.key,
    this.controller,
    this.onLostFocus,
  });

  @override
  createState() => LookupSearchBoxState();
}

class LookupSearchBoxState extends State<LookupSearchBox> {
  final FocusNode _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();

    _focusNode.addListener(() {
      if(!_focusNode.hasFocus){
        //debugPrint("Lost Focus");
        if(widget.onLostFocus != null) {
          widget.onLostFocus!();
        }
      }
    });
  }

  @override
  void dispose() {
    super.dispose();
    _focusNode.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        width: double.infinity,
        child: TextFormField(
            controller: widget.controller,
            focusNode: _focusNode,
            textInputAction: TextInputAction.go,
            style: TextStyle(fontSize: GlobalStyle.fontSize),
            textAlign: TextAlign.start,
            decoration: InputDecoration(
                errorStyle: GlobalStyle.errorStyle,
                border: UnderlineInputBorder(
                  borderRadius: const BorderRadius.all(Radius.zero),
                  borderSide: BorderSide(
                    color: GlobalStyle.primaryColor,
                    width: 1.0,
                  ),
                ),
                enabledBorder: UnderlineInputBorder(
                  borderRadius: const BorderRadius.all(Radius.zero),
                  borderSide: BorderSide(
                    color: GlobalStyle.primaryColor,
                    width: 1.0,
                  ),
                ),
                errorBorder: UnderlineInputBorder(
                  borderRadius: const BorderRadius.all(Radius.zero),
                  borderSide: BorderSide(
                    color: GlobalStyle.errorColor,
                    width: 1.0,
                  ),
                ),
                focusedBorder: UnderlineInputBorder(
                  borderRadius: const BorderRadius.all(Radius.zero),
                  borderSide: BorderSide(
                    color: GlobalStyle.primaryColor,
                    width: 1.0,
                  ),
                ),
                isDense: true,
                contentPadding: const EdgeInsets.only(left: 2, top: 10, right: 10, bottom: 10),
                filled: true,
                fillColor: Colors.white,
                hintText: "Search"
            )
        )
    );
  }
}