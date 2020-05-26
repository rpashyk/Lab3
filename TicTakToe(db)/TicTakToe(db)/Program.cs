using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConn;
using MySql.Data.MySqlClient;

namespace TicTakToe_db_
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = DBUtils.GetDBConnection();

            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();

                Console.WriteLine("Connection successful!");

                Console.ReadKey();

                TicTakToe_db_.MainMenu menu = new TicTakToe_db_.MainMenu();
                menu.Menu(conn); //меню
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
    }
}
