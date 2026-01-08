import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';
import '../../Common/AppConfig.dart';
import '../../Common/GlobalStyle.dart';
import '../../Dto/Base/ModalPopupResult.dart';
import 'package:url_launcher/url_launcher.dart';

class ReportViewer {
  static Future<ModalPopupResult?> show(
      {
        required BuildContext context,
        required String title,
        required String entity,
        Map<String, String>? param,
        bool isNewTab = false
      }) async {

    String strParam = "";

    if(param != null){
      param.forEach((key, value) {
        strParam += "&$key=$value";
      });
    }

    String strUrl = "${AppConfig.ApiBaseURL}Controls/ReportViewer.aspx?entity=$entity$strParam";
    strUrl = Uri.encodeFull(strUrl);
    debugPrint(strUrl);

    if(isNewTab){
      Uri uriReport = Uri.parse(Uri.encodeFull(strUrl));
      bool isLaunch = await canLaunchUrl(uriReport);
      if (isLaunch) {
        await launchUrl(uriReport, webOnlyWindowName: "_blank");
      }
    }else{
      // IFrameElement iframeElement = IFrameElement();
      // iframeElement.id = "frmContent${entity}_${DateTime.now().millisecondsSinceEpoch.toString()}";
      // iframeElement.src = strUrl;
      // iframeElement.style.width = "100%";
      // iframeElement.style.height = "100%";
      // iframeElement.style.border = "none";
      //
      // // ignore: undefined_prefixed_name
      // ui.platformViewRegistry.registerViewFactory(
      //   iframeElement.id,
      //       (int viewId) => iframeElement,
      // );

      return showDialog<ModalPopupResult>(
          context: context,
          barrierDismissible: false, // user must tap button for close dialog!
          builder: (BuildContext context) {
            return AlertDialog(
              contentPadding: EdgeInsets.all(10),
              content: SizedBox(
                width: MediaQuery.of(context).size.width,
                height: MediaQuery.of(context).size.height,
                child: Column(
                  mainAxisSize: MainAxisSize.max,
                  children: [
                    Row(
                      children: [
                        Expanded(
                          child: Text(title ?? "",
                              style: TextStyle(
                                fontSize: 12.0,
                                fontWeight: FontWeight.normal,
                                color: GlobalStyle.primaryColor,
                              )),
                        ),
                        InkWell(
                          onTap: () => Navigator.of(context, rootNavigator: true).pop(),
                          child: Text("close",
                              style: TextStyle(
                                fontSize: 12.0,
                                fontWeight: FontWeight.normal,
                                color: GlobalStyle.primaryColor,
                              )),
                        ),
                      ],
                    ),
                    Expanded(
                        child: SizedBox(
                          width: double.infinity,
                          height: double.infinity,
                          child: InAppWebView(
                            initialUrlRequest: URLRequest(url: WebUri(strUrl)),
                          ),
                        ) //_generatePage(strRouteMenu)
                    ),
                    // Expanded(
                    //     child: SizedBox(
                    //       width: double.infinity,
                    //       height: double.infinity,
                    //       child: HtmlElementView(
                    //         viewType: iframeElement.id,
                    //       ),
                    //     ) //_generatePage(strRouteMenu)
                    // ),
                  ],
                ),
              ),
            );


          });
    }

    return null;
  }
}
