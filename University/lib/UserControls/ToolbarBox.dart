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

enum ToolbarBoxMode {
  None,
  New,
  Search,
  Master,
  Transaction,
  TransactionWithApproval,
  Report,
  Approval,
  All,
}

class ToolbarBox extends StatefulWidget implements PreferredSizeWidget {
  final ToolbarBoxController? controller;
  final ToolbarBoxMode toolbarBoxMode;

  final bool? isBackVisible;
  final bool? isNextVisible;
  final bool? isNewVisible;
  final bool? isSaveVisible;
  final bool? isSubmitVisible;
  final bool? isVoidVisible;
  final bool? isCloseVisible;
  final bool? isCopyVisible;
  final bool? isListVisible;
  final bool? isPrintVisible;
  final bool? isDownloadVisible;
  final bool? isApproveVisible;
  final bool? isReviseVisible;
  final bool? isRejectVisible;
  final bool? isProceedVisible;

  final bool? isBackEnable;
  final bool? isNextEnable;
  final bool? isNewEnable;
  final bool? isSaveEnable;
  final bool? isSubmitEnable;
  final bool? isVoidEnable;
  final bool? isCloseEnable;
  final bool? isCopyEnable;
  final bool? isListEnable;
  final bool? isPrintEnable;
  final bool? isDownloadEnable;
  final bool? isApproveEnable;
  final bool? isReviseEnable;
  final bool? isRejectEnable;
  final bool? isProceedEnable;

  final Function()? onBack;
  final Function()? onNext;
  final Function()? onNew;
  final Function()? onSave;
  final Function()? onSubmit;
  final Function()? onVoid;
  final Function()? onClose;
  final Function()? onCopy;
  final Function()? onPrint;
  final Function()? onDownload;
  final Function()? onApprove;
  final Function()? onRevise;
  final Function()? onReject;
  final Function()? onProceed;

  final String listEntity;
  final String listFilter;
  final String listTitle;
  final double listWidth;
  final double listHeight;

  final String messageOnSave;
  final String messageOnSubmit;
  final String messageOnClose;
  final String messageOnVoid;
  final String messageOnCopy;
  final String messageOnApprove; //CWH
  final String messageOnRevise; //CWH
  final String messageOnReject; //CWH

  /// Hides column with the given column index.
  final List<int>? listHideColumns;
  final Function(Map<String, dynamic> item)? listOnSelected;

  ToolbarBox({
    this.controller,
    this.toolbarBoxMode = ToolbarBoxMode.Master,
    this.isBackVisible,
    this.isNextVisible,
    this.isNewVisible,
    this.isSaveVisible,
    this.isSubmitVisible,
    this.isVoidVisible,
    this.isCloseVisible,
    this.isCopyVisible,
    this.isListVisible,
    this.isPrintVisible,
    this.isDownloadVisible,
    this.isApproveVisible,
    this.isReviseVisible,
    this.isRejectVisible,
    this.isProceedVisible,

    this.isBackEnable,
    this.isNextEnable,
    this.isNewEnable,
    this.isSaveEnable,
    this.isSubmitEnable,
    this.isVoidEnable,
    this.isCloseEnable,
    this.isCopyEnable,
    this.isListEnable,
    this.isPrintEnable,
    this.isDownloadEnable,
    this.isApproveEnable,
    this.isReviseEnable,
    this.isRejectEnable,
    this.isProceedEnable,

    this.onBack,
    this.onNext,
    this.onNew,
    this.onSave,
    this.onSubmit,
    this.onVoid,
    this.onClose,
    this.onCopy,
    this.onPrint,
    this.onDownload,
    this.onApprove,
    this.onRevise,
    this.onReject,
    this.onProceed,

    this.listEntity = "",
    this.listFilter = "",
    this.listTitle = "",
    this.listWidth = 0,
    this.listHeight = 0,
    this.listOnSelected,
    this.listHideColumns,

    this.messageOnSave = "Do you want to save this data?",
    this.messageOnSubmit = "Do you want to submit this data?",
    this.messageOnClose = "Do you want to close this data?",
    this.messageOnVoid = "Do you want to void this data?",
    this.messageOnCopy = "Do you want to duplicate this data?",
    this.messageOnApprove = "Do you want to approve this data?",
    this.messageOnRevise = "Do you want to revise this data?",
    this.messageOnReject = "Do you want to reject this data?",
  });

  @override
  // TODO: implement preferredSize
  Size get preferredSize => Size.fromHeight(GlobalStyle.toolbarHeight);

  @override
  createState() => ToolbarBoxState();
}

class ToolbarBoxState extends State<ToolbarBox> {
  bool _isBackVisible = false;
  bool _isNextVisible = false;
  bool _isNewVisible = true;
  bool _isSaveVisible = true;
  bool _isSubmitVisible = true;
  bool _isVoidVisible = true;
  bool _isCloseVisible = true;
  bool _isCopyVisible = false;
  bool _isListVisible = true;
  bool _isPrintVisible = false;
  bool _isDownloadVisible = false;
  bool _isApproveVisible = false;
  bool _isReviseVisible = false;
  bool _isRejectVisible = false;
  bool _isProceedVisible = false;

  bool _isBackEnable = true;
  bool _isNextEnable = true;
  bool _isNewEnable = true;
  bool _isSaveEnable = true;
  bool _isSubmitEnable = true;
  bool _isVoidEnable = true;
  bool _isCloseEnable = true;
  bool _isCopyEnable = true;
  bool _isListEnable = true;
  bool _isPrintEnable = true;
  bool _isDownloadEnable = true;
  bool _isApproveEnable = true;
  bool _isReviseEnable = true;
  bool _isRejectEnable = true;
  bool _isProceedEnable = true;

  String strTitleMenu = "";

  @override
  void initState() {
    if(widget.controller != null){
      widget.controller!.isBackVisible = widget.isBackVisible;
      widget.controller!.isNextVisible = widget.isNextVisible;
      widget.controller!.isNewVisible = widget.isNewVisible;
      widget.controller!.isSaveVisible = widget.isSaveVisible;
      widget.controller!.isSubmitVisible = widget.isSubmitVisible;
      widget.controller!.isVoidVisible = widget.isVoidVisible;
      widget.controller!.isCloseVisible = widget.isCloseVisible;
      widget.controller!.isCopyVisible = widget.isCopyVisible;
      widget.controller!.isListVisible = widget.isListVisible;
      widget.controller!.isPrintVisible = widget.isPrintVisible;
      widget.controller!.isDownloadVisible = widget.isDownloadVisible;
      widget.controller!.isApproveVisible = widget.isApproveVisible;
      widget.controller!.isReviseVisible = widget.isReviseVisible;
      widget.controller!.isRejectVisible = widget.isRejectVisible;
      widget.controller!.isProceedVisible = widget.isProceedVisible;

      widget.controller!.isBackEnable = widget.isBackEnable;
      widget.controller!.isNewEnable = widget.isNewEnable;
      widget.controller!.isSaveEnable = widget.isSaveEnable;
      widget.controller!.isSubmitEnable = widget.isSubmitEnable;
      widget.controller!.isVoidEnable = widget.isVoidEnable;
      widget.controller!.isCloseEnable = widget.isCloseEnable;
      widget.controller!.isCopyEnable = widget.isCopyEnable;
      widget.controller!.isListEnable = widget.isListEnable;
      widget.controller!.isPrintEnable = widget.isPrintEnable;
      widget.controller!.isDownloadEnable = widget.isDownloadEnable;
      widget.controller!.isApproveEnable = widget.isApproveEnable;
      widget.controller!.isReviseEnable = widget.isReviseEnable;
      widget.controller!.isRejectEnable = widget.isRejectEnable;
      widget.controller!.isProceedEnable = widget.isProceedEnable;
    }
    super.initState();

    WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
      SharedPreferences prefs = await SharedPreferences.getInstance();

      await Future.delayed(const Duration(milliseconds: 1));

      Map<String, dynamic> args = {};
      Map<String, dynamic> argsEncrypted = {};

      if(ModalRoute.of(context)!.settings.arguments != null) {
        argsEncrypted = ModalRoute
            .of(context)!
            .settings
            .arguments as Map<String, dynamic>;

        // debugPrint("[PageBase-EncryptedArgs] ${argsEncrypted.toString()}");

        if(argsEncrypted.isNotEmpty){
          String strParam = await Encryption.symmetricDecrypt(argsEncrypted["xe"]);
          // debugPrint("[PageBase-Param] $strParam");

          if(strParam.contains("&")){
            List<String> q2 = strParam.split("&");

            for(int n = 0; n < q2.length; n++){
              List<String> q3 = q2[n].split("=");
              if(q3.length > 1) {
                args[q3[0]] = q3[1];
              }
            }
          }
        }
      }

      setState(() {
        if(args.containsKey("mt")) {
          strTitleMenu = args["mt"].toString().replaceAll("+", " ");
          strTitleMenu = Uri.decodeFull(strTitleMenu);
          prefs.setString("TitleMenu", strTitleMenu);
        }else{
          if(prefs.containsKey("TitleMenu")){
            strTitleMenu = prefs.getString("TitleMenu") ?? "";
          }
        }
      });
    });
  }

  void btnSave_Click() async {
    if(widget.messageOnSave.isNotEmpty){
      if (widget.onSave != null) {
        DialogResult? dlgResult = await MessageBox.show(
            context: context,
            title: "Save Data",
            message: widget.messageOnSave,
            dialogButton: DialogButton.OkCancel);

        if(dlgResult == DialogResult.OK){
          widget.onSave!();
        }
      }
    }else{
      widget.onSave!();
    }
  }

  void btnList_Click() async {
    String strResult = await LookupPage().showLookup(
      context: context,
      entity: widget.listEntity,
      filter: widget.listFilter,
      title: widget.listTitle,
      hideColumns: widget.listHideColumns,
    ) ?? "";
    if (strResult.isNotEmpty) {
      Map<String, dynamic> map = json.decode(strResult);
      if (widget.listOnSelected != null) {
        widget.listOnSelected!(map);
      }
    }
  }

  void btnSubmit_Click() async {
    if(widget.messageOnSubmit.isNotEmpty){
      if (widget.onSubmit != null) {
        DialogResult? dlgResult = await MessageBox.show(
            context: context,
            title: "Submit Data",
            message: widget.messageOnSubmit,
            dialogButton: DialogButton.OkCancel);

        if(dlgResult == DialogResult.OK){
          widget.onSubmit!();
        }
      }
    }else{
      widget.onSubmit!();
    }
  }

  void btnVoid_Click() async {
    if(widget.messageOnVoid.isNotEmpty){
      if (widget.onVoid != null) {
        DialogResult? dlgResult = await MessageBox.show(
            context: context,
            title: "Void Data",
            message: widget.messageOnVoid,
            dialogButton: DialogButton.OkCancel);

        if(dlgResult == DialogResult.OK){
          widget.onVoid!();
        }
      }
    }else{
      widget.onVoid!();
    }
  }

  void btnClose_Click() async {
    if(widget.messageOnClose.isNotEmpty){
      if (widget.onClose != null) {
        DialogResult? dlgResult = await MessageBox.show(
            context: context,
            title: "Close Data",
            message: widget.messageOnClose,
            dialogButton: DialogButton.OkCancel);

        if(dlgResult == DialogResult.OK){
          widget.onClose!();
        }
      }
    }else{
      widget.onClose!();
    }

  }

  void btnCopy_Click() async {
    if(widget.messageOnCopy.isNotEmpty){
      if (widget.onCopy != null) {
        DialogResult? dlgResult = await MessageBox.show(
            context: context,
            title: "Duplicate Data",
            message: widget.messageOnCopy,
            dialogButton: DialogButton.OkCancel);

        if(dlgResult == DialogResult.OK){
          widget.onCopy!();
        }
      }
    }else{
      widget.onCopy!();
    }

  }

  //CWH addfungsi approve,revise,reject
  void btnApprove_Click() async {
    if(widget.messageOnApprove.isNotEmpty){
      if (widget.onApprove != null) {
        DialogResult? dlgResult = await MessageBox.show(
            context: context,
            title: "Approve Data",
            message: widget.messageOnApprove,
            dialogButton: DialogButton.OkCancel);

        if(dlgResult == DialogResult.OK){
          widget.onApprove!();
        }
      }
    }else{
      widget.onApprove!();
    }
  }

  void btnRevise_Click() async {
    if(widget.messageOnRevise.isNotEmpty){
      if (widget.onRevise != null) {
        DialogResult? dlgResult = await MessageBox.show(
            context: context,
            title: "Revise Data",
            message: widget.messageOnRevise,
            dialogButton: DialogButton.OkCancel);

        if(dlgResult == DialogResult.OK){
          widget.onRevise!();
        }
      }
    }else{
      widget.onRevise!();
    }
  }

  void btnReject_Click() async {
    if(widget.messageOnReject.isNotEmpty){
      if (widget.onReject != null) {
        DialogResult? dlgResult = await MessageBox.show(
            context: context,
            title: "Reject Data",
            message: widget.messageOnReject,
            dialogButton: DialogButton.OkCancel);

        if(dlgResult == DialogResult.OK){
          widget.onReject!();
        }
      }
    }else{
      widget.onReject!();
    }
  }

  @override
  Widget build(BuildContext context) {
    // String strRoute = Uri.decodeFull(Uri.base.toString());
    // debugPrint("[Toolbar] route: ${context.location}");
    // debugPrint("[Toolbar] route: $strRoute");

    switch (widget.toolbarBoxMode) {
      case ToolbarBoxMode.None:
        {
          _isBackVisible = false;
          _isNextVisible = false;
          _isNewVisible = false;
          _isSaveVisible = false;
          _isSubmitVisible = false;
          _isVoidVisible = false;
          _isCloseVisible = false;
          _isCopyVisible = false;
          _isListVisible = false;
          _isPrintVisible = false;
          _isDownloadVisible = false;
          _isApproveVisible = false;
          _isReviseVisible = false;
          _isRejectVisible = false;
          _isProceedVisible = false;
          break;
        }
      case ToolbarBoxMode.New:
        {
          _isBackVisible = false;
          _isNextVisible = false;
          _isNewVisible = true;
          _isSaveVisible = false;
          _isSubmitVisible = false;
          _isVoidVisible = false;
          _isCloseVisible = false;
          _isCopyVisible = false;
          _isListVisible = false;
          _isPrintVisible = false;
          _isDownloadVisible = false;
          _isApproveVisible = false;
          _isReviseVisible = false;
          _isRejectVisible = false;
          _isProceedVisible = false;
          break;
        }
      case ToolbarBoxMode.Search:
        {
          _isBackVisible = false;
          _isNextVisible = false;
          _isNewVisible = true;
          _isSaveVisible = false;
          _isSubmitVisible = false;
          _isVoidVisible = false;
          _isCloseVisible = false;
          _isCopyVisible = false;
          _isListVisible = true;
          _isPrintVisible = false;
          _isDownloadVisible = false;
          _isApproveVisible = false;
          _isReviseVisible = false;
          _isRejectVisible = false;
          _isProceedVisible = false;
          break;
        }
      case ToolbarBoxMode.Master:
        {
          _isBackVisible = false;
          _isNextVisible = false;
          _isNewVisible = true;
          _isSaveVisible = true;
          _isSubmitVisible = false;
          _isVoidVisible = false;
          _isCloseVisible = false;
          _isCopyVisible = false;
          _isListVisible = true;
          _isPrintVisible = false;
          _isDownloadVisible = false;
          _isApproveVisible = false;
          _isReviseVisible = false;
          _isRejectVisible = false;
          _isProceedVisible = false;
          break;
        }
      case ToolbarBoxMode.Transaction:
        {
          _isBackVisible = true;
          _isNextVisible = false;
          _isNewVisible = true;
          _isSaveVisible = true;
          _isSubmitVisible = true;
          _isVoidVisible = true;
          _isCloseVisible = true;
          _isCopyVisible = false;
          _isListVisible = false;
          _isPrintVisible = false;
          _isDownloadVisible = false;
          _isApproveVisible = false;
          _isReviseVisible = false;
          _isRejectVisible = false;
          _isProceedVisible = false;
          break;
        }
      case ToolbarBoxMode.Report:
        {
          _isBackVisible = false;
          _isNextVisible = false;
          _isNewVisible = true;
          _isSaveVisible = false;
          _isSubmitVisible = false;
          _isVoidVisible = false;
          _isCloseVisible = false;
          _isCopyVisible = false;
          _isListVisible = false;
          _isPrintVisible = true;
          _isDownloadVisible = true;
          _isApproveVisible = false;
          _isReviseVisible = false;
          _isRejectVisible = false;
          _isProceedVisible = false;
          break;
        }
      case ToolbarBoxMode.TransactionWithApproval:
        _isBackVisible = true;
        _isNextVisible = false;
        _isNewVisible = true;
        _isSaveVisible = true;
        _isSubmitVisible = true;
        _isVoidVisible = true;
        _isCloseVisible = true;
        _isCopyVisible = false;
        _isListVisible = false;
        _isPrintVisible = false;
        _isDownloadVisible = false;
        _isApproveVisible = true;
        _isReviseVisible = true;
        _isRejectVisible = true;
        _isProceedVisible = true;
        break;
      case ToolbarBoxMode.Approval:
        _isBackVisible = true;
        _isNextVisible = false;
        _isNewVisible = false;
        _isSaveVisible = false;
        _isSubmitVisible = false;
        _isVoidVisible = false;
        _isCloseVisible = false;
        _isCopyVisible = false;
        _isListVisible = false;
        _isPrintVisible = false;
        _isDownloadVisible = false;
        _isApproveVisible = true;
        _isReviseVisible = true;
        _isRejectVisible = true;
        _isProceedVisible = false;
        break;
      case ToolbarBoxMode.All:
        _isBackVisible = true;
        _isNextVisible = true;
        _isNewVisible = true;
        _isSaveVisible = true;
        _isSubmitVisible = true;
        _isVoidVisible = true;
        _isCloseVisible = true;
        _isCopyVisible = true;
        _isListVisible = true;
        _isPrintVisible = true;
        _isDownloadVisible = true;
        _isApproveVisible = true;
        _isReviseVisible = true;
        _isRejectVisible = true;
        _isProceedVisible = true;
        break;
    }

    if (widget.controller != null) {
      if (widget.controller!.isBackVisible != null) _isBackVisible = widget.controller!.isBackVisible!;
      if (widget.controller!.isNextVisible != null) _isNextVisible = widget.controller!.isNextVisible!;
      if (widget.controller!.isNewVisible != null) _isNewVisible = widget.controller!.isNewVisible!;
      if (widget.controller!.isSaveVisible != null) _isSaveVisible = widget.controller!.isSaveVisible!;
      if (widget.controller!.isSubmitVisible != null) _isSubmitVisible = widget.controller!.isSubmitVisible!;
      if (widget.controller!.isVoidVisible != null) _isVoidVisible = widget.controller!.isVoidVisible!;
      if (widget.controller!.isCloseVisible != null) _isCloseVisible = widget.controller!.isCloseVisible!;
      if (widget.controller!.isCopyVisible != null) _isCopyVisible = widget.controller!.isCopyVisible!;
      if (widget.controller!.isListVisible != null) _isListVisible = widget.controller!.isListVisible!;
      if (widget.controller!.isPrintVisible != null) _isPrintVisible = widget.controller!.isPrintVisible!;
      if (widget.controller!.isDownloadVisible != null) _isDownloadVisible = widget.controller!.isDownloadVisible!;
      if (widget.controller!.isApproveVisible != null) _isApproveVisible = widget.controller!.isApproveVisible!;
      if (widget.controller!.isReviseVisible != null) _isReviseVisible = widget.controller!.isReviseVisible!;
      if (widget.controller!.isRejectVisible != null) _isRejectVisible = widget.controller!.isRejectVisible!;
      if (widget.controller!.isProceedVisible != null) _isProceedVisible = widget.controller!.isProceedVisible!;

      if (widget.controller!.isBackEnable != null) _isBackEnable = widget.controller!.isBackEnable!;
      if (widget.controller!.isNextEnable != null) _isNextEnable = widget.controller!.isNextEnable!;
      if (widget.controller!.isNewEnable != null) _isNewEnable = widget.controller!.isNewEnable!;
      if (widget.controller!.isSaveEnable != null) _isSaveEnable = widget.controller!.isSaveEnable!;
      if (widget.controller!.isSubmitEnable != null) _isSubmitEnable = widget.controller!.isSubmitEnable!;
      if (widget.controller!.isVoidEnable != null) _isVoidEnable = widget.controller!.isVoidEnable!;
      if (widget.controller!.isCloseEnable != null) _isCloseEnable = widget.controller!.isCloseEnable!;
      if (widget.controller!.isCopyEnable != null) _isCopyEnable = widget.controller!.isCopyEnable!;
      if (widget.controller!.isListEnable != null) _isListEnable = widget.controller!.isListEnable!;
      if (widget.controller!.isPrintEnable != null) _isPrintEnable = widget.controller!.isPrintEnable!;
      if (widget.controller!.isDownloadEnable != null) _isDownloadEnable = widget.controller!.isDownloadEnable!;
      if (widget.controller!.isApproveEnable != null) _isApproveEnable = widget.controller!.isApproveEnable!;
      if (widget.controller!.isReviseEnable != null) _isReviseEnable = widget.controller!.isReviseEnable!;
      if (widget.controller!.isRejectEnable != null) _isRejectEnable = widget.controller!.isRejectEnable!;
      if (widget.controller!.isProceedEnable != null) _isProceedEnable = widget.controller!.isProceedEnable!;

    }else{
      if (widget.isBackVisible != null) _isBackVisible = widget.isBackVisible!;
      if (widget.isNextVisible != null) _isNextVisible = widget.isNextVisible!;
      if (widget.isNewVisible != null) _isNewVisible = widget.isNewVisible!;
      if (widget.isSaveVisible != null) _isSaveVisible = widget.isSaveVisible!;
      if (widget.isSubmitVisible != null) _isSubmitVisible = widget.isSubmitVisible!;
      if (widget.isVoidVisible != null) _isVoidVisible = widget.isVoidVisible!;
      if (widget.isCloseVisible != null) _isCloseVisible = widget.isCloseVisible!;
      if (widget.isCopyVisible != null) _isCopyVisible = widget.isCopyVisible!;
      if (widget.isListVisible != null) _isListVisible = widget.isListVisible!;
      if (widget.isPrintVisible != null) _isPrintVisible = widget.isPrintVisible!;
      if (widget.isDownloadVisible != null) _isDownloadVisible = widget.isDownloadVisible!;
      if (widget.isApproveVisible != null) _isApproveVisible = widget.isApproveVisible!;
      if (widget.isReviseVisible != null) _isReviseVisible = widget.isReviseVisible!;
      if (widget.isRejectVisible != null) _isRejectVisible = widget.isRejectVisible!;
      if (widget.isProceedVisible != null) _isProceedVisible = widget.isProceedVisible!;


      if (widget.isBackEnable != null) _isBackEnable = widget.isBackEnable!;
      if (widget.isNextEnable != null) _isNextEnable = widget.isNextEnable!;
      if (widget.isNewEnable != null) _isNewEnable = widget.isNewEnable!;
      if (widget.isSaveEnable != null) _isSaveEnable = widget.isSaveEnable!;
      if (widget.isSubmitEnable != null) _isSubmitEnable = widget.isSubmitEnable!;
      if (widget.isVoidEnable != null) _isVoidEnable = widget.isVoidEnable!;
      if (widget.isCloseEnable != null) _isCloseEnable = widget.isCloseEnable!;
      if (widget.isCopyEnable != null) _isCopyEnable = widget.isCopyEnable!;
      if (widget.isListEnable != null) _isListEnable = widget.isListEnable!;
      if (widget.isPrintEnable != null) _isPrintEnable = widget.isPrintEnable!;
      if (widget.isDownloadEnable != null) _isDownloadEnable = widget.isDownloadEnable!;
      if (widget.isApproveEnable != null) _isApproveEnable = widget.isApproveEnable!;
      if (widget.isReviseEnable != null) _isReviseEnable = widget.isReviseEnable!;
      if (widget.isRejectEnable != null) _isRejectEnable = widget.isRejectEnable!;
      if (widget.isProceedEnable != null) _isProceedEnable = widget.isProceedEnable!;
    }

    return Container(
      width: double.infinity,
      padding: const EdgeInsets.only(top: 2),
      margin: const EdgeInsets.only(left: 0),
      decoration: BoxDecoration(
        color: GlobalStyle.toolbarBackgroundColor,
      ),
      child: Row(
        mainAxisSize: MainAxisSize.max,
        children: [
          Expanded(
            flex: 3,
            child: Container(
              padding: const EdgeInsets.fromLTRB(20, 0, 0, 0),
              child: ListView(
                scrollDirection: Axis.horizontal,
                children: [
                  toolbarIcon(iconImageAsset: "assets/icons/back(normal-light).png", iconLabel: "Back", isVisible: _isBackVisible, isEnable: _isBackEnable, onTapAction: widget.onBack),
                  toolbarIcon(iconImageAsset: "assets/icons/next(normal-light)b.png", iconLabel: "Next", isVisible: _isNextVisible, isEnable: _isNextEnable, onTapAction: widget.onNext),
                  toolbarIcon(iconImageAsset: "assets/icons/new(normal-light).png", iconLabel: "New", isVisible: _isNewVisible, isEnable: _isNewEnable, onTapAction: widget.onNew),
                  toolbarIcon(iconImageAsset: "assets/icons/save(normal-light).png", iconLabel: "Save", isVisible: _isSaveVisible, isEnable: _isSaveEnable, onTapAction: btnSave_Click),
                  toolbarIcon(iconImageAsset: "assets/icons/submit(normal-light).png", iconLabel: "Submit", isVisible: _isSubmitVisible, isEnable: _isSubmitEnable, onTapAction: btnSubmit_Click),
                  toolbarIcon(iconImageAsset: "assets/icons/void(normal-light).png", iconLabel: "Void", isVisible: _isVoidVisible, isEnable: _isVoidEnable, onTapAction: btnVoid_Click),
                  toolbarIcon(iconImageAsset: "assets/icons/close(normal-light).png", iconLabel: "Close", isVisible: _isCloseVisible, isEnable: _isCloseEnable, onTapAction: btnClose_Click),
                  toolbarIcon(iconImageAsset: "assets/icons/copy(normal-light).png", iconLabel: "Copy", isVisible: _isCopyVisible, isEnable: _isCopyEnable, onTapAction: widget.onCopy),
                  toolbarIcon(iconImageAsset: "assets/icons/list(normal-light).png", iconLabel: "List", isVisible: _isListVisible, isEnable: _isListEnable, onTapAction: btnList_Click),
                  toolbarIcon(iconImageAsset: "assets/icons/print(normal-light).png", iconLabel: "Print", isVisible: _isPrintVisible, isEnable: _isPrintEnable, onTapAction: widget.onPrint),
                  toolbarIcon(iconImageAsset: "assets/icons/download(normal-light).png", iconLabel: "Download", isVisible: _isDownloadVisible, isEnable: _isDownloadEnable, onTapAction: widget.onDownload),
                  toolbarIcon(iconImageAsset: "assets/icons/approve(normal-light).png", iconLabel: "Approve", isVisible: _isApproveVisible, isEnable: _isApproveEnable, onTapAction: btnApprove_Click),
                  toolbarIcon(iconImageAsset: "assets/icons/revise(normal-light).png", iconLabel: "Revise", isVisible: _isReviseVisible, isEnable: _isReviseEnable, onTapAction: btnRevise_Click),
                  toolbarIcon(iconImageAsset: "assets/icons/reject(normal-light).png", iconLabel: "Reject", isVisible: _isRejectVisible, isEnable: _isRejectEnable, onTapAction: btnReject_Click),
                  toolbarIcon(iconImageAsset: "assets/icons/proceed(normal-light).png", iconLabel: "Proceed", isVisible: _isProceedVisible, isEnable: _isProceedEnable, onTapAction: widget.onProceed),
                ],
              ),
            ),
          ),
          GlobalStyle.toolbarShowTitleDivider ? const VerticalDivider(color: Colors.grey,) : Container(),
          Expanded(
            flex: 1,
            child: Container(
              padding: const EdgeInsets.fromLTRB(0, 0, 20, 0),
              alignment: Alignment.centerRight,
              child: Tooltip(
                message: strTitleMenu,
                child: Text(
                  strTitleMenu,
                  style: TextStyle(
                    fontSize: 17,//18,
                    fontFamily: GlobalStyle.fontFamily,
                    fontWeight: FontWeight.bold,
                    color: GlobalStyle.toolbarColor,
                    overflow: TextOverflow.ellipsis,
                  ),
                ),
              ),
            ),
          ),

        ],
      ),
    );
  }

  Widget toolbarIcon({
    required String iconImageAsset ,
    bool isVisible = false,
    bool isEnable = true,
    String iconLabel = "",
    Function()? onTapAction
  })
  {

    return Visibility(
      visible: isVisible,
      child: HoverExtender(
        builder: (isHovered) {
          Widget? iconImage;
          // Color? iconBackgroundColor;
          Widget? iconContent;

          String strImageNormal = iconImageAsset ?? "";
          String strImageHover = strImageNormal.replaceAll("(normal-", "(mouseover-");
          String strImageDisable = strImageNormal.replaceAll("(normal-", "(disable-");

          iconContent = Tooltip(
            message: iconLabel,
            child: SizedBox(
              height: double.infinity,
              child: Image.asset(
                isEnable ? (isHovered ? strImageHover : strImageNormal) : strImageDisable,
                fit: BoxFit.fill,

              ),
            ),
          );

          if(isEnable){
            int intTapCount = 0;
            return InkWell(
                onTap: () {
                  intTapCount++;
                  // debugPrint("onTap $intTapCount");
                  if(intTapCount == 1){
                    // debugPrint("onTapAction");
                    if (onTapAction != null) {
                      onTapAction();
                    }
                  }
                },
                child: iconContent
            );
          }

          return iconContent;
        },
      ),
    );
  }

}

class ToolbarBoxController{
  bool? isBackVisible;
  bool? isNextVisible;
  bool? isNewVisible;
  bool? isSaveVisible;
  bool? isSubmitVisible;
  bool? isVoidVisible;
  bool? isCloseVisible;
  bool? isCopyVisible;
  bool? isListVisible;
  bool? isPrintVisible;
  bool? isDownloadVisible;
  bool? isApproveVisible;
  bool? isReviseVisible;
  bool? isRejectVisible;
  bool? isProceedVisible;

  bool? isBackEnable;
  bool? isNextEnable;
  bool? isNewEnable;
  bool? isSaveEnable;
  bool? isSubmitEnable;
  bool? isVoidEnable;
  bool? isCloseEnable;
  bool? isCopyEnable;
  bool? isListEnable;
  bool? isPrintEnable;
  bool? isDownloadEnable;
  bool? isApproveEnable;
  bool? isReviseEnable;
  bool? isRejectEnable;
  bool? isProceedEnable;
}