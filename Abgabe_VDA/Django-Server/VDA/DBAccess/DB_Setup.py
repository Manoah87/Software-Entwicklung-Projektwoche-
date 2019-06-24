import datetime as dt
import json

from DBAccess._models.CustomUser import UserInfo
from DBAccess._models.Project import *
from DBAccess._models.Sensor import *
from DBAccess._models.Relais import *
from DBAccess._models.ScheduledTask import *
from DBAccess._models.ProjectSensor import *
from SchedulerAPI.TimezoneHelper import TimezoneHelper

#Muss über manage.py aufgerufen werden, da ansonsten die DJango Models nicht initzialisiert wurden.

def Users():
    u1 = UserInfo(UserName="public", is_superuser=False,FirstName = "",LastName = "",email = "", Enabled=True, DateCreated = TimezoneHelper.GetCurrentDatetime(),avatar="")
    u1.save();

    u2 = UserInfo(UserName="admin", is_superuser=False,FirstName = "",LastName = "",email = "mi.pfister@hotmail.com", Enabled=True, DateCreated = TimezoneHelper.GetCurrentDatetime(),avatar="")
    u2.save();

def Projects():

    p1 = ProjectInfo(ProjectCodeName = "test", ProjectDisplayName = "Erster Test", StartDate = TimezoneHelper.GetCurrentDatetime(), EndDate = TimezoneHelper.GetCurrentDatetime(), Enabled = True)
    p1.save();

def Relais():
    R1 = RelaisInfo(RelaisCodeName = "PL1", RelaisDisplayName = "Pflanzenlampe", GPIO = 0, Description = "Pflanzenlampe (An/Aus)")
    R1.save()
    
    R2 = RelaisInfo(RelaisCodeName = "BL1", RelaisDisplayName = "Bewässerungsanlage", GPIO = 0, Description = "Bewässerungsanlage (An/Aus)")
    R2.save()

    R3 = RelaisInfo(RelaisCodeName = "ABL", RelaisDisplayName = "Abluft", GPIO = 0, Description = "Abluft (An/Aus)")
    R3.save()

    R4 = RelaisInfo(RelaisCodeName = "UV", RelaisDisplayName = "Umluft Ventilator", GPIO = 0, Description = "Umluft Ventilator (An/Aus)")
    R4.save()

    R5 = RelaisInfo(RelaisCodeName = "LEB", RelaisDisplayName = "Luft Ent- und Befeuchtung", GPIO = 0, Description = "Luft Ent- und Befeuchtung")
    R5.save()

def Sensors():
    S1 = SensorInfo(SensorCodeName = "DHT22T_t", SensorDisplayName = "DHT 22 Oben", SensorType = "dht22", GPIO=14, Description = "Description", ShowOnHome = False)
    S1.save();

    S2 = SensorInfo(SensorCodeName = "DHT22M_t", SensorDisplayName = "DHT 22 Mitte", SensorType = "dht22",GPIO=14, Description = "Description", ShowOnHome = False)
    S2.save();

    S3h = SensorInfo(SensorCodeName = "DHT22B_h", SensorDisplayName = "DHT 22 Unten (Luftfeuchtigkeit)", SensorType = "dht22_h",GPIO=14, Description = "Description", ShowOnHome = True, SensorUnit = "%")
    S3h.save();
    
    S3 = SensorInfo(SensorCodeName = "DHT22B_t", SensorDisplayName = "DHT 22 Unten (Temperatur)", SensorType = "dht22_t",GPIO=14, Description = "Description", ShowOnHome = True, SensorUnit = "°C")
    S3.save();

    S4 = SensorInfo(SensorCodeName = "photowiderstand", SensorDisplayName = "Photowiderstand", SensorType = "resistor", GPIO=4, Description = "Photowiderstand", ShowOnHome = True, SensorUnit = "lm")
    S4.save()

def ScheduledTasks():
    IntervalOne = TaskInterval(IntervalUnit.MINTUTES, 100)
    strIntervalOne = TaskIntervalEncoder().encode(IntervalOne)
    task1 = ScheduledTaskInfo(TaskCodeName = "ReadPhotowiderstand", TaskNameDisplay = "Photowiderstand", Interval = strIntervalOne, ObjectID = SensorInfoProvider.GetSensorInfo(codeName="photowiderstand").SensorID, ObjectType = "Sensor", LastRunTime = None, NextRunTime = TimezoneHelper.GetCurrentDatetime(), Executions = 0, DeleteAfterLastRun = False, Enabled = True, IsRunning = False)
    FilePath = "relativ to file has been execute"
    task1.save()

    IntervalTwo = TaskInterval(IntervalUnit.MINTUTES, 15)
    strIntervalTwo = TaskIntervalEncoder().encode(IntervalTwo)
    task2 = ScheduledTaskInfo(TaskCodeName = "DHT 22 Unten", TaskNameDisplay = "DHT 22 Unten (Luftfeuchtigkeit)", Interval = strIntervalTwo, ObjectID = SensorInfoProvider.GetSensorInfo(codeName="DHT22B_h").SensorID, ObjectType = "Sensor", LastRunTime = None, NextRunTime = TimezoneHelper.GetCurrentDatetime(), Executions = 0, DeleteAfterLastRun = False, Enabled = True, IsRunning = False)
    task2.save()

    IntervalThree = TaskInterval(IntervalUnit.MINTUTES, 15)
    strIntervalThree = TaskIntervalEncoder().encode(IntervalThree)
    task3 = ScheduledTaskInfo(TaskCodeName = "DHT 22 Unten T", TaskNameDisplay = "DHT 22 Unten (Temperatur)", Interval = IntervalThree, ObjectID = SensorInfoProvider.GetSensorInfo(codeName="DHT22B_t").SensorID, ObjectType = "Sensor", LastRunTime = None, NextRunTime = TimezoneHelper.GetCurrentDatetime(), Executions = 0, DeleteAfterLastRun = False, Enabled = True, IsRunning = False)
    task3.save()

def ProjectSensor():
  PS1 = ProjectSensorInfo(Project = ProjectInfoProvider.GetProjectInfo(codeName = "test"), Sensor = SensorInfoProvider.GetSensorInfo(codeName="DHT22B_h"))
  PS1.save()

  PS2 = ProjectSensorInfo(Project = ProjectInfoProvider.GetProjectInfo(codeName = "test"), Sensor = SensorInfoProvider.GetSensorInfo(codeName="photowiderstand"))
  PS2.save()


def runSetup():
    #Users()
    #Projects()
    #Sensors()
    ScheduledTasks()
    #Relais()

    #Beziehungen
    #ProjectSensor()
    #pass 
