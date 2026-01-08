import 'package:auto_size_text/auto_size_text.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';
import '../../Common/LabelDictionary.dart';

class LabelText extends StatefulWidget {
  final String labelText;
  final Color? labelColor;
  final double? fontSize;
  final double width;
  final FontWeight? fontWeight;
  final bool isMandatory;
  final bool isVisible;

  const LabelText({
    this.labelText = "",
    this.labelColor,
    this.width = 165,
    this.fontSize,
    this.fontWeight,
    this.isMandatory = false,
    this.isVisible = true,
  });

  @override
  createState() => LabelTextState();
}

class LabelTextState extends State<LabelText> {
  @override
  Widget build(BuildContext context) {
    String strLabel = widget.labelText;
    strLabel = LabelDictionary.getLabelDictionary(strLabel);
    // debugPrint("labelText: $strLabel");

    if (widget.isVisible) {
      if (strLabel.isNotEmpty) {
        return Container(
            margin: const EdgeInsets.only(top: 5),
            padding: const EdgeInsets.all(0),
            child: ConstrainedBox(
              constraints: BoxConstraints(
                minWidth: widget.width,
              ),
              child: Row(
                children: <Widget>[
                  Text(
                    strLabel,
                    style: TextStyle(
                      color: widget.labelColor ?? GlobalStyle.labelColor,
                      fontSize: widget.fontSize ?? GlobalStyle.labelFontSize,
                      fontWeight: widget.fontWeight ?? FontWeight.normal,
                        fontFamily: GlobalStyle.fontFamily
                    ),
                  ),
                  _mandatorySymbol(),
                ],
              ),
            ),
        );
      }
    }

    return Container();
  }

  Widget _mandatorySymbol() {
    if (widget.isMandatory) {
      return Text(
        "*",
        style: TextStyle(
          color: GlobalStyle.errorColor,
          fontSize: GlobalStyle.labelFontSize,
          fontFamily: GlobalStyle.fontFamily
        ),
      );
    } else {
      return Container();
    }
  }
}
