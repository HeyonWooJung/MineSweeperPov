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
            bool isGameRunning = false;
            bool runProgram = true;
            ConsoleKeyInfo input;

            Stopwatch tick = new Stopwatch(); //초 세기
            Stopwatch watch = new Stopwatch(); //경과 시간

            StartScreen();
            while (runProgram)
            {
                Console.Clear();
                Console.CursorVisible = true;
                _player.ResetPlayer();
                while (difficultSelect)
                {
                    Console.WriteLine("난이도를 선택해주세요\n" +
                        "1.초급  2. 중급  3. 고급  \nESC: 프로그램 종료");
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            _mineManager.MakeMap(9, 10);
                            difficultSelect = false;
                            isGameRunning = true;
                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            _mineManager.MakeMap(12, 20);
                            difficultSelect = false;
                            isGameRunning = true;
                            break;
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            _mineManager.MakeMap(16, 40);
                            difficultSelect = false;
                            isGameRunning = true;
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            Console.WriteLine("프로그램을 종료합니다.");
                            runProgram = false;
                            difficultSelect = false;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("잘못 입력하셨습니다");
                            difficultSelect = true;
                            break;
                    }
                }

                if (isGameRunning)
                {

                    _mineManager.UnveilNearby(0, 0); //시작지점 밝혀주기
                    _mineManager.PrintMap(false);
                    _player.SetLimits(_mineManager.MapSizeX(), _mineManager.MapSizeY());
                    Console.CursorVisible = false;

                    Console.SetCursorPosition(0, 0);
                    Console.SetCursorPosition(0, _mineManager.MapSizeY() + 1);
                    Console.WriteLine("아무 키를 누르면 시작합니다.");
                    Console.ReadKey(true);

                    tick.Start();
                    watch.Start();

                    DrawInfo(_mineManager.MapSizeY(), watch);
                    _player.Draw();
                    Console.SetCursorPosition(0, _mineManager.MapSizeY() + 1);
                    Console.WriteLine("WASD 또는 방향키: 이동 | Space: 깃발 세우기 상태 | 깃발 세우기 상태에서 이동: 해당 방향으로 깃발 세우기 | ESC: 종료");
                }

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
                                //핀 꽂게 할거임 스페이스 누르고 방향
                                _player.SetPin();
                                break;
                            case ConsoleKey.Escape:
                                isGameRunning = false;
                                break;
                        }
                        _mineManager.UnveilNearby(_player.GetX(), _player.GetY());
                        _mineManager.PrintMap(false);
                        _player.Draw();
                        DrawInfo(_mineManager.MapSizeY(), watch);
                        //ESC 종료
                        if (isGameRunning == false)
                        {
                            Console.Clear();
                            Console.WriteLine("ESC를 눌러 게임이 종료되었습니다.");
                            break;
                        }
                    }

                    //초마다 시간 바꾸기
                    if (tick.ElapsedMilliseconds >= 1000)
                    {
                        DrawInfo(_mineManager.MapSizeY(), watch);
                    }
                    //종료조건 확인
                    isGameRunning = SetGameRun();
                }

                //완전 종료가 아니면
                if (runProgram)
                {
                    //종료 시 소요시간 보여주기
                    if (watch.IsRunning)
                    {
                        Console.WriteLine("소요시간: " + (watch.ElapsedMilliseconds / 1000).ToString("D3"));
                        watch.Stop();
                        tick.Stop();
                    }
                    Console.WriteLine("아무 키나 눌러 계속합니다.");
                    Console.ReadKey(true);
                    watch.Reset();
                    tick.Reset();
                    difficultSelect = true;
                }
            }
        }

        //시간초, 지뢰 수 표기
        public void DrawInfo(int height, Stopwatch watch)
        {
            Console.SetCursorPosition(0, height + 2);
            Console.Write($"Time {(watch.ElapsedMilliseconds / 1000).ToString("D3")}");
            //중간 공백 계산
            int x = _mineManager.MapSizeX() * 4 - 16;//가로 사이즈 받아서 * 4(지뢰 한 칸 표현하는데 4칸이 필요) + 1(시작점 공백) - 17(시간표시 8칸, 지뢰수 표시 9칸)
            for (int i = 0; i < x; i++)
            {
                Console.Write(" ");
            }
            Console.Write($"Mines {_mineManager.RemainMines.ToString("D3")}");
        }

        //종료여부 확인
        public bool SetGameRun()
        {
            if (_mineManager.IsEverythingSearched())
            {
                Console.Clear();
                _mineManager.PrintMap(true);
                Console.WriteLine("모든 칸을 밝혔습니다");
                Console.WriteLine("찾아낸 지뢰 수: " + _mineManager.FoundMines());
                return false;
            }
            if (_mineManager.IsEveryMinePinned())
            {
                Console.Clear();
                _mineManager.PrintMap(true);
                Console.WriteLine("모든 지뢰를 찾았습니다");
                return false;
            }
            Mine playerMine = _mineManager.GetMine(_player.GetX(), _player.GetY());
            if (playerMine.IsPinned == false && playerMine.IsMine)
            {
                Console.Clear();
                _mineManager.PrintMap(true);
                Console.WriteLine("\n지뢰를 밟아 패배했습니다.\n찾아낸 지뢰 수: " + _mineManager.FoundMines() + "  ");
                return false;
            }
            return true;
        }

        public void StartScreen()
        {
            Console.WriteLine("   ▄▄▄▄███▄▄▄▄    ▄█  ███▄▄▄▄      ▄████████                                             \r\n ▄██▀▀▀███▀▀▀██▄ ███  ███▀▀▀██▄   ███    ███                                             \r\n ███   ███   ███ ███▌ ███   ███   ███    █▀                                              \r\n ███   ███   ███ ███▌ ███   ███  ▄███▄▄▄                                                 \r\n ███   ███   ███ ███▌ ███   ███ ▀▀███▀▀▀                                                 \r\n ███   ███   ███ ███  ███   ███   ███    █▄                                              \r\n ███   ███   ███ ███  ███   ███   ███    ███                                             \r\n  ▀█   ███   █▀  █▀    ▀█   █▀    ██████████                                             \r\n                                                                                         \r\n   ▄████████  ▄█     █▄     ▄████████    ▄████████    ▄███████▄    ▄████████    ▄████████\r\n  ███    ███ ███     ███   ███    ███   ███    ███   ███    ███   ███    ███   ███    ███\r\n  ███    █▀  ███     ███   ███    █▀    ███    █▀    ███    ███   ███    █▀    ███    ███\r\n  ███        ███     ███  ▄███▄▄▄      ▄███▄▄▄       ███    ███  ▄███▄▄▄      ▄███▄▄▄▄██▀\r\n▀███████████ ███     ███ ▀▀███▀▀▀     ▀▀███▀▀▀     ▀█████████▀  ▀▀███▀▀▀     ▀▀███▀▀▀▀▀  \r\n         ███ ███     ███   ███    █▄    ███    █▄    ███          ███    █▄  ▀███████████\r\n   ▄█    ███ ███ ▄█▄ ███   ███    ███   ███    ███   ███          ███    ███   ███    ███\r\n ▄████████▀   ▀███▀███▀    ██████████   ██████████  ▄████▀        ██████████   ███    ███\r\n                                                                               ███    ███\r\n   ▄███████▄  ▄██████▄   ▄█    █▄                                                        \r\n  ███    ███ ███    ███ ███    ███                                                       \r\n  ███    ███ ███    ███ ███    ███                                                       \r\n  ███    ███ ███    ███ ███    ███                                                       \r\n▀█████████▀  ███    ███ ███    ███                                                       \r\n  ███        ███    ███ ███    ███                                                       \r\n  ███        ███    ███ ███    ███                                                       \r\n ▄████▀       ▀██████▀   ▀██████▀                                                        ");
            Console.WriteLine("\n게임을 시작하려면 아무 키나 누르세요");
            Console.ReadKey(false);
            Console.Clear();
        }
    }
}
