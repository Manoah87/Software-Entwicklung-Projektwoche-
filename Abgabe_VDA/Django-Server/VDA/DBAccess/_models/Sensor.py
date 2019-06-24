#!/usr/bin/python3
#
# sensor.py
#   Sensor Module mit der Klasse SensorInfo und SensorInfoProvider
#
# 16.10.2018 / Michael Pfister
#

# Beginn des Programmes

import sys
from django.db import models
from django.utils.translation import ugettext_lazy as _
from DBAccess.MySqlHelper import MySqlHelper
from DBAccess._models.EventLog import EventLogInfoProvider

class SensorInfo(models.Model):
    """description of class"""
    SensorID = models.AutoField(_('SensorID'),primary_key=True)
    SensorCodeName = models.CharField(_('Codename'), max_length=100, blank=False, default="",unique=True)
    SensorDisplayName = models.CharField(_('Displayname'), max_length=100, blank=False, default="") 
    
    DHTH = "dht22_h"
    DHTT = "dht22_t"
    PhotoResistor = "resistor"

    SENSORTYPES = (
        (DHTH, 'DHT22 (Luftfeuchtigkeit)'),
        (DHTT, 'DHT22 (Temperatur)'),
        (PhotoResistor, 'Fotowiederstand'),
    )
    
    SensorType = models.CharField(_('Sensortyp'), max_length=50, blank=False, choices=SENSORTYPES, default=DHTH)
    GPIO = models.IntegerField(_('GPIO'), default=0, blank=False)
    Description = models.TextField(_('Beschreibung'), default="")
    ShowOnHome = models.BooleanField(_('Auf Startseite angezeigen ?'), default=False)
    SensorUnit = models.CharField(_('Einheit z.B. °C'), max_length=20, default="",blank=False)
    SensorCustomData = models.TextField(_('CustomData'))

    def __str__(self):
        return '%s' % (self.SensorDisplayName)

    class Meta:
        db_table = "sensor";

class SensorInfoProvider():
    """description of class"""
    # cursor = conn.cursor() ==> Ist so nicht möglich, da crusor manipuliert wird bei
    # bei jedem Methoden aufruf. Fall gleichzeitige Aufrufe geschen kann das zu einem Problem führen,
    # wenn der cursor nicht Thradsave ist.

  #  def __init__(self): Nicht mehr notwendig mit Django model
  #      print("init sensor provider")
        #super().__init__()
        # self.cursor = conn.cursor() ==> Ist so nicht möglich da Klasse nicht instanziiert wird.
    
    @staticmethod
    def GetSensorInfo(sensorId = 0, codeName = ""):
        """
            Wenn eine SensorID übergeben wird, wird der CodeName ignoriert. 
        """

        try:
            if sensorId > 0:
                return SensorInfo.objects.get(pk = sensorId)
            else:
                if len(SensorInfo.objects.filter(SensorCodeName = codeName)) > 0:
                    return SensorInfo.objects.filter(SensorCodeName = codeName)[0]
                else:
                    #Errormeldungen als enum oder prüfen ob fehlermeldungen geworfen werden können
                    return "NoResult" 
        except:
           EventLogInfoProvider.LogException("Unexpected error:",sys.exc_info())
           return "Unbehandelte Ausnahme"


    # Das Überladen von Methoden ist nicht möglich, muss mit Optionalen Paramether gearbeitet werden.
    #Generic list // list

    @staticmethod
    def GetSensors(where: str = "", order: str = ""): 
       sql_cmd = "SELECT * FROM sensor "
       if where != "":
           sql_cmd += "WHERE " + where + " "
       
       if order != "":
           sql_cmd += "ORDER BY " + order + " "
           
       sql_cmd += ";"
 
       try:
           #Muss geprüft werden, ob sicher, (Testkonzept)
          return SensorInfo.objects.raw(sql_cmd)
       except mysql.connector.Error as err:
           return err
        
