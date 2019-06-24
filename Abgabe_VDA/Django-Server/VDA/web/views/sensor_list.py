from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

#eigene imports
from DBAccess._models.Sensor import *
from DBAccess._models.EventLog import *
from DBAccess._models.Project import *
from DBAccess._models.ProjectSensor import *

from web.forms.ProjectSensorForm import *

def sensor_list(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)
   # type(i) is int
    
    FormMessage = ""
    RelationFormMessage = ""

    if request.method == 'POST':

        projectSensorForm = ProjectSensorForm(request.POST)
        if projectSensorForm.is_valid():
            projectSensorInfo = projectSensorForm.save(commit=False)
            projectSensorInfo.Project = ProjectInfoProvider.GetEnabledProject()
            projectSensorInfo.save()
            FormMessage = "<p class='success'>Sensor " + SensorInfoProvider.GetSensorInfo(sensorId = projectSensorInfo.Sensor.SensorID).SensorDisplayName + " wurde dem Projekt hinzugefügt</p>"
    else:
        RelationID = request.GET.get('RelationID') #Ist die SensorID
        if RelationID != None:
            result = ProjectSensorInfoProvider.GetProjectSensorInfo(projectId = ProjectInfoProvider.GetEnabledProject().ProjectID, sensorId = int(RelationID))
            if type(result) is str:
                RelationFormMessage = result #"Zuweisung konnte nicht gelöscht werden."
            else:
                result.delete()

        projectSensorForm = ProjectSensorForm(prefix='ProjectSensorForm')

        SensorID = request.GET.get('SensorID')
        if SensorID != None:
           sensor = SensorInfoProvider.GetSensorInfo(sensorId = int(SensorID))
           if type(sensor) is str:
               FormMessage = sensor #error message
           else:
               sensor.delete()

    return render(
        request,
        'sensor/sensor_list.html',
        {
            'currentPage': 'sensor',
            'title':'Sensoren',
            'message':'',
            'FormMessage': FormMessage,
            'RelationFormMessage': RelationFormMessage,
            #'sensor' : SensorInfoProvider.GetSensorInfo(sensorId = 0, codeName = "DHT22B"),
            'projectSensorForm' : projectSensorForm,
            'sensorlist' : SensorInfoProvider.GetSensors(),
            'projectsensorlist' : SensorInfoProvider.GetSensors(where = "SensorID In (SELECT SensorID FROM vda_db.project_sensor WHERE ProjectID = " + str(ProjectInfoProvider.GetEnabledProject().ProjectID) + ")"),
            'year':datetime.now().year,
        }
    )
