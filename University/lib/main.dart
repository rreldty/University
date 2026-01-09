import 'package:flutter/material.dart';
import 'package:university/Views/Xample/XM010A_Application.dart';
import 'package:university/Views/Xample/XM050A_UserGroup.dart';

import 'Views/Training/Fakultas.dart';
import 'Views/Training/Jurusan.dart';
import 'Views/Training/KRSHeader.dart';
import 'Views/Training/MataKuliah.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: HomePage(), // Pastikan ada MaterialApp di root
    );
  }
}

class HomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Training")),
      body: Column(
        children: [
          Container(
            child: ElevatedButton(
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => XM050A_UserGroup()),
                );
              },
              child: const Text("Xample"),
            ),
          ),
          SizedBox(
            height: 10,
          ),
          Container(
            child: ElevatedButton(
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => Fakultas()),
                );
              },
              child: const Text("Fakultas"),
            ),
          ),
          SizedBox(
            height: 10,
          ),
          Container(
            child: ElevatedButton(
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => Jurusan()),
                );
              },
              child: const Text("Jurusan"),
            ),
          ),
          SizedBox(
            height: 10,
          ),
          Container(
            child: ElevatedButton(
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => MataKuliah()),
                );
              },
              child: const Text("Mata Kuliah"),
            ),
          ),
          SizedBox(
            height: 10,
          ),
          Container(
            child: ElevatedButton(
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => KRSHeader()),
                );
              },
              child: const Text("KRS"),
            ),
          ),
        ],
      ),
    );
  }
}
