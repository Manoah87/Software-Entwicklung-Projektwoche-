from django.utils import timezone as DjangoTimeZone
import pytz
from pytz import timezone as pytz_timezon
from datetime import datetime

class TimezoneHelper(object):
    """description of class"""
    

    @staticmethod
    def GetCurrentDatetime():
        zone ='Europe/Berlin'
        local_tz = pytz.timezone(zone)
        local_dt = datetime.utcnow().replace(tzinfo=pytz.utc).astimezone(local_tz)
        return local_dt

