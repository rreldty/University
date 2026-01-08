
import 'package:flutter/material.dart';
import 'package:flutter_inappwebview/flutter_inappwebview.dart';

class IFrameExtender extends StatefulWidget {
  static const String route = "/UserControls/IFrameExtender";
  final String urlSource;

  const IFrameExtender({
    super.key,
    this.urlSource = "",
  });

  @override
  createState() => IFrameExtenderState();
}

class IFrameExtenderState extends State<IFrameExtender> {
  // late IFrameElement iframeElement;
  // String strUrlPath = "";

  @override
  void initState() {
    super.initState();

    // iframeElement = IFrameElement();
    // iframeElement.id = "frmContentExt";
    // iframeElement.src = "";
    // iframeElement.style.width = "100%";
    // iframeElement.style.height = "100%";
    // iframeElement.style.border = "none";
    //
    // // ignore: undefined_prefixed_name
    // ui.platformViewRegistry.registerViewFactory(
    //   iframeElement.id,
    //       (int viewId) => iframeElement,
    // );

    // WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
    //   await Future.delayed(const Duration(milliseconds: 30));
    //
    //   HtmlElement elem = document.getElementById(iframeElement.id) as HtmlElement;
    //   elem.attributes["src"] = widget.urlSource;
    // });
  }

  @override
  Widget build(BuildContext context) {
    // SchedulerBinding.instance.addPostFrameCallback((_) async {
    //   await Future.delayed(const Duration(milliseconds: 10));
    //
    //   var el = document.getElementById(iframeElement.id);
    //
    //   if(el != null){
    //     IFrameElement elem = document.getElementById(iframeElement.id) as IFrameElement;
    //
    //     Uri uri1 = Uri.parse((elem.src ?? "").toLowerCase());
    //     Uri uri2 = Uri.parse(widget.urlSource.toLowerCase());
    //
    //     // debugPrint("IFrameExtender-loader");
    //     // debugPrint(uri1.toString());
    //     // debugPrint(uri2.toString());
    //
    //     if(uri1.toString() != uri2.toString()) {
    //       elem.src = Uri.encodeFull(CommonMethod.resolveUrl(LoaderPage.route));
    //       await Future.delayed(const Duration(milliseconds: 10));
    //     }
    //
    //     // debugPrint("IFrameExtender-urlSource");
    //     // debugPrint(Uri.encodeFull(widget.urlSource));
    //     elem.src = Uri.encodeFull(widget.urlSource);
    //   }
    // });

    return Scaffold(
        resizeToAvoidBottomInset: false,
        backgroundColor: Colors.white,
        body: Row(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisSize: MainAxisSize.max,
          children: [
            Expanded(
                child: SizedBox(
                  width: double.infinity,
                  height: double.infinity,
                  child: InAppWebView(
                    initialUrlRequest: URLRequest(url: WebUri(widget.urlSource)),
                  ),
                ) //_generatePage(strRouteMenu)
            ),
            // Expanded(
            //     child: SizedBox(
            //       width: double.infinity,
            //       height: double.infinity,
            //       child: HtmlElementView(
            //         //key: UniqueKey(),
            //         viewType: iframeElement.id,
            //       ),
            //     )
            // ),
          ],
        )
    );
  }
}