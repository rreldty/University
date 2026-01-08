import 'HoverExtender.dart';

import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';

class IconButtonExtender extends StatefulWidget {
  final IconButtonController? controller;
  final Function()? onPressed;
  final IconData? icon;
  final String? iconImagePath;
  final double? size;
  final bool? isEnable;
  final bool isVisible;
  final Color? hoverColor;
  final Color? disableColor;
  final Color? color;
  final String? tooltipText;
  final String? iconLabel;

  const IconButtonExtender({
    super.key,
    this.controller,
    this.icon,
    this.iconImagePath,
    this.size,
    this.onPressed,
    this.isEnable,
    this.isVisible = true,
    this.color,
    this.hoverColor,
    this.disableColor,
    this.tooltipText,
    this.iconLabel,
  }) : assert(icon != null || iconImagePath != null);

  @override
  createState() => IconButtonExtenderState();
}

class IconButtonExtenderState extends State<IconButtonExtender> {
  bool _isEnable = true;

  @override
  void initState() {
    super.initState();

    if(widget.isEnable != null) {
      _isEnable = widget.isEnable ?? true;
    }

    if(widget.controller != null){
      widget.controller!.isEnable = widget.isEnable ?? true;
    }
  }

  @override
  Widget build(BuildContext context) {
    if(widget.controller != null){
      _isEnable = widget.controller!.isEnable;
    }

    if(widget.isVisible){
      int intTapCount = 0;
      return HoverExtender(
        builder: (isHovered) {

          Widget? iconContent;
          Widget? iconText;
          Widget? iconImage;
          String strIconLabel = widget.iconLabel ?? "";

          Color hoverColor = widget.hoverColor ?? GlobalStyle.primaryColor;
          Color disableColor = widget.disableColor ?? GlobalStyle.disableColor;
          Color color = widget.color ??  Colors.black;
          Color hoverShadowColor = hoverColor.withOpacity(0.10);

          if(widget.iconImagePath != null){
            iconImage = ImageIcon(
              AssetImage(widget.iconImagePath!),
              size: widget.size,
              color: _isEnable ? (isHovered ? hoverColor : color) : disableColor,
            );
          }
          else {
            if(widget.icon != null){
              iconImage = Icon(
                widget.icon!,
                size: widget.size,
                color: _isEnable ? (isHovered ? hoverColor : color) : disableColor,
              );
            }else{
              iconImage = Container();
            }
          }

          if(strIconLabel.isNotEmpty){
            if (_isEnable) {
              iconText = Text(
                strIconLabel,
                style: TextStyle(
                    color: _isEnable ? (isHovered ? hoverColor : color) : disableColor,
                    fontSize: GlobalStyle.fontSize
                ),
              );

              // iconBackgroundColor =
              // isHovered ? GlobalStyle.toolbarColor : GlobalStyle
              //     .toolbarBackgroundColor;
            } else {
              iconText = Text(
                strIconLabel,
                style: TextStyle(
                    color: GlobalStyle.disableColor,
                    fontSize: GlobalStyle.fontSize
                ),
              );
            }
          }


          if(iconText != null){
            iconContent = SizedBox(
              height: 40,
              child: InkWell(
                onTap: () {
                  if(_isEnable) {
                    intTapCount++;
                    // debugPrint("onPressed $intTapCount");
                    if (intTapCount == 1) {
                      intTapCount = 0;
                      // debugPrint("onPressedAction");
                      if (widget.onPressed != null) {
                        widget.onPressed!();
                      }
                    }
                  }
                },
                child: Row(
                  mainAxisSize: MainAxisSize.min,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    iconImage,
                    iconText,
                  ],
                ),
              ),
            );
          }
          else{
            iconContent = Container(
              width: 40,
              height: 40,
              decoration: BoxDecoration(
                color: isHovered ? hoverShadowColor : null,
                borderRadius: const BorderRadius.all(Radius.elliptical(45,45)),
              ),
              child: InkWell(
                onTap: () {
                  if(_isEnable) {
                    intTapCount++;
                    // debugPrint("onPressed $intTapCount");
                    if (intTapCount == 1) {
                      intTapCount = 0;
                      // debugPrint("onPressedAction");
                      if (widget.onPressed != null) {
                        widget.onPressed!();
                      }
                    }
                  }
                },
                child: iconImage,
              ),
            );
          }

          if(widget.tooltipText != null){
            return Tooltip(
              message: widget.tooltipText,
              child: iconContent,
            );
          }

          return iconContent;
        },
      );
    }else{
      return Container();
    }

  }

}

class IconButtonController{
  bool isEnable = true;
  bool isVisible = true;
}