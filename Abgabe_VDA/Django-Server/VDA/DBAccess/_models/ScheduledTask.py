from django.db import models
from django.utils.translation import ugettext_lazy as _
import datetime as dt
from DBAccess._models.EventLog import *

from json import JSONEncoder
import json

def enum(**named_values):
    return type('Enum', (), named_values)

IntervalUnit = enum(MONTH='m', DAY='day', HOUR='h', MINTUTES='min')

class TaskIntervalEncoder(JSONEncoder):
    def default(self, object):

        if isinstance(object, TaskInterval):
            return object.__dict__
        else:
            return json.JSONEncoder.default(self, object)

class TaskInterval(json.JSONEncoder):
    IntervalUnit = None
    Amount       = None

    def __init__(self, intervalUnit : IntervalUnit, amount : int):
        self.IntervalUnit = intervalUnit
        self.Amount = amount

class ScheduledTaskInfo(models.Model):
    ScheduledTaskID = models.AutoField(primary_key=True)
    TaskCodeName = models.CharField(_('Codename'), max_length=100, blank=False, default="", unique=True)
    TaskNameDisplay = models.CharField(_('Anzeigename'), max_length=100, blank=False, default="")
    FilePath = models.CharField(_('FilePath'), max_length=255, blank=True, default="",unique=False)
    
    SENSOREN = "sensor"
    FILE = "file"
    RELAIS = "relais"

    OBJECTTYPES = (
        (SENSOREN, 'Sensor'),
        (FILE, 'File'),
        (RELAIS,'relais'),
    )

    ObjectType = models.CharField(_('Objekttyp'), max_length=30, unique = False, choices=OBJECTTYPES, default=FILE)
    ObjectID = models.IntegerField(_('ObjectID'), default=0, blank=False)
    Interval = models.TextField(_('Interval'))
    LastRunTime = models.DateTimeField(_('Letzte Ausführung'),blank=True, null = True)
    NextRunTime = models.DateTimeField(_('Nächste Ausführung'), null = True)
    Executions = models.IntegerField(_('Ausführungen'), default=0)
    DeleteAfterLastRun = models.BooleanField(_('Nach letztem Ausführen Löschen'),default = False)
    Enabled = models.BooleanField(_('Aktiviert'), default = True)
    IsRunning = models.BooleanField(_('Wird Ausgeführt'), default = False)

    class Meta:
        db_table = "scheduled_task";

class ScheduledTaskInfoProvider():
    
    @staticmethod
    def GetTaskToRun():
        """
        Liefert die Auszuführenden Aufgaben zurück und setzt die neuer Zeiten
        """
        sql_cmd = "SELECT * FROM scheduled_task where NextRunTime <= UTC_TIMESTAMP()"
        try:
            #Muss geprüft werden, ob sicher, (Testkonzept)
            #Wenn None (NULL), dann wird ein Fehler zurück gegeben.
            return ScheduledTaskInfo.objects.raw(sql_cmd)
        except mysql.connector.Error as err:
            return err
        
        
    @staticmethod
    def GetScheduledTaskInfo(scheduledTaskId = 0, codeName = ""):
        """
            Wenn eine ScheduledTaskID übergeben wird, wird der CodeName ignoriert. 
        """
        try:
            if scheduledTaskId > 0:
                return ScheduledTaskInfo.objects.get(pk = scheduledTaskId)
            else:
                if len(ScheduledTaskInfo.objects.filter(ScheduledTaskCodeName = codeName)) > 0:
                    return ScheduledTaskInfo.objects.filter(ScheduledTaskCodeName = codeName)[0]
                else:
                    #Errormeldungen als enum oder prüfen ob fehlermeldungen geworfen werden können
                    return "NoResult" 
        except:
            EventLogInfoProvider.LogException("Unexpected error:",sys.exc_info())
            return "Unbehandelte Ausnahme"

    @staticmethod
    def GetScheduledTasks(where: str = "", order: str = ""): 
       sql_cmd = "SELECT * FROM scheduled_task "
       if where != "":
           sql_cmd += "WHERE " + where + " "
       
       if order != "":
           sql_cmd += "ORDER BY " + order + " "
           
       sql_cmd += ";"
 
       try:
           #Muss geprüft werden, ob sicher, (Testkonzept)
           #Wenn None (NULL), dann wird ein Fehler zurück gegeben.
          return ScheduledTaskInfo.objects.raw(sql_cmd)
       except mysql.connector.Error as err:
           return err
        
