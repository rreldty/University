import 'dart:convert';

import 'package:flutter/material.dart';
import 'LookupDataGrid.dart';
import '../../Common/GlobalStyle.dart';
import '../../Dao/Base/LookupDao.dart';
import '../../Dto/Base/EntityDto.dart';
import '../../Dto/Base/LookupDto.dart';

import 'ButtonExtender.dart';

class SearchBoxPage
{
  String _entity = "";
  String _filter = "";
  String _sort = "";

  int intPageNumber = 1;
  int intPageSize = 15;
  double sizeWidth = 800;
  double sizeHeight = 480;

  Future<LookupDto> getInitialData() async {
    EntityDto obj = EntityDto(
      Entity: _entity,
      Filter: _filter,
      Sort: _sort,
      PageNum: intPageNumber,
      PageSize: intPageSize,
    );

    LookupDao dao = LookupDao();
    return dao.listLookup(obj);
  }

  Future<String?> showSearchBox({
    required BuildContext context,
    required String entity,
    String filter = "",
    String sort = "",
    String title = "List of ...",
    List<int>? hideColumns,
  }) async {
    _entity = entity;
    _filter = filter;
    _sort = sort;

    debugPrint("show SearchBox");

    LookupDto objSearchBox = await getInitialData();

    // debugPrint("w: ${objSearchBox.WindowSize}");

    if(objSearchBox.WindowSize.isNotEmpty){
      List<String> windowSize = objSearchBox.WindowSize.split(";");

      sizeWidth = double.parse(windowSize[0]);
      sizeHeight = double.parse(windowSize[1]);

      // debugPrint("w: $sizeWidth");
      // debugPrint("h: $sizeHeight");
    }

    return await showDialog(
        context: context,
        barrierDismissible: false, // user must tap button for close dialog!
        builder: (BuildContext context) {
          return StatefulBuilder(
            builder: (context, setState) {
              return SimpleDialog(
                  backgroundColor: Colors.transparent,
                  insetPadding: const EdgeInsets.all(0),
                  contentPadding: const EdgeInsets.all(0),
                  children: [
                    Container(
                      width: sizeWidth,
                      height: sizeHeight,
                      padding: const EdgeInsets.all(10),
                      decoration: BoxDecoration(
                        color: Colors.white,
                        borderRadius: BorderRadius.circular(5),
                      ),
                      child:Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisSize: MainAxisSize.max,
                        children: [
                          Container(
                            padding: const EdgeInsets.only(bottom: 10),
                            child: Text(title, style: TextStyle(fontSize: GlobalStyle.lookupTitleFontSize, color: GlobalStyle.lookupTitleFontColor,)),
                          ),
                          Expanded(
                            child: LookupDataGrid(
                              initialData: objSearchBox,
                              entity: entity,
                              filter: filter,
                              sort: sort,
                              pageNumber: intPageNumber,
                              pageSize: intPageSize,
                              hideColumns: hideColumns,
                              selectedRowCallback: (map) {
                                Navigator.of(context, rootNavigator: true).pop(json.encode(map));
                              },
                            ),
                          ),
                          SizedBox(
                            width: double.infinity,
                            child: Row(
                              crossAxisAlignment: CrossAxisAlignment.center,
                              mainAxisAlignment: MainAxisAlignment.end,
                              mainAxisSize: MainAxisSize.max,
                              children: [
                                Container(
                                  margin: const EdgeInsets.only(top: 0),
                                  child: ButtonExtender(
                                    buttonText: "Cancel",
                                    onPressed: () {
                                      Navigator.of(context, rootNavigator: true).pop("");
                                    },
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ],
                      ),
                    ),
                  ]
              );
            },
          );
        });

    // return showGeneralDialog<String>(
    //   context: context,
    //   barrierColor: Colors.black12.withOpacity(0.6), // Background color
    //   barrierDismissible: false,
    //   barrierLabel: 'Dialog',
    //   transitionDuration: Duration(milliseconds: 400),
    //   pageBuilder: (_, __, ___) {
    //     return Column(
    //       children: <Widget>[
    //         Expanded(
    //           flex: 5,
    //           child: SizedBox.expand(child: FlutterLogo()),
    //         ),
    //         Expanded(
    //           flex: 1,
    //           child: SizedBox.expand(
    //             child: ElevatedButton(
    //               onPressed: () => Navigator.pop(context),
    //               child: Text('Dismiss'),
    //             ),
    //           ),
    //         ),
    //       ],
    //     );
    //   },
    // );


  }




}