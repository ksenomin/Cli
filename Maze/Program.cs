using System.Text;
using Maze.GameObjects;

namespace Maze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // Для отображения игрока
            Console.CursorVisible = false;

            Console.WriteLine("Добро пожаловать в игру Лабиринт!");
            Console.WriteLine("Введите размер лабиринта (рекомендуется 5-15): ");

            int size;
            while (!int.TryParse(Console.ReadLine(), out size) || size < 3)
            {
                Console.WriteLine("Введите число больше 2: ");
            }

            GameLogic game = new GameLogic(size, size);
            game.Run();
        }
    }
}
