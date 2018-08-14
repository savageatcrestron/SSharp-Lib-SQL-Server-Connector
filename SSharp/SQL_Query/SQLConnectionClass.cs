using System;
using System.Text;
using Crestron.SimplSharp;// For Basic SIMPL# Classes
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp.CrestronSockets;
using System.Data.SqlClient;
using Crestron.SimplSharp.CrestronAuthentication;
using Crestron.SimplSharp.Net.Http;

namespace SQL_Query
{
    public class SQLConnectionClass
    {
        public SQLConnectionClass()
        {
        }

        // public override bool ValidateUserInformation(string uname, string pword);

        public ushort UserInfo(string uname, string pword) // processor must have adlogin and an AD User defined in a group
        {
            ushort numBack = 0;
            Authentication.UserInformation myUser = new Authentication.UserInformation();
            myUser = Authentication.ValidateUserInformation(uname, pword);
            CrestronConsole.PrintLine("Results of auth check.. Authenticated = {0}, User = {1}, Access = {2}.", myUser.Authenticated, myUser.UserName, myUser.Access);
            if (myUser.Authenticated)
            {
                numBack = 1;
            }
            else
            {
                numBack = 0;
            }
            return numBack;
        }

        /* TBD
        public string CheckWithLDAP(string uname, string pword, string translatorURL, string LDAPServerURL)
        {
            HttpClient myClient = new HttpClient();
            string postContent = "";
            byte[] postContentBytes = new byte[];
            myClient.PostAsync(translatorURL, postContentBytes);
            myClient.Connect(translatorURL);

        }
        */

        public string GetData(string serverIP, string portNum, string dbName, string nameColumn, string userName, string userPass, string nameToFind)
        {
            try
            {
                string returnString = "";
                string connectString = System.String.Format("Data Source={0}, {1};Integrated Security=true;Initial Catalog={2};User ID={3};Password={4};", serverIP, portNum, dbName, userName, userPass);
                // "Data Source=10.71.4.129, 2301;Integrated Security=true;Initial Catalog=mydata;User ID=dummyforest\\davew;Password=P@ssword;"
                SqlConnection mySQL = new SqlConnection(connectString);
                mySQL.Open();

                // select * from mydata WHERE Name LIKE 'And%'; // looks for all rows where the name starts with 'And'. 
                // string SQLQuery = "SELECT * FROM mydata;"; // gets all data

                string SQLQuery = System.String.Format("SELECT * FROM {0} WHERE {1} LIKE '%{2}%';", dbName, nameColumn, nameToFind);
                SqlCommand myCommand = new SqlCommand(SQLQuery, mySQL);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    returnString = System.String.Format("SQL Result Row = {0}, {1}", myReader.GetString(0), myReader.GetString(1));
                    CrestronConsole.PrintLine("got back = {0}, {1}", myReader.GetString(0), myReader.GetValue(1));
                    return returnString;
                }
            }
            catch (SqlException e)
            {
                CrestronConsole.PrintLine("Exception in GetData = {0}, {1}, {2}, {3}", e.State, e.Errors, e.Procedure, e.Message);
            }
            return "";
        }
    }
}
