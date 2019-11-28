
unsigned int hexToDec(String hexString) {
  
  String outputString;
  int nextInt;
  byte outputArray[6];
  byte byteOutputArray[3];
  
  for (int i = 0; i < hexString.length(); i++) {
    
    nextInt = int(hexString.charAt(i));
    if (nextInt >= 48 && nextInt <= 57) nextInt = map(nextInt, 48, 57, 0, 9);
    if (nextInt >= 65 && nextInt <= 70) nextInt = map(nextInt, 65, 70, 10, 15);
    if (nextInt >= 97 && nextInt <= 102) nextInt = map(nextInt, 97, 102, 10, 15);
    nextInt = constrain(nextInt, 0, 15);
    
    outputArray[i] = nexInt;
    
  }

for (int i = 0; i < 3; i++) {
  int buffer16 = outputArray[0] * 16;
  int buffer1 = outputArray[1];

  byte outputByte = buffer16 + buffer1;

  for (int x = 0; x < 6; x++) {
    outputArray[x] = outputArray[x + 2];
  }

  byteOutputArray[i] = outputByte;
}

  for (int i = 0; i < 3; i++) {
    outputString += byteOutputArray[i].length();
    outputString += byteOutputArray[i];
  }
  
  return outputString;
}
