from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

from DBAccess._models.ScheduledTask import *

def scheduled_task_view(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)        

    if request.method == 'GET':
        taskID = request.GET.get('task_id')
        if taskID != None:
           task = ScheduledTaskInfoProvider.GetScheduledTaskInfo(scheduledTaskId = int(taskID))
           if type(task) is str:
               FormMessage = task #error message
           else:
               task.delete()

    return render(
        request,
        'admin/scheduledtask/scheduledtasks.html',
        {
            'title':'Scheduled Task',
            'currentPage': 'scheduled',
            'tasklist': ScheduledTaskInfoProvider.GetScheduledTasks()
        }
    )


