using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace InstationFinalVersion
{
    public class CBase
    {
        public string ConnectionString()
        {
            
            string server = "34.91.123.237";
            string database = "instation";
            string uid = "admin";
            string password = "instation*-*";
            string connectionString;

            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";


            return connectionString;
        }
    }
}
