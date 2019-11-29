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

String HexToInt(String input) {
  
}

void RollBuffer() {
  for (int z = 0; z < copyString.length(); z++) {
    copyString[z] = copyString[z + 1];
    copyString.remove(copyString.length() - 1); 
  }
}
