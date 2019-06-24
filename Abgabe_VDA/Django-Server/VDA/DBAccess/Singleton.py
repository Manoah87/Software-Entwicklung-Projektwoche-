class Singleton(object):
    """
        Ist eine Basisklasse welche sicherstellt, dass nur eine Instanz der Klasse erzeugt werden kann.
        Kann sein, dass die Klasse nicht Threassafe ist!!!
    """
    _instance = None
    def __new__(cls, *args, **kwargs):
        if not cls._instance:
            cls._instance = object.__new__(cls, *args, **kwargs)
        return cls._instance


