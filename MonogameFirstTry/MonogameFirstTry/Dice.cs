using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    class Dice
    {
        private static readonly Random dice = new Random();

        public int RollDice(int m, int M)
        {
            lock (dice) // synchronize
            {
                return dice.Next(m, M);
            }
        }
    }
}
