using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperPov
{
    internal class Player
    {
        int _posX = 0;
        int _posY = 0;
        int _xLimit = 0;
        int _yLimit = 0;
        char _sprite = '▶';

        public int GetX()
        {
            if ((_posX / 4) - 1 <= 0)
            {
                return 0;
            }
            return (_posX / 4) - 1;
        }

        public int GetY()
        {
            return _posY;
        }

        public void SetLimits(int x, int y)
        {
            if (x <= 0)
            {
                _xLimit = 4;
            }
            else
            {
                _xLimit = x * 4;
            }
            _yLimit = y;
        }

        //x = 4씩 y = 1씩
        public void Move(int x, int y)
        {
            _posX += x;
            _posY += y;

            if (_posX <= 0)
            {
                _posX = 4;
            }

            if (_posX > _xLimit)
            {
                _posX = _xLimit;
            }

            if (_posY < 0)
            {
                _posY = 0;
            }

            if (_posY >= _yLimit)
            {
                _posY = _yLimit - 1;
            }


            if (x < 0)
            {
                _sprite = '◀';
            }

            if (x > 0)
            {
                _sprite = '▶';
            }

            if (y < 0)
            {
                _sprite = '▲';
            }

            if (y > 0)
            {
                _sprite = '▼';
            }
        }

        public void Draw()
        {
            if (_posX > 0)
            {
                Console.SetCursorPosition(_posX - 1, _posY);
            }
            else
            {
                Console.SetCursorPosition(3, _posY);
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(_sprite);
            Console.ResetColor();
        }
    }
}
