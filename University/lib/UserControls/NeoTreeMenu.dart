import 'dart:convert';

import 'package:collection/collection.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../../Dto/Base/GlobalDto.dart';
import '../../Dto/Zystem/ZAUTDto.dart';
import 'HoverExtender.dart';
import '../../Common/GlobalStyle.dart';
import '../../Dto/Base/MenuNodeDto.dart';

class NeoTreeMenu extends StatefulWidget {
  final List<MenuNodeDto>? menuNodes;
  final Function(dynamic) menuOnClik;
  late bool showMenu;
  final double width;
  final double height;
  final bool showMinimizeIcon;
  final Color? backgroundColor;
  final bool showMenuBar;
  final String imagePathMenuBar;
  final Color? menuBarColor;
  final Color? menuHeaderColor;
  final Color? menuItemColor;

  NeoTreeMenu({
    super.key,
    this.menuNodes,
    required this.menuOnClik,
    this.showMenu = true,
    required this.width,
    required this.height,
    this.showMinimizeIcon = false,
    this.backgroundColor,
    this.showMenuBar = true,
    this.imagePathMenuBar = "assets/icons/menubar.png",
    this.menuBarColor = Colors.white,
    this.menuHeaderColor = Colors.white,
    this.menuItemColor = Colors.white,
  });

  @override
  createState() => NeoTreeMenuState();
}

class NeoTreeMenuState extends State<NeoTreeMenu> {
  final ScrollController _scrollControllerMinimize = ScrollController();
  final ScrollController _scrollControllerMaximize = ScrollController();
  final accordionColor = GlobalStyle.primaryColor;
  final accordionTitleColor = GlobalStyle.primaryColor; //Colors.white;
  final accordionBodyColor = Colors.white;
  final accordionBodyTitleColor = GlobalStyle.primaryColor;
  final accordionBodyTileColor = Colors.white;
  final hoverTitleColor = Colors.white;
  final hoverTileColor = GlobalStyle.primaryColor;
  final tappedTileColor = GlobalStyle.primaryColor;

  Map<String, bool>? lstPanelExpanded;
  // Map<String, bool> lstHoverTile = {};
  // Map<String, dynamic> lstTapTile = {};
  String strTapedMenu = "";

  late SharedPreferences prefs;

  @override
  void initState() {
    // if(widget.menuNodes != null){
    //   if (widget.menuNodes!.isNotEmpty) {
    //     for (int n = 0; n < widget.menuNodes!.length; n++) {
    //       lstPanelExpanded[widget.menuNodes![n].key] = true;
    //       // if (n == 0) {
    //       //   lstPanelExpanded[widget.menuNodes[n].key] = true;
    //       // } else {
    //       //   lstPanelExpanded[widget.menuNodes[n].key] = false;
    //       // }
    //
    //       // if (widget.menuNodes[n].children!.isNotEmpty) {
    //       //   for (int m = 0; m < widget.menuNodes[n].children!.length; m++) {
    //       //     // lstHoverTile[widget.menuNodes[n].children![m].key] = false;
    //       //     lstTapTile[widget.menuNodes[n].children![m].key] = false;
    //       //   }
    //       // }
    //     }
    //
    //   }
    // }

    super.initState();
  }

  @override
  void didChangeDependencies() async {
    // TODO: implement didChangeDependencies
    super.didChangeDependencies();

    SharedPreferences prefs = await SharedPreferences.getInstance();
    String strRouteActive = prefs.getString("routeActive") ?? "";

    if (widget.menuNodes != null) {
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

    // if(widget.menuNodes != null){
    //   if (widget.menuNodes!.isNotEmpty) {
    //     debugPrint("didChangeDependencies");
    //
    //     lstPanelExpanded = {};
    //     for (int n = 0; n < widget.menuNodes!.length; n++) {
    //       lstPanelExpanded[widget.menuNodes![n].key] = true;
    //       // if (n == 0) {
    //       //   lstPanelExpanded[widget.menuNodes[n].key] = true;
    //       // } else {
    //       //   lstPanelExpanded[widget.menuNodes[n].key] = false;
    //       // }
    //
    //       // if (widget.menuNodes[n].children!.isNotEmpty) {
    //       //   for (int m = 0; m < widget.menuNodes[n].children!.length; m++) {
    //       //     // lstHoverTile[widget.menuNodes[n].children![m].key] = false;
    //       //     lstTapTile[widget.menuNodes[n].children![m].key] = false;
    //       //   }
    //       // }
    //     }
    //
    //   }
    // }
  }

  void onExpansionClick(String key) {
    setState(() {
      if (lstPanelExpanded!.isNotEmpty) {
        if (widget.showMinimizeIcon && !widget.showMenu) {
          lstPanelExpanded![key] = true;
          widget.showMenu = true;
        } else {
          lstPanelExpanded![key] = !lstPanelExpanded![key]!;
        }

        // for (String strKey in lstPanelExpanded.keys) {
        //   if (strKey == key) {
        //     if (!(lstPanelExpanded[strKey] as bool)) {
        //       lstPanelExpanded[strKey] = true;
        //     }
        //   } else {
        //     lstPanelExpanded[strKey] = false;
        //   }
        // }
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Stack(
          children: [
            //Minimize
            Visibility(
              visible: widget.showMinimizeIcon ? !widget.showMenu : false,
              child: SizedBox(
                width: 60,
                height: widget.height,
                child: Scrollbar(
                  controller: _scrollControllerMinimize,
                  thumbVisibility: true,
                  trackVisibility: true,
                  child: Container(
                    decoration: BoxDecoration(
                      color: widget.backgroundColor ?? Colors.white,
                      //border: Border(right: BorderSide(color: Colors.black26, width: 0.5)),
                    ),
                    child: ListView(
                      controller: _scrollControllerMinimize,
                      children: builderMenuNode(true),
                    ),
                  ),
                ),
              ),
            ),
            //Maximize
            Visibility(
              visible: widget.showMenu,
              child: SizedBox(
                width: widget.width,
                height: widget.height,
                child: Scrollbar(
                  controller: _scrollControllerMaximize,
                  thumbVisibility: true,
                  trackVisibility: true,
                  child: Container(
                    decoration: BoxDecoration(
                      color: widget.backgroundColor ?? Colors.white,
                      border: Border(
                          right: BorderSide(color: Colors.black26, width: 0.5)),
                    ),
                    child: ListView(
                      controller: _scrollControllerMaximize,
                      children: builderMenuNode(false),
                    ),
                  ),
                ),
              ),
            ),
            // SizedBox(
            //   width: 60,
            //   height: 60,
            //   child: Container(
            //     color: widget.backgroundColor ?? Colors.white,
            //     //margin: const EdgeInsets.only(left: 3),
            //     // decoration: const BoxDecoration(
            //     //   border: Border(right: BorderSide(width: 1, color: Color(0xFFe5eaee))),
            //     // ),
            //     child: TextButton(
            //       style: TextButton.styleFrom(
            //         padding: const EdgeInsets.all(0),
            //         minimumSize: const Size(100, 100),
            //         alignment: Alignment.center,
            //       ),
            //       onPressed: () {
            //         widget.visible = !widget.visible;
            //       },
            //       child: Image.asset(
            //         "assets/icons/menubar.png",
            //         width: 40,
            //         height: 40,
            //       ),
            //     ),
            //   ),
            // ),
          ],
        ),
      ],
    );
    // return Stack(
    //   children: [
    //     //Minimize
    //     Visibility(
    //       visible: widget.showMinimizeIcon ? !widget.showMenu : false,
    //       child: SizedBox(
    //         width: 60,
    //         height: widget.height,
    //         child: Scrollbar(
    //           controller: _scrollControllerMinimize,
    //           thumbVisibility: true,
    //           trackVisibility: true,
    //           child: Container(
    //             decoration: BoxDecoration(
    //               color: widget.backgroundColor ?? Colors.white,
    //               //border: Border(right: BorderSide(color: Colors.black26, width: 0.5)),
    //             ),
    //             child: ListView(
    //               controller: _scrollControllerMinimize,
    //               children: builderMenuNode(true),
    //             ),
    //           ),
    //         ),
    //       ),
    //     ),
    //     //Maximize
    //     Visibility(
    //       visible: widget.showMenu,
    //       child: SizedBox(
    //         width: widget.width,
    //         height: widget.height,
    //         child: Scrollbar(
    //           controller: _scrollControllerMaximize,
    //           thumbVisibility: true,
    //           trackVisibility: true,
    //           child: Container(
    //             decoration: BoxDecoration(
    //               color: widget.backgroundColor ?? Colors.white,
    //               border: Border(right: BorderSide(color: Colors.black26, width: 0.5)),
    //             ),
    //             child: ListView(
    //               controller: _scrollControllerMaximize,
    //               children: builderMenuNode(false),
    //             ),
    //           ),
    //         ),
    //       ),
    //     ),
    //     // SizedBox(
    //     //   width: 60,
    //     //   height: 60,
    //     //   child: Container(
    //     //     color: widget.backgroundColor ?? Colors.white,
    //     //     //margin: const EdgeInsets.only(left: 3),
    //     //     // decoration: const BoxDecoration(
    //     //     //   border: Border(right: BorderSide(width: 1, color: Color(0xFFe5eaee))),
    //     //     // ),
    //     //     child: TextButton(
    //     //       style: TextButton.styleFrom(
    //     //         padding: const EdgeInsets.all(0),
    //     //         minimumSize: const Size(100, 100),
    //     //         alignment: Alignment.center,
    //     //       ),
    //     //       onPressed: () {
    //     //         widget.visible = !widget.visible;
    //     //       },
    //     //       child: Image.asset(
    //     //         "assets/icons/menubar.png",
    //     //         width: 40,
    //     //         height: 40,
    //     //       ),
    //     //     ),
    //     //   ),
    //     // ),
    //   ],
    // );
  }

  List<Widget> builderMenuNode(bool isMenuMini) {
    List<Widget> lstMenuTree = [];

    if (widget.showMenuBar) {
      lstMenuTree.add(neoTreeMenuBar(widget.imagePathMenuBar, isMenuMini));
    }

    if (widget.menuNodes != null) {
      if (widget.menuNodes!.isNotEmpty) {
        if (lstPanelExpanded == null) {
          lstPanelExpanded = {};
          for (int n = 0; n < widget.menuNodes!.length; n++) {
            lstPanelExpanded![widget.menuNodes![n].key] = true;
          }
        }

        lstMenuTree.addAll(widget.menuNodes!.map((menuNode) {
          return neoTreeMenuPanel(menuNode, isMenuMini);
        }).toList());
      }
    }

    return lstMenuTree;
  }

  Widget neoTreeMenuBar(String strAssetPath, bool isMenuMini) {
    Widget iconMenuImage = ImageIcon(
      AssetImage(strAssetPath),
      size: 20,
      color: widget.menuBarColor,
    );

    if (isMenuMini) {
      return Container(
        padding: const EdgeInsets.fromLTRB(8, 3, 20, 3),
        child: Column(
          children: [
            InkWell(
              onTap: () {
                setState(() {
                  widget.showMenu = !widget.showMenu;
                });
              },
              child: Container(
                padding: const EdgeInsets.fromLTRB(0, 3, 0, 3),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    iconMenuImage,
                  ],
                ),
              ),
            ),
          ],
        ),
      );
    }

    return Container(
      padding: const EdgeInsets.fromLTRB(8, 3, 20, 3),
      child: Column(
        children: [
          InkWell(
            onTap: () {
              setState(() {
                widget.showMenu = !widget.showMenu;
              });
            },
            child: Container(
              padding: const EdgeInsets.fromLTRB(0, 3, 0, 3),
              // decoration: const BoxDecoration(
              //   border: Border(bottom: BorderSide(width: 0.5, color: Colors.black26)),
              // ),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  iconMenuImage,
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget neoTreeMenuPanel(MenuNodeDto parentNode, bool isMenuMini) {
    Widget iconMenuImage = Icon(
      Icons.art_track_sharp,
      color: widget.menuHeaderColor,
    );

    ZAUTDto objZT = parentNode.data as ZAUTDto;

    // if(objZT.ZMIURL.isNotEmpty){
    //   iconMenuImage = ImageIcon(
    //     AssetImage("assets/icons/${objZT.ZMIURL}"),
    //     size: 20,
    //     // color: widget.menuHeaderColor,
    //   );
    // }

    if (objZT.ZMIURL.isNotEmpty) {
      iconMenuImage = SizedBox(
        width: 20,
        height: 20,
        child: Image.asset("assets/icons/${objZT.ZMIURL}"),
      );
    }

    if (isMenuMini) {
      return Container(
        padding: const EdgeInsets.fromLTRB(8, 3, 20, 3),
        child: Column(
          children: [
            InkWell(
              onTap: () => onExpansionClick(parentNode.key),
              child: Container(
                padding: const EdgeInsets.fromLTRB(0, 3, 0, 3),
                decoration: const BoxDecoration(
                  border: Border(
                      bottom: BorderSide(width: 0.5, color: Colors.black26)),
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    iconMenuImage,
                    // Text(
                    //   parentNode.label,
                    //   style: TextStyle(
                    //       color: Colors.black,
                    //       fontWeight: FontWeight.w700,
                    //       fontSize: GlobalStyle.fontSize),
                    // ),
                    // Icon(
                    //     lstPanelExpanded[parentNode.key]!
                    //         ? Icons.keyboard_arrow_up
                    //         : Icons.keyboard_arrow_right,
                    //     color: Colors.black),
                  ],
                ),
              ),
            ),
          ],
        ),
      );
    }

    return Container(
      padding: const EdgeInsets.fromLTRB(8, 3, 20, 3),
      child: Column(
        children: [
          InkWell(
            onTap: () => onExpansionClick(parentNode.key),
            child: Container(
              padding: const EdgeInsets.fromLTRB(0, 3, 0, 3),
              decoration: const BoxDecoration(
                border: Border(
                    bottom: BorderSide(width: 0.5, color: Colors.black26)),
              ),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  iconMenuImage,
                  Text(
                    parentNode.label,
                    style: TextStyle(
                        color: GlobalStyle.primaryColor,
                        fontWeight: FontWeight.w700,
                        fontSize: GlobalStyle.fontSize),
                  ),
                  Icon(
                    lstPanelExpanded![parentNode.key]!
                        ? Icons.keyboard_arrow_up
                        : Icons.keyboard_arrow_right,
                    color: GlobalStyle.primaryColor,
                  ),
                ],
              ),
            ),
          ),
          Visibility(
            visible: lstPanelExpanded![parentNode.key]!,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisSize: MainAxisSize.max,
              children: builderTreeMenuParent(parentNode),
            ),
          ),
        ],
      ),
    );
  }

  List<Widget> builderTreeMenuParent(MenuNodeDto parentNode) {
    List<Widget> lstMenuTile = [];

    if (parentNode.children != null) {
      double dblIndent = 0;
      for (int n = 0; n < parentNode.children!.length; n++) {
        MenuNodeDto childNode = parentNode.children![n];
        lstMenuTile.add(neoTreeMenuTile(childNode, dblIndent));

        builderTreeMenuChildren(childNode, dblIndent, lstMenuTile);
      }
    }

    return lstMenuTile;
  }

  builderTreeMenuChildren(
      MenuNodeDto parentNode, double dblIndent, List<Widget> lstMenuTile) {
    if (parentNode.children != null) {
      dblIndent = dblIndent + 5;
      for (int n = 0; n < parentNode.children!.length; n++) {
        MenuNodeDto childNode = parentNode.children![n];
        lstMenuTile.add(neoTreeMenuTile(childNode, dblIndent));

        builderTreeMenuChildren(childNode, dblIndent, lstMenuTile);
      }
    }
  }

  Widget neoTreeMenuTile(MenuNodeDto childNode, double dblIndent) {
    return InkWell(
      onTap: () async {
        widget.menuOnClik(childNode.data);
        strTapedMenu = childNode.key;
      },
      child: HoverExtender(
        builder: (bool isHovered) {
          return SizedBox(
              height: 30,
              child: Row(
                children: [
                  //sideTile(isHovered, strTapedMenu == childNode.key),
                  Expanded(
                    child: Container(
                      //color: strTapedMenu == childNode.key ? tapTileColor : hoverTileColor,
                      width: double.infinity,
                      height: double.infinity,
                      padding: EdgeInsets.only(
                          left: 8.0 + dblIndent, top: 3, bottom: 3),
                      child: Text(
                        childNode.label,
                        style: TextStyle(
                          color: strTapedMenu == childNode.key
                              ? tappedTileColor
                              : widget.menuItemColor,
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
    );
  }

  Widget sideTile(bool isHovered, bool isTaped) {
    if (isHovered || isTaped) {
      return Container(
        width: 8,
        height: double.infinity,
        color: tappedTileColor,
      );
    } else {
      return Container();
    }
  }
}
