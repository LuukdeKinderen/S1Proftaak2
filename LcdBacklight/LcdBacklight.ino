#define buzzer 13

#include <Wire.h> 
#include <LiquidCrystal_I2C.h>

LiquidCrystal_I2C lcd(0x27, 2, 1, 0, 4, 5, 6, 7, 3, POSITIVE); 


void setup()  
{
  Serial.begin(9600);  // Used to type in characters

  lcd.begin(16,2);         // initialize the lcd for 20 chars 4 lines and turn on backlight

 
  lcd.backlight(); // finish with backlight on  

//-------- Write characters on the display ----------------
// NOTE: Cursor Position: CHAR, LINE) start at 0  
  lcd.setCursor(3,0); //Start at character 4 on line 0
  lcd.print("Hello, world!");
  delay(1000);
  lcd.clear();
  lcd.setCursor(2,1);
  lcd.print("From Fromas");
  lcd.clear();
  delay(1000);
  lcd.setCursor(0,2);
  lcd.print("Gang shit bro");
  lcd.clear();
  lcd.setCursor(0,3);
  delay(2000);
  lcd.clear();
  lcd.print("Ez project");
  delay(8000);
  lcd.clear();
// Wait and then tell user they can start the Serial Monitor and type in characters to
// Display. (Set Serial Monitor option to "No Line Ending")
  lcd.setCursor(0,0); //Start at character 0 on line 0
  lcd.print("komt wel goed");
  lcd.setCursor(0,1);
  lcd.print("write serial");


}/*--(end setup )---*/


void loop()   /*----( LOOP: RUNS CONSTANTLY )----*/
{
  {
    // when characters arrive over the serial port...
    if (Serial.available()) {
      // wait a bit for the entire message to arrive
      delay(100);
      // clear the screen
      lcd.clear();
      // read all the available characters
      while (Serial.available() > 0) {
        // display each character to the LCD
        lcd.write(Serial.read());
      }
    }
  }
 }
