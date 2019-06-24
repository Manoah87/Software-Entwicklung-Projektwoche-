from .RandomValueHelper import *
class FeuchtigkeitsSensor():
    
     def GetMoistureLevel(AnalogIn, GAIN):
        # ADS1115 ADC (16-bit) instanziert.
        adc = Adafruit_ADS1x15.ADS1115()
        # Analoder Input und Gain müsssen falls gewünscht direkt in der Funktion geändert werden.
        #AnalogIn = 0
        #GAIN = 1
        return RandomValueHelper.GetRandomInt(22690, 90000)
