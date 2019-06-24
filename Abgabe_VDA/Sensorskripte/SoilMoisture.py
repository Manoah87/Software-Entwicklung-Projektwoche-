# Trocken ca. 22690, sehr feucht ca. 90000

# Import ADS1x15 Modul.
import Adafruit_ADS1x15

class FeuchtigkeitsSensor():
    
     def GetMoistureLevel(AnalogIn, GAIN):
        # ADS1115 ADC (16-bit) instanziert.
        adc = Adafruit_ADS1x15.ADS1115()
        # Analoder Input und Gain müsssen falls gewünscht direkt in der Funktion geändert werden.
        #AnalogIn = 0
        #GAIN = 1
        return float(adc.read_adc(AnalogIn, gain=GAIN))