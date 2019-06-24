from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

#eigene imports
from DBAccess._models.Project import *


def project_list(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)
    FormMessage = "<p class='Error'></p>"
    if request.method == 'POST':
        pass

    if request.method == 'GET':
        ProjectID = request.GET.get('ProjectID')

        if ProjectID != None:
            result = ProjectInfoProvider.GetProjectInfo(projectId = int(ProjectID))
            if type(result) is str:
                FormMessage = "<p class='error'>" + result + "</p>" 
            else:
                result.delete()
                FormMessage = "<p class='success'>Projekt wurde gelöscht</p>"

        projectlist = ProjectInfoProvider.GetProjecs()

    return render(
        request,
        'project/project_list.html',
        {
            'currentPage': 'project',
            'title':'Projekte',
            'projectlist': projectlist,
            'FormMessage': FormMessage,
            'year':datetime.now().year,
        }
    )
