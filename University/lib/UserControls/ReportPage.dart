import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:printing/printing.dart';
import '../../Common/GlobalStyle.dart';
import '../../Common/PageBase.dart';

class ReportPage extends StatefulWidget {
  static const String route = "/Widget/ReportPage";

  final String title;

  ReportPage({
    this.title = "",
  });

  @override
  createState() => ReportPageState();
}

class ReportPageState extends PageBase<ReportPage> {
  double toolbarFontSize = 11;
  Color hoverColor = Color(0xffffdd66);
  bool isBackHover = false;
  bool isPrintHover = false;

  String strBack = "Back";
  String strPrint = "Print";

  @override
  void appInit(PagePlatform pagePlatform) {
    // TODO: implement appInit
  }

  @override
  void pageBehaviour(PageMode pageMode) {
    // TODO: implement pageBehaviour
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: _generateReportToolbar(),
      body: PdfPreview(
        build: (format) => _generatePdf(format),
      ),
    );
  }

  PreferredSizeWidget _generateReportToolbar() {
    return AppBar(
      backgroundColor: GlobalStyle.primaryColor,
      leading: Container(),
      flexibleSpace: Container(
        width: double.infinity,
        padding: const EdgeInsets.only(left: 5, right: 5),
        color: GlobalStyle.primaryColor,
        child: Row(
          mainAxisSize: MainAxisSize.max,
          children: <Widget>[
            InkWell(
              onTap: () {
                
              },
              onHover: (value) {
                setState(() {
                  isBackHover = value;
                });
              },
              child: Container(
                padding: const EdgeInsets.only(top: 4, left: 15),
                child: Column(
                  children: <Widget>[
                    Icon(
                      Icons.insert_drive_file,
                      color: isBackHover ? hoverColor : Colors.white,
                    ),
                    Text(
                      strBack,
                      style: TextStyle(color: isBackHover ? hoverColor : Colors.white, fontSize: toolbarFontSize),
                    )
                  ],
                ),
              ),
            ),
            InkWell(
              onTap: () {
                
              },
              onHover: (value) {
                setState(() {
                  isPrintHover = value;
                });
              },
              child: Container(
                padding: const EdgeInsets.only(top: 4, left: 25),
                child: Column(
                  children: <Widget>[
                    Icon(
                      Icons.print,
                      color: isPrintHover ? hoverColor : Colors.white,
                    ),
                    Text(
                      strPrint,
                      style: TextStyle(color: isPrintHover ? hoverColor : Colors.white, fontSize: toolbarFontSize),
                    )
                  ],
                ),
              ),
            ),
            Expanded(
              child: Row(
                crossAxisAlignment: CrossAxisAlignment.center,
                mainAxisAlignment: MainAxisAlignment.end,
                mainAxisSize: MainAxisSize.max,
                children: [
                  Container(
                    padding: const EdgeInsets.only(right: 25),
                  ),
                  Container(
                    padding: const EdgeInsets.only(right: 15),
                    child: Text(
                      widget.title,
                      style: const TextStyle(color: Colors.white, fontSize: 18),
                    ),
                  ),
                ],
              ),
            ),

          ],
        ),
      ),
    );
  }

  Future<Uint8List> _generatePdf(PdfPageFormat format) async {
    final pdf = pw.Document(
        compress: true
    );

    pdf.addPage(
      pw.Page(
        pageFormat: format,
        build: (context) {
          return pw.Column(
            children: [
              pw.SizedBox(
                width: double.infinity,
                child: pw.FittedBox(
                  child: pw.Text("Title"),
                ),
              ),
              pw.SizedBox(height: 20),
              pw.Flexible(child: pw.FlutterLogo())
            ],
          );
        },
      ),
    );

    return pdf.save();
  }
}