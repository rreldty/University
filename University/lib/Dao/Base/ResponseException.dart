class ResponseException implements Exception {
  final _message;
  final _prefix;

  ResponseException([this._message, this._prefix]);

  String toString() {
    return "$_prefix$_message";
  }
}

class FetchDataException extends ResponseException {
  FetchDataException([String message = ""])
      : super(message, "[Communication Error] ");
}

class BadRequestException extends ResponseException {
  BadRequestException([String message = ""]) : super(message, "[400] ");
}

class UnauthorizedException extends ResponseException {
  UnauthorizedException([String message = ""]) : super(message, "[401] ");
}

class InvalidInputException extends ResponseException {
  InvalidInputException([String message = ""]) : super(message, "[400] ");
}

class InternalServerException extends ResponseException {
  InternalServerException([String message = ""]) : super(message, "[500] ");
}