// Keyword
  String Keyword_Start = "FxFF";
  String Keyword_Stop = "FxF0";

// Variable Decliration
  byte LEDStrip1[120][3];
  byte LEDStrip2[120][3];
  String copyString;

void setup() {
  Serial.begin(9600);  

}

void loop() {
  if (Serial.available()) {
    String inputString = Serial.readString();
    copyString = inputString;
    bool done = false;
    int intLength = 0;

    while (true) {
      for (int i = 0; i < 4; i++) {
        if (copyString[i] == Keyword_Start[i]) {
          done = true;
        } else {
          break;
        }
      }

      if (!done) {
        if (copyString.length() > 0) {
          RollBuffer();
        } else  {
          return 0;
        }
      } else {
        break;
      }
    }
    
    RollBuffer();

    char adress = copyString[0];

    RollBuffer(); 

    switch(adress) {
      case 9:
        for (int i = 0; i < 240; i++) {
          String hexValue = "";
          for (int x = 0; x < 6; x++) {
            hexValue += copyString[0];
            RollBuffer();
          }

          String intString = HexToInt(hexValue);
          byte byteValue[3];

          for (int x = 0; x < 3; x++) {
            intLength = atol(intString[0]);
            String bufferString = "";
            
            for (int y = 0; y < intLength; y++) {
              bufferString += intString[i + 1];   
            }
            
            byteValue[x] = bufferString.toInt();
            
          }

          for (int x = 0; x < 3; x++) {
            if (i < 120) {
              LEDStrip1[i][x] = byteValue[x];
            } else {
              LEDStrip2[i - 120][x] = byteValue[x];
            }
            
          }
        }
      
      break;










      

    }
    
  }
}

String HexToInt(String hexString) {
  
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
    
    outputArray[i] = nextInt;
    
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
    String carrierLength = "";
    
    if (byteOutputArray[i] < 10) {
      carrierLength = "1";
    }
    if (byteOutputArray[i] >= 10 && byteOutputArray[i] < 100) {
      carrierLength = "2";
    }
    if (byteOutputArray[i] >= 100) {
      carrierLength = "3";
    }

    
    outputString += carrierLength;
    outputString += byteOutputArray[i];
  }
  
  return outputString;
}

void RollBuffer() {
  for (int z = 0; z < copyString.length(); z++) {
    copyString[z] = copyString[z + 1];
    copyString.remove(copyString.length() - 1); 
  }
}
