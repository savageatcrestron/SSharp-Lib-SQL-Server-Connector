using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;
using SQL_Query;

namespace UserModule_SQL_SERVER_CONNECTOR
{
    public class UserModuleClass_SQL_SERVER_CONNECTOR : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput STARTSEARCH;
        Crestron.Logos.SplusObjects.DigitalInput CONFIRMUSER;
        Crestron.Logos.SplusObjects.StringInput NAMETOQUERY;
        Crestron.Logos.SplusObjects.StringInput CHECKNAME;
        Crestron.Logos.SplusObjects.StringInput CHECKPASS;
        Crestron.Logos.SplusObjects.DigitalOutput USERISCONFIRMED;
        Crestron.Logos.SplusObjects.StringOutput PHONENUMBER;
        StringParameter SERVERLOCATION;
        StringParameter SERVERPORT;
        StringParameter SQL_DATABASE_NAME;
        StringParameter SQL_NAME_COLUMN;
        StringParameter SQL_USER_NAME;
        StringParameter SQL_USER_PASSWORD;
        SQL_Query.SQLConnectionClass MYSQLCONN;
        object STARTSEARCH_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                CrestronString SERVERIP;
                CrestronString PORTNUM;
                CrestronString DBNAME;
                CrestronString NAMECOLUMN;
                CrestronString USERNAME;
                CrestronString USERPASS;
                CrestronString NAMETOFIND;
                CrestronString GOTBACK;
                SERVERIP  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 20, this );
                PORTNUM  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 6, this );
                DBNAME  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 30, this );
                NAMECOLUMN  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 30, this );
                USERNAME  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 80, this );
                USERPASS  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 40, this );
                NAMETOFIND  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
                GOTBACK  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 1000, this );
                
                
                __context__.SourceCodeLine = 25;
                NAMETOFIND  .UpdateValue ( NAMETOQUERY  ) ; 
                __context__.SourceCodeLine = 26;
                SERVERIP  .UpdateValue ( SERVERLOCATION  ) ; 
                __context__.SourceCodeLine = 27;
                PORTNUM  .UpdateValue ( SERVERPORT  ) ; 
                __context__.SourceCodeLine = 28;
                DBNAME  .UpdateValue ( SQL_DATABASE_NAME  ) ; 
                __context__.SourceCodeLine = 29;
                NAMECOLUMN  .UpdateValue ( SQL_NAME_COLUMN  ) ; 
                __context__.SourceCodeLine = 30;
                USERNAME  .UpdateValue ( SQL_USER_NAME  ) ; 
                __context__.SourceCodeLine = 31;
                USERPASS  .UpdateValue ( SQL_USER_PASSWORD  ) ; 
                __context__.SourceCodeLine = 33;
                try 
                    { 
                    __context__.SourceCodeLine = 35;
                    GOTBACK  .UpdateValue ( MYSQLCONN . GetData (  SERVERIP  .ToString() ,  PORTNUM  .ToString() ,  DBNAME  .ToString() ,  NAMECOLUMN  .ToString() ,  USERNAME  .ToString() ,  USERPASS  .ToString() ,  NAMETOFIND  .ToString() )  ) ; 
                    __context__.SourceCodeLine = 36;
                    Trace( "Returned Data = {0}", GOTBACK ) ; 
                    } 
                
                catch (Exception __splus_exception__)
                    { 
                    SimplPlusException __splus_exceptionobj__ = new SimplPlusException(__splus_exception__, this );
                    
                    __context__.SourceCodeLine = 40;
                    Trace( "something broke") ; 
                    
                    }
                    
                    
                    
                }
                catch(Exception e) { ObjectCatchHandler(e); }
                finally { ObjectFinallyHandler( __SignalEventArg__ ); }
                return this;
                
            }
            
        object CONFIRMUSER_OnPush_1 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                ushort RESULT = 0;
                
                CrestronString UNAME;
                CrestronString PWORD;
                UNAME  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
                PWORD  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
                
                
                __context__.SourceCodeLine = 49;
                UNAME  .UpdateValue ( CHECKNAME  ) ; 
                __context__.SourceCodeLine = 50;
                PWORD  .UpdateValue ( CHECKPASS  ) ; 
                __context__.SourceCodeLine = 52;
                RESULT = (ushort) ( MYSQLCONN.UserInfo( UNAME .ToString() , PWORD .ToString() ) ) ; 
                __context__.SourceCodeLine = 53;
                Trace( "Result is {0:d}", (short)RESULT) ; 
                __context__.SourceCodeLine = 55;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (RESULT == 1))  ) ) 
                    {
                    __context__.SourceCodeLine = 56;
                    Functions.Pulse ( 500, USERISCONFIRMED ) ; 
                    }
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    
    public override void LogosSplusInitialize()
    {
        SocketInfo __socketinfo__ = new SocketInfo( 1, this );
        InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
        _SplusNVRAM = new SplusNVRAM( this );
        
        STARTSEARCH = new Crestron.Logos.SplusObjects.DigitalInput( STARTSEARCH__DigitalInput__, this );
        m_DigitalInputList.Add( STARTSEARCH__DigitalInput__, STARTSEARCH );
        
        CONFIRMUSER = new Crestron.Logos.SplusObjects.DigitalInput( CONFIRMUSER__DigitalInput__, this );
        m_DigitalInputList.Add( CONFIRMUSER__DigitalInput__, CONFIRMUSER );
        
        USERISCONFIRMED = new Crestron.Logos.SplusObjects.DigitalOutput( USERISCONFIRMED__DigitalOutput__, this );
        m_DigitalOutputList.Add( USERISCONFIRMED__DigitalOutput__, USERISCONFIRMED );
        
        NAMETOQUERY = new Crestron.Logos.SplusObjects.StringInput( NAMETOQUERY__AnalogSerialInput__, 30, this );
        m_StringInputList.Add( NAMETOQUERY__AnalogSerialInput__, NAMETOQUERY );
        
        CHECKNAME = new Crestron.Logos.SplusObjects.StringInput( CHECKNAME__AnalogSerialInput__, 50, this );
        m_StringInputList.Add( CHECKNAME__AnalogSerialInput__, CHECKNAME );
        
        CHECKPASS = new Crestron.Logos.SplusObjects.StringInput( CHECKPASS__AnalogSerialInput__, 50, this );
        m_StringInputList.Add( CHECKPASS__AnalogSerialInput__, CHECKPASS );
        
        PHONENUMBER = new Crestron.Logos.SplusObjects.StringOutput( PHONENUMBER__AnalogSerialOutput__, this );
        m_StringOutputList.Add( PHONENUMBER__AnalogSerialOutput__, PHONENUMBER );
        
        SERVERLOCATION = new StringParameter( SERVERLOCATION__Parameter__, this );
        m_ParameterList.Add( SERVERLOCATION__Parameter__, SERVERLOCATION );
        
        SERVERPORT = new StringParameter( SERVERPORT__Parameter__, this );
        m_ParameterList.Add( SERVERPORT__Parameter__, SERVERPORT );
        
        SQL_DATABASE_NAME = new StringParameter( SQL_DATABASE_NAME__Parameter__, this );
        m_ParameterList.Add( SQL_DATABASE_NAME__Parameter__, SQL_DATABASE_NAME );
        
        SQL_NAME_COLUMN = new StringParameter( SQL_NAME_COLUMN__Parameter__, this );
        m_ParameterList.Add( SQL_NAME_COLUMN__Parameter__, SQL_NAME_COLUMN );
        
        SQL_USER_NAME = new StringParameter( SQL_USER_NAME__Parameter__, this );
        m_ParameterList.Add( SQL_USER_NAME__Parameter__, SQL_USER_NAME );
        
        SQL_USER_PASSWORD = new StringParameter( SQL_USER_PASSWORD__Parameter__, this );
        m_ParameterList.Add( SQL_USER_PASSWORD__Parameter__, SQL_USER_PASSWORD );
        
        
        STARTSEARCH.OnDigitalPush.Add( new InputChangeHandlerWrapper( STARTSEARCH_OnPush_0, false ) );
        CONFIRMUSER.OnDigitalPush.Add( new InputChangeHandlerWrapper( CONFIRMUSER_OnPush_1, false ) );
        
        _SplusNVRAM.PopulateCustomAttributeList( true );
        
        NVRAM = _SplusNVRAM;
        
    }
    
    public override void LogosSimplSharpInitialize()
    {
        MYSQLCONN  = new SQL_Query.SQLConnectionClass();
        
        
    }
    
    public UserModuleClass_SQL_SERVER_CONNECTOR ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}
    
    
    
    
    const uint STARTSEARCH__DigitalInput__ = 0;
    const uint CONFIRMUSER__DigitalInput__ = 1;
    const uint NAMETOQUERY__AnalogSerialInput__ = 0;
    const uint CHECKNAME__AnalogSerialInput__ = 1;
    const uint CHECKPASS__AnalogSerialInput__ = 2;
    const uint USERISCONFIRMED__DigitalOutput__ = 0;
    const uint PHONENUMBER__AnalogSerialOutput__ = 0;
    const uint SERVERLOCATION__Parameter__ = 10;
    const uint SERVERPORT__Parameter__ = 11;
    const uint SQL_DATABASE_NAME__Parameter__ = 12;
    const uint SQL_NAME_COLUMN__Parameter__ = 13;
    const uint SQL_USER_NAME__Parameter__ = 14;
    const uint SQL_USER_PASSWORD__Parameter__ = 15;
    
    [SplusStructAttribute(-1, true, false)]
    public class SplusNVRAM : SplusStructureBase
    {
    
        public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
        
        
    }
    
    SplusNVRAM _SplusNVRAM = null;
    
    public class __CEvent__ : CEvent
    {
        public __CEvent__() {}
        public void Close() { base.Close(); }
        public int Reset() { return base.Reset() ? 1 : 0; }
        public int Set() { return base.Set() ? 1 : 0; }
        public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
    }
    public class __CMutex__ : CMutex
    {
        public __CMutex__() {}
        public void Close() { base.Close(); }
        public void ReleaseMutex() { base.ReleaseMutex(); }
        public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
    }
     public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
