import RPi.GPIO as gpio

import Adafruit_DHT

class DHT22():    
    def __init__(self, Temp, pin):
        self.pin = pin
        self.Temp = Temp
        
    def SetValue(self):
        self.Lf, self.Temp = Adafruit_DHT.read_retry(Adafruit_DHT.DHT22, self.pin)    

    def init(self):
        gpio.setmode(gpio.BCM)
        gpio.setup(self.pin, gpio.OUT)
        self.Lf, self.Temp = Adafruit_DHT.read_retry(self.Temp, self.pin)   

    def DisplayValue(self):
        print(self.Lf)
        print(self.Temp)
        
    def clear(self):
        gpio.cleanup()