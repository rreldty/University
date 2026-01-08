import 'dart:convert';
import 'package:flutter/services.dart';
import 'package:go_router/go_router.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../Common/Encryption.dart';
import 'HoverExtender.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';
import 'MessageBox.dart';

import '../Common/CommonMethod.dart';
import 'LookupPage.dart';

class CustomToolbar extends StatefulWidget implements PreferredSizeWidget {
  final List<Widget>? children;
  final Color? backgroundColor;
  final double height;
  final EdgeInsetsGeometry? padding;
  final EdgeInsetsGeometry? margin;
  final MainAxisAlignment? mainAxisAlignment;
  final CrossAxisAlignment? crossAxisAlignment;

  const CustomToolbar({
    super.key,
    this.children,
    this.backgroundColor,
    this.height = 60,
    this.padding,
    this.margin,
    this.mainAxisAlignment,
    this.crossAxisAlignment,
  });

  @override
  // TODO: implement preferredSize
  Size get preferredSize => Size.fromHeight(height);

  @override
  createState() => CustomToolbarState();
}

class CustomToolbarState extends State<CustomToolbar> {

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: widget.padding ?? EdgeInsets.only(top: 2),
      margin: widget.margin ??  EdgeInsets.only(left: 0),
      decoration: BoxDecoration(
        color: widget.backgroundColor,
      ),
      child: Row(
        mainAxisSize: MainAxisSize.max,
        mainAxisAlignment: widget.mainAxisAlignment ?? MainAxisAlignment.start,
        crossAxisAlignment: widget.crossAxisAlignment ?? CrossAxisAlignment.start,
        children: widget.children ?? [Container()],
      ),
    );
  }


}
