import sys
from django.db import models
import mysql.connector
from django.utils.translation import ugettext_lazy as _
from DBAccess._models.EventLog import EventLogInfoProvider

class ProjectInfo(models.Model):
    ProjectID = models.AutoField(_('ProjectID'), primary_key=True)
    ProjectCodeName = models.CharField(_('Codename'), max_length=100, blank=False, default="",unique=True)
    ProjectDisplayName = models.CharField(_('Anzeigename'), max_length=100, blank=False, default="")   
    StartDate = models.DateField(_('Startdatum'))
    EndDate = models.DateField(_('Enddatum'))
    Enabled = models.BooleanField(_('Aktiv'), default=False)

    class Meta:
        db_table = "Project"

class ProjectInfoProvider():
    """
        Beschreibung ...
    """

    @staticmethod
    def GetEnabledProject():
        return ProjectInfo.objects.filter(Enabled = True)[0]

    @staticmethod
    def GetProjectInfo(projectId=0, codeName=""):
        """
            Wenn eine ProjecID übergeben wird, wird der CodeName ignoriert. 
        """
        try:
            if projectId > 0:
                return ProjectInfo.objects.get(pk = projectId)
            else:
                if len(ProjectInfo.objects.filter(ProjectCodeName = codeName)) > 0:
                    return ProjectInfo.objects.filter(ProjectCodeName = codeName)[0]
                else:
                    #Errormeldungen als enum oder prüfen ob fehlermeldungen
                    #geworfen werden können
                    return "NoResult" 
        except:
            EventLogInfoProvider.LogException("Unexpected error:",sys.exc_info())
            return "Unbehandelte Ausnahme"


    @staticmethod
    def GetProjecs(where: str="", order: str=""): 
       sql_cmd = "SELECT * FROM Project "
       if where != "":
           sql_cmd += "WHERE " + where + " "
       
       if order != "":
           sql_cmd += "ORDER BY " + order + " "
           
       sql_cmd += ";"
 
       try:
           #Muss geprüft werden, ob sicher, (Testkonzept)
          return ProjectInfo.objects.raw(sql_cmd)
       except mysql.connector.Error as err:
           return err
