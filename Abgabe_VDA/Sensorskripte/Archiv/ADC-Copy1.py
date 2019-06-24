# Trocken ca. 22690, sehr feucht ca. 9000

# Import ADS1x15 Modul.
import Adafruit_ADS1x15

# ADS1115 ADC (16-bit) instanziert.
adc = Adafruit_ADS1x15.ADS1115()

class FeuchtigkeitsSensor():
    
    def __init__(self, GAIN, AnalogIn):
        self.GAIN = GAIN
        self.AnalogIn = AnalogIn
        
    def feuchtigkeit(self):
        print(adc.read_adc(self.AnalogIn, gain=self.GAIN))

f = FeuchtigkeitsSensor(1 , 0)
f.feuchtigkeit()

# Testskript

#import time

# Import the ADS1x15 module.
#import Adafruit_ADS1x15

# Create an ADS1115 ADC (16-bit) instance.
#adc = Adafruit_ADS1x15.ADS1115()

# Note you can change the I2C address from its default (0x48), and/or the I2C
# bus by passing in these optional parameters:
#adc = Adafruit_ADS1x15.ADS1015(address=0x49, busnum=1)

# Choose a gain of 1 for reading voltages from 0 to 4.09V.
# Or pick a different gain to change the range of voltages that are read:
#  - 2/3 = +/-6.144V
#  -   1 = +/-4.096V
#  -   2 = +/-2.048V
#  -   4 = +/-1.024V
#  -   8 = +/-0.512V
#  -  16 = +/-0.256V
# See table 3 in the ADS1015/ADS1115 datasheet for more info on gain.
#   GAIN = 1
#   AnalogIn = 0

#  count=0
#  for i in range(0,50):
#      print(adc.read_adc(AnalogIn, gain=GAIN))
#      time.sleep(2)
#      count= count +1