from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

from DBAccess._models.Relais import *
from DBAccess._models.EventLog import *
from web.forms.RelaisForm import *
    

def relais_edit(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)

    RelaisID = request.GET.get('RelaisID')
    strFormMessage = "<p>&nbsp;</p>"
    strTitle = ""

    if request.method == 'POST':
        if RelaisID != None:
            relais = RelaisInfoProvider.GetRelaisInfo(relaisId = int(RelaisID))
            form = RelaisForm(request.POST or None, instance = relais)
            strTitle = 'Neues Relais erfassen'
        else:
            form = RelaisForm(request.POST or None)
            strTitle = 'Relais bearbeiten'

        if form.is_valid():
            if form.instance.RelaisID == None:
                EventLogInfoProvider.LogInformation(source = "Neues Relais wurde erfasst", description = "Relais " + form.instance.RelaisDisplayName + " wurde erstellt")
            else:
                EventLogInfoProvider.LogInformation(source = "Relais wurde aktualisiert", description = "Relais " + form.instance.RelaisDisplayName + " wurde aktualisiert")

            form.save()
            strFormMessage = "<p class='success'>Formular Daten wurden gespeichert</p>"


    if request.method == 'GET':
        if RelaisID != None:
            relais = RelaisInfoProvider.GetRelaisInfo(relaisId = int(RelaisID))
            form = RelaisForm(request.POST or None, instance = relais)
            strTitle = 'Relais bearbeiten'
        else:
            form = RelaisForm()
            strTitle = 'Neues Relais erfassen'



    return render(
        request,
        'relais/relais_edit.html',
        {
            'currentPage': 'relais',
            'title': strTitle,
            'form': form,
            'strFormMessage': strFormMessage,
            'year':datetime.now().year,
        }
    )