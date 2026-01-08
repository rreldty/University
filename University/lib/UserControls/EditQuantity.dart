import '../../Common/CommonMethod.dart';
import 'package:flutter/foundation.dart';
import 'package:intl/intl.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import '../Common/AppConfig.dart';
import 'EditText.dart';

class EditQuantity extends StatefulWidget {
  final EditTextController controller;
  final String? hintText;
  final String? labelText;
  final String? helperText;
  final String? mandatoryText;
  final bool isMandatory;
  final bool? isEnable;
  final bool? isVisible;
  final Icon? prefixIcon;
  final double? maxValue;
  final double? minValue;
  final Function()? onLostFocus;
  final TextInputAction? textInputAction;
  final double width;
  final double height;
  final EdgeInsetsGeometry? contentPadding;

  const EditQuantity({
    super.key,
    required this.controller,
    this.hintText,
    this.labelText,
    this.helperText,
    this.mandatoryText,
    this.isMandatory = false,
    this.isEnable,
    this.isVisible = true,
    this.prefixIcon,
    this.onLostFocus,
    this.textInputAction = TextInputAction.done,
    this.width = 80,
    this.height = 100,
    this.maxValue,
    this.minValue,
    this.contentPadding = const EdgeInsets.fromLTRB(10, 10, 10, 10)
  });

  @override
  createState() => EditQuantityState();
}

class EditQuantityState extends State<EditQuantity> {

  @override
  void initState() {

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        IconButton(
            onPressed: () {
              double dblQty = widget.controller.numericValue;
              if(dblQty > 0) {
                widget.controller.text = CommonMethod.NumericToQuantityFormat(dblQty - 1);
              }
            },
            icon: Icon(Icons.remove, color: GlobalStyle.primaryColor,)
        ),
        EditText(
          controller: widget.controller,
          width: widget.width,
          height: widget.height,
          textMode: TextInputType.number,
          numericType: NumericType.Quantity,
          hintText: widget.hintText,
          labelText: widget.labelText,
          helperText: widget.helperText,
          mandatoryText: widget.mandatoryText,
          isMandatory: widget.isMandatory,
          isEnable: widget.isEnable,
          isVisible: widget.isVisible,
          prefixIcon: widget.prefixIcon,
          onLostFocus: widget.onLostFocus,
          textInputAction:  widget.textInputAction,
          maxValue: widget.maxValue,
          minValue: widget.minValue,
          contentPadding: widget.contentPadding,
        ),
        IconButton(
            onPressed: () {
              widget.controller.text = CommonMethod.NumericToQuantityFormat(widget.controller.numericValue + 1);
            },
            icon: Icon(Icons.add, color: GlobalStyle.primaryColor,)
        ),
      ],
    );


  }

}
