from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime
    
from DBAccess._models.Relais import *
from DBAccess._models.Project import *
from DBAccess._models.ProjectRelais import *
from web.forms.ProjectRelaisForm import *

def relais_list(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)
    FormMessage = ""
    RelationFormMessage = ""

    # if this is a POST request we need to process the form data
    if request.method == 'POST':
        RelationForm = ProjectRelaisForm(request.POST)
        if RelationForm.is_valid():
            projectRelais = RelationForm.save(commit=False)
            projectRelais.Project = ProjectInfoProvider.GetEnabledProject()
            projectRelais.save()
            FormMessage = "<p class='success'>Relais " + RelaisInfoProvider.GetRelaisInfo(relaisId = projectRelais.Relais.RelaisID).RelaisDisplayName + " wurde dem Projekt hinzugefügt</p>"


    if request.method == 'GET':
        RelationForm = ProjectRelaisForm(prefix='ProjectRelaisForm')

        RelationID = request.GET.get('RelationID') #Ist die RelaisID
        if RelationID != None:
            result = ProjectRelaisProvider.GetProjectRelaisInfo(projectId = ProjectInfoProvider.GetEnabledProject().ProjectID, relaisId = int(RelationID))
            if type(result) is str:
                RelationFormMessage = result #"Zuweisung konnte nicht gelöscht werden."
            else:
                result.delete()

        RelationForm = ProjectRelaisForm(prefix='ProjectRelaisForm')

        RelaisID = request.GET.get('RelaisID')
        if RelaisID != None:
           relais = RelaisInfoProvider.GetRelaisInfo(relaisId = int(RelaisID))
           if type(relais) is str:
               FormMessage = relais #error message
           else:
               relais.delete()


    relatedRelaisList = RelaisInfoProvider.GetRelaiss(where = "RelaisID In (SELECT RelaisID FROM vda_db.project_relais WHERE ProjectID = " + str(ProjectInfoProvider.GetEnabledProject().ProjectID) + ")", order = "RelaisDisplayName")
    relaislist = RelaisInfoProvider.GetRelaiss(order = "RelaisDisplayName")

    return render(
        request,
        'relais/relais_list.html',
        {
            'title':'Relais Verwaltung',
            'currentPage': 'relais',
            'relatedRelaisList': relatedRelaisList,
            'relaislist': relaislist,
            'RelationForm' : RelationForm,
        }
    )