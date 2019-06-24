class RelaisController():

    # Schaltet das Relais um
    @staticmethod
    def ToogleRelais(gpio : int = -1, debug : str = None):
        if gpio == -1:
            return "Error GPIO von Relais nicht definiert"

        if debug == "on":
            return False
        else:
            return True

        