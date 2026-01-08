import 'dart:ui';

import 'package:flutter/material.dart';

class GlobalStyle{
  static Color primaryColor = const Color(0xff016E85);//const Color(0xff00BFB4);//Colors.black;
  static Color errorColor = const Color(0xffff0000);
  static Color disableColor = const Color(0xffd5d5d5);
  static Color hoverColor = const Color(0xffffdd66);
  static String fontFamily = "Inter";
  static double fontSize = 12;
  static Color fontColor = Colors.black;//const Color(0xff016E85);
  static Color headerColor = Colors.white;
  static TextStyle errorStyle = TextStyle(fontSize: 12, fontWeight: FontWeight.bold, color: errorColor);

  // CheckBox
  static Color checkBoxBackgroundColor = const Color(0xff9F9F9F);

  // EditText
  // static Color editTextBorderColor = const Color(0xffbababa);
  static Color editTextBorderColor = primaryColor.withOpacity(0.5);

  //DataGridExtender
  static Color gridColor = Colors.white;//const Color(0xff00BFB4); // yang diatass
  static Color gridBorderColor = Colors.white;//const Color(0xff016E85);//
  static Color gridFontColor = Colors.black;
  static Color gridItemColor = Colors.white;
  static Color gridItemAlternateColor = const Color(0xffF7F8F8);
  static Color gridHeaderTextColor = Colors.black;
  static Color gridHeaderColor = const Color(0xffd9d9d9);//const Color(0xff824C31);
  static Color gridColorSeparator = const Color(0xffd9d9d9);
  static Color listItemColor = const Color(0xff016E85);
  static Color listItemTextColor = Colors.white;

  //LabelExtender
  static Color labelColor = fontColor;
  static double labelFontSize = fontSize;

  //ToolbarBox
  static Color toolbarBackgroundColor = const Color(0xff824C31);
  static Color toolbarBorderColor = const Color(0xff016E85);
  static Color toolbarColor = Colors.white; //const Color(0xff016E85);
  static Color toolbarHoverColor = const Color(0xff496aff);
  static Color toolbarDisableColor = const Color(0xff85858);
  static double toolbarHeight = 60;
  static bool toolbarShowTitleDivider = false;

  //Breadxcrumb
  static Color crumbColor = const Color(0xff2233CC);

  //Lookup
  static double lookupTitleFontSize = 18;
  static Color lookupTitleFontColor = primaryColor;
  static bool lookupShowPaging = true;

  //ModalProgress
  static Color modalProgressTextColor = Colors.orange;
  static Color modalProgressColor = Colors.orange;

  // BorderRadius
  static BorderRadius defaultBorderRadius = BorderRadius.circular(10);

  //MaterialColor
  static MaterialColor primaryMaterialColor = _generateMaterialColor(primaryColor);
  static MaterialColor disableMaterialColor = _generateMaterialColor(disableColor);

  static double screenWidth = 0;
  static double screenHeight = 0;

  static double webControlWidth = 350;

  static MaterialColor _generateMaterialColor(Color myColor){
    int red = myColor.red;
    int green = myColor.green;
    int blue = myColor.blue;

    Map<int, Color> myColorCodes =
    {
      50: Color.fromRGBO(red, green, blue, .1),
      100:Color.fromRGBO(red, green, blue, .2),
      200:Color.fromRGBO(red, green, blue, .3),
      300:Color.fromRGBO(red, green, blue, .4),
      400:Color.fromRGBO(red, green, blue, .5),
      500:Color.fromRGBO(red, green, blue, .6),
      600:Color.fromRGBO(red, green, blue, .7),
      700:Color.fromRGBO(red, green, blue, .8),
      800:Color.fromRGBO(red, green, blue, .9),
      900:Color.fromRGBO(red, green, blue, 1),
    };

    return MaterialColor(myColor.value, myColorCodes);
  }
}