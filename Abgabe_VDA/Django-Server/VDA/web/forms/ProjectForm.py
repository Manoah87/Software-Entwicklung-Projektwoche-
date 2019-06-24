from .BaseForm import BaseModelForm
from DBAccess._models.Project import ProjectInfo


# Create the form class.
class ProjectForm(BaseModelForm):
    class Meta:
        model = ProjectInfo
        exclude = ['ProjectID']
        #fields = ['pub_date', 'headline', 'content', 'reporter']


