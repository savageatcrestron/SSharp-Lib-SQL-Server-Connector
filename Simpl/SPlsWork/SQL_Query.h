namespace SQL_Query;
        // class declarations
         class SQLConnectionClass;
     class SQLConnectionClass 
    {
        // class delegates

        // class events

        // class functions
        INTEGER_FUNCTION UserInfo ( STRING uname , STRING pword );
        STRING_FUNCTION GetData ( STRING serverIP , STRING portNum , STRING dbName , STRING nameColumn , STRING userName , STRING userPass , STRING nameToFind );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

