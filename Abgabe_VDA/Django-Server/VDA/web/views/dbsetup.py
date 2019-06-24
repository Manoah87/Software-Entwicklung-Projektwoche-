from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

#eigene Import
from DBAccess.DB_Setup import runSetup

def dbsetup(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)

    if request.method == 'POST':
       if 'InitDB' in request.POST:
           runSetup()

    return render(
        request,
        'admin/dbsetup.html',
        {
            'title':'DB-Setup',
        }
    )
