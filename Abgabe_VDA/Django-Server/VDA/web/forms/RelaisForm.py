from .BaseForm import BaseModelForm
from DBAccess._models.Relais import * 

# Create the form class.
class RelaisForm(BaseModelForm):
    class Meta:
        model = RelaisInfo 
        exclude = ['RelaisID']
