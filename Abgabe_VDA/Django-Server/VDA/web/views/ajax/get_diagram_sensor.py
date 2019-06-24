from django.http import Http404
from django.http import JsonResponse
import json
from django.views.decorators.csrf import csrf_exempt, csrf_protect

from DBAccess._models.SensorValue import *
from DBAccess._models.Project import *

@csrf_exempt  #deaktiviert csrf Schutz
def get_diagram_sensor(request):
     
    if request.is_ajax():
        body_unicode = request.body.decode('utf-8')
        body = json.loads(body_unicode)
        values = SensorValueInfoProvider.GetSensorValues(where = "SensorID = " + body["SensorID"] + " AND ProjectID = " + str(ProjectInfoProvider.GetEnabledProject().ProjectID), order = "Timestamp ASC")

        valueDictionary = {}
        
        for value in values:
           valueDictionary[str(value.Timestamp)] = str(value.Value)

        return JsonResponse(valueDictionary)
    else:
        raise Http404

    


