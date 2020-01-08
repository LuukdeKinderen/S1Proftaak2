 
#include <SPI.h>
#include <MFRC522.h>
#include <Keypad.h>

const byte ROWS = 4; 
const byte COLS = 3; 

char hexaKeys[ROWS][COLS] = {
  {'1', '2', '3'},
  {'4', '5', '6'},
  {'7', '8', '9'},
  {'*', '0', '#'}
};

byte rowPins[ROWS] = {9, 8, 7, 6}; 
byte colPins[COLS] = {5, 4, 3}; 
String keyString = "";

Keypad customKeypad = Keypad(makeKeymap(hexaKeys), rowPins, colPins, ROWS, COLS); 


#define SS_PIN 10
#define RST_PIN 2
 
MFRC522 rfid(SS_PIN, RST_PIN); // Instance of the class

MFRC522::MIFARE_Key key;

// Init array that will store new NUID
byte nuidPICC[4];

void setup() {
  Serial.begin(9600);
  SPI.begin(); // Init SPI bus
  rfid.PCD_Init(); // Init MFRC522

  for (byte i = 0; i < 4; i++) {
    key.keyByte[i] = 0xFF;
  }

 
}
 
void loop() {

  char  customKey = customKeypad.getKey();

  if (customKey != NULL) {
    switch(customKey) {
      case 42:
        keyString = ""; 
        Serial.println(F("Leeg"));
        break;

      case 35:
        keyString.remove(keyString.length() - 1);
        break;

      default:
        keyString += customKey;
        break;
    }
    customKey = "";  
 
  }
  
  // Look for new cards
  if (rfid.PICC_IsNewCardPresent()) {
   
  // Verify if the NUID has been readed
  if ( rfid.PICC_ReadCardSerial()) {


      for (byte i = 0; i < 4; i++) {
        nuidPICC[i] = rfid.uid.uidByte[i];
      }
       
    
       Serial.println();
      rfid.PICC_HaltA();

      if (keyString == "") {
      
      String payloadString = "FxFF 1 ";
      String bufferPayload = "";
      for (int i; i < 4; i++) {
        bufferPayload += nuidPICC[i];
      }
      payloadString += ConvertUID(bufferPayload);
      payloadString += "~";
      Serial.println(payloadString);
       
    } else {
      
      String payloadString = "FxFF 2 ";
      payloadString += keyString.length();
      payloadString += "_";
      payloadString += keyString;
      payloadString += " ";
      
      for (int i; i < 4; i++) {
        payloadString += nuidPICC[i];
      }
      
      payloadString += "~";
      Serial.println(payloadString);

       keyString = "";  
    }
    
    if (Serial.available()) {
       if (Serial.read() == "FxFF") {
         char readArray[500];
         float startTime = millis();
         int count = 0;
           
         while (!Serial.available()) {
             if ((startTime - millis()) > 5000) {
               return;
             }
           }
          
         char adress = Serial.read(); 
    
         while (true) {
           while (!Serial.available()) {
             if ((startTime - millis()) > 5000) {
               return;
             }
           }
    
           readArray[count] = Serial.read();
           count++;
    
           if (readArray[count] == 'FXF0') {
              RunCodeIfRead();
              return;
           }
          }
        }
      }
    }
  }
}


void printHex(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? " 0" : " ");
    Serial.print(buffer[i], HEX);
  }
  
}


void RunCodeIfRead() {
  
}

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
}
