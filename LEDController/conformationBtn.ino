int button0State;
int last0ButtonState = LOW;
unsigned long last0DebounceTime = 0;
unsigned long debounceDelay= 50;

bool conformationBtnPressed(){
  int reading = digitalRead(conformationBtn);
    if (reading != last0ButtonState) {
    last0DebounceTime = millis();
  }
  if ((millis() - last0DebounceTime) > debounceDelay) {
    if (reading != button0State) {
      button0State = reading;
      if (button0State == HIGH) {
        last0ButtonState = reading;
        return true;
      }
    }
  }
  last0ButtonState = reading;
  return false;
}
