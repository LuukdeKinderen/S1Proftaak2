#include <FastLED.h>

#include <MFRC522.h>

#include <Keypad.h>

#include <Wire.h>

#include <LiquidCrystal_I2C.h>

// Settings
  #define stripPin0 9
  #define stripPin1 8
  #define numberLed 120
  #define numberStrip 2
  #define conformationBtn 10
  #define pot A0
  #define SS_PIN 53
  #define RST_PIN 49
  #define SP_PIN 50

CRGB leds[numberStrip][numberLed];

// Variable Decliration
char bufferInput[1000];
bool selecting = false;
String Keyword_Start = "FxFF";

//keypad
const byte ROWS = 4;
const byte COLS = 3;

char hexaKeys[ROWS][COLS] = {
  {'1', '2', '3'},
  {'4', '5', '6'},
  {'7', '8', '9'},
  {'*', '0', '#'}
};

byte rowPins[ROWS] = {12, 11, 7, 6}; 
byte colPins[COLS] = {5, 4, 3}; 
String keyString = "";


Keypad customKeypad = Keypad(makeKeymap(hexaKeys), rowPins, colPins, ROWS, COLS);
//end keypad

//lcd
LiquidCrystal_I2C lcd(0x27, 2, 1, 0, 4, 5, 6, 7, 3, POSITIVE);
//end lcd

//nfc
String UID = "";
MFRC522 rfid(SS_PIN, RST_PIN);
MFRC522::MIFARE_Key key;
byte nuidPICC[4];
//end nfc

void setup() {
  Serial.begin(250000);
  lcd.begin(16, 2);
  lcd.backlight();
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print("Scan een pas");
  lcd.setCursor(0, 1);
  lcd.print("om te beginnen");
  FastLED.addLeds < WS2812, stripPin0, GRB > (leds[0], numberLed);
  FastLED.addLeds < WS2812, stripPin1, GRB > (leds[1], numberLed);

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

  SPI.begin();
  rfid.PCD_Init();
  byte nuidPICC[4];
  for (byte i = 0; i < 4; i++) {
    key.keyByte[i] = 0xFF;
  }

}

void loop() {

  int readBit = 0;
  bool done;
  if (Serial.available()) {

    Serial.readBytesUntil('~', bufferInput, 1500);

    while (true) {
      for (int i = 0; i < 4; i++) {
        if (bufferInput[i + readBit] == Keyword_Start[i]) {
          if (i == 3) {
            done = true;
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

    switch (adress) {
    case 4:

      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print("Saldo vernieuwd");

      break;
    case 9:
      for (int i = 0; i < 120; i++) {
        leds[0][i] = CRGB(0, 0, 0);
        leds[1][i] = CRGB(0, 0, 0);
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
          if (!selecting && rfid.PICC_IsNewCardPresent()) {
            if (rfid.PICC_ReadCardSerial()) {
              for (byte i = 0; i < 4; i++) {
                nuidPICC[i] = rfid.uid.uidByte[i];
              }
              rfid.PICC_HaltA();
              String bufferUID = "";
              for (int i; i < 4; i++) {
                bufferUID += nuidPICC[i];
              }
              UID = ConvertUID(bufferUID);
              selecting = true;
              Serial.print("FxFF 3 GetStations~");
            }
          }

          break;
        }
      }

      break;
    case 3:

      int stationsCount = bufferInput[readBit] - '0';
      readBit++;

      byte stations[stationsCount][4];

      for (int i = 0; i < stationsCount; i++) {
        byte readByte[3];
        for (int ri = 0; ri < 3; ri++) {
          readByte[ri] = bufferInput[readBit] - '0';
          readBit++;
        }
        byte startind = readByte[0] * 100 + readByte[1] * 10 + readByte[2];
        for (int ri = 0; ri < 4; ri++) {
          stations[i][ri] = startind + ri;
        }
      }
      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print("Kies een station");
      lcd.setCursor(0, 1);
      lcd.print("of kies bedrag");
      while (selecting) {

        char customKey = customKeypad.getKey();

        if (customKey != NULL) {
          switch (customKey) {
          case 42:
            Serial.print("FxFF 2 " + UID + "," + keyString + "~");
            CleanUp();
            selecting = false;
            lcd.clear();

            keyString = "";
            return;
            break;

          case 35:
            keyString.remove(keyString.length() - 1);
            break;

          default:
            keyString += customKey;
            break;
          }
          customKey = "";
          lcd.clear();
          lcd.setCursor(0, 0);
          lcd.print("opwaarderen met:");
          lcd.setCursor(0, 1);
          lcd.print(keyString + " cent?");

        }

        int mappedPotMeter = map(analogRead(pot), 0, 1023, 0, stationsCount);
        int selected[4];
        for (int i = 0; i < 4; i++) {
          selected[i] = stations[mappedPotMeter][i];
        }
        for (int i = 0; i < 120; i++) {
          if (i < selected[0] || i > selected[3]) {
            leds[0][i] = CRGB(0, 0, 0);
            leds[1][i] = CRGB(0, 0, 0);
          } else {
            leds[0][i] = CRGB(0, 255, 0);
            leds[1][i] = CRGB(0, 255, 0);
          }
        }
        FastLED.show();

        if (conformationBtnPressed()) {

          String command = "FxFF 3 " + UID + "," + String(mappedPotMeter) + "~";
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
  while (Serial.available()) {
    Serial.read();
  }
}
