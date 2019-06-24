"""
Definition of urls for VDA.
"""

from datetime import datetime
from django.conf.urls import url
import django.contrib.auth.views
from django.contrib.auth import views as auth_views #add latter

import web.forms
import web.views


# Uncomment the next lines to enable the admin:
# from django.conf.urls import include
# from django.contrib import admin
# admin.autodiscover()

urlpatterns = [
    # Examples:
    url(r'^$', web.views.home, name='home'),
    url(r'^contact$', web.views.contact, name='contact'),
    url(r'^profil$', web.views.profil, name='profil'),
    url(r'^projectlist$', web.views.project_list, name='project_list'),
    url(r'^projectedit$', web.views.project_edit, name='project_edit'),
    url(r'^relaislist$', web.views.relais_list, name='relaislist'),
    url(r'^relaisedit$', web.views.relais_edit, name='relaisedit'),
    url(r'^sensorlist$', web.views.sensor_list, name='sensor_list'),
    url(r'^sensoredit$', web.views.sensor_edit, name='sensor_edit'),
    url(r'^eventlog$', web.views.eventlog, name='eventlog'),
    url(r'^setup$', web.views.dbsetup, name='setup'),
    url(r'^scheduledtasks$', web.views.scheduled_task_view, name='scheduledtasks'),
    url(r'^scheduledtasksedit$', web.views.scheduled_task_edit, name='scheduledtasksedit'),
    url(r'^get_diagram_sensor$', web.views.get_diagram_sensor, name='get_diagram_sensor'),
    url(r'^get_eventlog_detail$', web.views.get_eventlog_detail, name='get_eventlog_detail'), 
    url(r'^relais_toggle$', web.views.relais_toggle, name='relais_toggle'),

    url(r'^login/$',
    auth_views.auth_login, #changed
    {
        'template_name': 'app/login.html',
        'authentication_form': web.forms.BootstrapAuthenticationForm,
        'extra_context':
        {
            'title': 'Log in',
            'year': datetime.now().year,
        }
    },
    name='login'),
    url(r'^logout$',
        auth_views.auth_logout, #changed
        {
            'next_page': '/',
        },
        name='logout'),
    url(r'^profil', web.views.profil, name='profil'),

    # Uncomment the admin/doc line below to enable admin documentation:
    # url(r'^admin/doc/', include('django.contrib.admindocs.urls')),

    # Uncomment the next line to enable the admin:
    # url(r'^admin/', include(admin.site.urls)),
]
