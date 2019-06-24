#!/usr/bin/python3
#
# MySqlHelper.py
#   Sensor Module mit der Klasse SensorInfo und SensorInfoProvider
#
# 12.06.2018 / Michael Pfister
#

# Beginn des Programmes

import mysql.connector
from django.db import connection
from collections import namedtuple

class MySqlHelper():
    """
        Erzeugt die Verbindung zur DB für die einzelnen Klassen Instanzen
        Wird später aus Serverconfig ausgelesen.        
    """
    def __new__(cls, *args, **kwargs):
       if len(args) > 0 or len(kwargs) > 0:
         cls._instance = object.__new__(cls)
       else:
        cls._instance = object.__new__(cls, *args, **kwargs)

       cls._instance.Connection = connection
       cls._instance.cursor = cls._instance.Connection.cursor()
       return cls._instance #ohne Return Value funktioniert Instanzierung nicht mehr

    def __init__(self):
        print("mysql init");

    def namedtuplefetchall(self, cursor):
        "Return all rows from a cursor as a namedtuple"
        desc = cursor.description
        nt_result = namedtuple('Result', [col[0] for col in desc])
        return [nt_result(*row) for row in cursor.fetchall()]





            