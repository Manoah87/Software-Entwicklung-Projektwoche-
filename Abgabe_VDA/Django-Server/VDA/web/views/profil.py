from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

from web.forms import ProfilForm

def profil(request):
    """Renders the about page."""
    assert isinstance(request, HttpRequest)

    # if this is a POST request we need to process the form data
    if request.method == 'POST':
        # create a form instance and populate it with data from the request:
        form = ProfilForm(request.POST)
        # check whether it's valid:
        if form.is_valid():
            # process the data in form.cleaned_data as required
            # Form abspeichern
            # redirect to a new URL:

            #try:
            #    time.sleep(10)
            #except Exception as ex:
            #    print(ex)

            return HttpResponseRedirect('/')

    # if a GET (or any other method) we'll create a blank form
    else:
        form = ProfilForm()

    return render(
        request,
        'web/profil.html',
        {
            'title':'Profil',
            'currentPage': 'profil',
            'message':'Hier kannst du dein Profil bearbeiten',
            'form':form,
        }
    )