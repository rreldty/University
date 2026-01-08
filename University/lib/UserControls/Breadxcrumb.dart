
import 'dart:core';
import 'package:go_router/go_router.dart';

import '../Common/AppConfig.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';

import 'HoverExtender.dart';

class Breadxcrumb extends StatefulWidget implements PreferredSizeWidget {
  final BreadxcrumbController? controller;
  final List<CrumbItem>? crumbItems;
  final bool isBackVisible;
  final bool isBackEnable;
  final bool isHomeVisible;

  Breadxcrumb({
    this.controller,
    this.crumbItems,
    this.isBackVisible = true,
    this.isBackEnable = true,
    this.isHomeVisible = true,
  });

  @override
  // TODO: implement preferredSize
  Size get preferredSize => const Size.fromHeight(40);

  @override
  createState() => BreadxcrumbState();
}

class BreadxcrumbState extends State<Breadxcrumb> {

  @override
  void initState() {

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.only(left: 10, top: 10),
      margin: const EdgeInsets.only(left: 0),
      child: Row(
        mainAxisSize: MainAxisSize.max,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          backBuilder(),
          Expanded(
            child: Container(
              padding: const EdgeInsets.fromLTRB(10, 2, 0, 0),
              child: ListView(
                scrollDirection: Axis.horizontal,
                children: crumbBuilder(),
              ),
            ),
          ),

        ],
      ),
    );
  }

  Widget backBuilder(){
    if((widget.crumbItems?.isNotEmpty ?? false) && widget.isBackVisible){
      return Container(
          padding: const EdgeInsets.fromLTRB(3, 0, 8, 0),
          child: toolbarIcon(iconData: Icons.arrow_back, iconLabel: "Back", isVisible: widget.isBackVisible, isEnable: widget.isBackEnable, onTapAction: (){context.pop();})
      );
    }
    return Container();
  }

  List<Widget> crumbBuilder(){
    List<Widget> lstCrumb = [];
    List<CrumbItem> crumbItems = widget.crumbItems ?? [];

    if(widget.isHomeVisible) {
      lstCrumb.add(crumbNavigation(
          label: "Home",
          onTap: () {
            context.go(AppConfig.crumbHome);
          },
          color: GlobalStyle.crumbColor
      ));
    }

    for(int n = 0; n < crumbItems.length; n++){
      if(lstCrumb.isNotEmpty){
        lstCrumb.add(crumbDivider());
      }
      lstCrumb.add(crumbNavigation(
          label: crumbItems[n].label,
          onTap: crumbItems[n].onTap,
          color: (crumbItems[n].color != null ? crumbItems[n].color : GlobalStyle.crumbColor)
      ));
    }

    return lstCrumb;
  }

  Widget crumbDivider(){
    return Container(
      padding: const EdgeInsets.fromLTRB(5, 0, 5, 0),
      child: Text(">", style: TextStyle(fontSize: GlobalStyle.fontSize, color: GlobalStyle.crumbColor),),
    );
  }

  Widget crumbNavigation({String? label, Function()? onTap, Color? color}){
    int intTapCount = 0;
    return InkWell(
        onTap: () {
          intTapCount++;
          // debugPrint("onTap $intTapCount");
          if(intTapCount == 1){
            // debugPrint("onTapAction");
            if (onTap != null) {
              onTap();
            }
          }
        },
        hoverColor: Colors.transparent,
        child: Text(label ?? "", style: TextStyle(fontSize: GlobalStyle.fontSize, color: color ),)
    );
  }

  Widget toolbarIcon({
    IconData? iconData,
    bool isVisible = false,
    bool isEnable = true,
    String iconLabel = "",
    Function()? onTapAction
  })
  {

    return Visibility(
      visible: isVisible,
      child: HoverExtender(
        builder: (isHovered) {
          Widget? iconContent;

          iconContent = Tooltip(
            message: iconLabel,
            child: Container(
              padding: const EdgeInsets.fromLTRB(2, 0, 2, 2),
              child: Icon(
                iconData,
                size: 20,
                color: isEnable ? (isHovered ? GlobalStyle.toolbarHoverColor : Colors.black) : GlobalStyle.toolbarDisableColor,
              ),
            ),
          );

          if(isEnable){
            int intTapCount = 0;
            return InkWell(
                onTap: () {
                  intTapCount++;
                  // debugPrint("onTap $intTapCount");
                  if(intTapCount == 1){
                    // debugPrint("onTapAction");
                    if (onTapAction != null) {
                      onTapAction();
                    }
                  }
                },
                child: iconContent
            );
          }

          return iconContent;
        },
      ),
    );
  }
}

class CrumbItem{
  final String? label;
  final Function()? onTap;
  final Color? color;

  CrumbItem({
    this.label,
    this.onTap,
    this.color,
  });
}

class BreadxcrumbController{

}