#!/usr/bin/python3
#
# MainSensorReader.py
#   
#
# 20.12.2018 / Michael Pfister
#

# Beginn des Programmes
from DBAccess._models.SensorValue import SensorValueInfo
from DBAccess._models.Sensor import *
from DBAccess._models.Project import ProjectInfoProvider
from RasPiAPI.PhotoResistor import PhotoResistor
from RasPiAPI.DHT22 import DHT22
from SchedulerAPI.TimezoneHelper import TimezoneHelper

import datetime as dt

class MainSensorReader(object):
    """
        MainSEnsorReader
    """
    
    @staticmethod
    def WriteSensorValueToDB(sensorId : int):
        sensor = SensorInfoProvider.GetSensorInfo(sensorId = sensorId)
        valueToSave = 0

        if sensor.SensorType == "resistor":
            valueToSave = PhotoResistor.GetValue(gpio = sensor.GPIO)
        
        if sensor.SensorType == "dht22_h":
            valueToSave = DHT22.GetHumidity(gpio = sensor.GPIO)

        if sensor.SensorType == "dht22_t":
            valueToSave = DHT22.GetTemperature(gpio = sensor.GPIO)

        value = SensorValueInfo(Project = ProjectInfoProvider.GetEnabledProject(), Sensor = sensor, Value = valueToSave, Timestamp = TimezoneHelper.GetCurrentDatetime())
        value.save()


