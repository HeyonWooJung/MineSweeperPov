using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperPov
{
    class GameManager
    {
        MineManager _mineManager;
        Player _player;

        public GameManager(MineManager mineManager, Player player)
        {
            _mineManager = mineManager;
            _player = player;
        }

        public void GameStart()
        {
            bool difficultSelect = true;
            bool isGameRunning = true;

            while (difficultSelect)
            {

                Console.WriteLine("난이도를 선택해주세요\n" +
                    "1.초급  2. 중급  3. 고급");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        _mineManager.MakeMap(9, 10);
                        difficultSelect = false;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        _mineManager.MakeMap(12, 20);
                        difficultSelect = false;
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        _mineManager.MakeMap(16, 40);
                        difficultSelect = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못 입력하셨습니다");
                        difficultSelect = true;
                        break;
                }
            }
            Console.Clear();
            while (isGameRunning)
            {
                _mineManager.PrintMap();
                Console.ReadLine();
            }
        }
    }
}
