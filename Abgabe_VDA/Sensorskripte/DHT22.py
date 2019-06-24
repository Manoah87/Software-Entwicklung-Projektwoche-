# Adafruit_DHT ist die Bibliothek zum steuern des Sensors.
import Adafruit_DHT
import sys
from DBAccess._models.EventLog import *

# Die Klasse dient zum auslesen der Luftfeuchtigkeit und der Temperatur.
class DHT22():
# In den Methoden wurde aus 'self' verzichtet damit man die Funktion direkt aufrufen kann. DHT22.GetTemperatur(4). 
# Würde man erst eine Instanz erzeugen wollen müsste man das self wieder ergänzen.
    @staticmethod
    def GetHumidity(gpio : int = -1):
        if gpio == -1:
            return "Error GPIO nicht definiert"
        
        s = Adafruit_DHT.DHT22
        Lf, Temp = Adafruit_DHT.read_retry(s, gpio)

        try:
            EventLogInfoProvider.LogInformation(source = "DHT22", description = "Value Type. " + str(type(Lf)) )
            return float(Lf)  #"{0:.2f}".format(Lf)
        except:
            EventLogInfoProvider.LogException("DHT22 error:",sys.exc_info())
            return Lf
    
    @staticmethod
    def GetTemperature(gpio : int = -1):
        if gpio == -1:
            return "Error GPIO nicht definiert"
        
        s = Adafruit_DHT.DHT22
        Lf, Temp = Adafruit_DHT.read_retry(s, gpio)
        return float(Temp)
        #return "{0:.2f}".format(Temp)