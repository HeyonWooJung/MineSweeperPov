using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperPov
{
    static class Statics
    {
        public static Random Random = new Random();
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            MineManager mineManager = new MineManager();
            Player player = new Player();
            GameManager gameManager = new GameManager(mineManager, player);

            gameManager.GameStart();
        }
    }
}
