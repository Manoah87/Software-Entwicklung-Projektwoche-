from django.shortcuts import render
from django.http import HttpRequest
from django.template import RequestContext
from datetime import datetime

def contact(request):
    """Renders the contact page."""
    assert isinstance(request, HttpRequest)
    return render(
        request,
        'web/contact.html',
        {
            'title':'Contact',
            'message':'Your contact page.',
            'year':datetime.now().year,
        }
    )
