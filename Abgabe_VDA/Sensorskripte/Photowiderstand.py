#!/usr/bin/python

import spidev
import time 
import os
import RPi.GPIO as gpio

# SPI Verbindung herstellen

spi = spidev.SpiDev()
spi.open(0,0)

#Liest Daten von MPC3008
def analogEingang(channel):
    adc = spi.xfer2([1,(8+channel)<<4,0])
    data = ((adc[1]&3) << 8) + adc[2]
    return data

while True:
    print("0: "+str(analogEingang(0)))
    print("1: "+str(analogEingang(1)))
    print("2: "+str(analogEingang(2)))
    print("3: "+str(analogEingang(3)))
    print("4: "+str(analogEingang(4)))
    print("5: "+str(analogEingang(5)))
    print("6: "+str(analogEingang(6)))
    time.sleep(0.7)