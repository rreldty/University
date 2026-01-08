import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';
import 'Breadxcrumb.dart';
import '../Common/AppConfig.dart';

class PowerBIViewer extends StatefulWidget {
  static const String route = "/UserControls/PowerBIViewer";

  const PowerBIViewer({super.key});

  @override
  createState() => PowerBIViewerState();
}

class PowerBIViewerState extends State<PowerBIViewer> {
  // final form1 = GlobalKey<FormState>();
  late Map<String, String> args;
  String strPbiUrl = "";

  @override
  void initState() {
    // IFrameElement iframeElement = IFrameElement();
    // iframeElement.id = "frmPBIContent";
    // iframeElement.src = "";
    // iframeElement.style.width = "100%";
    // iframeElement.style.height = "100%";
    // iframeElement.style.border = "none";
    //
    // // ignore: undefined_prefixed_name
    // ui.platformViewRegistry.registerViewFactory(
    //   "iframeElementPBI",
    //       (int viewId) => iframeElement,
    // );

    super.initState();

    WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
      await Future.delayed(const Duration(milliseconds: 10));

      args = {};
      if(ModalRoute.of(context)!.settings.arguments != null) {
        args = ModalRoute
            .of(context)!
            .settings
            .arguments as Map<String, String>;

        String pbiWorkspaceId = args["wid"] ?? "";
        String pbiReportId = args["rid"] ?? "";
        String pbiFilterTable = args["tbno"] ?? "";
        String pbiFilter = args["filter"] ?? "";

        setState(() {
          strPbiUrl = _generatePBIEmbedUrl(pbiWorkspaceId, pbiReportId, pbiFilterTable: pbiFilterTable, pbiFilter: pbiFilter);
        });
        // HtmlElement elem = document.getElementById("frmPBIContent") as HtmlElement;
        // elem.attributes["src"] = generatePBIEmbedUrl(pbiWorkspaceId, pbiReportId, pbiFilterTable: pbiFilterTable, pbiFilter: pbiFilter);

      }

    });
  }

  String _generatePBIEmbedUrl(String pbiWorkspaceId, String pbiReportId, {String? pbiFilterTable, String? pbiFilter}){
    //ret=BI&wid=1c9a599a-da21-4061-9c92-75c8cf11720d&rid=e43dd0f8-51e6-4dcd-8912-b96b86e0f143&tbno=User Authorization&filter=User ID ieq #USNO

    String strUrl = "~/Controls/PBIEmbed.aspx?ret=BI&wid=$pbiWorkspaceId&rid=$pbiReportId";
    if(pbiFilterTable != null && pbiFilterTable.isNotEmpty && pbiFilter != null && pbiFilter.isNotEmpty){
      strUrl += "&tbno=$pbiFilterTable&filter=$pbiFilter";
    }

    strUrl = strUrl.replaceFirst("~", AppConfig.ApiBaseURL);
    return strUrl;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        resizeToAvoidBottomInset: false,
        backgroundColor: Colors.white,
        // appBar: ToolbarBox(
        //   toolbarBoxMode: ToolbarBoxMode.None,
        //   isBackVisible: true,
        //   onBack: (){
        //     context.pop();
        //   },
        // ),
        appBar: Breadxcrumb(
          isBackVisible: false,
          crumbItems: [
            CrumbItem(label: "Dashboard POSM", onTap: (){}),
          ],
        ),
        body: Row(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisSize: MainAxisSize.max,
          children: [
            Expanded(
                child: SizedBox(
                  width: double.infinity,
                  height: double.infinity,
                  child: InAppWebView(
                    initialUrlRequest: URLRequest(url: WebUri(strPbiUrl)),
                  ),
                ) //_generatePage(strRouteMenu)
            ),
            // Expanded(
            //     child: SizedBox(
            //       width: double.infinity,
            //       height: double.infinity,
            //       child: HtmlElementView(
            //         //key: UniqueKey(),
            //         viewType: 'iframeElementPBI',
            //       ),
            //     )
            // ),
          ],
        )
    );
  }
}