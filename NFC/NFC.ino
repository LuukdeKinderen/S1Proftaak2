
#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN 10
#define RST_PIN 9
 
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

  // Look for new cards
  if ( ! rfid.PICC_IsNewCardPresent())
    return;

  // Verify if the NUID has been readed
  if ( ! rfid.PICC_ReadCardSerial())
    return;



 for (byte i = 0; i < 4; i++) {
      nuidPICC[i] = rfid.uid.uidByte[i];
    }
   
  //printHex(rfid.uid.uidByte, rfid.uid.size);
    Serial.println();
  rfid.PICC_HaltA();

  String payloadString = "FxFF 1 ";
  for (int i; i < 4; i++) {
    payloadString += nuidPICC[i];
  }
  payloadString += " FxF0";
  Serial.println(payloadString);
  
//  Serial.println("FxFF");
//  Serial.println("1");
//  printHex(rfid.uid.uidByte, rfid.uid.size);

 //Recieving data from c#
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


void printHex(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? " 0" : " ");
    Serial.print(buffer[i], HEX);
  }
  
}


void RunCodeIfRead() {
  
}
