import 'package:adaptive_scrollbar/adaptive_scrollbar.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import '../Common/CommonMethod.dart';
import 'ModalProgressExtender.dart';
import '../../Common/PageBase.dart';
import 'ScrollExtender.dart';

class ModalContent extends StatefulWidget {
  final ValueNotifier<bool> showModalProgress;
  final Key formKey;
  final Widget Function(BuildContext context, BoxConstraints constraints)? builder;
  final Widget Function(BuildContext context, BoxConstraints constraints)? builderPhone;
  final bool hideScrollbar;
  final double paddingLeft;
  final double paddingTop;

  ModalContent({
    required this.formKey,
    required this.showModalProgress,
    this.builder,
    this.builderPhone,
    this.hideScrollbar = false,
    this.paddingLeft = 20,
    this.paddingTop = 20,
  }) : assert(builder != null || builderPhone != null);

  @override
  createState() => ModalContentState();
}

class ModalContentState extends PageBase<ModalContent> {
  final ScrollController horizontalScroll = ScrollController();
  final ScrollController verticalScroll = ScrollController();
  final double adaptiveScrollWidth = 20;
  final double adaptiveScrollBorderRadius = 5;
  double paddingPageContentRight = 0;
  double paddingUnderSpacing = 0;
  double dblLastFactor = 0;

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
    return LayoutBuilder(builder: (context, constraints) {
      if(verticalScroll.hasClients)
      {
        Future.delayed(Duration.zero, () {
          // debugPrint("vertical-LayoutBuilder: ${verticalScroll.position.maxScrollExtent}");
          double dblFactor = verticalScroll.position.maxScrollExtent > 0 ? 1 : 0;
          // debugPrint("Factor: $dblFactor; Last Factor: $dblLastFactor");
          if(dblFactor != dblLastFactor)
          {
            dblLastFactor = dblFactor;
            setState(() {
              paddingPageContentRight = adaptiveScrollWidth + (adaptiveScrollWidth * dblLastFactor);
              paddingUnderSpacing = adaptiveScrollWidth * dblLastFactor;
            });
          }
        });

      }

      if(CommonMethod.isWebOrDesktop()){
        // debugPrint("sw ${constraints.maxWidth.toString()}");
        // Web
        return AdaptiveScrollbar(
            controller: verticalScroll,
            width: adaptiveScrollWidth,
            scrollToClickDelta: 75,
            scrollToClickFirstDelay: 200,
            scrollToClickOtherDelay: 50,
            sliderDecoration: BoxDecoration(
                color: const Color(0xffc1c1c1),
                borderRadius: BorderRadius.all(Radius.circular(adaptiveScrollBorderRadius))),
            sliderActiveDecoration: BoxDecoration(
                color: const Color(0xffa8a8a8),
                borderRadius: BorderRadius.all(Radius.circular(adaptiveScrollBorderRadius))),
            underDecoration: const BoxDecoration(shape: BoxShape.rectangle, color: Color(0xfff1f1f1)),
            //underColor: const Color(0xfff1f1f1),
            child: AdaptiveScrollbar(
              // underSpacing: EdgeInsets.only(bottom: (!verticalScroll.hasClients ||
              //     verticalScroll.position.maxScrollExtent == 0) ? adaptiveScrollWidth : 0),
              underSpacing: EdgeInsets.only(bottom: paddingUnderSpacing),
              controller: horizontalScroll,
              width: adaptiveScrollWidth,
              position: ScrollbarPosition.bottom,
              sliderDecoration: BoxDecoration(
                  color: const Color(0xffc1c1c1),
                  borderRadius: BorderRadius.all(Radius.circular(adaptiveScrollBorderRadius))),
              sliderActiveDecoration: BoxDecoration(
                  color: const Color(0xffa8a8a8),
                  borderRadius: BorderRadius.all(Radius.circular(adaptiveScrollBorderRadius))),
              underDecoration: const BoxDecoration(shape: BoxShape.rectangle, color: Color(0xfff1f1f1)),
              child: ModalProgressExtender(
                inAsyncCall: widget.showModalProgress,
                child: Form(
                  key: widget.formKey,
                  child: Scaffold(
                    resizeToAvoidBottomInset: true,
                    backgroundColor: Colors.white,
                    body: SizedBox(
                      width: constraints.maxWidth,
                      height: constraints.maxHeight,
                      // child: Container(color: Colors.amber,),
                      child: SingleChildScrollView(
                        controller: verticalScroll,
                        scrollDirection: Axis.vertical,
                        child: SingleChildScrollView(
                          controller: horizontalScroll,
                          scrollDirection: Axis.horizontal,
                          child: Container(
                            padding: EdgeInsets.fromLTRB(widget.paddingLeft, widget.paddingTop, !widget.hideScrollbar ? paddingPageContentRight : 0, !widget.hideScrollbar ? 20 : 0),
                            child: builderWeb(constraints),
                          ),
                        ),
                      ),
                    ),
                  ),
                ),
              ),
            )
        );
      }
      else{
        //Mobile
        return ModalProgressExtender(
          inAsyncCall: widget.showModalProgress,
          child: Form(
            key: widget.formKey,
            child: Scaffold(
              resizeToAvoidBottomInset: true,
              backgroundColor: Colors.white,
              body: SingleChildScrollView(
                // child: Container(color: Colors.amber,),
                child: builderPhone(constraints),
              ),
            ),
          ),
        );
      }
    },);
  }

  Widget builderWeb(BoxConstraints constraints){
    if(constraints.maxWidth < 600){
      if(widget.builderPhone != null){
        return widget.builderPhone!(context, constraints);
      }
    }

    if(widget.builder != null){
      return widget.builder!(context, constraints);
    }

    return Container();
  }

  Widget builderPhone(BoxConstraints constraints){
    if(widget.builderPhone != null){
      return widget.builderPhone!(context, constraints);
    }

    return Container();
  }
}