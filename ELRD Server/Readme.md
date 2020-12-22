### Backend für die ELRD App
Dieses Projekt bietet eine WebAPI für die ELRD App an. 
Es werden REST Endpunkte zur Verfügung gestellt um Daten zu übermitteln und abzurufen von Daten.

Daten werden in einer SQL Datenbank gepeichert. Dafür wird das ELRDDataAccessLibrary Projekt verwendet.

Alle APIs sind mit Swagger dokumentiert.

### Existierende Endpunkte

### User
##### [POST]
  - api/v1/users
    - Legt neuen Nutzer in der Datenbank an


#### [GET]  
  - api/v1/users
    - Liefert alle Nutzer der Datenbank zurück
  - api/v1/users/&#123;userID&#125;
    - Liefert einen Nutzer mit der &#123;userID&#125; zurück
    
#### [PUT]  
  - api/v1/users/&#123;userID&#125;
    - Updated den completten Datensatz für einen Nutzer mit der ID &#123;userID&#125; und dem Inhalt aus dem Body

#### [DELTE]
  - api/v1/users/&#123;userID&#125;
    - Löscht den Nutzer mit der &#123;userID&#125; aus der Datenbank


### Identity
##### [POST]
  - api/v1/identity/register/
    - registriert neuen Benutzer
    - Benutzername, Passwort, Email
  - api/v1/identity/login/
    - Login für username/passwort
    - Login für email/passwort
  - api/v1/identity/refresh/
    - Erneuert den token mit notwendigem refreshtoken

### BaseData
##### [GET]
  - api/v1/basedata/baseunits
    - Gibt alle Fahrzeuge der Stammdaten zurück
  - api/v1/basedata/seedbaseunit
    - Fügt ein paar vordefinierte Fahrzeuge in die Datenbank ein