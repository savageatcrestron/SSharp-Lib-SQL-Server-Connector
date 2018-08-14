# SSharp-Lib-SQL-Server-Connector


Project created as a proof of concept;

So it is possible to connect through Active Directory to a SQL database. I have a test setup at my desk with an MC3 processor and 2 virtual machines.
- Windows Server 2012 running with Active Directory installed with it's own domain.
- Windows 10 Client with SQL Express v14.0.1 and a single testing database.
- SQL Browser service is activated.
- Windows Firewall is currently turned off on the Client, but exceptions could be added to allow the outside connection with Firewall enabled.
- The testing database is a simple 2 column 5 row list of names and phone numbers.
- The AD has the Client and a User in the same Security Group (for testing I used Universal scope).
- The User is able to log into the domain on the Client computer (this is the user that installed the SQL Server on the Client)
- The MC3 processor is logged into the domain with the same domain User that I am using with the Client computer.
- My SIMPL# library uses System.Data.SqlClient to establish the connection to the SQL server.
- I need to provide all of the applicable information to the module to get connected and retrieve data from the SQL server
  - Server IP address
  - Server SQL Service Port Number
  - Database Name
  - 'Names' column header
  - Domain Username "dummyforest\\davew"
  - Domain User Password
  - What name I am looking for in the directory
 
The SIMPL# module uses basic SQL query syntax to search the database.
Currently;
string SQLQuery = System.String.Format("SELECT * FROM {0} WHERE {1} LIKE '%{2}%';", dbName, nameColumn, nameToFind)
 
In order to make this work you will need to know the following;
- Database schema
- Expected search requirements (format of the query)
- Details of Active Directory being used (no LDAP support currently)
- Active Directory Credentials
- Server IP address
- Server Port number
 
There would also be a requirement to deal with the parsing of the returned information. This will entirely depend on the database schema and how you need the information displayed on the touchpanel. My test setup is functional, but by no means does it cover off all of the possibilities.