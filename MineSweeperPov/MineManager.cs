using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperPov
{
    struct Mine
    {
        int _x;
        int _y;
        int _nearMines;
        bool _isMine;
        bool _isExplored;
        bool _isPinned;

        public int NearMines
        {
            get { return _nearMines; }
        }

        public int X
        {
            get { return _x; }
        }
        public int Y
        {
            get { return _y; }
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

        public void SetXY(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }

    class MineManager
    {
        Mine[,] _map; //전체 맵
        Mine[] _mineArr; //지뢰 저장용

        int _remainMines = 0;

        public int RemainMines
        {
            get { return _remainMines; }
            set { _remainMines = value; }
        }

        public Mine GetMine(int x, int y)
        {
            return _map[y, x];
        }

        //깃발 세우기
        public void SetPin(int x, int y)
        {
            if (y >= 0 && y < _map.GetLength(0) && x >= 0 && x < _map.GetLength(1))
            {
                if (_remainMines > 0)
                {
                    if (_map[y, x].IsPinned)
                    {
                        _remainMines++;
                    }
                    else
                    {
                        _remainMines--;
                    }
                    _map[y, x].IsPinned = !_map[y, x].IsPinned;
                }
                else
                {
                    if (_map[y, x].IsPinned)
                    {
                        _remainMines++;
                        _map[y, x].IsPinned = !_map[y, x].IsPinned;
                    }
                }
            }
        }

        //맵 크기 xy 반환
        public int MapSizeX()
        {
            return _map.GetLength(1);
        }

        public int MapSizeY()
        {
            return _map.GetLength(0);
        }

        //찾아낸 지뢰 개수 세기
        public int FoundMines()
        {
            int founded = 0;
            foreach (var mine in _mineArr)
            {
                if (_map[mine.Y, mine.X].IsPinned == true)
                {
                    founded++;
                }
            }
            return founded;
        }

        //맵 생성
        public void MakeMap(int size, int mines)
        {
            _map = new Mine[size, size];
            _mineArr = new Mine[mines];
            _remainMines = mines;

            //지뢰 놓기
            for (int i = 0; i < mines; i++)
            {
                int x = Statics.Random.Next(0, size);
                int y = Statics.Random.Next(0, size);
                if (_map[y, x].IsMine || (x == 0 && y == 0))
                {
                    i--;
                    continue;
                }
                _map[y, x].SetMine();
                _mineArr[i].SetXY(x, y);
            }

            //주변 지뢰 수 세기
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for (int l = j - 1; l <= j + 1; l++)
                        {
                            if (k >= 0 && k < size && l >= 0 && l < size)
                            {
                                if (!(k == i && l == j))
                                {
                                    if (_map[k, l].IsMine)
                                    {
                                        _map[i, j].IncreaseNearMines();
                                    }
                                }
                            }
                        }
                    }
                    _map[i, j].SetXY(j, i);
                }
            }
        }

        //맵 출력
        public void PrintMap(bool isEnd)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                Console.Write(" | ");
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    /*if (_map[i, j].IsMine)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("*");
                    }*/
                    if (_map[i, j].IsPinned)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("P");
                    }
                    else if (_map[i, j].IsMine && isEnd)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("*");
                    }
                    else if (_map[i, j].IsExplored == false)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("?");
                    }
                    else
                    {
                        switch (_map[i, j].NearMines)
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
                        Console.Write($"{_map[i, j].NearMines}");
                    }
                    Console.ResetColor();
                    Console.Write(" | ");
                }
                Console.WriteLine();
            }
        }

        //주변 8칸 밝히기
        public void UnveilNearby(int x, int y)
        {
            for (int k = y - 1; k <= y + 1; k++)
            {
                for (int l = x - 1; l <= x + 1; l++)
                {
                    if (k >= 0 && k < _map.GetLength(0) && l >= 0 && l < _map.GetLength(1))
                    {
                        //1차배열이 y임
                        _map[k, l].IsExplored = true;
                        //상하좌우의 빈 공간 밝히기
                        if (k == y || l == x)
                        {
                            UnveilEmptys(l, k);
                        }
                    }
                }
            }
        }

        //주변 빈 공간 밝히기
        public void UnveilEmptys(int x, int y)
        {
            //Stack<Mine> mines = new Stack<Mine>();
            Queue<Mine> mines = new Queue<Mine>();

            Mine mine; //반복문에서 값 가질 친구

            //mines.Push(_map[y, x]); //첫 부분 넣어주기 (xy자리 반대로 넣은 이유는 2차배열에서 왼쪽이 y임)
            mines.Enqueue(_map[y, x]); //첫 부분 넣어주기 (xy자리 반대로 넣은 이유는 2차배열에서 왼쪽이 y임)

            //BFS 짭
            while (mines.Any())
            {
                //mine = mines.Pop();
                mine = mines.Dequeue();
                _map[mine.Y, mine.X].IsExplored = true;

                //주변의 지뢰 수가 0이면 상하좌우의 아직 밝혀지지 않은 것들을 큐에 집어넣음
                if (mine.NearMines == 0)
                {
                    if (mine.X - 1 >= 0 && _map[mine.Y, mine.X - 1].NearMines == 0 && _map[mine.Y, mine.X - 1].IsExplored == false)
                    {
                        //mines.Push(_map[mine.Y, mine.X - 1]);
                        mines.Enqueue(_map[mine.Y, mine.X - 1]);
                    }
                    if (mine.X + 1 < _map.GetLength(1) && _map[mine.Y, mine.X + 1].NearMines == 0 && _map[mine.Y, mine.X + 1].IsExplored == false)
                    {
                        //mines.Push(_map[mine.Y, mine.X + 1]);
                        mines.Enqueue(_map[mine.Y, mine.X + 1]);
                    }

                    if (mine.Y - 1 >= 0 && _map[mine.Y - 1, mine.X].NearMines == 0 && _map[mine.Y - 1, mine.X].IsExplored == false)
                    {
                        //mines.Push(_map[mine.Y - 1, mine.X]);
                        mines.Enqueue(_map[mine.Y - 1, mine.X]);
                    }
                    if (mine.Y + 1 < _map.GetLength(0) && _map[mine.Y + 1, mine.X].NearMines == 0 && _map[mine.Y + 1, mine.X].IsExplored == false)
                    {
                        //mines.Push(_map[mine.Y + 1, mine.X]);
                        mines.Enqueue(_map[mine.Y + 1, mine.X]);
                    }
                }
            }
        }

        //모든 칸을 밝혔는가
        public bool IsEverythingSearched()
        {
            foreach (var mine in _map)
            {
                if (mine.IsExplored == false)
                {
                    return false;
                }
            }
            return true;
        }

        //모든 지뢰에 깃발이 꽂혔는가
        public bool IsEveryMinePinned()
        {
            foreach (var mine in _mineArr)
            {
                if (_map[mine.Y, mine.X].IsPinned == false)
                {

                    return false;
                }
            }
            return true;
        }
    }
}
