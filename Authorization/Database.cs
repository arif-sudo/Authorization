using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Authorization
{
    internal class Database
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;username=root;database=c#test");
        
        public void openConnect()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open(); 
            }
        }

        public void closeConnect()
        {
            if (connection.State == System.Data.ConnectionState.Open) 
            { 
                connection.Close();
            }
        }

        public MySqlConnection getConnection() 
        {
            return connection;
        }

    }
}
