from django.apps import AppConfig
#from django.utils.translation import ugettext_lazy as _

class StartEvent(AppConfig):
    name = 'VDA.configuration.apps.StartEvent'
    verbose_name = "Start Event"
    def ready(self):
        print("test")