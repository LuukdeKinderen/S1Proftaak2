#include <FastLED.h>

// Settings
  #define stripPin0 9
  #define stripPin1 8
  #define numberLed 120
  #define numberStrip 2
  
  CRGB leds[numberStrip][numberLed];


// Keyword
  String Keyword_Start = "FxFF";
  

// Variable Decliration
  char bufferInput[1500];
  double startTime;
  int enabledLEDs[1500];
  
  
void setup() {
  Serial.begin(9600); 

  FastLED.addLeds<WS2812, stripPin0, GRB>(leds[0], numberLed); 
  FastLED.addLeds<WS2812, stripPin1, GRB>(leds[1], numberLed); 

  for (int i = 0; i < 120; i++) {
      leds[0][i] = CRGB(0, 0, 0);
      leds[1][i] = CRGB(0, 0, 0);
    }
  FastLED.show();

      
    for (int i = 0; i < 240; i++) {
      enabledLEDs[i] = -1;
    }
}

void loop() {
  startTime = millis();
  int readBit = 0;
  bool done = false;
  int testLED = 0;
  
  if (Serial.available()) {

    for (int i = 0; i < 120; i++) {
      leds[0][i] = CRGB(0,0,0); 
      leds[1][i] = CRGB(0,0,0);
    }

    Serial.readBytesUntil('~', bufferInput, 1500); 

    while (true) {
      for (int i = 0; i < 4; i++) {
        //Serial.println(bufferInput[i + readBit]);
        if (bufferInput[i + readBit] == Keyword_Start[i]) {
          if (i == 3) {
            done = true;
            //Serial.println("Found!");
            
          }
        } else {
          break;
        }
      }

      if (!done) {
        readBit++;
        if (bufferInput[readBit] == 0) {
          CleanUp();
          return 0;
        }
      } else {
        break;
      }
    }
    
    readBit += 5;
    
    int adress = bufferInput[readBit] - '0';
    readBit += 2;

    switch(adress) {
      case 9:
        while(true){
          if(bufferInput[readBit] != 0) {

            int readArray[3];
            String hexString = "";
            int hexToIntArray[2];
            byte byteColorValue[3];
            
            for (int i = 0; i < 3; i++) {
              readArray[i] = bufferInput[readBit] - '0';
              readBit++;
            }
            int adressLED = readArray[0] * 100 + readArray[1] * 10 + readArray[2];

            testLED = 0;
            while (true) {
              if (enabledLEDs[testLED] == -1) {
                enabledLEDs[testLED] = adressLED;
                break;
              } else {
                testLED++;
              }
            }

            for (int i = 0; i < 6; i++) {
              hexString += bufferInput[readBit + i];
              if (int(bufferInput[readBit]) == 38) {
                break;
              }
                
            }
            
            if (int(bufferInput[readBit]) != 38) {
              for (int i = 0; i < 3; i++) {
                for (int x = 0; x < 2; x++) {
                  int nextInt = int(bufferInput[readBit + x]);
                  if (nextInt >= 48 && nextInt <= 57) { nextInt = map(nextInt, 48, 57, 0, 9); }
                  if (nextInt >= 65 && nextInt <= 70) { nextInt = map(nextInt, 65, 70, 10, 15); }
                  if (nextInt >= 97 && nextInt <= 102) { nextInt = map(nextInt, 97, 102, 10, 15); }
  
                  hexToIntArray[x] = nextInt;
                  readBit++;
                }
  
                
                int buffer16 = hexToIntArray[0] * 16;
                int buffer1 = hexToIntArray[1];
                byteColorValue[i] = buffer16 + buffer1;
                
              }
            } else {
              for (int i = 0; i < 3; i++) {
                byteColorValue[i] = 0;
              }
              readBit++;
            }

            for (int i = 0; i < 3; i++) {
              if (adressLED < 120) {
                leds[0][adressLED] = CRGB(byteColorValue[0], byteColorValue[1], byteColorValue[2]);
              } else {
                leds[1][adressLED - 120] = CRGB(byteColorValue[0], byteColorValue[1], byteColorValue[2]);
              }
            }
              
          } else {         
            
            break;            
          }
        }
        



      
      break;
    }
    CleanUp();
    FastLED.show();
  }


  
}









void CleanUp() {
  while(Serial.available()) {
    Serial.read();
  }

  double endTime = millis() - startTime;
  Serial.println(endTime);
  
  bufferInput[1500];
}
