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
            
            for (int i = 0; i < mines; i++)
            {
                _map[random.Next(0, size + 1), random.Next(0, size + 1)].IsMine = true;
            }
        }

        //맵 출력(테스트용)
        public void PrintMap()
        {
            for(int i = 0; i < _map.GetLength(1);i++)
            {
                for(int j=0;  j < _map.GetLength(1); j++)
                {
                    Console.Write($"{_map[i,j].NearMines} {_map[i, j].IsMine} {_map[i, j].IsExplored} | ");
                }
                Console.WriteLine();
            }
        }
    }
}
