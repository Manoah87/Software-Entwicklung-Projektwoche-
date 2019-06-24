#!/bin/bash
#
# Add_Django_to_Autostart.py
# (Nicht notwendig, würde immer ausgeführt werden, wenn sich ein Benutzer anmeldet)
#
# 28.12.2018 / Michael Pfister
#

# Beginn des Programmes

autostart_dir = "/etc/xdg/autostart/" 
dateiname="$autostart_dir VDA-Django.desktop"
programmName = "VDA-Django"
cmd = "/home/pi/Dango/VDA/SchedulerAPI/MainSensorReader.py"

cat >$dateiname << EOF
[Desktop Entry]
Type=Application
Name=$programmName
Exec=
EOF