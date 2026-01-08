import 'package:flutter/foundation.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';

class ExpandablePanel extends StatefulWidget {
  final Widget header;
  final Widget child;
  bool isExpanded;

  ExpandablePanel({
    required this.header,
    required this.child,
    this.isExpanded = true
  });

  @override
  createState() => ExpandablePanelState();
}

class ExpandablePanelState extends State<ExpandablePanel> {
  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: MediaQuery.of(context).size.width - 30,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.start,
        mainAxisSize: MainAxisSize.max,
        children: [
          InkWell(
            onTap: (){
              setState(() {
                widget.isExpanded = !widget.isExpanded;
              });
            },
            child: Container(
              width: double.infinity,
              padding: const EdgeInsets.only(top: 5, bottom: 5, left: 10, right: 10),
              decoration: BoxDecoration(
                color: GlobalStyle.primaryColor,
              ),
              child: Row(
                mainAxisSize: MainAxisSize.max,
                children: [
                  widget.header,
                  // Expanded(
                  //   child: Container(
                  //     alignment: Alignment.centerRight,
                  //     child: Icon(widget.isExpanded ? Icons.keyboard_arrow_down : Icons.keyboard_arrow_up, color: Colors.white,),
                  //   ),
                  // ),
                ],
              ),
            ),
          ),
          Visibility(
            //visible: widget.isExpanded,
            child: Container(
              width: double.infinity,
              padding: const EdgeInsets.all(10),
              decoration: BoxDecoration(
                  border: Border.all(color: GlobalStyle.primaryColor)
              ),
              child: widget.child
            ),
          ),
        ],
      ),
    );

  }

}