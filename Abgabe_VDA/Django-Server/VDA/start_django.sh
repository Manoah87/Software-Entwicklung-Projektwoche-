#!/bin/bash
#
# Add_Django_to_Autostart.py
# (Nicht notwendig, würde immer ausgeführt werden, wenn sich ein Benutzer anmeldet)
#
# 28.12.2018 / Michael Pfister
#

# Beginn des Programmes

#python /home/pi/manage.py runserver . &

nohup python3 /home/pi/Dango/VDA/SchedulerAPI/MainSensorReader.py &