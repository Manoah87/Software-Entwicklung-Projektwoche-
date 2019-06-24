# Trocken ca. 22690, sehr feucht ca. 9000

# Import ADS1x15 Modul.
import Adafruit_ADS1x15

# ADS1115 ADC (16-bit) instanziert.
adc = Adafruit_ADS1x15.ADS1115()

class FeuchtigkeitsSensor():
    
    def __init__(self, GAIN, AnalogIn):
        self.GAIN = GAIN
        self.AnalogIn = AnalogIn
        
    def GetMoistureLevel(self):
        print(adc.read_adc(self.AnalogIn, gain=self.GAIN))

f = FeuchtigkeitsSensor(1 , 0)
f.GetMoistureLevel()