import 'package:flutter/material.dart';

import 'IconButtonExtender.dart';

class TooltipButton extends StatefulWidget {
  final TooltipButtonController? controller;
  final bool isEnable;
  final bool isVisible;
  final Widget child;
  final double? width;
  final double? height;
  final Color? color;
  final Color? hoverColor;
  final IconData? icon;

  const TooltipButton({
    super.key,
    required this.child,
    this.controller,
    this.isEnable = true,
    this.isVisible = true,
    this.width,
    this.height,
    this.color,
    this.hoverColor,
    this.icon,
  });

  @override
  createState() => TooltipButtonState();
}

class TooltipButtonState extends State<TooltipButton> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return IconButtonExtender(
      isEnable: widget.controller != null ? widget.controller!.isEnable : widget.isEnable,
      isVisible: widget.isVisible,
      color: widget.color,
      hoverColor: widget.hoverColor,
      onPressed: () async {
        await show();
      },
      icon: widget.icon ?? Icons.info_outline,
    );
  }

  Future show(){
    return showDialog(
      context: context,
      barrierDismissible: true,
      builder: (BuildContext context) {
        return AlertDialog(
          content: ConstrainedBox(
              constraints: BoxConstraints(
                minWidth: 300,
                maxWidth: widget.width ?? double.infinity,
                minHeight: 200,
                maxHeight: widget.height ?? double.infinity,
              ),
              child: Container(
                  color: Colors.white,
                  child: widget.child
              )
          ),
        );
      },
    );
  }
}

class TooltipButtonController{
  bool isEnable = true;
}