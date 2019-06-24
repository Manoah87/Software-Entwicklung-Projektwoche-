#!/usr/bin/python3
#
# EventLog.py
#   EventLog Module 
#
# 24.11.2018 / Michael Pfister
#

# Beginn des Programmes

import sys
import os.path
import datetime as dt
from enum import Enum
from django.db import models
from django.utils.translation import ugettext_lazy as _
from DBAccess.MySqlHelper import MySqlHelper
from DBAccess.Singleton import Singleton
from DBAccess._models.CustomUser import *
from SchedulerAPI.TimezoneHelper import TimezoneHelper

def enum(**named_values):
    return type('Enum', (), named_values)

EventType = enum(ERROR='E', INFORMATION ='I', WARNING='W')

class EventLogInfo(models.Model):
    """
        Repräsentiert die Tabele Eventlog
    """
    EventID = models.AutoField(primary_key=True)
    EventType = models.CharField(_('Event-Typ'), max_length=5, blank=False, default="")
    EventTime = models.DateTimeField(_('Logzeit'))
    Source = models.CharField(_('Source'), max_length=100, blank=False, default="")
    EventDescription = models.TextField(_('Beschreibung'), default="")
    EventUrl = models.CharField(_('URL'), max_length=255, blank=False, default="")
    EventMachineName = models.CharField(_('MachineName'), max_length=100, blank=False, default="")
    EventUserAgent = models.CharField(_('UserAgent'), max_length=400, blank=False, default="")
    EventUrlReferrer = models.CharField(_('Url Referrer'), max_length=255, blank=False, default="")
    UserInfo = models.ForeignKey(UserInfo,db_column="UserID", on_delete=models.SET(-1))

    class Meta:
        db_table = "eventlog";


class EventLogInfoProvider(MySqlHelper):
    """
       Bietet Funktionen, für das Arbeiten mit den Eventlog
    """

    @staticmethod
    def ClearEventLog():
        """
        Löscht alle Einträge im Event-Log und erzeugt ein Logeintrag.
        """
        try:
            EventLogInfo.objects.clear()
            EventLogInfoProvider.LogInformation()
        except mysql.connector.Error as err:
            if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
                EventLogInfoProvider().LogDBErrorToFile(ErrorMessage = "Something is wrong with your user name or password")
            elif err.errno == errorcode.ER_BAD_DB_ERROR:
                EventLogInfoProvider().LogDBErrorToFile(ErrorMessage = "Database does not exist")
            else:
                EventLogInfoProvider().LogDBErrorToFile(ErrorMessage = err)
        except:
            EventLogInfoProvider.LogException("Unexpected error:",sys.exc_info())
    
    @staticmethod
    def GetEventLogInfo(eventId : int = 0):
        if eventId <= 0:
            return "EventID muss grösser 0 sein" 
        else:
           return EventLogInfo.objects.get(pk = eventId)

    @staticmethod
    def GetEvents(where: str = "", order: str = "EventTime DESC"):
       sql_cmd = "SELECT * FROM eventlog "
       if where != "":
           sql_cmd += "WHERE " + where + " "
       
       if order != "":
           sql_cmd += "ORDER BY " + order + " "
           
       sql_cmd += ";"
       
       try:
           return EventLogInfo.objects.raw(sql_cmd)
       except mysql.connector.Error as err:
           return err

    @staticmethod
    def GetPreviousNext():
        pass

    @staticmethod
    def LogEvent(event : EventLogInfo):
        event.save()

    @staticmethod
    def LogException(Exeption : str, exeption : tuple):
          Exception = EventLogInfo(EventType = EventType.ERROR, EventTime=TimezoneHelper.GetCurrentDatetime(), Source= str(exeption[0]), EventDescription = str(exeption[1]), UserInfo = UserInfo.objects.filter(UserName = "public")[0])
          Exception.save()

    @staticmethod
    def LogInformation(source : str, description : str):
        Event = EventLogInfo(EventType = EventType.INFORMATION, EventTime = TimezoneHelper.GetCurrentDatetime(), Source=source, EventDescription = description, UserInfo = UserInfo.objects.filter(UserName = "public")[0])
        Event.save()

    @staticmethod
    def LogWarning():
        pass

    @staticmethod
    def LogDbError():
        """
        Fehler werden in dem fall in einem Logfile gespeichert, als
        Fallback für den fall, dass die DB Verbindung Fehlschlägt.
        """
        pass

    @staticmethod
    def LogDBErrorToFile(ErrorMessage: str, ErrorTitle: str = "(Kein Titel)"):
        """
        Wenn im Event-Log ein Fehler beim Zugriff auf die Datenbank geschieht, wird die
	    Funktion verwendet, um den Fehler in einem Logfile zu Protokolieren.
        """
        PROJECT_ROOT = os.path.dirname(os.path.abspath(__file__))
        HeaderHTML = ""
        if os.path.isfile(PROJECT_ROOT + '\\log\\DB_ErrorLog.log') == False:
            HeaderHTML = "Datenbank Error-Log\r\n\r\n"

        log = open(PROJECT_ROOT + '\\log\\DB_ErrorLog.log', 'a') #Datei wird ebenfalls erstellt
        if HeaderHTML != "":
            log.write(HeaderHTML)
        
        log.write(str(TimezoneHelper.GetCurrentDatetime()) + ": " +  ErrorTitle + "\n" + ErrorMessage + "\r\n")
