using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            ConsoleKeyInfo input;

            Stopwatch tick = new Stopwatch();
            Stopwatch watch = new Stopwatch();

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

            tick.Start();
            watch.Start();
            _mineManager.UnveilNearby(0, 0);
            _mineManager.PrintMap(false);
            _player.SetLimits(_mineManager.MapSizeX(), _mineManager.MapSizeY());
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            DrawInfo(_mineManager.MapSizeY(), watch);

            while (isGameRunning)
            {
                //키가 눌렸을때만 작동
                if (Console.KeyAvailable)
                {
                    input = Console.ReadKey(true);
                    switch (input.Key)
                    {
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                            if (_player.IsPinning)
                            {
                                _mineManager.SetPin(_player.GetX() - 1, _player.GetY());
                                _player.SetPin();
                            }
                            else
                            {
                                _player.Move(-4, 0);
                            }
                            break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            if (_player.IsPinning)
                            {
                                _mineManager.SetPin(_player.GetX(), _player.GetY() - 1);
                                _player.SetPin();
                            }
                            else
                            {
                                _player.Move(0, -1);
                            }
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            if (_player.IsPinning)
                            {
                                _mineManager.SetPin(_player.GetX(), _player.GetY() + 1);
                                _player.SetPin();
                            }
                            else
                            {
                                _player.Move(0, 1);
                            }
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            if (_player.IsPinning)
                            {
                                _mineManager.SetPin(_player.GetX() + 1, _player.GetY());
                                _player.SetPin();
                            }
                            else
                            {
                                _player.Move(4, 0);
                            }
                            break;
                        case ConsoleKey.Spacebar:
                            //핀 꽂게 할거임 스페이스 누른 상태로 방향?
                            _player.SetPin();
                            break;
                    }
                    //_mineManager.UnveilEmptys(_player.GetX(), _player.GetY());
                    _mineManager.UnveilNearby(_player.GetX(), _player.GetY());
                    _mineManager.PrintMap(false);
                    _player.Draw();
                    DrawInfo(_mineManager.MapSizeY(), watch);
                }

                //얘는 계속 돌아감
                if(tick.ElapsedMilliseconds >= 1000)  
                {
                    //계속 업데이트 해야되는 거 넣기
                    DrawInfo(_mineManager.MapSizeY(), watch);
                }

                isGameRunning = SetGameEnd();
            }
            Console.WriteLine("소요시간: " + (watch.ElapsedMilliseconds / 1000).ToString("D3"));
        }

        public void DrawInfo(int height, Stopwatch watch)
        {
            Console.SetCursorPosition(0, height + 2);
            Console.Write($"Time {(watch.ElapsedMilliseconds/1000).ToString("D3")}");
            //중간 공백 계산
            int x = _mineManager.MapSizeX() * 4 - 16;//가로 사이즈 받아서 * 4(지뢰 한 칸 표현하는데 4칸이 필요) + 1(시작점 공백) - 17(시간표시 8칸, 지뢰수 표시 9칸)
            for (int i = 0; i < x; i++)
            {
                Console.Write(" ");
            }
            Console.Write($"Mines {_mineManager.RemainMines.ToString("D3")}");
        }

        public bool SetGameEnd()
        {
            if (_mineManager.IsEverythingSearched() || _mineManager.IsEveryMinePinned())
            {
                Console.Clear();
                Console.WriteLine("다 찾음");
                return false;
            }
            Mine playerMine = _mineManager.GetMine(_player.GetX(), _player.GetY());
            if (playerMine.IsPinned == false && playerMine.IsMine)
            {
                Console.Clear();
                _mineManager.PrintMap(true);
                Console.WriteLine("지뢰 밟음");
                return false;
            }
            return true;
        }
    }
}
