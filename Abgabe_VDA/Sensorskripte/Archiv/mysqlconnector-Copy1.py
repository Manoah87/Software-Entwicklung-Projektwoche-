import mysql.connector


mydb = mysql.connector.connect(user='piconnection', 
                                  password='Eppendorf4!',
                                  host='192.168.0.11',# Da ich den Pi benutze ist das die MAC IP. Mitl alt + wlansymbol
                                  port= '3306',
                                  database="test",#Dies kann erst weggelassen werden und mit dem Befehl unten alle verfügbaren DB anzeigen.
                              
                             )
    