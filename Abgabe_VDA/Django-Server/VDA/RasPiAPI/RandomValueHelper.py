import random
from DBAccess.Singleton import Singleton

class RandomValueHelper(Singleton):
    """
        Kann verwendet werden, um Zufallswerte zu generieren
    """

    def GetRandomFloat(LowerBound : float, UpperBound : float):
        return random.randrange(LowerBound, UpperBound)

    def GetRandomInt(LowerBound : int, UpperBound : int):
        return random.randint(LowerBound,UpperBound)
