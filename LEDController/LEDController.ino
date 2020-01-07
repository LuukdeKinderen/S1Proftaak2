#include <FastLED.h>

// Settings
  #define stripPin0 9
  #define stripPin1 8
  #define numberLed 120
  #define numberStrip 2
  #define conformationBtn 10
  #define pot A0
  
  CRGB leds[numberStrip][numberLed];

// Keyword Decliration
  String Keyword_Start = "FxFF";

// Variable Decliration
  char bufferInput[1000];


void setup() {
  Serial.begin(115200);

  FastLED.addLeds<WS2812, stripPin0, GRB>(leds[0], numberLed); 
  FastLED.addLeds<WS2812, stripPin1, GRB>(leds[1], numberLed); 

  pinMode(conformationBtn, INPUT);
  pinMode(pot, INPUT);

  for (int i = 0; i < 120; i++) {
      leds[0][i] = CRGB(0, 0, 0);
      leds[1][i] = CRGB(0, 0, 0);
    }
  FastLED.show();  

  for (int i = 0; i < 1000; i++) {
      bufferInput[i] = 0;
  }
}

bool selecting= false;
void loop() {

  //Replace button interreupt with NFC
  if(conformationBtnPressed() && !selecting){
    selecting = true;
    Serial.print("FxFF 3 GetStations~");
  }


  
  int readBit = 0;
  bool done;
  if (Serial.available()) {
    
    Serial.readBytesUntil('~', bufferInput, 1500); 




    while (true) {
      for (int i = 0; i < 4; i++) {
        if (bufferInput[i + readBit] == Keyword_Start[i]) {
          if (i == 3) { done = true; }
        } else { break; }
      }

      if (!done) {
        readBit++;
        if (bufferInput[readBit] == 0) {
          CleanUp();
          return 0;
        }
      } else { break; }
    }

    readBit += 5; 
    int adress = bufferInput[readBit] - '0';
    readBit += 2;


    switch(adress) {
      case 9:
          for (int i = 0; i < 120; i++) {
      leds[0][i] = CRGB(0,0,0); 
      leds[1][i] = CRGB(0,0,0);
    }
        while (true) {
          byte readByte[3];
          byte byteRGB[3];
          char readID[3];
          byte readIDByte;
          
          if (bufferInput[readBit] != 0) {
            for (int i = 0; i < 3; i++) {
              readID[i] = bufferInput[readBit] - '0';
              readBit++;
            }

            readIDByte = readID[0] * 100 + readID[1] * 10 + readID[2];

            for (int i = 0; i < 3; i++) {
              for (int x = 0; x < 3; x++) {

                readByte[x] = bufferInput[readBit] - '0';
                readBit++;
                
              }
              
              byteRGB[i] = readByte[0] * 100 + readByte[1] * 10 + readByte[2];
              
            }

            if (readIDByte < 120) {
              leds[0][readIDByte] = CRGB(byteRGB[0], byteRGB[1], byteRGB[2]);
            } else {
              leds[1][readIDByte - 120] = CRGB(byteRGB[0], byteRGB[1], byteRGB[2]);
            }
            
          } else { 
            CleanUp();
            FastLED.show();
            break; 
          }
        }

      
      break;
      case 3:

      int stationsCount = bufferInput[readBit] - '0';
      readBit++;
      
      byte stations[stationsCount][4];

      for(int i = 0; i<stationsCount; i++){
        byte readByte[3];
        for(int ri = 0; ri<3; ri++){
          readByte[ri] = bufferInput[readBit] - '0';
          readBit++;
        }
        byte startind = readByte[0] * 100 + readByte[1] * 10 + readByte[2];
        for(int ri = 0; ri <4; ri++){
          stations[i][ri] = startind+ri;
        }
      }
      
      while(selecting){
     
        int mappedPotMeter = map(analogRead(pot), 0, 1023, 0, stationsCount);
            for (int i = 0; i < 120; i++) {
      leds[0][i] = CRGB(0,0,0); 
      leds[1][i] = CRGB(0,0,0);
    }FastLED.show();
            for(int i = 0; i<4; i++){
          leds[0][stations[mappedPotMeter][i]] = CRGB(0, 255, 0);
          leds[1][stations[mappedPotMeter][i]] = CRGB(0, 255, 0);
        }
        FastLED.show();  

        if(conformationBtnPressed()){
          String command = "FxFF 3 "+String(mappedPotMeter)+"~";
          Serial.print(command);
          CleanUp();
          selecting = false;    
        }
      }
      break;
      
    }
  }
}



void CleanUp() {
  while(Serial.available()) {
    Serial.read();
  }
}
