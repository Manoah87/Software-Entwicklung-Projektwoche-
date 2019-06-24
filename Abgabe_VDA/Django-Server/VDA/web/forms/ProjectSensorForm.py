from .BaseForm import BaseModelForm
from DBAccess._models.ProjectSensor import ProjectSensorInfo
from DBAccess._models.Sensor import SensorInfo

class ProjectSensorForm(BaseModelForm):

    class Meta:
        model = ProjectSensorInfo
        exclude = ['ProjectSensorID', 'Project']
        fields = ['Sensor']
    