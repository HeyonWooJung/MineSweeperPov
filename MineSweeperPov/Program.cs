using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperPov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MineManager mineManager = new MineManager();
            Random rnd = new Random();
            mineManager.MakeMap(9, 10, rnd);
            mineManager.PrintMap();
        }
    }
}
