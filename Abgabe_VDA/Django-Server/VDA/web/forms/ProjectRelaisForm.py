from .BaseForm import BaseModelForm
from DBAccess._models.ProjectRelais import ProjectRelaisInfo
from DBAccess._models.Relais import RelaisInfo

class ProjectRelaisForm(BaseModelForm):
    """description of class"""
    class Meta:
        model = ProjectRelaisInfo
        exclude = ['ProjectRelaisID', 'Project']
        fields = ['Relais']
    


