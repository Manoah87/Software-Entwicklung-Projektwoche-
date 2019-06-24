#from django.core.management import setup_environ (deprecaed)
##from django.core.management import   # noqa
#call_command(settings)

import datetime as dt
import json, time, os, sys
from datetime import timedelta

dirPath = os.path.dirname(os.path.realpath(__file__))
dirVDAapp = os.path.abspath("VDA")
dirVDA = os.path.abspath(os.path.join(dirVDAapp, os.pardir))

sys.path.append(dirVDAapp)
sys.path.append(dirVDA)

import django
from django.conf import settings

os.environ.setdefault("DJANGO_SETTINGS_MODULE", "VDA.settings")

from django.core.wsgi import get_wsgi_application
application = get_wsgi_application()

django.setup()


from DBAccess.Singleton import Singleton
from DBAccess._models.ScheduledTask import *
from SchedulerAPI.MainSensorReader import MainSensorReader
from SchedulerAPI.TimezoneHelper import TimezoneHelper  

class SchedulerEngin(Singleton):
    """description of class""" 

    def __init__(self):
        if hasattr(self, 'initialized') == False:
            self.initialized = True
            #self.log = open('inverval_log_2.txt', 'a+') #File wird im Root-Verzeichnis des Projektes erstellt
            #self.log.write("Start: " + "\r\n")
            self.running = True

    def as_interval(self, dct):
        return TaskInterval(dct['IntervalUnit'], dct['Amount'])

    def InitEngin(self):
        tasks = ScheduledTaskInfoProvider.GetScheduledTasks(where = "Enabled = 1")
      #  currentDate = TimezoneHelper.GetCurrentDatetime()
      #  currentDate = TimezoneHelper.GetCurrentDatetime()
      #  print(currentDate)

        for task in tasks:
            task.IsRunning = False
            interval = json.loads(task.Interval,  object_hook = self.as_interval)
            if interval.IntervalUnit == IntervalUnit.HOUR:
                pass
                #DelayedStartTime += timedelta(hours = interval.Amount)

            task.save()

    def GetElapsedTime(self):
        DelayedStartTime = TimezoneHelper.GetCurrentDatetime()
        DelayedStartTime += timedelta(minutes = 1)
        DelayedStartTime -= timedelta(seconds = DelayedStartTime.second)
        elapsedTime = DelayedStartTime - TimezoneHelper.GetCurrentDatetime()
        return elapsedTime.total_seconds()

    

    def Run(self): # muss in async thread umgewandelt werden
        while self.running:
            print(self.GetElapsedTime())
            time.sleep(self.GetElapsedTime())

            TaskList = ScheduledTaskInfoProvider.GetTaskToRun()
            DateTimeNow = TimezoneHelper.GetCurrentDatetime()
            print(DateTimeNow)

          #  self.log.write(str(DateTimeNow) + "\r\n")
  
            #os.system("execute file");
            for task in TaskList:
                interval = json.loads(task.Interval,  object_hook = self.as_interval)
                task.LastRunTime = DateTimeNow;
                
                if interval.IntervalUnit == IntervalUnit.MONTH:
                    task.NextRunTime = DateTimeNow + timedelta(days= interval.Amount * 30)

                if interval.IntervalUnit == IntervalUnit.DAY:
                    task.NextRunTime = DateTimeNow + timedelta(days = interval.Amount)

                if interval.IntervalUnit == IntervalUnit.HOUR:
                    task.NextRunTime = DateTimeNow + timedelta(hours = interval.Amount)

                if interval.IntervalUnit == IntervalUnit.MINTUTES:
                    task.NextRunTime = DateTimeNow + timedelta(minutes = interval.Amount)

                if task.ObjectType == "Sensor":
                   MainSensorReader.WriteSensorValueToDB(sensorId=task.ObjectID)

                task.Executions += 1
                task.save()
           

            f = open(dirPath + os.sep + 'endprog.txt', 'r')
            
            textLines = f.readlines()
            if textLines.__len__() > 0 and textLines[0].find("end") != -1:
                f.close()
                self.StopLoop()
            else:
                f.close()


    def StopLoop(self):
        self.running = False;

        #log.write(">> END << ")
        #if log.closed == False:
        #    log.close()


Reader = SchedulerEngin()
Reader.InitEngin()
Reader.Run()