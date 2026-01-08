import 'package:flutter/material.dart';

class HoverExtender extends StatefulWidget {

  final Widget Function(bool isHovered) builder;

  const HoverExtender({Key? key, required this.builder}) : super(key: key);

  @override
  HoverExtenderState createState() => HoverExtenderState();
}

class HoverExtenderState extends State<HoverExtender> {
  bool isHovered = false;

  @override
  Widget build(BuildContext context) {
    return MouseRegion(
      onEnter: (_)=> onEntered(true),
      onExit: (_)=> onEntered(false),
      child: AnimatedContainer(
        duration: const Duration(milliseconds: 300),
        child: widget.builder(isHovered),
      ),
    );
  }

  void onEntered(bool isHovered){
    setState(() {
      this.isHovered = isHovered;
    });
  }
}