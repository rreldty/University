import 'package:flutter/material.dart';

class LoadingIndicator extends StatelessWidget {
  @override
  Widget build(BuildContext context) => Center(
    child: SizedBox(
      child: CircularProgressIndicator(),
      height: 50.0,
      width: 50.0,
    ),
  );
}