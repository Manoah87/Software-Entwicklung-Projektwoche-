#!/usr/bin/python
import RPi.GPIO as GPIO




class Relais():
    
    def __init__(self, relais):
        self.relais = relais
        # Dieser Befehl entscheidet ob auf Pins oder GPIO gesetzt wird.
        GPIO.setmode(GPIO.BCM) # GPIO Nummer statt Board Nummer
        GPIO.setup(relais, GPIO.OUT) # GPIO Modus zuweisen
    
        # Schaltet das Relais ein
    def relaisAN(self):
        GPIO.output(self.relais, GPIO.LOW) #AN
        
        # Schaltet das Relais Aus
    def relaisAUS(self):
        GPIO.output(self.relais, GPIO.HIGH) #AUS



r = Relais(21)

#r.relaisAN()
#r.relaisAUS()