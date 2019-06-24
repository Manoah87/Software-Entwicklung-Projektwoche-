#!/usr/bin/python
import RPi.GPIO as GPIO

class RelaisController():
    # Schaltet das Relais um
    def ToogleRelais(gpio : int = -1, debug : str = None):
        if gpio == -1:
            return "Error GPIO von Relais nicht definiert"
        # Dieser Befehl entscheidet ob auf Pins oder GPIO gesetzt wird.
        GPIO.setmode(GPIO.BCM) # GPIO Nummer statt Board Nummer
        GPIO.setup(gpio, GPIO.OUT) # GPIO Modus zuweisen
        state = GPIO.input(gpio)
        
        if state is True:
            GPIO.output(gpio, GPIO.LOW) #AN
            return True
        else:
            GPIO.output(gpio, GPIO.HIGH) #AUS
            return False



    def GetRelaisState(gpio : int = -1):
         if gpio == -1:
            return "Error GPIO von Relais nicht definiert"        
        
            GPIO.setmode(GPIO.BCM) # GPIO Nummer statt Board Nummer
            GPIO.setup(iRelais, GPIO.OUT) # GPIO Modus zuweisen
            state = GPIO.input(gpio)
            return not state # Wenn status low, dann ist das RElais an, damit der Were dem Relais Status entspricht wird er invertiert. 


        
        