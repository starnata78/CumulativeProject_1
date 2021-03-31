using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//MySQL.Data was installed

using MySql.Data.MySqlClient;

namespace CumulativeProject_1.Models
{
    public class SchoolDbContext
    {
        //Here we change "secrete" properties to match my won local school database

        private static string User { get { return "root";} }
        private static string Password { get { return ""; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //The following series of credentials are called "ConnectionString" and used to connect to the database

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
                    
            }
        }
        //This methods is used to get the database.
        ///<summary>
        ///Returns a connection to the schooldb database.
        ///</summary>
        ///<example>
        ///private SchoolDbContext SchoolDb = new SchoolDbContext();
        ///MySqlConnection Conn = SchoolDb.AccessDatabase();
        ///</example>
        ///<returns> A MySqlConnection Object</returns>
        
        public MySqlConnection AccessDatabase()
        {
            //MySqlConnection Class is installed to create an object
            //the object is a specific connection to our blog database on port 3306 of localhost

            return new MySqlConnection(ConnectionString);
        }
        

    }
}