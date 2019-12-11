<<<<<<< Updated upstream
=======
#include <FastLED.h> 





>>>>>>> Stashed changes
// Keyword
  String Keyword_Start = "FxFF";
  String Keyword_Stop = "FxF0";

// Variable Decliration
<<<<<<< Updated upstream
  byte LEDStrip1[120][3];
  byte LEDStrip2[120][3];
  String copyString;

void setup() {
  Serial.begin(9600);  

=======
  byte LEDStrip0[120][3];
  byte LEDStrip1[120][3];
  String copyString;
  int readBit = 0;

  #define stripPin0 9
  #define stripPin1 10
  
  #define numberLed 120
  #define numberStrip 2

  CRGB leds[numberStrip][numberLed];

void setup() {
  Serial.begin(9600); 
  FastLED.addLeds<WS2812, stripPin0, GRB>(leds[0], numberLed); 
  FastLED.addLeds<WS2812, stripPin1, GRB>(leds[1], numberLed); 
>>>>>>> Stashed changes
}

void loop() {
  if (Serial.available()) {
<<<<<<< Updated upstream
    String inputString = Serial.readString();
    copyString = inputString;
    bool done = false;
    int intLength = 0;

    while (true) {
      for (int i = 0; i < 4; i++) {
        if (copyString[i] == Keyword_Start[i]) {
          done = true;
=======
    Serial.println("Start!");
    double startTime = millis();
    
    readBit = 0;
    copyString = "";
    copyString = Serial.readString();
    
    bool done = false;
    int count = 0;

    while (true) {
      for (int i = 0; i < 4; i++) {
        if (copyString[i + readBit] == Keyword_Start[i]) {
          if (i == 3) {
            done = true;
          }
>>>>>>> Stashed changes
        } else {
          break;
        }
      }

      if (!done) {
        if (copyString.length() > 0) {
<<<<<<< Updated upstream
          RollBuffer();
=======
          readBit++;
>>>>>>> Stashed changes
        } else  {
          return 0;
        }
      } else {
        break;
      }
<<<<<<< Updated upstream
    }
    
    RollBuffer();

    char adress = copyString[0];

    RollBuffer(); 
=======
      
      count++;
      if (count > 100) {
        return 0;
      }
    }

    readBit += 5;

    char adress = copyString[readBit] - '0';
    
    readBit += 2;
>>>>>>> Stashed changes

    switch(adress) {
      case 9:
        for (int i = 0; i < 240; i++) {
<<<<<<< Updated upstream
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
      
=======

            //for (int p = 0; p < 6; p++) {
              //Serial.print(copyString[p + readBit]);
            //}
            //Serial.print(" ");
            //Serial.println(i);

//////////

          int nextInt = 0;
          byte outputArray[6] = { 0, 0, 0, 0, 0, 0 };
          byte byteValue[3] = { 0, 0, 0 };

          int buffer16 = 0;
          int buffer1 = 0;

          if (int(copyString.charAt(readBit) != 122)) {
            for (int i = 0; i < 3; i++) {
              for (int x = 0; x < 2; x++) {
              
                nextInt = int(copyString.charAt(x + readBit));
                if (nextInt >= 48 && nextInt <= 57) nextInt = map(nextInt, 48, 57, 0, 9);
                if (nextInt >= 65 && nextInt <= 70) nextInt = map(nextInt, 65, 70, 10, 15);
                if (nextInt >= 97 && nextInt <= 102) nextInt = map(nextInt, 97, 102, 10, 15);
                nextInt = constrain(nextInt, 0, 15);
                
                outputArray[x] = nextInt;           
              } 
              readBit += 2;
              
              buffer16 = outputArray[0] * 16;
              buffer1 = outputArray[1]; 
              byteValue[i] = buffer16 + buffer1; 
      
              
            } 
  
            for (int x = 0; x < 3; x++) {
              if (i < 120) {
                LEDStrip0[i][x] = byteValue[x];
                leds[0][i] = CRGB(byteValue[0], byteValue[1], byteValue[2]);
              } else {
                LEDStrip1[i - 120][x] = byteValue[x];
                leds[1][i - 120] = CRGB(byteValue[0], byteValue[1], byteValue[2]);
              }      
            }
            
            //for (int x = 0; x < 3; x++){
              //Serial.println(byteValue[x]);
            //}
  
            //Serial.println("");
          } else {
            readBit += 6;
          }
        }
        FastLED.show();
        FlushSerial();
        Serial.println("Done!");
        double timeDone =  (millis() - startTime) / 1000 ;
        Serial.println(timeDone);
>>>>>>> Stashed changes
      break;










      

    }
    
  }
}

<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes

void RollBuffer() {
  for (int z = 0; z < copyString.length(); z++) {
    copyString[z] = copyString[z + 1];
<<<<<<< Updated upstream
    copyString.remove(copyString.length() - 1); 
=======
  }
}

void FlushSerial() {
  while(Serial.available()) {
    Serial.read();
>>>>>>> Stashed changes
  }
}
