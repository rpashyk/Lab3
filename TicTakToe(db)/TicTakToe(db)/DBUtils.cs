using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SqlConn
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "127.0.0.1"; //192.168.205.130  127.0.0.1 [::1]
            int port = 3306;
            string database = "tictactoe";
            string username = "root";
            string password = "Go0c72J1RR97ZXug"; //Go0c72J1RR97ZXug

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}

