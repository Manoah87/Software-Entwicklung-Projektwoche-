from django import forms
from django.utils.translation import ugettext_lazy as _

class BaseForm(forms.Form):
    """Kopiert von Web, nur mit label_suffix"""
    def __init__(self, *args, **kwargs):
        kwargs.setdefault('label_suffix', '')  
        super(BaseForm, self).__init__(*args, **kwargs)

class BaseModelForm(forms.ModelForm):
    """Kopiert von Web, nur mit label_suffix"""
    def __init__(self, *args, **kwargs):
        kwargs.setdefault('label_suffix', '')
        super(BaseModelForm, self).__init__(*args, **kwargs)