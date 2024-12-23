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

        public int NearMines
        {
            get { return _nearMines; }
            set { _nearMines = value; }
        }

        public bool IsMine
        {
            get { return _isMine; }
            set { _isMine = value; }
        }

        public bool IsExplored
        {
            get { return _isExplored; }
            set { _isExplored = value; }
        }

    }

    class MineManager
    {
        Mine[,] _map;

        //맵 생성
        public void MakeMap(int size, int mines, Random random)
        {
            _map = new Mine[size, size];

            //지뢰 놓기
            for (int i = 0; i < mines; i++)
            {
                int x = random.Next(0, size);
                int y = random.Next(0, size);
                if (_map[x,y].IsMine || (x == 0 && y == 0))
                {
                    i--;
                    continue;
                }
                _map[x, y].IsMine = true;
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
                                        _map[i, j].NearMines++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //맵 출력(테스트용)
        public void PrintMap()
        {
            for (int i = 0; i < _map.GetLength(1); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    if (_map[i, j].IsMine)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
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
