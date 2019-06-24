from .BaseForm import BaseModelForm
from DBAccess._models.ScheduledTask import ScheduledTaskInfo
from django.utils.translation import ugettext_lazy as _
from django.db import models
from django import forms

# Create the form class.
class ScheduledTaskForm(BaseModelForm):
    #Username = forms.CharField(max_length=100,
    #                           label=_("Benutzername*"),
    #                           widget=forms.TextInput({}))

    class Meta:
        model = ScheduledTaskInfo
        exclude = ['ScheduledTaskID']
