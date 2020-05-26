using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTakToe_db_
{
    interface IPlayer
    {
        Move MakeMove(char Who, char[,] field, int Size);
    }
    class BotPlayer : IPlayer
    {
        public Move MakeMove(char Who, char[,] field, int Size)
        {
            int X, Y;
            Random rnd = new Random();
            X = rnd.Next() % Size + 1;
            Y = rnd.Next() % Size + 1;
            return new Move(X, Y);
        }
    }
    class ConsolePlayer : IPlayer
    {
        public Move MakeMove(char Who, char[,] field, int Size)
        {
            int X, Y;
            Console.WriteLine("Enter coordinates from 1 to " + Size);
            X = int.Parse(Console.ReadLine());
            Y = int.Parse(Console.ReadLine());
            return new Move(X, Y);
        }
    }
    class Move
    {
        int X, Y;
        public int x
        {
            get
            {
                return X;
            }
        }
        public int y
        {
            get
            {
                return Y;
            }
        }
        public Move(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

    }

}
