import 'package:global_configuration/global_configuration.dart';

enum NumericType {
  Amount,
  Factor,
  Percent,
  Price,
  Rate,
  Quantity,
  Unit,
}

class AppConfig {
  //region Config
  static String ApiBaseURL = "http://localhost:5000/";

  static String CompanyTypePrincipal = "P";
  static String DocStatusDraft = "D";
  static String DocStatusOpen = "O";
  static String DocStatusFinish = "F";
  static String DocStatusClose = "C";
  static String DocStatusCancel = "E";
  static String DocStatusApproved = "A";
  static String DocStatusRejected = "R";
  static String DocStatusWaitApprove = "WA";

  static String DocStatusDraftName = "Draft";

  static String ApprovalStatusDraft = "DR";
  static String ApprovalStatusWaitApprove = "WA";
  static String ApprovalStatusApprove = "AV";
  static String ApprovalStatusReject = "RJ";
  static String ApprovalStatusRevise = "RV";

  static String UploadTypeLinkFile = "LF";
  static String UploadTypeUploadFile = "UF";

  static String CloseStatusDraft = "10";
  static String CloseStatusPreActivity = "20";
  static String CloseStatusCloseActivity = "30";
  static String CloseStatusPostActivity = "40";
  static String CloseStatusSettlementPlan = "50";
  static String CloseStatusWriteOff = "60";
  static String CloseStatusDocumentChecklist = "70";

  static String NPWPFormat = "00.000.000.0-000.000";

  static String SystReady = "RD";
  static String SystFinish = "FN";
  static String StatDraft = "10";
  static String StatSubmitted = "11";
  static String StatWaitRelease = "20";
  static String StatConfirmed = "23";
  static String StatRevise = "50";
  static String StatCancelled = "99";
  static double RecordStatusActive = 1;
  static double RecordStatusInactive = 0;

  static String decimalSeparator = ".";
  static String thousandSeparator = (decimalSeparator == "." ? "," : ".");
  static Map<String, String> numericFormat = {
    "Amount": "#$thousandSeparator##0",
    "Factor": "#$thousandSeparator##0${decimalSeparator}00",
    "Price": "#$thousandSeparator##0",
    "Percent": "#$thousandSeparator##0${decimalSeparator}00",
    "Rate": "#$thousandSeparator##0${decimalSeparator}00",
    "Quantity": "#$thousandSeparator##0",
    "Unit": "#$thousandSeparator##0",
  };

  static String patternNumericDate = "yyyyMMdd";
  static String patternNumericTime = "HHmmss";
  static String patternNumericDateTime = "yyyyMMddHHmmss";
  static String patternStringDate = "dd-MM-yyyy";
  static String patternStringDateTime = "dd-MM-yyyy HH:mm";
  static String patternStringDateTimeSecond = "dd-MM-yyyy HH:mm:ss";

  static String Language = "EN";
  static int PageSize = 20;

  static String crumbHome = "/Controls/LandingPage";

  static String APNO_ControlPanel = "SYST";
  static String APNO_Workflow = "AGFL";
  static String APNO_ReportAnalytic = "RPAS";

  static String RCST_ACTIVE = "1";
  static String RCST_NOTACTIVE = "0";
//endregion
}
