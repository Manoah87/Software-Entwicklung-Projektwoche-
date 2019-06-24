import sys
from django.db import models
from django.utils.translation import ugettext_lazy as _
import mysql.connector
from DBAccess._models.Project import ProjectInfo
from DBAccess._models.Sensor import SensorInfo

from json import JSONEncoder
import json

class SensorValueInfo(models.Model):
    SensorValueID = models.AutoField(primary_key=True)
    Project = models.ForeignKey(ProjectInfo,db_column='ProjectID', on_delete=models.CASCADE)
    Sensor = models.ForeignKey(SensorInfo, db_column='SensorID', on_delete=models.CASCADE)
    Value = models.DecimalField(_('Value'), default=None, max_digits=9,decimal_places=2 )
    Timestamp = models.DateTimeField(_('Timestamp'), default=None)
    
    class Meta:
        db_table = "sensor_value";

class SensorValueInfoEncoder(JSONEncoder):
    def default(self, object):

        if isinstance(object, SensorValueInfo):
            return object.__dict__

        else:
            return json.JSONEncoder.default(self, object)

class SensorValueInfoProvider():
    @staticmethod
    def GetSensorValueInfo(sensorValueId = 0):
        """
            Wenn eine sensorValueId übergeben wird, wird der CodeName ignoriert. 
        """
        try:
            if sensorValueId > 0:
                return RelaisInfo.objects.get(pk = sensorValueId)
            else:
                return "NoResult" 
        except:
            EventLogInfoProvider.LogException("Unexpected error:",sys.exc_info())
            return "Unbehandelte Ausnahme"

    @staticmethod
    def GetSensorValues(where: str = "", order: str = ""): 
       sql_cmd = "SELECT * FROM sensor_value "
       if where != "":
           sql_cmd += "WHERE " + where + " "
       
       if order != "":
           sql_cmd += "ORDER BY " + order + " "
           
       sql_cmd += "LIMIT 15;"
 
       try:
           #Muss geprüft werden, ob sicher, (Testkonzept)
          return SensorValueInfo.objects.raw(sql_cmd)
       except mysql.connector.Error as err:
           return err

