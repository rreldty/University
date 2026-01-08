import 'dart:convert';

import 'package:auto_size_text/auto_size_text.dart';
import 'package:collection/collection.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../../Dto/Base/GlobalDto.dart';
import '../../Dto/Zystem/ZAUTDto.dart';
import 'HoverExtender.dart';
import '../../Common/CommonMethod.dart';
import '../../Common/GlobalStyle.dart';
import '../../Dto/Base/MenuNodeDto.dart';

class AccordionMenu extends StatefulWidget {
  final List<MenuNodeDto>? menuNodes;
  final Function(dynamic)? menuOnClik;

  const AccordionMenu({
    super.key,
    this.menuNodes,
    this.menuOnClik,
  });

  @override
  createState() => AccordionMenuState();
}

class AccordionMenuState extends State<AccordionMenu> {
  final ScrollController _scrollController = ScrollController();
  final accordionColor = GlobalStyle.primaryColor;
  final accordionTitleColor = Colors.white;
  final accordionBodyColor = Colors.white;
  final accordionBodyTitleColor = GlobalStyle.primaryColor;
  final accordionBodyTileColor = Colors.white;
  final hoverTitleColor = Colors.white;
  final hoverTileColor = const Color(0xffE7E7E7);
  final tapTileColor = GlobalStyle.primaryColor;

  Map<String, bool> lstPanelExpanded = {};
  // Map<String, bool> lstHoverTile = {};
  // Map<String, dynamic> lstTapTile = {};
  String strTapedMenu = "";

  late SharedPreferences prefs;

  @override
  void initState() {
    if (widget.menuNodes!.isNotEmpty) {
      for (int n = 0; n < widget.menuNodes!.length; n++) {
        if (n == 0) {
          lstPanelExpanded[widget.menuNodes![n].key] = true;
        } else {
          lstPanelExpanded[widget.menuNodes![n].key] = false;
        }

        // if (widget.menuNodes![n].children!.isNotEmpty) {
        //   for (int m = 0; m < widget.menuNodes![n].children!.length; m++) {
        //     // lstHoverTile[widget.menuNodes![n].children![m].key] = false;
        //     lstTapTile[widget.menuNodes![n].children![m].key] = false;
        //   }
        // }
      }
    }

    super.initState();
  }

  @override
  void didChangeDependencies() async {
    // TODO: implement didChangeDependencies
    super.didChangeDependencies();

    SharedPreferences prefs = await SharedPreferences.getInstance();
    String strRouteActive = prefs.getString("routeActive") ?? "";

    if (GlobalDto.listAuthority.isNotEmpty && strRouteActive.isNotEmpty) {
      Iterable list = json.decode(GlobalDto.listAuthority);
      List<ZAUTDto> lstZT = list.map((e) => ZAUTDto.fromJson(e)).toList();
      ZAUTDto? objZT = lstZT.firstWhereOrNull((objZT) {
        String strRouteMenu =
            (!objZT.ZPPURL.toString().startsWith("/") ? "/" : "") +
                objZT.ZPPURL.replaceAll(".xaml", "");
        return strRouteMenu.toLowerCase() == strRouteActive.toLowerCase();
      });
      strTapedMenu = objZT?.ZTMENO ?? "";
    }
  }

  void onExpansionClick(String key) {
    setState(() {
      if (lstPanelExpanded.isNotEmpty) {
        for (String strKey in lstPanelExpanded.keys) {
          if (strKey == key) {
            if (!(lstPanelExpanded[strKey] as bool)) {
              lstPanelExpanded[strKey] = true;
            }
          } else {
            lstPanelExpanded[strKey] = false;
          }
        }
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: accordionColor,
        border: Border(
          right: BorderSide(
              color: accordionColor, style: BorderStyle.solid, width: 0.5),
        ),
      ),
      child: Column(
        mainAxisSize: MainAxisSize.max,
        children: widget.menuNodes!.map((menuNode) {
          return AccordionMenuPanel(menuNode);
        }).toList(),
      ),
    );
  }

  Widget AccordionMenuPanel(MenuNodeDto parentNode) {
    Widget accordionPanel = Container(
      decoration: const BoxDecoration(
        border: Border(
          bottom: BorderSide(
              color: Colors.white54, style: BorderStyle.solid, width: 0.5),
        ),
      ),
      child: Column(
        children: [
          InkWell(
            onTap: () => onExpansionClick(parentNode.key),
            child: Padding(
              padding: const EdgeInsets.all(8.0),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Text(
                    parentNode.label,
                    style: TextStyle(
                        color: accordionTitleColor,
                        fontWeight: FontWeight.w700,
                        fontSize: GlobalStyle.fontSize),
                  ),
                  Icon(
                      lstPanelExpanded[parentNode.key]!
                          ? Icons.keyboard_arrow_down_outlined
                          : Icons.keyboard_arrow_up_outlined,
                      color: accordionTitleColor),
                ],
              ),
            ),
          ),
          Visibility(
            visible: lstPanelExpanded[parentNode.key]!,
            child: Expanded(
              child: Container(
                color: accordionBodyColor,
                height: double.infinity,
                child: Scrollbar(
                  controller: _scrollController,
                  thumbVisibility: true,
                  child: SingleChildScrollView(
                    controller: _scrollController,
                    child: Container(
                      alignment: Alignment.topLeft,
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisSize: MainAxisSize.max,
                        children: parentNode.children!.map((menuNode) {
                          return AccordionMenuTile(menuNode);
                        }).toList(),
                      ),
                    ),
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
    );

    if (lstPanelExpanded[parentNode.key]!) {
      return Expanded(child: accordionPanel);
    } else {
      return accordionPanel;
    }
  }

  Widget AccordionMenuTile(MenuNodeDto childNode) {
    return Padding(
      padding: const EdgeInsets.only(top: 2),
      child: InkWell(
        onTap: () async {
          widget.menuOnClik!(childNode.data);
          strTapedMenu = childNode.key;
          print(strTapedMenu);
        },
        child: HoverExtender(
          builder: (bool isHovered) {
            return SizedBox(
                height: 30,
                child: Row(
                  children: [
                    sideTile(isHovered, strTapedMenu == childNode.key),
                    Expanded(
                      child: Container(
                        color: strTapedMenu == childNode.key
                            ? tapTileColor
                            : hoverTileColor,
                        width: double.infinity,
                        height: double.infinity,
                        padding: const EdgeInsets.only(left: 8, top: 8),
                        child: AutoSizeText(
                          childNode.label,
                          style: TextStyle(
                            color: strTapedMenu == childNode.key
                                ? hoverTileColor
                                : tapTileColor,
                            fontSize: GlobalStyle.fontSize,
                          ),
                          maxLines: 1,
                          overflow: TextOverflow.ellipsis,
                        ),
                      ),
                    ),
                  ],
                ));
          },
        ),
      ),
    );
  }

  Widget sideTile(bool isHovered, bool isTaped) {
    if (isHovered || isTaped) {
      return Container(
        width: 8,
        height: double.infinity,
        color: tapTileColor,
      );
    } else {
      return Container();
    }
  }
}
