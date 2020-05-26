using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConn;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace TicTakToe_db_
{
    class QueryData
    {
        public int QueryIdGame(MySqlConnection conn)
        {
            string sql = "select Max(Id) from game";

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = conn;
            cmd.CommandText = sql;
            int Id = 0;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Id = int.Parse(reader.GetString(0));
                    }
                }
            }
            return Id;
        }
    }
}
