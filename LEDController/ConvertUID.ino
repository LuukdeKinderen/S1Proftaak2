String ConvertUID(String bufferUID) {
  bool found = false;
  String outputUID = "";
  for (int i = 0; i < bufferUID.length(); i++) {
    if (bufferUID[i] != '0') {
      found = true;
    }
    if (found) {
      outputUID += bufferUID[i];
    }
  }
  return outputUID;
}
