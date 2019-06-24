from django.http import Http404
from django.http import JsonResponse
import json
from django.views.decorators.csrf import csrf_exempt, csrf_protect

from DBAccess._models.EventLog import *

@csrf_exempt #deaktiviert csrf Schutz
def get_eventlog_detail(request):
     
    if request.is_ajax():
        body_unicode = request.body.decode('utf-8')
        body = json.loads(body_unicode)
        event = EventLogInfoProvider.GetEventLogInfo(eventId = int(body["EventID"])) 

        result = {}
        
        result['Event ID'] = str(event.EventID)
        result['Eventtpy'] = str(event.EventType)
        result['EventTime'] = str(event.EventTime)
        result['Source'] = str(event.Source)
        result['EventDescription'] = str(event.EventDescription)
        result['EventUrl'] = str(event.EventUrl)
        result['EventMachineName'] = str(event.EventMachineName)
        result['EventUserAgent'] = str(event.EventUserAgent)
        result['EventUrlReferrer'] = str(event.EventUrlReferrer)
        try:
            result['Benutzername'] = str(event.UserInfo.get_username())
        except:
            result['Benutzername'] = 'None' 

        return JsonResponse(result)
    else:
        raise Http404

    




