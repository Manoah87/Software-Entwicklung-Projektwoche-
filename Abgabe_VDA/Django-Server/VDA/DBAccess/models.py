from django.db import models

from django.utils import timezone
from django.utils.http import urlquote
from django.utils.translation import ugettext_lazy as _
from django.core.mail import send_mail
from django.contrib.auth.models import AbstractBaseUser, PermissionsMixin


# Create your models here.
from DBAccess._models.CustomUser import UserInfo
from DBAccess._models.Project import ProjectInfo
from DBAccess._models.Relais import RelaisInfo
from DBAccess._models.ProjectRelais import ProjectRelaisInfo
from DBAccess._models.Sensor import SensorInfo
from DBAccess._models.ProjectSensor import ProjectSensorInfo
from DBAccess._models.SensorValue import SensorValueInfo
from DBAccess._models.EventLog import EventLogInfo
from DBAccess._models.ScheduledTask import ScheduledTaskInfo