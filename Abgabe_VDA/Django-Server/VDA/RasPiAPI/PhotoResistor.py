from .RandomValueHelper import *

class PhotoResistor(object):
    """description of class"""
    @staticmethod
    def GetValue(gpio : int = -1):
        if gpio <= 0:
            return "GPIO port kann nicht kleiner gleich 0 sein"
        else:
            return RandomValueHelper.GetRandomInt(20, 2000)

