from .BaseForm import BaseModelForm
from DBAccess._models.Sensor import SensorInfo


# Create the form class.
class SensorForm(BaseModelForm):
    class Meta:
        model = SensorInfo
        exclude = ['SensorID', 'SensorCustomData']
        #fields = ['pub_date', 'headline', 'content', 'reporter']
