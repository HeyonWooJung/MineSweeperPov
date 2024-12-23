using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperPov
{
    struct Mine
    {
        int _nearMines;
        bool _isMine;
        bool _isExplored;
        bool _isPinned;

        public int NearMines
        {
            get { return _nearMines; }
        }

        public bool IsMine
        {
            get { return _isMine; }
        }

        public bool IsExplored
        {
            get { return _isExplored; }
            set { _isExplored = value; }
        }

        public bool IsPinned
        {
            get { return _isPinned; }
            set { _isPinned = value; }
        }

        public void SetMine()
        {
            _isMine = true;
        }

        public void IncreaseNearMines()
        {
            _nearMines++;
        }
    }

    class MineManager
    {
        Mine[,] _map;

        //맵 생성
        public void MakeMap(int size, int mines)
        {
            _map = new Mine[size, size];

            //지뢰 놓기
            for (int i = 0; i < mines; i++)
            {
                int x = Statics.Random.Next(0, size);
                int y = Statics.Random.Next(0, size);
                if (_map[x,y].IsMine || (x == 0 && y == 0))
                {
                    i--;
                    continue;
                }
                _map[x, y].SetMine();
            }

            //주변 지뢰 수 세기
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = i-1; k <= i+1; k++)
                    {
                        for (int l = j-1; l <= j+1; l++)
                        {
                            if (k >= 0 && k < size && l >= 0 && l < size)
                            {
                                if (!(k == i && l == j))
                                {
                                    if (_map[k,l].IsMine)
                                    {
                                        _map[i, j].IncreaseNearMines();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //맵 출력
        public void PrintMap()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    if (_map[i, j].IsMine)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else
                    {
                        switch(_map[i, j].NearMines)
                        {
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case 4:
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                break;
                            case 5:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                            case 6:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case 7:
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                break;
                            case 8:
                                Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                        }
                    }
                    Console.Write($"{_map[i, j].NearMines}");
                    Console.ResetColor();
                    Console.Write(" | ");
                }
                Console.WriteLine();
            }
        }
    }
}
