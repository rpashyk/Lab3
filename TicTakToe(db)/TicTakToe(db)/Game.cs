using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConn;
using MySql.Data.MySqlClient;

namespace TicTakToe_db_
{
    class Game
    {
        IPlayer playerX, player0;
        int Size = 3;
        int step;
        char[,] field;

        public Game(IPlayer playerX, IPlayer player0)
        {
            this.playerX = playerX;
            this.player0 = player0;
        }

        char WhoFirst()
        {
            char first;
            do
            {
                Console.Write("Who will go first?");
                Console.Write("\n X - if you\n 0 - if bot\n --- ");
                first = char.Parse(Console.ReadLine());
            } while (first != 'X' && first != '0');
            Console.WriteLine();
            return first;
        }

        public void Start(MySqlConnection conn, int mode, int[] stats)
        {
            Console.Clear();
            InsertData insertData = new InsertData();
            insertData.InsertStatusLog(conn, "STARTED");
            Console.WriteLine("STARTED");

            field = new char[Size, Size];
            for (int i = 0; i < Size; i++) //заполнение массива пробелами
            {
                for (int j = 0; j < Size; j++)
                {
                    field[i, j] = ' ';
                }
            }
            int first = 1; //при 1 - первым ходит игрок, при 0 - бот
            if (mode == 1 && WhoFirst() == '0') first = 0;
            bool flag = true;
            step = 1;
            Field();//чертим пустое поле

            do
            {
                Console.WriteLine("Step: " + step);
                if (step % 2 == first)
                {
                    Move move;
                    do
                    {
                        move = playerX.MakeMove('X', field, Size);
                    } while (move.x > Size || move.x < 1 || move.y > Size || move.y < 1 || field[move.x - 1, move.y - 1] != ' ');
                    field[move.x - 1, move.y - 1] = 'X';
                }
                else
                {
                    Move move;
                    do
                    {
                        move = player0.MakeMove('0', field, Size);
                    } while (move.x > Size || move.x < 1 || move.y > Size || move.y < 1 || field[move.x - 1, move.y - 1] != ' ');
                    field[move.x - 1, move.y - 1] = '0';
                }
                Field(); //Чертим поле после хода
                flag = GameOver(conn, stats); //поиск победителя
                step++;
            } while (step <= Size * Size && flag); //пока не превышено число ходов или кто-то из игроков не победил
            Console.Write("FINISHED. ");
            insertData.InsertStatusLog(conn, "FINISHED");
            Console.WriteLine("Tap to continue..."); Console.ReadKey();
        }

        void Field()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(" " + field[i, j] + " ");
                    if (j < Size - 1) Console.Write("|");
                }
                Console.WriteLine();

                for (int j = 0; j < Size; j++)
                {
                    if (i < Size - 1)
                    {
                        Console.Write("---");
                        if (j < Size - 1) Console.Write("+");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        bool GameOver(MySqlConnection conn, int[] stats)
        {
            InsertData insertData = new InsertData();

            char winner = Winner(conn);
            bool flag = true;
            if (winner == 'n')
            {
                insertData.InsertStats(conn, "drawn", "drawn");
                Console.WriteLine("Drawn Game\n");
                stats[1]++;
            }
            else if (winner == '0' || winner == 'X')
            {
                if (winner == '0') stats[2]++;
                else stats[0]++;
                Console.WriteLine("Player" + winner + " winner\n");
                flag = false;

                if (winner == 'X')
                {
                    insertData.InsertStats(conn, "win", "lose");
                }
                else
                {
                    insertData.InsertStats(conn, "lose", "win");
                }
            }
            Console.ResetColor();

            return flag;
        }

        char Winner(MySqlConnection conn)
        {
            char winner = ' '; // ' ' - нет победителя
            for (int i = 0; i < Size; i++) //по горизонтали
            {
                int j;
                winner = field[i, 0];
                for (j = 0; j < Size; j++)
                {
                    if (field[i, j] != winner) break;
                }
                if (j == Size)
                {
                    return winner;
                }
            }

            for (int j = 0; j < Size; j++) //по вертикали
            {
                int i;
                winner = field[0, j];
                for (i = 0; i < Size; i++)
                {
                    if (field[i, j] != winner) break;
                }
                if (i == Size)
                {
                    return winner;
                }
            }

            for (int i = 1; i < Size; i++) //по главной диагонали
            {
                winner = field[0, 0];
                if (field[i, i] != winner)
                {
                    winner = ' ';
                    break;
                }
                if (i == Size - 1)
                {
                    return winner;
                }
            }

            for (int i = 1; i < Size; i++) //по побочной диагонали
            {
                winner = field[0, Size - 1];
                if (field[i, Size - i - 1] != winner)
                {
                    winner = ' ';
                    break;
                }
                if (i == Size - 1)
                {
                    return winner;
                }
            }

            if (step == Size * Size) // ' ' - ничья
            {
                winner = 'n';
            }

            return winner;
        }
    }
}
