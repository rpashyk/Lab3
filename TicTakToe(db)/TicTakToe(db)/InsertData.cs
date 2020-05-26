using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConn;
using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;

namespace TicTakToe_db_
{
    class InsertData
    {
        public void InsertGame(MySqlConnection conn, string playerX, string player0)
        {
            string sql = "Insert into game(PlayerX_name, Player0_name) "
                                            + " values (@playerX, @player0)";
            

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            
            MySqlParameter playerX_name = new MySqlParameter("@playerX", SqlDbType.VarChar);
            cmd.Parameters.Add("@playerX", MySqlDbType.VarChar).Value = playerX;
            
            cmd.Parameters.Add("@player0", MySqlDbType.VarChar).Value = player0;

            int rowCount = cmd.ExecuteNonQuery();
            //Console.WriteLine("Row Count affected = " + rowCount);
        }

        public void InsertStatusLog(MySqlConnection conn, string Status)
        {
            string sql = "Insert into game_status_log(game_id, game_status) "
                                             + " values (@id, @status) ";

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            QueryData query = new QueryData();

            MySqlParameter id = new MySqlParameter("@id", SqlDbType.VarChar);
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = query.QueryIdGame(conn);

            cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = Status;
            
            int rowCount = cmd.ExecuteNonQuery();
            //Console.WriteLine("Row Count affected = " + rowCount);
        }

        public void InsertStats(MySqlConnection conn, string playerX, string player0)
        {
            string sql = "Insert into game_stats(game_id, playerX_win, player0_win) "
                                             + " values (@id, @playerX_win, @player0_win)";

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            QueryData query = new QueryData();

            MySqlParameter Id = new MySqlParameter("@id", SqlDbType.VarChar);
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = query.QueryIdGame(conn);

            cmd.Parameters.Add("@playerX_win", MySqlDbType.VarChar).Value = playerX;
                      
            cmd.Parameters.Add("@player0_win", MySqlDbType.VarChar).Value = player0;
          
            int rowCount = cmd.ExecuteNonQuery();
           //  Console.WriteLine("Row Count affected = " + rowCount);
        }
    }

}