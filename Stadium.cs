using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall
{
    public class Stadium
    {
        public Stadium(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; } // Ширина стадиона

        public int Height { get; } // Высота стадиона

        public bool IsIn(double x, double y) // Метод для проверки нахождения игрока в пределах поля или вне его приделах
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}
