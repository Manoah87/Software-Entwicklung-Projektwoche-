import sys
from django.db import models
from django.utils.translation import ugettext_lazy as _
from DBAccess._models.Project import *
from DBAccess._models.Relais import RelaisInfo 

class ProjectRelaisInfo(models.Model):
    ProjectRelaisID = models.AutoField(primary_key=True)
    Project = models.ForeignKey(ProjectInfo, db_column='ProjectID', on_delete=models.CASCADE)
    Relais = models.ForeignKey(RelaisInfo, db_column='RelaisID', on_delete=models.CASCADE)

    class Meta:
        db_table = "project_relais";

class ProjectRelaisProvider():
    @staticmethod
    def GetProjectRelaisInfo(projectId : int, relaisId : int):
        try:
            project = ProjectInfo.objects.get(pk = projectId)
            relais = RelaisInfo.objects.get(pk = relaisId)
            return ProjectRelaisInfo.objects.filter(Project = project, Relais = relais);
        except:
            return str(sys.exc_info()[0])
    
    @staticmethod
    def GetProjectRelaisFromEnabledProject():
       sql_cmd = "SELECT * FROM project_relais "
       sql_cmd += "WHERE ProjectID = " + str(ProjectInfoProvider.GetEnabledProject().ProjectID) + "; "
 
       try:
           #Muss geprüft werden, ob sicher, (Testkonzept)
          return ProjectRelaisInfo.objects.raw(sql_cmd)
       except mysql.connector.Error as err:
           return err
