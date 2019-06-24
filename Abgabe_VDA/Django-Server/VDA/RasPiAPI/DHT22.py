from .RandomValueHelper import *

class DHT22(object):
    @staticmethod
    def GetHumidity(gpio : int = -1):
        if gpio <= 0:
            return "GPIO port kann nicht kleiner gleich 0 sein"
        else:
            return RandomValueHelper.GetRandomFloat(30.0, 50.0)
    
    @staticmethod
    def GetTemperature(gpio : int = -1):
        if gpio <= 0:
            return "GPIO port kann nicht kleiner gleich 0 sein"
        else:
            return RandomValueHelper.GetRandomFloat(15.0, 26.0)


