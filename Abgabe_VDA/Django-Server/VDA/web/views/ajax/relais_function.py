from django.http import Http404
from django.http import JsonResponse
import json
from django.views.decorators.csrf import csrf_exempt, csrf_protect

from RasPiAPI.RelaisController import *

@csrf_exempt #deaktiviert csrf Schutz
def relais_toggle(request):
    if request.is_ajax():
        body_unicode = request.body.decode('utf-8')
        body = json.loads(body_unicode)
        gpio = int(body['gpio'])
        RelaisID = str(body['RelaisID'])
    
        #debug
        strDebug = "off"
        if bool(body['debug']):
            strDebug = "on"

        #strDebug = None  #Online auskomentieren

        state = RelaisController.ToogleRelais(gpio = RelaisID,debug = strDebug)
    
        data = {
            'state': str(state),
            'RelaisID' : RelaisID
        }

        return JsonResponse(data)
    else:
        raise Http404

@csrf_exempt #deaktiviert csrf Schutz
def relais_state(request):
        
    data = {
        'is_taken': User.objects.filter(username__iexact=username).exists()
    }
    return JsonResponse(data)
    

