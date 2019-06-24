from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

#eigene imports
from DBAccess._models.Project import *
from web.forms.ProjectForm import *


def project_edit(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)

    ProjectID = request.GET.get('ProjectID')
    strFormMessage = "<p>&nbsp;</p>"
    strTitle = ""

    if request.method == 'POST':
        if ProjectID != None:
            project = ProjectInfoProvider.GetProjectInfo(projectId = int(ProjectID))
            form = ProjectForm(request.POST or None, instance = project)
            strTitle = 'Project bearbeiten'
        else:
            form = ProjectForm(request.POST or None)
            strTitle = 'Neues Project erfassen'

        if form.is_valid():
          if 'Enabled' in form.changed_data and form.instance.Enabled:
              CurrentEnabledProject = ProjectInfoProvider.GetEnabledProject() #Aktives Projekt deaktivieren
              CurrentEnabledProject.Enabled = False
              CurrentEnabledProject.save()

          form.save()
          strFormMessage = "<p class='success'>Formular Daten wurden gespeichert</p>"

    

    if request.method == 'GET':
        if ProjectID != None:
            project = ProjectInfoProvider.GetProjectInfo(projectId = int(ProjectID))
            form = ProjectForm(request.POST or None, instance = project)
            strTitle = 'Project bearbeiten'
        else:
            form = ProjectForm()
            strTitle = 'Neues Project erfassen'

    return render(
        request,
        'project/project_edit.html',
        {
            'currentPage': 'project',
            'title': strTitle,
            'form': form,
            'strFormMessage': strFormMessage,
            'year':datetime.now().year,
        }
    )
