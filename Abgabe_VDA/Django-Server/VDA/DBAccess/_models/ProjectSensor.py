import sys
from django.db import models
from django.utils.translation import ugettext_lazy as _
from DBAccess._models.Project import *
from DBAccess._models.Sensor import SensorInfo

class ProjectSensorInfo(models.Model):
    ProjectSensorID = models.AutoField(primary_key=True)
    Project = models.ForeignKey(ProjectInfo, db_column='ProjectID', on_delete=models.CASCADE)
    Sensor = models.ForeignKey(SensorInfo, db_column='SensorID', on_delete=models.CASCADE)
    
    class Meta:
        db_table = "project_sensor";

class ProjectSensorInfoProvider():
    @staticmethod
    def GetProjectSensorInfo(projectId : int, sensorId : int):
        try:
            project = ProjectInfo.objects.get(pk = projectId)
            sensor = SensorInfo.objects.get(pk = sensorId)
            return ProjectSensorInfo.objects.filter(Project = project, Sensor = sensor);
        except:
            return str(sys.exc_info()[0])
    
    @staticmethod
    def GetProjectSensorFromEnabledProject():
       sql_cmd = "SELECT * FROM project_sensor "
       sql_cmd += "WHERE ProjectID = " + str(ProjectInfoProvider.GetEnabledProject().ProjectID) + "; "
 
       try:
           #Muss geprüft werden, ob sicher, (Testkonzept)
          return ProjectSensorInfo.objects.raw(sql_cmd)
       except mysql.connector.Error as err:
           return err


