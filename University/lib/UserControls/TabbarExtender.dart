import 'package:flutter/rendering.dart';

import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';

import 'HoverExtender.dart';

class TabbarExtender extends StatefulWidget {
  final List<TabbarItem> children;
  final double minHeight;

  const TabbarExtender({
    super.key,
    required this.children,
    this.minHeight = 400,
  });

  @override
  createState() => TabbarExtenderState();
}

class TabbarExtenderState extends State<TabbarExtender> with SingleTickerProviderStateMixin {
  int currentIndex = 0;
  final ScrollController _scrollControllerHorizontal = ScrollController();

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    if(widget.children.isNotEmpty){
      TabbarItem tabItem1 = widget.children[currentIndex];
      if(!tabItem1.isVisible || !tabItem1.isEnable){
        for(int n = 0; n < widget.children.length; n++){
          TabbarItem tabItem2 = widget.children[n];
          if(tabItem2.isVisible && tabItem2.isEnable){
            currentIndex = n;
            break;
          }
        }
      }
    }

    return Container(
      margin: const EdgeInsets.only(top: 5),
      child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            SizedBox(
              width: MediaQuery.of(context).size.width - 100,
              child: Scrollbar(
                controller: _scrollControllerHorizontal,
                child: SingleChildScrollView(
                  scrollDirection: Axis.horizontal,
                  controller: _scrollControllerHorizontal,
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: widget.children.map((obj){
                      if(obj.isVisible) {
                        if(obj.isEnable) {
                          return HoverExtender(
                            builder: (bool isHovered) {
                              return InkWell(
                                  onTap: () {
                                    setState(() {
                                      currentIndex = widget.children.indexOf(obj);
                                    });
                                  },
                                  child: builderTabHeader(obj, isHovered)
                              );
                            },
                          );
                        }
                        return builderTabHeader(obj, false);
                      }
                      return Container();
                    }).toList(),
                  ),
                ),
              ),
            ),
            IndexedStack(
              index: currentIndex,
              children: widget.children.map((obj){
                return ConstrainedBox(
                  constraints: BoxConstraints(
                      minWidth: MediaQuery.of(context).size.width - 100,
                      minHeight: widget.minHeight
                  ),
                  child: Container(
                    alignment: Alignment.topLeft,
                    decoration: BoxDecoration(
                      border: Border.all(
                          color: Colors.black26, //GlobalStyle.primaryColor,
                          width: 1
                      ),
                      borderRadius: const BorderRadius.only(bottomLeft: Radius.circular(10), bottomRight: Radius.circular(10), topRight: Radius.circular(10)),
                    ),
                    padding: const EdgeInsets.all(5),
                    child: obj.content,
                  ),
                );
              }).toList(),
            ),
          ]
      ),
    );
  }

  Widget builderTabHeader(TabbarItem obj, bool isHovered){
    Color colorTitle = Colors.black;

    if(obj.isEnable){
      if(currentIndex == widget.children.indexOf(obj)){
        colorTitle = GlobalStyle.primaryColor;
      }
      if(isHovered){
        colorTitle = GlobalStyle.primaryColor;
      }
    }else{
      colorTitle = Colors.black26;
    }

    return ConstrainedBox(
      constraints: const BoxConstraints(
          minWidth: 100
      ),
      child: Container(
          padding: const EdgeInsets.fromLTRB(
              10, 4, 10, 4),
          margin: const EdgeInsets.fromLTRB(
              0, 3, 0, 3),
          alignment: Alignment.center,
          decoration: const BoxDecoration(
            // color: Colors.white,
            // color: currentIndex ==
            //     widget.children.indexOf(obj) ? GlobalStyle
            //     .primaryColor : (obj.isEnable ? Colors.white : GlobalStyle.disableColor),
            // borderRadius: const BorderRadius.only(
            //     topLeft: Radius.circular(10),
            //     topRight: Radius.circular(10)),
            border: Border(right: BorderSide(color: Colors.black26)),
          ),
          child: Text(
            obj.title,
            style: TextStyle(
              fontFamily: GlobalStyle.fontFamily,
              fontSize: GlobalStyle.fontSize,
              color: colorTitle,
            ),
          )
      ),
    );
  }
}

class TabbarItem{
  final String title;
  final Widget content;
  final bool isVisible;
  final bool isEnable;

  TabbarItem({
    this.title = "",
    required this.content,
    this.isVisible = true,
    this.isEnable = true,
  });
}