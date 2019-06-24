from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

#eigene imports
from DBAccess._models.Sensor import *
from web.forms.SensorForm import *

def sensor_edit(request):
    assert isinstance(request, HttpRequest)
    
    SensorID = request.GET.get('SensorID')

    if request.method == 'POST':
        if SensorID != None:
            sensor = SensorInfoProvider.GetSensorInfo(sensorId = int(SensorID))
            form = SensorForm(request.POST or None, instance = sensor)
        else:
            form = SensorForm(request.POST or None)

        if form.is_valid():
          form.save()

    strTitle = 'Neuer Sensoren erfassen'

    if request.method == 'GET':
        if SensorID != None:
            sensor = SensorInfoProvider.GetSensorInfo(sensorId = int(SensorID))
            form = SensorForm(request.POST or None, instance = sensor)
        else:
            form = SensorForm()

    return render(
        request,
        'sensor/sensor_edit.html',
        {
            'title': strTitle,
            'form': form,
            'currentPage': 'sensor'
        }
    )
