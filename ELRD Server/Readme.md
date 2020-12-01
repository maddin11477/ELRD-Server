### Backend für die ELRD App
Dieses Projekt bietet eine WebAPI für die ELRD App an. 
Es werden REST Endpunkte zur Verfügung gestellt um Daten zu übermitteln und abzurufen von Daten.

Daten werden in einer SQL Datenbank gepeichert. Dafür wird das ELRDDataAccessLibrary Projekt verwendet.

Alle APIs sind mit Swagger dokumentiert.

### Existierende Endpunkte

### User
##### [POST]
  - api/User/Login 
    - Meldet den Benutzer an
    - Benötigt username und password
    - Liefert ein JWT Token zurück

#### [GET]  
  - api/User
    - Liefert alle Nutzer der Datenbank zurück
