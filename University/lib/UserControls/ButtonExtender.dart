import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';

class ButtonExtender extends StatefulWidget {
  final ButtonController? controller;
  final Function()? onPressed;
  final String buttonText;
  final double width;
  final bool? isEnable;
  final bool? isVisible;
  final IconData? icon;

  const ButtonExtender({
    super.key,
    this.controller,
    this.onPressed,
    this.buttonText = "",
    this.width = 120,
    this.isEnable,
    this.isVisible = true,
    this.icon,
  });

  @override
  createState() => ButtonExtenderState();
}

class ButtonExtenderState extends State<ButtonExtender> {
  bool _isEnable = true;
  bool _isVisible = true;

  @override
  void initState() {
    super.initState();

    _isEnable = widget.isEnable ?? true;
    _isVisible = widget.isVisible ?? true;

    final bool? isVisible;
  }

  @override
  Widget build(BuildContext context) {
    if(widget.controller != null){
      _isEnable = widget.controller!.isEnable;
      _isVisible = widget.controller!.isVisible;
    }

    if(_isVisible){
      return Container(
          margin: const EdgeInsets.only(top: 5),
          width: widget.width,
          height: 30,
          child: buttonBuilder()
      );
    }else{
      return Container();
    }

  }

  Widget buttonBuilder(){
    int intTapCount = 0;

    onPressed() async {
      if(_isEnable){
        intTapCount++;
        // debugPrint("onPressed $intTapCount");
        if(intTapCount == 1){
          intTapCount = 0;
          // debugPrint("onPressedAction");
          if(widget.onPressed != null){
            widget.onPressed!();
          }
        }

      }
    }

    Widget label = Text(
      widget.buttonText,
      style: const TextStyle(fontSize: 12, color: Colors.white,fontWeight: FontWeight.bold),
    );

    ButtonStyle buttonStyle = ButtonStyle(
        backgroundColor: MaterialStateProperty.all<Color>(_isEnable ? GlobalStyle.primaryColor : GlobalStyle.disableColor),
        shape: MaterialStateProperty.all<RoundedRectangleBorder>(
            RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(5.0),
            )
        )
    );

    Widget buttonContent = TextButton(
      style: buttonStyle,
      onPressed: onPressed,
      child: label,
    );

    if(widget.icon != null){
      buttonContent = TextButton.icon(
          style: buttonStyle,
          onPressed: onPressed,
          icon: Icon(
            widget.icon,
            color: Colors.white,
            size: 20.0,
          ),
          label: label
      );
    }

    return buttonContent;
  }
}

class ButtonController{
  bool isEnable = true;
  bool isVisible = true;
}