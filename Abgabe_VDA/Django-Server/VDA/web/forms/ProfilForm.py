from django import forms
from django.utils.translation import ugettext_lazy as _
from .BaseForm import *

class ProfilForm(BaseForm):
    """max_length und so müssen noch ein DB angepasst werden! """

    Username = forms.CharField(max_length=100,
                               label=_("Benutzername*"),
                               widget=forms.TextInput({}))
   
    SALUTATION_CHOICES = (
        ("Frau", _("Frau")),
        ("Mann", _("Mann"))
    )

    Salutation = forms.ChoiceField(
            choices=SALUTATION_CHOICES,
            label=_("Anrede"),
            required=False,
            widget=forms.RadioSelect()
    )

    Firstname = forms.CharField(max_length=254,
                                label=_("Vorname"),
                                required=False,
                               widget=forms.TextInput({}))

    Lastname = forms.CharField(max_length=254,
                               label=_("Nachname"),
                               required=False,
                               widget=forms.TextInput({}))

    Mail = forms.CharField(max_length=254,
                           label=_("E-Mail*"),
                           widget=forms.EmailInput({}))

    Birthdate = forms.CharField(max_length=254,
                                label=_("Geburtstag"),
                                required=False,
                                widget=forms.DateInput({}))

    STATUS_CHOICES = (
        (0, _("keines")),
        (1, _("Editor")),
        (2, _("Administrator"))
    )

    Userprivilegelevel = forms.ChoiceField(
            choices=STATUS_CHOICES,
            required=False,
            label=_("Privileg Level"),
            widget=forms.Select()
    )

    UserEnabled = forms.BooleanField(
                    label=_("Benutzer aktiviert"),
                    widget=forms.CheckboxInput({})
                 )