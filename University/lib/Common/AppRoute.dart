import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:university/Views/Training/Fakultas.dart';
import 'package:university/Views/Training/Jurusan.dart';
import 'package:university/Views/Training/MataKuliah.dart';

import '../../Views/Controls/BlankPage.dart';
import '../../Views/Controls/ErrorPage.dart';
import '../../Views/Controls/Login.dart';
import '../../Views/Controls/MainHome.dart';


import '../../Views/Xample/XM010A_Application.dart';
import '../../Views/Xample/XM050A_UserGroup.dart';
import '../UserControls/LoaderPage.dart';
import '../Views/Controls/LandingPage.dart';
import '../Views/Controls/MainApp.dart';

import '../UserControls/PowerBIViewer.dart';

class AppRoute {
  Map<String, Widget> getRouteList() {
    Map<String, Widget> route = {};

    route = {
      Fakultas.route : Fakultas(),
      Jurusan.route : Jurusan(),
      MataKuliah.route : MataKuliah(),

    };

    Map<String, Widget> routeCustom = {};
    route.forEach((key, value) {
      String strKey = key.startsWith("/") ? key : "/$key";
      routeCustom[strKey] = value;
    });

    return routeCustom;
  }

  Map<String, Widget> getParentRouteList() {
    //DO NOT CHANGE

    Map<String, Widget> route = {};

    route = {
      MainApp.route: const MainApp(),
      //MainHome.route: const MainHome(navigationShell: null,),
      ErrorPage.route: const ErrorPage(),
      // Login.route: Login(),
      PowerBIViewer.route: const PowerBIViewer(),
      BlankPage.route: const BlankPage(),
      LoaderPage.route: const LoaderPage(),
      LandingPage.route: LandingPage(),

      XM010A_Application.route: XM010A_Application(),
      XM050A_UserGroup.route: XM050A_UserGroup(),
    };

    Map<String, Widget> routeCustom = {};
    route.forEach((key, value) {
      String strKey = key.startsWith("/") ? key : "/$key";
      routeCustom[strKey] = value;
    });

    return routeCustom;
  }
}
