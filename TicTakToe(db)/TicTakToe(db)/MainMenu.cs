using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConn;
using MySql.Data.MySqlClient;

namespace TicTakToe_db_
{
    class MainMenu
    {
        int Size = 3;
        public int[] stats = new int[3] { 0, 0, 0 };
        public void Menu(MySqlConnection conn)
        {
            InsertData insertData = new InsertData();
            Console.Clear();
            int number;
            Console.Write("                 Menu                                                                                 program by rPashyk"
                + "\n1. With bot (" + Size + "x" + Size + ")\n2. With another player (" + Size + "x" + Size
                + ")\n3. View stats\n4. Change size\n5. Close game\n Choose number: ");
            do
            {
                number = int.Parse(Console.ReadLine());
                if (number < 1 || number > 5) Console.WriteLine("Wrong number\n\nChoose number: ");
            } while (number < 1 || number > 5);
            if (number == 1 || number == 2)
            {
                Game game;
                if (number == 1) game = new Game(new ConsolePlayer(), new BotPlayer());
                else game = new Game(new ConsolePlayer(), new ConsolePlayer());
                insertData.InsertGame(conn, "X", "0");
                insertData.InsertStatusLog(conn, "CREATED");
                game.Start(conn, number, stats);
                Menu(conn);
            }
            else if (number == 3) Stats(conn);
            else if (number == 4)
            {
                Console.Clear();
                Console.Write("Enter number: ");
                Size = int.Parse(Console.ReadLine());
                Menu(conn);
            }
            else
            {
                Console.Clear();
                Console.Write("\n                                                        Good Game                                    program by rPashyk\n");
            }
        }

        void Stats(MySqlConnection conn)
        {
            Console.Clear();
            Console.Write("                 Stats\n"
                + "\tX Wins\tDraws\t0 Wins\n" + "\t   " + stats[0] + "\t  " + stats[1] + "\t   " + stats[2] + "\nTap to continue...");
            Console.ReadKey();
            Menu(conn);
        }
    }
}

