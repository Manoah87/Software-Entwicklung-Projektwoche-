from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

from web.forms.ScheduledTaskForm import *
from DBAccess._models.ScheduledTask import *

def scheduled_task_edit(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)

    TaskID = request.GET.get('TaskID');

    if request.method == 'POST':
        if TaskID != None:
            task = ScheduledTaskInfoProvider.GetScheduledTaskInfo(scheduledTaskId = int(TaskID))
            form = ScheduledTaskForm(request.POST or None, instance = task)
        else:
            form = ScheduledTaskForm(request.POST or None)

        if form.is_valid():
          form.save()

    strTitle = 'Neuer Sensoren erfassen'

    if request.method == 'GET':
        if TaskID != None:
            task = ScheduledTaskInfoProvider.GetScheduledTaskInfo(scheduledTaskId = int(TaskID))
            form = ScheduledTaskForm(request.POST or None, instance = task)
        else:
            form = ScheduledTaskForm()

    return render(
        request,
        'admin/scheduledtask/scheduledtasks_edit.html',
        {
            'title': strTitle,
            'form': form,
            'currentPage': 'scheduled',
        }
    )


