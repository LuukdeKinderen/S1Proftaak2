#include <FastLED.h> 

#define stripPin 10
#define numberLed 240

byte color[] = { 95, 255 , 196 };
int count = 0;

CRGB leds[numberLed];

void setup() {
  FastLED.addLeds<WS2812, stripPin, GRB>(leds, numberLed);

}

void loop() {
  for (int i = 0; i < numberLed; i++) {
    leds[i] = CRGB(color[0], color[1], color[2]);
    FastLED.show();
    delay(10);
    }

    switch(count) {
      case 0:
        color[0] = 255;
        color[1] = 0;
        color[2] = 0;
      break;
        color[0] = 0;
        color[1] = 255;
        color[2] = 0;
      case 1:
        color[0] = 0;
        color[1] = 0;
        color[2] = 255;
      break;
      case 2:
        color[0] = 255;
        color[1] = 255;
        color[2] = 0;
      break;
      case 3:
        color[0] = 0;
        color[1] = 255;
        color[2] = 255;
      break;
      case 4:
        color[0] = 255;
        color[1] = 255;
        color[2] = 255;
        count = 0;
      break;

    }

    count++;
  }
