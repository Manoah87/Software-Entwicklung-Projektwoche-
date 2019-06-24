from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime
from web.core.UserHelper import *

from DBAccess._models.Sensor import *
from DBAccess._models.ProjectSensor import *

from RasPiAPI.DHT22 import DHT22
from RasPiAPI.PhotoResistor import PhotoResistor

def home(request):
    """Renders the home page."""
    assert isinstance(request, HttpRequest)
    
    UserHelper.AuthenticateUser(request)
    print(request.user.id)

    sensorShowOnHome = SensorInfoProvider.GetSensors(where = "ShowOnHome = 1") 
    for sensor in sensorShowOnHome:
        if sensor.SensorType == "resistor":
            sensor.SensorCustomData = PhotoResistor.GetValue(gpio = sensor.GPIO)
        
        if sensor.SensorType == "dht22_h":
            sensor.SensorCustomData = "{0:.2f}".format(DHT22.GetHumidity(gpio = sensor.GPIO))

        if sensor.SensorType == "dht22_t":
            sensor.SensorCustomData = "{0:.2f}".format(DHT22.GetTemperature(gpio = sensor.GPIO))

    sensorProjectList = ProjectSensorInfoProvider.GetProjectSensorFromEnabledProject()


    return render(
        request,
        'web/index.html',
        {
            'title':'Home Page',
            'year':datetime.now().year,
            'currentPage': 'home',
            'sensorlist' : sensorShowOnHome,
            'sensorProjectList' : sensorProjectList,
        }
    )
