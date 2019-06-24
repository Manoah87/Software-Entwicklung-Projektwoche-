from django.db import models
from django.utils.translation import ugettext_lazy as _

class RelaisInfo(models.Model):
    RelaisID = models.AutoField(primary_key=True)
    RelaisCodeName = models.CharField(_('Codename'), max_length=100, blank=False, default="", unique=True)
    RelaisDisplayName = models.CharField(_('Displayname'), max_length=100, blank=False, default="") 
    GPIO = models.IntegerField(_('GPIO'), default=0, blank=False)
    Description = models.TextField(_('Beschreibung'), default="")
    RelaisState = models.BooleanField(_('Aktiviert'), default = True)

    class Meta:
        db_table = "relais";

    def __str__(self):
       return '%s' % (self.RelaisDisplayName)

class RelaisInfoProvider():
    
    @staticmethod
    def GetRelaisInfo(relaisId = 0, codeName = ""):
        """
            Wenn eine RelaisID übergeben wird, wird der CodeName ignoriert. 
        """
        try:
            if relaisId > 0:
                return RelaisInfo.objects.get(pk = relaisId)
            else:
                if len(RelaisInfo.objects.filter(RelaisCodeName = codeName)) > 0:
                    return RelaisInfo.objects.filter(RelaisCodeName = codeName)[0]
                else:
                    #Errormeldungen als enum oder prüfen ob fehlermeldungen geworfen werden können
                    return "NoResult" 
        except:
            EventLogInfoProvider.LogException("Unexpected error:",sys.exc_info())
            return "Unbehandelte Ausnahme"

    @staticmethod
    def GetRelaiss(where: str = "", order: str = ""): 
       sql_cmd = "SELECT * FROM relais "
       if where != "":
           sql_cmd += "WHERE " + where + " "
       
       if order != "":
           sql_cmd += "ORDER BY " + order + " "
           
       sql_cmd += ";"
 
       try:
           #Muss geprüft werden, ob sicher, (Testkonzept)
          return RelaisInfo.objects.raw(sql_cmd)
       except mysql.connector.Error as err:
           return err

