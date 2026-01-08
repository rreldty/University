import 'package:flutter/material.dart';

class ScrollExtender extends StatefulWidget {
  final Widget child;

  ScrollExtender({
    required this.child,
  });

  @override
  createState() => ScrollExtenderState();
}

class ScrollExtenderState extends State<ScrollExtender> {
  final ScrollController _scrollControllerVertical = ScrollController();
  final ScrollController _scrollControllerHorizontal = ScrollController();

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        width: double.infinity,
        height: double.infinity,
        child: Scrollbar(
            controller: _scrollControllerVertical,
            thumbVisibility: true,
            trackVisibility: true,
            child: SingleChildScrollView(
                scrollDirection: Axis.vertical,
                controller: _scrollControllerVertical,
                child: Scrollbar(
                  controller: _scrollControllerHorizontal,
                  thumbVisibility: true,
                  trackVisibility: true,
                  child: SingleChildScrollView(
                      scrollDirection: Axis.horizontal,
                      controller: _scrollControllerHorizontal,
                      child: widget.child
                  ),
                )
            )
        )
    );
  }
}
