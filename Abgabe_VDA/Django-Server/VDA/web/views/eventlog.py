from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

from DBAccess._models.EventLog import *

def eventlog(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)
    EventMessage = ""

    if request.method == 'POST':
       pass


    if request.method == 'GET':
        eventlist = EventLogInfoProvider.GetEvents()
        EventID = request.GET.get('EventID') #Ist die EventID
        if EventID != None:
            result = EventLogInfoProvider.GetEventLogInfo(eventId = int(EventID))
            if type(result) is str:
                EventMessage = result #"Zuweisung konnte nicht gelöscht werden."
            else:
                result.delete()

    return render(
        request,
        'admin/eventlog.html',
        {
            'title':'Eventlog',
            'currentPage': 'eventlog',
            'eventlist': eventlist,
            'EventMessage': EventMessage,
        }
    )

