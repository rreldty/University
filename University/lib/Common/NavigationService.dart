import 'package:university/Views/Controls/BlankPage.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import '../../Common/AppRoute.dart';
import '../../Views/Controls/Login.dart';

import '../Dto/Base/GlobalDto.dart';
import '../Views/Controls/ErrorPage.dart';
import '../Views/Controls/MainHome.dart';

class NavigationService {
  GoRouter generateRouter({String? initialLocation}) {
    Map<String, Widget> routeList = AppRoute().getRouteList();
    List<GoRoute> routeNodes = [];

    routeList.forEach((key, value) {
      GoRoute routeNode = GoRoute(
        path: key,
        pageBuilder: (context, state) {
          return MaterialPage(
              arguments: state.uri.queryParameters,
              child: SelectionArea(child: value)
          );
        },
      );
      routeNodes.add(routeNode);
    });

    GoRouter router = GoRouter(
      debugLogDiagnostics: kDebugMode,
      routes: routeNodes,
      initialLocation: initialLocation,
      redirect: (context, state) {
        // redirect to the login page if the user is not logged in
        // bool loggedIn = GlobalDto.USNO.isNotEmpty;
        // bool loggingIn = state.matchedLocation.toLowerCase() == Login.route.toLowerCase();
        // if the user is not logged in, they need to login
        // if(!loggingIn){
        //   if (!loggedIn) {
        //     return Login.route;
        //   }
        // }
        // user logged in, no need to redirect to login
        return null;
      },
      errorBuilder: (context, state) {
        return ErrorPage(
          errorTitle: "Page Error",
          errorMessage: state.error!.toString(),
        );
      },
    );

    return router;
  }

  GoRouter generateNestedRouter({String? initialLocation}) {
    Map<String, Widget> lstRouteParent = AppRoute().getParentRouteList();
    Map<String, Widget> lstRoute = AppRoute().getRouteList();
    List<RouteBase> routeNodes = [];
    List<StatefulShellBranch> routeBranches = [];
    String strParentRoutes = "";

    lstRoute.forEach((key, value){
      routeBranches.add(StatefulShellBranch(routes: [
        GoRoute(
          path: key,
          pageBuilder: (context, state) {
            return MaterialPage(
                key: UniqueKey(),
                arguments: state.uri.queryParameters,
                child: SelectionArea(child: value)
            );
          },
        ),
      ]));
    });

    routeBranches.add(StatefulShellBranch(routes: [
      GoRoute(
        path: "/",
        pageBuilder: (context, state) {
          return MaterialPage(
              arguments: state.uri.queryParameters,
              child: SelectionArea(child: BlankPage())
          );
        },
      ),
    ]));

    routeNodes.add(
        StatefulShellRoute.indexedStack(
          pageBuilder: (context, state, navigationShell) {
            return MaterialPage(
                arguments: state.uri.queryParameters,
                child: SelectionArea(child: MainHome(navigationShell: navigationShell))
            );
          },
          branches: routeBranches,
        )
    );

    lstRouteParent.forEach((key, value) {
      if(strParentRoutes.isNotEmpty){
        strParentRoutes += "|";
      }
      strParentRoutes += key;

      GoRoute routeNode = GoRoute(
        path: key,
        pageBuilder: (context, state) {
          return MaterialPage(
              arguments: state.uri.queryParameters,
              child: SelectionArea(child: value)
          );
        },
      );

      routeNodes.add(routeNode);
    });

    GoRouter router = GoRouter(
      debugLogDiagnostics: kDebugMode,
      routes: routeNodes,
      initialLocation: initialLocation,
      redirect: (context, state) {
        // redirect to the login page if the user is not logged in
        // bool loggedIn = GlobalDto.USNO.isNotEmpty;
        // bool loggingIn = state.matchedLocation.toLowerCase() == Login.route.toLowerCase();
        // bool haveAuth =  state.matchedLocation.toLowerCase() == "/";

        // if(!haveAuth) {
        //   haveAuth = strParentRoutes.toLowerCase().contains(
        //           state.matchedLocation.substring(1).toLowerCase());
        // }

        // if(!haveAuth) {
        //   List<String> lstUrlRaw = state.matchedLocation.split("/");
        //   List<String> lstUrlProgram = lstUrlRaw[lstUrlRaw.length - 1].split("_");
        //   // debugPrint("[NavService-UrlProgramCode]${lstUrlProgram[0].substring(0, lstUrlProgram[0].length - 1).toLowerCase()}");
        //   haveAuth = GlobalDto.listAuthority.isNotEmpty && GlobalDto.listAuthority.toLowerCase().contains(
        //           lstUrlProgram[0].substring(0, lstUrlProgram[0].length - 1).toLowerCase()
        //       );
        // }

        // if(!loggingIn){
        //   // if the user is not logged in, they need to login
        //   if (!loggedIn) {
        //     return Login.route;
        //   }
        //   // if the user is logged in, but don't have authority
        //   else{
        //     if(!haveAuth) {
        //       return Uri.encodeFull("${ErrorPage.route}?t=Page Not Found&m=We couldn't find the page ${state.matchedLocation}");
        //     }
        //   }
        // }

        // user logged in, no need to redirect to login
        return null;
      },
      errorBuilder: (context, state) {
        return ErrorPage(
          errorTitle: "Page Error",
          errorMessage: state.error!.toString(),
        );
      },
    );

    return router;
  }
}
